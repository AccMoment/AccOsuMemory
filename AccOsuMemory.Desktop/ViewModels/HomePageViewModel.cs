using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using AccOsuMemory.Core.NetCoreAudio;
using AccOsuMemory.Desktop.DTO.Sayo;
using AccOsuMemory.Desktop.Message;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.Utils;
using AccOsuMemory.Desktop.VO;
using AutoMapper;
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

    private readonly IMapper _mapper;

    [ObservableProperty] private BeatmapStorage _beatmapStorage = new();

    [ObservableProperty] private BeatmapInfoStorage _beatmapInfoStorage = new();

    [ObservableProperty] private Vector _currentOffset;

    [ObservableProperty] private bool _canConnectNetWork = true;

    [ObservableProperty] private bool _isOpenDetailMapControl;


    public Action<string, string>? ShowTips { get; set; }
    public Func<string, Task>? HideTips { get; set; }


    public HomePageViewModel(ISayoApiService service, IFileProvider fileProvider, HttpClient httpClient,
        IMapper mapper) :
        base(fileProvider)
    {
        _service = service;
        _httpClient = httpClient;
        _mapper = mapper;
        WeakReferenceMessenger.Default.Register<ShareLinkMessage>(this, ReceiveShareLinkMessage);

        Task.Run(async () => { await CheckingNetworkStatus(); });
    }

    [RelayCommand]
    private async Task OpenDetailPanelAsync(int sid)
    {
        if (BeatmapInfoStorage.BeatmapInfo?.Sid == sid && !IsOpenDetailMapControl)
        {
            IsOpenDetailMapControl = true;
            return;
        }

        try
        {
            var info = _mapper.Map<BeatmapInfoDto>(await _service.GetBeatmapListInfo(sid.ToString()));
            info.MapDetailData.Sort((x, y) => x.Star.CompareTo(y.Star));
            BeatmapInfoStorage.BeatmapInfo = info;
            var file = Path.Combine(FileProvider.GetThumbnailCacheDirectory(), $"{sid}.jpg");
            BeatmapInfoStorage.SelectedDiffMap = BeatmapInfoStorage.BeatmapInfo.MapDetailData.FirstOrDefault();
            BeatmapInfoStorage.BeatmapInfo.ThumbnailFile = file;
            IsOpenDetailMapControl = true;
        }
        catch (Exception e)
        {
            await PopupTips("ShowErrorTips", e.Message);
            throw;
        }
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
    private async Task AddDownloadTaskByTypeAsync(DownloadType type)
    {
        var beatmap = BeatmapStorage.SelectedBeatmap;
        beatmap!.DownloadType = type;
        await PopupTips("ShowTips", "已添加进下载列表中(*^▽^*)",
            async () =>
            {
                WeakReferenceMessenger.Default.Send(new DownloadTaskMessage(beatmap));
                await Task.CompletedTask;
            });
    }

    [RelayCommand]
    private async Task AddDownloadTaskAsync(BeatmapDto beatmap)
    {
        beatmap.DownloadType = DownloadType.Mini;
        await PopupTips("ShowTips", "已添加进下载列表中(*^▽^*)",
            async () =>
            {
                WeakReferenceMessenger.Default.Send(new DownloadTaskMessage(beatmap));
                await Task.CompletedTask;
            });
    }


    [RelayCommand]
    private async Task LoadBeatMapsAsync()
    {
        try
        {
            if (!BeatmapStorage.CanLoadBeatMapList)
            {
                await PopupTips("ShowTips", "o(╥﹏╥)o 到底了~~");
                return;
            }

            await PopupTips("ShowTips", "(*^▽^*)加载中~~", async () =>
            {
                var list = await _service.GetBeatmapList(++BeatmapStorage.CurrentPage);
                if (list.Status != 0)
                {
                    BeatmapStorage.CanLoadBeatMapList = false;
                    await PopupTips("ShowTips", "o(╥﹏╥)o 到底了~~");
                    return;
                }

                list.BeatMaps.ForEach(map =>
                {
                    if (BeatmapStorage.Beatmaps.Any(b => b.Sid == map.Sid))
                    {
                        return;
                    }

                    var mapDto = _mapper.Map<BeatmapDto>(map);
                    Task.Run(async () =>
                    {
                        var file = Path.Combine(FileProvider.GetThumbnailCacheDirectory(), $"{mapDto.Sid}.jpg");
                        if (!File.Exists(file))
                        {
                            await using var stream =
                                await _httpClient.GetStreamAsync(mapDto.GetThumbnailUrl());
                            await using var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
                            await stream.CopyToAsync(fs);
                        }

                        mapDto.ThumbnailFile = file;
                    });
                    BeatmapStorage.Beatmaps.Add(mapDto);
                });
            });
        }
        catch (HttpRequestException)
        {
            await PopupTips("ShowErrorTips", "网络状况不佳或请求超时!");
            CanConnectNetWork = false;
        }
        catch (TaskCanceledException)
        {
            await PopupTips("ShowErrorTips", "网络请求超时!");
            CanConnectNetWork = false;
        }
        catch (Exception e)
        {
            await PopupTips("ShowErrorTips", e.Message);
            CanConnectNetWork = false;
        }
    }

    [RelayCommand]
    private async Task CheckingNetworkStatus()
    {
        var status = await NetworkChecker.CheckNetworkStatusAsync();
        CanConnectNetWork = status == IPStatus.Success;
    }

    [RelayCommand]
    private void CloseDetailPanelAsync()
    {
        IsOpenDetailMapControl = false;
    }


    private async void ReceiveShareLinkMessage(object r, ShareLinkMessage m)
    {
       await PopupTips("ShowTips", "(*^▽^*)复制成功，分享给你的好友吧~~", () =>
       {
           var url = m.Value;
           return m.TopLevel?.Clipboard?.SetTextAsync(url)!;
       });
        
    }

    private async Task PopupTips(string className, string text, Func<Task>? action = null)
    {
        ShowTips?.Invoke(className, text);
        if (action != null)
            await action.Invoke();
        HideTips?.Invoke(className);
    }
}