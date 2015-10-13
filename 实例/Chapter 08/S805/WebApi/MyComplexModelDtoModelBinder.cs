using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace WebApi
{
    public class MyComplexModelDtoModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ComplexModelDto dto = bindingContext.Model as ComplexModelDto;
            if (null == dto)
            {
                return false;
            }
            foreach (ModelMetadata property in dto.PropertyMetadata)
            {
                ModelBindingContext subContext = new ModelBindingContext(bindingContext)
                {
                    ModelMetadata = property,
                    ModelName = ModelNameBuilder.CreatePropertyModelName(bindingContext.ModelName, property.PropertyName)
                };
                if (actionContext.Bind(subContext))
                {
                    dto.Results[property] = new ComplexModelDtoResult(subContext.Model, subContext.ValidationNode);
                }
            }
            return true;
        }
    }
}