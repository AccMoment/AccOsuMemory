using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.Models.SayoModels;

public class SayoQueryParams
{
    [JsonPropertyName("cmd")] public string Cmd { get; init; }
    [JsonPropertyName("limit")] public int Limit { get; init; }
    [JsonPropertyName("offset")] public int Offset { get; init; }
    [JsonPropertyName("type")] public int Type { get; set; }
}