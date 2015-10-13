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
        public string Name { get; set; }
        public string Gender { get; set; }
        [Required]
        public int? Age { get; set; }
    }
}
