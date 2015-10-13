using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebApi.Models
{
    [ModelBinder]
    [DataContract(Namespace = "http://www.artech.com/")]
    public class DemoModel
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public int Z { get; set; }
    }
}