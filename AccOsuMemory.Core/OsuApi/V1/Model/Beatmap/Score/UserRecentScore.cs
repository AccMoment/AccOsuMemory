using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.OsuApi.V1.Model.Beatmap.Score;

public class UserRecentScore:ScoreBase
{
    [JsonPropertyName("beatmap_id")] 
    public int BeatMapId { get; set; }
    
    [JsonPropertyName("score_id")] 
    public new long? ScoreId { get; set; }
}