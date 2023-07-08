using System.ComponentModel;
using AccOsuMemory.Core.Attribute;
using AccOsuMemory.Core.Models.OsuModels.V1.Enum;

namespace AccOsuMemory.Core.Models.OsuModels.V1.UrlParameters;

public class UserRecentScoreParams
{
    #region Constructor

    public UserRecentScoreParams(string userName, GameMode mode, int limit)
    {
        this.UserName = userName;
        this.Mode = mode;
        this.Limit = limit;
        if (this.UserName != null) this.Type = "string";
    }

    public UserRecentScoreParams(int userId, GameMode mode, int limit)
    {
        this.UserId = userId;
        this.Mode = mode;
        this.Limit = limit;
        if (this.UserId != null) this.Type = "id";
    }

    #endregion

    #region Properties

    [Description("UserName")]
    [UrlParam("u")]
    public string? UserName { get; set; } = null;

    [Description("UserId")]
    [UrlParam("u")]
    public int? UserId { get; set; } = null;

    [Description(" mode (0 = osu!, 1 = Taiko, 2 = CtB, 3 = osu!mania).")]
    [UrlParam("m")]
    public GameMode Mode { get; set; }

    [Description(
        "specify if u is a user_id or a username. Use string for usernames or id for user_ids. Optional, default behaviour is automatic recognition (may be problematic for usernames made up of digits only).")]
    [UrlParam("type")]
    public string? Type { get; set; } = null;

    [UrlParam("limit")]
    [Description("amount of results (range between 1 and 50 - defaults to 10).")]
    public int Limit { get; set; } = 10;

    #endregion
}