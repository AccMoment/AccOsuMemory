using System;
using System.Linq;
using System.Threading.Tasks;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia.Controls;
using Avalonia.Input;

namespace AccOsuMemory.Desktop.Views.Component;

public partial class SideBar : UserControl
{
    public SideBar()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            var name = vm.ViewModelBase?.GetType().Name.Replace("ViewModel", "");
            var index = vm.PageModels.ToList().FindIndex(page => page.Name == name);
            var y = index * 49 + 15;
            Canvas.SetTop(FloatPoint, y);
        }
        base.OnDataContextChanged(e);
    }
    
    private async void PageChange_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            if (sender is Border { Child: TextBlock tb })
                vm.ChangePage(App.AppHost, tb.Tag?.ToString());
        }

        var index = Math.Floor(e.GetPosition(this).Y / 49);
        FloatPoint.Height = 0;
        await Task.Delay(300);
        var y = index * 49 + 15;
        Canvas.SetTop(FloatPoint, y);
        FloatPoint.Height = 20;
    }
}