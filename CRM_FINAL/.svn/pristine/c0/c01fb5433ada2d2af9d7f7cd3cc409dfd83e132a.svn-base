using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class CounterDB
    {
       public static Counter GetCounterByInvestigatePossibilityID(long investigatePossibilityID)
        {
            using (MainDataContext context = new MainDataContext())
            {

               IQueryable<Counter> counter = context.Counters
                    .Join(context.InstallLines, c => c.ID, i => i.CounterID, (c, i) => new { counter = c, installLines = i })
                    .Join(context.Wirings, i => i.installLines.WiringID, w => w.ID, (i, w) => new { wiring = w, installLines = i })
                    .Where(x => x.wiring.InvestigatePossibilityID == investigatePossibilityID)
                    .Select(t => t.installLines.counter);
                return counter.SingleOrDefault();
            }
        }

       public static Counter GetCounterByWiringID(long wiringID)
       {
           using (MainDataContext context = new MainDataContext())
           {

               Counter counter = context.Counters
                   .Join(context.InstallLines, c => c.ID, i => i.CounterID, (c, i) => new { counter = c, installLines = i })
                   .Join(context.Wirings, i => i.installLines.WiringID, w => w.ID, (i, w) => new { wiring = w, installLines = i })
                   .Where(x => x.wiring.ID == wiringID)
                   .Select(t => t.installLines.counter).SingleOrDefault();
               return counter;
           }
       }
       public static Counter GetCounterByTelephonNo(long TelNo)
       {
          // List<Counter> lstCounter = CounterDB.GetAllCounter();
           using (MainDataContext context = new MainDataContext())
           {

               Counter counter = context.Counters
                   .Where(x => x.TelephoneNo == TelNo)
                   .OrderByDescending(t=>t.InsertDate)
                   .FirstOrDefault();
               return counter;
           }
       }

       public static List<Counter> GetAllCounter()
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.Counters.ToList();
           }
       }

       public static Counter GetCounterByID(long counterID)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.Counters.Where(t => t.ID == counterID).SingleOrDefault();
           }
       }
    }
}
