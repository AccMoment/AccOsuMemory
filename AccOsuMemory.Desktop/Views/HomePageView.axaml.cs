using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Microsoft.Extensions.DependencyInjection;

namespace AccOsuMemory.Desktop.Views;

public partial class HomePageView : UserControl
{
    private readonly DownloadPageViewModel? _downloadPageViewModel =
        App.AppHost?.Services.GetRequiredService<DownloadPageViewModel>();

    private readonly HomePageViewModel _vm = App.AppHost?.Services.GetRequiredService<HomePageViewModel>()!;

    public HomePageView()
    {
        InitializeComponent();
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        _vm.CurrentOffset = SongsScroll.Offset;
        base.OnDetachedFromLogicalTree(e);
    }

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        SongsScroll.Offset = _vm.CurrentOffset;
        base.OnAttachedToLogicalTree(e);
    }

    protected override async void OnInitialized()
    {
        try
        {
            var replay = await _vm.CheckNetStatus();
            if (replay.Status == IPStatus.Success)
            {
                _vm.CanConnectNetWork = true;
                LoadBeatmaps();
                SongsScroll.ScrollChanged += ScrollEvent;
            }
        }
        catch (Exception e)
        {
            _vm.WriteErrorToFile(e.StackTrace ?? e.Message);
        }

        base.OnInitialized();
    }

    private async void Download_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: BeatMap beatmap })
        {
            TipsBorder.Classes.Add("ShowTips");
            TipsText.Text = "已添加进下载列表中(*^▽^*)";
            _downloadPageViewModel?.AddTask($"{beatmap.Sid} {beatmap.Creator} - {beatmap.Title}",
                beatmap.MiniDownloadUrl, null, ".osz");
            await Task.Delay(1145);
            TipsBorder.Classes.Remove("ShowTips");
        }
    }

    private async void AudioPlay_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: BeatMap beatmap })
        {
            await _vm.PlayAudio(beatmap.PreviewAudio);
        }
    }

    private async void Refresh_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var replay = await _vm.CheckNetStatus();
            if (replay.Status == IPStatus.Success)
            {
                // _vm.CanConnectNetWork = true;
                // LoadBeatmaps();
                // SongsScroll.ScrollChanged += ScrollEvent;
            }
        }
        catch (Exception ex)
        {
            _vm.WriteErrorToFile(ex.StackTrace ?? ex.Message);
        }
    }

    private async void LoadBeatmaps()
    {
        TipsBorder.Classes.Add("ShowTips");
        TipsText.Text = "(*^▽^*)加载中~~";
        await _vm.LoadBeatMapsAsync();
        await Task.Delay(1145);
        TipsBorder.Classes.Remove("ShowTips");
    }

    private async void ScrollEvent(object? sender, ScrollChangedEventArgs e)
    {
        var extentHeight = SongsScroll.Extent.Height;
        var currentOffset = SongsScroll.Offset.Y + SongsScroll.Viewport.Height;
// #if DEBUG
//             Logger.TryGet(LogEventLevel.Debug, LogArea.Control)?.Log("ScrollEvent",
//                 $"extentHeight:{extentHeight} currentOffset:{currentOffset}");
// #endif
        if (extentHeight - currentOffset >= 50d) return;
        try
        {
            if (_vm.CanLoadBeatMapList)
            {
                LoadBeatmaps();
            }
            else
            {
                TipsBorder.Classes.Add("ShowTips");
                TipsText.Text = "o(╥﹏╥)o 到底了~~";
                await Task.Delay(1145);
                TipsBorder.Classes.Remove("ShowTips");
            }
        }
        catch (Exception ex)
        {
            TipsBorder.Classes.Remove("ShowTips");
            TipsBorder.Classes.Add("ShowErrorTips");
            TipsText.Text = ex.Message;
            await Task.Delay(1145);
            TipsBorder.Classes.Remove("ShowErrorTips");
        }
    }
}