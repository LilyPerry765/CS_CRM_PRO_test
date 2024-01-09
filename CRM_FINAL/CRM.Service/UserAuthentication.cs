using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;


namespace ServiceHost
{
    public class UserAuthentication : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            try
            {
                if (userName == "billing" && password == "kermanshah@billing")
                {

                }
                else if (userName == "data" && password == "kermanshah@data")
                {
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
}