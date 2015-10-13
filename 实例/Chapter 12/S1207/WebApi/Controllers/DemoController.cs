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
        [Cache(AbsoluteExpiration = "00:00:02")]
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }

}
