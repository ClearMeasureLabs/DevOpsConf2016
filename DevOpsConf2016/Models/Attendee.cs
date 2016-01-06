using System;
using System.ComponentModel.DataAnnotations;

namespace DevOpsConf2016.Models
{
    public class Attendee
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress()]
        public string EMail { get; set; }


        public virtual Speaker SpeakerInfo { get; set; }

    }
}
