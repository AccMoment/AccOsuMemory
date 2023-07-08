using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;
using AccOsuMemory.Core.Models.SayoModels;

namespace AccOsuMemory.Core.Utils;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(BeatMapList))]
[JsonSerializable(typeof(SayoQueryParams))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}