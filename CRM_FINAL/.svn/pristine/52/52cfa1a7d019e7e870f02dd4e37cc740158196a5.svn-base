using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class WirelessChangeServiceDB
    {
        public static WirelessChangeService GetWirelessChangeServicebyID(long requeatID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WirelessChangeServices.Where(t => t.ID == requeatID).SingleOrDefault();
            }
        }
    }
}
