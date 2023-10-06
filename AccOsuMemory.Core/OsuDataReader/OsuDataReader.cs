using AccOsuMemory.Core.OsuDataReader.Enum;

namespace AccOsuMemory.Core.OsuDataReader;

public static class OsuDataReader
{
    public static OsuData GetOsuDataFromOsuDb(string filePath)
    {
        var file = new FileInfo(filePath);
        if (file.Name != "osu!.db") throw new Exception("路径错误,请检查osu!.db文件的路径是否正确!");
        using var stream = file.Open(FileMode.Open, FileAccess.Read);
        var version = stream.ReadInt();
        var folderCount = stream.ReadInt();
        stream.ReadBoolean(); //read accountUnlocked
        var unlockDate = new DateTime(stream.ReadLong());
        var playerName = stream.ReadString();
        var beatmaps = stream.GetBeatmaps();
        var permission = EnumParser.GetPermissionEnum(stream.ReadByte());
        return new OsuData(version, folderCount, unlockDate, playerName, beatmaps, permission);
    }

    public static OsuCollectionList GetListFromOsuCollectionDb(string filePath)
    {
        var file = new FileInfo(filePath);
        if (file.Name != "collection.db") throw new Exception("路径错误,请检查collection.db文件的路径是否正确!");
        using var stream = file.Open(FileMode.Open, FileAccess.Read);
        var version = stream.ReadInt();
        var collections = stream.GetCollections();
        return new OsuCollectionList(version, collections);
    }

    public static ScoreList GetScoresListFromOsuScoresDb(string filePath)
    {
        var file = new FileInfo(filePath);
        if (file.Name != "scores.db") throw new Exception("路径错误,请检查scores.db文件的路径是否正确!");
        using var stream = file.Open(FileMode.Open, FileAccess.Read);
        var version = stream.ReadInt();
        var collections = stream.GetBeatmapScoresList();
        return new ScoreList(version, collections);
    }

    private static List<Beatmap> GetBeatmaps(this Stream stream)
    {
        var mapCount = stream.ReadInt();
        var list = new List<Beatmap>();
        for (var i = 0; i < mapCount; i++)
        {
            var artistAscii = stream.ReadString();
            var artistUnicode = stream.ReadString();
            var titleAscii = stream.ReadString();
            var titleUnicode = stream.ReadString();
            var creator = stream.ReadString();
            var difficultyName = stream.ReadString();
            var audio = stream.ReadString();
            var hash = stream.ReadString();
            var fileName = stream.ReadString();
            var status = EnumParser.GetRankStatusEnum(stream.ReadByte());
            var hitCircleCount = stream.ReadShort();
            var sliderCount = stream.ReadShort();
            var spinnerCount = stream.ReadShort();
            var lastModified = new DateTime(stream.ReadLong());
            var approachRate = stream.ReadFloat();
            var circleSize = stream.ReadFloat();
            var hpDrain = stream.ReadFloat();
            var overallDifficulty = stream.ReadFloat();
            var sliderVelocity = stream.ReadDouble();
            var stdRatings = stream.ReadStarRatings();
            var taikoRatings = stream.ReadStarRatings();
            var ctbRatings = stream.ReadStarRatings();
            var maniaRatings = stream.ReadStarRatings();
            var drainTime = stream.ReadInt();
            var totalTime = stream.ReadInt();
            var previewTime = stream.ReadInt();
            var timingPoints = stream.ReadTimingPoints();
            var beatmapId = stream.ReadInt();
            var beatmapSetId = stream.ReadInt();
            var threadId = stream.ReadInt();
            var stdGrade = EnumParser.GetGradeEnum(stream.ReadByte());
            var taikoGrade = EnumParser.GetGradeEnum(stream.ReadByte());
            var ctbGrade = EnumParser.GetGradeEnum(stream.ReadByte());
            var maniaGrade = EnumParser.GetGradeEnum(stream.ReadByte());
            var localBeatMapOffset = stream.ReadShort();
            var stackLeniency = stream.ReadFloat();
            var mode = EnumParser.GetModeEnum(stream.ReadByte());
            var songSource = stream.ReadString();
            var tags = stream.ReadString();
            var onlineOffset = stream.ReadShort();
            var titleFont = stream.ReadString();
            var unPlayed = stream.ReadBoolean();
            var lastPlayed = new DateTime(stream.ReadLong());
            var isOsz2 = stream.ReadBoolean();
            var folderName = stream.ReadString();
            var lastOnlineCheck = new DateTime(stream.ReadLong());
            var isIgnoreSounds = stream.ReadBoolean();
            var isIgnoreSkin = stream.ReadBoolean();
            var isDisableStoryBoard = stream.ReadBoolean();
            var isDisableVideo = stream.ReadBoolean();
            var isVisualOverride = stream.ReadBoolean();
            var mysteriousLastModified = stream.ReadInt();
            var maniaScrollSpeed = stream.ReadByte();
            list.Add(new Beatmap(artistAscii, artistUnicode, titleAscii, titleUnicode, creator, difficultyName, audio,
                hash, fileName, status, hitCircleCount, sliderCount, spinnerCount, lastModified, approachRate,
                circleSize, hpDrain, overallDifficulty, sliderVelocity, stdRatings, taikoRatings, ctbRatings,
                maniaRatings, drainTime, totalTime, previewTime, timingPoints, beatmapId, beatmapSetId, threadId,
                stdGrade, taikoGrade, ctbGrade, maniaGrade, localBeatMapOffset, stackLeniency, mode, songSource, tags,
                onlineOffset, titleFont, unPlayed, lastPlayed, isOsz2, folderName, lastOnlineCheck, isIgnoreSounds,
                isIgnoreSkin, isDisableStoryBoard, isDisableVideo, isVisualOverride, mysteriousLastModified,
                maniaScrollSpeed));
        }

        return list;
    }

