using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web.Security;
using DevOpsConf2016.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Extensions;

namespace DevOpsConf2016.Controllers
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly string[] _roles;

        public CustomPrincipal(string email,string[] roles)
        {
            _roles = roles;
            this.Identity = new GenericIdentity(email);
        }
        
        public IIdentity Identity { get; private set; }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public bool IsInRole(string role)
        {
            if (_roles != null && _roles.Length > 0)
            {
                return _roles.Contains(role);
            }
            return false;
        }
    }
}