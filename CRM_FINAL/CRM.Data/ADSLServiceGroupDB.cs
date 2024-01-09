using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLServiceGroupDB
    {
        public static ADSLServiceGroup GetADSLServiceGroupById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroups
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<ADSLServiceGroup> SearchADSLServiceGroups(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroups
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (t.ID != -1))
                    .OrderBy(t => t.Title).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceGroupCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroups.Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.Title,
                    IsChecked = false
                }
                ).ToList();
            }
        }


    }
}
