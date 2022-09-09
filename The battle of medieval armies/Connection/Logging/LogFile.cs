using System;
using System.IO;

namespace Dapper_BDSQL.Controller
{
    public enum LogLevel
    {
        Information,
        Warning,
        Error
    }
    public static class LogFile
    {
        private static string Path { get; set; }
        static LogFile() => Path = "log.log";
        static bool Start { get; set; } = true;

        public static void SetPath(string pathToLogFile) => Path = pathToLogFile;
        public static void Log(string? msg, LogLevel logLevel = LogLevel.Error)
        {
            if (Start)
                File.AppendAllText(Path,
                    $"{DateTime.Now.ToString()}:{DateTime.Now.Millisecond}|" +
                    $" {logLevel} |" +
                    $" {msg}\n");
        }

        public static void StopLogging()
        {
            Start = false;
        }

        public static void StartLogging()
        {
            Start = true;
        }
    }
}
