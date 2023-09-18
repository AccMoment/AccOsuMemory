using System.Text.Json.Serialization;
using AccOsuMemory.Core.Models.SayoModels.QueryParams;

namespace AccOsuMemory.Core.Models.SayoModels;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(BeatmapList))]
[JsonSerializable(typeof(BeatmapListInfo))]
[JsonSerializable(typeof(SayoQueryParams))]
public partial class SayoJsonSerializerContext:JsonSerializerContext
{
    
}