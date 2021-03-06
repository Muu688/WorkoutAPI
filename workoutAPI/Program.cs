using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutAPI.Data;
using WorkoutAPI.Models;

namespace WorkoutAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<WorkoutContext>();
                    //context.Database.EnsureCreated();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static bool IsDevelopment =>
       Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        public static string HostPort =>
       IsDevelopment
           ? "5000"
           : Environment.GetEnvironmentVariable("PORT");

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults((webBuilder) =>
            {
                webBuilder.UseUrls($"http://+:{HostPort}");
                //webBuilder.UseSerilog((context, config) =>
                //{
                //    config.ReadFrom.Configuration(context.Configuration);
                //});
                webBuilder.UseStartup<Startup>();
            });
    }
}
