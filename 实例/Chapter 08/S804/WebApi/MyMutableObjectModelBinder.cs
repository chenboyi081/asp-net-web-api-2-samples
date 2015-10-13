using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace WebApi
{
    public class MyMutableObjectModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (!CanBindType(bindingContext.ModelType))
            {
                return false;
            }
            if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                return false;
            }

            bindingContext.Model = Activator.CreateInstance(bindingContext.ModelType);
            ComplexModelDto dto = new ComplexModelDto(bindingContext.ModelMetadata, bindingContext.PropertyMetadata.Values);
            ModelBindingContext subContext = new ModelBindingContext(bindingContext)
            {
                ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(() => dto, typeof(ComplexModelDto)),
                ModelName = bindingContext.ModelName
            };
            actionContext.Bind(subContext);

            foreach (KeyValuePair<ModelMetadata, ComplexModelDtoResult> item in dto.Results)
            {
                ModelMetadata propertyMetadata = item.Key;
                ComplexModelDtoResult dtoResult = item.Value;
                if (dtoResult != null)
                {
                    PropertyInfo propertyInfo = bindingContext.ModelType.GetProperty(propertyMetadata.PropertyName);
                    if (propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(bindingContext.Model, dtoResult.Model);
                    }
                }
            }
            return true;
        }

        internal static bool CanBindType(Type modelType)
        {
            if (TypeDescriptor.GetConverter(modelType).CanConvertFrom(typeof(string)))
            {
                return false;
            }
            if (modelType == typeof(ComplexModelDto))
            {
                return false;
            }
            return true;
        }
    }
}