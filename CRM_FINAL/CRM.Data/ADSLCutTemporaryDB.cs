using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLCutTemporaryDB
    {
        public static ADSLCutTemporaryFullViewInfo GetADSLCutTemporaryInfo(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCutTemporaries
                    .Where(t => t.ID == requestID)
                    .Select(t => new ADSLCutTemporaryFullViewInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.Request.TelephoneNo,
                        CustomerNationalCode = t.Request.Customer.NationalCodeOrRecordNo,
                        CustomerName = t.Request.Customer.FirstNameOrTitle + " " + t.Request.Customer.LastName,
                        //CustomerStatus = DB.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)t.Telephone.ADSL.CustomerOwnerStatus),
                       
                        //PostalCode = t.Telephone.Address.PostalCode,
                        Address=t.Request.Center.Address,
                        //Address = t.Telephone.Address.AddressContent,
                        //ServiceType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), (int)t.Telephone.ADSL.ServiceType),
                        CutType= DB.GetEnumDescriptionByValue(typeof(DB.ADSLCutType), (int)t.CutType),
                        //RegistrationProjectType= DB.GetEnumDescriptionByValue(typeof(DB.ADSLRegistrationProjectType), (int)t.Telephone.ADSL.RegistrationProjectType),
                        //TariffTitle = t.Telephone.ADSL.ADSLService.Title,                        
                        //LicenseLetterNo = t.Telephone.ADSL.LicenseLetterNo,
                        //BandWidth = t.Telephone.ADSL.ADSLService.ADSLServiceBandWidth.Title.ToString() + "کیلو بایت",
                        //TrafficLimitation = t.Telephone.ADSL.ADSLService.ADSLServiceTraffic.Title.ToString() + "گیگا بایت",
                        //Duration = t.Telephone.ADSL.ADSLService.ADSLServiceTraffic.Title.ToString() + "ماه",
                        //Price = t.Telephone.ADSL.ADSLService.Price.ToString(),
                        CreatorUser = GetUserFullName(t.Request.CreatorUserID),
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                        Center = t.Request.Center.CenterName,
                        RequestDate = Date.GetPersianDate(t.Request.RequestDate, Date.DateStringType.Short),
                        //CommentCustomers = t.CommentCustomers,
                        //CutUser = GetUserFullName(t.CutUserID),
                        //CutDate = Date.GetPersianDate(t.CutDate, Date.DateStringType.DateTime),
                        ////CutCommnet = t.CutComment,
                        //FinalUser = GetUserFullName(t.FinalUserID),
                        //FinalDate = Date.GetPersianDate(t.FinalDate, Date.DateStringType.DateTime),
                        //FinalComment = t.FinalComment,
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

        public static ADSLCutTemporary GetADSLCutTemproryByRequestID(long RequestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCutTemporaries.Where(t => t.ID == RequestID).SingleOrDefault();
            }
        }
    }
}
