using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;

namespace WebApi.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            this.Initialize(controllerContext);

            IHttpActionSelector actionSelector = this.Configuration.Services.GetActionSelector();
            HttpControllerDescriptor controllerDescriptor = this.ControllerContext.ControllerDescriptor;
            HttpActionDescriptor actionDescriptor = actionSelector.SelectAction(controllerContext);
            HttpActionContext actionContext = new HttpActionContext(controllerContext, actionDescriptor);
            actionDescriptor.ActionBinding.ExecuteBindingAsync(actionContext, cancellationToken).Wait();
            return this.Configuration.Services.GetActionInvoker().InvokeActionAsync(actionContext, cancellationToken);
        }
    }
}