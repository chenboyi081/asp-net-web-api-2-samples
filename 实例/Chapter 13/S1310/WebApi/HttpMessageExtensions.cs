using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApi
{
    public static class Extensions
    {
        public static bool TryGetAuthorizationCode(this HttpRequestMessage request, out string authorizationCode)
        {
            authorizationCode = HttpUtility.ParseQueryString(request.RequestUri.Query)["code"];
            return !string.IsNullOrEmpty(authorizationCode);
        }

        public  static bool TryGetAccessToken(this HttpRequestMessage request, out string accessToken)
        {
            //从请求的Cookie中获取Access Token
            accessToken = null;
            CookieHeaderValue cookieValue = request.Headers.GetCookies(AuthenticateAttribute.CookieName).FirstOrDefault();
            if (null != cookieValue)
            {
                accessToken = cookieValue.Cookies.FirstOrDefault().Value;
                return true;
            }

            //获取Attach的Access Token
            object token;
            if( request.Properties.TryGetValue(AuthenticateAttribute.CookieName, out token))
            {
                accessToken = (string)token;
                return true;
            }
            
            return false;
        }

        public static void AttachAccessToken(this HttpRequestMessage request, string accessToken)
        {
            string token;
            if (!request.TryGetAccessToken(out token))
            {
                request.Properties[AuthenticateAttribute.CookieName] = accessToken;
            }
        }

        public static void SetAccessToken(this HttpResponseMessage response, HttpRequestMessage request, string accessToken)
        {
            if (request.Headers.GetCookies(AuthenticateAttribute.CookieName).Any())
            {
                return;
            }
            CookieHeaderValue cookie = new CookieHeaderValue(AuthenticateAttribute.CookieName, accessToken)
            {
                HttpOnly = true,
                Path = "/",
            };
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        }
    }
}