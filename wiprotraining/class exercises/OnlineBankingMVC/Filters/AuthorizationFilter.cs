using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizationFilter : IActionFilter
{
    private readonly IAuthService _auth;
    private readonly string _role;

    public AuthorizationFilter(IAuthService auth, string role)
    {
        _auth = auth;
        _role = role;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!_auth.HasRole(_role))
        {
            context.Result = new ContentResult
            {
                Content = "Access Denied: You do not have permission to access this page."
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
