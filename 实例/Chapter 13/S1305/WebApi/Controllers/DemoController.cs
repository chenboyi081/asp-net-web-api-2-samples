using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public IEnumerable<string> GetUsers()
        {
            yield return HttpContext.Current.User.Identity.Name;
            yield return Thread.CurrentPrincipal.Identity.Name;
            yield return this.User.Identity.Name;
        }
    }
}
