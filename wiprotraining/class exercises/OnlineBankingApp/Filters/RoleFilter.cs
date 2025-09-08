using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RoleFilter : IActionFilter
{
    private readonly string _role;

    public RoleFilter(string role)
    {
        _role = role;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var userRole = context.HttpContext.Session.GetString("UserRole");
        if (userRole != _role)
        {
            context.Result = new ContentResult { Content = "Access Denied" };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
