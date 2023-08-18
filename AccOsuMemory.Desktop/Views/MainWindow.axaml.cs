using System;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Desktop.Services;
using AccOsuMemory.Desktop.ViewModels;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static AccOsuMemory.Desktop.App;

namespace AccOsuMemory.Desktop.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? _viewModel;
        
        
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override async void OnClosed(EventArgs e)
        {
            await AppHost!.StopAsync();
            _viewModel?.ClearTempFiles();
            base.OnClosed(e);
        }

        private void MinimizeOnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            WindowState = WindowState.Minimized;
        }

        private void CloseOnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Close();
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

        protected override void OnDataContextChanged(EventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                _viewModel = vm;
                _viewModel.ViewModelBase = AppHost?.Services.GetRequiredService<HomePageViewModel>();
            }
            base.OnDataContextChanged(e);
        }
    }
}