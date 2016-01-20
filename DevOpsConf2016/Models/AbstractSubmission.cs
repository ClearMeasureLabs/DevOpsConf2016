using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace DevOpsConf2016.Models
{
    public class AbstractSubmission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail Address")]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(18)]
        [Display(Name = "Phone Number")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Session Title")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Brief Description")]
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}