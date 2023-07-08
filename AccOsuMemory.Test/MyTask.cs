using System.ComponentModel;
using System.Net.Http.Handlers;

namespace AccOsuMemory.Test;

public class MyTask
{
    private readonly ProgressMessageHandler _progressMessageHandler;
    private HttpClient HttpClient;
    private int _id;
    public MyTask(int id, ProgressMessageHandler pmh, HttpClient httpClient)
    {
        _progressMessageHandler = pmh;
        HttpClient = httpClient;
        _id = id;
    }
    
    public async Task Download(string path)
    {
        _progressMessageHandler.HttpReceiveProgress += ReportProgress;
        var url = "https://dl.sayobot.cn/beatmaps/download/mini/1478618";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await HttpClient.SendAsync(request);
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
        await responseStream.CopyToAsync(fileStream);
        _progressMessageHandler.HttpReceiveProgress -= ReportProgress;
    }

    private void ReportProgress(object? s, HttpProgressEventArgs e)
    {
        Console.WriteLine($"{_id}:{(double)e.BytesTransferred/e.TotalBytes*100:F}");
    }
    
    
}