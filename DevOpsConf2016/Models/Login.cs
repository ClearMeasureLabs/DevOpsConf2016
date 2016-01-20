﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using AutoMapper;
using DevOpsConf2016.Contexts;
using DevOpsConf2016.Controllers;
using DevOpsConf2016.Extensions;
using DevOpsConf2016.Models.ViewModels;

namespace DevOpsConf2016.Models
{
    [Table("Users")]
    public class Login
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Email")]
        public string EMail { get; set; }

        [Required]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [IgnoreMap]
        [NotMapped]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Your passwords must match!")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

        public virtual Attendee UserInfo { get; set; }

        public static Login ValidateUser(string userName, string password)
        {
            Login user;

            using (var db = new DevOpsContext())
            {
                user = db.Users.FirstOrDefault(x => x.EMail == userName && x.Password == password.EncodeToSHA1());
            }
            return user;
        }
    }
}
