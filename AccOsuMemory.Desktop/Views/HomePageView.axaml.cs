using System;
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
    private readonly TaskPageViewModel? _taskPageVM =
        App.AppHost?.Services.GetRequiredService<TaskPageViewModel>();

    private readonly HomePageViewModel _homePageVM = App.AppHost?.Services.GetRequiredService<HomePageViewModel>()!;

    public HomePageView()
    {
        InitializeComponent();
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        _homePageVM.CurrentOffset = SongsScroll.Offset;
        base.OnDetachedFromLogicalTree(e);
    }

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        SongsScroll.Offset = _homePageVM.CurrentOffset;
        base.OnAttachedToLogicalTree(e);
    }

    protected override async void OnInitialized()
    {
        try
        {
            await _homePageVM.CheckNetStatus();
            if (_homePageVM.CanConnectNetWork)
            {
                if (_homePageVM.Beatmaps.Count == 0)
                {
                    LoadBeatmaps();
                }

                SongsScroll.ScrollChanged += ScrollEvent;
            }
        }
        catch (Exception e)
        {
            _homePageVM.WriteErrorToFile(e.StackTrace ?? e.Message);
        }

        base.OnInitialized();
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        SongsScroll.ScrollChanged -= ScrollEvent;
        base.OnUnloaded(e);
    }

    private async void Download_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: BeatMap beatmap })
        {
            TipsBorder.Classes.Add("ShowTips");
            TipsText.Text = "已添加进下载列表中(*^▽^*)";
            _taskPageVM?.AddTask($"{beatmap.Sid} {beatmap.Creator} - {beatmap.Title}",
                beatmap.MiniDownloadUrl, ".osz");
            await Task.Delay(1145);
            TipsBorder.Classes.Remove("ShowTips");
        }
    }

    private async void AudioPlay_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: BeatMap beatmap })
        {
            // Task.Run(async () => await _vm.PlayAudio(beatmap.PreviewAudio));
            await _homePageVM.PlayAudio(beatmap.PreviewAudio);
        }
    }

    private async void Refresh_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            await _homePageVM.CheckNetStatus();
            if (!_homePageVM.CanConnectNetWork) return;
            LoadBeatmaps();
            SongsScroll.ScrollChanged += ScrollEvent;
        }
        catch (Exception ex)
        {
            _homePageVM.WriteErrorToFile(ex.StackTrace ?? ex.Message);
        }
    }

    private async void LoadBeatmaps()
    {
        TipsBorder.Classes.Add("ShowTips");
        TipsText.Text = "(*^▽^*)加载中~~";
        await _homePageVM.LoadBeatMapsAsync();
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
            if (_homePageVM.CanLoadBeatMapList)
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