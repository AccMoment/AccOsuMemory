using System.Text.Json.Serialization;
using AccOsuMemory.Core.Utils.Converter;

namespace AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;

public class MapScore:ScoreBase
{
   
    [JsonPropertyName("username")] 
    public string UserName { get; set; }
    
    [JsonPropertyName("pp")] 
    public double PP { get; set; }

    [JsonPropertyName("replay_available")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    public bool ReplayAvailable { get; set; }
    
}