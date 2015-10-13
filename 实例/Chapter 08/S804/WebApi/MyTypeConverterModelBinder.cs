using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace WebApi
{
    public class MyTypeConverterModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (null == result)
            {
                return false;
            }
            try
            {
                bindingContext.Model = result.ConvertTo(bindingContext.ModelType);
                if (bindingContext.Model is string && string.IsNullOrWhiteSpace((string)bindingContext.Model) && bindingContext.ModelMetadata.ConvertEmptyStringToNull)
                {
                    bindingContext.Model = null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}