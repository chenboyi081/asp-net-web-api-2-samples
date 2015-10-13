using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace WebApi
{
    public class MyHttpRoutingDispatcher : HttpRoutingDispatcher
    {
        public MyHttpRoutingDispatcher(HttpConfiguration configuration)
            : base(configuration)
        { }

        public new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}