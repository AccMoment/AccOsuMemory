using System.Text.Json.Serialization;
using AccOsuMemory.Core.OsuApi.Utils;

namespace AccOsuMemory.Core.OsuApi.V1.Model.Beatmap.Score;

public class UserBestScore: ScoreBase
{
    [JsonPropertyName("beatmap_id")] 
    public int BeatMapId { get; set; }
    
    [JsonPropertyName("pp")] 
    public double PP { get; set; }

    [JsonPropertyName("replay_available")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    public bool ReplayAvailable { get; set; }
}