using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class MDFRowDB
    {
        public static List<CheckableItem> GetMDFRowCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Number.ToString(),
                        IsChecked = false
                    }
                    )
                    .ToList();
            }
        }
    }
}
