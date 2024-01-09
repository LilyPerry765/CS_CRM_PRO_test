using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class TitleIn118DB
    {
        public static TitleIn118 GetTitleIn118ByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
              return context.TitleIn118s.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static TitleIn118 GetLastTitlein118ByTelephone(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TitleIn118s.Where(t => t.TelephoneNo == telephoneNo).OrderByDescending(t => t.Date).FirstOrDefault(); 
            }
        }
    }
}
