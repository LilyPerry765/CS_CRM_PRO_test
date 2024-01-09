using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Transactions;

namespace CRM.Data
{
    public static class WaitingListDB
    {
        public static List<WaitingListInfo> SearchWaitingList(
            string requestID,
            string telephoneNo,
            List<int> requestTypeID,
            List<int> reasonID,
            DateTime? fromInsertDate,
            DateTime? toInsertDate,
            DateTime? fromExitDate,
            DateTime? toExitDate,
            bool status,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WaitingLists
                    .Where(t =>
                            (DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID))&&
                            (string.IsNullOrWhiteSpace(requestID) || t.RequestID.ToString().Contains(requestID)) &&
                            (string.IsNullOrWhiteSpace(telephoneNo) || t.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                            (requestTypeID.Count == 0 || requestTypeID.Contains(t.Request.RequestTypeID)) &&
                            (reasonID.Count == 0 || reasonID.Contains(t.ReasonID ?? 0)) &&
                            (!fromInsertDate.HasValue || t.InsertDate >= fromInsertDate) &&
                            (!toInsertDate.HasValue || t.InsertDate <= toInsertDate) &&
                            (!fromExitDate.HasValue || t.ExitDate >= fromExitDate) &&
                            (!toExitDate.HasValue || t.ExitDate <= toExitDate) &&
                            (t.WaitingListType == null || t.WaitingListType != (int)DB.WatingListType.investigatePossibility) && // To be displayed The investigatePossibility watingList in The InvestigatePossibilityWaitinglistForm
                            (t.Status == status))
                    .OrderByDescending(t => t.InsertDate)
                    .Select(t => new WaitingListInfo
                    {
                        ID = t.ID,
                        RequestID = t.RequestID,
                        TelephoneNo=t.Request.TelephoneNo,
                        RequestType = GetRequestTypeTitle(t.Request.RequestTypeID),
                        ReasonID = t.ReasonID ?? 0,
                        Status = t.Status,
                        InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.Short),
                        CreatorUser = GetUserFullName(t.CreatorUserID),
                        ExitDate = Date.GetPersianDate(t.ExitDate, Date.DateStringType.Short),
                        ExitUser = GetUserFullName(t.ExitUserID)
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchWaitingListCounts(
            string requestID,
            string telephoneNo,
            List<int> requestTypeID,
            List<int> reasonID,
            DateTime? fromInsertDate,
            DateTime? toInsertDate,
            DateTime? fromExitDate,
            DateTime? toExitDate,
            bool status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WaitingLists
                    .Where(t =>
                            (DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID)) &&
                            (string.IsNullOrWhiteSpace(requestID) || t.RequestID.ToString().Contains(requestID)) &&
                            (string.IsNullOrWhiteSpace(telephoneNo) || t.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                            (requestTypeID.Count == 0 || requestTypeID.Contains(t.Request.RequestTypeID)) &&
                            (reasonID.Count == 0 || reasonID.Contains(t.ReasonID ?? 0)) &&
                            (!fromInsertDate.HasValue || t.InsertDate >= fromInsertDate) &&
                            (!toInsertDate.HasValue || t.InsertDate <= toInsertDate) &&
                            (!fromExitDate.HasValue || t.ExitDate >= fromExitDate) &&
                            (!toExitDate.HasValue || t.ExitDate <= toExitDate) &&
                            (t.WaitingListType == null || t.WaitingListType != (int)DB.WatingListType.investigatePossibility) && // To be displayed The investigatePossibility watingList in The InvestigatePossibilityWaitinglistForm
                            (t.Status == status))
                    .Count();
            }
        }

        public static WaitingList GetWaitingListByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WaitingLists
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static string GetRequestTypeTitle(long requestTypeId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.Where(t => t.ID == requestTypeId).SingleOrDefault().Title;
            }
        }

        public static string GetUserFullName(int? id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Users.Where(t => t.ID == id).SingleOrDefault() != null)
                {
                    User user = context.Users.Where(t => t.ID == id).SingleOrDefault();
                    return user.FirstName + " " + user.LastName;
                }
                else
                    return "";

            }
        }

        public static List<WaitingList> GetWaitingListByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WaitingLists.Where(t => t.RequestID == requestID).ToList();
            }
        }

        public static void SaveWaitingList(long requestID, Request request, WaitingList waitingList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    request.IsWaitingList = true;
                    request.Detach();
                    DB.Save(request, false);
                }

                if (waitingList != null)
                {
                    waitingList.RequestID = requestID;
                    waitingList.Detach();
                    DB.Save(waitingList, true);
                }

                scope.Complete();
            }
        }
    }
}