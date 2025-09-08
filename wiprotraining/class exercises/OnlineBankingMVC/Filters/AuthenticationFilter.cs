using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthenticationFilter : IActionFilter
{
    private readonly IAuthService _auth;

    public AuthenticationFilter(IAuthService auth)
    {
        _auth = auth;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!_auth.IsLoggedIn())
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
