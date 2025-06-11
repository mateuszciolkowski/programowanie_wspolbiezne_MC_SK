using System;
using System.Data;
using System.IO;

namespace Data
{
    public static class CollisionLogger
    {
        private static readonly string logFilePath;
        private static readonly object fileLock = new object();
        private static StreamWriter? streamWriter;

        static CollisionLogger()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string fileName = $"collision_log_{timestamp}.txt";
            logFilePath = Path.Combine(GetProjectDataPath(), fileName);

            streamWriter = new StreamWriter(logFilePath, append: false)
            {
                AutoFlush = true
            };
        }

        public static void Log(string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            lock (fileLock)
            {
                streamWriter?.WriteLine(logEntry);
            }
        }

        public static void LogCollision(double X, double Y,double Radius,double VelocityX,
                                        double VelocityY, double width, double height)
        {
            double radius = Radius / 2;
            string wall;

            if (X - radius <= 0)
                wall = "ściana lewa";
            else if (X + radius >= width)
                wall = "ściana prawa";
            else if (Y - radius <= 0)
                wall = "ściana górna";
            else if (Y + radius >= height)
                wall = "ściana dolna";
            else
                wall = "nieznana";

            string message = $"Kolizja kula–{wall}: Ball (X={X:F2}, Y={Y:F2}, Vx={VelocityX:F2}, Vy={VelocityY:F2})";
            Log(message);
        }


        public static void LogCollision(double X1,double Y1, double VelocityX1, double VelocityY1,
                                         double X2, double Y2, double VelocityX2, double VelocityY2)
        {
            string message = $"Kolizja kula–kula: " +
                             $"Ball1 (X={X1:F2}, Y={Y1:F2}, Vx={VelocityX1:F2}, Vy={VelocityY1:F2}) vs " +
                             $"Ball2 (X={X2:F2}, Y={Y2:F2}, Vx={VelocityX2:F2}, Vy={VelocityY2:F2})";
            Log(message);
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

        public static void Stop()
        {
            lock (fileLock)
            {
                streamWriter?.Flush();
                streamWriter?.Close();
                streamWriter = null;
            }
        }
    }
}
