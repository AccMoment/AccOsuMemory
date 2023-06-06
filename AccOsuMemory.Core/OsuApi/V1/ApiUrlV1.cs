namespace AccOsuMemory.Core.OsuApi.V1;

public static class ApiUrlV1
{
    private static string BaseUrl => "https://osu.ppy.sh/api";
    public static string BeatMap => $"{BaseUrl}/get_beatmaps";
    public static string User => $"{BaseUrl}/get_user";
    public static string Scores => $"{BaseUrl}/get_scores";
    public static string BestPerformance => $"{BaseUrl}/get_user_best";
    public static string RecentlyPlayed => $"{BaseUrl}/get_user_recent";
    
}