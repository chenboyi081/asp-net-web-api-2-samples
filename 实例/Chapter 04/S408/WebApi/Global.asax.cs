using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<IContactRepository, DefaultContactRepository>();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),new UnityHttpControllerActivator(unityContainer));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
