using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class AnnounceDB
    {

        public static List<CheckableItem> GetAnnounceCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Announces.Select(t => new CheckableItem
                                                {
                                                    ID = t.ID,
                                                    Name = t.AnnounceTitle,
                                                    IsChecked = false
                                                }
                                                 ).ToList();
            }
        }

        public static List<Announce> SearchAnnounce(
            string announceTitle,
            string issueNumber,
            DateTime? fromIssueDate,
            DateTime? toIssueDate,
            DateTime? fromStartDate,
            DateTime? toStartDate,
            DateTime? fromEndDate,
            DateTime? toEndDate,
            List<int> status
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Announces
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(announceTitle) || t.AnnounceTitle.Contains(announceTitle)) &&
                            (string.IsNullOrWhiteSpace(issueNumber) || t.IssueNumber.Contains(issueNumber)) &&
                            (!fromIssueDate.HasValue || t.IssueDate >= fromIssueDate) &&
                            (!toIssueDate.HasValue || t.IssueDate <= toIssueDate) &&
                            (!fromStartDate.HasValue || t.StartDate >= fromStartDate) &&
                            (!toStartDate.HasValue || t.StartDate <= toStartDate) &&
                            (!fromEndDate.HasValue || t.EndDate >= fromEndDate) &&
                            (!toEndDate.HasValue || t.EndDate <= toEndDate) &&
                            (status.Count == 0 || status.Contains((byte)t.Status)))
                    .ToList();
            }
        }

        public static Announce GetAnnounceById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Announces
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }
    }
}
