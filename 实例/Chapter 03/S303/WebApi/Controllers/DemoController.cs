using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public async Task<IDictionary<string, object>> Get()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute("default", "wheather/{areaCode}/{days}");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://www.artech.com/wheather/010/2");
            MyHttpRoutingDispatcher dispatcher =
                new MyHttpRoutingDispatcher(configuration);
            await dispatcher.SendAsync(request, CancellationToken.None);
            IHttpRouteData routeData = request.GetRouteData();
            return routeData.Values;
        }
    }
}
