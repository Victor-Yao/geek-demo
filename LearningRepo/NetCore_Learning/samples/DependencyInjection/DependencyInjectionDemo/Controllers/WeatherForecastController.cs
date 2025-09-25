using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        IOrderService _orderService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var name = _orderService.ToString();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public int GetService([FromServices] IMySingletonService singleton1,
                              [FromServices] IMySingletonService singleton2,
                              [FromServices] IMyTransientService transient1,
                              [FromServices] IMyTransientService transient2,
                              [FromServices] IMyScopedService scoped1,
                              [FromServices] IMyScopedService scoped2)
        {
            Console.WriteLine($"singleton1:{singleton1.GetHashCode()}");
            Console.WriteLine($"singleton2:{singleton2.GetHashCode()}");

            Console.WriteLine($"transient1:{transient1.GetHashCode()}");
            Console.WriteLine($"transient2:{transient2.GetHashCode()}");

            Console.WriteLine($"scoped1:{scoped1.GetHashCode()}");
            Console.WriteLine($"scoped2:{scoped2.GetHashCode()}");

            Console.WriteLine("===========request done===========");
            return 1;
        }

        [HttpGet]
        public int GetServiceList([FromServices] IEnumerable<IOrderService> services)
        {
            foreach (var item in services)
            {
                Console.WriteLine($"Got service: {item.ToString()}, {item.GetHashCode()}");
            }
            return 1;
        }
    }
}
