using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;

namespace AccOsuMemory.Desktop.Services;

public interface ISayoApiService
{
    Task<BeatMapList>  GetBeatmapList(int currentPage, int offset = 25,SearchType type = SearchType.New);
}