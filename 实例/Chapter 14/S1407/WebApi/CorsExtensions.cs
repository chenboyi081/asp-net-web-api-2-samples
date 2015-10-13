using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Cors;

namespace WebApi
{
    public static class CorsExtensions
    {
        public static CorsRequestContext CreateCorsRequestContext(this HttpRequestMessage request)
        {
            CorsRequestContext context = new CorsRequestContext
            {
                RequestUri = request.RequestUri,
                HttpMethod = request.Method.Method,
                Host = request.Headers.Host,
                Origin = request.GetHeader("Origin"),
                AccessControlRequestMethod = request.GetHeader("Access-Control-Request-Method")
            };

            string requestHeaders = request.GetHeader("Access-Control-Request-Headers");
            if (!string.IsNullOrEmpty(requestHeaders))
            {
                Array.ForEach(requestHeaders.Split(','), header => context.AccessControlRequestHeaders.Add(header.Trim()));
            }
            return context;
        }

        public static void AddCorsHeaders(this HttpResponseMessage response, CorsResult result)
        {
            foreach (var item in result.ToResponseHeaders())
            {
                response.Headers.TryAddWithoutValidation(item.Key, item.Value);
            }
        }

        private static string GetHeader(this HttpRequestMessage request, string name)
        {
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(name, out headerValues))
            {
                return headerValues.FirstOrDefault();
            }
            return null;
        }
    }
}