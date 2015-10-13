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
            dictionary.Add("contacts[0].Name", "张三");
            dictionary.Add("contacts[0].PhoneNo", "123456789");
            dictionary.Add("contacts[0].EmailAddress", "zhangsan@gmail.com");
            dictionary.Add("contacts[1].Name", "李四");
            dictionary.Add("contacts[1].PhoneNo", "987654321");
            dictionary.Add("contacts[1].EmailAddress", "lisi@gmail.com");

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

            //Prefix="contacts[0]"
            Console.WriteLine("\nPrefix: contacts[0]");
            Console.WriteLine("{0,-14}{1}", "Key", "Value");
            keys = valueProvider.GetKeysFromPrefix("contacts[0]");
            foreach (var item in keys)
            {
                Console.WriteLine("{0,-14}{1}", item.Key, item.Value);
            }

            //Prefix="contacts[1]"
            Console.WriteLine("\nPrefix: contacts[1]");
            Console.WriteLine("{0,-14}{1}", "Key", "Value");
            keys = valueProvider.GetKeysFromPrefix("contacts[1]");
            foreach (var item in keys)
            {
                Console.WriteLine("{0,-14}{1}", item.Key, item.Value);
            }
        }
    }
}
