using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;

namespace WebApi
{
    public class CacheableActionDescriptor : ReflectedHttpActionDescriptor
    {
        public CacheAttribute CacheAttribute { get; private set; }

        public CacheableActionDescriptor(ReflectedHttpActionDescriptor actionDescriptor,CacheAttribute cacheAttribute)
            : base(actionDescriptor.ControllerDescriptor,actionDescriptor.MethodInfo)
        {
            this.CacheAttribute = cacheAttribute;
        }

        public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
        {
            object result = base.ExecuteAsync(controllerContext, arguments, cancellationToken).Result;

            DateTime absoluteExpiration = Cache.NoAbsoluteExpiration;
            TimeSpan slidingExpiration = Cache.NoSlidingExpiration;
            CacheItemPriority priority = this.CacheAttribute.Priority;

            if (!string.IsNullOrEmpty(this.CacheAttribute.AbsoluteExpiration))
            {
                absoluteExpiration = DateTime.Now + TimeSpan.Parse(this.CacheAttribute.AbsoluteExpiration);
            }
            if (!string.IsNullOrEmpty(this.CacheAttribute.SlidingExpiration))
            {
                slidingExpiration = TimeSpan.Parse(this.CacheAttribute.SlidingExpiration);
            }

            CacheKey key = new CacheKey(this.MethodInfo, arguments);
            HttpRuntime.Cache.Insert(key.ToString(), new object[] { result }, null, absoluteExpiration, slidingExpiration, priority, null);

            return Task.FromResult<object>(result);
        }
    }
}