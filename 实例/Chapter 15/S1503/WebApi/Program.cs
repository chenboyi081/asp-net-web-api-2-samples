using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {

            HttpClient httpClient = HttpClientFactory.Create(new FooHandler(), new BarHandler(), new BazHandler());
            HttpResponseMessage response = httpClient.GetAsync("http://www.asp.net").Result;
            Console.Read();
        }
    }
}
