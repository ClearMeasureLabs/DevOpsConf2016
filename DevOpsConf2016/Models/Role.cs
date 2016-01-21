using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Login> Users { get; set; } 

    }
}