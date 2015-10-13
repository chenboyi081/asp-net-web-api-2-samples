using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, "http://localhost:3721/api/demo/action1");
            HttpRequestMessage request2 = new HttpRequestMessage(HttpMethod.Get, "http://localhost:3721/api/demo/action1");
            HttpRequestMessage request3 = new HttpRequestMessage(HttpMethod.Get, "http://localhost:3721/api/demo/action1");

            MyHttpClientHandler handler1 = new MyHttpClientHandler { AllowAutoRedirect = false };
            MyHttpClientHandler handler2 = new MyHttpClientHandler { MaxAutomaticRedirections = 1 };
            MyHttpClientHandler handler3 = new MyHttpClientHandler { MaxAutomaticRedirections = 2 };

            HttpResponseMessage response1 = handler1.SendAsync(request1, new CancellationToken()).Result;
            HttpResponseMessage response2 = handler2.SendAsync(request2, new CancellationToken()).Result;
            HttpResponseMessage response3 = handler3.SendAsync(request3, new CancellationToken()).Result;

            Console.WriteLine(response1.StatusCode);
            Console.WriteLine(response2.StatusCode);
            Console.WriteLine(response3.StatusCode);

            Console.Read();
        }
    }

    public class MyHttpClientHandler : HttpClientHandler
    {
        public new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
