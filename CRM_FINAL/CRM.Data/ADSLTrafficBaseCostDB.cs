using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLTrafficBaseCostDB
    {
        public static ADSLTrafficBaseCost GetTrafficCost(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTrafficBaseCosts.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static long GetTrafficCostbyID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTrafficBaseCosts.Where(t => t.ID == id).SingleOrDefault().Cost;
            }
        }
    }
}
