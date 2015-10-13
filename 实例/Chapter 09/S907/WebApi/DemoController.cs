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
        public void DemoAction(string str, Contact contact,
            HttpRequestMessage request, CancellationToken cancellationToken,
            HttpContent content)
        {}
    }
}
