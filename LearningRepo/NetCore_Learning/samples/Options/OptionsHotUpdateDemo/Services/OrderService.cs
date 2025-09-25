using System;
using System.ComponentModel.DataAnnotations;
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
        [Range(1, 20)]
        public int MaxOrderCount { get; set; } = 100;
    }

    public class OrderServiceValidateOptions : IValidateOptions<OrderServiceOptions>
    {
        public ValidateOptionsResult Validate(string name, OrderServiceOptions options)
        {
            if (options.MaxOrderCount > 100)
            {
                return ValidateOptionsResult.Fail("cannot greater than 100");
            }
            else
            {
                return ValidateOptionsResult.Success;
            }
        }
    }
}