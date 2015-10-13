using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        [Route("api/demo/{foo}/{bar}/{baz}")]
        public Tuple<string, int, int?> Get(string foo, int bar, int? baz)
        {
            return new Tuple<string, int, int?>(foo, bar, baz);
        }
    }
}
