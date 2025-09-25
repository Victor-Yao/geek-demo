using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace RoutingDemo.Constraints
{
    public class MyRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (RouteDirection.IncomingRequest == routeDirection)
            {
                var v = values[routeKey];
                if (long.TryParse(v.ToString(), out var value))
                {
                    return true;
                }
            }

            return false;
        }
    }

}