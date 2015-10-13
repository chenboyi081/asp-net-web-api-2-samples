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

            StaticValueProviderFactory.Add("[0].Name", "张三");
            StaticValueProviderFactory.Add("[0].PhoneNo", "123");
            StaticValueProviderFactory.Add("[0].EmailAddress", "zhangsan@gmail.com");

            StaticValueProviderFactory.Add("[1].Name", "李四");
            StaticValueProviderFactory.Add("[1].PhoneNo", "456");
            StaticValueProviderFactory.Add("[1].EmailAddress", "lisi@gmail.com");

            StaticValueProviderFactory.Add("[2].Name", "王五");
            StaticValueProviderFactory.Add("[2].PhoneNo", "789");
            StaticValueProviderFactory.Add("[2].EmailAddress", "wangwu@gmail.com");
        }

        public IEnumerable<Contact> Get([ModelBinder]IEnumerable<Contact> contacts)
        {
            return contacts;
        }
    }
}
