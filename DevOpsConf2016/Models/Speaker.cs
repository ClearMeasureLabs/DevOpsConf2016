using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models
{
    public class Speaker
    {
        //public Speaker()
        //{
        //    Sessions = new List<SessionInfo>();
        //}

        [Key]
        public Guid Id { get; set; }
        [MaxLength(35)]
        public string TwitterHandle { get; set; }
[MaxLength(50)]
        public string Company { get; set; }
        [Required]
        [MaxLength(150)]
        public string CompanyURL { get; set; }
        [Required]
        [MaxLength(150)]        
        public string BlogURL { get; set; }

        public virtual Attendee Attendee { get; set; }

        public virtual ICollection<SessionInfo> Sessions { get; set; }
    }
}