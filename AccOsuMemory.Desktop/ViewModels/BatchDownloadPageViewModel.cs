using AccOsuMemory.Desktop.Services;

namespace AccOsuMemory.Desktop.ViewModels;

public class BatchDownloadPageViewModel : ViewModelBase
{
    public BatchDownloadPageViewModel(IFileProvider fileProvider) : base(fileProvider)
    {
    }
}