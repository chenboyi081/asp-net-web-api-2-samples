using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential("username", "password", "demain");
            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = client.GetAsync("http://localhost/webapi/api/demo").Result;
                IEnumerable<string> userNames = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach (string userName in userNames)
                {
                    Console.WriteLine(userName);
                }
            }
            Console.Read();
        }
    }
}
