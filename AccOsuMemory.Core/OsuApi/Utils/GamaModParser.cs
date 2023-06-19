using AccOsuMemory.Core.OsuApi.V1.Enum;

namespace AccOsuMemory.Core.OsuApi.Utils;

public static class GamaModParser
{
    public static int ParseFromGameMods(IEnumerable<GameMods> mods) =>
        mods.Aggregate(0, (current, gameMod) => current | (int)gameMod);

    public static IEnumerable<GameMods> ParseToGameMods(int mods)
        => Enum.GetValues<GameMods>().Where(gameMod => ((int)gameMod & mods) == mods);
}