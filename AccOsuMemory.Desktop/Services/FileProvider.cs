using System;
using System.IO;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Desktop.Utils;
using Microsoft.Extensions.Options;


namespace AccOsuMemory.Desktop.Services;

public class FileProvider : IFileProvider
{
    private static string DataPath => "data";
    private static string TempPath => Path.Combine(DataPath, "temp");
    private static string LogPath => Path.Combine(DataPath, "log.txt");

    private string DownloadPath;

    private string MusicCacheDirectoryPath => Path.Combine(DataPath, "cache", "music");

    private string ThumbnailCacheDirectoryPath => Path.Combine(DataPath, "cache", "thumbnail");

    public FileProvider(IOptions<AppSettings> options)
    {
        // if (!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);
        if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);
        if (!Directory.Exists(MusicCacheDirectoryPath)) Directory.CreateDirectory(MusicCacheDirectoryPath);
        if (!Directory.Exists(ThumbnailCacheDirectoryPath)) Directory.CreateDirectory(ThumbnailCacheDirectoryPath);
        if (!File.Exists(LogPath)) File.Create(LogPath).Dispose();
        var appSettings = options.Value;
        if (string.IsNullOrWhiteSpace(appSettings.DefaultDownloadPath))
        {
            appSettings.DefaultDownloadPath = Path.Combine(Environment.CurrentDirectory, DataPath, "download");
            if (!Directory.Exists(appSettings.DefaultDownloadPath))
                Directory.CreateDirectory(appSettings.DefaultDownloadPath);
            AppSettingsWriter.Write(appSettings);
            DownloadPath = appSettings.DefaultDownloadPath;
        }
        else
        {
            DownloadPath = string.IsNullOrWhiteSpace(appSettings.OsuDirectoryPath)
                ? appSettings.OsuDirectoryPath
                : appSettings.DefaultDownloadPath;
        }
    }


    public string GetTempDirectoryPath() => TempPath;
    public string GetMusicCacheDirectory() => MusicCacheDirectoryPath;
    public string GetThumbnailCacheDirectory() => ThumbnailCacheDirectoryPath;
    public string GetLogFilePath() => LogPath;
    public string GetDownloadDirectory() => DownloadPath;
}