using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ValueProviders;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            ValueProviderResult result1 = new ValueProviderResult(new string[] { "1", "2", "3" }, "", null);
            ValueProviderResult result2 = new ValueProviderResult("123", "", null);

            //string[] => int[]
            int[] value1 = (int[])result1.ConvertTo(typeof(int[]));
            //string[] => int
            int value2 = (int)result1.ConvertTo(typeof(int));
            //string => int[]
            int[] value3 = (int[])result2.ConvertTo(typeof(int[]));

            Console.WriteLine("{0,-16}{1}", "RawValue", "NewValue");
            Console.WriteLine("{0,-16}{1}", result1.RawValue.ConvertToString(), value1.ConvertToString());
            Console.WriteLine("{0,-16}{1}", result1.RawValue.ConvertToString(), value2.ConvertToString());
            Console.WriteLine("{0,-16}{1}", result2.RawValue.ConvertToString(), value3.ConvertToString());
        }
    }
}
