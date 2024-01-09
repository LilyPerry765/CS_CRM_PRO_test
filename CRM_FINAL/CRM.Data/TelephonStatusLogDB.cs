using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TelephonStatusLogDB
    {
        public static TelephoneStatusLog GetLastTelephoneLogByTelephoneNo(long telephoneNo)
        {
            using(MainDataContext context = new MainDataContext())
            {
              return  context.TelephoneStatusLogs.Where(t => t.TelephoneNo == telephoneNo).OrderByDescending(t => t.InsertData).FirstOrDefault();
            }
        }
    }
}
