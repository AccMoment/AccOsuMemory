using System;
using System.Windows.Input;
using AccOsuMemory.Desktop.DTO.Sayo;
using AccOsuMemory.Desktop.Message;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;

namespace AccOsuMemory.Desktop.Views.Component;

public partial class DetailBeatmap : UserControl
{
    private HomePageViewModel _vm;

    public static readonly StyledProperty<bool> IsOpenProperty = AvaloniaProperty.Register<DetailBeatmap, bool>(
        "IsOpen");

    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public static readonly RoutedEvent<RoutedEventArgs> CloseEvent =
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(Close), RoutingStrategies.Bubble);


    public event EventHandler<RoutedEventArgs>? Close
    {
        add => AddHandler(CloseEvent, value);
        remove => RemoveHandler(CloseEvent, value);
    }


    public DetailBeatmap()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is HomePageViewModel vm) _vm = vm;
    }

    private void CloseBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(CloseEvent));
    }

    private void ShareLink_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button { DataContext : BeatmapDto beatmap }) return;
        var topLevel = TopLevel.GetTopLevel(this);
        WeakReferenceMessenger.Default.Send(new ShareLinkMessage(beatmap, topLevel));
    }
}