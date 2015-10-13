using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheAttribute : ActionFilterAttribute
    {
        public string AbsoluteExpiration { get; set; }
        public string SlidingExpiration { get; set; }
        public CacheItemPriority Priority { get; set; }

        public CacheAttribute()
        {
            this.Priority = CacheItemPriority.Normal;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ReflectedHttpActionDescriptor actionDescriptor = actionContext.ActionDescriptor as ReflectedHttpActionDescriptor;
            if (null == actionDescriptor)
            {
                base.OnActionExecuting(actionContext);
            }

            CacheKey key = new CacheKey(actionDescriptor.MethodInfo,actionContext.ActionArguments);
            object[] array = HttpRuntime.Cache.Get(key.ToString()) as object[];
            if (null == array)
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            object value = array.Any() ? array[0] : null;
            IHttpActionResult actionResult = value as IHttpActionResult;
            if (null != actionResult)
            {
                actionContext.Response = actionResult.ExecuteAsync(CancellationToken.None).Result;
                return;
            }
            actionContext.Response = actionDescriptor.ResultConverter.Convert(actionContext.ControllerContext, value);
        }
    }

}