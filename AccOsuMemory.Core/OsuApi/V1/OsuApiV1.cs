using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using AccOsuMemory.Core.Attribute;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;
using AccOsuMemory.Core.Models.OsuModels.V1.Enum;
using AccOsuMemory.Core.Models.OsuModels.V1.UrlParameters;
using AccOsuMemory.Core.Models.OsuModels.V1.User;
using AccOsuMemory.Core.Net;

namespace AccOsuMemory.Core.OsuApi.V1;

public class OsuApiV1
{
    private readonly string _key;

    public OsuApiV1(string key)
    {
        _key = key;
    }


    private static readonly HttpClient HttpClient = DownloadManager.HttpClient;

    public Task<List<BeatMap>?> GetBeatMaps(DateTime dateTime, GameMode mode = GameMode.Standard, int limit = 500) =>
        GetBeatMaps(new BeatMapParams(dateTime, mode, limit));

    [RequiresUnreferencedCode("GetBeatMaps")]
    public Task<List<BeatMap>?> GetBeatMaps(BeatMapParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.BeatMap);
        return HttpClient.GetFromJsonAsync<List<BeatMap>?>(url);
    }

    public Task<List<UserBestScore>?> GetUserBP(string username, GameMode mode = GameMode.Standard, int limit = 10) =>
        GetUserBP(new UserBestParams(username, mode, limit));

    public Task<List<UserBestScore>?> GetUserBP(int userId, GameMode mode = GameMode.Standard, int limit = 10) =>
        GetUserBP(new UserBestParams(userId, mode, limit));

    [RequiresUnreferencedCode("GetUserBP")]
    public Task<List<UserBestScore>?> GetUserBP(UserBestParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.BestPerformance);
        return HttpClient.GetFromJsonAsync<List<UserBestScore>>(url);
    }

    public Task<UserInfo?> GetUserInfo(string userName, GameMode mode = GameMode.Standard, int eventDays = 1) =>
        GetUserInfo(new UserInfoParams(userName, mode, eventDays));

    public Task<UserInfo?> GetUserInfo(int userId, GameMode mode = GameMode.Standard, int eventDays = 1) =>
        GetUserInfo(new UserInfoParams(userId, mode, eventDays));

    [RequiresUnreferencedCode("GetUserBP")]
    public async Task<UserInfo?> GetUserInfo(UserInfoParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.User);
        var list = await HttpClient.GetFromJsonAsync<List<UserInfo>>(url);
        return list?[0];
    }

    public Task<List<MapScore>?> GetMapScores(int beatMapId) => GetMapScores(new ScoresParams(beatMapId));

    public Task<List<MapScore>?>
        GetMapScores(int beatMapId, string? userName, GameMode mode = GameMode.Standard) =>
        GetMapScores(new ScoresParams(beatMapId, mode, userName));

    public Task<List<MapScore>?> GetMapScores(int beatMapId, int? userId, GameMode mode = GameMode.Standard) =>
        GetMapScores(new ScoresParams(beatMapId, mode, userId));

    [RequiresUnreferencedCode("GetUserBP")]
    public Task<List<MapScore>?> GetMapScores(ScoresParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.Scores);
        return HttpClient.GetFromJsonAsync<List<MapScore>>(url);
    }

    public Task<List<UserRecentScore>?> GetRecentScore(string userName, GameMode mode = GameMode.Standard,
        int limit = 500) => GetRecentScore(new UserRecentScoreParams(userName, mode, limit));

    public Task<List<UserRecentScore>?> GetRecentScore(int userId, GameMode mode = GameMode.Standard,
        int limit = 500) => GetRecentScore(new UserRecentScoreParams(userId, mode, limit));

    [RequiresUnreferencedCode("GetUserBP")]
    public Task<List<UserRecentScore>?> GetRecentScore(UserRecentScoreParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.RecentlyPlayed);
        return HttpClient.GetFromJsonAsync<List<UserRecentScore>>(url);
    }


    private string BuildUrl<T>(T param, string url)
    {
        var ps = param!.GetType().GetProperties();
        var sb = new StringBuilder();
        sb.Append($"{url}?k={_key}");
        foreach (var p in ps)
        {
            var k = p.GetCustomAttribute<UrlParam>()?.Name;
            var v = p.GetValue(param);
            switch (v)
            {
                case null:
                    continue;
                case DateTime d:
                    sb.Append($"&{k}={d:yyyy-M-d}");
                    break;
                case string s:
                    if (!string.IsNullOrWhiteSpace(s)) sb.Append($"&{k}={s}");
                    break;
                case int i:
                    sb.Append($"&{k}={i}");
                    break;
                case bool b:
                    sb.Append($"&{k}={(b ? 1 : 0)}");
                    break;
                case GameMode mode:
                    sb.Append($"&{k}={(int)mode}");
                    break;
                case GameMods mods:
                    sb.Append($"&{k}={(int)mods}");
                    break;
            }
        }

        return sb.ToString();
    }
}