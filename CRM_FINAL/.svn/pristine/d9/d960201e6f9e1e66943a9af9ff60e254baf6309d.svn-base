using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class CauseOfRefundDepositDB
    {
       public static List<CheckableItem> GetCauseOfRefundDepositCheckableItem()
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfRefundDeposits.Select(t=> new CheckableItem { ID = t.ID  , Name = t.Name , IsChecked= false}).ToList();
           }
       }

       public static List<CauseOfRefundDeposit> Search( string name)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfRefundDeposits
                   .Where(t =>
                           (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                         )
                   .ToList();
           }
       }

       public static CauseOfRefundDeposit GetCauseOfRefundDepositID(int id)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.CauseOfRefundDeposits
                   .Where(t => t.ID == id)
                   .SingleOrDefault();
           }
       }

    }
}
