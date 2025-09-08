using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace EcommerceRoutingDemo.Constraints
{
    public class PriceRangeConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var value) || value == null)
                return false;

            // Accept format like "100-500"
            string pattern = @"^\d+-\d+$";
            return Regex.IsMatch(Convert.ToString(value)!, pattern);
        }
    }
}
