using System.Net.Http.Handlers;

namespace AccOsuMemory.Core.Net;

public interface IHttpTask
{
    public long Id { get;  }

    public string Url { get;  }
    
    
    public void OnStart();

    public void OnDownload(object? sender, HttpProgressEventArgs e);

    public Task OnFinished(Stream responseStream);

    public void OnError(string errorMessage);
}