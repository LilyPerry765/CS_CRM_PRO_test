using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class SwitchOfficeDB
    {

        public static List<SwitchOffice> GetSwitchOfficeByRequestID(long requestID)
       {
           using(MainDataContext context = new MainDataContext())
           {
               return context.SwitchOffices.Where(t=>t.RequestID == requestID).ToList();
           }
       }
        
    }
}
