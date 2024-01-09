using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLRequestDB
    {
        public static ADSLRequestFullViewInfo GetADSLRequestInfo(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLRequests
                    .Where(t => t.ID == requestId)
                    .Select(t => new ADSLRequestFullViewInfo
                    {
                        ID = t.ID,
                        TelephoneNo = (long)t.Request.TelephoneNo,
                        CustomerNationalCode = t.Customer.NationalCodeOrRecordNo,
                        CustomerName = t.Customer.FirstNameOrTitle + " " + t.Customer.LastName,
                        CustomerStatus = DB.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)t.CustomerOwnerStatus),
                        //PostalCode = t.Telephone.Address.PostalCode,
                        //Address = t.Telephone.Address.AddressContent,
                        CustomerPriority = DB.GetEnumDescriptionByValue(typeof(DB.ADSLCustomerPriority), (int)t.CustomerPriority),
                        ADSLRegistrationProjectType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLRegistrationProjectType), (int)t.RegistrationProjectType),
                        ISRequiredInstalation = t.RequiredInstalation,
                        ISNeedModem = t.NeedModem,
                        ServiceTitle = t.ADSLService.Title,
                        BandWidth = t.ADSLService.BandWidthID.ToString() + "کیلو بایت",
                        LicenseLetterNo = t.LicenseLetterNo,
                        TrafficLimitation = t.ADSLService.TrafficID.ToString() + "گیگا بایت",
                        Duration = t.ADSLService.DurationID.ToString() + "ماه",
                        Price = t.ADSLService.Price.ToString(),
                        CreatorUser = GetUserFullName(t.Request.CreatorUserID),
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                        Center = t.Request.Center.CenterName,
                        RequestDate = Date.GetPersianDate(t.Request.RequestDate, Date.DateStringType.Short),
                        CommentCustomers = t.CommentCustomers,
                        EquipmentTitle = t.ADSLPort.ADSLEquipment.Equipment,
                        PortNo = t.ADSLPort.PortNo,
                        BuchtSpliter = DB.GetConnectionByBuchtID(t.ADSLPort.InputBucht),
                        BuchtLine = DB.GetConnectionByBuchtID(t.ADSLPort.OutBucht),
                        AssignmentLineUser = GetUserFullName(t.AssignmentLineUserID),
                        AssignmentLineDate = Date.GetPersianDate(t.AssignmentLineDate, Date.DateStringType.DateTime),
                        AssignmentLineCommnet = t.AssignmentLineCommnet,
                        MDFUser = GetUserFullName(t.MDFUserID),
                        MDFDate = Date.GetPersianDate(t.MDFDate, Date.DateStringType.DateTime),
                        MDFCommnet = t.MDFCommnet,
                        OMCUser = GetUserFullName(t.OMCUserID),
                        OMCDate = Date.GetPersianDate(t.OMCDate, Date.DateStringType.DateTime),
                        OMCCommnet = t.OMCCommnet,
                        Contractor = t.Contractor.Title,
                        //ModemModel = t.ADSLModem.Title,
                        SetupUser = GetUserFullName(t.SetupUserID),
                        SetupDate = Date.GetPersianDate(t.SetupDate, Date.DateStringType.DateTime),
                        SetupComment = t.SetupCommnet,
                        InstallDate = Date.GetPersianDate(t.InstallDate, Date.DateStringType.DateTime)
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

        public static ADSLRequest GetADSLRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLRequests.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static ADSLRequestInfo GetADSLRequestInfoByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLRequests.Where(t => t.Request.TelephoneNo == telephoneNo)
                                            .Select(t => new ADSLRequestInfo
                                            {
                                                ID = t.ID,
                                                TelephoneNo = t.Request.TelephoneNo.ToString(),
                                                CustomerOwnerName = t.Customer.FirstNameOrTitle + " " + t.Customer.LastName,
                                                ServiceID = (int)t.ServiceID,
                                                ServiceTitle = t.ADSLService.Title,
                                            }).SingleOrDefault();
            }
        }

        public static List<ADSLRequest> GetADSLRequestinSupportStep()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLRequests.Where(t => t.Request.Status.RequestStep.StepTitle == "پشتیبان خارجی").ToList();
            }
        }

        public static List<Request> GetRequestinSupportStep()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.Status.RequestStepID == (byte)DB.ADSLRequestStep.ForeignSupport).ToList();
            }
        }
    }
}



