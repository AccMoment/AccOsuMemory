using System;
using System.IO;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Desktop.Utils;
using Microsoft.Extensions.Options;


namespace AccOsuMemory.Desktop.Services;

public class FileProvider : IFileProvider
{
    private string DataPath => "data";
    public string TempPath => Path.Combine(DataPath, "temp");
    public string LogPath => Path.Combine(DataPath, "log.txt");

    private string DownloadPath;

    public FileProvider(IOptions<AppSettings> options)
    {
        if (!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);
        if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);
        if (!File.Exists(LogPath)) File.Create(LogPath).Dispose();
        var appSettings = options.Value;
        if (string.IsNullOrWhiteSpace(appSettings.DefaultDownloadPath))
        {
            appSettings.DefaultDownloadPath = Path.Combine(Environment.CurrentDirectory, DataPath, "download");
            if (!Directory.Exists(appSettings.DefaultDownloadPath)) Directory.CreateDirectory(appSettings.DefaultDownloadPath);
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
    public string GetLogPath() => LogPath;
    public string GetDownloadDirectoryPath() => DownloadPath;
}