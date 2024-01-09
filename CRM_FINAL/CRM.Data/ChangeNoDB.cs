using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ChangeNoDB
    {

        public static ChangeNo GetChangeNoDBByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeNos.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<ChangeNo> GetChangeNoDBByIDs(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeNos.Where(t =>requestIDs.Contains(t.ID)).OrderBy(t=> t.OldTelephoneNo).ToList();
            }
        }
    }
}
