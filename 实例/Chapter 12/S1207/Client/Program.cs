using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < 6; i++)
                {
                    HttpResponseMessage response = client.GetAsync("http://localhost:3721/api/demo").Result;
                    Console.WriteLine(response.Content.ReadAsAsync<DateTime>().Result.ToString());
                    Thread.Sleep(1000);
                }
            }
            Console.Read();
        }
    }

}
