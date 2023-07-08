using System.ComponentModel;
using System.Text.Json.Serialization;
using AccOsuMemory.Core.Utils.Converter;

namespace AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;

public abstract class ScoreBase
{
    [JsonPropertyName("score_id")] 
    public long ScoreId { get; set; }

    [JsonPropertyName("score")] 
    public int Score { get; set; }

    [JsonPropertyName("maxcombo")] 
    public int MaxCombo { get; set; }

    [JsonPropertyName("count50")] 
    public int Count50 { get; set; }

    [JsonPropertyName("count100")] 
    public int Count100 { get; set; }

    [JsonPropertyName("count300")] 
    public int Count300 { get; set; }

    [JsonPropertyName("countmiss")] 
    public int CountMiss { get; set; }

    [JsonPropertyName("countkatu")]
    [Description("喝!")]
    public int CountKaTu { get; set; }

    [JsonPropertyName("countgeki")]
    [Description("激!")]
    public int CountGeKi { get; set; }

    [JsonPropertyName("perfect")]
    [Description("1 = maximum combo of map reached, 0 otherwise")]
    public int Perfect { get; set; }

    [JsonPropertyName("enabled_mods")] 
    public int EnabledMods { get; set; }

    [JsonPropertyName("user_id")] 
    public int UserId { get; set; }

    [JsonPropertyName("date")]
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime Date { get; set; }

    [JsonPropertyName("rank")] 
    public string Rank { get; set; }

    
}