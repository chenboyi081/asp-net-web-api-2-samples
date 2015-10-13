using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Services.ReplaceRange(typeof(ModelValidatorProvider),
                new ModelValidatorProvider[] { new InvalidModelValidatorProvider() });
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
