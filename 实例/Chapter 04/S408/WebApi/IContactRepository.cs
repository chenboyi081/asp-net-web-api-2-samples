using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts(Predicate<Contact> predicate);
    }
}