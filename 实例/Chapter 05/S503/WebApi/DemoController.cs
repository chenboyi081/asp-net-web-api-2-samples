using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi
{
    public class DemoController : ApiController
    {
        public IEnumerable<Contact> Get(string firstName, string lastName, string group = "Colleague")
        {
            throw new NotImplementedException();
        }
    }

    public class Contact { }
}