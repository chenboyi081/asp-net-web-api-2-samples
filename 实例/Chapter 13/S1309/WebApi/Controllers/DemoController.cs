using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    [Authenticate("https://www.artech.com/webapi/account/capturetoken")]
    public class DemoController : ApiController
    {
        public HttpResponseMessage GetProfile()
        {
            string accessToken;
            if (this.Request.TryGetAccessToken(out accessToken))
            {
                using (HttpClient client = new HttpClient())
                {
                    string address = string.Format("https://apis.live.net/v5.0/me?access_token={0}", accessToken);
                    return client.GetAsync(address).Result;
                }
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "No access token" };
        }
    }
}
