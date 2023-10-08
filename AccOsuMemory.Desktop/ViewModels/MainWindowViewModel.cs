using System.Collections.ObjectModel;
using System.IO;
using AccOsuMemory.Desktop.Model;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.Views;
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
            new(nameof(HomePageView), "主页"),
            new(nameof(SearchPageView), "搜索歌曲"),
            new(nameof(BatchDownloadPageView), "批量下载"),
            new(nameof(TaskPageView), "任务列表"),
            new(nameof(HitTestPageView), "手速测试"),
            new(nameof(BackupPageView),"数据备份")
        };
        ViewModelBase = App.AppHost?.Services.GetRequiredService<BatchDownloadPageViewModel>();
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
            nameof(HomePageView) => appHost?.Services.GetRequiredService<HomePageViewModel>(),
            nameof(SearchPageView) => appHost?.Services.GetRequiredService<SearchPageViewModel>(),
            nameof(BatchDownloadPageView) => appHost?.Services.GetRequiredService<BatchDownloadPageViewModel>(),
            nameof(TaskPageView) => appHost?.Services.GetRequiredService<TaskPageViewModel>(),
            nameof(HitTestPageView) => appHost?.Services.GetRequiredService<HitTestPageViewModel>(),
            nameof(BackupPageView) => appHost?.Services.GetRequiredService<BackupPageViewModel>(),
            _ => null
        };
    }
}