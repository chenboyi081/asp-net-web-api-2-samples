using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("foobar", "1"));
            list.Add(new KeyValuePair<string, string>("foobar", "2"));
            list.Add(new KeyValuePair<string, string>("foobar", "3"));

            ExtendedNameValuePairsValueProvider valueProvider = new ExtendedNameValuePairsValueProvider(list, null);
            var result = valueProvider.GetValue("foobar");

            int[] value1 = (int[])result.ConvertTo(typeof(int[]));
            int value2 = (int)result.ConvertTo(typeof(int));

            Console.WriteLine("{0,-16}{1}", "RawValue", "NewValue");
            Console.WriteLine("{0,-16}{1}", result.RawValue.ConvertToString(), value1.ConvertToString());
            Console.WriteLine("{0,-16}{1}", result.RawValue.ConvertToString(), value2.ConvertToString());
        }
    }
}
