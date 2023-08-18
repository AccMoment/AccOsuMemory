using System.ComponentModel;

namespace AccOsuMemory.Core.Models.SayoModels.Enum;
[Description("状态")]
public enum ApprovedState
{
    RankedOrApproved = 1,
    Qualified = 2,
    Loved = 4,
    PendingOrWIP = 8,
    Graveyard = 16,
    All = RankedOrApproved | Qualified | Loved | PendingOrWIP | Graveyard
}