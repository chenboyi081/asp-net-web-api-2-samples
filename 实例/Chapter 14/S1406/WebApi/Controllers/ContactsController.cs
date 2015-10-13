using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ContactsController : ApiController
    {
        [EnableCors("http://localhost:9527","*","*")]
        public IHttpActionResult GetAllContacts()
        {
            Contact[] contacts = new Contact[]
            {
                new Contact{ Name="张三", PhoneNo="123", EmailAddress="zhangsan@gmail.com"},
                new Contact{ Name="李四", PhoneNo="456", EmailAddress="lisi@gmail.com"},
                new Contact{ Name="王五", PhoneNo="789", EmailAddress="wangwu@gmail.com"},
            };
            return Json<IEnumerable<Contact>>(contacts);
        }
    }
}
