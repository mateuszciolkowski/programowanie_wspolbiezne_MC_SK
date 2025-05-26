using System;
using System.IO;

namespace Data
{
    public static class CollisionLogger
    {
        // Ścieżka: folder projektu -> Data -> collision_log.txt
        private static readonly string logFilePath = Path.Combine(GetProjectDataPath(), "collision_log.txt");

        private static readonly object fileLock = new object();

        public static void Log(string message)
        {
            try
            {
                lock (fileLock)
                {
                    File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd przy zapisie logu: " + ex.Message);
            }
        }

        private static string GetProjectDataPath()
        {
            // Cofamy się z bin/Debug/... do katalogu głównego projektu
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
