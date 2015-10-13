using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public Tuple<IEnumerable<string>, IEnumerable<string>> Get()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.MessageHandlers.Add(new FooHandler());
            configuration.MessageHandlers.Add(new BarHandler());
            configuration.MessageHandlers.Add(new BazHandler());

            MyHttpServer httpServer = new MyHttpServer(configuration);
            IEnumerable<string> chain1 = this.GetHandlerChain(httpServer).ToArray();
            httpServer.Initialize();
            IEnumerable<string> chain2 = this.GetHandlerChain(httpServer).ToArray();
            return new Tuple<IEnumerable<string>, IEnumerable<string>>(chain1, chain2);
        }
        private IEnumerable<string> GetHandlerChain(DelegatingHandler handler)
        {
            yield return handler.GetType().Name;
            while (null != handler.InnerHandler)
            {
                yield return handler.InnerHandler.GetType().Name;
                handler = handler.InnerHandler as DelegatingHandler;
                if (null == handler)
                {
                    break;
                }
            }
        }
    }
}
