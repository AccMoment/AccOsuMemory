using System.ComponentModel;

namespace AccOsuMemory.Core.Models.SayoModels.Enum;
[Description("范围")]
public enum SubType
{
    Title =1,
    Artist =2,
    Creator =4,
    Version = 8,
    Tags = 16,
    Source =32,
    All = Title | Artist | Creator | Version | Tags | Source
}