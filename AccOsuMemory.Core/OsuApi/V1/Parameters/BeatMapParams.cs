using System.ComponentModel;
using System.Text.Json.Serialization;
using AccOsuMemory.Core.OsuApi.Utils;
using AccOsuMemory.Core.OsuApi.V1.Enum;

namespace AccOsuMemory.Core.OsuApi.V1.Parameters.BeatMap;


public class BeatMapParams
{
    public BeatMapParams(DateTime dateTime, GameMode mode, int limit)
    {
        this.SinceDate = dateTime;
        this.Mode = mode;
        this.Limit = limit;
    }

    public BeatMapParams()
    {
        
    }
    [UrlParaName("since")]
    [Description("return all beatmaps ranked or loved since this date. Must be a MySQL date. In UTC")]
    public DateTime SinceDate { get; set; } = new DateTime(2007, 10, 6);

    [UrlParaName("s")]
    [Description("specify a beatmapset_id to return metadata from.")]
    public int? BeatMapSetId { get; set; } = null;

    [UrlParaName("b")]
    [Description("specify a beatmap_id to return metadata from.")]
    public int? BeatMapId { get; set; } = null;

    [UrlParaName("u")]
    [Description("specify a user_id or a username to return metadata from.")]
    public string? User { get; set; } = null;

    [UrlParaName("type")]
    [Description("specify if u is a user_id or a username. Use string for usernames or id for user_ids. Optional, default behaviour is automatic recognition (may be problematic for usernames made up of digits only).")]
    public string? Type { get; set; } = null;

    [UrlParaName("m")]
    [Description("mode (0 = osu!, 1 = Taiko, 2 = CtB, 3 = osu!mania). Optional, maps of all modes are returned by default.")]
    public GameMode Mode { get; set; } = GameMode.Standard;

    [UrlParaName("a")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    [Description(
        " specify whether converted beatmaps are included (0 = not included, 1 = included). Only has an effect if m is chosen and not 0. Converted maps show their converted difficulty rating. Optional, default is 0.")]
    public bool IncludeConvertedBeatMaps { get; set; } = false;

    [UrlParaName("h")]
    [Description("the beatmap hash. It can be used, for instance, if you're trying to get what beatmap has a replay played in, as .osr replays only provide beatmap hashes (example of hash: a5b99395a42bd55bc5eb1d2411cbdf8b). Optional, by default all beatmaps are returned independently from the hash.")]
    public string? BeatMapHash { get; set; } = null;

    [UrlParaName("limit")]
    [Description("the amount of results. Optional, default and maximum are 500.")]
    public int Limit { get; set; } = 500;

    [UrlParaName("mods")]
    [Description(
        "mods that applies to the beatmap requested. Optional, default is 0. (Refer to the Mods section below, note that requesting multiple mods is supported, but it should not contain any non-difficulty-increasing mods or the return value will be invalid.)")]
    public GameMods Mods { get; set; } = GameMods.None;
}