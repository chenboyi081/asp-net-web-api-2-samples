using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebApi
{
    public class MyDictionaryModelBinder<TKey, TValue> : MyCollectionModelBinder<KeyValuePair<TKey, TValue>>
    {
        protected override bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<KeyValuePair<TKey, TValue>> list)
        {
            IDictionary<TKey, TValue> model = bindingContext.Model as IDictionary<TKey, TValue>;
            if (null == model || model.IsReadOnly)
            {
                model = new Dictionary<TKey, TValue>();
                bindingContext.Model = model;
            }
            model.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in list)
            {
                model[pair.Key] = pair.Value;
            }
            return true;
        }
    }
}