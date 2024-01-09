using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public static  class OtherCostDB
    {
        public static List<CheckableItem> GetOtherCostCheckable()
        {
                using(MainDataContext context = new MainDataContext())
                {
                    return context.OtherCosts.Select(t=> new CheckableItem{ID=t.ID , Name=t.CostTitle, IsChecked=false}).ToList();
                }
        }

        public static OtherCost GetOtherCostByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.OtherCosts.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
