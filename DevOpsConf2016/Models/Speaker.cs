using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models
{
    public class Speaker
    {
        [Key]
        public Guid AttendeeId { get; set; }
        [MaxLength(35)]
        public string TwitterHandle { get; set; }
[MaxLength(50)]
        public string Company { get; set; }
        [Url]
        [Required]
        [MaxLength(150)]
        public string CompanyURL { get; set; }
        [Url]
        [Required]
        [MaxLength(150)]        
        public string BlogURL { get; set; }
        public virtual ICollection<SessionInfo> Sessions { get; set; } 
    }
}