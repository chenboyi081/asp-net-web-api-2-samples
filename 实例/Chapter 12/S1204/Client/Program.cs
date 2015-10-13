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
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://localhost:3721/api/demo").Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("认证失败");
                AuthenticationHeaderValue challenge = response.Headers.WwwAuthenticate.FirstOrDefault();
                if (challenge != null && challenge.Scheme == "Basic")
                {
                    Console.Write("输入用户名：");
                    string userName = Console.ReadLine().Trim();
                    Console.Write("输入密码：");
                    string password = Console.ReadLine().Trim();
                    byte[] credential = Encoding.Default.GetBytes(string.Format("{0}:{1}", userName, password));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credential));
                    response = client.GetAsync("http://localhost:3721/api/demo").Result;
                    string result = response.Content.ReadAsAsync<string>().Result;
                    Console.WriteLine(result);
                }
            }
            Console.ReadLine();
        }
    }
}
