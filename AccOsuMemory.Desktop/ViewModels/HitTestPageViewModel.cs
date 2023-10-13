using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.VO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class HitTestPageViewModel(IFileProvider fileProvider) : ViewModelBase(fileProvider)
{
    [ObservableProperty]
    private HitCalculator _hitCalculator = new();

    [RelayCommand]
    public void Reset()
    {
        HitCalculator.Reset();
    }
   
}