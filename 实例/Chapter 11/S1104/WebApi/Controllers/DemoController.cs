using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public IEnumerable<string> Get()
        {
            HttpRequestMessage request1 = this.CreateRequestMessage(null, "application/json", "utf-7;q=0.4, utf-8;q=0.8, utf-16;q=1.0");
            HttpRequestMessage request2 = this.CreateRequestMessage(null, "application/json", "utf-7, utf-8, utf-16");
            HttpRequestMessage request3 = this.CreateRequestMessage("utf-8", "application/json", null);
            HttpRequestMessage request4 = this.CreateRequestMessage(null, "application/json", null);

            HttpRequestMessage[] requests = new HttpRequestMessage[] { request1, request2, request3, request4 };
            IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { new JsonFormatter() };
            return from request in requests
                   select negotiator.Negotiate(typeof(string), request, formatters).MediaType.CharSet;
        }
        private HttpRequestMessage CreateRequestMessage(string charset, string accept, string acceptCharsets)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            if (!string.IsNullOrEmpty(charset))
            {
                request.Content = new ObjectContent(typeof(string), "", new JsonMediaTypeFormatter());
                request.Content.Headers.ContentType.CharSet = charset;
            }
            if (!string.IsNullOrEmpty(accept))
            {
                request.Headers.Add("Accept", accept);
            }
            if (!string.IsNullOrEmpty(acceptCharsets))
            {
                request.Headers.Add("Accept-Charset", acceptCharsets);
            }
            return request;
        }
    }

    public class JsonFormatter : MediaTypeFormatter
    {
        public JsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            this.SupportedEncodings.Add(Encoding.UTF7);
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);
        }
        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}