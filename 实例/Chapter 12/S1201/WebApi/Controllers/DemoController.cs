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
    [Foo]
    public class DemoController : ApiController
    {
        [Bar]
        [Baz]
        public IEnumerable<Tuple<string, FilterScope>> Get()
        {
            IHttpActionSelector actionSelector = this.Configuration.Services.GetActionSelector();
            HttpActionDescriptor actionDescriptor = actionSelector.SelectAction(this.ControllerContext);
            foreach (FilterInfo filterInfo in actionDescriptor.GetFilterPipeline())
            {
                yield return new Tuple<string, FilterScope>(filterInfo.Instance.GetType().Name, filterInfo.Scope);
            }
        }
    }

    public  class FooAttribute : ActionFilterAttribute { }
    public  class BarAttribute : ActionFilterAttribute { }
    public  class BazAttribute : ActionFilterAttribute { }
    public  class QuxAttribute : ActionFilterAttribute { }
}
