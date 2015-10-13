using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApi.Controllers
{
    [RoutePrefix("api/demo")]
    public class DemoController : ApiController
    {
        [Route("action1")]
        [HttpGet]
        public IHttpActionResult Action1()
        {
            Console.WriteLine("Action1");
            return new RedirectResult(new Uri("http://localhost:3721/api/demo/action2"), this);
        }

        [Route("action2")]
        [HttpGet]
        public IHttpActionResult Action2()
        {
            Console.WriteLine("Action2");
            return new RedirectResult(new Uri("http://localhost:3721/api/demo/action3"), this);
        }

        [Route("action3")]
        [HttpGet]
        public IHttpActionResult Action3()
        {
            return new OkResult(this);
        }
    }
}
