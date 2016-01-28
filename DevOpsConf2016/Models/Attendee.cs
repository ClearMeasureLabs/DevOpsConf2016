using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOpsConf2016.Models
{
    public class Attendee
    {
        //public Attendee ()
        //{
        //    SpeakerInfo = new Speaker();
        //}

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(60)]
        public string Title { get; set; }

        [Display(Name = "Twitter Handle")]
        [MaxLength(30)]
        public string Twitter { get; set; }

        public virtual Speaker SpeakerInfo { get; set; }
        public virtual Login UserInfo { get; set; }
    }
}
