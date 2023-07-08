using System.Diagnostics;
using System.Net.Http.Handlers;
using AccOsuMemory.Core.OsuApi.V1;
using NUnit.Framework.Internal;

namespace AccOsuMemory.Test;

public class OsuApiV1Tests
{
    private HttpClient HttpClient;
    private OsuApiV1 api;
    private ProgressMessageHandler pmh = new(new HttpClientHandler());
    [SetUp]
    public void Setup()
    {
       
        HttpClient = new(pmh);
        api = new("c79b5e03926c8013656913718b7cb609a726470b");
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

    [Test]
    public void NormalTest()
    {
        MyTask t1 = new MyTask(1, pmh, HttpClient);
        MyTask t2 = new MyTask(2, pmh, HttpClient);
        var a = Task.Run(async () =>
        {
            await t1.Download(@"D:\osu!\Songs\a.osz");
        });
        var b = Task.Run(async () =>
        {
            await t2.Download(@"D:\osu!\Songs\b.osz");
        });
        Task.WaitAll(a, b);
    }

  
    
   
}