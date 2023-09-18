using System.Text.Json.Serialization;
using AccOsuMemory.Core.Models.SayoModels.Enum;

namespace AccOsuMemory.Core.Models.SayoModels.QueryParams;

public class SayoQueryParams
{
    [JsonPropertyName("cmd")] public string Cmd { get; init; }
    [JsonPropertyName("limit")] public int Limit { get; init; }
    [JsonPropertyName("offset")] public int Offset { get; init; }
    [JsonPropertyName("type")] public SearchType Type { get; set; }
    [JsonPropertyName("genre")] public Genre Genre { get; set; }
    [JsonPropertyName("language")] public Language Language { get; set; }
    [JsonPropertyName("mode")] public Mode Mode { get; set; }
    [JsonPropertyName("class")] public ApprovedState ApprovedState { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("keyword")]
    public string? KeyWord { get; set; }

    [JsonPropertyName("subtype")] public SubType SubType { get; set; }
}