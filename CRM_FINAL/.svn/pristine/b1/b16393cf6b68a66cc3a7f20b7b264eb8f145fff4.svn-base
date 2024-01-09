using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class JobGroupDB
    {
        public static List<JobGroup> SearchJobGroups(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.JobGroups
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)))
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        public static JobGroup GetJobGroupByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.JobGroups
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetJobGroupsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.JobGroups
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
