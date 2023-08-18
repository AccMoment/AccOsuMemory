using System.ComponentModel;

namespace AccOsuMemory.Core.Models.SayoModels.Enum;
[Description("语言")]
public enum Language
{
    Any = 1,
    Unspecified = 2,
    English = 4,
    Japanese = 8,
    Chinese = 16,
    Instrumental = 32,
    Korean = 64,
    French = 128,
    German = 256,
    Swedish = 512,
    Spanish = 1024,
    Italian = 2048,
    // Russian = 12,
    // Polish = 13,
    Other = 4096,
    All = Any | Unspecified | English | Japanese | Chinese | Instrumental | Korean | French | German | Swedish | Spanish | Italian
}