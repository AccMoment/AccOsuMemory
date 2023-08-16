﻿using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.NetCoreAudio;
using AccOsuMemory.Desktop.Model;
using AccOsuMemory.Desktop.Services;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    private readonly ISayoApiService _service;

    private readonly Player _player = new();


    [ObservableProperty] private BeatmapStorage _beatmapStorage = new();


    [ObservableProperty] private Vector _currentOffset;

    [ObservableProperty] private bool _canConnectNetWork;

    public HomePageViewModel(ISayoApiService service, IFileProvider fileProvider) : base(fileProvider)
    {
        _service = service;
    }


    public async ValueTask PlayAudio(string url)
    {
        // if (_player.Playing) await _player.Stop();
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
            await using var response = await DownloadManager.HttpClient.GetStreamAsync(url);
            await response.CopyToAsync(fileStream);
        });
        await _player.Play(audioPath);
    }

    public async ValueTask LoadBeatMapsAsync()
    {
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
                        await DownloadManager.HttpClient.GetStreamAsync(map.GetThumbnailUrl());
                    await using var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
                    await stream.CopyToAsync(fs);
                }

                map.ThumbnailFile = file;
            });
            BeatmapStorage.Beatmaps.Add(map);
        });
    }

    public async ValueTask CheckNetStatus()
    {
        using Ping ping = new();
        const string hostName = "www.baidu.com";
        var reply = await ping.SendPingAsync(hostName, 10000);
        CanConnectNetWork = reply.Status == IPStatus.Success;
    }
}