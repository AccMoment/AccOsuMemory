using System.Net.Http.Handlers;

namespace AccOsuMemory.Core.Net;

public class HttpClientWorker:HttpClient
{
    public bool IsWorking { get; private set; }

    private readonly ProgressMessageHandler? _handler;

    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    
    public HttpClientWorker(ProgressMessageHandler handler) :base(handler)
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
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            await task.OnFinished(responseStream);
        }
        catch (HttpRequestException e)
        {
            task.ErrorMessage = e.Message;
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