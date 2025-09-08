using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILoggingService _logger;

    public GlobalExceptionFilter(ILoggingService logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.Log($"Exception: {context.Exception.Message}");
        context.Result = new ViewResult
        {
            ViewName = "Error"
        };
        context.ExceptionHandled = true;
    }
}
