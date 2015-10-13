using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();
            configuration.EnsureInitialized();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://www.artech.com/api/movies/123");
            IHttpRouteData routeData = configuration.Routes.GetRouteData(request);
            Console.WriteLine("{0,-8}{1}", "Name", "Value");
            foreach (IHttpRouteData subRouteData in routeData.Values["MS_SubRoutes"] as IEnumerable<IHttpRouteData>)
            {
                foreach (var item in subRouteData.Values)
                {
                    Console.WriteLine("{0,-8}{1}", item.Key, item.Value);
                }
            }
        }
    }
}