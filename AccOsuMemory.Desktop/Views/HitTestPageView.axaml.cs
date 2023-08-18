using System;
using System.Diagnostics;
using System.Linq;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia.Controls;
using Avalonia.Input;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace AccOsuMemory.Desktop.Views;

public partial class HitTestPageView : UserControl
{
    private readonly int[] _hitKeys;

    public HitTestPageView()
    {
        InitializeComponent();
        Focusable = true;
        InitChart();
        _hitKeys = new[] { 0, 0 };
        var keys = Enum.GetNames(typeof(Key)).ToList();
        Key1.ItemsSource = keys;
        Key1.SelectedIndex = keys.FindIndex(f => f == "Z");
        Key2.ItemsSource = keys;
        Key2.SelectedIndex = keys.FindIndex(f => f == "X");
        _hitKeys[0] = (int)Key.Z;
        _hitKeys[1] = (int)Key.X;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if ((Cb1.IsChecked == true && (int)e.Key == _hitKeys[0]) ||
            (Cb2.IsChecked == true && (int)e.Key == _hitKeys[1]))
        {
            if (DataContext is HitTestPageViewModel vm)
            {
                if (!vm.HitCalculator.IsStarted) vm.HitCalculator.Start();
                vm.HitCalculator.Tap();
            }
        }
    }


    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox cb) return;
        if (cb.Name == "Key1")
        {
            _hitKeys[0] = (int)Enum.Parse<Key>(e.AddedItems[0]?.ToString() ?? "Z");
        }
        else
        {
            _hitKeys[1] = (int)Enum.Parse<Key>(e.AddedItems[0]?.ToString() ?? "X");
        }
        // Debug.WriteLine(e.AddedItems[0]?.ToString());
    }

    private void InitChart()
    {
        Chart.XAxes = new[]
        {
            new Axis
            {
                TextSize = 0
            }
        };
        Chart.YAxes = new[]
        {
            new Axis
            {
                MaxLimit = 350,
                TextSize = 0,
                ShowSeparatorLines = false,
                MinLimit = 0,
            }
        };
        Chart.DrawMarginFrame = new DrawMarginFrame()
        {
            Stroke = new SolidColorPaint(SKColors.LightGray),
        };
        Chart.DrawMargin = new Margin(8);
    }
}