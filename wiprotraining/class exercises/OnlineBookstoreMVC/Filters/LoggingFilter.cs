using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace OnlineBookstoreMVC.Filters
{
    public class LoggingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Runs before action executes
            Console.WriteLine($"[LOG] Executing action: {context.ActionDescriptor.DisplayName} at {DateTime.Now}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Runs after action executes
            if(context.Exception != null)
            {
                Console.WriteLine($"[ERROR] Exception in action: {context.ActionDescriptor.DisplayName} - {context.Exception.Message}");
            }
            else
            {
                Console.WriteLine($"[LOG] Action executed successfully: {context.ActionDescriptor.DisplayName} at {DateTime.Now}");
            }
        }
    }
}
