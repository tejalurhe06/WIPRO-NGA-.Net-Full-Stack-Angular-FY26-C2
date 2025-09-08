using System;

public class LoggingService : ILoggingService
{
    public void Log(string userId, string action)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: User {userId} performed action: {action}");
    }
}
