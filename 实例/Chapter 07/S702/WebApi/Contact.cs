using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class Contact
    {
        [Display(Description = "Contact Name")]
        [Editable(false)]
        [ReadOnly(false)]
        public string Name { get; set; }

        [Display(Description = "Phone No.")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string PhoneNo { get; set; }

        [Display(Description = "Email Address")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string EmailAddress { get; set; }
    }
}
