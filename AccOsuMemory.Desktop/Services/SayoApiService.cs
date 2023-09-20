using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using AccOsuMemory.Core.Models.SayoModels.QueryParams;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.Utils;

namespace AccOsuMemory.Desktop.Services;

public class SayoApiService : ISayoApiService
{
    private const string Url = "https://api.sayobot.cn/?post";
    private const string BeatmapInfoUrl = "https://api.sayobot.cn/v2/beatmapinfo";
    private readonly HttpClient _httpClient;

    public SayoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BeatmapList> GetBeatmapList(int currentPage, int offset, SearchType searchType,
        Language language, Mode mode, Genre genre, ApprovedState approvedState, SubType subType)
    {
        var body = new SayoQueryParams
        {
            Cmd = "beatmaplist",
            Limit = 25,
            Offset = (currentPage - 1) * offset,
            Type = searchType,
            Language = language,
            Mode = mode,
            Genre = genre,
            ApprovedState = approvedState,
            SubType = subType
        };
        var response = await _httpClient
            .PostAsync(Url,
                new StringContent(JsonSerializer.Serialize(body, SayoJsonSerializerContext.Default.SayoQueryParams)));
        var result = await response.Content.ReadAsStreamAsync();
        var list = await JsonSerializer.DeserializeAsync(result, SayoJsonSerializerContext.Default.BeatmapList);
        if (list == null) throw new Exception("发生错误，请重新尝试!");
        return list;
    }

    public async Task<BeatmapInfo> GetBeatmapListInfo(string keyWord, int type = 0)
    {
        var response = await _httpClient.GetAsync($"{BeatmapInfoUrl}?0={keyWord}");
        var result = await response.Content.ReadAsStreamAsync();
        var beatmapListInfo =
            await JsonSerializer.DeserializeAsync<BeatmapListInfo>(result, new JsonSerializerOptions
            {
                TypeInfoResolver = SayoJsonSerializerContext.Default,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
        if (beatmapListInfo == null) throw new Exception("发生错误，请重新尝试!");
        return beatmapListInfo.BeatmapInfo;
    }
}