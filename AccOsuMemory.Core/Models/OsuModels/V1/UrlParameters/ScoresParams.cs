using System.ComponentModel;
using AccOsuMemory.Core.Attribute;
using AccOsuMemory.Core.Models.OsuModels.V1.Enum;

namespace AccOsuMemory.Core.Models.OsuModels.V1.UrlParameters;

public class ScoresParams
{
    #region Constructor

    public ScoresParams(int beatMapId)
    {
        this.BeatMapId = beatMapId;
    }

    public ScoresParams(int beatMapId, GameMode mode, string? userName)
    {
        this.BeatMapId = beatMapId;
        this.UserName = userName;
        this.Mode = mode;
        if (this.UserName != null) this.Type = "string";
    }

    public ScoresParams(int beatMapId, GameMode mode, int? userId)
    {
        this.BeatMapId = beatMapId;
        this.UserId = userId;
        this.Mode = mode;
        if (this.UserId != null) this.Type = "id";
    }

    #endregion

    #region Properties

    [UrlParam("b")]
    [Description("specify a beatmap_id to return metadata from.")]
    public int BeatMapId { get; set; }

    [UrlParam("u")]
    [Description("specify a user_id or a username to return metadata from.")]
    public int? UserId { get; set; } = null;

    [UrlParam("u")]
    [Description("specify a user_id or a username to return metadata from.")]
    public string? UserName { get; set; } = null;

    [UrlParam("type")]
    [Description(
        "specify if u is a user_id or a username. Use string for usernames or id for user_ids. Optional, default behaviour is automatic recognition (may be problematic for usernames made up of digits only).")]
    public string? Type { get; set; } = null;

    [UrlParam("m")]
    [Description(
        "mode (0 = osu!, 1 = Taiko, 2 = CtB, 3 = osu!mania). Optional, maps of all modes are returned by default.")]
    public GameMode Mode { get; set; } = GameMode.Standard;

    [UrlParam("mods")]
    [Description("specify a mod or mod combination")]
    public int Mods { get; set; } = (int)GameMods.None;

    [UrlParam("limit")]
    [Description("the amount of results. Optional, default and maximum are 500.")]
    public int Limit { get; set; } = 500;

    #endregion
}