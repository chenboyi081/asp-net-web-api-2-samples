using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace WebApi
{
    public class MyCollectionModelBinder<TElement> : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                return false;
            }
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            List<TElement> list = null != result ?
                this.BindSimpleCollection(actionContext, bindingContext, result.RawValue, CultureInfo.InvariantCulture) :
                this.BindComplexCollection(actionContext, bindingContext);
            return this.CreateOrReplaceCollection(actionContext, bindingContext, list);
        }

        protected virtual bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<TElement> list)
        {
            ICollection<TElement> model = bindingContext.Model as ICollection<TElement>;
            if ((model == null) || model.IsReadOnly)
            {
                model = new List<TElement>();
                bindingContext.Model = model;
            }
            model.Clear();
            foreach (TElement local in list)
            {
                model.Add(local);
            }
            return true;
        }

        private List<TElement> BindSimpleCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, object rawValue, CultureInfo culture)
        {
            List<object> list1 = new List<object>();
            if (!(rawValue is string))
            {
                IEnumerable enumerable = rawValue as IEnumerable;
                if (null != enumerable)
                {
                    list1 = enumerable.Cast<object>().ToList();
                }
            }
            else
            {
                list1 = new List<object> { rawValue };
            }

            List<TElement> list2 = new List<TElement>();
            foreach (object obj in list1)
            {
                ModelBindingContext subContext = new ModelBindingContext(bindingContext)
                {
                    ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TElement)),
                    ModelName = bindingContext.ModelName,
                    ValueProvider = new CompositeValueProvider { new ElementalValueProvider(bindingContext.ModelName, obj, culture), bindingContext.ValueProvider }
                };
                if (actionContext.Bind(subContext))
                {
                    list2.Add((TElement)subContext.Model);
                }
                else
                {
                    list1.Add(null);
                }
            }
            return list2;
        }

        private List<TElement> BindComplexCollection(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            string key = ModelNameBuilder.CreatePropertyModelName(bindingContext.ModelName, "index");
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(key);
            IEnumerable<string> indexes;
            bool useZeroBasedIndexes = false;
            if (null == result)
            {
                indexes = this.GetZeroBasedIndexes();
                useZeroBasedIndexes = true;
            }
            else
            {
                indexes = (IEnumerable<string>)result.ConvertTo(typeof(IEnumerable<string>));
            }
            List<TElement> list = new List<TElement>();
            foreach (string index in indexes)
            {
                ModelBindingContext subContext = new ModelBindingContext(bindingContext)
                {
                    ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TElement)),
                    ModelName = ModelNameBuilder.CreateIndexModelName(bindingContext.ModelName, index)
                };
                bool failed = true;
                if (actionContext.Bind(subContext))
                {
                    list.Add((TElement)subContext.Model);
                    failed = false;
                }
                if (failed && useZeroBasedIndexes)
                {
                    return list;
                }
            }
            return list;
        }

        private IEnumerable<string> GetZeroBasedIndexes()
        {
            int index = 0;
            while (true)
            {
                yield return index.ToString(CultureInfo.InvariantCulture);
                index++;
            }
        }
    }
}