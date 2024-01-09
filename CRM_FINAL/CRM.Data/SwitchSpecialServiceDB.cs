using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class SwitchSpecialServiceDB
    {
        public static List<SwitchSpecialService> SearchSwitchSpecialService(List<int> status, List<int> switchString, List<int> specialServiceType, int capacity)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchSpecialServices
                    .Where(t => 
							(status.Count == 0 || status.Contains(t.Status)) && 
                            (switchString.Count == 0 || switchString.Contains(t.SwitchID)) && 
                            (specialServiceType.Count == 0 || specialServiceType.Contains(t.SpecialServiceTypeID)) && 
                            (capacity == -1 || t.Capacity == capacity)
						  )
                    //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static SwitchSpecialService GetSwitchSpecialServiceByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchSpecialServices
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

    }
}