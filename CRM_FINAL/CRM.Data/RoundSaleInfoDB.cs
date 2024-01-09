using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class RoundSaleInfoDB
    {
        public static List<RoundSaleInfo> SearchRoundSaleInfo(List<int> roundType, DateTime? entryDate, DateTime? startDate, DateTime? endDate, bool? isAuction, bool? isActive, long basePrice)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RoundSaleInfos
                    .Where(t =>
                            (roundType.Count == 0 || roundType.Contains((int)t.RoundType)) &&
                            (!entryDate.HasValue || t.EntryDate == entryDate) &&
                            (!startDate.HasValue || t.StartDate == startDate) &&
                            (!endDate.HasValue || t.EndDate == endDate) &&
                            (!isAuction.HasValue || t.IsAuction == isAuction) &&
                            (!isActive.HasValue || t.IsActive == isActive) &&
                            (basePrice == -1 || t.BasePrice == basePrice)
                          )
                    //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static RoundSaleInfo GetRoundSaleInfoByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RoundSaleInfos
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }



        public static List<RoundSaleInfo> GetRoundSaleInfoByRoundTypeID(byte roundType  , int centerID)
        {
            DateTime dateTime = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {
               return context.RoundSaleInfos
                             .Where(t => t.RoundType == roundType &&
                                         t.CenterID == centerID &&
                                         t.StartDate <= dateTime &&
                                         (t.EndDate == null || t.EndDate >= dateTime) &&
                                         t.IsActive == true
                                   ).ToList();
            }
        }
        public static RoundSaleInfo GetRoundSaleInfoByTelephoneNo(long telephonNo, List<long> ids)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RoundSaleInfos.Where(t2 => t2.ID == context.TelRoundSales.Where(t => t.TelephoneNo == telephonNo && ids.Contains(t.RoundSaleInfoID) && t.IsActive == true).SingleOrDefault().RoundSaleInfoID).SingleOrDefault();
            }
        }
    }
}