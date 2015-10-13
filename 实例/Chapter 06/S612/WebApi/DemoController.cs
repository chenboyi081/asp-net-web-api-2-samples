using System;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        [Route("api/demo/abc")]
        public void Action1(string x) { }

        [Route("api/demo/{x:int}")]
        public void Action2(DateTime x) { }

        [Route("api/demo/{x}")]
        public void Action3(string x) { }

        [Route("api/demo/{*x:datetime}")]
        public void Action4(int x) { }

        [Route("api/demo/{*x}")]
        public void Action5() { }
    }
}