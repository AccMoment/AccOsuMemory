using System.Net.Http.Handlers;
using AccOsuMemory.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Core.Net;

public partial class DownloadTask : BaseModel, IHttpTask
{
    [ObservableProperty] private bool _isWaiting = true;
    [ObservableProperty] private bool _isDownloading;
    [ObservableProperty] private bool _isFinished;
    [ObservableProperty] private bool _isError;
    [ObservableProperty] private long _currentNetSpeed;
    [ObservableProperty] private double _downloadedProgress;
    [ObservableProperty] private long _bytesTransferred;
    [ObservableProperty] private long _totalBytes;
    [ObservableProperty] private long _recordBytesTransferred;
    private string _errorMessage = string.Empty;

    private readonly System.Timers.Timer _timer = new()
    {
        Interval = 1000,
        Enabled = true
    };

    private string DestinationFilePath { get; }

    public long Id { get; init; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
    public string Name { get; init; }
    public string Url { get; init; }


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
            RecordBytesTransferred = _bytesTransferred;
        };
        _timer.Disposed += (s, e) =>
        {
            DownloadedProgress = (double)_bytesTransferred / _totalBytes * 100;
            CurrentNetSpeed = _bytesTransferred - _recordBytesTransferred;
            RecordBytesTransferred = _bytesTransferred;
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
        TotalBytes = e.TotalBytes ?? -1;
        BytesTransferred = e.BytesTransferred;
    }

    public async ValueTask OnFinished(Stream responseStream)
    {
        await using var fileStream = new FileStream(DestinationFilePath, FileMode.OpenOrCreate, FileAccess.Write);
        await responseStream.CopyToAsync(fileStream);
        IsFinished = true;
        IsDownloading = false;
        _timer.Stop();
        _timer.Dispose();
    }
}