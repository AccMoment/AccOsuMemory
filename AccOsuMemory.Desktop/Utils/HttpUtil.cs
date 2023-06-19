using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AccOsuMemory.Desktop.Utils;

public static class HttpUtil
{
    public static HttpClient HttpClient = new()
    {
        DefaultRequestHeaders =
        {
            Referrer = new Uri("https://github.com/AccMoment/AccOsuMemory")
        }
    };
}