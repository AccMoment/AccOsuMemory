namespace AccOsuMemory.Core.OsuDataReader;

public record OsuCollection(
    string? Name,
    List<string?> BeatmapHashes
);

public record OsuCollectionList(
    int Version,
    List<OsuCollection> Collections
);