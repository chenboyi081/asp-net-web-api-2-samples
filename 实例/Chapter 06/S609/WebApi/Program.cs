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

            Console.WriteLine("{0,-26}{1}", "Name", "Type");
            foreach (var item in configuration.GetRouteMapping())
            {
                Console.WriteLine("{0,-26}{1}", item.Key, item.Value.GetType().Name);
            }
        }
    }
}
