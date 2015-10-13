using System;
using System.Collections.Generic;
using System.Linq;
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
            
            using (HttpClient client = new HttpClient())
            {
                byte[] credential = Encoding.Default.GetBytes(string.Format("{0}:{1}", "username", "password"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credential));

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
