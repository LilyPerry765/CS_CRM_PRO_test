using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class WorkUnitDB
    {
        public static List<CheckableItem> GetWorkUnitsCheckable(bool addNullItem = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = context.WorkUnits
                        .Select(t => new CheckableItem { Name = t.WorkUnitName, ID = t.ID, IsChecked = false })
                        .ToList();

                if (addNullItem) result.Insert(0, new CheckableItem { ID = -1, IsChecked = false });

                return result;
            }
        }

        public static WorkUnit GetWorkunitById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkUnits
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<WorkUnit> SearchWorkUnit(string workunitName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkUnits
                    .Where(t => (string.IsNullOrWhiteSpace(workunitName) || t.WorkUnitName.Contains(workunitName))).OrderBy(t => t.WorkUnitName).ToList();
            }            
        }

        public static List<CheckableItem> GetWorkUnitCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkUnits.Select(t => new CheckableItem { ID = t.ID, Name = t.WorkUnitName, IsChecked = false }).ToList();
            }
        }
    }
}
