using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class instLineDB
    {
     
        public static InstallLine GetinstLineByCounterID(long CounterID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallLines.Where(t => t.CounterID == CounterID).Single();
            }
        }
    }
}
