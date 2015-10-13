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

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor(new HttpConfiguration(), "demo", typeof(DemoController));
            MethodInfo methodInfo = typeof(DemoController).GetMethod("Get");
            ReflectedHttpActionDescriptor actionDescriptor = new ReflectedHttpActionDescriptor(controllerDescriptor, methodInfo);

            Console.WriteLine("{0,-16}{1,-16}{2,-16}{3,-10}", "ParameterName", "ParameterType", "DefaultValue", "IsOptional");
            foreach (ReflectedHttpParameterDescriptor parameter in actionDescriptor.GetParameters())
            {
                Console.WriteLine("{0,-16}{1,-16}{2,-16}{3,-10}", parameter.ParameterName, parameter.ParameterType.Name, parameter.DefaultValue ?? "N/A", parameter.IsOptional);
            }
        }
    }
}
