using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApi.Models
{
    [DataContract(Namespace = "http://www.artech.com/")]
    public class Employee : Person
    {
        [DataMember]
        public string Department { get; set; }
    }
}