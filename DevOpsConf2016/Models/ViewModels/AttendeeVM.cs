using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DevOpsConf2016.Models.ViewModels
{
    public class AttendeeVM 
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Twitter { get; set; }

        public SpeakerVM SpeakerInfo { get; set; }
        
        public bool IsSpeaker
        {
            get { return (SpeakerInfo != null); }
        }
    }
}