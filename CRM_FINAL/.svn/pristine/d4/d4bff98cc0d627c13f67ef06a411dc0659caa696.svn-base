using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class CauseOfCutDB
    {
       public static List<CheckableItem> GetCauseOfCutCheckableItem()
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfCuts.Select(t=> new CheckableItem { ID = t.ID  , Name = t.Name , IsChecked= false}).ToList();
           }
       }

       public static List<CauseOfCut> Search( string name)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfCuts
                   .Where(t =>
                           (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                         )
                   .ToList();
           }
       }

       public static CauseOfCut GetCauseOfCutID(int id)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfCuts
                   .Where(t => t.ID == id)
                   .SingleOrDefault();
           }
       }

    }
}
