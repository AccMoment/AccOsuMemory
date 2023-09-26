using System;
using System.Windows.Input;
using AccOsuMemory.Desktop.DTO.Sayo;
using AccOsuMemory.Desktop.Message;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
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
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(Close), RoutingStrategies.Direct);


    public event EventHandler<RoutedEventArgs>? Close
    {
        add => AddHandler(CloseEvent, value);
        remove => RemoveHandler(CloseEvent, value);
    }

    public static readonly RoutedEvent<RoutedEventArgs> PlayAudioEvent =
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(PlayAudio), RoutingStrategies.Direct);


    public event EventHandler<RoutedEventArgs>? PlayAudio
    {
        add => AddHandler(PlayAudioEvent, value);
        remove => RemoveHandler(PlayAudioEvent, value);
    }

    public static readonly RoutedEvent<RoutedEventArgs> DownloadEvent =
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(Download), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs>? Download
    {
        add => AddHandler(DownloadEvent, value);
        remove => RemoveHandler(DownloadEvent, value);
    }
    
    public static readonly RoutedEvent<RoutedEventArgs> NoVideoDownloadEvent =
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(NoVideoDownload), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs>? NoVideoDownload
    {
        add => AddHandler(NoVideoDownloadEvent, value);
        remove => RemoveHandler(NoVideoDownloadEvent, value);
    }
    
    public static readonly RoutedEvent<RoutedEventArgs> MiniDownloadEvent =
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(MiniDownload), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs>? MiniDownload
    {
        add => AddHandler(MiniDownloadEvent, value);
        remove => RemoveHandler(MiniDownloadEvent, value);
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
        if (sender is not Button { DataContext : BeatmapInfoDto beatmap }) return;
        var topLevel = TopLevel.GetTopLevel(this);
        WeakReferenceMessenger.Default.Send(new ShareLinkMessage(beatmap.FullDownloadUrl, topLevel));
    }

    private void PlayAudio_OnClick(object? sender, RoutedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(PlayAudioEvent));
    }

    private void Download_OnClick(object? sender, RoutedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(DownloadEvent));
    }
    
    private void NoVideDownload_OnClick(object? sender, RoutedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(NoVideoDownloadEvent));
    }
    
    private void MiniDownload_OnClick(object? sender, RoutedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(MiniDownloadEvent));
    }


    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;
    }
}