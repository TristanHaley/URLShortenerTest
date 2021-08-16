using System;
using System.IO;
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
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                            webBuilder.UseStartup<Startup>();
                            webBuilder.UseIISIntegration();

                            // webBuilder.UseUrls("https://localhost:5001");
                        })
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
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to migrate the database");
            }

            await host.RunAsync();
        }
    }
}