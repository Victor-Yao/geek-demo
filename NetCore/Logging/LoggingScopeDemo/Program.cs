using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LoggingScopeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run LoggingScopeDemo");

            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddCommandLine(args);
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = configBuilder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            /// 用工厂模式将配置对象注册到容器中，又容器来管理他的生命周期
            serviceCollection.AddSingleton(p => config);
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });
            IServiceProvider service = serviceCollection.BuildServiceProvider();

            var logger = service.GetService<ILogger<Program>>();

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                using (logger.BeginScope("ScopeId:{scopeId}", Guid.NewGuid()))
                {
                    logger.LogInformation("\"logger.LogInformation\"");
                    logger.LogError("\"logger.LogError\"");
                    logger.LogTrace("\"logger.LogError\"");
                }
                System.Threading.Thread.Sleep(100);
                Console.WriteLine("========分割线=======");
            }

            Console.ReadKey();
        }
    }
}
