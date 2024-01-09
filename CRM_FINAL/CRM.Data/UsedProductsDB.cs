using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class UsedProductsDB
    {
        public static List<CheckableItem> GetCheckableItem()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.UsedProducts.Select(t => new CheckableItem { ID = t.ID, Name = t.Name, Description=t.Unit , IsChecked = false }).ToList();
            }
        }
    }
}
