using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ONuLinkDB
    {
        public static List<CheckableItem> GetONuLinkCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ONULinks
                    .Select(t => new CheckableItem
                    {
                        ID=t.ID,
                        Name = t.LinkNo.ToString(),
                        IsChecked = false
                    }
                    )
                    .ToList();
            }
        }
    }
}
