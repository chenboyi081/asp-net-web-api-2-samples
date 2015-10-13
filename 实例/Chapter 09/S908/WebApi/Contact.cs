using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
    }

}
