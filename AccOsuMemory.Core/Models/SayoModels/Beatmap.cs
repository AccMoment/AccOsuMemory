using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.Utils;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Core.Models.SayoModels;

public partial class Beatmap : BaseModel
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
    
}