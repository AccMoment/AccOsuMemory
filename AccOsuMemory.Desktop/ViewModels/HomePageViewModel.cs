using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.NetCoreAudio;
using AccOsuMemory.Desktop.Services;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.ViewModels;
public partial class HomePageViewModel : ViewModelBase
{
    private readonly ISayoApiService _service;
    private readonly IFileProvider _fileProvider;

    private readonly Player _player = new();
    private int _currentPage;

    [ObservableProperty] private ObservableCollection<BeatMap> _beatmaps = new();

    [ObservableProperty] private bool _canLoadBeatMapList = true;

    [ObservableProperty] private BeatMap? _selectedBeatmap;

    [ObservableProperty] private Vector _currentOffset;

    [ObservableProperty] private bool _canConnectNetWork;
    
    public HomePageViewModel(ISayoApiService service, IFileProvider fileProvider)
    {
        _service = service;
        _fileProvider = fileProvider;
        // _player.PlaybackFinished += (s, e) =>
        // {
        //     if (File.Exists(_tempAudioPath)) File.Delete(_tempAudioPath);
        // };
    }


    public async Task PlayAudio(string url)
    {
        await _player.Stop();
        var name = url[url.LastIndexOf('/')..];
        var audioPath = _fileProvider.GetTempDirectory() + $"/{name}";
        if (File.Exists(audioPath))
        {
            await _player.Play(audioPath);
            return;
        }

        var fileStream = new FileStream(audioPath, FileMode.OpenOrCreate, FileAccess.Write);
        var response = await DownloadManager.GetHttpClient().GetStreamAsync(url);
        await response.CopyToAsync(fileStream);
        await fileStream.DisposeAsync();
        await response.DisposeAsync();
        await _player.Play(audioPath);
    }

    public async Task LoadBeatMapsAsync()
    {
        _currentPage++;
        var list = await _service.GetBeatmapList(_currentPage);
        if (list.Status != 0)
        {
            CanLoadBeatMapList = false;
            return;
        }
        list.BeatMaps.ForEach(map => { Beatmaps.Add(map); });
    }

    public async Task<PingReply> CheckNetStatus()
    {
        using Ping ping = new();
        const string hostName = "www.baidu.com";
        return await ping.SendPingAsync(hostName);
    }

    public void WriteErrorToFile(string errorText)
    {
        using var file = File.AppendText(_fileProvider.GetLogTxt());
        file.Write(errorText);
    }
    
}