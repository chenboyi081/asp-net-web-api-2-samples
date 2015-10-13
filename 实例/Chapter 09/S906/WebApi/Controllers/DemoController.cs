using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public string Get(HttpRequestMessage request)
        {
            return string.Format("object.ReferenceEquals(request, this.Request) = {0}", object.ReferenceEquals(request, this.Request));
        }
    }
}