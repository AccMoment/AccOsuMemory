namespace AccOsuMemory.Core.OsuDataReader.Enum;

internal static class EnumParser
{
    public static Permission GetPermissionEnum(int x) => x switch
    {
        0 => Permission.None,
        1 => Permission.Normal,
        2 => Permission.Moderator,
        4 => Permission.Supporter,
        8 => Permission.Friend,
        16 => Permission.Peppy,
        32 => Permission.WorldCupStaff,
        _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
    };

    public static Grade GetGradeEnum(int x) => x switch
    {
        0x00 => Grade.SSPlus,
        0x01 => Grade.SPlus,
        0x02 => Grade.SS,
        0x03 => Grade.S,
        0x04 => Grade.A,
        0x05 => Grade.B,
        0x06 => Grade.C,
        0x07 => Grade.D,
        0x09 => Grade.UnPlayed,
        _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
    };

    public static Mode GetModeEnum(int x) => x switch
    {
        0=>Mode.Standard,
        1=>Mode.Taiko,
        2=>Mode.CatchTheBeat,
        3=>Mode.Mania,
        _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
    };

    public static RankedStatus GetRankStatusEnum(int x) => x switch
    {
        0x00 => RankedStatus.Unknown,
        0x01 => RankedStatus.Unsubmitted,
        0x02 => RankedStatus.PendingWipGraveyard,
        0x04 => RankedStatus.Ranked,
        0x05 => RankedStatus.Approved,
        0x06 => RankedStatus.Qualified,
        0x07 => RankedStatus.Loved,
        _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
    };

}