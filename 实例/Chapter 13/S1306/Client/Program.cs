using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            string token = GetSecurityToken("Foo", "Password", "http://localhost/webapi/account/login", ".ASPXAUTH");
            string address = "http://localhost/webapi/api/demo";
            if (!string.IsNullOrEmpty(token))
            {
                HttpClientHandler handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
                handler.CookieContainer.Add(new Uri(address), new Cookie(".ASPXAUTH", token));
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage response = httpClient.GetAsync(address).Result;
                    IEnumerable<string> userNames = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                    foreach (string userName in userNames)
                    {
                        Console.WriteLine(userName);
                    }
                }
            }
        }

        private static string GetSecurityToken(string userName, string password, string url, string cookieName)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Dictionary<string, string> credential = new Dictionary<string, string>();
                credential.Add("UserName", userName);
                credential.Add("Password", password);
                HttpResponseMessage response = httpClient.PostAsync(url, new FormUrlEncodedContent(credential)).Result;
                IEnumerable<string> cookies;
                if (response.Headers.TryGetValues("Set-Cookie", out cookies))
                {
                    string token = cookies.FirstOrDefault(value => value.StartsWith(cookieName));
                    if (null == token)
                    {
                        return null;
                    }
                    return token.Split(';')[0].Substring(cookieName.Length + 1);
                }
                return null;
            }
        }
    }
}
