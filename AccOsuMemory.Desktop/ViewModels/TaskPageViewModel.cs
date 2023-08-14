using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Options;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class TaskPageViewModel : ViewModelBase
{
    private readonly IFileProvider _fileProvider;
    [ObservableProperty] private ObservableCollection<DownloadTask> _tasks = new();
    private readonly DownloadManager _manager = new();

    public TaskPageViewModel(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public void AddTask(string name, string url, string suffix)
    {
        var directoryName = Path.GetDirectoryName(_fileProvider.GetDownloadDirectory());
        var downloadPath = directoryName == "osu!" ? Path.Combine(_fileProvider.GetDownloadDirectory(), "Songs") : _fileProvider.GetDownloadDirectory();
        Console.WriteLine(downloadPath);
        var task = new DownloadTask(name, url, downloadPath, suffix);
        Tasks.Add(task);
        _manager.SubmitTask(task);
        if (!_manager.IsRunning) _manager.Start();
    }
}