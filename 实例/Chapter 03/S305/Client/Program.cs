using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient1 = new HttpClient();
            HttpClient httpClient2 = new HttpClient();
            HttpClient httpClient3 = new HttpClient();
            HttpClient httpClient4 = new HttpClient();

            httpClient3.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "PUT");
            httpClient4.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "DELETE");

            Console.WriteLine("{0,-7}{1,-24}{2,-6}", "Method", "X-HTTP-Method-Override", "Action");
            InvokeWebApi(httpClient1, HttpMethod.Get);
            InvokeWebApi(httpClient2, HttpMethod.Post);
            InvokeWebApi(httpClient3, HttpMethod.Post);
            InvokeWebApi(httpClient4, HttpMethod.Post);

            Console.Read();
        }

        async static void InvokeWebApi(HttpClient httpClient, HttpMethod method)
        {
            string requestUri = "http://localhost:3721/api/demo";
            HttpRequestMessage request = new HttpRequestMessage(method, requestUri);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            IEnumerable<string> methodsOverride;
            httpClient.DefaultRequestHeaders.TryGetValues("X-HTTP-Method-Override", out methodsOverride);
            string actionName = response.Content.ReadAsStringAsync().Result;
            string methodOverride = methodsOverride == null ? "N/A" : methodsOverride.First();
            Console.WriteLine("{0,-7}{1,-24}{2,-6}", method, methodOverride, actionName.Trim('"'));
        }
    }
}
