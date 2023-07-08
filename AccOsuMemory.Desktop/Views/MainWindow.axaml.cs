using System;
using AccOsuMemory.Desktop.ViewModels;
using AccOsuMemory.Desktop.Views.Component;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using ShimSkiaSharp;
using static AccOsuMemory.Desktop.App;
namespace AccOsuMemory.Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewPages.Content = AppHost?.Services.GetRequiredService<HomePageView>();
        }
        
        protected override async void OnClosed(EventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnClosed(e);
        }

        private void MinimizeOnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            e.Handled = true;
        }

        private void CloseOnClick(object sender, RoutedEventArgs e)
        {
            Close();
            e.Handled = true;
        }


        private void WindowMoved(object sender, PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
        }

        private void MaximizeOnclick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            e.Handled = true;
        }

        private void MenuButton_OnClick(object? sender, RoutedEventArgs e)
        {
            ClearSelectedMenuButtonState();
            var btn = (MenuButton)sender!;
            btn.IsSelect = true;
            ViewPages.Content = btn.Name switch
            {
                "HomePageBtn" => AppHost?.Services.GetRequiredService<HomePageView>(),
                "DownloadPageBtn" => AppHost?.Services.GetRequiredService<DownloadPageView>(),
                _ => null
            };
        }

        private void ClearSelectedMenuButtonState()
        {
            foreach (var menuItem in MenuLs.Children)
            {
                if (menuItem is MenuButton b)
                {
                    b.IsSelect = false;
                }
            }
        }
        
    }
}