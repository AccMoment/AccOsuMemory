using AccOsuMemory.Core.Models;
using AccOsuMemory.Desktop.DTO.Sayo;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AccOsuMemory.Desktop.VO;

public partial class BeatmapInfoStorage : BaseModel
{
    [ObservableProperty] private BeatmapInfoDto? _beatmapInfo;
    [ObservableProperty] private MapDetailDataDto? _selectedDiffMap;
    
    
    
    
    [RelayCommand]
    private void ChangeDiffMapAsync(int bid)
    {
        var data = BeatmapInfo?.MapDetailData.Find(f => f.Bid == bid);
        SelectedDiffMap = data;
    }
    
    
  
   
}