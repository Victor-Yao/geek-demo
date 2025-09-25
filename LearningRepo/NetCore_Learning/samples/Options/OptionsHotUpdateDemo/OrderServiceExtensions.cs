using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
            }).Services.AddSingleton<IValidateOptions<OrderServiceOptions>, OrderServiceValidateOptions>();

            // services.PostConfigure<OrderServiceOptions>(options =>
            // {
            //     options.MaxOrderCount += 100;
            // });

            services.AddSingleton<IOrderService, OrderService>();
            return services;
        }
    }
}