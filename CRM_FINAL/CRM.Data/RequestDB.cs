﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Enterprise;
using System.Globalization;
using System.Transactions;
using System.Linq.Dynamic;
using System.Data;

namespace CRM.Data
{
    public static class RequestDB
    {

        public static  DataTable GetColorInf(long id )
        {
            DBHelper dbh = new DBHelper();
            string query = @"SELECT  f.CableColor1 , f.CableColor2  , c.Color, c1.Color, f.ID
                              FROM crm.[dbo].[FailureForm]
                                    f
                            JOIN crm.dbo.CableColor c
                              on c.ID = f.CableColor1
                              join crm.dbo.CableColor c1
                              on c1.ID = f.CableColor2
                              WHERE f.FailureRequestID = "+ id.ToString();
            return  dbh.ExecuteDT(query);
        }

        public static List<RequestInfo> SearchRequests(string id, string telephoneNo,
                                                       DateTime? requestStartDate, DateTime? requestEndDate,
                                                       DateTime? modifyStartDate, DateTime? modifyEndDate,
                                                       List<int> requestTypesIDs, List<int> centerIDs,
                                                       string customerName, string requesterName,
                                                       List<int> paymentTypesIDs, List<int> stepIDs,
                                                       string requestLetterNo, DateTime? letterDate,
                                                       bool isInquiryMode, bool isArchived,
                                                       string requestRejectReasonDescription,
                                                       out int totalRecords,
                                                       string sortParameter = null,
                                                       int pageSize = 10,
                                                       int startRowIndex = 0)
        {
            int validTime = 0;
            int.TryParse(DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.RequestValidTime)), out validTime);
            DateTime currentTime = DB.GetServerDate();

