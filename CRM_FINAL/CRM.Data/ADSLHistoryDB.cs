using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace CRM.Data
{
    public static class ADSLHistoryDB
    {
        public static List<ADSLHistoryInfo> SearchCustomersHistory(
             List<int> Services,
             string telephoneNo,
             DateTime? StartDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLHistories.Where(t => (Services.Count == 0 || (Services.Contains((int)t.ServiceID))) &&
                    ((!StartDate.HasValue) || (t.InsertDate == StartDate)) &&
                     (string.IsNullOrWhiteSpace(telephoneNo) || telephoneNo.Contains(t.TelephoneNo))).Select
                     (t => new ADSLHistoryInfo
                     {
                         TelephoneNo = t.TelephoneNo,
                         ServiceID = (int)t.ServiceID,
                         InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime)
                     }
                     ).ToList();
            }
        }

        public static List<ADSLHistoryInfo> SearchAll()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLHistories
                    .Select
                     (t => new ADSLHistoryInfo
                     {
                         TelephoneNo = t.TelephoneNo,
                         ServiceID = (int)t.ServiceID,
                         InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime)
                     }
                     ).ToList();
            }
        }

        public static List<ADSLHistoryInfo> GetADSLHistorybyTelephoneNo(string telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLHistories.Where(t => t.TelephoneNo == telephoneNo)
                                            .Select(t => new ADSLHistoryInfo
                                            {
                                                ID = t.ID,
                                                ServiceID = (int)t.ServiceID,
                                                ServiceTitle = t.ADSLService.Title,
                                                ServiceType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), t.ADSLService.TypeID),
                                                ServicePrice = t.ADSLService.PriceSum.ToString() + "ریا ل",
                                                ServiceGroup = t.ADSLService.ADSLServiceGroup.Title,
                                                User = t.User.FirstName + " " + t.User.LastName,
                                                InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.Short)
                                            })//.OrderByDescending(t=>t.PersianInsertDate)
                                            .ToList();
            }
        }

        public static List<RequestInfo> GetADSLHistoryRequest(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID != 38 && t.RequestTypeID != 83)
                    .Select(t => new RequestInfo
                    {
                        ID = t.ID,
                        RequestTypeName = t.RequestType.Title,
                        CurrentStep = t.Status.RequestStep.StepTitle,
                        InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                        ModifyUser = GetUserFullName(t.ModifyUserID),
                        IsCanceled = t.IsCancelation,
                        IsWatting = t.IsWaitingList
                    }).OrderBy(t => t.ID).ToList();
            }
        }

        public static List<RequestInfo> GetADSLHistoryRequestbyDate(long telephoneNo, DateTime startDate, DateTime endDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID != 38 && t.RequestTypeID != 83 && (t.InsertDate > startDate && t.InsertDate < endDate))
                    .Select(t => new RequestInfo
                    {
                        ID = t.ID,
                        RequestTypeName = t.RequestType.Title,
                        CurrentStep = t.Status.RequestStep.StepTitle,
                        InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                        ModifyUser = GetUserFullName(t.ModifyUserID),
                        IsCanceled = t.IsCancelation,
                        IsWatting = t.IsWaitingList
                    }).OrderBy(t => t.ID).ToList();
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
    }
}
