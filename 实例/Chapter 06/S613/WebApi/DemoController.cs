using System;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class Demo1Controller : ApiController
    {
        [Route("api/demo/action1", Name = "route1")]
        public void Action1(string x) { }

        [Route("api/demo/action2", Name = "route2")]
        public void Action2(DateTime x) { }
    }
}