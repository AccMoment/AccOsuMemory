using Avalonia;
using System;
using System.Threading;
using Avalonia.Dialogs;

namespace AccOsuMemory.Desktop
{
    internal class Program
    {
        const string AppName = "AccOsuMemory";

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var isExists = Mutex.TryOpenExisting(AppName, out var mutex);

            if (isExists && null != mutex)
            {
                //发生消息给已经运行的软件 最大化程序
                // NamedPipeClientStream clientStream = new NamedPipeClientStream(AppName);
                // clientStream.Connect();
                // StreamWriter sw = new StreamWriter(clientStream);
                // sw.WriteLine("Active");
                // sw.Flush();
                // sw.Close();
                // clientStream.Close();

                return;
            }
            else
            {
                //否则创建新的互斥体
                mutex = new Mutex(true, AppName);

                //创建管道接收器
                // NamedPipeServerStream serverStream = new NamedPipeServerStream(AppName);
                // serverStream.BeginWaitForConnection(NPReceiveDataCallBack, serverStream);
            }

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);

            //程序关闭，记得释放所占用资源
            if (null != mutex)
            {
                mutex.Dispose();
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseManagedSystemDialogs()
                .LogToTrace()
                .With(new X11PlatformOptions
                {
                    RenderingMode = new[] { X11RenderingMode.Software, X11RenderingMode.Egl }
                });
        // .With(new FontManagerOptions
        // {
        //     DefaultFamilyName =
        //         "avares://AccOsuMemory/Assets/Fonts/LXGWWenKaiLite-Regular.ttf#LXGW WenKai Lite",
        //     FontFallbacks = new []{ new FontFallback()
        //     {
        //         FontFamily = "avares://AccOsuMemory/Assets/Fonts/LXGWWenKaiLite-Regular.ttf#LXGW WenKai Lite"
        //     }}
        // });
    }
}