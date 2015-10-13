using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Controllers
{
    internal static class HttpRequestMessageExtensions
    {
        public static IList<IFilter> GetFilters(this HttpRequestMessage requestMessage)
        {
            object filters;
            if (!requestMessage.Properties.TryGetValue("__filters", out filters))
            {
                filters = new List<IFilter>();
                requestMessage.Properties.Add("__filters", filters);
            }
            return (IList<IFilter>)filters;
        }
    }

    [ControllerScopeFilter]
    public class FooController : ApiController
    {
        [ActionScopeFilter]
        public IEnumerable<string> Get()
        {
            return this.Request.GetFilters().Select(filter => filter.GetType().Name);
        }

    }

    [ControllerScopeFilter]
    public class BarController : ApiController
    {
        [ActionScopeFilter]
        [OverrideActionFilters]
        public IEnumerable<string> Get()
        {
            return this.Request.GetFilters().Select(filter => filter.GetType().Name);
        }

    }

    [ControllerScopeFilter]
    public class BazController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return this.Request.GetFilters().Select(filter => filter.GetType().Name);
        }
    }

    [ControllerScopeFilter]
    [OverrideActionFilters]
    public class QuxController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return this.Request.GetFilters().Select(filter => filter.GetType().Name);
        }
    }

    public abstract class FilterBaseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Request.GetFilters().Add(this);
        }
    }

    public class GlobalScopeFilterAttribute : FilterBaseAttribute { }
    public class ControllerScopeFilterAttribute : FilterBaseAttribute { }
    public class ActionScopeFilterAttribute : FilterBaseAttribute{ }
}
