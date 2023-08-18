using System.ComponentModel;

namespace AccOsuMemory.Core.Models.SayoModels.Enum;
[Description("分类")]
public enum Genre
{
    Any = 1,
    Unspecified = 2,
    VideoGame = 4,
    Anime = 8,
    Rock = 16,
    Pop = 32,
    Other = 64,
    Novelty = 128,
    HipHop = 256,
    Electronic = 1024,
    // Metal = 11,
    // Classical = 12,
    // Folk = 13,
    // Jazz = 14
    All = Any | Unspecified | VideoGame | Anime | Rock | Pop | Other | Novelty | HipHop | Electronic
}