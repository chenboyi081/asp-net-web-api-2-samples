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
    public  static bool TryGetAccessToken(this HttpRequestMessage request, out string accessToken)
    {
        accessToken = null;
        CookieHeaderValue cookieValue = request.Headers.GetCookies(AuthenticateAttribute.CookieName).FirstOrDefault();
        if (null != cookieValue)
        {
            accessToken = cookieValue.Cookies.FirstOrDefault().Value;
            return true;
        }

        accessToken = HttpUtility.ParseQueryString(request.RequestUri.Query)["access_token"];
        return !string.IsNullOrEmpty(accessToken);
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
            Path = "/"
        };
        response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
    }
}
}