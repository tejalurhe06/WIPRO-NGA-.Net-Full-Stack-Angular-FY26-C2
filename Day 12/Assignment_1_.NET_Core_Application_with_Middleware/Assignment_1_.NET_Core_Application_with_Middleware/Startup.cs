namespace Assignment_1_.NET_Core_Application_with_Middleware
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // No services needed for this simple demo
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Force HTTPS redirection
            app.UseHttpsRedirection();

            // Custom Middleware 1: Request & Response Logging
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");
                await next();
                Console.WriteLine($"[Response] {context.Response.StatusCode}");
            });

            // Custom Middleware 2: Global Exception Handling
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"[Error] {ex.Message}");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("<h1>Something went wrong. Please try again later.</h1>");
                }
            });

            // Static File Middleware with Security Headers
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
                    return Task.CompletedTask;
                });

                await next();
            });

            // Enable serving static files from wwwroot
            app.UseStaticFiles();
        }
    }
}
