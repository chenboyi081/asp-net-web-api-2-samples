using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace WebApi
{
    public class HttpHeaderValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (var item in actionContext.Request.Headers)
            {
                foreach (var value in actionContext.Request.Headers.GetValues(item.Key))
                {
                    list.Add(new KeyValuePair<string, string>(item.Key.Replace("-", ""), value));
                }
            }
            return new NameValuePairsValueProvider(list,CultureInfo.InvariantCulture);
        }
    }
}