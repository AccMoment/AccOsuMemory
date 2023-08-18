using System.Collections.Generic;
using System.Threading.Tasks;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap;
using AccOsuMemory.Core.Models.OsuModels.V1.Beatmap.Score;
using AccOsuMemory.Core.Models.OsuModels.V1.UrlParameters;
using AccOsuMemory.Core.Models.OsuModels.V1.User;

namespace AccOsuMemory.Desktop.Services;

public interface IOsuApiService
{
    Task<List<BeatMap>?> GetBeatMaps(BeatMapParams param);

    Task<List<UserBestScore>?> GetUserBP(UserBestParams param);

    Task<UserInfo?> GetUserInfo(UserInfoParams param);

    Task<List<MapScore>?> GetMapScores(ScoresParams param);

    Task<List<UserRecentScore>?> GetRecentScore(UserRecentScoreParams param);

}