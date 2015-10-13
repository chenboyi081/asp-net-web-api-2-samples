using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ContactsController : ApiController
    {
        public IContactRepository Repository { get; private set; }
        public ContactsController(IContactRepository repository)
        {
            this.Repository = repository;
        }
        public IEnumerable<Contact> Get(string id = "")
        {
            return this.Repository.GetContacts(contact => string.IsNullOrEmpty(id) || id == contact.Id);
        }
    }
}
