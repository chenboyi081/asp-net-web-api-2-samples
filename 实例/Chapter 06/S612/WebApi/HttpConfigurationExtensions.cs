using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApi
{
    public static class HttpConfigurationExtensions
    {
        public static IDictionary<string, IHttpRoute> GetRouteMapping(this HttpConfiguration configuration)
        {
            FieldInfo dictionary = typeof(HttpRouteCollection).GetField("_dictionary", BindingFlags.Instance | BindingFlags.NonPublic);
            return (IDictionary<string, IHttpRoute>)dictionary.GetValue(configuration.Routes);
        }

        public static IEnumerable<IHttpRoute> GetSubRoutes(this HttpConfiguration configuration)
        {
            IHttpRoute route = configuration.Routes["MS_attributerouteWebApi"];
            PropertyInfo subRoutesProperty = route.GetType().GetProperty("SubRoutes");
            return (IEnumerable<IHttpRoute>)subRoutesProperty.GetValue(route);
        }

        public static IDictionary<string, IHttpRoute> GetSubRouteMapping(this HttpConfiguration configuration)
        {
            var route = configuration.GetSubRoutes();
            FieldInfo dictionary = route.GetType().GetField("_dictionary", BindingFlags.Instance | BindingFlags.NonPublic);
            return (IDictionary<string, IHttpRoute>)dictionary.GetValue(route);
        }
    }
}
