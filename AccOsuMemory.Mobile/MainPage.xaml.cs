using AccOsuMemory.Mobile.ViewModel;

namespace AccOsuMemory.Mobile;

public partial class MainPage : ContentPage
{
    private MainPageViewModel vm = new();

    public MainPage()
    {
        InitializeComponent();
        this.BindingContext = vm;
        vm.LoadBeatMapsAsync();
    }


}