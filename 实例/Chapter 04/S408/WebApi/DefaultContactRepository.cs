using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi
{
    public class DefaultContactRepository : IContactRepository
    {
        private static List<Contact> contacts = new List<Contact>
    {
        new Contact{ Id="001", Name = "张三",  PhoneNo="123", EmailAddress = "zhangsan@gmail.com"},
        new Contact{ Id="002", Name = "李四",  PhoneNo="456", EmailAddress = "lisi@gmail.com"}
    };

        public IEnumerable<Contact> GetContacts(Predicate<Contact> predicate)
        {
            return contacts.Where(contact => predicate(contact));
        }
    }
}