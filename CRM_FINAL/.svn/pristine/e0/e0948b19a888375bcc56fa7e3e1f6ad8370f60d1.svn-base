using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PendarPajouhAPI.Helpers
{
    public static class SecurityHelpers
    {
        public static bool IsValidUser(string userName, string password)
        {
            bool isValid = false;

            string pendarPajouhApiUserNameInConfigFile = ConfigurationHelpers.PendarPajouhApiUserName;
            string pendarPajouhApiPasswordInConfigFile = ConfigurationHelpers.PendarPajouhApiPassword;

            if (userName.Equals(pendarPajouhApiUserNameInConfigFile) && password.Equals(pendarPajouhApiPasswordInConfigFile))
            {
                isValid = true;
            }

            return isValid;
        }
    }
}