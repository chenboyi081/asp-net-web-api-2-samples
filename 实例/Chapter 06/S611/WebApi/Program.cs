using System;
using System.Collections.Generic;
using System.Linq;
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

            IHttpRoute route = configuration.Routes["MS_attributerouteWebApi"];
            PropertyInfo subRoutesProperty = route.GetType().GetProperty("SubRoutes");
            IEnumerable<IHttpRoute> subRoutes = (IEnumerable<IHttpRoute>)subRoutesProperty.GetValue(route);
            IHttpRoute subRoute = subRoutes.First();

            Console.WriteLine("Defaults:");
            foreach (var item in subRoute.Defaults)
            {
                Console.WriteLine("{0,-12}{1}", item.Key, item.Value);
            }

            Console.WriteLine("\nDataTokens:");
            foreach (var item in subRoute.DataTokens)
            {
                Console.WriteLine("{0,-12}{1}", item.Key, item.Value);
            }

            Console.WriteLine("\nConstraints:");
            foreach (var item in subRoute.Constraints)
            {
                Console.WriteLine("{0,-12}{1}", item.Key, item.Value.GetType().Name);
            }
        }
    }
}
