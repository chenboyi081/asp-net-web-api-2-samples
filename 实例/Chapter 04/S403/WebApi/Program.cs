using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Type typeCacheType = Type.GetType("System.Web.Http.Dispatcher.HttpControllerTypeCache, System.Web.Http");
            object typeCache = Activator.CreateInstance(typeCacheType, new object[] { new HttpConfiguration() });
            PropertyInfo cacheProperty = typeCacheType.GetProperty("Cache", BindingFlags.Instance | BindingFlags.NonPublic);
            Dictionary<string, ILookup<string, Type>> cachedTypes = (Dictionary<string, ILookup<string, Type>>)cacheProperty.GetValue(typeCache, null);

            Console.WriteLine("{0,-16}{1,-20}{2,-10}", "ControllerName", "Namespace", "TypeName");
            foreach (var item in cachedTypes)
            {
                foreach (string key in item.Value.Select(group => group.Key).Distinct())
                {
                    foreach (Type type in item.Value[key])
                    {
                        Console.WriteLine("{0,-16}{1,-20}{2,-10}", item.Key, key, type.Name);
                    }
                }
            }
        }
    }
}
