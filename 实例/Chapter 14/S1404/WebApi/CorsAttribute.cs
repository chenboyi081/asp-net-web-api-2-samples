
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CorsAttribute : Attribute
    {
        public Uri[] AllowOrigins { get; private set; }
        public string ErrorMessage { get; private set; }

        public CorsAttribute(params string[] allowOrigins)
        {
            this.AllowOrigins = (allowOrigins ?? new string[0]).Select(origin => new Uri(origin)).ToArray();
        }

        public bool TryEvaluate(HttpRequestMessage request, out IDictionary<string, string> headers)
        {
            headers = null;
            string origin = request.Headers.GetValues("Origin").First();
            Uri originUri = new Uri(origin);
            if (this.AllowOrigins.Contains(originUri))
            {
                headers = this.GenerateResponseHeaders(request);
                return true;
            }
            this.ErrorMessage = "Cross-origin request denied";
            return false;
        }

        private IDictionary<string, string> GenerateResponseHeaders(HttpRequestMessage request)
        {
            //设置响应报头"Access-Control-Allow-Methods"
            string origin = request.Headers.GetValues("Origin").First();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Access-Control-Allow-Origin", origin);
            if (request.IsPreflightRequest())
            {
                //设置响应报头"Access-Control-Request-Headers"和"Access-Control-Allow-Headers"
                headers.Add("Access-Control-Allow-Methods", "*");
                string requestHeaders = request.Headers.GetValues("Access-Control-Request-Headers").FirstOrDefault();
                if (!string.IsNullOrEmpty(requestHeaders))
                {
                    headers.Add("Access-Control-Allow-Headers", requestHeaders);
                }
            }
            return headers;
        }
    }
}