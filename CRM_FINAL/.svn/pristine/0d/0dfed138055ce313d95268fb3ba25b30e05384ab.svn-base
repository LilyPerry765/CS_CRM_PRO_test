using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;

namespace CRM.Data
{
    public static class ADSLChangeTariffDB
    {
        public static ADSLChangeServiceFullViewInfo GetADSLChangeServiceInfo(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangeServices
                    .Where(t => t.ID == requestID)
                    .Select(t => new ADSLChangeServiceFullViewInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.Request.TelephoneNo,
                        //CustomerNationalCode = t.Request.Telephone.Customer.NationalCodeOrRecordNo,
                        //CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                        //CustomerStatus = DB.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)t.Telephone.ADSL.CustomerOwnerStatus),
                        //PostalCode = t.Telephone.Address.PostalCode,
                        //Address = t.Telephone.Address.AddressContent,
                        //ServiceType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), (int)t.Telephone.ADSL.ServiceType),
                        //ADSLRegistrationProjectType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLRegistrationProjectType), (int)t.Telephone.ADSL.RegistrationProjectType),
                        OldTariffTitle = t.ADSLService1.Title,
                        OldBandWidth = t.ADSLService1.BandWidthID.ToString() + "کیلو بایت",
                        //OldLicenseLetterNo = t.Telephone.ADSL.LicenseLetterNo,
                        OldTrafficLimitation = t.ADSLService1.TrafficID.ToString() + "گیگا بایت",
                        OldDuration = t.ADSLService1.DurationID.ToString() + "ماه",
                        OldPrice = t.ADSLService1.Price.ToString(),
                        NewTariffTitle = t.ADSLService.Title,
                        NewBandWidth = t.ADSLService.BandWidthID.ToString() + "کیلو بایت",
                        NewLicenseLetterNo = t.LicenseLetterNo,
                        NewTrafficLimitation = t.ADSLService.TrafficID.ToString() + "گیگا بایت",
                        NewDuration = t.ADSLService.DurationID.ToString() + "ماه",
                        NewPrice = t.ADSLService.Price.ToString(),
                        CreatorUser = GetUserFullName(t.Request.CreatorUserID),
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                        Center = t.Request.Center.CenterName,
                        RequestDate = Date.GetPersianDate(t.Request.RequestDate, Date.DateStringType.Short),
                        CommentCustomers = t.CommentCustomers,
                        OMCUser = GetUserFullName(t.OMCUserID),
                        OMCDate = Date.GetPersianDate(t.OMCDate, Date.DateStringType.DateTime),
                        OMCCommnet = t.OMCComment,
                        FinalUser = GetUserFullName(t.FinalUserID),
                        FinalDate = Date.GetPersianDate(t.FinalDate, Date.DateStringType.DateTime),
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

        public static ADSLChangeService GetADSLChangeServicebyID(long requeatID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangeServices.Where(t => t.ID == requeatID).SingleOrDefault();
            }
        }

        public static List<ADSLChangeServiceRequestInfo> SearchADSLChangeServiceInfo(string requestID, string telephoneNo, bool? isPaid)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangeServices
                    .Join(context.RequestPayments, c => c.ID, t => t.RequestID, (c, t) => new { ChangeService = c, RequestPayment = t })
                    .Where(t => (t.ChangeService.ChangeServiceActionType == (byte)DB.ADSLChangeServiceType.Internet) &&
                                (string.IsNullOrWhiteSpace(requestID) || t.ChangeService.ID.ToString().Contains(requestID)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.ChangeService.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (!isPaid.HasValue || isPaid == t.RequestPayment.IsPaid))
                    .Select(t => new ADSLChangeServiceRequestInfo
                    {
                        RequestID = t.ChangeService.ID,
                        TelephoneNo = t.ChangeService.Request.TelephoneNo.ToString(),
                        CustomerName = t.ChangeService.Request.Customer.FirstNameOrTitle + " " + t.ChangeService.Request.Customer.LastName,
                        ServiceTitle = t.ChangeService.ADSLService.Title,
                        InserDate = Date.GetPersianDate(t.ChangeService.Request.InsertDate, Date.DateStringType.DateTime),
                        BillID = t.RequestPayment.BillID,
                        PaymentID = t.RequestPayment.PaymentID,
                        BankName = t.RequestPayment.Bank.BankName,
                        FicheNumber = t.RequestPayment.FicheNunmber,
                        PaymentDate = Date.GetPersianDate(t.RequestPayment.PaymentDate, Date.DateStringType.Short),
                        IsPaid = t.RequestPayment.IsPaid
                    }).ToList();
            }
        }

        public static List<ADSLChangeServiceRequestInfo> GetADSLChangeServiceInfo(string telephoneNo, DateTime startDate, DateTime endDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangeServices
                    .Join(context.RequestPayments, c => c.ID, t => t.RequestID, (c, t) => new { ChangeService = c, RequestPayment = t })
                    .Where(t => (string.IsNullOrWhiteSpace(telephoneNo) || t.ChangeService.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (t.ChangeService.Request.InsertDate > startDate && t.ChangeService.Request.InsertDate < endDate))
                    .Select(t => new ADSLChangeServiceRequestInfo
                    {
                        RequestID = t.ChangeService.ID,
                        TypeID = DB.GetEnumDescriptionByValue(typeof(DB.ADSLChangeServiceType), t.ChangeService.ChangeServiceType),
                        ServiceTitle = t.ChangeService.ADSLService.Title,
                        BillID = t.RequestPayment.BillID,
                        PaymentID = t.RequestPayment.PaymentID,
                        PaymentDate = Date.GetPersianDate(t.RequestPayment.PaymentDate, Date.DateStringType.Short),
                        ModifyUser = GetUserFullName(t.RequestPayment.Request.ModifyUserID),
                        IsPaid = t.RequestPayment.IsPaid,
                        IBSngStatus =GetIBSngStatus(t.ChangeService.IsIBSngUpdated)
                    }).OrderBy(t => t.RequestID).ToList();
            }
        }

        private static int GetIBSngStatus(bool? isIBSngUpdated)
        {
            int ibsStatus=0;

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

        private static string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }
    }
}
