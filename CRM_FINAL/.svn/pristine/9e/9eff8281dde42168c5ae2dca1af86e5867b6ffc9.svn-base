using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class SwapPCMDB
    {
        public static SwapPCM GetSwapPCMByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwapPCMs.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }
    }
}
