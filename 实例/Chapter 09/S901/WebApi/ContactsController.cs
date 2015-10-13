using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;

namespace WebApi
{
    public class ContactsController : ApiController
    {
        [Route("api/contacts")]
        public void Post()
        {
            IEnumerable<MediaTypeFormatter> formatters = new MediaTypeFormatter[] { new XmlMediaTypeFormatter() };
            Contact contact = this.Request.Content.ReadAsAsync<Contact>(formatters).Result;

            Console.WriteLine("{0,-12}: {1}", "Name", contact.Name);
            Console.WriteLine("{0,-12}: {1}", "Phone No.", contact.PhoneNo);
            Console.WriteLine("{0,-12}: {1}", "EmailAddress", contact.EmailAddress);
            Console.WriteLine("{0,-12}: {1}", "Address", contact.Address);
        }
    }
}
