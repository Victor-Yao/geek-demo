using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScopeAndDisposableDemo.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;

namespace ScopeAndDisposableDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // [HttpGet]
        // public int Get(
        //     [FromServices] IOrderService orderService,
        //     [FromServices] IOrderService orderService2)
        // {
        //     #region 
        //     Console.WriteLine("========1========");
        //     using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
        //     {
        //         var service = scope.ServiceProvider.GetService<IOrderService>();
        //         var service2 = scope.ServiceProvider.GetService<IOrderService>();
        //     }
        //     Console.WriteLine("========2========");
        //     #endregion

        //     Console.WriteLine("interface request is finised!");
        //     return 1;
        // }

        [HttpGet]
        public int Get(
            [FromServices] IHostApplicationLifetime hostApplicationLifeTime,
            [FromQuery] bool stop = false)
        {
            #region 
            if (stop)
            {
                hostApplicationLifeTime.StopApplication();
            }
            #endregion

            Console.WriteLine("interface request is finised!");
            return 1;
        }
    }
}
