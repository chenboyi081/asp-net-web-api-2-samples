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
            Console.WriteLine("{0,-12}{1,-10}", "ActionName", "HttpMethod");
            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor(new HttpConfiguration(), "demo", typeof(DemoController));
            foreach (MethodInfo methodInfo in typeof(DemoController).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                ReflectedHttpActionDescriptor actionDescriptor = new ReflectedHttpActionDescriptor(controllerDescriptor, methodInfo);
                foreach (HttpMethod httpMethod in actionDescriptor.SupportedHttpMethods)
                {
                    Console.WriteLine("{0,-12}{1,-10}", actionDescriptor.ActionName, httpMethod);
                }
            }
        }
    }
}
