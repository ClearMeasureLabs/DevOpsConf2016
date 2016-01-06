using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models
{
    [Table("Sessions")]
    public class SessionInfo
    {
        [Key]
        public int ID { get; set; }
        public Guid AttendeeId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Abstract { get; set; }
        public string Objectives { get; set; }
        [Required]
        public TalkLevel Level { get; set; }
        public string Requirements { get; set; }
        [DefaultValue(false)]
        public bool Accepted { get; set; }
        
        public virtual Speaker SpeakerInfo { get; set; } 
    }
}