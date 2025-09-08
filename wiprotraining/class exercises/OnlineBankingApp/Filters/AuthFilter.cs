using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthFilter : IActionFilter
{
    private readonly IUserService _userService;

    public AuthFilter(IUserService userService)
    {
        _userService = userService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Login", "Home", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
