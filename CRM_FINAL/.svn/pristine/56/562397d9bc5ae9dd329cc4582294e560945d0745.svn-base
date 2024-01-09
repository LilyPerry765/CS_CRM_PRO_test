using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class CycleDB
    {
        public static Cycle GetCycleById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cycles
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<Cycle> SearchCycle(DateTime? fromDate, DateTime? toDate, DateTime? dueDate, string cycleYear, string cycleName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cycles
                    .Where(t => (!fromDate.HasValue || t.FromDate == fromDate)
                        && (!toDate.HasValue || t.ToDate == toDate)
                        && (!dueDate.HasValue || t.DueDate == dueDate)
                        && (string.IsNullOrWhiteSpace(cycleYear) || t.CycleYear.Contains(cycleYear))
                        && (string.IsNullOrWhiteSpace(cycleName) || t.CycleName.Contains(cycleName)))
                        .OrderBy(t => t.CycleName)
                        .ToList();

            }
        }

        public static List<CheckableItem> GetCycleByCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cycles.Select(t => new CheckableItem { ID = t.ID, Name = t.CycleName, IsChecked = false }).ToList();
            }
        }
        public static List<Cycle> GetCycle(string Year)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cycles
                    .Where (t=> (t.CycleYear == Year))
                    .ToList();
            }
        }

        public static bool CheckOverlappingDateRanges(Cycle cycle)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.Cycles.Where(t => t.ID != cycle.ID).Any(t => t.FromDate <= cycle.FromDate && t.ToDate >= cycle.ToDate);
            }
        }



        public static Cycle GetDateCurrentCycle()
        {
            DateTime currentDateTime = (DateTime)DB.ServerDate();

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cycles.Where(t => t.FromDate.Value.Date <= currentDateTime.Date && t.ToDate.Value.Date >= currentDateTime.Date).SingleOrDefault();
            }
        }
        public static Cycle GetCycleByDate(DateTime date)
        {

            try
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.Cycles.Where(t => t.FromDate.Value.Date <= date.Date && t.ToDate.Value.Date >= date.Date).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
              return null;
            }

        }

    }

}
