using System.Collections.ObjectModel;
using System.IO;
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
    }


    public async Task PlayAudio(string url)
    {
        // if (_player.Playing) await _player.Stop();
        var index = url.LastIndexOf('/');
        var name = url[++index..];
        var audioPath = Path.Combine(_fileProvider.GetTempDirectoryPath(),name);
        if (File.Exists(audioPath))
        {
            await _player.Play(audioPath);
            return;
        }
        await Task.Run(async () =>
        {
            await using var fileStream = new FileStream(audioPath, FileMode.OpenOrCreate, FileAccess.Write);
            await using var response = await DownloadManager.GetHttpClient().GetStreamAsync(url);
            await response.CopyToAsync(fileStream);
        });
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

    public async Task CheckNetStatus()
    {
        using Ping ping = new();
        const string hostName = "www.baidu.com";
        var reply =await ping.SendPingAsync(hostName,10000);
        CanConnectNetWork = reply.Status == IPStatus.Success;
    }

    public void WriteErrorToFile(string errorText)
    {
        using var file = File.AppendText(_fileProvider.GetLogPath());
        file.Write(errorText);
    }
}