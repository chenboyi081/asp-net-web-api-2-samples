using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyArrayModelBinder<TElement> : MyCollectionModelBinder<TElement>
    {
        protected override bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<TElement> list)
        {
            bindingContext.Model = list.ToArray();
            return true;
        }
    }
}