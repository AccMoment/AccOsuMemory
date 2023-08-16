using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Utils;
using AccOsuMemory.Desktop.Model;
using AccOsuMemory.Desktop.Services;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _viewModelBase;

    [ObservableProperty] private ObservableCollection<Page> _pageModels;

    public MainWindowViewModel(IFileProvider fileProvider) : base(fileProvider)
    {
        // _viewModelBase = _appHost?.Services.GetRequiredService<HomePageViewModel>();
        _pageModels = new ObservableCollection<Page>
        {
            new("HomePage", "主页"),
            new("SearchPage", "搜索歌曲"),
            new("DownloadPage", "批量下载"),
            new("TaskPage", "任务列表")
        };
        AppSettingsWriter.Write(nameof(AppSettings.ApiV1Key), "nmsl");
    }

    public void ChangePage(IHost? appHost, string? name)
    {
        ViewModelBase = name switch
        {
            "HomePage" => appHost?.Services.GetRequiredService<HomePageViewModel>(),
            "SearchPage" or "DownloadPage" => null,
            "TaskPage" => appHost?.Services.GetRequiredService<TaskPageViewModel>(),
            _ => null
        };
    }
}