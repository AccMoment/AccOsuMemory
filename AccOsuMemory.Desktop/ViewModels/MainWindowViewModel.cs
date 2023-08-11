using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AccOsuMemory.Desktop.Model;
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

    private readonly IHost? _appHost = App.AppHost;

    [ObservableProperty] private ObservableCollection<PageModel> _pageModels;

    public MainWindowViewModel()
    {
        _viewModelBase = _appHost?.Services.GetRequiredService<HomePageViewModel>();
        _pageModels = new ObservableCollection<PageModel>
        {
            new PageModel("HomePage", "主页"), 
            new PageModel("SearchPage", "搜索歌曲"), 
            new PageModel("DownloadPage", "批量下载"),
            new PageModel("TaskPage", "任务列表")
        };
    }

    [RelayCommand]
    private void ChangePage(string name)
    {
        ViewModelBase = name switch
        {
            "HomePage" => _appHost?.Services.GetRequiredService<HomePageViewModel>(),
            "SearchPage" or "DownloadPage" => null,
            "TaskPage" => _appHost?.Services.GetRequiredService<TaskPageViewModel>()
        };
    }
}