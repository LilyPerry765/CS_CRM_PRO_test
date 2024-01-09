using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TelRoundSaleDB
    {
        public static List<TelRoundSale> GetTelRoundSaleByRoundSaleInfoID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelRoundSales.Where(t => t.RoundSaleInfoID == id).ToList();
            }
        }

        public static List<CheckableItem> GetTelRoundTelephoneByRoundSaleInfoID(List<long> ids)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelRoundSales.Where(t => ids.Contains(t.RoundSaleInfoID) && t.IsActive == true)
                                            .Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.TelephoneNo.ToString() }).ToList();
                
            }
        }

        public static TelRoundSale GetTelRoundSaleByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelRoundSales.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static TelRoundSale GetTelRoundSaleByTelephoneNo(long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelRoundSales.Where(t => t.TelephoneNo == telephone && t.IsActive==true).SingleOrDefault();
            }
        }
    }
}
