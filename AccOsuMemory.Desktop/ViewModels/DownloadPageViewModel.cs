using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Options;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class DownloadPageViewModel : ViewModelBase
{
    private readonly AppSettings _appSettings;
    [ObservableProperty] private ObservableCollection<DownloadTask> _tasks = new();
    private readonly DownloadManager _manager = new();

    public DownloadPageViewModel(IOptions<AppSettings> settings)
    {
        _appSettings = settings.Value;
    }

    public void AddTask(string name, string url, string? filePath, string suffix)
    {
        var path = filePath ?? _appSettings.OsuDirectoryPath + "/Songs/";
        var task = new DownloadTask(name, url, path, suffix);
        Tasks.Add(task);
        _manager.SubmitTask(task);
        if (!_manager.IsRunning) _manager.Start();
    }
}