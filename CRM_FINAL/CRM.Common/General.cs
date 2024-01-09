using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Common
{
    public static class General
    {
        public static bool IsValidType(object o, Type t)
        {
            try
            {
                System.Convert.ChangeType(o, t);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
