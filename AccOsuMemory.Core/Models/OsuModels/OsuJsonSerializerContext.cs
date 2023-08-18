using System.Text.Json.Serialization;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;
using AccOsuMemory.Core.Models.OsuModels.V1.User;

namespace AccOsuMemory.Core.Models.OsuModels;
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(List<BeatMap>))]

[JsonSerializable(typeof(List<UserBestScore>))]
[JsonSerializable(typeof(List<UserInfo>))]
[JsonSerializable(typeof(List<MapScore>))]
[JsonSerializable(typeof(List<UserRecentScore>))]
public partial class OsuJsonSerializerContext : JsonSerializerContext
{
    
}