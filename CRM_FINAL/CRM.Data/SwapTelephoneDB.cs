using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class SwapTelephoneDB
    {
        public static SwapTelephone GetSwapTelephoneByID(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.SwapTelephones.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }
    }
}
