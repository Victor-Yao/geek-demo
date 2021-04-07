using System;
using Microsoft.Extensions.Configuration;
using OptionsHotUpdateDemo.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddMyOrderService(this IServiceCollection services, IConfiguration configuration)
        {
            // services.Configure<OrderServiceOptions>(configuration.GetSection("OrderService"));

            services.AddOptions<OrderServiceOptions>().Configure(options =>
            {
                configuration.GetSection("OrderService").Bind(options);
            }).ValidateDataAnnotations();

            // services.PostConfigure<OrderServiceOptions>(options =>
            // {
            //     options.MaxOrderCount += 100;
            // });

            services.AddSingleton<IOrderService, OrderService>();
            return services;
        }
    }
}