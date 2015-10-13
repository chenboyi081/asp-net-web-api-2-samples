using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.ValueProviders;

namespace WebApi.Controllers
{
    [ValueProvider(typeof(HttpHeaderValueProviderFactory))]
    public class CommonHttpHeaders
    {
        public string Connection { get; set; }
        public string CacheControl { get; set; }
        public string Host { get; set; }

        public IEnumerable<string> Accept { get; set; }
        public IEnumerable<string> AcceptEncoding { get; set; }
        public IEnumerable<string> AcceptLanguage { get; set; }
        public IEnumerable<string> UserAgent { get; set; }
    }
}
