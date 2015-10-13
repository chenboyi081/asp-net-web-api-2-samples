using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticateAttribute : FilterAttribute, IAuthenticationFilter
    {
        private static Dictionary<string, string> userAccounters;
        static AuthenticateAttribute()
        {
            userAccounters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            userAccounters.Add("Foo", "Password");
            userAccounters.Add("Bar", "Password");
            userAccounters.Add("Baz", "Password");
        }
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            IPrincipal user = null;
            AuthenticationHeaderValue headerValue = context.Request.Headers.Authorization;
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
                            user = new GenericPrincipal(identity, new string[0]);
                        }
                    }
                }
            }
            context.Principal = user;
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            IPrincipal user = context.ActionContext.ControllerContext.RequestContext.Principal;
            if (null == user || !user.Identity.IsAuthenticated)
            {
                string parameter = string.Format("realm=\"{0}\"", context.Request.RequestUri.DnsSafeHost);
                AuthenticationHeaderValue challenge = new AuthenticationHeaderValue("Basic", parameter);
                context.Result = new UnauthorizedResult(new AuthenticationHeaderValue[] { challenge }, context.Request);
            }
            return Task.FromResult<object>(null);
        }
    }
}