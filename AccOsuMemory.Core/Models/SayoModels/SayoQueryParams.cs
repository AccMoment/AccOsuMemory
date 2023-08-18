using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.Models.SayoModels;

public class SayoQueryParams
{
    [JsonPropertyName("cmd")] public string Cmd { get; init; }
    [JsonPropertyName("limit")] public int Limit { get; init; }
    [JsonPropertyName("offset")] public int Offset { get; init; }
    [JsonPropertyName("type")] public int Type { get; set; }
    [JsonPropertyName("genre")] public int Genre { get; set; }
    [JsonPropertyName("language")] public int Language { get; set; }
    [JsonPropertyName("mode")] public int Mode { get; set; }
    [JsonPropertyName("class")] public int ApprovedState { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("keyword")] public string? KeyWord { get; set; }
    [JsonPropertyName("subtype")] public int SubType { get; set; }
}