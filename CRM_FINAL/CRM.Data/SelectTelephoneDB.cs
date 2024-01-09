using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class SelectTelephoneDB
    {
        public static SelectTelephone GetSelectTelephone(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.SelectTelephones.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }
    }
}
