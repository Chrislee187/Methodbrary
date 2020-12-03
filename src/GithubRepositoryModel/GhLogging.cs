using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Serilog;

namespace GithubRepositoryModel
{
    public static class GhLogging
    {
        public static ILogger Logger { get; private set; }
        public static void SetLogger(ILogger logger) => Logger = logger;

        static GhLogging()
        {
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static async Task<(T, TimeSpan)> LogAsyncTask<T>(Func<Task<T>> asyncCall, string taskDescription)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = await asyncCall();

            var elapsed = sw.Elapsed;
            LogDuration(taskDescription, elapsed);
            return (result, elapsed);
        }

        public static void LogDuration(string task, TimeSpan duration)
        {
            Logger.Information("{task} took {duration}ms", task, duration.TotalMilliseconds.ToString("#"));
        }
    }
}