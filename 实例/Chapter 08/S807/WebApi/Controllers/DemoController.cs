using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            DictionaryValueProviderFactory.Values.Clear();
            Employee employee = new Employee
            {
                Name = "张三",
                Gender = "男",
                BirthDate = new DateTime(1981, 8, 24),
                Department = "IT部"
            };
            DictionaryValueProviderFactory.Values.Add("person", employee);
        }

        public Person Get([ValueProvider(typeof(DictionaryValueProviderFactory))]Person person)
        {
            return person;
        }
    }
}