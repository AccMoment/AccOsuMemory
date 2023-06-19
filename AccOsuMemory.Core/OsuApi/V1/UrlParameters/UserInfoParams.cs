using System.ComponentModel;
using AccOsuMemory.Core.Attribute;
using AccOsuMemory.Core.OsuApi.V1.Enum;

namespace AccOsuMemory.Core.OsuApi.V1.UrlParameters;

public class UserInfoParams
{
    #region Constructor

    public UserInfoParams(string userName, GameMode mode, int eventDays)
    {
        this.UserName = userName;
        this.Mode = mode;
        this.EventDays = eventDays;
        if (this.UserName != null) this.Type = "string";
    }

    public UserInfoParams(int userId, GameMode mode, int eventDays)
    {
        this.UserId = userId;
        this.Mode = mode;
        this.EventDays = eventDays;
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

    [Description("Max number of days between now and last event date. Range of 1-31. Optional, default value is 1.")]
    [UrlParam("event_days")]
    public int EventDays { get; set; }

    #endregion
}