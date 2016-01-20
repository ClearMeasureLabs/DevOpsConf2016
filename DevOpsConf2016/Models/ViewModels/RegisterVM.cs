using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AutoMapper;

namespace DevOpsConf2016.Models.ViewModels
{
    public class RegisterVM : LoginVM
    {

        [Required]
        [IgnoreMap]
        [NotMapped]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Your passwords must match!")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

        public AttendeeVM AttendeeInfo { get; set; }

    }
}