using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using AccOsuMemory.Desktop.DTO.Sayo;
using AccOsuMemory.Desktop.Message;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.VO;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class SearchPageViewModel(IFileProvider fileProvider,ISayoApiService sayoApiService, IMapper mapper,
    HomePageViewModel homePageViewModel) : ViewModelBase(fileProvider)
{
    [ObservableProperty] private string _searchText = string.Empty;
    [ObservableProperty] private string _errorText = string.Empty;
    [ObservableProperty] private bool _isError;
    [ObservableProperty] private bool _isVisibleDetailMapControl;
    [ObservableProperty] private BeatmapInfoStorage _beatmapInfoStorage = new();

    [RelayCommand]
    private async Task Search()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            ErrorText = "这个搜索栏里面怎么什么东西都没有啊o(╥﹏╥)o";
            IsError = true;
            return;
        }

        try
        {
            var info = mapper.Map<BeatmapInfoDto>(await sayoApiService.GetBeatmapListInfo(SearchText));
            info.MapDetailData.Sort((x, y) => x.Star.CompareTo(y.Star));
            BeatmapInfoStorage.BeatmapInfo = info;
            BeatmapInfoStorage.SelectedDiffMap = BeatmapInfoStorage.BeatmapInfo.MapDetailData.FirstOrDefault();
            BeatmapInfoStorage.BeatmapInfo.ThumbnailFile = BeatmapInfoStorage.BeatmapInfo.GetThumbnailUrl();
            IsVisibleDetailMapControl = true;
        }
        catch (Exception e)
        {
            ErrorText = e.Message;
            IsError = true;
        }
    }

    [RelayCommand]
    private async Task PlayAudioAsync(string url)
    {
         await homePageViewModel.PlayAudioCommand.ExecuteAsync(url);
    }

    [RelayCommand]
    private void AddDownloadTaskByType(DownloadType type)
    {
        var beatmap = mapper.Map<BeatmapDto>(BeatmapInfoStorage.BeatmapInfo);
        beatmap.DownloadType = type;
        WeakReferenceMessenger.Default.Send(new DownloadTaskMessage(beatmap));
    }
    

    partial void OnSearchTextChanged(string value)
    {
        if (IsError)
        {
            IsError = false;
            ErrorText = string.Empty;
        }
    }
}