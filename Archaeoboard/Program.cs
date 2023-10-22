using Avalonia;
using Avalonia.Threading;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Archaeoboard
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var application = BuildAvaloniaApp();

            PrepareExceptionHandling();

            application.StartWithClassicDesktopLifetime(args);
        }

        private static void PrepareExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Dispatcher.UIThread.Post(() => WriteException(e.Exception));
                e.SetObserved();
            };
        }

        private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e) => WriteException((Exception)e.ExceptionObject);

        private static void WriteException(Exception exceptionObject)
        {
            Debug.WriteLine(exceptionObject.ToString());
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}