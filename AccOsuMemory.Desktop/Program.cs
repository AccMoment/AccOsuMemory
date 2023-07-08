using Avalonia;
using System;
using Avalonia.Dialogs;
using Avalonia.Logging;
using Avalonia.Media;

namespace AccOsuMemory.Desktop
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .With(new FontManagerOptions
                {
                    DefaultFamilyName = "avares://AccOsuMemory/Assets/Fonts/LXGWWenKaiLite-Regular.ttf#LXGW WenKai Lite",
                    // FontFallbacks = new []
                    // {
                    //     new FontFallback
                    //     {
                    //         FontFamily = new FontFamily("avares://AccOsuMemory/Assets/Fonts/LXGWWenKaiLite-Regular.ttf#LXGW WenKai Lite")
                    //     }
                    // }
                });
        // .WithIcons(builder=>builder.Register<FontAwesomeIconProvider>());
    }
}