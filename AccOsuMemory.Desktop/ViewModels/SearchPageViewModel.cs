using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class SearchPageViewModel : ViewModelBase
{
    private IFileProvider _fileProvider;

    [ObservableProperty]
    private string _searchText;
    
    public SearchPageViewModel(IFileProvider fileProvider) : base(fileProvider)
    {
        _fileProvider = fileProvider;
    }
}