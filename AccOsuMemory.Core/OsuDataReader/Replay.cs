using AccOsuMemory.Core.OsuDataReader.Enum;

namespace AccOsuMemory.Core.OsuDataReader;

public record Replay(
    Mode Mode,
    int Version,
    string? BeatmapHash,
    string? PlayerName,
    string? ReplayHash,
    short Count300,
    short Count100,
    short Count50,
    short CountGeKi,
    short CountKaTsu,
    short CountMiss,
    int Score,
    short MaxCombo,
    bool PerfectCombo,
    int Mods,
    string? LifeGraph,
    DateTime TimeStamp,
    // List<Action> replayData,
    // List<byte> rawReplayData,
    long OnlineScoreId
);

public record Action(
    long delta,
    float x,
    float y,
    float z
    );