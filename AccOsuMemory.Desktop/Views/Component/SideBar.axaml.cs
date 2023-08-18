using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AccOsuMemory.Desktop.Model;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AccOsuMemory.Desktop.Views.Component;

public partial class SideBar : UserControl
{
    public SideBar()
    {
        InitializeComponent();
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