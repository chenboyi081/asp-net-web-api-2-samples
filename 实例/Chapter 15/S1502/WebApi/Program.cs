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
        HttpMessageHandler handler = new HttpClientHandler();
        handler = new BazHandler { InnerHandler = handler };
        handler = new BarHandler { InnerHandler = handler };
        handler = new FooHandler { InnerHandler = handler };
        HttpClient httpClient = new HttpClient(handler);
        HttpResponseMessage  response = httpClient.GetAsync("http://www.asp.net").Result;
        Console.Read();
    }
}
}
