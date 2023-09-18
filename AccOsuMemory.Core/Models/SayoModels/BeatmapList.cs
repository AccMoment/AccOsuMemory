using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.Models.SayoModels;

public class BeatmapList
{
    [JsonPropertyName("data")] public List<Beatmap> BeatMaps { get; set; }

    [JsonPropertyName("endid")] public int EndId { get; set; }
    [JsonPropertyName("status")] public int Status { get; set; }
}