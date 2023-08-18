using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AccOsuMemory.Core.Attribute;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Models.OsuModels;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;
using AccOsuMemory.Core.Models.OsuModels.V1.Enum;
using AccOsuMemory.Core.Models.OsuModels.V1.UrlParameters;
using AccOsuMemory.Core.Models.OsuModels.V1.User;
using AccOsuMemory.Core.OsuApi.V1;
using Microsoft.Extensions.Options;

namespace AccOsuMemory.Desktop.Services;

public class OsuApiService : IOsuApiService
{
    private readonly string _key;

    private readonly HttpClient _httpClient;

    public OsuApiService(IOptions<AppSettings> options, HttpClient httpClient)
    {
        _key = options.Value.ApiV1Key;
        _httpClient = httpClient;
    }


    public Task<List<BeatMap>?> GetBeatMaps(DateTime dateTime, GameMode mode = GameMode.Standard, int limit = 500) =>
        GetBeatMaps(new BeatMapParams(dateTime, mode, limit));

    public async Task<List<BeatMap>?> GetBeatMaps(BeatMapParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.BeatMap);
        return await JsonSerializer.DeserializeAsync<List<BeatMap>>(await _httpClient.GetStreamAsync(url),
            new JsonSerializerOptions
            {
                TypeInfoResolver = OsuJsonSerializerContext.Default,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
    }

    public Task<List<UserBestScore>?> GetUserBP(string username, GameMode mode = GameMode.Standard, int limit = 10) =>
        GetUserBP(new UserBestParams(username, mode, limit));

    public Task<List<UserBestScore>?> GetUserBP(int userId, GameMode mode = GameMode.Standard, int limit = 10) =>
        GetUserBP(new UserBestParams(userId, mode, limit));

    public async Task<List<UserBestScore>?> GetUserBP(UserBestParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.BestPerformance);
        return await JsonSerializer.DeserializeAsync<List<UserBestScore>>(await _httpClient.GetStreamAsync(url)
            , new JsonSerializerOptions
            {
                TypeInfoResolver = OsuJsonSerializerContext.Default,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
    }

    public Task<UserInfo?> GetUserInfo(string userName, GameMode mode = GameMode.Standard, int eventDays = 1) =>
        GetUserInfo(new UserInfoParams(userName, mode, eventDays));

    public Task<UserInfo?> GetUserInfo(int userId, GameMode mode = GameMode.Standard, int eventDays = 1) =>
        GetUserInfo(new UserInfoParams(userId, mode, eventDays));

    public async Task<UserInfo?> GetUserInfo(UserInfoParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.User);
        return (await JsonSerializer.DeserializeAsync<List<UserInfo>>(await _httpClient.GetStreamAsync(url),
            new JsonSerializerOptions
            {
                TypeInfoResolver = OsuJsonSerializerContext.Default,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            }))?.First();
    }

    public Task<List<MapScore>?> GetMapScores(int beatMapId) => GetMapScores(new ScoresParams(beatMapId));

    public Task<List<MapScore>?>
        GetMapScores(int beatMapId, string? userName, GameMode mode = GameMode.Standard) =>
        GetMapScores(new ScoresParams(beatMapId, mode, userName));

    public Task<List<MapScore>?> GetMapScores(int beatMapId, int? userId, GameMode mode = GameMode.Standard) =>
        GetMapScores(new ScoresParams(beatMapId, mode, userId));

    public async Task<List<MapScore>?> GetMapScores(ScoresParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.Scores);
        return await JsonSerializer.DeserializeAsync<List<MapScore>>(await _httpClient.GetStreamAsync(url),
            new JsonSerializerOptions
            {
                TypeInfoResolver = OsuJsonSerializerContext.Default,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
    }

    public Task<List<UserRecentScore>?> GetRecentScore(string userName, GameMode mode = GameMode.Standard,
        int limit = 500) => GetRecentScore(new UserRecentScoreParams(userName, mode, limit));

    public Task<List<UserRecentScore>?> GetRecentScore(int userId, GameMode mode = GameMode.Standard,
        int limit = 500) => GetRecentScore(new UserRecentScoreParams(userId, mode, limit));

    public async Task<List<UserRecentScore>?> GetRecentScore(UserRecentScoreParams param)
    {
        var url = BuildUrl(param, ApiUrlV1.RecentlyPlayed);
        return await JsonSerializer.DeserializeAsync<List<UserRecentScore>>(await _httpClient.GetStreamAsync(url),
            new JsonSerializerOptions
            {
                TypeInfoResolver = OsuJsonSerializerContext.Default,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
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