namespace AccOsuMemory.Core.OsuDataReader;

public record BeatmapScores(string? BeatmapHash,List<Replay> Scores);


public record ScoreList(int Version,List<BeatmapScores> BeatmapScores);