using System;
using System.Security.Principal;

namespace DevOpsConf2016.Controllers
{
    internal class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }
        
        public IIdentity Identity { get; private set; }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}