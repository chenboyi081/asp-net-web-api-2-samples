using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration("http://localhost:3721");
            configuration.MapHttpAttributeRoutes();
            HttpSelfHostServer server = new HttpSelfHostServer(configuration);
            server.OpenAsync().Wait();
            Console.Read();
        }
    }
}
