using Microsoft.AspNetCore.Mvc.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILoggingService _logger;

    public LoggingFilter(ILoggingService logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.Log($"Executing: {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.Log($"Executed: {context.HttpContext.Response.StatusCode}");
    }
}
