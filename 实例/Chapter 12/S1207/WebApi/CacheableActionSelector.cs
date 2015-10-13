using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace WebApi
{
    public class CacheableActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor actionDescriptor = base.SelectAction(controllerContext);

            ReflectedHttpActionDescriptor reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;

            if (null == reflectedActionDescriptor)
            {
                return actionDescriptor;
            }

            CacheAttribute cacheAttribute = reflectedActionDescriptor.GetCustomAttributes<CacheAttribute>().FirstOrDefault()
                ?? reflectedActionDescriptor.ControllerDescriptor.GetCustomAttributes<CacheAttribute>().FirstOrDefault();

            if (null == cacheAttribute)
            {
                return actionDescriptor;
            }
            return new CacheableActionDescriptor(reflectedActionDescriptor, cacheAttribute);
        }
    }
}