using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
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