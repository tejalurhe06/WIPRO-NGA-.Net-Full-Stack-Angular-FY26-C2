using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System;

namespace AdvancedRoutingDemo.Constraints
{
    public class GuidRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var value))
                return false;

            // Check if the value is a valid GUID
            return Guid.TryParse(Convert.ToString(value), out _);
        }
    }
}
