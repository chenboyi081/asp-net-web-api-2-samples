using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyTypeConverterModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (TypeDescriptor.GetConverter(modelType).CanConvertFrom(typeof(string)))
            {
                return new MyTypeConverterModelBinder();
            }
            return null;
        }
    }

}