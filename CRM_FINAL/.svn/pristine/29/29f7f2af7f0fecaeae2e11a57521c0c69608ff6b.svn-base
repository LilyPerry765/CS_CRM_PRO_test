using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class CableDesignOfficeDB
    {
        public static List<CableDesignOffice> GetCableDesignOfficeByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableDesignOffices.Where(t => t.RequestID == requestID).ToList();
            }
        }
    }
}
