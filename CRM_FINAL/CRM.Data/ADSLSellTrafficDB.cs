using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Data.Services;
using CookComputing.XmlRpc;

namespace CRM.Data
{
    public class ADSLSellTrafficDB
    {
        public static ADSLSellTraffic GetADSLSellTrafficById(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellTraffics.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<ADSLSellTrafficRequestInfo> SearchADSLChangeServiceInfo(string requestID, string telephoneNo, bool? isPaid)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellTraffics
                    .Join(context.RequestPayments, c => c.ID, t => t.RequestID, (c, t) => new { SellTraffic = c, RequestPayment = t })
                    .Where(t => (t.SellTraffic.ChangeServiceType == (byte)DB.ADSLChangeServiceType.Internet) &&
                                (string.IsNullOrWhiteSpace(requestID) || t.SellTraffic.ID.ToString().Contains(requestID)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.SellTraffic.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (!isPaid.HasValue || isPaid == t.RequestPayment.IsPaid))
                    .Select(t => new ADSLSellTrafficRequestInfo
                    {
                        RequestID = t.SellTraffic.ID,
                        TelephoneNo = t.SellTraffic.Request.TelephoneNo.ToString(),
                        CustomerName = t.SellTraffic.Request.Customer.FirstNameOrTitle + " " + t.SellTraffic.Request.Customer.LastName,
                        TrafficTitle = t.SellTraffic.ADSLService.Title,
                        InserDate = Date.GetPersianDate(t.SellTraffic.Request.InsertDate, Date.DateStringType.DateTime),
                        BillID = t.RequestPayment.BillID,
                        PaymentID = t.RequestPayment.PaymentID,
                        BankName = t.RequestPayment.Bank.BankName,
                        FicheNumber = t.RequestPayment.FicheNunmber,
                        PaymentDate = Date.GetPersianDate(t.RequestPayment.PaymentDate, Date.DateStringType.Short),
                        IsPaid = t.RequestPayment.IsPaid
                    }).ToList();
            }
        }

        public static List<ADSLSellTrafficRequestInfo> GetADSLSellTrafficInfo(string telephoneNo, DateTime startDate, DateTime endDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellTraffics
                    .Join(context.RequestPayments, c => c.ID, t => t.RequestID, (c, t) => new { SellTraffic = c, RequestPayment = t })
                    .Where(t => (string.IsNullOrWhiteSpace(telephoneNo) || t.SellTraffic.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (t.SellTraffic.Request.InsertDate > startDate && t.SellTraffic.Request.InsertDate < endDate))
                    .Select(t => new ADSLSellTrafficRequestInfo
                    {
                        RequestID = t.SellTraffic.ID,
                        TypeID = DB.GetEnumDescriptionByValue(typeof(DB.ADSLChangeServiceType), t.SellTraffic.ChangeServiceType),
                        TrafficTitle = t.SellTraffic.ADSLService.Title,
                        BillID = t.RequestPayment.BillID,
                        PaymentID = t.RequestPayment.PaymentID,
                        PaymentDate = Date.GetPersianDate(t.RequestPayment.PaymentDate, Date.DateStringType.Short),
                        ModifyUser = GetUserFullName(t.RequestPayment.Request.ModifyUserID),
                        IsPaid = t.RequestPayment.IsPaid,
                        IBSngStatus = GetIBSngStatus(t.SellTraffic.IsIBSngUpdated)
                    }).OrderBy(t => t.RequestID).ToList();
            }
        }

        private static int GetIBSngStatus(bool? isIBSngUpdated)
        {
            int ibsStatus = 0;

            if (isIBSngUpdated != null)
            {
                if (isIBSngUpdated == true)
                    ibsStatus = 1;
                else
                    ibsStatus = 2;
            }
            else
                ibsStatus = 0;

            return ibsStatus;
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

        public static WirelessSellTraffic GetWirelessSellTrafficById(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WirelessSellTraffics.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<ADSLSellTrafficRequestInfo> GetWirelessSellTrafficInfo(string telephoneNo, DateTime startDate, DateTime endDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WirelessSellTraffics
                    .Join(context.RequestPayments, c => c.ID, t => t.RequestID, (c, t) => new { SellTraffic = c, RequestPayment = t })
                    .Where(t => (string.IsNullOrWhiteSpace(telephoneNo) || t.SellTraffic.WirelessCode.Contains(telephoneNo)) &&
                                (t.SellTraffic.Request.InsertDate > startDate && t.SellTraffic.Request.InsertDate < endDate))
                    .Select(t => new ADSLSellTrafficRequestInfo
                    {
                        RequestID = t.SellTraffic.ID,
                        TypeID = DB.GetEnumDescriptionByValue(typeof(DB.ADSLChangeServiceType), t.SellTraffic.ChangeServiceType),
                        TrafficTitle = t.SellTraffic.ADSLService.Title,
                        BillID = t.RequestPayment.BillID,
                        PaymentID = t.RequestPayment.PaymentID,
                        PaymentDate = Date.GetPersianDate(t.RequestPayment.PaymentDate, Date.DateStringType.Short),
                        ModifyUser = GetUserFullName(t.RequestPayment.Request.ModifyUserID),
                        IsPaid = t.RequestPayment.IsPaid,
                        IBSngStatus = GetIBSngStatus(t.SellTraffic.IsIBSngUpdated)
                    }).OrderBy(t => t.RequestID).ToList();
            }
        }
    }
}
