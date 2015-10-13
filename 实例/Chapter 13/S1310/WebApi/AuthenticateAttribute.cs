using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WebApi
{
    public class AuthenticateAttribute : FilterAttribute, IAuthenticationFilter, IActionFilter
    {
        public const string CookieName = "AccessToken";

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            //从请求中获取Access Token
            string accessToken;
            if (context.Request.TryGetAccessToken(out accessToken))
            {
                return Task.FromResult<object>(null);
            }

            //从请求中获取Authorization Code，并利用它来获取Access Token
            string authorizationCode;
            if (context.Request.TryGetAuthorizationCode(out authorizationCode))
            { 
                string query = string.Format("code={0}", authorizationCode);
                string callbackUri = context.Request.RequestUri.AbsoluteUri.Replace(query, "").TrimEnd('?');
                using (HttpClient client = new HttpClient())
                {
                    Dictionary<string, string> postData = new Dictionary<string, string>();
                    postData.Add("client_id", "000000004810C359");
                    postData.Add("redirect_uri", callbackUri);
                    postData.Add("client_secret", "37cN-CGV9JPzolcOicYwRGc9VHdgvg6y");
                    postData.Add("code", authorizationCode);
                    postData.Add("grant_type", "authorization_code");
                    HttpContent httpContent = new FormUrlEncodedContent(postData);
                    HttpResponseMessage tokenResponse = client.PostAsync("https://login.live.com/oauth20_token.srf", httpContent).Result;

                    //得到Access Token并Attach到请求的Properties字典中
                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        string content = tokenResponse.Content.ReadAsStringAsync().Result;
                        JObject jObject = JObject.Parse(content);
                        accessToken = (string)JObject.Parse(content)["access_token"];
                        context.Request.AttachAccessToken(accessToken);

                        return Task.FromResult<object>(null);
                    }
                    else
                    {
                        return Task.FromResult<HttpResponseMessage>(tokenResponse);
                    }
                }
            }
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            string accessToken;
            if (!context.Request.TryGetAccessToken(out accessToken))
            {
                string clientId = "000000004810C359";
                string redirectUri = context.Request.RequestUri.ToString();
                string scope = "wl.signin%20wl.basic%20wl.offline_access";
                string url = "https://login.live.com/oauth20_authorize.srf";
                url += "?response_type=code&redirect_uri={0}&client_id={1}&scope={2}";
                url = String.Format(url, redirectUri, clientId, scope);
                context.Result = new RedirectResult(new Uri(url), context.Request);
            }
            return Task.FromResult<object>(null);
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            HttpResponseMessage response = continuation().Result;
            string accessToken;
            if (actionContext.Request.TryGetAccessToken(out accessToken))
            {
                response.SetAccessToken(actionContext.Request, accessToken);
            }
            return Task.FromResult<HttpResponseMessage>(response);
        }
    }
}