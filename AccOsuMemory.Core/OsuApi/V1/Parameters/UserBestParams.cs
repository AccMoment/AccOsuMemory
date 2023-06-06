using System.ComponentModel;
using AccOsuMemory.Core.OsuApi.Utils;
using AccOsuMemory.Core.OsuApi.V1.Enum;

namespace AccOsuMemory.Core.OsuApi.V1.Parameters;

public class UserBestParams
{
    public UserBestParams()
    {
        
    }

    public UserBestParams(string username, GameMode mode, int limit = 10)
    {
        this.User = username;
        this.Mode = mode;
        this.Limit=limit;
    }
    
    [UrlParaName("type")]
    [Description("specify if u is a user_id or a username. Use string for usernames or id for user_ids. Optional, default behaviour is automatic recognition (may be problematic for usernames made up of digits only).")]
    public string? Type { get; set; } = null;
    
    [UrlParaName("m")]
    [Description("mode (0 = osu!, 1 = Taiko, 2 = CtB, 3 = osu!mania). Optional, maps of all modes are returned by default.")]
    public GameMode Mode { get; set; } = GameMode.Standard;
    
    [UrlParaName("u")]
    [Description("specify a user_id or a username to return metadata from.")]
    public string User { get; set; } =string.Empty;
    
    
    [UrlParaName("limit")]
    [Description("the amount of results. Optional, default and maximum are 500.")]
    public int Limit { get; set; } = 500;
}