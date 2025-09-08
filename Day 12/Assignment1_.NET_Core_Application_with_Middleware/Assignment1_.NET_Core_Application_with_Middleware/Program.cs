using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace Assignment1_.NET_Core_Application_with_Middleware
{
        public class Program
        {
            public static void Main(string[] args)
            {
                CreateHostBuilder(args).Build().Run();
            }

            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        // This will now look into Startup.cs for configuration
                        webBuilder.UseStartup<Startup>();
                    });
        }
}
