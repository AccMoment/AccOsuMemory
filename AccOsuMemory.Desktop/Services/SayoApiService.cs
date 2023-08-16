using System;
using System.Net.Http;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.Utils;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AccOsuMemory.Desktop.Services;

public class SayoApiService : ISayoApiService
{
    private const string Url = "https://api.sayobot.cn/?post";

    public async Task<BeatMapList> GetBeatmapList(int currentPage, int offset = 25, SearchType type = SearchType.New)
    {
        var body = new SayoQueryParams
        {
            Cmd = "beatmaplist",
            Limit = 25,
            Offset = (currentPage - 1) * offset,
            Type = (int)type
        };
        var response = await DownloadManager.HttpClient
            .PostAsync(Url,
                new StringContent(JsonSerializer.Serialize(body, SourceGenerationContext.Default.SayoQueryParams)));
        var result = await response.Content.ReadAsStreamAsync();
        var list = JsonSerializer.Deserialize(result, SourceGenerationContext.Default.BeatMapList);
        if (list == null) throw new Exception("发生错误，请重新尝试!");
        return list;
    }
}