using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models
{
    public class Sponsor
    {
        public Sponsor()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Display(Name = "Company Name")]
        public string Company { get; set; }

        [Display(Name = "Your Title")]
        public string Title { get; set; }

        [Required]
        [Phone(ErrorMessage = "The phone number you entered is invalid!")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The e-mail address you entered is invalid!")]
        [Display(Name = "E-Mail Address")]
        public string EMail { get; set; }
    }
}