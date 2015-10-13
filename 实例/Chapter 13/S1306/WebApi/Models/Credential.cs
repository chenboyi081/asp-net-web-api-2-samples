using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
public class Credential
{
    [DisplayName("用户名")]
    [Required]
    public string UserName { get; set; }

    [DisplayName("密码")]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
}