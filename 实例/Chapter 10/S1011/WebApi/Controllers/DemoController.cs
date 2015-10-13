using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Validate]
    public class DemoController : ApiController
    {
        public void Get([FromUri] DemoModel model) { }
    }
}
