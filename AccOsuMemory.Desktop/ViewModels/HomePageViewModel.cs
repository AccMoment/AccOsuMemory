using System;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.NetCoreAudio;
using AccOsuMemory.Desktop.Message;
using AccOsuMemory.Desktop.Model;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.Utils;
using AccOsuMemory.Desktop.Views;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    private readonly ISayoApiService _service;

    private readonly Player _player = new();

    private readonly HttpClient _httpClient;

    [ObservableProperty] private BeatmapStorage _beatmapStorage = new();

    [ObservableProperty] private Vector _currentOffset;

    [ObservableProperty] private bool _canConnectNetWork = true;

    public Action<string, string>? ShowTips;
    public Func<string,Task>? HideTips;

    public HomePageViewModel(ISayoApiService service, IFileProvider fileProvider, HttpClient httpClient) :
        base(fileProvider)
    {
        _service = service;
        _httpClient = httpClient;
        WeakReferenceMessenger.Default.Register<ShareLinkMessage>(this, ReceiveShareLinkMessage);
    }


    [RelayCommand]
    private async Task PlayAudioAsync(string url)
    {
        var index = url.LastIndexOf('/');
        var name = url[++index..];
        var audioPath = Path.Combine(FileProvider.GetMusicCacheDirectory(), name);
        if (File.Exists(audioPath))
        {
            await _player.Play(audioPath);
            return;
        }

        await Task.Run(async () =>
        {
            await using var fileStream = new FileStream(audioPath, FileMode.OpenOrCreate, FileAccess.Write);
            await using var response = await _httpClient.GetStreamAsync(url);
            await response.CopyToAsync(fileStream);
            await _player.Play(audioPath);
        });
    }

    [RelayCommand]
    private Task AddDownloadTaskAsync(BeatMap beatMap)
    {
        ShowTips?.Invoke("ShowTips", "已添加进下载列表中(*^▽^*)");
        WeakReferenceMessenger.Default.Send(new DownloadTaskMessage(beatMap));
        HideTips?.Invoke("ShowTips");

        return Task.CompletedTask;
    }

    
    private async void ReceiveShareLinkMessage(object r,ShareLinkMessage m)
    {
        var beatMap = m.Value;
        ShowTips?.Invoke("ShowTips", "(*^▽^*)复制成功，分享给你的好友吧~~");
        await m.TopLevel?.Clipboard?.SetTextAsync(beatMap.FullDownloadUrl)!;
        HideTips?.Invoke("ShowTips");
    }

    [RelayCommand]
    private async Task LoadBeatMapsAsync()
    {
        try
        {
            if (!BeatmapStorage.CanLoadBeatMapList)
            {
                ShowTips?.Invoke("ShowErrorTip", "o(╥﹏╥)o 到底了~~");
                HideTips?.Invoke("ShowTips");
            }

            var status = await NetworkChecker.CheckNetworkStatusAsync();
            if (status != IPStatus.Success)
            {
                ShowTips?.Invoke("ShowErrorTip", "网络错误!");
                CanConnectNetWork = false;
                HideTips?.Invoke("ShowErrorTip");
                return;
            }

            ShowTips?.Invoke("ShowTips", "(*^▽^*)加载中~~");
            var list = await _service.GetBeatmapList(++BeatmapStorage.CurrentPage);
            if (list.Status != 0)
            {
                BeatmapStorage.CanLoadBeatMapList = false;
                return;
            }

            list.BeatMaps.ForEach(map =>
            {
                Task.Run(async () =>
                {
                    var file = Path.Combine(FileProvider.GetThumbnailCacheDirectory(), $"{map.Sid}.jpg");
                    if (!File.Exists(file))
                    {
                        await using var stream =
                            await _httpClient.GetStreamAsync(map.GetThumbnailUrl());
                        await using var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
                        await stream.CopyToAsync(fs);
                    }

                    map.ThumbnailFile = file;
                });
                BeatmapStorage.Beatmaps.Add(map);
            });
            HideTips?.Invoke("ShowTips");
        }
        catch (Exception e)
        {
            ShowTips?.Invoke("ShowErrorTip", e.Message);
            CanConnectNetWork = false;
            HideTips?.Invoke("ShowErrorTip");
        }
    }
}