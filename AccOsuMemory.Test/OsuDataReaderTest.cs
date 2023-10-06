using System.Diagnostics;
using AccOsuMemory.Core.OsuDataReader;

namespace AccOsuMemory.Test;

public class OsuDataReaderTest
{
    [Test]
    public void GetMainDataTest()
    {
        var watch = new Stopwatch();
        watch.Start();
        var data = OsuDataReader.GetOsuDataFromOsuDb(@"D:\osu!\osu!.db");
        watch.Stop();
        Debug.WriteLine($"timer:{watch.ElapsedMilliseconds}");
        Assert.IsNotNull(data);
    }

   

    [Test]
    public void GetScoresListTest()
    {
        var watch = new Stopwatch();
        watch.Start();
        var data = OsuDataReader.GetScoresListFromOsuScoresDb(@"D:\osu!\scores.db");
        watch.Stop();
        Debug.WriteLine($"timer:{watch.ElapsedMilliseconds}");
        Assert.IsNotNull(data);
    }

    [Test]
    public void GetListFromOsuCollectionDbTest()
    {
        var watch = new Stopwatch();
        watch.Start();
        var data = OsuDataReader.GetListFromOsuCollectionDb(@"D:\osu!\collection.db");
        watch.Stop();
        Debug.WriteLine($"timer:{watch.ElapsedMilliseconds}");
        Assert.IsNotNull(data);
    }
}