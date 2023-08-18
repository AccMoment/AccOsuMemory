using System.ComponentModel;

namespace AccOsuMemory.Core.Models.SayoModels.Enum;
[Description("模式")]
public enum Mode
{
    Std = 1,
    Taiko = 2,
    Ctb = 4,
    Mania = 8,
    All = Std | Taiko | Ctb | Mania
}