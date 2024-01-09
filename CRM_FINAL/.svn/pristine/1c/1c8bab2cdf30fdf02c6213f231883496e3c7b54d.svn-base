using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class UsedProductDB
    {
        public static List<UsedProduct> Search(string name, string unit)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.UsedProducts.Where(t => t.Name.Contains(name) && t.Unit.Contains(unit)).ToList();
            }
        }

        public static UsedProduct GetUsedProductID(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.UsedProducts.Where(t => t.ID == ID).SingleOrDefault();
            }
        }
    }
}
