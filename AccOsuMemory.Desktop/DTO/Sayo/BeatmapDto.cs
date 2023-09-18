using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using AccOsuMemory.Core.Net;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.DTO.Sayo;

public partial class BeatmapDto : BaseModel
{
    public int Approved { get; set; }

    public string Artist { get; set; }

    public string Creator { get; set; }

    public int LastUpdate { get; set; }
    public int Modes { get; set; }
    public int Sid { get; set; }
    public string Title { get; set; }

    public DownloadType DownloadType { get; set; } = DownloadType.Mini;
    [Description("试听音频")] public string PreviewAudio => $"https://cdn.sayobot.cn:25225/preview/{Sid}.mp3";

    [Description("完整版下载")] public string FullDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/full/{Sid}";

    [Description("无视频版下载")]
    public string NoVideoDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/novideo/{Sid}";

    [Description("Mini版下载")] public string MiniDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/mini/{Sid}";

    private string FileUrl => $"https://dl.sayobot.cn/beatmaps/files/{Sid}/";

    [Description("缩略图")] [ObservableProperty]
    private string? _thumbnailFile;

    [ObservableProperty] private bool _isExist;

    public async Task<List<string>?> GetOriginalImageUrls() =>
        (await DownloadManager.HttpClient.GetFromJsonAsync<JsonArray>(FileUrl) ?? throw new Exception("获取失败，请重新尝试。"))
        .Select(s => (string)s["name"])
        .Where(w =>
            w?.LastIndexOf(".jpg", StringComparison.Ordinal) != -1 ||
            w.LastIndexOf(".png", StringComparison.Ordinal) != -1)
        .Select(s => FileUrl + s)
        .ToList();

    public async Task<string> GetFullAudio() =>
        (await DownloadManager.HttpClient.GetFromJsonAsync<JsonArray>(FileUrl) ?? throw new Exception("获取失败，请重新尝试。"))
        .Select(s => (string)s["name"])
        .Where(w => w?.LastIndexOf(".mp3", StringComparison.Ordinal) != -1)
        .Select(s => FileUrl + s)
        .ToList()[0];

    public string GetThumbnailUrl() => $"https://cdn.sayobot.cn:25225/beatmaps/{Sid}/covers/cover.jpg";
}