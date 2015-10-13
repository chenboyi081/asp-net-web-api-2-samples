using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    internal static class ModelBinderBuilder
    {
        public static IModelBinder CreateGenericModelBinder(Type modelType, Type modelTypeDefinition, Type modelBinderTypeDefinition)
        {
            if (!modelType.IsGenericType || modelType.IsGenericTypeDefinition)
            {
                return null;
            }
            //确保泛型参数个数一致
            Type[] genericArguments = modelType.GetGenericArguments();
            if (genericArguments.Length != modelBinderTypeDefinition.GetGenericArguments().Length)
            {
                return null;
            }
            //确保类型兼容性
            Type modelRealType = modelTypeDefinition.MakeGenericType(genericArguments);
            if (modelType.IsAssignableFrom(modelRealType))
            {
                Type modelBinderType = modelBinderTypeDefinition.MakeGenericType(genericArguments);
                return Activator.CreateInstance(modelBinderType) as IModelBinder;
            }
            return null;
        }
    }
}