            using (MainDataContext context = new MainDataContext())
            {

                var query = context.Requests
                                   .Where(t =>
                                                (string.IsNullOrWhiteSpace(id) || t.ID.ToString().Contains(id)) &&
                                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                                (!requestStartDate.HasValue || t.RequestDate >= requestStartDate) &&
                                                (!requestEndDate.HasValue || t.RequestDate <= requestEndDate) &&
                                                (!modifyStartDate.HasValue || t.ModifyDate >= modifyStartDate) &&
                                                (!modifyEndDate.HasValue || t.ModifyDate <= modifyEndDate) &&
                                                (requestTypesIDs.Count == 0 || requestTypesIDs.Contains(t.RequestTypeID)) &&
                                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                (string.IsNullOrWhiteSpace(customerName) || t.Customer.LastName.Contains(customerName) || t.Customer.FirstNameOrTitle.Contains(customerName)) &&
                                                (string.IsNullOrWhiteSpace(requesterName) || t.RequesterName.Contains(requesterName)) &&
                                                (paymentTypesIDs.Count == 0 || paymentTypesIDs.Contains((int)t.RequestPaymentTypeID)) &&
                                                (stepIDs.Count == 0 || stepIDs.Contains(t.Status.RequestStepID)) &&
                                                (string.IsNullOrWhiteSpace(requestLetterNo) || t.RequestLetterNo.Contains(requestLetterNo)) &&
                                                (!letterDate.HasValue || t.RequestLetterDate == letterDate) &&
                                                (t.IsWaitingList == false) &&
                                                (t.IsCancelation == false) &&
                                                (t.IsVisible == true || t.IsVisible == null) &&
                                                (t.WaitForToBeCalculate == null || t.WaitForToBeCalculate == false) &&
                                                (
                                                 (isInquiryMode && t.CreatorUserID == DB.CurrentUser.ID)
                                                 ||
                                                 (!isInquiryMode && (DB.CurrentUser.CenterIDs.Contains(t.CenterID)) && (DB.CurrentUser.RequestStepsIDs.Contains(t.Status.RequestStepID)))
                                                ) &&
                                                ((isArchived && t.EndDate != null && t.EndDate < currentTime) || (!isArchived && (t.EndDate == null || t.EndDate > currentTime)))
                                           )
                                   .OrderByDescending(t => t.ModifyDate)
                                   .Select(t => new RequestInfo
                                               {
                                                   ID = t.ID,
                                                   TelephoneNo = t.TelephoneNo,
                                                   RequestTypeID = t.RequestTypeID,
                                                   RequestTypeName = t.RequestType.Title,
                                                   CenterName = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                                                   CustomerName = t.Customer.FirstNameOrTitle + " " + (t.Customer.LastName ?? ""),
                                                   RequestDate = t.RequestDate.ToPersian(Date.DateStringType.Short),
                                                   RequestLetterNo = t.RequestLetterNo,
                                                   RequestLetterDate = t.RequestLetterDate.ToPersian(Date.DateStringType.Short),
                                                   RequesterName = t.RequesterName,
                                                   RequestInsertDate = t.InsertDate.ToPersian(Date.DateStringType.Short),
                                                   CreatorUser = t.User.FirstName + " " + t.User.LastName,
                                                   RequestModifyDate = t.ModifyDate.ToPersian(Date.DateStringType.Short),
                                                   ModifyUser = t.User1.FirstName + " " + t.User1.LastName,
                                                   StatusName = t.Status.Title,
                                                   CurrentStep = t.Status.RequestStep.StepTitle,
                                                   StatusID = t.StatusID,
                                                   StepID = t.Status.RequestStepID,
                                                   PreviousAction = t.PreviousAction,
                                                   IsViewed = t.IsViewed,
                                                   CenterID = t.CenterID,
                                                   //Children = t.SubFlowStatus.Select(r => new SubRequestInfo { ID = r.ID, StatusID = r.StatusID, StatusName = r.Status.Title }).ToList(),
                                                   E1Links = (t.RequestTypeID == (byte)DB.RequestType.E1) ? t.E1.E1Links.Select(e => new E1LinkInfo { LinkNumber = e.LinkNumber, ID = e.ID }).ToList() : null,
                                                   RequesRejectReason = t.StatusLogs.Where(sl => sl.ToStatusID == t.StatusID).OrderByDescending(sl => sl.Date).Take(1).Select(x => new SubRequesRejectReason
                                                                                               {
                                                                                                   Reason = x.RequestRejectReason.Description,
                                                                                                   Description = x.Description
                                                                                               }
                                                                                      )
                                                                               .SingleOrDefault(),
                                                   isValidTime = t.Status.StatusType == (byte)DB.RequestStatusType.Start ? (t.InsertDate.AddMonths(validTime) <= currentTime ? true : false) : false,
                                               }
                                          )
                                .AsQueryable();
                var secondaryQuery = query.Where(ri =>
                                                   (string.IsNullOrEmpty(requestRejectReasonDescription) || ri.RequesRejectReason.Description.Contains(requestRejectReasonDescription))
                                                 )
                                          .AsQueryable();

                totalRecords = secondaryQuery.Count();

                if (string.IsNullOrEmpty(sortParameter))
                {
                    return secondaryQuery.OrderByDescending(t => t.ID).ThenByDescending(t => t.StatusName)
                                .Skip(startRowIndex)
                                .Take(pageSize)
                                .ToList();
                }

                return secondaryQuery.OrderBy(sortParameter)
                            .Skip(startRowIndex)
                            .Take(pageSize)
                            .ToList();
            }
        }

        public static List<RequestInfo> SearchRequestsHaveForeigSupportStep(string id, string telephoneNo, DateTime? requestStartDate, DateTime? requestEndDate, DateTime? modifyStartDate, DateTime? modifyEndDate, List<int> requestTypesIDs, List<int> centerIDs, string customerName, string requesterName, List<int> stepIDs, string requestLetterNo, DateTime? letterDate, bool isInquiryMode, string sortParameter = null, int pageSize = 10, int startRowIndex = 0)
        {
            int validTime = 0;
            int.TryParse(DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.RequestValidTime)), out validTime);
            DateTime currentTime = DB.GetServerDate();

            using (MainDataContext context = new MainDataContext())
            {
                var query = context.Requests
                    .Where(t => (string.IsNullOrWhiteSpace(id) || t.ID.ToString().Contains(id)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (!requestStartDate.HasValue || t.RequestDate >= requestStartDate) &&
                                (!requestEndDate.HasValue || t.RequestDate <= requestEndDate) &&
                                (!modifyStartDate.HasValue || t.ModifyDate >= modifyStartDate) &&
                                (!modifyEndDate.HasValue || t.ModifyDate <= modifyEndDate) &&
                                (requestTypesIDs.Count == 0 || requestTypesIDs.Contains(t.RequestTypeID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (string.IsNullOrWhiteSpace(customerName) || t.Customer.LastName.Contains(customerName) || t.Customer.FirstNameOrTitle.Contains(customerName)) &&
                                (string.IsNullOrWhiteSpace(requesterName) || t.RequesterName.Contains(requesterName)) &&
                                (stepIDs.Count == 0 || stepIDs.Contains(t.Status.RequestStepID)) &&
                                (string.IsNullOrWhiteSpace(requestLetterNo) || t.RequestLetterNo.Contains(requestLetterNo)) &&
                                (!letterDate.HasValue || t.RequestLetterDate == letterDate) &&
                                (t.IsWaitingList == false) &&
                                (t.IsCancelation == false) &&
                                (t.IsVisible == true || t.IsVisible == null) &&
                                ((isInquiryMode && t.CreatorUserID == DB.CurrentUser.ID) || (!isInquiryMode && (DB.CurrentUser.CenterIDs.Contains(t.CenterID)) && (DB.CurrentUser.RequestStepsIDs.Contains(t.Status.RequestStepID)))) &&
                                ((t.EndDate == null)))
                    .OrderByDescending(t => t.ModifyDate)
                    .Select(t => new RequestInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        RequestTypeID = t.RequestTypeID,
                        RequestTypeName = t.RequestType.Title,
                        CenterName = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        CustomerName = t.Customer.FirstNameOrTitle + " " + (t.Customer.LastName ?? ""),
                        RequestDate = t.RequestDate.ToPersian(Date.DateStringType.Short),
                        RequestLetterNo = t.RequestLetterNo,
                        RequestLetterDate = t.RequestLetterDate.ToPersian(Date.DateStringType.Short),
                        RequesterName = t.RequesterName,
                        InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                        CreatorUser = GetUserFullName(t.CreatorUserID),
                        ModifyDate = Date.GetPersianDate(t.ModifyDate, Date.DateStringType.DateTime),
                        ModifyUser = GetUserFullName(t.ModifyUserID),
                        StatusName = t.Status.Title,
                        CurrentStep = t.Status.RequestStep.StepTitle,
                        StatusID = t.StatusID,
                        StepID = t.Status.RequestStepID,
                        PreviousAction = t.PreviousAction,
                        IsViewed = t.IsViewed,
                        Children = t.SubFlowStatus.Select(r => new SubRequestInfo { ID = r.ID, StatusID = r.StatusID, StatusName = r.Status.Title }).ToList(),
                        RequesRejectReason = context.StatusLogs.Where(sl => sl.ToStatusID == t.StatusID && sl.ReqeustID == t.ID).OrderByDescending(sl => sl.Date).Take(1).Select(x => new SubRequesRejectReason { Reason = x.RequestRejectReason.Description, Description = x.Description }).SingleOrDefault(),
                        isValidTime = t.Status.StatusType == (byte)DB.RequestStatusType.Start ? (t.InsertDate.AddMonths(validTime) <= currentTime ? true : false) : false,
                    }
                            );

                if (string.IsNullOrEmpty(sortParameter))
                    return query.OrderByDescending(t => t.ID).ThenByDescending(t => t.StatusName)
                        .Skip(startRowIndex)
                        .Take(pageSize)
                        .ToList();

                return query.OrderBy(sortParameter)
                        .Skip(startRowIndex)
                        .Take(pageSize)
                        .ToList();
                //return x;
            }
        }

        public static int SearchRequestsCount(string id, string telephoneNo, DateTime? requestStartDate, DateTime? requestEndDate, DateTime? modifyStartDate, DateTime? modifyEndDate, List<int> requestTypesIDs, List<int> centerIDs, string customerName, string requesterName, List<int> paymentTypesIDs, List<int> stepIDs, string requestLetterNo, DateTime? letterDate, bool isInquiryMode, bool isArchived)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.Requests
                    .Where(t => (string.IsNullOrWhiteSpace(id) || t.ID.ToString().Contains(id)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (!requestStartDate.HasValue || t.RequestDate >= requestStartDate) &&
                                (!requestEndDate.HasValue || t.RequestDate <= requestEndDate) &&
                                (!modifyStartDate.HasValue || t.ModifyDate >= modifyStartDate) &&
                                (!modifyEndDate.HasValue || t.ModifyDate <= modifyEndDate) &&
                                (requestTypesIDs.Count == 0 || requestTypesIDs.Contains(t.RequestTypeID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (string.IsNullOrWhiteSpace(customerName) || t.Customer.LastName.Contains(customerName) || t.Customer.FirstNameOrTitle.Contains(customerName)) &&
                                (string.IsNullOrWhiteSpace(requesterName) || t.RequesterName.Contains(requesterName)) &&
                                (paymentTypesIDs.Count == 0 || paymentTypesIDs.Contains((int)t.RequestPaymentTypeID)) &&
                                (stepIDs.Count == 0 || stepIDs.Contains(t.Status.RequestStepID)) &&
                                (string.IsNullOrWhiteSpace(requestLetterNo) || t.RequestLetterNo.Contains(requestLetterNo)) &&
                                (!letterDate.HasValue || t.RequestLetterDate == letterDate) &&
                                (t.IsWaitingList == false) &&
                                (t.IsCancelation == false) &&
                                (t.IsVisible == true || t.IsVisible == null) &&
                                ((isInquiryMode && t.CreatorUserID == DB.CurrentUser.ID) || (!isInquiryMode && (DB.CurrentUser.CenterIDs.Contains(t.CenterID)) && (DB.CurrentUser.RequestStepsIDs.Contains(t.Status.RequestStepID)))) &&
                                ((isArchived && t.EndDate != null && t.EndDate < DB.GetServerDate()) || (!isArchived && (t.EndDate == null || t.EndDate > DB.GetServerDate()))))
                        .Count();
                return x;
            }
        }

        public static int SearchRequestsHaveSCount(string id, string telephoneNo, DateTime? requestStartDate, DateTime? requestEndDate, DateTime? modifyStartDate, DateTime? modifyEndDate, List<int> requestTypesIDs, List<int> centerIDs, string customerName, string requesterName, List<int> stepIDs, string requestLetterNo, DateTime? letterDate, bool isInquiryMode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.Requests
                    .Where(t => (string.IsNullOrWhiteSpace(id) || t.ID.ToString().Contains(id)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (!requestStartDate.HasValue || t.RequestDate >= requestStartDate) &&
                                (!requestEndDate.HasValue || t.RequestDate <= requestEndDate) &&
                                (!modifyStartDate.HasValue || t.ModifyDate >= modifyStartDate) &&
                                (!modifyEndDate.HasValue || t.ModifyDate <= modifyEndDate) &&
                                (requestTypesIDs.Count == 0 || requestTypesIDs.Contains(t.RequestTypeID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (string.IsNullOrWhiteSpace(customerName) || t.Customer.LastName.Contains(customerName) || t.Customer.FirstNameOrTitle.Contains(customerName)) &&
                                (string.IsNullOrWhiteSpace(requesterName) || t.RequesterName.Contains(requesterName)) &&

                                (stepIDs.Count == 0 || stepIDs.Contains(t.Status.RequestStepID)) &&
                                (string.IsNullOrWhiteSpace(requestLetterNo) || t.RequestLetterNo.Contains(requestLetterNo)) &&
                                (!letterDate.HasValue || t.RequestLetterDate == letterDate) &&
                                (t.IsWaitingList == false) &&
                                (t.IsCancelation == false) &&
                                ((isInquiryMode && t.CreatorUserID == DB.CurrentUser.ID) || (!isInquiryMode && (DB.CurrentUser.CenterIDs.Contains(t.CenterID)) && (DB.CurrentUser.RequestStepsIDs.Contains(t.Status.RequestStepID)))) &&
                                ((t.EndDate == null)))
                        .Count();
                return x;
            }
        }

        public static RequestInfo GetRequestInfoByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                    .Where(t => t.ID == requestID)
                    .Select(t => new RequestInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        RequestTypeName = t.RequestType.Title,
                        RequestTypeID = t.RequestTypeID,
                        CenterID = t.CenterID,
                        CenterName = t.Center.CenterName,
                        CustomerName = t.Customer.FirstNameOrTitle + " " + (t.Customer.LastName ?? ""),
                        strRequestDate = Date.GetPersianDate(t.RequestDate, Date.DateStringType.DateTime),
                        RequestLetterNo = t.RequestLetterNo,
                        strRequestLetterDate = Date.GetPersianDate(t.RequestLetterDate, Date.DateStringType.Short),
                        RequesterName = t.RequesterName,
                        RequestPaymentTypeID = t.RequestPaymentTypeID ?? 1,
                        InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                        CreatorUser = GetUserFullName(t.CreatorUserID),
                        ModifyDate = Date.GetPersianDate(t.ModifyDate, Date.DateStringType.DateTime),
                        ModifyUser = GetUserFullName(t.ModifyUserID),
                        StatusName = t.Status.Title,
                        CurrentStep = t.Status.RequestStep.StepTitle,
                        StatusID = t.StatusID,
                        StepID = t.Status.RequestStepID,
                        PreviousAction = t.PreviousAction
                    })
                    .SingleOrDefault();
            }
        }

        public static RequestShortInfo GetRequestShortInfoById(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                RequestShortInfo result;
                var query = context.Requests
                                 .Where(req =>
                                                (req.ID == requestId) &&
                                                (req.EndDate == null)
                                       )
                                 .Select(filteredData => new RequestShortInfo
                                                        {
                                                            ID = filteredData.ID,
                                                            CustomerID = filteredData.CustomerID,
                                                            CityName = filteredData.Center.Region.City.Name,
                                                            CenterName = filteredData.Center.CenterName,
                                                            RequestTypeId = filteredData.RequestTypeID,
                                                            RequestTypeTitle = filteredData.RequestType.Title,
                                                            RequestInsertDate = filteredData.InsertDate
                                                        }
                                        );
                result = query.SingleOrDefault();
                return result;
            }
        }
        public static List<RequestInfo> GetRequestInfoListByIDs(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                    .Where(t => requestIDs.Contains(t.ID))
                    .Select(t => new RequestInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        RequestTypeName = t.RequestType.Title,
                        RequestTypeID = t.RequestTypeID,
                        CenterID = t.CenterID,
                        CenterName = t.Center.CenterName,
                        CustomerName = t.Customer.FirstNameOrTitle + " " + (t.Customer.LastName ?? ""),
                        strRequestDate = Date.GetPersianDate(t.RequestDate, Date.DateStringType.DateTime),
                        RequestLetterNo = t.RequestLetterNo,
                        strRequestLetterDate = Date.GetPersianDate(t.RequestLetterDate, Date.DateStringType.Short),
                        RequesterName = t.RequesterName,
                        RequestPaymentTypeID = t.RequestPaymentTypeID ?? 1,
                        InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                        CreatorUser = GetUserFullName(t.CreatorUserID),
                        ModifyDate = Date.GetPersianDate(t.ModifyDate, Date.DateStringType.DateTime),
                        ModifyUser = GetUserFullName(t.ModifyUserID),
                        StatusName = t.Status.Title,
                        CurrentStep = t.Status.RequestStep.StepTitle,
                        StatusID = t.StatusID,
                        StepID = t.Status.RequestStepID,
                        PreviousAction = t.PreviousAction
                    }).ToList();
            }
        }

        public static List<Request> GetRequestbyTelephoneNoandRequestTypeID(long telephoneNo, int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID == requestTypeID && t.EndDate == null && t.IsCancelation == false && (t.IsVisible == true || t.IsVisible == null)).ToList();
            }
        }

        public static List<Request> GetFailureRequestbyTelephoneNoandRequestTypeID(long telephoneNo, int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID == requestTypeID && t.IsCancelation == false && (t.IsVisible == true || t.IsVisible == null)).ToList();
            }
        }

        public static List<RequestsTime> GetRequestTime(List<int> cites, List<int> centers, DateTime? fromDate = null, DateTime? toDate = null, int? WaitTimeRequest = null, TimeSpan? maxTime = null)
        {

            DateTime NowDateTime = DB.GetServerDate();

            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                             .Where(t =>
                                      (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                      (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                                      ((fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) && (toDate.HasValue ? t.InsertDate.Date <= (DateTime?)toDate : true)) &&
                                      (t.IsWaitingList == false && t.IsCancelation == false && t.EndDate != null) &&
                                      (WaitTimeRequest == null || t.RequestTypeID == WaitTimeRequest))

                             .GroupBy(x => x.RequestType.Title)
                             .Select(s => new RequestsTime
                             {
                                 RequestName = s.Key,
                                 minTime = s.Min(x => x.EndDate - x.InsertDate) ?? TimeSpan.Zero,
                                 maxTime = s.Max(x => x.EndDate - x.InsertDate) ?? TimeSpan.Zero,
                                 averageTime = TimeSpan.FromMinutes(s.Average(x => (x.EndDate.Value - x.InsertDate).TotalMinutes)),

                             })
                    // .Where(t2 => (maxTime == null || t2.maxTime >= maxTime))
                             .ToList();
            }
        }

        //private static TimeSpan ConverMinutesToTime(double minutes)
        //{
        //    TimeSpan time;
        //    CultureInfo culture;
        //    string formats = @"h\:mm\:ss\.fff";
        //    culture = CultureInfo.CurrentCulture;
        //    if (TimeSpan.TryParseExact(minutes.ToString(), formats, culture, TimeSpanStyles.AssumeNegative, out time))
        //    {
        //        return time;
        //    }
        //    else
        //    {
        //        return TimeSpan.Zero;
        //    }
        //}

        public static void GetRequestByInsertDate(List<int> cites,
                                                  List<int> centers,
                                                  DateTime? fromDate,
                                                  DateTime? toDate,
                                                  int? requestTypeID,
                                                  out int recordedRequestToday,
                                                  out int finishedRequestToday,
                                                  out int openRequestToday,
                                                  out int rejectedRequestToday,
                                                  out int canceledRequestToday,
                                                  out int watingListRequestToday,
                                                  out int recordedRequest,
                                                  out int rejectedRequest,
                                                  out int canceledRequest,
                                                  out int watingListRequest)
        {

            if (toDate.HasValue)
                toDate = toDate.Value.AddDays(1);

            DateTime currentDate = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {



                IQueryable<Request> query = context.Requests.Where(t => (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                                                       (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                                                                       (requestTypeID.HasValue ? t.RequestTypeID == requestTypeID : true)
                                                                       );

                recordedRequestToday = query.Where(t => t.InsertDate.Date == currentDate.Date).Count();
                finishedRequestToday = query.Where(t => (t.EndDate.HasValue && t.EndDate.Value.Date == currentDate.Date)).Count();
                openRequestToday = (recordedRequestToday - finishedRequestToday < 0 ? 0 : recordedRequestToday - finishedRequestToday);
                rejectedRequestToday = query.Where(t => t.PreviousAction == (byte)DB.Action.Reject && t.InsertDate.Date == currentDate.Date).Count();
                canceledRequestToday = query.Where(t => t.IsCancelation == true && t.InsertDate.Date == currentDate.Date).Count();
                watingListRequestToday = query.Where(t => t.IsWaitingList == true && t.InsertDate.Date == currentDate.Date).Count();



                IQueryable<Request> querytotal = context.Requests.Where(t =>
                                                                       (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                                                       (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                                                                       (requestTypeID.HasValue ? t.RequestTypeID == requestTypeID : true) &&
                                                                       ((fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) && (toDate.HasValue ? t.InsertDate.Date <= (DateTime?)toDate : true)));
                recordedRequest = querytotal.Count();
                rejectedRequest = querytotal.Where(t => t.PreviousAction == (byte)DB.Action.Reject).Count();
                canceledRequest = querytotal.Where(t => t.IsCancelation == true).Count();
                watingListRequest = querytotal.Where(t => t.IsWaitingList == true).Count();

            }
        }

        public static List<RequestWaitTimeInfo> GetRequestWaitTime(List<int> cites, List<int> centers, DateTime? fromDate = null, DateTime? toDate = null, int? WaitTimeRequest = null, TimeSpan? maxTime = null)
        {
            DateTime NowDateTime = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                              .Where(t =>
                                          (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                          (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                                          ((fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) && (toDate.HasValue ? t.InsertDate.Date <= (DateTime?)toDate : true)) &&
                                          (t.IsWaitingList == false && t.IsCancelation == false) &&
                                          (WaitTimeRequest == null || t.RequestTypeID == WaitTimeRequest)
                                    )
                              .Select(t => new RequestWaitTimeInfo
                              {
                                  CreateTime = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                                  waitTime = (t.EndDate ?? NowDateTime) - t.InsertDate,
                                  CustomerName = t.Customer.FirstNameOrTitle + " " + t.Customer.LastName,
                                  NowStatus = t.Status.Title,
                                  RequestName = t.RequestType.Title,
                                  RequestID = t.ID,
                              })
                              .Where(t2 => (maxTime == null || t2.waitTime >= maxTime))
                              .ToList();
            }

        }

        public static int GetCenterIDByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.FirstOrDefault(t => t.ID == requestID).CenterID;
            }
        }

        public static List<RequestFollowInfo> SearchFollowRequest(long requestID, long telephoneNo, string NationalCode, List<int> requestTypeIDs, string CustomerName, string CustomerLastName, string fatherName, string birthCertificateID, DateTime? requestData, out int count, int pageSize = 10, int startRowIndex = 0)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.Requests
                   .Where(t => (requestID == 0 || t.ID == requestID) &&
                               (string.IsNullOrEmpty(NationalCode) || t.Customer.CustomerID.ToString().Contains(NationalCode)) &&
                               (string.IsNullOrEmpty(CustomerName) || t.Customer.FirstNameOrTitle.Contains(CustomerName)) &&
                               (string.IsNullOrEmpty(CustomerLastName) || t.Customer.LastName.Contains(CustomerLastName)) &&
                               (string.IsNullOrEmpty(fatherName) || t.Customer.FatherName.Contains(fatherName)) &&
                               (string.IsNullOrEmpty(birthCertificateID) || t.Customer.BirthCertificateID.Contains(birthCertificateID)) &&
                               (!requestData.HasValue || t.InsertDate == requestData) &&
                               (telephoneNo == 0 || t.TelephoneNo == telephoneNo) &&
                               (requestTypeIDs.Count == 0 || requestTypeIDs.Contains(t.RequestTypeID)))
                   .Select(t => new RequestFollowInfo
                   {
                       ID = t.ID,
                       TelephoneNo = t.TelephoneNo.ToString(),
                       RequestTypeName = t.RequestType.Title,
                       RequestTypeID = t.RequestType.ID,
                       CenterName = t.Center.CenterName,
                       CustomerName = t.Customer.FirstNameOrTitle + " " + (t.Customer.LastName ?? ""),
                       RequesterName = t.RequesterName,
                       InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                       CreatorUser = GetUserFullName(t.CreatorUserID),
                       ModifyDate = Date.GetPersianDate(t.ModifyDate, Date.DateStringType.DateTime),
                       ModifyUser = GetUserFullName(t.ModifyUserID),
                       CurrentStep = t.Status.RequestStep.StepTitle,
                       CurrentStatus = t.StatusLogs.OrderByDescending(t2 => t2.Date).Take(1).Select(t2 => t2.Status1.RequestStep.StepTitle + ">" + t2.Status1.Title).SingleOrDefault(),
                       PreviousStatus = t.StatusLogs.OrderByDescending(t2 => t2.Date).Take(1).Select(t2 => t2.Status.RequestStep.StepTitle + ">" + t2.Status.Title).SingleOrDefault(),
                       IsCanceled = t.IsCancelation,
                       RequesRejectReason = context.StatusLogs.Where(sl => sl.ToStatusID == t.StatusID && sl.ReqeustID == t.ID).OrderByDescending(sl => sl.Date).Take(1).Select(x => new SubRequesRejectReason { Reason = x.RequestRejectReason.Description, Description = x.Description }).SingleOrDefault(),
                       FatherName = t.Customer.FatherName,
                       BirthCertificateID = t.Customer.BirthCertificateID,
                       IsWatting = t.IsWaitingList
                   });

                count = query.Count();

                return query.Skip(startRowIndex).Take(pageSize).ToList();

            }
        }


        public static int SearchFollowRequestCount(long requestID, long telephoneNo, string NationalCode, List<int> requestTypeIDs, string CustomerName, string CustomerLastName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                   .Where(t => (requestID == 0 || t.ID == requestID) &&
                               (string.IsNullOrEmpty(NationalCode) || t.Customer.CustomerID.ToString().Contains(NationalCode)) &&
                               (string.IsNullOrEmpty(CustomerName) || t.Customer.FirstNameOrTitle.ToString().Contains(CustomerName)) &&
                               (string.IsNullOrEmpty(CustomerLastName) || t.Customer.LastName.ToString().Contains(CustomerLastName)) &&
                               (telephoneNo == 0 || t.TelephoneNo == telephoneNo) &&
                               (requestTypeIDs.Count == 0) || requestTypeIDs.Contains(t.RequestTypeID)).Count();
            }
        }

        public static List<CheckableItem> GetADSLRequestCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes
                  .Where(t => (t.ID == (byte)DB.RequestType.ADSL)
                             || (t.ID == (byte)DB.RequestType.ADSLChangeService)
                             || (t.ID == (byte)DB.RequestType.ADSLCutTemporary)
                             || (t.ID == (byte)DB.RequestType.ADSLDischarge)
                             || (t.ID == (byte)DB.RequestType.ADSLChangePort))
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

        public static List<CheckableItem> GetADSLPapRequestCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes
                  .Where(t =>
                              (t.ID == (int)Data.DB.RequestType.ADSLDischargePAPCompany)
                              || (t.ID == (int)Data.DB.RequestType.ADSLInstalPAPCompany))
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

        public static Request GetRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == id).SingleOrDefault();

            }
        }

        public static DateTime GetRequestInsertDateByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == id).SingleOrDefault().InsertDate;

            }
        }

        public static List<Request> GetRequestByIDs(List<long> ids)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => ids.Contains(t.ID)).ToList();

            }
        }

        public static List<Request> GetRequestListByID(List<long> iDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => iDs.Contains(t.ID)).ToList();

            }
        }

        public static List<int> GetADSLRequest()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes
                 .Where(t => (t.ID == (int)Data.DB.RequestType.ADSL) ||
                             (t.ID == (int)Data.DB.RequestType.ADSLChangePort) ||
                             (t.ID == (int)Data.DB.RequestType.ADSLChangeService) ||
                             (t.ID == (int)Data.DB.RequestType.ADSLDischarge) ||
                             (t.ID == (int)Data.DB.RequestType.ADSLCutTemporary))
                   .Select(t => t.ID)

                   .ToList();

            }
        }

        public static List<int> GetADSLPapRequest()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes
                 .Where(t => (t.ID == (int)Data.DB.RequestType.ADSLDischargePAPCompany) ||
                             (t.ID == (int)Data.DB.RequestType.ADSLInstalPAPCompany))
                   .Select(t => t.ID)
                   .ToList();

            }
        }

        public static InstallRequest GetInstallRequestByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallRequests.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }

        public static int GetRequestCurrentStatusID(int requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == requestID).Select(t => t.StatusID).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetRequestCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Select(t => new CheckableItem { LongID = t.ID, Name = t.ID.ToString(), IsChecked = false }).ToList();
            }
        }

        //milad doran
        //public static List<RequestTitleInfo> GetRequestsStepsGroupInfo(bool isWaitingList = false, bool isCancelation = false, DB.Action? previousAction = null, DateTime? fromDate = null, DateTime? toDate = null, bool isEndDate = false)
        //{
        //    if (DB.CurrentUser == null)
        //        Logger.WriteInfo("CRM_CurrentUser is NULL");
        //    else
        //    {
        //        Logger.WriteInfo("CRM_CurrentUser : " + DB.CurrentUser.UserName);
        //        Logger.WriteInfo("CRM_CurrentUser.CenterIDsCount : " + DB.CurrentUser.CenterIDs.Count.ToString());
        //    }

        //    DateTime currentDateTime = DB.GetServerDate();
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        var x = context.Requests
        //                .Where(t =>
        //                          (DB.CurrentUser.CenterIDs.Contains(t.CenterID)) &&
        //                          (DB.CurrentUser.RequestStepsIDs.Contains(t.Status.RequestStepID)) &&
        //                          t.IsWaitingList == isWaitingList &&
        //                          t.IsCancelation == isCancelation &&
        //                          (t.IsVisible == true || t.IsVisible == null) &&
        //                          (previousAction == null || t.PreviousAction == (byte?)previousAction) &&
        //                          t.Status.RequestStep.IsVisible == true &&
        //                          (t.EndDate == null || currentDateTime < t.EndDate) &&
        //                          (t.WaitForToBeCalculate == null || t.WaitForToBeCalculate == false) &&
        //                          (fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) &&
        //                          (toDate.HasValue ? t.InsertDate.Date <= (DateTime?)toDate : true))
        //                .GroupBy(t => new { RequestTypeID = t.RequestTypeID, StepID = t.Status.RequestStepID, StepTitle = t.Status.RequestStep.StepTitle, RequestTitle = t.RequestType.Title })
        //                .Select(t => new RequestTitleInfo
        //                {
        //                    RequestTypeID = t.Key.RequestTypeID,
        //                    RequestTitle = t.Key.RequestTitle,
        //                    RequestDetails = new RequestInboxInfo
        //                    {
        //                        StepID = t.Key.StepID,
        //                        StepTitle = t.Key.StepTitle,
        //                        Count = t.Count(),
        //                        LastRequestDate = t.Max(x2 => x2.ModifyDate)
        //                        // LastRequestDate = context.Requests.Where(s => (DB.CurrentUser.CenterIDs.Contains(s.CenterID)) && (DB.CurrentUser.RequestStepsIDs.Contains(s.Status.RequestStepID)) && (s.Status.RequestStepID == t.Key.StepID)).Select(s => s.ModifyDate).Max()
        //                    }
        //                }).OrderBy(t => t.RequestTitle).ToList();
        //        return x;
        //    }

        //}

        //TODO:rad 13950226
        public static List<RequestTitleInfo> GetRequestsStepsGroupInfo(bool isWaitingList = false, bool isCancelation = false, DB.Action? previousAction = null, DateTime? fromDate = null, DateTime? toDate = null, bool isEndDate = false)
        {
            if (DB.CurrentUser == null)
                Logger.WriteInfo("CRM_CurrentUser is NULL");
            else
            {
                Logger.WriteInfo("CRM_CurrentUser : " + DB.CurrentUser.UserName);
                Logger.WriteInfo("CRM_CurrentUser.CenterIDsCount : " + DB.CurrentUser.CenterIDs.Count.ToString());
            }

            DateTime currentDateTime = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {
                var requestTitleInfos = context.Requests
                                               .Where(t =>
                                                         (DB.CurrentUser.CenterIDs.Contains(t.CenterID)) &&
                                                         (DB.CurrentUser.RequestStepsIDs.Contains(t.Status.RequestStepID)) &&
                                                         t.IsWaitingList == isWaitingList &&
                                                         t.IsCancelation == isCancelation &&
                                                         (t.IsVisible == true || t.IsVisible == null) &&
                                                         (previousAction == null || t.PreviousAction == (byte?)previousAction) &&
                                                         t.Status.RequestStep.IsVisible == true &&
                                                         (t.EndDate == null || currentDateTime < t.EndDate) &&
                                                         (t.WaitForToBeCalculate == null || t.WaitForToBeCalculate == false) &&
                                                         (fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) &&
                                                         (toDate.HasValue ? t.InsertDate.Date <= (DateTime?)toDate : true)
                                                       )
                                               .GroupBy(t => new
                                                            {
                                                                RequestTypeID = t.RequestTypeID,
                                                                StepID = t.Status.RequestStepID,
                                                                StepTitle = t.Status.RequestStep.StepTitle,
                                                                RequestTitle = t.RequestType.Title
                                                            }
                                                        )
                                               .Select(groupedData => new RequestTitleInfo
                                                           {
                                                               RequestTypeID = groupedData.Key.RequestTypeID,
                                                               RequestTitle = groupedData.Key.RequestTitle,
                                                               RequestDetails = new RequestInboxInfo
                                                                                    {
                                                                                        StepID = groupedData.Key.StepID,
                                                                                        StepTitle = groupedData.Key.StepTitle,
                                                                                        Count = groupedData.Count(),
                                                                                        LastRequestDate = groupedData.Max(request => request.ModifyDate)
                                                                                        //LastRequestDate = context.Requests
                                                                                        //                          .Where(s => 
                                                                                        //                                    (DB.CurrentUser.CenterIDs.Contains(s.CenterID)) && 
                                                                                        //                                    (DB.CurrentUser.RequestStepsIDs.Contains(s.Status.RequestStepID)) && 
                                                                                        //                                    (s.Status.RequestStepID == t.Key.StepID)
                                                                                        //                                 )
                                                                                        //                          .Select(s => s.ModifyDate)
                                                                                        //                          .Max()
                                                                                    }
                                                           }
                                                      )
                                               .OrderBy(requestTitleInfo => requestTitleInfo.RequestTitle)
                                               .ToList();

                return requestTitleInfos;
            }
        }

        public static int GetCurrentState(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == requestID).Select(t => t.StatusID).Single();
            }
        }

        public static int GetCurrentStep(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == requestID).Select(t => t.Status.RequestStepID).Single();
            }
        }

        public static int GetCity(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        //public static string GetFolderUser(Guid id)
        //{
        //    using (Folder.FolderDataContext contextFolder = new Folder.FolderDataContext())
        //    {
        //        if (contextFolder.Users.Where(t => t.ID == id).SingleOrDefault() != null)
        //            return contextFolder.Users.Where(t => t.ID == id).SingleOrDefault().Fullname;
        //        else
        //        {
        //            using (MainDataContext context = new MainDataContext())
        //            {
        //                return context.PAPInfoUsers.Where(t => t.ID == id).SingleOrDefault().Fullname;
        //            }
        //        }
        //    }
        //}

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

        public static void ExitWaitingList(WaitingList waitingList, Request request)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    request.IsWaitingList = false;

                    request.Detach();
                    DB.Save(request, false);
                }

                if (waitingList != null)
                {
                    waitingList.ExitDate = DB.GetServerDate();
                    waitingList.ExitUserID = DB.CurrentUser.ID;
                    waitingList.Status = true;

                    waitingList.Detach();
                    DB.Save(waitingList, false);
                }

                scope.Complete();
            }

        }

        public static List<Request> GetRelatedRequestByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.RelatedRequestID == requestID).ToList();
            }
        }

        //public static List<CostsOutsideBound> CostsOutsideBound(long requestID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.VisitAddresses.Where(t => t.RequestID == requestID).AsEnumerable()
        //                                     .Select(t => new CostsOutsideBound
        //                                                 {
        //                                                     OutBoundMeter = t.OutBoundMeter ?? 0,
        //                                                     InitialCost = GetInitialCosts(t.OutBoundMeter ?? 0),
        //                                                     MonthlyCosts = GetMonthlyCosts(t.OutBoundMeter ?? 0)

        //                                                 }).ToList();

        //    }
        //}

        //private static double GetInitialCosts(int outBoundMeter)
        //{
        //    return (5784000 / 1000 * outBoundMeter) + ((5784000 / 1000 * outBoundMeter) * ((double)6 / 100));
        //}
        //public static double GetMonthlyCosts(int outBoundMeter)
        //{
        //    int km = outBoundMeter / 1000;
        //    int mod = outBoundMeter % 1000;
        //    double totol = km * 18800;
        //    if (mod <= 500)
        //        totol = totol + 18800 * ((double)0.5);
        //    else
        //        totol = totol + 18800;

        //    return totol;
        //}


        public static bool CheckIsDocument(long requestID, long requestDocumentTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ReferenceDocuments.Where(t => t.RequestID == requestID && t.RequestDocument.DocumentRequestType.DocumentTypeID == requestDocumentTypeID).Any();
            }
        }

        public static Request GetLastRequestByTelephone(long Telephone, int requestType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == Telephone && t.RequestTypeID == requestType).OrderByDescending(t => t.ID).FirstOrDefault();
            }
        }

        public static Request GetLastRequestByTelephone(long Telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == Telephone).OrderByDescending(t => t.ID).FirstOrDefault();
            }
        }

        public static List<Request> GetAllOpenRequestbytelephone(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.EndDate == null).ToList();
            }
        }

        public static string GetOpenRequestNameTelephone(List<long> telephoneNos, out bool inWatinglist)
        {
            using (MainDataContext context = new MainDataContext())
            {

                string requestName = string.Empty;

                inWatinglist = context.Requests.Any(t => telephoneNos.Contains((long)t.TelephoneNo) && t.EndDate == null && t.IsCancelation == false && t.IsWaitingList == true);
                requestName += string.Join(",", context.Requests.Where(t => telephoneNos.Contains((long)t.TelephoneNo) && t.EndDate == null && t.IsCancelation == false && t.RequestTypeID != (int)DB.RequestType.Failure117).Select(t => t.RequestType.Title + ":" + t.TelephoneNo.ToString()).ToArray());
                requestName += string.Join(",", context.TranslationOpticalCabinetToNormalConncetions.Where(t => telephoneNos.Contains((long)t.FromTelephoneNo) && t.Request.EndDate == null && t.Request.IsCancelation == false).Select(t => t.Request.RequestType.Title + ":" + t.FromTelephoneNo.ToString()).ToArray());
                requestName += string.Join(",", context.ExchangeCabinetInputConncetions.Where(t => telephoneNos.Contains((long)t.FromTelephoneNo) && t.Request.EndDate == null && t.Request.IsCancelation == false).Select(t => t.Request.RequestType.Title + ":" + t.FromTelephoneNo.ToString()).ToArray());
                requestName += string.Join(",", context.ExchangeBrokenPCMs.Where(t => telephoneNos.Contains((long)t.TelephoneNo) && t.Request.EndDate == null && t.Request.IsCancelation == false).Select(t => t.Request.RequestType.Title + ":" + t.TelephoneNo.ToString()).ToArray());
                return requestName;
            }
        }


        public static Request LastInstalledRequestByTelephone(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID == (int)DB.RequestType.E1).OrderByDescending(t => t.EndDate).FirstOrDefault();
            }
        }

        public static E1 LastInstalledE1ByTelephone(long teleponeNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1s.Where(t => t.Request.TelephoneNo == teleponeNo).OrderByDescending(t => t.Request.EndDate).FirstOrDefault();
            }
        }

        public static List<RequestTitleInfo> GetAllRequestInfo(bool isCancelation = false, DB.Action? previousAction = null, List<int> cites = null, List<int> centers = null, DateTime? fromDate = null, DateTime? toDate = null, int? RequestType = null, TimeSpan? maxTime = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                              .Where(t =>
                                          (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                          (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                                          (t.IsWaitingList == false) &&
                                          (isCancelation == false || t.IsCancelation == isCancelation) &&
                                          (previousAction == null || t.PreviousAction == (byte?)previousAction) &&
                                          ((fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) && (toDate.HasValue ? t.InsertDate.Date <= (DateTime?)toDate : true)) &&
                                          (RequestType == null || t.RequestTypeID == RequestType)
                                    )
                    .GroupBy(t => new { RequestTypeID = t.RequestTypeID, StepID = t.Status.RequestStepID, StepTitle = t.Status.RequestStep.StepTitle, RequestTitle = t.RequestType.Title })
                    .Select(t => new RequestTitleInfo
                    {
                        RequestTypeID = t.Key.RequestTypeID,
                        RequestTitle = t.Key.RequestTitle,
                        RequestDetails = new RequestInboxInfo
                        {
                            StepID = t.Key.StepID,
                            StepTitle = t.Key.StepTitle,
                            Count = t.Count(),
                            LastRequestDate = context.Requests.Where(s => (DB.CurrentUser.CenterIDs.Contains(s.CenterID)) && (DB.CurrentUser.RequestStepsIDs.Contains(s.Status.RequestStepID)) && (s.Status.RequestStepID == t.Key.StepID)).Select(s => s.ModifyDate).Max()
                        }
                    }).OrderBy(t => t.RequestTitle).ToList();
            }
        }

        public static List<ReqeustStatusLogDetails> GetReqeustStatusLogDetails(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //var a = context.StatusLogs.Where(t => t.ReqeustID == requestID).OrderByDescending(t => t.ID).Take(1)
                //    .Join(context.Status, sl => sl.FromStatusID, s => s.ID, (sl, s) => new { StatusLog = sl, Status = s })
                //    .Select(t => new ReqeustStatusLogDetails { Description = t.StatusLog.Description, ReasonReject = t.StatusLog.RequestRejectReason.Description, Step = t.Status.RequestStep.StepTitle }).ToList();

                List<ReqeustStatusLogDetails> t1 = context.StatusLogs.Where(t => t.ReqeustID == requestID).OrderBy(t => t.ID)
                    .Join(context.Status, sl => sl.FromStatusID, s => s.ID, (sl, s) => new { StatusLog = sl, Status = s })
                    .Select(t => new ReqeustStatusLogDetails { Description = (t.StatusLog.Description ?? string.Empty + "\n" + t.StatusLog.RequestRejectReason.Description ?? string.Empty), Step = t.Status.RequestStep.StepTitle, ActionID = t.StatusLog.ActionID }).ToList();

                ReqeustStatusLogDetails t2 = context.StatusLogs.Where(t => t.ReqeustID == requestID).OrderByDescending(t => t.ID).Take(1)
                 .Join(context.Status, sl => sl.ToStatusID, s => s.ID, (sl, s) => new { StatusLog = sl, Status = s })
                 .Select(t => new ReqeustStatusLogDetails { Description = (t.StatusLog.Description ?? string.Empty + "\n" + t.StatusLog.RequestRejectReason.Description ?? string.Empty), Step = t.Status.RequestStep.StepTitle, ActionID = t.StatusLog.ActionID }).SingleOrDefault();
                t1.Add(t2);
                return t1;

            }
        }


        public static int ManagementSearchRequestsCount(List<int> cites, List<int> centers, DateTime? fromDate, int? requestType, string ID, string telephoneNo, bool isWaitingList, bool isCancelation)
        {
            DateTime NowDateTime = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                               .Where(t =>
                                          (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                          (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                                          (t.IsWaitingList == isWaitingList) &&
                                          (t.IsCancelation == isCancelation) &&
                                          (t.EndDate == null) &&
                                          (fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) &&
                                          (requestType == null || t.RequestTypeID == requestType) &&
                                          (ID == string.Empty || t.ID.ToString().Contains(ID)) &&
                                          (telephoneNo == string.Empty || t.TelephoneNo.ToString().Contains(telephoneNo))).Count();
            }
        }

        public static long? GetTeleponeNoByRequestID(long? RequestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == RequestID).Select(t => t.TelephoneNo).SingleOrDefault();
            }
        }
        public static List<RequestInfo> ManagementSearchRequests(List<int> cites, List<int> centers, DateTime? fromDate, int? requestType, string ID, string telephoneNo, bool isWaitingList, bool isCancelation, int pageSize = 10, int startRowIndex = 0)
        {
            DateTime NowDateTime = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests
                             .Where(t =>
                                          (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                          (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                                          (t.IsWaitingList == isWaitingList) &&
                                          (t.IsCancelation == isCancelation) &&
                                          (t.EndDate == null) &&
                                          (fromDate.HasValue ? t.InsertDate.Date >= (DateTime?)fromDate : true) &&
                                          (requestType == null || t.RequestTypeID == requestType) &&
                                          (ID == string.Empty || t.ID.ToString().Contains(ID)) &&
                                          (telephoneNo == string.Empty || t.TelephoneNo.ToString().Contains(telephoneNo))
                                    ).Select(t => new RequestInfo
                                                {
                                                    ID = t.ID,
                                                    TelephoneNo = t.TelephoneNo,
                                                    RequestTypeID = t.RequestTypeID,
                                                    RequestTypeName = t.RequestType.Title,
                                                    CenterName = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                                                    CustomerName = t.Customer.FirstNameOrTitle + " " + (t.Customer.LastName ?? ""),
                                                    RequestDate = t.RequestDate.ToPersian(Date.DateStringType.Short),
                                                    RequestLetterNo = t.RequestLetterNo,
                                                    RequestLetterDate = t.RequestLetterDate.ToPersian(Date.DateStringType.Short),
                                                    RequesterName = t.RequesterName,
                                                    InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                                                    CreatorUser = GetUserFullName(t.CreatorUserID),
                                                    ModifyDate = t.ModifyDate.ToPersian(Date.DateStringType.Short),
                                                    ModifyUser = GetUserFullName(t.ModifyUserID),
                                                    StatusName = t.Status.Title,
                                                    CurrentStep = t.Status.RequestStep.StepTitle,
                                                    StatusID = t.StatusID,
                                                    StepID = t.Status.RequestStepID,
                                                    PreviousAction = t.PreviousAction,
                                                    IsViewed = t.IsViewed,
                                                    RequesRejectReason = context.StatusLogs.Where(sl => sl.ToStatusID == t.StatusID && sl.ReqeustID == t.ID).OrderByDescending(sl => sl.Date).Take(1).Select(x => new SubRequesRejectReason { Reason = x.RequestRejectReason.Description, Description = x.Description }).SingleOrDefault(),
                                                }
                            ).Skip(startRowIndex)
                            .Take(pageSize)
                            .ToList();
            }
        }

        public static bool CheckForReuqestChild(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Any(t => t.MainRequestID == id && t.IsCancelation == false);
            }
        }

        public static bool HasTelephoneConnectionInstallment(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool result = false;

                result = context.RequestPayments
                                .Join(context.TelephoneConnectionInstallments, rp => rp.ID, tci => tci.RequestPaymentID, (rp, tci) => new { _RequestPayment = rp, _TelephoneConnectionInstallment = tci })
                                .Where(joinedData => joinedData._RequestPayment.RequestID == requestId)
                                .Any();

                return result;
            }
        }
    }

    public class RequestTitleInfo : INotifyPropertyChanged
    {
        private string _requestTitle;
        private RequestInboxInfo _requestInboxInfo;
        public int RequestTypeID { get; set; }
        public string RequestTitle
        {
            get
            {
                return _requestTitle;
            }
            set
            {
                _requestTitle = value;
                FirePropertyChangedEvent("RequestTitle");
            }
        }
        public RequestInboxInfo RequestDetails
        {
            get
            {
                return _requestInboxInfo;
            }
            set
            {
                _requestInboxInfo = value;
                FirePropertyChangedEvent("RequestTitle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void FirePropertyChangedEvent(string propertyName)
        {

            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));



        }

    }

    public class RequestInboxInfo : RequestTitleInfo
    {
        private int _stepID;
        private string _stepTitle;
        private DateTime? _lastRequestDate;
        private int _count;

        public int StepID
        {
            get { return _stepID; }
            set { _stepID = value; base.FirePropertyChangedEvent("StepID"); }
        }
        public string StepTitle
        {
            get { return _stepTitle; }
            set { _stepTitle = value; FirePropertyChangedEvent("StepTitle"); }
        }
        public DateTime? LastRequestDate
        {
            get { return _lastRequestDate; }
            set { _lastRequestDate = value; FirePropertyChangedEvent("LastRequestDate"); }
        }
        public int Count
        {
            get { return _count; }
            set { _count = value; FirePropertyChangedEvent("Count"); }
        }


    }
}