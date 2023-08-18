using AccOsuMemory.Desktop.Model;
using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class HitTestPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private HitCalculator _hitCalculator = new();
    
    public HitTestPageViewModel(IFileProvider fileProvider) : base(fileProvider)
    {
    }

    [RelayCommand]
    public void Reset()
    {
        HitCalculator.Reset();
    }
   
}