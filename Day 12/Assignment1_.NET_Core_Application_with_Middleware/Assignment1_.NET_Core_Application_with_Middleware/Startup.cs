using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Assignment1_.NET_Core_Application_with_Middleware
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // No services needed for now
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enforce HTTPS
            app.UseHttpsRedirection();

            // Middleware 1: Log Requests & Responses
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");
                await next();
                Console.WriteLine($"[Response] {context.Response.StatusCode}");
            });

            // Middleware 2: Global Exception Handler
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"[Exception] {ex.Message}");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected error occurred. Please try again.");
                }
            });

            // Middleware 3: Content Security Policy Header
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'");
                    return Task.CompletedTask;
                });

                await next();
            });

            // Middleware 4: Serve static files
            app.UseStaticFiles();
        }
    }
}
