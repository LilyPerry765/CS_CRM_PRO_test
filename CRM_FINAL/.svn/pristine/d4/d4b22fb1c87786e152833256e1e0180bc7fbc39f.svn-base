using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class MDFWorkingHoursDB
    {
        public static List<MDFWorkingHour> SearchMDFWorkingHour(string day)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFWorkingHours
                    .Where(t => (string.IsNullOrWhiteSpace(day) || t.Day.Contains(day)))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static MDFWorkingHour GetMDFWorkingHourByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFWorkingHours
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
    }
}
