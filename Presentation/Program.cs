using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using var scope  = host.Services.CreateScope();
            var       logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                //var context = scope.ServiceProvider.GetService<IUrlShortenerContext>();
                
                
                logger.LogDebug("Attempting database migration");

                //await concreteCreate.Database.MigrateAsync();

                // Initialise
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to migrate the database");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                 {
                     var hostEnvironment = hostingContext.HostingEnvironment;
                     configuration.SetBasePath(hostEnvironment.ContentRootPath);
                     configuration.AddJsonFile("appsettings.json", false, true)
                                  .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true);
                     configuration.AddEnvironmentVariables();
                 })
                .UseSerilog((hostingContext, loggerBuilder) => loggerBuilder.ReadFrom.Configuration(hostingContext.Configuration));
    }
}