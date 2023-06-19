using System.ComponentModel;
using AccOsuMemory.Core.Attribute;
using AccOsuMemory.Core.OsuApi.V1.Enum;

namespace AccOsuMemory.Core.OsuApi.V1.UrlParameters;

public class UserBestParams
{
    #region Constructor

    public UserBestParams(string username, GameMode mode, int limit)
    {
        this.UserName = username;
        this.Mode = mode;
        this.Limit = limit;
        if (this.UserName != null) this.Type = "string";
    }

    public UserBestParams(int userId, GameMode mode, int limit)
    {
        this.UserId = userId;
        this.Mode = mode;
        this.Limit = limit;
        if (this.UserId != null) this.Type = "id";
    }

    #endregion

    #region Properties

    [UrlParam("type")]
    [Description(
        "specify if u is a user_id or a username. Use string for usernames or id for user_ids. Optional, default behaviour is automatic recognition (may be problematic for usernames made up of digits only).")]
    public string? Type { get; set; } = null;

    [UrlParam("m")]
    [Description(
        "mode (0 = osu!, 1 = Taiko, 2 = CtB, 3 = osu!mania). Optional, maps of all modes are returned by default.")]
    public GameMode Mode { get; set; }

    [UrlParam("u")]
    [Description("specify a user_id or a username to return metadata from.")]
    public string? UserName { get; set; } = null;

    [UrlParam("u")]
    [Description("specify a user_id or a username to return metadata from.")]
    public int? UserId { get; set; } = null;

    [UrlParam("limit")]
    [Description("the amount of results. Optional, default and maximum are 500.")]
    public int Limit { get; set; }

    #endregion
}