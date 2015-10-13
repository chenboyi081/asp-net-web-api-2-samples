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
using System.Web.Http.Validation;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            ModelMetadataProvider metadataProvider = configuration.Services.GetModelMetadataProvider();
            ModelMetadata metadata = metadataProvider.GetMetadataForProperty(null, typeof(Employee), "Salary");
            IEnumerable<ModelValidatorProvider> validatorProviders = configuration.Services.GetModelValidatorProviders();
            ModelValidator[] validators = metadata.GetValidators(validatorProviders).ToArray();
            for (int i = 0; i < validators.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, validators[i].GetType().Name);
            }
        }
    }
}
