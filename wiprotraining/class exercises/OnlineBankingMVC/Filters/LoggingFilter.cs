using Microsoft.AspNetCore.Mvc.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILoggingService _logger;
    private readonly IAuthService _auth;

    public LoggingFilter(ILoggingService logger, IAuthService auth)
    {
        _logger = logger;
        _auth = auth;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        string userId = _auth.GetCurrentUserId() ?? "Guest";
        string action = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        _logger.Log(userId, action);
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
