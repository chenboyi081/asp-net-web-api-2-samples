using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi
{
    public class DemoController : ApiController
    {
        public void DemoAction(
            [FromBody]string foo,
            [FromBody]string bar,
            [FromBody]string baz)
        { }
    }
}
