using System;
using System.IO;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Utils;
using Microsoft.Extensions.Options;

namespace AccOsuMemory.Desktop.Services;

public class FileProvider : IFileProvider
{
    private static string DataPath => "data";
    private static string TempPath => Path.Combine(DataPath, "temp");
    private static string LogFilePath => Path.Combine(DataPath, "log.txt");

    private string DownloadPath;

    private string MusicCacheDirectoryPath => Path.Combine(DataPath, "cache", "music");

    private string ThumbnailCacheDirectoryPath => Path.Combine(DataPath, "cache", "thumbnail");

    public FileProvider(IOptions<AppSettings> options)
    {
        // if (!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);
        CreateDirectory(TempPath);
        CreateDirectory(MusicCacheDirectoryPath);
        CreateDirectory(ThumbnailCacheDirectoryPath);
        if (!File.Exists(LogFilePath)) File.Create(LogFilePath).Dispose();
        var appSettings = options.Value;
        if (string.IsNullOrWhiteSpace(appSettings.DefaultDownloadPath))
        {
            DownloadPath = Path.Combine(Environment.CurrentDirectory, DataPath, "download");
            CreateDirectory(DownloadPath);
            AppSettingsWriter.Write(nameof(AppSettings.DefaultDownloadPath), DownloadPath);
        }
        else
        {
            DownloadPath = string.IsNullOrWhiteSpace(appSettings.OsuDirectoryPath)
                ? appSettings.OsuDirectoryPath
                : appSettings.DefaultDownloadPath;
        }
    }

    private void CreateDirectory(string path)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }


    public string GetTempDirectoryPath() => TempPath;
    public string GetMusicCacheDirectory() => MusicCacheDirectoryPath;
    public string GetThumbnailCacheDirectory() => ThumbnailCacheDirectoryPath;
    public string GetLogFilePath() => LogFilePath;
    public string GetDownloadDirectory() => DownloadPath;
}