using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.Utils;

namespace AccOsuMemory.Desktop.Services;

public class SayoApiService : ISayoApiService
{
    private const string Url = "https://api.sayobot.cn/?post";
    
    private readonly HttpClient _httpClient;

    public SayoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BeatMapList> GetBeatmapList(int currentPage, int offset, SearchType searchType,
        Language language, Mode mode, Genre genre, ApprovedState approvedState,SubType subType)
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
        var list = JsonSerializer.Deserialize(result, SayoJsonSerializerContext.Default.BeatMapList);
        if (list == null) throw new Exception("发生错误，请重新尝试!");
        return list;
    }
}