using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticateAttribute : AuthorizationFilterAttribute
    {
        private static Dictionary<string, string> userAccounters;
        static AuthenticateAttribute()
        {
            userAccounters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            userAccounters.Add("Foo", "Password");
            userAccounters.Add("Bar", "Password");
            userAccounters.Add("Baz", "Password");
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            AuthenticationHeaderValue headerValue = actionContext.Request.Headers.Authorization;
            if (null != headerValue && headerValue.Scheme == "Basic")
            {
                string credential = Encoding.Default.GetString(Convert.FromBase64String(headerValue.Parameter));
                string[] split = credential.Split(':');
                if (split.Length == 2)
                {
                    string userName = split[0];
                    string password;
                    if (userAccounters.TryGetValue(userName, out password))
                    {
                        if (password == split[1])
                        {
                            GenericIdentity identity = new GenericIdentity(userName);
                            actionContext.ControllerContext.RequestContext.Principal = new GenericPrincipal(identity, new string[0]);
                            return;
                        }
                    }
                }
            }
            HttpResponseMessage response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            string parameter = string.Format("realm=\"{0}\"", actionContext.Request.RequestUri.DnsSafeHost);
            AuthenticationHeaderValue challenge = new AuthenticationHeaderValue("Basic", parameter);
            response.Headers.WwwAuthenticate.Add(challenge);
            actionContext.Response = response;
        }
    }
}