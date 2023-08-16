using System.Collections.ObjectModel;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Models.SayoModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.Model;

public partial class BeatmapStorage : BaseModel
{
    [ObservableProperty] private bool _canLoadBeatMapList = true;
    [ObservableProperty] private ObservableCollection<BeatMap> _beatmaps = new();
    [ObservableProperty] private BeatMap? _selectedBeatmap;

    public int CurrentPage = 0;
}