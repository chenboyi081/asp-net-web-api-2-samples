using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.ValueProviders;

namespace WebApi
{
    public class DictionaryValueProvider : IValueProvider
    {
        public IDictionary<string, object> Values { get; private set; }

        public DictionaryValueProvider(IDictionary<string, object> values)
        {
            this.Values = values;
        }

        public bool ContainsPrefix(string prefix)
        {
            string[] keys = this.Values.Keys.ToArray();
            if (prefix.Length == 0)
            {
                return (keys.Length > 0);
            }
            if (keys.Any(key => string.Compare(key, prefix, true) == 0))
            {
                return true;
            }
            if (keys.Any(key => key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && (key[prefix.Length] == '.' || key[prefix.Length] == '[')))
            {
                return true;
            }
            return false;

        }

        public ValueProviderResult GetValue(string key)
        {
            object value;
            if (this.Values.TryGetValue(key, out value))
            {
                return new ValueProviderResult(value, value.ToString(), CultureInfo.InvariantCulture);
            }
            return null;
        }
    }
}