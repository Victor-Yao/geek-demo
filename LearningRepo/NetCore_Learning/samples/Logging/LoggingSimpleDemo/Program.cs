using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LoggingSimpleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("logging simple demo runs!");

            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = configBuilder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(p => config);

            serviceCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
            });

            serviceCollection.AddTransient<OrderService>();
            IServiceProvider service = serviceCollection.BuildServiceProvider();

            /// *1.Use Factory to create a logger.* 
            // ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();
            // ILogger alogger = loggerFactory.CreateLogger("alogger");

            // alogger.LogDebug(2001, "\"LogDebug\" of alogger");
            // alogger.LogInformation("\"LogInformation\" of alogger");

            // alogger.LogError(new Exception("new my Except"), "\"LogError\" of alogger");

            // var alogger2 = loggerFactory.CreateLogger("alogger");
            // alogger2.LogDebug("\"LogDebug\" from alogger2");

            /// ***2.Register customized logger***
            // var order = service.GetService<OrderService>();
            // order.Show();

            /// The logger of LoggingSimpleDemo.Program
            var logger = service.GetService<ILogger<Program>>();
            Console.WriteLine(logger.GetType().ToString());
            logger.LogInformation(new EventId(201, "weyao-event"), "weyao hello world");



            Console.ReadKey();
        }
    }
}
