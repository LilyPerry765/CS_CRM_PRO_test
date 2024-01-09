using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class WorkFlowVersionDB
    {
        public static List<CheckableItem> GetVersionsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowVersions.Select(t => new CheckableItem { ID = t.ID, Name = t.Name, IsChecked = false }).ToList();
            }
        }
    }
}
