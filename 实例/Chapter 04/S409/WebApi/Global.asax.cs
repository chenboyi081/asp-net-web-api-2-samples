using System.Web.Http;
using System.Web.Mvc;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NinjectDependencyResolver dependencyResolver = new NinjectDependencyResolver();
            dependencyResolver.Register<IContactRepository, DefaultContactRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver; 

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
