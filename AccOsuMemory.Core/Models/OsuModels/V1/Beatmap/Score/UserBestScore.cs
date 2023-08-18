using System.Text.Json.Serialization;
using AccOsuMemory.Core.JsonConverter;

namespace AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;

public class UserBestScore : ScoreBase
{
    [JsonPropertyName("beatmap_id")] public int BeatMapId { get; set; }

    [JsonPropertyName("pp")] public double PP { get; set; }

    [JsonPropertyName("replay_available")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    public bool ReplayAvailable { get; set; }
}