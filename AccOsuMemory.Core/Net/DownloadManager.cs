using System.Net.Http.Handlers;
using AccOsuMemory.Core.Utils;

namespace AccOsuMemory.Core.Net;

public class DownloadManager
{
    private int _maxTaskWorker;
    private HttpClientWorker[] _workers;
    private readonly List<IHttpTask> _httpTasks;

    public bool IsRunning { get; private set; }
    public int WorkingTasks => _httpTasks.Count;

    public static readonly HttpClient HttpClient = new()
    {
        DefaultRequestHeaders =
        {
            Referrer = new Uri("https://github.com/AccMoment/AccOsuMemory")
        },
        Timeout = new TimeSpan(0, 0, 10, 0)
    };

    public DownloadManager(int maxTaskWorker = 3)
    {
        _maxTaskWorker = maxTaskWorker < 1 ? 1 : maxTaskWorker;
        _httpTasks = new List<IHttpTask>();
        SetTaskWorker(maxTaskWorker);
    }

    public void SetTaskWorker(int maxTaskWorker)
    {
        if (IsRunning) throw new InvalidOperationException("当前任务正在运行，请勿修改！");
        if (maxTaskWorker < 1) _maxTaskWorker = 1;
        _workers = new HttpClientWorker[maxTaskWorker];
        for (var i = 0; i < _maxTaskWorker; i++)
        {
            _workers[i] = new HttpClientWorker(new ProgressMessageHandler(new HttpClientHandler()));
        }
    }


    public void SubmitTask(IHttpTask task)
    {
        _httpTasks.Add(task);
    }

    public bool CancelTask(IHttpTask task)
    {
        var t = _httpTasks.Find(f => f.Id == task.Id);
        if (t == null) return false;
        _httpTasks.Remove(t);
        return true;
    }

    public void Start()
    {
        Task.Run(() =>
        {
            IsRunning = true;
            while (_httpTasks.Count != 0)
            {
                for (var i = 0; i < _maxTaskWorker; i++)
                {
                    if (_httpTasks.Count == 0) break;
                    var worker = _workers[i];
                    if (worker.IsWorking) continue;
                    var task = _httpTasks.GetAndRemove();
                    Task.Run(() => worker.Work(task));
                    Task.Delay(100);
                }
            }

            IsRunning = false;
        });
    }
}