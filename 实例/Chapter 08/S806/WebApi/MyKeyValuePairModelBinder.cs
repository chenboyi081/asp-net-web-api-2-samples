using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyKeyValuePairModelBinder<TKey, TValue> : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ModelBindingContext context4key = new ModelBindingContext(bindingContext)
            {
                ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TKey)),
                ModelName = ModelNameBuilder.CreatePropertyModelName(bindingContext.ModelName, "key")
            };
            if (actionContext.Bind(context4key))
            {
                TKey key = (TKey)context4key.Model;
                ModelBindingContext context4Value = new ModelBindingContext(bindingContext)
                {
                    ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TValue)),
                    ModelName = ModelNameBuilder.CreatePropertyModelName(bindingContext.ModelName, "value")
                };
                if (actionContext.Bind(context4Value))
                {
                    TValue value = (TValue)context4Value.Model;
                    bindingContext.Model = new KeyValuePair<TKey, TValue>(key, value);
                    return true;
                }
            }
            return false;
        }
    }
}