using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class RefundDepositDB
    {
        public static RefundDeposit GetRefundDepositByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RefundDeposits.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCauseOfRefundDepositCheckableItem()
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                result = context.CauseOfRefundDeposits.Select(cr => new CheckableItem
                                                                    {
                                                                        ID = cr.ID,
                                                                        Name = cr.Name,
                                                                        IsChecked = false
                                                                    }
                                                             )
                                                      .OrderByDescending(cr => cr.Name)
                                                      .ToList();
                return result;
            }
        }
    }
}
