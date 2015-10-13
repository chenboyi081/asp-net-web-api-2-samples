using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class DemoModel
    {
        [Required(ErrorMessage = "Foo是必需数据成员")]
        public string Foo { get; set; }

        [Required(ErrorMessage = "Bar是必需数据成员")]
        public string Bar { get; set; }
    }
}