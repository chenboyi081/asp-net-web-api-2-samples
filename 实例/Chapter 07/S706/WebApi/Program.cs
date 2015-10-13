using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ValueProviders.Providers;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("contact.Name", "张三");
            dictionary.Add("contact.PhoneNo", "123456789");
            dictionary.Add("contact.EmailAddress", "zhangsan@gmail.com");
            dictionary.Add("contact.Address.Province", "江苏");
            dictionary.Add("contact.Address.City", "苏州");
            dictionary.Add("contact.Address.District", "工业园区");
            dictionary.Add("contact.Address.Street", "星湖街328号");
            NameValuePairsValueProvider valueProvider = new NameValuePairsValueProvider(dictionary.ToArray(), null);

            //Prefix=""
            Console.WriteLine("Prefix: <Empty>");
            Console.WriteLine("{0,-14}{1}", "Key", "Value");
            IDictionary<string, string> keys = valueProvider.GetKeysFromPrefix(string.Empty);
            foreach (var item in keys)
            {
                Console.WriteLine("{0,-14}{1}", item.Key, item.Value);
            }

            //Prefix="contact"
            Console.WriteLine("\nPrefix: contact");
            Console.WriteLine("{0,-14}{1}", "Key", "Value");
            keys = valueProvider.GetKeysFromPrefix("contact");
            foreach (var item in keys)
            {
                Console.WriteLine("{0,-14}{1}", item.Key, item.Value);
            }

            //Prefix="contact.Address"
            Console.WriteLine("\nPrefix: contact.Address");
            Console.WriteLine("{0,-14}{1}", "Key", "Value");
            keys = valueProvider.GetKeysFromPrefix("contact.Address");
            foreach (var item in keys)
            {
                Console.WriteLine("{0,-14}{1}", item.Key, item.Value);
            }
        }
    }
}
