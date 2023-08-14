namespace AccOsuMemory.Desktop.Services;

public interface IFileProvider
{
    string GetMusicCacheDirectory();

    string GetThumbnailCacheDirectory();
    
    string GetLogFilePath();

    string GetDownloadDirectory();
}