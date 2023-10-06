using AccOsuMemory.Core.OsuDataReader.Enum;

namespace AccOsuMemory.Core.OsuDataReader;

public record OsuData(
    int Version,
    int FolderCount,
    DateTime UnbanDate,
    string? PlayerName,
    List<Beatmap> Beatmaps,
    Permission UserPermissions
);