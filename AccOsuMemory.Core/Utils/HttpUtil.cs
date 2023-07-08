namespace AccOsuMemory.Core.Utils;

internal class HttpUtil
{
    public static HttpClient HttpClient = new()
    {
        DefaultRequestHeaders =
        {
            Referrer = new Uri("https://github.com/AccMoment/AccOsuMemory")
        }
    };
}