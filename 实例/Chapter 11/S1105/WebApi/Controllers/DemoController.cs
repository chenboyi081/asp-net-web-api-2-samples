using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public IEnumerable<Tuple<string, string>> Get()
        {
            IHttpActionSelector actionSelector = GlobalConfiguration.Configuration.Services.GetActionSelector();
            foreach (var group in actionSelector.GetActionMapping(this.ControllerContext.ControllerDescriptor))
            {
                foreach (HttpActionDescriptor actionDescriptor in group)
                {
                    if (actionDescriptor.ActionName != "Get")
                    {
                        string converterTypeName = actionDescriptor.ResultConverter == null ? "N/A" : actionDescriptor.ResultConverter.GetType().Name;
                        yield return new Tuple<string, string>(actionDescriptor.ActionName, converterTypeName);
                    }
                }
            }
        }

        public object Foo()
        {
            throw new NotImplementedException();
        }

        public void Bar()
        { }

        public HttpResponseMessage Baz()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Qux()
        {
            throw new NotImplementedException();
        }
    }
}