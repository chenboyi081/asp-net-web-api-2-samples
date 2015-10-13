using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;

namespace WebApi.Models
{
    [FromUri]
    [DataContract]
    public class DemoModel
    {
        [Required]
        public string Foo { get; set; }

        [DataMember(IsRequired = true)]
        public string Bar { get; set; }

        [HttpBindRequired]
        public string Baz { get; set; }

        [AlwaysFail]
        public string Qux { get; set; }
    }

    public class AlwaysFailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return false;
        }
    }
}