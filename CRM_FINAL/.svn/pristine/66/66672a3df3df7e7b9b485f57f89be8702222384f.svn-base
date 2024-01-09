using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class PAPInfoOperatingStatusDB
    {
        public static List<PAPInfoOperatingStatus> SearchOperatingStatuses(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoOperatingStatus
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)))
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        public static PAPInfoOperatingStatus GetOperatingStatusByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoOperatingStatus
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetOperatingStatusCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoOperatingStatus
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }
    }
}
