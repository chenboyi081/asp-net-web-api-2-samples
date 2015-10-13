using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Validate]
    public class DemoController : ApiController
    {
        public void Get([FromUri]Person person) { }
    }
}