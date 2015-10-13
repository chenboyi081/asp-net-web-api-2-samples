using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            StaticValueProviderFactory.Clear();
            StaticValueProviderFactory.Add("Name", "张三");
            StaticValueProviderFactory.Add("PhoneNo", "123456789");
            StaticValueProviderFactory.Add("EmailAddress", "zhangsan@gmail.com");
            StaticValueProviderFactory.Add("Address.Province", "江苏省");
            StaticValueProviderFactory.Add("Address.City", "苏州市");
            StaticValueProviderFactory.Add("Address.District", "工业园区");
            StaticValueProviderFactory.Add("Address.Street", "星湖街328号");
        }

        public Contact Get([ModelBinder]Contact contact)
        {
            return contact;
        }
    }
}
