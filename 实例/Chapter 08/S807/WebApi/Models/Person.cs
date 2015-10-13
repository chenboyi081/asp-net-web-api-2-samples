using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApi.Models
{
    [DataContract(Namespace = "http://www.artech.com/")]
    [KnownType(typeof(Employee))]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
    }
}