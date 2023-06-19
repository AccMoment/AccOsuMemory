using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.OsuApi.Sayo.Model;

public class BeatMapList
{
    [JsonPropertyName("data")]
    public List<BeatMap> BeatMaps { get; set; }
    [JsonPropertyName("endid")]
    public int EndId { get; set; }
    
    [JsonPropertyName("status")]
    public int Status { get; set; }
}
