using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi
{
    public class ContactsController : ApiController
    {
        private static List<Contact> contacts = new List<Contact>
        {
            new Contact{ Id="001", Name = "张三",  PhoneNo="123", EmailAddress="zhangsan@gmail.com"},
            new Contact{ Id="002",Name = "李四", PhoneNo="456", EmailAddress="lisi@gmail.com"}
        };

        public IEnumerable<Contact> Get()
        {
            return contacts;
        }

        public Contact Get(string id)
        {
            return contacts.FirstOrDefault(c => c.Id == id);
        }
    }

    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
    }
}
