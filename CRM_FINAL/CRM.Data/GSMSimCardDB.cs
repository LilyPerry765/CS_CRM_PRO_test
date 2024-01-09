using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class GSMSimCardDB
    {
        public static GSMSimCard GetGSMSimCardByTelephone(long telephone)
        {
           using(MainDataContext context = new MainDataContext())
           {
               return context.GSMSimCards.Where(t => t.TelephoneNo == telephone).SingleOrDefault();
           }
        }

        public static bool GsmCodeIsExist(string code)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool result = false;
                result = context.GSMSimCards.Any(gsm => gsm.Code == code);
                return result;
            }
        }
    }
}
