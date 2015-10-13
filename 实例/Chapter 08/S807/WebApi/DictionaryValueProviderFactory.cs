using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace WebApi
{
    public class DictionaryValueProviderFactory : ValueProviderFactory
    {
        public static IDictionary<string, object> Values { get; private set; }
        static DictionaryValueProviderFactory()
        {
            Values = new Dictionary<string, object>();
        }
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new DictionaryValueProvider(Values);
        }
    }
}