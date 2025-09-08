using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

public class LoggingFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.Session.GetString("UserRole") ?? "Anonymous";
        var action = context.ActionDescriptor.DisplayName;
        Debug.WriteLine($"[{DateTime.Now}] User: {user} executing {action}");
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
