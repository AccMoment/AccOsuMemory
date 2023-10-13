using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class BatchDownloadPageViewModel(IFileProvider fileProvider,IOptionsMonitor<AppSettings> options) : ViewModelBase(fileProvider)
{
    [ObservableProperty] private DateTimeOffset _selectedDate = new(new DateTime(2007, 5, 25));
    // [ObservableProperty] private DateTimeOffset _minYears = new(new DateTime(2007, 5, 25));
    [ObservableProperty] private BatchDownloadType _downloadType = BatchDownloadType.Ranked;

    [ObservableProperty] private string _gamerName = string.Empty;
    [ObservableProperty] private string _mapperName = string.Empty;

    [ObservableProperty] private ObservableCollection<string> _log = new();

    [ObservableProperty] private bool _isGrantApiKey = string.IsNullOrWhiteSpace(options.CurrentValue.ApiV1Key);
    
    [RelayCommand]
    private void ChangeDownloadType(BatchDownloadType type)
    {
        Debug.WriteLine(type);
        DownloadType = type;
    }
    
    
}

