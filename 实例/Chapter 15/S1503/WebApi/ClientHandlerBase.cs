using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    public class ClientHandlerBase : DelegatingHandler
    {
        protected virtual void BeforeSendRequest(HttpRequestMessage request)
        {
            Console.WriteLine(string.Format("{0}.BeforeSendRequest()", this.GetType().Name));
        }

        protected virtual void AfterReceiveResponse(HttpResponseMessage response)
        {
            Console.WriteLine(string.Format("{0}.AfterReceiveResponse()", this.GetType().Name));
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.BeforeSendRequest(request);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            this.AfterReceiveResponse(response);
            return response;
        }
    }

    public class FooHandler : ClientHandlerBase
    { }

    public class BarHandler : ClientHandlerBase
    { }

    public class BazHandler : ClientHandlerBase
    { }
}
