using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class CancelationListDB
    {
        public static List<CancelationListInfo> SearchCancelationList(
            string requestID,
            string telephoneNo,
            List<int> requestTypeIDs,
            List<int> centerIDs,
            string customerName,
            DateTime? requestDate,
            List<int> stepIDs,
            DateTime? cancelationDate,
            string reason,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CancelationRequestLists
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(requestID) || t.ID.ToString().Contains(requestID)) &&
                            (string.IsNullOrWhiteSpace(telephoneNo) || t.ID.ToString().Contains(telephoneNo)) &&
                            (requestTypeIDs.Count == 0 || requestTypeIDs.Contains(t.Request.RequestTypeID)) &&
                            (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                            (!requestDate.HasValue || t.Request.InsertDate == requestDate) &&
                            (string.IsNullOrWhiteSpace(customerName) || t.Request.Customer.LastName.Contains(customerName) || t.Request.Customer.FirstNameOrTitle.Contains(customerName)) &&
                            (stepIDs.Count == 0 || stepIDs.Contains(t.Request.Status.RequestStepID)) &&
                            (!cancelationDate.HasValue || t.EntryDate == cancelationDate) &&
                            (string.IsNullOrWhiteSpace(reason) || t.Reason.Contains(reason)))
                    .OrderByDescending(t => t.EntryDate)
                    .Select(t => new CancelationListInfo
                    {
                        ID = t.ID,                        
                        TelephoneNo = (long)t.Request.TelephoneNo,
                        RequestType = t.Request.RequestType.Title,
                        Center = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                        InsertRequestDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.Short),
                        Customer = t.Request.Customer.FirstNameOrTitle + " " + t.Request.Customer.LastName,
                        Step = t.Request.Status.RequestStep.StepTitle,
                        Date = Date.GetPersianDate(t.EntryDate, Date.DateStringType.Short),
                        User = GetFolderUser(t.UserID),
                        Reason = t.Reason,
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchCancelationListCount(
            string requestID,
            string telephoneNo,
            List<int> requestTypeIDs,
            List<int> centerIDs,
            string customerName,
            DateTime? requestDate,
            List<int> stepIDs,
            DateTime? cancelationDate,
            string reason)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CancelationRequestLists
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(requestID) || t.ID.ToString().Contains(requestID)) &&
                            (string.IsNullOrWhiteSpace(telephoneNo) || t.ID.ToString().Contains(telephoneNo)) &&
                            (requestTypeIDs.Count == 0 || requestTypeIDs.Contains(t.Request.RequestTypeID)) &&
                            (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                            (!requestDate.HasValue || t.Request.InsertDate == requestDate) &&
                            (string.IsNullOrWhiteSpace(customerName) || t.Request.Customer.LastName.Contains(customerName) || t.Request.Customer.FirstNameOrTitle.Contains(customerName)) &&
                            (stepIDs.Count == 0 || stepIDs.Contains(t.Request.Status.RequestStepID)) &&
                            (!cancelationDate.HasValue || t.EntryDate == cancelationDate) &&
                            (string.IsNullOrWhiteSpace(reason) || t.Reason.Contains(reason)))
                    .Count();
            }
        }

        public static string GetFolderUser(Guid id)
        {
            using (Folder.FolderDataContext contextFolder = new Folder.FolderDataContext())
            {
                return contextFolder.Users.Where(t => t.ID == id).SingleOrDefault().Fullname;

            }
        }
    }
}
