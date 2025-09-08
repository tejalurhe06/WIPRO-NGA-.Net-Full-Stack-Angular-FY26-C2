using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Ecommerce_Website.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetString("username") ?? "Guest";
            Debug.WriteLine($"[LOG] User: {user} is accessing {context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // After action executes
        }
    }
}
