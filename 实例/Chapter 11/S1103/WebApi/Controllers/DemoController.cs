using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public IEnumerable<string> Get()
        {
            HttpRequestMessage request1 = this.CreateRequestMessage(null, null, "x-formatted-by", "baz-formatter");
            HttpRequestMessage request2 = this.CreateRequestMessage(null, "application/foo;q=0.4, application/bar;q=0.6, text/baz;q=0.8", null, null);
            HttpRequestMessage request3 = this.CreateRequestMessage(null, "applicaiton/*, text/baz, */*", null, null);
            HttpRequestMessage request4 = this.CreateRequestMessage(null, "text/*, */*", null, null);
            HttpRequestMessage request5 = this.CreateRequestMessage(null, "*/*", null, null);
            HttpRequestMessage request6 = this.CreateRequestMessage("application/json", "image/jpeg", null, null);
            HttpRequestMessage request7 = this.CreateRequestMessage(null, null, null, null);

            HttpRequestMessage[] requests = new HttpRequestMessage[] { request1, request2, request3, request4, request5, request6, request7 };
            IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { new FooFormatter(), new BarFormatter(), new BazFormatter(), new JsonMediaTypeFormatter() };
            return from request in requests
                   select negotiator.Negotiate(typeof(string), request, formatters).Formatter.GetType().Name;
        }
        private HttpRequestMessage CreateRequestMessage(string requestMediaType, string accept, string headerName, string headerValue)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            if (!string.IsNullOrEmpty(headerName))
            {
                request.Headers.Add(headerName, headerValue);
            }
            if (!string.IsNullOrEmpty(requestMediaType))
            {
                request.Content = new ObjectContent(typeof(string), "", new JsonMediaTypeFormatter());
            }
            if (!string.IsNullOrEmpty(accept))
            {
                request.Headers.Add("Accept", accept);
            }
            return request;
        }
    }

    public class FormatterBase : MediaTypeFormatter
    {
        public FormatterBase(string mediaType)
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType));
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

    public class FooFormatter : FormatterBase
    {
        public FooFormatter()
            : base("application/foo")
        { }
    }

    public class BarFormatter : FormatterBase
    {
        public BarFormatter()
            : base("application/bar")
        {
            this.AddRequestHeaderMapping("x-formatted-by", "baz-formatter", StringComparison.OrdinalIgnoreCase, true, "text/baz");
        }
    }

    public class BazFormatter : FormatterBase
    {
        public BazFormatter()
            : base("text/baz")
        {
        }
    }
}