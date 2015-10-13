using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Http;
using System.Threading;
using System.Net;
namespace WebApi
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //如果当前为HTTPS请求，授权通过
            if (actionContext.Request.RequestUri.Scheme == Uri.UriSchemeHttps)
            {
                base.OnAuthorization(actionContext);
                return;
            }

            //对于HTTP-GET请求，将Scheme替换成https进行重定向
            if (actionContext.Request.Method == HttpMethod.Get)
            {
                Uri requestUri = actionContext.Request.RequestUri;
                string location = string.Format("https://{0}/{1}", requestUri.Host, requestUri.LocalPath.TrimStart('/'));
                IHttpActionResult actionResult = new RedirectResult(new Uri(location), actionContext.Request);
                actionContext.Response = actionResult.ExecuteAsync(new CancellationToken()).Result;
                return;
            }

            //采用其他HTTP方法的请求被视为Bad Request
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = "SSL Required"
            };
        }
    }
}