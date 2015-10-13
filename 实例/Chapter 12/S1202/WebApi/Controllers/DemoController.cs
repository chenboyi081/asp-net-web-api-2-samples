using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public IEnumerable<Tuple<string, string, FilterScope>> Get()
        {
            HttpControllerDescriptor[] controllerDescriptors = new HttpControllerDescriptor[] { 
            new HttpControllerDescriptor(this.Configuration,"foo",typeof(FooController)),
            new HttpControllerDescriptor(this.Configuration,"bar",typeof(BarController)),
            new HttpControllerDescriptor(this.Configuration,"baz",typeof(BazController)),
        };

            IHttpActionSelector actionSelector = this.Configuration.Services.GetActionSelector();
            IEnumerable<HttpActionDescriptor> actionDescriptors = controllerDescriptors.SelectMany(controllerDescriptor =>
                actionSelector.GetActionMapping(controllerDescriptor)["Action"]);
            foreach (HttpActionDescriptor actionDescriptor in actionDescriptors)
            {
                foreach (FilterInfo filterInfo in actionDescriptor.GetFilterPipeline())
                {
                    yield return new Tuple<string, string, FilterScope>(
                            string.Format("{0}.{1}", actionDescriptor.ControllerDescriptor.ControllerType.Name, actionDescriptor.ActionName),
                            filterInfo.Instance.GetType().Name,
                            filterInfo.Scope);
                }
            }
        }
    }

    [Authenticate]
    public class FooController : ApiController
    {

        [Authenticate]
        public void Action()
        { }
    }

    [Authenticate]
    public class BarController : ApiController
    {
        public void Action()
        { }
    }

    public class BazController : ApiController
    {
        public void Action()
        { }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthenticateAttribute : FilterAttribute, IAuthenticationFilter
    {
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => { });
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => { });
        }
    }
}
