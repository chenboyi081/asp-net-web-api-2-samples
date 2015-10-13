using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public IEnumerable<string> Get()
        {
            MyHttpServer httpServer = new MyHttpServer();

            //Thread.CurrentPrincipal == Null
            Thread.CurrentPrincipal = null;
            HttpRequestMessage request = new HttpRequestMessage();
            httpServer.SendAsync(request, new CancellationToken(false));
            GenericPrincipal principal = (GenericPrincipal)Thread.CurrentPrincipal;
            string identity1 = string.IsNullOrEmpty(principal.Identity.Name) ? "N/A" : principal.Identity.Name;

            //Thread.CurrentPrincipal ！= Null            
            GenericIdentity identity = new GenericIdentity("Artech");
            Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[0]);
            request = new HttpRequestMessage();
            httpServer.SendAsync(request, new CancellationToken(false));
            principal = (GenericPrincipal)Thread.CurrentPrincipal;
            string identity2 = string.IsNullOrEmpty(principal.Identity.Name) ? "N/A" : principal.Identity.Name;

            return new string[] { identity1, identity2 };
        }
    }
}
