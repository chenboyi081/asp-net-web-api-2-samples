using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://127.0.0.1:3721");
            using (HttpSelfHostServer httpServer = new HttpSelfHostServer(new HttpSelfHostConfiguration(baseAddress)))
            {
                httpServer.Configuration.Services.Replace(typeof(IAssembliesResolver),new ExtendedDefaultAssembliesResolver());
                httpServer.Configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                httpServer.OpenAsync().Wait();
                Console.Read();
            }

        }
    }
}
