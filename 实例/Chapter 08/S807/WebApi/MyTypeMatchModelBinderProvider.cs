using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyTypeMatchModelBinderProvider : ModelBinderProvider
    {
        private static readonly MyTypeMatchModelBinder binder = new MyTypeMatchModelBinder();
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return binder;
        }
    }
}