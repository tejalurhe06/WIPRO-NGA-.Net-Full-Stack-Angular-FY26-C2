using System;

namespace Design_Patterns.SingletonPattern
{
    public class SingletonDemo
    {
        public static void Run()
        {
            Console.WriteLine("Singleton Pattern Demo:");
            Logger logger1 = Logger.Instance;
            Logger logger2 = Logger.Instance;

            logger1.Log("This is the first log message.");
            logger2.Log("This is the second log message.");

            Console.WriteLine($"Logger1 and Logger2 refer to the same instance: {ReferenceEquals(logger1, logger2)}");
            Console.WriteLine();
        }
    }
}