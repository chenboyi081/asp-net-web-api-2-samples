using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ContactsController : ApiController
    {
        public IEnumerable<Contact> GetAllContacts()
        {
            Contact[] contacts = new Contact[]
            {
                new Contact{ Name="张三", PhoneNo="123", EmailAddress="zhangsan@gmail.com"},
                new Contact{ Name="李四", PhoneNo="456", EmailAddress="lisi@gmail.com"},
                new Contact{ Name="王五", PhoneNo="789", EmailAddress="wangwu@gmail.com"},
            };
            return contacts;
        }
    }
}
