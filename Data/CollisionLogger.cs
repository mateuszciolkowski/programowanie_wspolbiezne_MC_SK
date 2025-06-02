using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public static class CollisionLogger
    {
        private static readonly string logFilePath = Path.Combine(GetProjectDataPath(), "collision_log.txt");

        private static readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
        private static readonly AutoResetEvent logEvent = new AutoResetEvent(false);

        private static bool isRunning = true;
        private static StreamWriter? streamWriter;

        static CollisionLogger()
        {
            streamWriter = new StreamWriter(logFilePath, append: true)
            {
                AutoFlush = false 
            };

            Task.Run(() => ProcessLogQueue());
        }

        public static void Log(string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            logQueue.Enqueue(logEntry);
            logEvent.Set();
        }

        private static void ProcessLogQueue()
        {
            var buffer = new List<string>();

            while (isRunning)
            {
                logEvent.WaitOne(1000);

                while (logQueue.TryDequeue(out string? logEntry))
                {
                    buffer.Add(logEntry);

                    if (buffer.Count >= 100)
                    {
                        FlushBuffer(buffer);
                    }
                }

                if (buffer.Count > 0)
                {
                    FlushBuffer(buffer);
                }
            }

            if (buffer.Count > 0)
            {
                FlushBuffer(buffer);
            }

            streamWriter?.Flush();
            streamWriter?.Close();
            streamWriter = null;
        }

        private static void FlushBuffer(List<string> buffer)
        {
            foreach (var logEntry in buffer)
            {
                streamWriter?.WriteLine(logEntry);
            }
            streamWriter?.Flush();
            buffer.Clear();
        }

        public static void Stop()
        {
            isRunning = false;
            logEvent.Set();
        }

        private static string GetProjectDataPath()
        {
            string? baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string? projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\.."));
            string dataPath = Path.Combine(projectDir, "Data");

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }

            return dataPath;
        }
    }
}
