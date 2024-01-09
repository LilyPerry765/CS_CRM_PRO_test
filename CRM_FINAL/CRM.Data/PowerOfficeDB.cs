using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class PowerOfficeDB
    {

        public static List<PowerOffice> GetPowerOfficeByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PowerOffices.Where(t => t.RequestID == requestID).ToList();
            }
        }
    }
}
