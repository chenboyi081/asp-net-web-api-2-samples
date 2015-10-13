using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    [DataContract(Namespace = "http://www.artech.com/")]
    public class Contact
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string Address { get; set; }
    }

}
