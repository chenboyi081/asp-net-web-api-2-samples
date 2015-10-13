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

            StaticValueProviderFactory.Add("[0].Key", "张三");
            StaticValueProviderFactory.Add("[0].Value.Name", "张三");
            StaticValueProviderFactory.Add("[0].Value.PhoneNo", "123");
            StaticValueProviderFactory.Add("[0].Value.EmailAddress", "zhangsan@gmail.com");

            StaticValueProviderFactory.Add("[1].Key", "李四");
            StaticValueProviderFactory.Add("[1].Value.Name", "李四");
            StaticValueProviderFactory.Add("[1].Value.PhoneNo", "456");
            StaticValueProviderFactory.Add("[1].Value.EmailAddress", "lisi@gmail.com");

            StaticValueProviderFactory.Add("[2].Key", "王五");
            StaticValueProviderFactory.Add("[2].Value.Name", "王五");
            StaticValueProviderFactory.Add("[2].Value.PhoneNo", "789");
            StaticValueProviderFactory.Add("[2].Value.EmailAddress", "wangwu@gmail.com");
        }

        public IDictionary<string, Contact> Get([ModelBinder]IDictionary<string, Contact> contacts)
        {
            return contacts;
        }
    }
}
