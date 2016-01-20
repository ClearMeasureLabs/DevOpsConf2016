using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }

    }
}