using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi.Properties;

namespace WebApi.Models
{
    public class Person
    {
        [Required(ErrorMessageResourceName = "Required",ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required",ErrorMessageResourceType = typeof(Resources))]
        [Domain("M", "F", "m", "f", ErrorMessageResourceName = "Domain",ErrorMessageResourceType = typeof(Resources))]
        public string Gender { get; set; }

        [Required(ErrorMessageResourceName = "Required",ErrorMessageResourceType = typeof(Resources))]
        [Range(18, 25, ErrorMessageResourceName = "Range",ErrorMessageResourceType = typeof(Resources))]
        public int? Age { get; set; }
    }
}