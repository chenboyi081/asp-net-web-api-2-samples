using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Services.ReplaceRange(typeof(ModelBinderProvider), new ModelBinderProvider[]{ 
           new MyTypeConverterModelBinderProvider(),
           new MyKeyValuePairModelBinderProvider(),
           new MyComplexModelDtoModelBinderProvider(),
           new MyArrayModelBinderProvider(),
           new MyCollectionModelBinderProvider(),
           new MyDictionaryModelBinderProvider(),
           new MyMutableObjectModelBinderProvider()});

            GlobalConfiguration.Configuration.Services.ReplaceRange(typeof(ValueProviderFactory), new ValueProviderFactory[] {new StaticValueProviderFactory()});
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
