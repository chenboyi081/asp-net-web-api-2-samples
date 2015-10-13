using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyArrayModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(System.Web.Http.HttpConfiguration configuration, Type modelType)
        {
            if (!modelType.IsArray)
            {
                return null;
            }
            Type elementType = modelType.GetElementType();
            return (IModelBinder)Activator.CreateInstance(typeof(MyArrayModelBinder<>).MakeGenericType(new Type[] { elementType }));
        }
    }
}