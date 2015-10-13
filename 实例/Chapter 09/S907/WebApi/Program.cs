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
            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor(configuration, "demo", typeof(DemoController));
            IHttpActionSelector actionSelector = configuration.Services.GetActionSelector();
            HttpActionDescriptor actionDescriptor = actionSelector.GetActionMapping(controllerDescriptor)["DemoAction"].First();
            IActionValueBinder actionValueBinder = configuration.Services.GetActionValueBinder();
            HttpActionBinding actionBinding = actionValueBinder.GetBinding(actionDescriptor);
            Console.WriteLine("{0,-18}{1}", "Parameter", "HttpParameterBinding");
            foreach (HttpParameterBinding parameterBinding in actionBinding.ParameterBindings)
            {
                Console.WriteLine("{0,-18}{1}", parameterBinding.Descriptor.ParameterName, parameterBinding.GetType().Name);
            }
        }
    }
}
