using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class WaitingListReasonDB
    {
        public static List<WaitingListReason> SearchWaitingListReason(List<int> requestType, string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WaitingListReasons
                    .Where(t => 
						
                         (requestType.Count == 0 || requestType.Contains(t.RequestTypeID)) && 
                         (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title))
						  )
                        //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static WaitingListReason GetWaitingListReasonByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WaitingListReasons
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetWaitingListReasonCheckableByRequestTypeID(int? requestTypeID=null)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.WaitingListReasons
                    .Where(t=> !requestTypeID.HasValue  || t.RequestTypeID == requestTypeID)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
    }
}