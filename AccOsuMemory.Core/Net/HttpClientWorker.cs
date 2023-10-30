using System.Net;
using System.Net.Http.Handlers;

namespace AccOsuMemory.Core.Net;

public class HttpClientWorker : HttpClient
{
    public bool IsWorking { get; private set; }

    private readonly ProgressMessageHandler? _handler;

    private readonly CancellationTokenSource _cancellationTokenSource = new();


    public HttpClientWorker(ProgressMessageHandler handler) : base(handler)
    {
        _handler = handler;
    }

    public async void Work(IHttpTask task)
    {
        try
        {
            IsWorking = true;
            var request = new HttpRequestMessage(HttpMethod.Get, task.Url);
            request.Headers.Referrer = new Uri("https://github.com/AccMoment/AccOsuMemory");
            if (_handler != null)
                _handler.HttpReceiveProgress += task.OnDownload;
            task.OnStart();
            var response = await SendAsync(request, _cancellationTokenSource.Token);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                task.OnError("发生错误：可能因为以下原因导致不能下载：服务器资源不足，读取失败，ppy不给下载");
                return;
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            await task.OnFinished(responseStream);
        }
        catch (HttpRequestException e)
        {
            task.OnError(e.Message);
        }
        catch (TaskCanceledException)
        {
            task.OnError("下载时间过长,请重新尝试下载!");
        }
        finally
        {
            if (_handler != null)
                _handler.HttpReceiveProgress -= task.OnDownload;
            _cancellationTokenSource.TryReset();
            IsWorking = false;
        }
    }

    public void CancelWork()
    {
        if (!IsWorking) return;
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.TryReset();
    }
}