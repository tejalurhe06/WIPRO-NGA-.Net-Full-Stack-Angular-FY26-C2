using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

public class ErrorHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Log error
        Debug.WriteLine($"[{DateTime.Now}] Exception: {context.Exception.Message}");

        // Show friendly page
        context.Result = new ViewResult
        {
            ViewName = "Error"
        };
        context.ExceptionHandled = true;
    }
}
