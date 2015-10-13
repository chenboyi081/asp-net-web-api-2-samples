using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost.Channels;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyHttpSelfHostServer httpServer = new MyHttpSelfHostServer(new HttpConfiguration(), new Uri("http://127.0.0.1:3721")))
            {
                httpServer.Configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                httpServer.Open();
                Console.Read();
            }
        }
    }
}
