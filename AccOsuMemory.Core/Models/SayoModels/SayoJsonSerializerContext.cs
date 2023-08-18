using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.Models.SayoModels;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(BeatMapList))]
[JsonSerializable(typeof(SayoQueryParams))]
public partial class SayoJsonSerializerContext:JsonSerializerContext
{
    
}