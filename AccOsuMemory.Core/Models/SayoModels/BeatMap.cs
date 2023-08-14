using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AccOsuMemory.Core.Utils;

namespace AccOsuMemory.Core.Models.SayoModels;

public class BeatMap
{
    [JsonPropertyName("approved")] public int Approved { get; set; }

    [JsonPropertyName("artist")] public string Artist { get; set; } = string.Empty;

    [JsonPropertyName("artistU")] public string ArtistUnicode { get; set; } = string.Empty;

    [JsonPropertyName("creator")] public string Creator { get; set; } = string.Empty;

    [JsonPropertyName("favourite_count")] public int FavouriteCount { get; set; }

    [JsonPropertyName("lastupdate")] public int LastUpdate { get; set; }

    [JsonPropertyName("modes")] public int Modes { get; set; }

    [JsonPropertyName("order")] public double Order { get; set; }

    [JsonPropertyName("play_count")] public int PlayCount { get; set; }

    [JsonPropertyName("sid")] public int Sid { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;

    [JsonPropertyName("titleU")] public string TitleUnicode { get; set; } = string.Empty;

    [Description("缩略图")] public string? ThumbnailFile { get; set; }

    [Description("试听音频")] public string PreviewAudio => $"https://cdn.sayobot.cn:25225/preview/{Sid}.mp3";

    [Description("完整版下载")] public string FullDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/full/{Sid}";

    [Description("无视频版下载")]
    public string NoVideoDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/novideo/{Sid}";

    [Description("Mini版下载")] public string MiniDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/mini/{Sid}";

    private string FileUrl => $"https://dl.sayobot.cn/beatmaps/files/{Sid}/";

    public bool IsExist { get; set; }

    public async Task<List<string>?> GetOriginalImageUrls() =>
        (await HttpUtil.HttpClient.GetFromJsonAsync<JsonArray>(FileUrl) ?? throw new Exception("获取失败，请重新尝试。"))
        .Select(s => (string)s["name"])
        .Where(w =>
            w?.LastIndexOf(".jpg", StringComparison.Ordinal) != -1 ||
            w.LastIndexOf(".png", StringComparison.Ordinal) != -1)
        .Select(s => FileUrl + s)
        .ToList();

    public async Task<string> GetFullAudio() =>
        (await HttpUtil.HttpClient.GetFromJsonAsync<JsonArray>(FileUrl) ?? throw new Exception("获取失败，请重新尝试。"))
        .Select(s => (string)s["name"])
        .Where(w => w?.LastIndexOf(".mp3", StringComparison.Ordinal) != -1)
        .Select(s => FileUrl + s)
        .ToList()[0];
    
    public string GetThumbnailUrl()=>$"https://cdn.sayobot.cn:25225/beatmaps/{Sid}/covers/cover.jpg";
}