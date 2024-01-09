using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLPAPRequestDB
    {
        public static List<ADSLPAPRequestInfo> SearchADSLPAPRequests(
            int papId,
            string telephoneNo,
            int centerID,
            List<int> centerIDs,
            int requestType,
            string customerName,
            byte? customerStatus,
            DateTime? fromInsertDate,
            DateTime? toInsertDate,
            byte? instalTimeOut,
            DateTime? fromEndDate,
            DateTime? toEndDate,
            byte status
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests
                    .Where(t => (papId == t.PAPInfoID) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (centerID == 0 || t.Request.CenterID == centerID) &&
                                (centerIDs.Contains(t.Request.Center.ID)) &&
                                (requestType == -1 || t.RequestTypeID == requestType) &&
                                (string.IsNullOrWhiteSpace(customerName) || t.Customer.Contains(customerName)) &&
                                (customerStatus == 0 || t.CustomerStatus == customerStatus) &&
                                (!fromInsertDate.HasValue || t.Request.InsertDate >= fromInsertDate) &&
                                (!toInsertDate.HasValue || t.Request.InsertDate <= toInsertDate) &&
                                (instalTimeOut == 0 || t.InstalTimeOut == instalTimeOut) &&
                                (!fromEndDate.HasValue || t.FinalDate >= fromEndDate) &&
                                (!toEndDate.HasValue || t.FinalDate <= toEndDate) &&
                                (status == 0 || status == t.Status))
                    .OrderByDescending(t => t.ID)
                    .Select(t => new ADSLPAPRequestInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        Center = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                        Customer = t.Customer,
                        CustomerStatus = DB.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (byte)t.CustomerStatus),
                        ADSLPAPPort = getADSLPAPPortbyID(t.ADSLPAPPortID),// (t.ADSLPAPPortID != null) ? t.ADSLPAPPort.RowNo + " ، " + t.ADSLPAPPort.ColumnNo + " ، " + t.ADSLPAPPort.BuchtNo : "",
                        SplitorBucht = t.SplitorBucht,
                        NewPort = t.NewPort,
                        LineBucht = t.LineBucht,
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.Short),
                        InstalTimeOut = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (byte)t.InstalTimeOut),
                        Step = t.Request.Status.RequestStep.StepTitle,
                        EndDate = Date.GetPersianDate(t.Request.EndDate, Date.DateStringType.Short),
                        Comment = t.CommnetReject,
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPRequestStatus), (byte)t.Status)
                    }).ToList();
            }
        }

        private static string getADSLPAPPortbyID(long? id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.ID == id).SingleOrDefault();

                if (port != null)
                    return port.RowNo + " ، " + port.ColumnNo + " ، " + port.BuchtNo;
                else
                    return "";
            }
        }

        public static List<ADSLPAPRequestInfo> SearchADSLPAPRequestsforHistory(
            List<int> papIDs,
            string telephoneNo,
            List<int> centerIDs,
            List<int> requestTypeIDs,
            int rowNo,
            int columnNo,
            int buchtNo,
            List<int> statusIDs,
            DateTime? fromEndDate,
            DateTime? toEndDate,
            int startRowIndex,
            int pageSize
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests
                    .Where(t => (papIDs.Count == 0 || papIDs.Contains(t.PAPInfoID)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.Request.Center.ID)) &&
                                (requestTypeIDs.Count == 0 || requestTypeIDs.Contains((int)t.RequestTypeID)) &&
                                (rowNo == -1 || t.ADSLPAPPort.RowNo == rowNo) &&
                                (columnNo == -1 || t.ADSLPAPPort.ColumnNo == columnNo) &&
                                (buchtNo == -1 || t.ADSLPAPPort.BuchtNo == buchtNo) &&
                                (statusIDs.Count == 0 || statusIDs.Contains((int)t.Status)) &&
                                (!fromEndDate.HasValue || t.Request.EndDate >= fromEndDate) &&
                                (!toEndDate.HasValue || t.Request.EndDate <= toEndDate))
                    .OrderByDescending(t => t.ID)
                    .Select(t => new ADSLPAPRequestInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        Center = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                        Customer = t.Customer,
                        PAPName = t.PAPInfo.Title,
                        RequestType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPRequestType), t.RequestTypeID),
                        ADSLPAPPort = (t.ADSLPAPPortID != null) ? t.ADSLPAPPort.RowNo.ToString() + " ، " + t.ADSLPAPPort.ColumnNo.ToString() + " ، " + t.ADSLPAPPort.BuchtNo.ToString() : "",
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.Request.EndDate, Date.DateStringType.Short),
                        Step = t.Request.Status.RequestStep.StepTitle,
                        Comment = (t.CommnetReject != null) ? t.CommnetReject : "",
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPRequestStatus), (byte)t.Status)
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchADSLPAPRequestsforHistoryCount(
            List<int> papIDs,
            string telephoneNo,
            List<int> centerIDs,
            List<int> requestTypeIDs,
            int rowNo,
            int columnNo,
            int buchtNo,
            List<int> statusIDs,
            DateTime? fromEndDate,
            DateTime? toEndDate
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests
                    .Where(t => (papIDs.Count == 0 || papIDs.Contains(t.PAPInfoID)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.Request.Center.ID)) &&
                                (requestTypeIDs.Count == 0 || requestTypeIDs.Contains((int)t.RequestTypeID)) &&
                                (rowNo == -1 || t.ADSLPAPPort.RowNo == rowNo) &&
                                (columnNo == -1 || t.ADSLPAPPort.ColumnNo == columnNo) &&
                                (buchtNo == -1 || t.ADSLPAPPort.BuchtNo == buchtNo) &&
                                (statusIDs.Count == 0 || statusIDs.Contains((int)t.Status)) &&
                                (!fromEndDate.HasValue || t.Request.EndDate >= fromEndDate) &&
                                (!toEndDate.HasValue || t.Request.EndDate <= toEndDate))
                    .Count();
            }
        }

        public static List<ADSLPAPRequestInfo> SearchADSLPAPRequestbytelephoneNoforWeb(
            int papId,
            string telephoneNo,
            int? centerID,
            List<int> centerIDs,
            int? requestType,
            DateTime? fromInsertDate,
            DateTime? toInsertDate,
            DateTime? fromEndDate,
            DateTime? toEndDate
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests
                    .Where(t => (papId == t.PAPInfoID) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (centerID == 0 || t.Request.CenterID == centerID) &&
                                (centerIDs.Contains(t.Request.Center.ID)) &&
                                (requestType == -1 || t.RequestTypeID == requestType) &&
                                (!fromInsertDate.HasValue || t.Request.InsertDate >= fromInsertDate) &&
                                (!toInsertDate.HasValue || t.Request.InsertDate <= toInsertDate) &&
                                (!fromEndDate.HasValue || t.FinalDate >= fromEndDate) &&
                                (!toEndDate.HasValue || t.FinalDate <= toEndDate))
                    .OrderBy(t => t.ID)
                    .Select(t => new ADSLPAPRequestInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        Center = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                        Customer = t.Customer,
                        RequestType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPRequestType), t.RequestTypeID),
                        ADSLPAPPort = (t.ADSLPAPPortID != null) ? t.ADSLPAPPort.RowNo.ToString() + " ، " + t.ADSLPAPPort.ColumnNo.ToString() + " ، " + t.ADSLPAPPort.BuchtNo.ToString() : "",
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.Request.EndDate, Date.DateStringType.Short),
                        Step = t.Request.Status.RequestStep.StepTitle,
                        Comment = (t.CommnetReject != null) ? t.CommnetReject : "",
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPRequestStatus), (byte)t.Status)
                    }).ToList();
            }
        }

        //private static string GetPAPRequestStatus(long requestId)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Requests.Where(t => t.ID == requestId).SingleOrDefault().Status.RequestStep.StepTitle;
        //        //int statusID = context.Requests.Where(t => t.ID == requestId).SingleOrDefault().StatusID;
        //        //int requestStepID = context.Status.Where(t => t.ID == statusID).SingleOrDefault().RequestStepID;
        //        //return context.RequestSteps.Where(t => t.ID == requestStepID).SingleOrDefault().StepTitle;
        //    }
        //}

        public static List<RequestStep> GetRequestSteps(int requestType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestSteps.Where(t => t.RequestTypeID == requestType).ToList();
            }
        }

        public static ADSLPAPRequestInfo GetADSLPAPRequestInfo(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests
                    .Where(t => t.ID == requestId)
                    .Select(t => new ADSLPAPRequestInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        PAPName = t.PAPInfo.Title,
                        Customer = t.Customer,
                        CustomerStatus = DB.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)t.CustomerStatus),
                        SplitorBucht = t.SplitorBucht,
                        LineBucht = t.LineBucht,
                        InstalTimeOut = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (int)t.InstalTimeOut),
                        CreatorUser = GetUserFullName(t.Request.CreatorUserID),
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                        Center = t.Request.Center.CenterName,
                        RequestDate = Date.GetPersianDate(t.Request.RequestDate, Date.DateStringType.Short),
                        CommentCustomers = t.CommentCustomers,
                        CommnetReject = t.CommnetReject,
                        MDFUser = GetUserFullName(t.MDFUserID),
                        MDFDate = Date.GetPersianDate(t.MDFDate, Date.DateStringType.DateTime),
                        MDFComment = t.MDFComment,
                        FinalUser = GetUserFullName(t.FinalUserID),
                        FinalDate = Date.GetPersianDate(t.Request.EndDate, Date.DateStringType.DateTime),
                        FinalComment = t.FinalComment,
                    })
                    .SingleOrDefault();
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

        public static int GetPAPRequestNo(int papUserID, byte requestTypeID)//(int papID, int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //return context.ADSLPAPRequests.Where(t => t.PAPInfoID == papID && t.Request.Center.Region.CityID == cityID && t.Request.InsertDate.Date == DB.GetServerDate().Date && t.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany).ToList().Count;
                return context.Requests.Where(t => t.CreatorUserID == papUserID && t.InsertDate.Date == DB.GetServerDate().Date && t.RequestTypeID == requestTypeID).ToList().Count;
            }
        }

        public static ADSLPAPRequest GetADSLPAPRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static PAPRequestPrintInfo GetPAPRequestPrintbyTelephoneNos(long telephoneNo, int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID == requestTypeID & t.Request.EndDate == null)
                    .Select(t => new PAPRequestPrintInfo
                    {
                        ID = t.ID.ToString(),
                        RequestType = t.Request.RequestType.Title,
                        TelephoneNo = t.TelephoneNo.ToString(),
                        PAPInfo = t.PAPInfo.Title,
                        RowNo = t.ADSLPAPPort.RowNo.ToString(),
                        ColumnNo = t.ADSLPAPPort.ColumnNo.ToString(),
                        BuchtNo = t.ADSLPAPPort.BuchtNo.ToString(),
                        BuchtADSL = (t.ADSLPAPPortID != null) ? "ردیف: " + t.ADSLPAPPort.RowNo.ToString() + " ، طبقه: " + t.ADSLPAPPort.ColumnNo.ToString() + " ، اتصالی: " + t.ADSLPAPPort.BuchtNo.ToString() : " - ",
                        OldBuchtADSL = GetOldADSLBucht(t.TelephoneNo),
                        Date = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.Short),
                        Center = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                    }).SingleOrDefault();
            }
        }

        public static string GetOldADSLBucht(long telephoneNo)
        {
            ADSLPAPPort port = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo(telephoneNo);

            if (port != null)
                return "ردیف: " + port.RowNo.ToString() + " ، طبقه: " + port.ColumnNo.ToString() + " ، اتصالی: " + port.BuchtNo.ToString();
            else
                return " - ";
        }

        public static string GetTechnicalInfobyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = 0;
                if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault() != null)
                {
                    if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID != null)
                        switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                    if (switchPortID != 0)
                    {
                        return context.Buchts.Where(t => t.SwitchPortID == switchPortID)
                                           .Select(t => "ردیف: " + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه: " + t.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی: " + t.BuchtNo.ToString()).SingleOrDefault();
                    }
                    else
                        return "-";
                }
                else
                    return "-";
            }
        }

        public static string GetPAPInstallMontlyInfoReport(List<int> centerIDs, int papID, DateTime? fromDate, DateTime? toDate, int requestTypeID, int status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                          (t.Request.EndDate != null) &&
                                                          (!fromDate.HasValue || t.Request.EndDate >= fromDate) &&
                                                          (!toDate.HasValue || t.Request.EndDate <= toDate) &&
                                                          (t.PAPInfoID == papID) &&
                                                          (t.RequestTypeID == requestTypeID) &&
                                                          (t.Status == status)).Count().ToString();
            }
        }

        public static string GetPAPInstallMontlyInfoReportbyPAPIDs(List<int> centerIDs, List<int> papIDs, DateTime? fromDate, DateTime? toDate, int requestTypeID, int status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                          (t.Request.EndDate != null) &&
                                                          (!fromDate.HasValue || t.Request.EndDate >= fromDate) &&
                                                          (!toDate.HasValue || t.Request.EndDate <= toDate) &&
                                                          (papIDs.Count == 0 || papIDs.Contains(t.PAPInfoID)) &&
                                                          (t.RequestTypeID == requestTypeID) &&
                                                          (t.Status == status)).Count().ToString();
            }
        }

        public static string GetPAPBusyInfoReport(List<int> centerIDs, int papID, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int portCount = context.ADSLPAPPorts.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                                (t.PAPInfoID == papID) &&
                                                                (!toDate.HasValue || t.InstallDate <= toDate)).ToList().Count();

                int dischargeCount = context.ADSLPAPRequests.Where(t => (t.RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany) &&
                                                                        (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                                        (t.PAPInfoID == papID) &&
                                                                        (!fromDate.HasValue || t.Request.EndDate >= fromDate) &&
                                                                        (!toDate.HasValue || t.Request.EndDate <= toDate)).ToList().Count();
                return (portCount + dischargeCount).ToString();

            }
        }

        public static string GetPAPAvailableInfoReport(List<int> centerIDs, int papID, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                       (t.PAPInfoID == papID)).Count().ToString();
            }
        }

        public static string GetPAPBusyInfoReportbyPAPIDs(List<int> centerIDs, List<int> papIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int portCount = context.ADSLPAPPorts.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                                (papIDs.Count == 0 || papIDs.Contains(t.PAPInfoID)) &&
                                                                (!toDate.HasValue || t.InstallDate <= toDate)).ToList().Count();

                int dischargeCount = context.ADSLPAPRequests.Where(t => (t.RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany) &&
                                                                        (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                                        (papIDs.Count == 0 || papIDs.Contains(t.PAPInfoID)) &&
                                                                        (!fromDate.HasValue || t.Request.EndDate >= fromDate) &&
                                                                        (!toDate.HasValue || t.Request.EndDate <= toDate)).ToList().Count();

                return (portCount + dischargeCount).ToString();

            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> GetPAPBillingInfoList(DateTime fromDate, DateTime toDate, byte status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests.Where(t => (t.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany) &&
                                                          (t.Request.EndDate >= fromDate) &&
                                                          (t.Request.EndDate <= toDate) &&
                                                          (t.Status == status))
                                            .GroupBy(t => new { papName = t.PAPInfo.Title })
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo
                                            {
                                                PAPCode = "",
                                                PAPName = t.Key.papName,
                                                Count24 = t.Count(x => ((DateTime.Compare(new DateTime(x.Request.EndDate.Value.Year, x.Request.EndDate.Value.Month, x.Request.EndDate.Value.Day, 0, 0, 0), new DateTime(x.Request.InsertDate.Year, x.Request.InsertDate.Month, x.Request.InsertDate.Day, 0, 0, 0)) * 24) + ((x.Request.EndDate.Value.Hour >= x.Request.InsertDate.Hour) ? (x.Request.EndDate.Value.Hour - x.Request.InsertDate.Hour) : (24 - x.Request.InsertDate.Hour + x.Request.EndDate.Value.Hour))) <= 24).ToString(),

                                                Count48 = t.Count(x => (((DateTime.Compare(new DateTime(x.Request.EndDate.Value.Year, x.Request.EndDate.Value.Month, x.Request.EndDate.Value.Day, 0, 0, 0), new DateTime(x.Request.InsertDate.Year, x.Request.InsertDate.Month, x.Request.InsertDate.Day, 0, 0, 0)) * 24) + ((x.Request.EndDate.Value.Hour >= x.Request.InsertDate.Hour) ? (x.Request.EndDate.Value.Hour - x.Request.InsertDate.Hour) : (24 - x.Request.InsertDate.Hour + x.Request.EndDate.Value.Hour))) > 24) &&
                                                                       (((DateTime.Compare(new DateTime(x.Request.EndDate.Value.Year, x.Request.EndDate.Value.Month, x.Request.EndDate.Value.Day, 0, 0, 0), new DateTime(x.Request.InsertDate.Year, x.Request.InsertDate.Month, x.Request.InsertDate.Day, 0, 0, 0)) * 24) + ((x.Request.EndDate.Value.Hour >= x.Request.InsertDate.Hour) ? (x.Request.EndDate.Value.Hour - x.Request.InsertDate.Hour) : (24 - x.Request.InsertDate.Hour + x.Request.EndDate.Value.Hour))) <= 48)).ToString(),

                                                Count72 = t.Count(x => (((DateTime.Compare(new DateTime(x.Request.EndDate.Value.Year, x.Request.EndDate.Value.Month, x.Request.EndDate.Value.Day, 0, 0, 0), new DateTime(x.Request.InsertDate.Year, x.Request.InsertDate.Month, x.Request.InsertDate.Day, 0, 0, 0)) * 24) + ((x.Request.EndDate.Value.Hour >= x.Request.InsertDate.Hour) ? (x.Request.EndDate.Value.Hour - x.Request.InsertDate.Hour) : (24 - x.Request.InsertDate.Hour + x.Request.EndDate.Value.Hour))) > 48) &&
                                                                       (((DateTime.Compare(new DateTime(x.Request.EndDate.Value.Year, x.Request.EndDate.Value.Month, x.Request.EndDate.Value.Day, 0, 0, 0), new DateTime(x.Request.InsertDate.Year, x.Request.InsertDate.Month, x.Request.InsertDate.Day, 0, 0, 0)) * 24) + ((x.Request.EndDate.Value.Hour >= x.Request.InsertDate.Hour) ? (x.Request.EndDate.Value.Hour - x.Request.InsertDate.Hour) : (24 - x.Request.InsertDate.Hour + x.Request.EndDate.Value.Hour))) <= 72)).ToString(),

                                                CountAll = t.Count().ToString()
                                            }).ToList();

            }
        }

        public static List<PAPTotalReportInfo> GetADSLRequestTotalReport(List<int> centerIDs, List<int> papIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime dateThisYear = Date.PersianToGregorian(1, 1, Convert.ToInt32(Date.GetPersianDate(DateTime.Now, Date.DateStringType.Year)));

                return context.ADSLPAPRequests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                          (t.Request.EndDate != null) &&
                                                          (!fromDate.HasValue || t.Request.EndDate >= fromDate) &&
                                                          (!toDate.HasValue || t.Request.EndDate <= toDate) &&
                                                          (papIDs.Count == 0 || papIDs.Contains(t.PAPInfoID)))
                                               .GroupBy(t => new { PAPID = t.PAPInfoID, CenterID = t.Request.CenterID })
                                               .Select(t => new PAPTotalReportInfo
                                               {
                                                   InstalCompleted = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed).ToString(),
                                                   DischargeCompleted = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed).ToString(),
                                                   ExchangeCompleted = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLExchangePAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed).ToString(),
                                                   Center = CenterDB.GetCenterFullName(t.Key.CenterID),
                                                   PAP = PAPInfoDB.GetPAPInfoName(t.Key.PAPID),
                                                   CenterID = t.Key.CenterID,
                                                   PAPID = t.Key.PAPID,                                                   
                                                   InstalCompletedThisYear = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed && x.EndDate >= dateThisYear).ToString(),
                                                   DischargeCompletedThisYear = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed && x.EndDate >= dateThisYear).ToString(),
                                                   ExchangeCompletedThisYear = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLExchangePAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed && x.EndDate >= dateThisYear).ToString(),
                                                   InstalRejected = "",
                                                   DischargeRejected = "",
                                                   ExchangeRejected = ""
                                               }).ToList();

            }
        }

        public static PAPTotalReportInfo GetADSLRequestTotalReportThisYear(int centerID, int papID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime dateThisYear = Date.PersianToGregorian(1, 1, Convert.ToInt32(Date.GetPersianDate(DateTime.Now, Date.DateStringType.Year)));

                return context.ADSLPAPRequests.Where(t => (centerID== 0 || centerID==t.Request.CenterID) &&
                                                          (t.Request.EndDate != null) &&                                                          
                                                          (papID == 0 || papID==t.PAPInfoID))
                                               .GroupBy(t => new { PAPID = t.PAPInfoID, CenterID = t.Request.CenterID })
                                               .Select(t => new PAPTotalReportInfo
                                               {
                                                   InstalCompletedThisYear = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed && x.Request.EndDate >= dateThisYear).ToString(),
                                                   DischargeCompletedThisYear = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed && x.Request.EndDate >= dateThisYear).ToString(),
                                                   ExchangeCompletedThisYear = t.Count(x => x.RequestTypeID == (byte)DB.RequestType.ADSLExchangePAPCompany && x.Status == (byte)DB.ADSLPAPRequestStatus.Completed && x.Request.EndDate >= dateThisYear).ToString()                                                                                                     
                                               }).SingleOrDefault();

            }
        }

        public static string GetADSLPAPPortsReport(int centerID, int papID, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts.Where(t => t.CenterID == centerID && t.PAPInfoID == papID).ToList().Count.ToString();

            }
        }

        public static string GetADSLPAPPortBusyReport(int centerID, int papID, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts.Where(t => (t.CenterID == centerID && t.PAPInfoID == papID && t.Status == (byte)DB.ADSLPAPPortStatus.Instal) &&
                                                       (!toDate.HasValue || t.InstallDate <= toDate)).ToList().Count.ToString();

            }
        }

        public static ADSLPAPRequest GetPAPRequestbyRequestIDandPAPID(long requestID, int papID)
        {
            using (MainDataContext context = new MainDataContext())
            {
              ADSLPAPRequest request= context.ADSLPAPRequests.Where(t => t.ID == requestID && t.PAPInfoID == papID).SingleOrDefault();

              if (request != null)
                  return request;
              else
                  return null;
            }
        }
    }
}