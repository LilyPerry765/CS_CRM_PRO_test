using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Collections.ObjectModel;

namespace CRM.Data
{
    public static class IssueWiringDB
    {
        public static ObservableCollection<CheckableObject> SearchIssueWiring(List<int> wiringType, int printCount, DateTime? wiringIssueDate, bool? isPrinted, string wiringNo, string commentStatus,List<int> status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ObservableCollection<CheckableObject> temp= new ObservableCollection<CheckableObject>();
                temp =new ObservableCollection<CheckableObject>(context.IssueWirings
                    .Where(t =>
                            (wiringType.Count == 0 || wiringType.Contains(t.WiringTypeID)) &&
                            (printCount == -1 || t.PrintCount == printCount) &&
                            (!wiringIssueDate.HasValue || t.WiringIssueDate == wiringIssueDate) &&
                            (!isPrinted.HasValue || t.IsPrinted == isPrinted) &&
                            (string.IsNullOrWhiteSpace(wiringNo) || t.WiringNo.Contains(wiringNo)) &&
                            (string.IsNullOrWhiteSpace(commentStatus) || t.CommentStatus.Contains(commentStatus))&&
                            (status.Count == 0 || status.Contains(t.Status)) 
                          ).Select(t => new CheckableObject { IsChecked = false, Object = t }));

                return temp;
            }
        }

        public static IssueWiring GetIssueWiringByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.IssueWirings
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static IssueWiring GetIssueWiringByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.IssueWirings.Where(t => t.RequestID == requestID).OrderByDescending(t=>t.ID).FirstOrDefault();
            }
        }

        public static List<IssueWiring> GetIssueWiringByRequestIDByStatus(long requestID, int status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.IssueWirings.Where(t => t.RequestID == requestID && t.Status == status).ToList();
            }
        }
    }
}