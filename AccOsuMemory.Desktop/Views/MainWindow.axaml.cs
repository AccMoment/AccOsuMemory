using AccOsuMemory.Desktop.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using ShimSkiaSharp;

namespace AccOsuMemory.Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        protected override void OnLoaded()
        {
            // if (ViewPages.Content is HomePageViewModel vm)
            // {
            //     vm.LoadBeatMapsAsync();
            // }
            base.OnLoaded();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            // _viewModel.AddBeatMap();
            // if (ViewPages.Content is HomePageViewModel vm)
            // {
            //     vm.LoadBeatMapsAsync();
            // }
            // Logger.Sink?.Log(LogEventLevel.Debug,"",this,"load beatmaps");
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
    }
}