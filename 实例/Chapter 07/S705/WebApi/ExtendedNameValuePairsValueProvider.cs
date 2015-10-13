using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace WebApi
{
    public class ExtendedNameValuePairsValueProvider : NameValuePairsValueProvider
    {
        public ExtendedNameValuePairsValueProvider(Func<IEnumerable<KeyValuePair<string, string>>> valuesFactory,CultureInfo culture)
            : base(valuesFactory, culture)
        { }

        public ExtendedNameValuePairsValueProvider(IEnumerable<KeyValuePair<string, string>> values, CultureInfo culture)
            : base(values, culture)
        { }

        public override ValueProviderResult GetValue(string key)
        {
            ValueProviderResult result = base.GetValue(key);
            List<string> list = result.RawValue as List<string>;
            if (null != list)
            {
                return new ValueProviderResult(list.ToArray(), result.AttemptedValue,result.Culture);
            }
            return result;
        }
    }
}
