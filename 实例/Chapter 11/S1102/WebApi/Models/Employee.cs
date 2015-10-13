using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebApi.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
    }
}