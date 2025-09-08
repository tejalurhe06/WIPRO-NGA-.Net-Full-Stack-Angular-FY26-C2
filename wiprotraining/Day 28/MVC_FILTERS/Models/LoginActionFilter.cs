using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace mvc_filters
{
    public class LogActionFilter : IActionFilter
    {
         private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("[Log] Executing: {Action}", context.ActionDescriptor.DisplayName);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("[Log] Executed: {Action}", context.ActionDescriptor.DisplayName);
        }
    }

}