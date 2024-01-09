using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class E1ModemDB
    {
       public static List<CheckableItem> GetE1ModemCheckableItem()
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.E1Modems.Select(t=> new CheckableItem { ID = t.ID  , Name = t.Name , IsChecked= false}).ToList();
           }
       }

       public static List<E1Modem> SearchE1Modem( string name)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.E1Modems
                   .Where(t =>
                           (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                         )
                   .ToList();
           }
       }

       public static E1Modem GetE1ModemByID(int id)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.E1Modems
                   .Where(t => t.ID == id)
                   .SingleOrDefault();
           }
       }

    }
}
