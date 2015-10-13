using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            IHttpControllerSelector controllerSelector = configuration.Services.GetHttpControllerSelector();
            IDictionary<string, HttpControllerDescriptor> mappings = controllerSelector.GetControllerMapping();

            Console.WriteLine("{0,-16}{1,-10}", "ControllerName", "TypeName");
            foreach (var item in mappings)
            {
                Console.WriteLine("{0,-16}{1,-10}", item.Key, item.Value.ControllerType.Name);
            }
        }
    }
}
