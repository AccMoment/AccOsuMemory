using System;
using System.Threading.Tasks;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;

namespace AccOsuMemory.Desktop.Views;

public partial class HomePageView : UserControl
{
    public HomePageView()
    {
        InitializeComponent();

        
        SongsScroll.ScrollChanged += async (_, _) =>
        {
            var extentHeight =  SongsScroll.Extent.Height;
            var currentOffset = SongsScroll.Offset.Y + SongsScroll.Viewport.Height;
            Logger.TryGet(LogEventLevel.Debug,LogArea.Control)?.Log("ScrollEvent",$"extentHeight:{extentHeight} currentOffset:{currentOffset}");
            if (extentHeight - currentOffset >= 50d) return;
            if (DataContext is HomePageViewModel { CanLoadBeatMapList: true } vm)
            {
                TipsBorder.Classes.Add("ShowTipText");
                TipsText.Text = "加载中~~(*^▽^*)";
                await vm.LoadBeatMapsAsync();
                TipsBorder.Classes.Remove("ShowTipText");
            }
            else
            {
                TipsBorder.Classes.Add("ShowTipText");
                TipsText.Text = "o(╥﹏╥)o 到底了~~";
                await Task.Delay(1500);
                TipsBorder.Classes.Remove("ShowTipText");
            }
        
        };
    }
    
    
}