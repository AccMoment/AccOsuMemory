using System.Diagnostics.CodeAnalysis;

namespace AccOsuMemory.Core.Models;

public class AppSettings
{
    public string ApiV1Key { get; set; } = string.Empty;

    public string ApiV2Token { get; set; } = string.Empty;
    public string OsuDirectoryPath { get; set; } = string.Empty;
    public string DefaultDownloadPath { get; set; } = string.Empty;

    public int ThreadCount { get; set; }
}