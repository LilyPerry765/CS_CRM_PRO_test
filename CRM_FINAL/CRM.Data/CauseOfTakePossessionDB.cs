using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class CauseOfTakePossessionDB
    {
       public static List<CheckableItem> GetCauseOfTakePossessionCheckableItem()
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfTakePossessions.Select(t=> new CheckableItem { ID = t.ID  , Name = t.Name , IsChecked= false}).ToList();
           }
       }

       public static List<CauseOfTakePossession> Search( string name)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfTakePossessions
                   .Where(t =>
                           (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                         )
                   .ToList();
           }
       }

       public static CauseOfTakePossession GetCauseOfTakePossessionID(int id)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfTakePossessions
                   .Where(t => t.ID == id)
                   .SingleOrDefault();
           }
       }

    }
}
