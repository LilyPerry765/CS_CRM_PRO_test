using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLChangeIPRequestDB
    {
        public static ADSLChangeIPRequest GetADSLChangeIPRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangeIPRequests.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
