using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;

namespace MvcApp
{
    public class HttpRouteDataTraceHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IHttpRouteData routeData = request.GetRouteData();
            foreach (var item in routeData.Values)
            {
                HttpContext.Current.Response.Write(string.Format("{0}: {1}<br/>", item.Key, item.Value));
            }
            return Task.FromResult<HttpResponseMessage>(null);
        }
    }
}