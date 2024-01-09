using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class WarningHistoryDB
    {
        public static List<WarningHistory> SearchWarningHistory(List<int> type, DateTime? date, DateTime? insertDate, long telephonNo, string time, DateTime? arrestLetterNoDate, string arrestReference, string arrestLetterNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WarningHistories
                              .Where(t =>
                                      (type.Count == 0 || type.Contains(t.Type)) &&
                                      (!date.HasValue || t.Date == date) &&
                                      (!insertDate.HasValue || t.InsertDate == insertDate) &&
                                      (telephonNo == -1 || t.TelephonNo == telephonNo) &&
                                      (!arrestLetterNoDate.HasValue || t.ArrestLetterNoDate == arrestLetterNoDate) &&
                                      (string.IsNullOrEmpty(arrestLetterNo) || t.ArrestLetterNo.Contains(arrestLetterNo)) &&
                                      (string.IsNullOrEmpty(arrestReference) || t.ArrestReference.Contains(arrestReference))
                                    )
                              .ToList();
            }
        }

        public static WarningHistory GetWarningHistoryByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WarningHistories
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static WarningHistory GetLastWarningHistoryByTelephon(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WarningHistories.Where(t => t.TelephonNo == telephoneNo).OrderByDescending(t => t.Date).ThenByDescending(t => t.Time).Take(1).SingleOrDefault();
            }
        }

        //TODO:rad
        public static WarnedTelephoneInfo GetWarnedTelephoneTrustByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                WarnedTelephoneInfo result = new WarnedTelephoneInfo();

                long? trust = 0;

                var query = context.Requests
                                   .Where(r => r.RequestTypeID == (int)DB.RequestType.Dayri && (r.TelephoneNo.HasValue && r.TelephoneNo.Value == telephoneNo))
                                   .OrderByDescending(r => r.RequestDate)
                                   .FirstOrDefault();
                if (query != null)
                {
                    trust = query.RequestPayments
                                 .Where(rp => rp.BaseCost.IsDeposit)
                                 .Sum(rp => rp.AmountSum);
                }
                result.Trust = (trust.HasValue) ? trust.Value.ToString() : string.Empty;
                result.TelephoneNo = telephoneNo.ToString();
                result.CustomerName = context.Telephones.Where(t => t.TelephoneNo == telephoneNo).Select(t => t.Customer.FirstNameOrTitle + " " + t.Customer.LastName).FirstOrDefault();

                return result;
            }
        }

    }
}