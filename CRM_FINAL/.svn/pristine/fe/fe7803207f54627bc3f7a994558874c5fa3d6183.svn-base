using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class QuotaDiscountDB
    {
        public static List<QuotaDiscount> SearchQuotaDiscount(
            int discountAmount,
            List<int> requestType,
            List<int> jobTitle,
            List<int> announceID,
            DateTime? fromStartDate,
            DateTime? toStartDate,
            DateTime? fromEndDate,
            DateTime? toEndDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.QuotaDiscounts
                    .Where(t =>
                            (discountAmount == -1 || t.DiscountAmount == discountAmount) &&
                            (requestType.Count == 0 || requestType.Contains(t.RequestTypeID)) &&
                            (jobTitle.Count == 0 || jobTitle.Contains(t.JobTitleID)) &&
                            (announceID.Count == 0 || announceID.Contains((int)t.AnnounceID) &&
                            (!fromStartDate.HasValue || t.StartDate >= fromStartDate) &&
                            (!toStartDate.HasValue || t.StartDate <= toStartDate) &&
                            (!fromEndDate.HasValue || t.EndDate >= fromEndDate) &&
                            (!toEndDate.HasValue || t.EndDate <= toEndDate))
                          )
                    .ToList();
            }
        }

        public static QuotaDiscount GetQuotaDiscountByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.QuotaDiscounts
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetQuotaDiscountCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.QuotaDiscounts.Select(t => new CheckableItem { ID = t.ID, Name = t.QuotaJobTitle.Title, Description = t.DiscountAmount + "     " + t.Announce.AnnounceTitle , IsChecked = false }).ToList();
            }
        }
    }
}
