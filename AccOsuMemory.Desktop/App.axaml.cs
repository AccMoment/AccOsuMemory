using System;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.Utils;
using AccOsuMemory.Desktop.ViewModels;
using AccOsuMemory.Desktop.Views;
using Avalonia;
using Avalonia.Controls;
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
                    services.AddTransient<HitTestPageViewModel>();
                    services.AddTransient<SearchPageViewModel>();
                    services.AddTransient<BatchDownloadPageViewModel>();
                    services.AddTransient<BackupPageViewModel>();

                    // Others
                    services.AddSingleton<DownloadManager>();
                    services.AddSingleton(DownloadManager.HttpClient);
                    services.AddAutoMapper((provider, expression) =>
                    { 
                    },typeof(MapperProfile));
                    services.AddLogging();
                })
                .Build();
            
            base.RegisterServices();
        }

        private static void InitService(IServiceProvider provider)
        {
            //单例，先进行初始化，不然在进入任务列表之前所有的下载都无效
            _ = provider.GetRequiredService<TaskPageViewModel>();
        }
        
        
        public override async void OnFrameworkInitializationCompleted()
        {
            await AppHost!.StartAsync();
            InitService(AppHost.Services);
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = AppHost.Services.GetRequiredService<MainWindowViewModel>()
                };
                desktop.ShutdownRequested +=async (_, _) =>
                {
                    try
                    {
                        await AppHost.StopAsync();
                        AppHost.Dispose();
                        AppHost = null;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }

                    // 停 500 ms
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}