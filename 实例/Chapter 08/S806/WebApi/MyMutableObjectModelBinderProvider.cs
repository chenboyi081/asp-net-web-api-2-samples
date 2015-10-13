using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyMutableObjectModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (MyMutableObjectModelBinder.CanBindType(modelType))
            {
                return new MyMutableObjectModelBinder();
            }
            return null;
        }
    }
}