using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyDictionaryModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return ModelBinderBuilder.CreateGenericModelBinder(modelType, typeof(Dictionary<,>), typeof(MyDictionaryModelBinder<,>));
        }
    }
}