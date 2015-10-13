using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi
{
    [FromUri]
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
    }
}