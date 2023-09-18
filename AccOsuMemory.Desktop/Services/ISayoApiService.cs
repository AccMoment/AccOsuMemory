using System.Threading.Tasks;
using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Core.Models.SayoModels.Enum;

namespace AccOsuMemory.Desktop.Services;

public interface ISayoApiService
{
    Task<BeatmapList> GetBeatmapList(
        int currentPage, int offset = 25, SearchType type = SearchType.Search,
        Language language = Language.All, Mode mode = Mode.All,
        Genre genre = Genre.All, ApprovedState approvedState = ApprovedState.All, SubType subType = SubType.All);

    Task<BeatmapInfo> GetBeatmapListInfo(string keyword, int type = 0);
}