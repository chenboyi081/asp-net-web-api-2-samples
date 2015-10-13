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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticateAttribute : FilterAttribute, IAuthenticationFilter, IActionFilter
    {
        public const string CookieName = "AccessToken";
        public string CaptureTokenUri { get; private set; }
        public AuthenticateAttribute(string captureTokenUri)
        {
            this.CaptureTokenUri = captureTokenUri;
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            string accessToken;
            if (!context.Request.TryGetAccessToken(out accessToken))
            {
                string clientId = "000000004810C359";
                string redirectUri = string.Format("{0}?requestUri={1}", this.CaptureTokenUri, context.Request.RequestUri);
                string scope = "wl.signin%20wl.basic";

                string uri = "https://login.live.com/oauth20_authorize.srf";
                uri += "?response_type=token&redirect_uri={0}&client_id={1}&scope={2}";
                uri = String.Format(uri, redirectUri, clientId, scope);
                context.Result = new RedirectResult(new Uri(uri), context.Request);
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