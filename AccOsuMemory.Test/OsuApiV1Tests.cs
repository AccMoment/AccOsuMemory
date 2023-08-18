using System.Diagnostics;
using System.Net.Http.Handlers;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.OsuApi.V1;
using AccOsuMemory.Desktop.Services;
using Microsoft.Extensions.Options;
using NUnit.Framework.Internal;

namespace AccOsuMemory.Test;

public class OsuApiV1Tests
{
    private HttpClient HttpClient;
    private OsuApiService  api;
    private ProgressMessageHandler pmh = new(new HttpClientHandler());
    [SetUp]
    public void Setup()
    {
        
        HttpClient = new(pmh);
        api = new(new OptionsWrapper<AppSettings>(new AppSettings()
        {
            ApiV1Key = "c79b5e03926c8013656913718b7cb609a726470b"
        }),HttpClient);
    }

    [Test]
    public async Task GetBeatMapList()
    {
        var list = await api.GetBeatMaps(new DateTime(2022, 2, 2));
        Assert.That(list, Is.Not.Empty);
    }

    [Test]
    public async Task GetUserScores()
    {
        var list = await api.GetMapScores(693195, "AccMoment");
        Assert.That(list, Is.Not.Empty);
    }

    [Test]
    public async Task GetMapScores()
    {
        var list = await api.GetMapScores(693195);
        Assert.That(list, Is.Not.Empty);
    }

    [Test]
    public async Task GetUserBP()
    {
        var list = await api.GetUserBP("AccMoment");
        Assert.That(list, Is.Not.Empty);
    }

    [Test]
    public async Task GetUserInfo()
    {
        var user = await api.GetUserInfo("AccMoment");
        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public async Task GetUserRecentPlay()
    {
        var list = await api.GetRecentScore("AccMoment");

        Assert.That(list, Is.Not.Empty);
    }
    
    
   
}