using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using AccOsuMemory.Core.OsuApi.Utils;
using AccOsuMemory.Core.OsuApi.V1.Enum;
using AccOsuMemory.Core.OsuApi.V1.Model.Beatmap;
using AccOsuMemory.Core.OsuApi.V1.Model.Beatmap.Score;
using AccOsuMemory.Core.OsuApi.V1.Parameters;
using AccOsuMemory.Core.OsuApi.V1.Parameters.BeatMap;

namespace AccOsuMemory.Core.OsuApi.V1;

public class OsuApiV1
{
    public string key;

    public OsuApiV1(string key)
    {
        this.key = key;
    }


    private static HttpClient _httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(10),
    };

    public async Task<List<BeatMap>?> GetBeatMapsByDateTime(DateTime dateTime, GameMode mode, int limit = 500) =>
        await GetBeatMapsByDateTime(new BeatMapParams(dateTime, mode, limit));

    public Task<List<BeatMap>?> GetBeatMapsByDateTime(BeatMapParams param)
    {
        var url = buildUrl(param, ApiUrlV1.BeatMap);
        return _httpClient.GetFromJsonAsync<List<BeatMap>>(url);
    }

    public async Task<List<UserBestScore>?> GetUserBP(string username, GameMode mode, int limit = 10) =>
        await GetUserBP(new UserBestParams(username,mode,limit));


    public Task<List<UserBestScore>?> GetUserBP(UserBestParams param)
    {
        var url = buildUrl(param, ApiUrlV1.BeatMap);
        return _httpClient.GetFromJsonAsync<List<UserBestScore>>(url);
    }

    private string buildUrl<T>(T param, string url)
    {
        var ps = param.GetType().GetProperties();
        var sb = new StringBuilder();
        sb.Append($"{url}?k={key}");
        foreach (var p in ps)
        {
            var k = p.GetCustomAttribute<UrlParaName>()?.Name;
            var v = p.GetValue(param);
            switch (v)
            {
                case null:
                    continue;
                case DateTime d:
                    sb.Append($"&{k}={d:yyyy-M-d}");
                    break;
                case string:
                case int:
                    sb.Append($"&{k}={v}");
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