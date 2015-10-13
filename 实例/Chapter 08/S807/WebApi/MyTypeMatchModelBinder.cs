using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace WebApi
{
    public class MyTypeMatchModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (result == null)
            {
                return false;
            }
            object rawValue = result.RawValue;
            if (bindingContext.ModelType.IsInstanceOfType(result.RawValue))
            {
                //如果ModelMetadata的ConvertEmptyStringToNull属性为True
                //并且原始类型是一个空白字符串
                //将绑定的Model对象设置为Null
                if (rawValue is string && string.IsNullOrWhiteSpace((string)rawValue) && bindingContext.ModelMetadata.ConvertEmptyStringToNull)
                {
                    rawValue = null;
                }
                bindingContext.Model = rawValue;
                return true;
            }
            return false;
        }
    }
}