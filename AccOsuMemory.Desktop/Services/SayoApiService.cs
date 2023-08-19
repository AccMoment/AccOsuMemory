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

    public async Task<BeatMapList> GetBeatmapList(int currentPage, int offset, SearchType type,
        Language language, Mode mode, Genre genre, ApprovedState approvedState,SubType subType)
    {
        var body = new SayoQueryParams
        {
            Cmd = "beatmaplist",
            Limit = 25,
            Offset = (currentPage - 1) * offset,
            Type = (int)type,
            Language = (int)language,
            Mode = (int)mode,
            Genre = (int)genre,
            ApprovedState = (int) approvedState,
            KeyWord = "",
            SubType = (int)subType
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