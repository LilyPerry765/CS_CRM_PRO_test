using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class BuchtSwitchingDB
    {
        public static BuchtSwitching GetBuchtSwitchingByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtSwitchings.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }
    }
}
