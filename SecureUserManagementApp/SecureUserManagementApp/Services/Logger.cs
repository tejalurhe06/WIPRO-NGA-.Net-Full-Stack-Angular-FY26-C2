using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUserManagementApp.Services
{
    public class Logger
    {
        private static readonly string logPath = "log.txt";

        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        private static void Log(string level, string message)
        {
            string entry = $"[{DateTime.Now}] [{level}] {message}";
            File.AppendAllText(logPath, entry + Environment.NewLine);
        }
    }
}
