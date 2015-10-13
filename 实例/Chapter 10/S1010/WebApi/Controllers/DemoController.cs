using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public Tuple<string[], string[]> Get()
        {
            HttpActionContext actionContext = new HttpActionContext
            {
                ControllerContext = this.ControllerContext
            };
            ModelMetadataProvider metadataProvider = this.Configuration.Services.GetModelMetadataProvider();
            ModelMetadata metadata = metadataProvider.GetMetadataForType(null, typeof(DemoModel));
            IValueProvider valueProvider = new CompositeValueProviderFactory(this.Configuration.Services.GetValueProviderFactories()).GetValueProvider(actionContext);
            ModelBindingContext bindingContext = new ModelBindingContext
            {
                ModelMetadata = metadata,
                ValueProvider = valueProvider,
                ModelState = actionContext.ModelState
            };
            actionContext.Bind(bindingContext);

            //验证之前的错误消息
            string[] errorMessages1 = actionContext.ModelState.SelectMany(
                item => item.Value.Errors.Select(
                error => error.ErrorMessage)).ToArray();

            //验证之前的错误消息
            bindingContext.ValidationNode.Validate(actionContext);
            string[] errorMessages2 = actionContext.ModelState.SelectMany(
                item => item.Value.Errors.Select(
                error => error.ErrorMessage)).ToArray();
            return new Tuple<string[], string[]>(errorMessages1, errorMessages2);
        }
    }
}
