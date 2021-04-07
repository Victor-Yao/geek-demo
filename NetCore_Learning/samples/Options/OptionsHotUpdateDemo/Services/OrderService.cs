using System;
using Microsoft.Extensions.Options;

namespace OptionsHotUpdateDemo.Services
{
    public interface IOrderService
    {
        int ShowMaxOrderCount();
    }

    public class OrderService : IOrderService
    {
        // IOptionsSnapshot<OrderServiceOptions> _options;
        IOptionsMonitor<OrderServiceOptions> _options;

        public OrderService(IOptionsMonitor<OrderServiceOptions> options)
        {
            this._options = options;
            _options.OnChange(options =>
            {
                Console.WriteLine($"value changed: {options.MaxOrderCount}");
            });
        }

        public int ShowMaxOrderCount()
        {
            return _options.CurrentValue.MaxOrderCount;
        }
    }

    public class OrderServiceOptions
    {
        public int MaxOrderCount { get; set; } = 100;
    }
}