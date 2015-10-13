using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WebApi
{
    public class CultureRouteConstraint : IHttpRouteConstraint
    {
        private static IList<string> allCultures;
        static CultureRouteConstraint()
        {
            allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).Select(culture => culture.Name).ToList();
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object culture;
            if (values.TryGetValue(parameterName, out culture))
            {
                return allCultures.Any(c => string.Compare(c, culture.ToString(), true) == 0);
            }
            return false;
        }
    }
}