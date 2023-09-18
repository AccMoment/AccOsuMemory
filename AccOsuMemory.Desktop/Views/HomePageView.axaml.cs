using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Desktop.DTO;
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


    protected override void OnLoaded(RoutedEventArgs e)
    {
        SongsScroll.ScrollChanged += ScrollEvent;
        base.OnLoaded(e);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        SongsScroll.ScrollChanged -= ScrollEvent;
        base.OnUnloaded(e);
    }


    private async void ScrollEvent(object? sender, ScrollChangedEventArgs e)
    {
        var extentHeight = SongsScroll.Extent.Height;
        var currentOffset = SongsScroll.Offset.Y + SongsScroll.Viewport.Height;
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