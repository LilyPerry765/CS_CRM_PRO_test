using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class MDFWiringDB
    {
        public static MDFWiring GetMDFWiringByRequestID(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.MDFWirings.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<MDFWiring> GetMDFWiringByRequestIDs(List<long> requestIDs)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.MDFWirings.Where(t => requestIDs.Contains(t.ID)).ToList();
            }
        }
    }
}
