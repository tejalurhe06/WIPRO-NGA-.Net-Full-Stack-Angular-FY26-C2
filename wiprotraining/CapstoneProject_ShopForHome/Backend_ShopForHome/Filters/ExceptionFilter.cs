// ExceptionFilter.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopForHome.API.Helpers;
using ShopForHome.API.Exceptions;

namespace ShopForHome.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(IWebHostEnvironment environment, ILogger<ExceptionFilter> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred");

            var response = new ApiResponse<object>(500, "An unexpected error occurred");

            if (context.Exception is AppException appException)
            {
                context.HttpContext.Response.StatusCode = appException.StatusCode;
                response = new ApiResponse<object>(appException.StatusCode, appException.Message)
                {
                    ErrorCode = appException.ErrorCode
                };
            }
            else if (context.Exception is NotFoundException)
            {
                context.HttpContext.Response.StatusCode = 404;
                response = new ApiResponse<object>(404, context.Exception.Message);
            }
            else if (context.Exception is UnauthorizedException)
            {
                context.HttpContext.Response.StatusCode = 401;
                response = new ApiResponse<object>(401, context.Exception.Message);
            }
            else if (context.Exception is ForbiddenException)
            {
                context.HttpContext.Response.StatusCode = 403;
                response = new ApiResponse<object>(403, context.Exception.Message);
            }

            if (_environment.IsDevelopment())
            {
                response.Exception = context.Exception.Message;
                response.StackTrace = context.Exception.StackTrace;
            }

            context.Result = new ObjectResult(response);
            context.ExceptionHandled = true;
        }
    }
}