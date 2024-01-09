using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace ServiceHost
{
    public class CustomPrincipal : IPrincipal 
    {
        IIdentity _identity;
        string[] _roles;

        public CustomPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        // helper method for easy access (without casting)
        public static CustomPrincipal Current
        {
            get
            {
                return Thread.CurrentPrincipal as CustomPrincipal;
            }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        // return all roles
        public string[] Roles
        {
            get
            {
                EnsureRoles();
                return _roles;
            }
        }

        // IPrincipal role check
        public bool IsInRole(string role)
        {
            EnsureRoles();

            return _roles.Contains(role);
        }

        // read Role of user from database
        protected virtual void EnsureRoles()
        {
            if (_identity.Name == "billing")
            {
                _roles = new string[1] { "billing"};
            }
            else if (_identity.Name == "data")
            {
                _roles = new string[1] { "data" };
            }
            else
            {
                _roles = new string[1] { "other" };
            }
        }
    }
}