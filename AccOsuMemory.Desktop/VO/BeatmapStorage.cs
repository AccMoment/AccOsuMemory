using System.Collections.ObjectModel;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Desktop.DTO;
using AccOsuMemory.Desktop.DTO.Sayo;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.VO;

public partial class BeatmapStorage : BaseModel
{
    [ObservableProperty] private bool _canLoadBeatMapList = true;
    [ObservableProperty] private ObservableCollection<BeatmapDto> _beatmaps = new();
    [ObservableProperty] private BeatmapDto? _selectedBeatmap;

    [ObservableProperty] private BeatmapInfoDto? _beatmapInfo;
    [ObservableProperty] private MapDetailDataDto? _selectedDiffMap;
    public int CurrentPage = 0;
}