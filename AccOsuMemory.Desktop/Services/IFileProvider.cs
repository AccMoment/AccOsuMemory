namespace AccOsuMemory.Desktop.Services;

public interface IFileProvider
{
    string GetTempDirectoryPath();
    string GetLogPath();

    string GetDownloadDirectoryPath();
}