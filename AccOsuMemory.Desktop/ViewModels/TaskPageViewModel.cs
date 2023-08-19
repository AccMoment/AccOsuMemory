using System.Collections.ObjectModel;
using System.IO;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class TaskPageViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<DownloadTask> _downloadTasks = new();
    private readonly DownloadManager _manager;

    public TaskPageViewModel(IFileProvider fileProvider, DownloadManager manager) : base(fileProvider)
    {
        _manager = manager;
    }

    public void AddTask(string name, string url, string suffix)
    {
        var directoryName = Path.GetDirectoryName(FileProvider.GetDownloadDirectory());
        var downloadPath = directoryName == "osu!"
            ? Path.Combine(FileProvider.GetDownloadDirectory(), "Songs")
            : FileProvider.GetDownloadDirectory();
        var task = new DownloadTask(name, url, downloadPath, suffix);
        DownloadTasks.Add(task);
        _manager.SubmitTask(task);
        if (!_manager.IsRunning) _manager.Start();
    }
}