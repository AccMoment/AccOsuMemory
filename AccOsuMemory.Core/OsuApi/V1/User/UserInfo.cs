using System.Text.Json.Serialization;
using AccOsuMemory.Core.OsuApi.Utils;

namespace AccOsuMemory.Core.OsuApi.V1.User;

public class UserInfo
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    [JsonPropertyName("join_date")]
    [JsonConverter(typeof(StringToDateTimeConverter))]
    public DateTime JoinDate { get; set; }
    
    [JsonPropertyName("count300")]
    public int Count300 { get; set; }
    
    [JsonPropertyName("count100")]
    public int Count100 { get; set; }
    
    [JsonPropertyName("count50")]
    public int Count50 { get; set; }
    
    [JsonPropertyName("playcount")]
    public int Playcount { get; set; }
    
    [JsonPropertyName("ranked_score")]
    public long RankedScore { get; set; }
    
    [JsonPropertyName("total_score")]
    public long TotalScore { get; set; }
    
    [JsonPropertyName("pp_rank")]
    public int Rank { get; set; }
    
    [JsonPropertyName("level")]
    public double Level { get; set; }
    
    [JsonPropertyName("pp_raw")]
    public double ppRaw { get; set; }
    
    [JsonPropertyName("accuracy")]
    public double Accuracy { get; set; }
    
    [JsonPropertyName("count_rank_ss")]
    public int CountRankSS { get; set; }
    
    [JsonPropertyName("count_rank_ssh")]
    public int CountRankSSH { get; set; }
    
    [JsonPropertyName("count_rank_s")]
    public int CountRankS { get; set; }
    
    [JsonPropertyName("count_rank_sh")]
    public int CountRankSH { get; set; }
    
    [JsonPropertyName("count_rank_a")]
    public int CountRankA { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [JsonPropertyName("total_seconds_played")]
    public int TotalSecondsPlayed { get; set; }
    
    [JsonPropertyName("pp_country_rank")]
    public int CountryRank { get; set; }
    
    [JsonPropertyName("events")]
    public Events[] Events { get; set; }
    
}

public class Events
{
    
    [JsonPropertyName("display_html")]
    public string DisplayHtml { get; set; }
    
    [JsonPropertyName("beatmap_id")]
    public int BeatMapId { get; set; }
    
    [JsonPropertyName("beatmapset_id")]
    public int BeatMapSetId { get; set; }
    
    [JsonPropertyName("date")]
    [JsonConverter(typeof(StringToDateTimeConverter))]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("epicfactor")]
    public string EpicFactor { get; set; }
    
}


