namespace AccOsuMemory.Core.OsuDataReader.Enum;

public enum RankedStatus
{
    Unknown = 0x00,
    Unsubmitted = 0x01,

    /// Any of the three.
    PendingWipGraveyard = 0x02,
    Ranked = 0x04,
    Approved = 0x05,
    Qualified = 0x06,
    Loved = 0x07
}