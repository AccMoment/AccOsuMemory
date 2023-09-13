using System.Net.Http.Handlers;

namespace AccOsuMemory.Core.Net;

public interface IHttpTask
{
    public long Id { get; init; }

    public string Url { get; init; }

    public string ErrorMessage { get; set; }

    public void OnStart();

    public void OnDownload(object? sender, HttpProgressEventArgs e);

    public Task OnFinished(Stream responseStream);

    public void OnError(string errorMessage);
}