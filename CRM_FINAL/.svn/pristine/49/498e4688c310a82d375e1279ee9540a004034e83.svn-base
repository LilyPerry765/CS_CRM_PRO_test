using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class SpecialWireAddressDB
    {
        public static SpecialWireAddress GetSpecialWireAddressByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialWireAddresses.Where(t => t.BuchtID == buchtID).SingleOrDefault();
            }
        }

        public static bool ExsistBuchtInSpecialWireAddress(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialWireAddresses.Any(t => t.BuchtID == buchtID);
            }
        }
    }
}
