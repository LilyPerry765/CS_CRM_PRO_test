using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TakePossessionDB
    {
        public static TakePossession GetTakePossessionByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TakePossessions.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<TakePossession> GetTakePossessionByIDs(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TakePossessions.Where(t => requestIDs.Contains(t.ID)).OrderBy(t => t.OldTelephone).ToList();
            }
        }
    }
}
