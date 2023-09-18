using System.Collections.ObjectModel;
using System.IO;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Desktop.Message;
using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class TaskPageViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<DownloadTask> _downloadTasks = new();
    private readonly DownloadManager _manager;

    public TaskPageViewModel(IFileProvider fileProvider, DownloadManager manager) : base(fileProvider)
    {
        _manager = manager;
        WeakReferenceMessenger.Default.Register<DownloadTaskMessage>(this, ReceiveDownloadTaskMessage);
    }

    private void AddTask(string name, string url, string suffix)
    {
        var directoryName = Path.GetDirectoryName(FileProvider.GetDownloadDirectory());
        var downloadPath = directoryName == "osu!"
            ? Path.Combine(FileProvider.GetDownloadDirectory(), "Songs")
            : FileProvider.GetDownloadDirectory();
        var task = new DownloadTask(name, url, suffix, downloadPath);
        DownloadTasks.Add(task);
        _manager.SubmitTask(task);
        if (!_manager.IsRunning) _manager.Start();
    }

    [RelayCommand]
    private void ClearAllTask()
    {
        _manager.ClearAllTask();
    }

    private void ReceiveDownloadTaskMessage(object r, DownloadTaskMessage m)
    {
        var beatmap = m.Value;
        var downloadUrl = beatmap.DownloadType switch
        {
            DownloadType.Full => beatmap.FullDownloadUrl,
            DownloadType.NoVideo => beatmap.NoVideoDownloadUrl,
            DownloadType.Mini => beatmap.MiniDownloadUrl,
            _ => ""
        };
        AddTask($"{beatmap.Sid} {beatmap.Creator} - {beatmap.Title}",
            downloadUrl, ".osz");
    }
}