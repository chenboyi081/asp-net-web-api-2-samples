using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Contact
    {
        [AlwaysFail]
        public string Name { get; set; }
        [AlwaysFail]
        public string PhoneNo { get; set; }
        [AlwaysFail]
        public string EmailAddress { get; set; }
        [AlwaysFail(ErrorMessage = "Contact的Address属性验证失败")]
        public Address Address { get; set; }
    }

    [AlwaysFail(ErrorMessage = "Address对象验证失败")]
    public class Address
    {
        //[AlwaysFail]
        public string Province { get; set; }
        //[AlwaysFail]
        public string City { get; set; }
        //[AlwaysFail]
        public string District { get; set; }
        //[AlwaysFail]
        public string Street { get; set; }
    }

    public class AlwaysFailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return false;
        }
    }
}