    private static List<OsuCollection> GetCollections(this Stream stream)
    {
        var collectionCount = stream.ReadInt();
        var collections = new List<OsuCollection>();
        for (var i = 0; i < collectionCount; i++)
        {
            var name = stream.ReadString();
            var size = stream.ReadInt();
            var beatmapHashes = new List<string?>();
            for (var j = 0; j < size; j++)
            {
                var hash = stream.ReadString();
                beatmapHashes.Add(hash);
            }

            collections.Add(new OsuCollection(name, beatmapHashes));
        }

        return collections;
    }

    private static List<BeatmapScores> GetBeatmapScoresList(this Stream stream)
    {
        var count = stream.ReadInt();
        var list = new List<BeatmapScores>();
        for (var i = 0; i < count; i++)
        {
            var hash = stream.ReadString();
            var replayList = stream.GetReplayList();
            var scores = new BeatmapScores(hash, replayList);
            list.Add(scores);
        }

        return list;
    }

    private static List<Replay> GetReplayList(this Stream stream)
    {
        var count = stream.ReadInt();
        var list = new List<Replay>();
        for (var i = 0; i < count; i++)
        {
            var mode = EnumParser.GetModeEnum(stream.ReadByte());
            var version = stream.ReadInt();
            var beatmapHash = stream.ReadString();
            var playerName = stream.ReadString();
            var replayHash = stream.ReadString();
            var count300 = stream.ReadShort();
            var count100 = stream.ReadShort();
            var count50 = stream.ReadShort();
            var countGeKi = stream.ReadShort();
            var countKaTsu = stream.ReadShort();
            var countMiss = stream.ReadShort();
            var score = stream.ReadInt();
            var maxCombo = stream.ReadShort();
            var perfectCombo = stream.ReadBoolean();
            var mods = stream.ReadInt();
            var lifeGraph = stream.ReadString();
            var timeStamp = new DateTime(stream.ReadLong());
            stream.ReadInt(); // don't know what is this
            var onlineScoreId = stream.ReadLong();
            var replay = new Replay(mode, version, beatmapHash, playerName, replayHash, count300, count100, count50,
                countGeKi, countKaTsu, countMiss, score, maxCombo, perfectCombo, mods, lifeGraph, timeStamp,
                onlineScoreId);
            list.Add(replay);
        }

        return list;
    }
}