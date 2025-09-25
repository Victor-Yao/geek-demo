using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace RoutingDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">must can be converted to long</param>
        /// <returns></returns>
        // [HttpGet("{id:MyRouteConstraint}")]
        [HttpGet("{id:isLong}")]
        public bool OrderExist([FromRoute] string id)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">maximum 20</param>
        /// <param name="linkGenerator"></param>
        /// <returns></returns>
        [HttpGet("{id:max(20)}")]
        public bool Max([FromRoute] long id, [FromServices] LinkGenerator linkGenerator)
        {
            var a = linkGenerator.GetPathByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" });

            var uri = linkGenerator.GetUriByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" });

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">required</param>
        /// <returns></returns>
        [HttpGet("{name:required}")]
        [Obsolete]
        public bool Reque(string name)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">limit 3 numbers</param>
        /// <returns></returns>
        [HttpGet("{number:regex(^\\d{{3}}$)}")]
        public bool Number(string number)
        {
            return true;
        }
    }
}
