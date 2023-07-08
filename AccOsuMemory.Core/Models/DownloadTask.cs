using System.Net.Http.Handlers;
using AccOsuMemory.Core.Net;
using CommunityToolkit.Mvvm.ComponentModel;
namespace AccOsuMemory.Core.Models;

public class DownloadTask : ObservableObject, IHttpTask
{
    private bool _isWaiting = true;
    private bool _isDownloading;
    private bool _isFinished;
    private bool _isError;
    private string _errorMessage = string.Empty;
    private double _downloadedProgress;
    private long _bytesTransferred;
    private long _totalBytes;

    private readonly System.Timers.Timer _timer = new()
    {
        Interval = 1000,
        Enabled = true
    };

    private long _currentNetSpeed;
    private long _recordBytesTransferred = 0;
    public long Id { get; init; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
    public string Name { get; init; }
    public string Url { get; init; }
    public string DestinationFilePath { get; init; }

    public bool IsWaiting
    {
        get => _isWaiting;
        private set => SetProperty(ref _isWaiting, value);
    }

    public bool IsDownloading
    {
        get => _isDownloading;
        private set => SetProperty(ref _isDownloading, value);
    }

    public bool IsFinished
    {
        get => _isFinished;
        private set => SetProperty(ref _isFinished, value);
    }

    public bool IsError
    {
        get => _isError;
        private set => SetProperty(ref _isError, value);
    }

    public double DownloadedProgress
    {
        get => _downloadedProgress;
        set => SetProperty(ref _downloadedProgress, value);
    }

    public long CurrentNetSpeed
    {
        get => _currentNetSpeed;
        set => SetProperty(ref _currentNetSpeed, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            IsDownloading = false;
            IsFinished = false;
            IsWaiting = false;
            IsError = true;
            _timer.Stop();
            _timer.Dispose();
            SetProperty(ref _errorMessage, value);
        }
    }

    public DownloadTask(string name, string url, string filePath, string suffix)
    {
        Name = name;
        Url = url;
        DestinationFilePath = Path.Combine(filePath, name + suffix);
        _timer.Elapsed += (s, e) =>
        {
            DownloadedProgress = (double)_bytesTransferred / _totalBytes * 100;
            CurrentNetSpeed = _bytesTransferred - _recordBytesTransferred;
            _recordBytesTransferred = _bytesTransferred;
        };
        _timer.Disposed += (s, e) =>
        {
            DownloadedProgress = (double)_bytesTransferred / _totalBytes * 100;
            CurrentNetSpeed = _bytesTransferred - _recordBytesTransferred;
        };
    }

    public void OnStart()
    {
        IsWaiting = false;
        IsDownloading = true;
        _timer.Start();
    }

    public void OnDownload(object? sender, HttpProgressEventArgs e)
    {
        _totalBytes = e.TotalBytes ?? -1;
        _bytesTransferred = e.BytesTransferred;
    }

    public async Task OnFinished(Stream responseStream)
    {
        await using var fileStream = new FileStream(DestinationFilePath, FileMode.OpenOrCreate, FileAccess.Write);
        await responseStream.CopyToAsync(fileStream);
        IsFinished = true;
        IsDownloading = false;
        _timer.Stop();
        _timer.Dispose();
    }
}