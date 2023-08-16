using System;
using System.IO;
using System.Net.Http;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.ViewModels;
using AccOsuMemory.Desktop.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccOsuMemory.Desktop
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void RegisterServices()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    /*Setting*/
                    services.AddOptions().Configure<AppSettings>(config);

                    /*Service*/
                    services.AddSingleton<ISayoApiService, SayoApiService>();
                    services.AddSingleton<IFileProvider, FileProvider>();

                    /*ViewModel*/
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<HomePageViewModel>();
                    services.AddSingleton<TaskPageViewModel>();

                    // Others
                    services.AddSingleton<DownloadManager>();
                })
                .Build();


            base.RegisterServices();
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            await AppHost!.StartAsync();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = AppHost.Services.GetRequiredService<MainWindowViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}