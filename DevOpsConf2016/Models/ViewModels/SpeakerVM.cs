using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models.ViewModels
{
    public class SpeakerVM
    {
        
        public string Company { get; set; }
        public string CompanyURL { get; set; }
        public string BlogURL { get; set; }
        public ICollection<SessionInfoVM> Sessions { get; set; }
    }
}