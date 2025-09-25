using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace LoggingSimpleDemo
{
    public class OrderService
    {
        ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }

        public void Show()
        {
            //concatenate string in the Method 
            _logger.LogInformation("Show Time: {time}", DateTime.Now);

            //concatenate string when deliver parameters.
            // _logger.LogInformation($"Show Time{DateTime.Now}");
        }
    }
}