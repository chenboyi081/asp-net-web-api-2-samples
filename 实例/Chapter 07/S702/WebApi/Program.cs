using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Metadata;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            ModelMetadataProvider metadataProvider = configuration.Services.GetModelMetadataProvider();
            Console.WriteLine("{0,-14}{1,-15}{2,-26}{3}", "PropertyName", "Description", "ConvertEmptyStringToNull", "IsReadOnly");
            foreach (ModelMetadata metadata in metadataProvider.GetMetadataForType(null, typeof(Contact)).Properties)
            {
                Console.WriteLine("{0,-14}{1,-15}{2,-26}{3}", metadata.PropertyName, metadata.Description, metadata.ConvertEmptyStringToNull, metadata.IsReadOnly);
            }
        }
    }
}
