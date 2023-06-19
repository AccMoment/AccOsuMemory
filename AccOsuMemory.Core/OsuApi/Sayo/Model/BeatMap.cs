using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.OsuApi.Sayo.Model;

public class BeatMap
{
    [JsonPropertyName("approved")]
    public int Approved { get; set; }
    
    [JsonPropertyName("artist")]
    public string Artist { get; set; }
    
    [JsonPropertyName("artistU")]
    public string ArtistUnicode { get; set; }
    
    [JsonPropertyName("creator")]
    public string Creator { get; set; }
    
    [JsonPropertyName("favourite_count")]
    public int FavouriteCount { get; set; }
    
    [JsonPropertyName("lastupdate")]
    public int LastUpdate { get; set; }
    
    [JsonPropertyName("modes")]
    public int Modes { get; set; }
    
    [JsonPropertyName("order")]
    public double Order { get; set; }
    
    [JsonPropertyName("play_count")]
    public int PlayCount { get; set; }
    
    [JsonPropertyName("sid")]
    public int Sid { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("titleU")]
    public string TitleUnicode { get; set; }

    public string ThumbnailUrl => $"https://cdn.sayobot.cn:25225/beatmaps/{Sid}/covers/cover.jpg";

    public string OriginalImageUrl => $"https://dl.sayobot.cn/beatmaps/files/{Sid}/bg.jpg";

}

