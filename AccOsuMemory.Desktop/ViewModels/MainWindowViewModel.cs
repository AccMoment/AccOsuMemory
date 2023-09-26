using System.Collections.ObjectModel;
using System.IO;
using AccOsuMemory.Desktop.Model;
using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            new("BatchDownloadPage", "批量下载"),
            new("TaskPage", "任务列表"),
            new("HitTestPage", "手速测试")
        };
        ViewModelBase = App.AppHost?.Services.GetRequiredService<SearchPageViewModel>();
    }

    public void ClearTempFiles()
    {
        Directory.Delete(FileProvider.GetMusicCacheDirectory(), true);
        Directory.Delete(FileProvider.GetThumbnailCacheDirectory(), true);
    }

    public void ChangePage(IHost? appHost, string? name)
    {
        ViewModelBase = name switch
        {
            "HomePage" => appHost?.Services.GetRequiredService<HomePageViewModel>(),
            "SearchPage" => appHost?.Services.GetRequiredService<SearchPageViewModel>(),
            "BatchDownloadPage" => appHost?.Services.GetRequiredService<BatchDownloadPageViewModel>(),
            "TaskPage" => appHost?.Services.GetRequiredService<TaskPageViewModel>(),
            "HitTestPage" => appHost?.Services.GetRequiredService<HitTestPageViewModel>(),
            _ => null
        };
    }
}