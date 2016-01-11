using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Models.ViewModels
{
    public class SessionInfoVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Objectives { get; set; }
        public TalkLevel Level { get; set; }
        public string Requirements { get; set; }
        public bool Accepted { get; set; }
    }
}