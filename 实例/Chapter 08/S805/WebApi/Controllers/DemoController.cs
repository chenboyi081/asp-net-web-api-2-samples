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

            StaticValueProviderFactory.Add("contacts.index", "foo");
            StaticValueProviderFactory.Add("contacts.index", "bar");
            StaticValueProviderFactory.Add("contacts.index", "baz");

            StaticValueProviderFactory.Add("contacts[foo].Name", "张三");
            StaticValueProviderFactory.Add("contacts[foo].PhoneNo", "123");
            StaticValueProviderFactory.Add("contacts[foo].EmailAddress", "zhangsan@gmail.com");

            StaticValueProviderFactory.Add("contacts[bar].Name", "李四");
            StaticValueProviderFactory.Add("contacts[bar].PhoneNo", "456");
            StaticValueProviderFactory.Add("contacts[bar].EmailAddress",
                "lisi@gmail.com");

            StaticValueProviderFactory.Add("contacts[baz].Name", "王五");
            StaticValueProviderFactory.Add("contacts[baz].PhoneNo", "789");
            StaticValueProviderFactory.Add("contacts[baz].EmailAddress", "wangwu@gmail.com");

        }

        public IEnumerable<Contact> Get([ModelBinder] Contact[] contacts)
        {
            return contacts;
        }
    }
}
