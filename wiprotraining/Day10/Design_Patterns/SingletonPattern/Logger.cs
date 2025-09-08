using System;

namespace Design_Patterns.SingletonPattern
{
    public sealed class Logger
    {
        private static readonly Logger instance = new Logger();

        private Logger()
        {
            Console.WriteLine("Logger Initialized");
        }

        public static Logger Instance
        {
            get { return instance; }
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG]: {message}");
        }
    }
}