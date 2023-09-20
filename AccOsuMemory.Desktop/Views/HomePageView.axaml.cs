using System.Diagnostics;
using System.Threading.Tasks;
using AccOsuMemory.Desktop.DTO.Sayo;
using AccOsuMemory.Desktop.Message;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AccOsuMemory.Desktop.Views;

public partial class HomePageView : UserControl
{
    private readonly HomePageViewModel _homePageVM = App.AppHost?.Services.GetRequiredService<HomePageViewModel>()!;


    public HomePageView()
    {
        InitializeComponent();

        _homePageVM.ShowTips += (className, text) =>
        {
            TipsText.Text = text;
            TipsBorder.Classes.Add(className);
        };
        _homePageVM.HideTips += async className =>
        {
            while (TipsBorder.IsAnimating(OpacityProperty))
            {
                await Task.Delay(1000);
            }

            TipsBorder.Classes.Remove(className);
        };
    }


    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        SongsScroll.ScrollChanged += ScrollEvent;
        base.OnAttachedToLogicalTree(e);
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        SongsScroll.ScrollChanged -= ScrollEvent;
        base.OnDetachedFromLogicalTree(e);
    }

    private async void ScrollEvent(object? sender, ScrollChangedEventArgs e)
    {
        Debug.WriteLine($"value:{_homePageVM.BeatmapStorage.Beatmaps.Count}");
        var extentHeight = SongsScroll.Extent.Height;
        var currentOffset = _homePageVM.CurrentOffset.Y + SongsScroll.Viewport.Height;
        Debug.WriteLine($"extentHeight:{extentHeight},currentOffset:{currentOffset}");
        if (extentHeight - currentOffset >= 150d) return;
        await _homePageVM.LoadBeatMapsCommand.ExecuteAsync(null);
    }

    private void ShareLink_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button { DataContext : BeatmapDto beatmap }) return;
        var topLevel = TopLevel.GetTopLevel(this);
        WeakReferenceMessenger.Default.Send(new ShareLinkMessage(beatmap, topLevel));
    }


    private void MaskGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (_homePageVM.IsOpenDetailMapControl)
        {
            _homePageVM.IsOpenDetailMapControl = false;
            e.Handled = true;
        }
    }
}