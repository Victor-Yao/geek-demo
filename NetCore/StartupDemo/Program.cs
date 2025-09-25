using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpsPolicy;

namespace StartupDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configurationBuilder =>
                {
                    Console.WriteLine("ConfigureHostConfiguration");
                })
                .ConfigureServices(service=>{
                    Console.WriteLine("ConfigureServices");
                })
                .ConfigureLogging(loggingBuilder => {
                    Console.WriteLine("ConfigureLogging");
                })
                .ConfigureAppConfiguration((hostBuilderContext,configurationBinder)=>{
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");
                    // Startup is not necessary for asp.net core. 
                    // It's design for wrap ConfigureService and Configure to make program.cs is simple and short
                    // webBuilder.UseStartup<Startup>();
                    
                    webBuilder.ConfigureServices(services => {
                        Console.WriteLine("Startup.ConfigureServices");
                        services.AddControllers();
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "StartupDemo", Version = "v1" });
                        });
                    });

                    webBuilder.Configure(app=>{
                        Console.WriteLine("Startup.Configure");

                        app.UseHttpsRedirection();

                        app.UseRouting();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
