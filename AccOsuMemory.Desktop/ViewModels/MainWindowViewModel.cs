using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AccOsuMemory.Core.OsuApi.Sayo.Model;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _currentPage = Pages[0];
        }
        
        private readonly ViewModelBase[] Pages = {
            new HomePageViewModel()
        };

        private ViewModelBase _currentPage;

        public ViewModelBase CurrentPage => _currentPage;
    }
}