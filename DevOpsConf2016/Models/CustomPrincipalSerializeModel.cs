﻿using System;
using System.Collections.Generic;

namespace DevOpsConf2016.Models
{
    public class CustomPrincipalSerializeModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; } 
    }
}