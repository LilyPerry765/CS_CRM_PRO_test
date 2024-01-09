using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace CRM.Data
{
    public class ADSLDB
    {
        public static ADSLCustomerInfo GetADSLCustomerInfobyID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.ID == id)
                                    .Select(t => new ADSLCustomerInfo
                                    {
                                        ID = t.ID,
                                        TelephoneNo = t.TelephoneNo.ToString(),
                                        UserID = t.UserID,
                                        CustomerName = t.Customer.FirstNameOrTitle + " " + ((t.Customer.LastName != null) ? t.Customer.LastName : ""),
                                        CustomerStatus = DB.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), t.CustomerOwnerStatus),
                                        MelliCode = t.Customer.NationalCodeOrRecordNo,
                                        ServiceID = (int)t.TariffID,
                                        Service = t.ADSLService.Title,
                                        PortID = t.ADSLPortID,
                                        Port = "ردیف : " + t.ADSLPort.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.ADSLPort.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.ADSLPort.Bucht.BuchtNo.ToString(),
                                        IPStatic = t.ADSLIP.IP,
                                        GroupIPStatic = t.ADSLGroupIP.StartRange,
                                        IPStartDate = (t.IPStaticID != null) ? Date.GetPersianDate(t.ADSLIP.InstallDate, Date.DateStringType.Short) : ((t.GroupIPStaticID != null) ? Date.GetPersianDate(t.ADSLGroupIP.InstallDate, Date.DateStringType.Short) : ""),
                                        IPEndDate = (t.IPStaticID != null) ? Date.GetPersianDate(t.ADSLIP.ExpDate, Date.DateStringType.Short) : ((t.GroupIPStaticID != null) ? Date.GetPersianDate(t.ADSLGroupIP.ExpDate, Date.DateStringType.Short) : ""),
                                        ModemID = t.ModemID,
                                        Modem = t.ADSLModemProperty.SerialNo,
                                        MobileNo = t.Customer.MobileNo,
                                        Email = t.Customer.Email,
                                        Center = t.Telephone.Center.Region.City.Name + " : " + t.Telephone.Center.CenterName,
                                        PostalCode = "",
                                        Address = "",
                                        InstallDate = Date.GetPersianDate(t.InstallDate, Date.DateStringType.Short),
                                        ExpDate = Date.GetPersianDate(t.ExpDate, Date.DateStringType.Short)
                                    }).SingleOrDefault();
            }
        }

        public static ADSL GetADSLByTelephoneNo(long? telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.TelephoneNo == telephone && t.Wireless == null).SingleOrDefault();
            }
        }

        public static ADSL GetWirelessbyCode(string code)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.Wireless == code).SingleOrDefault();
            }
        }

        public static ADSL GetADSLByUserName(string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.UserName == userName).OrderByDescending(t => t.ID).FirstOrDefault();
            }
        }

        public static ADSLInfo GetADSLInfoByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.TelephoneNo == telephoneNo)
                                    .Select(t => new ADSLInfo
                                    {
                                        TelephoneNo = t.TelephoneNo.ToString(),
                                        CustomerOwnerName = t.Customer.FirstNameOrTitle + " " + ((t.Customer.LastName != null) ? t.Customer.LastName : ""),
                                        ServiceID = (int)t.TariffID,
                                        ServiceTitle = t.ADSLService.Title,
                                    }).SingleOrDefault();
            }
        }

        public static List<ADSL> SearchAll()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Select(t => t).ToList();
            }
        }

        public static List<ADSLCustomerInfo> SearchADSLInfo(
            List<int> cites,
            List<int> centers,
            List<int> Statuses,
            List<int> serviceGroups,
            List<int> services,
            string TelNo,
            string CustomerName,
            string CustomerLastName,
            string UserName,
            bool? hasIP,
            List<int> customerType,
            List<int> personType,
            string mobileNo,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => (cites.Count == 0 || cites.Contains(t.Telephone.Center.Region.CityID)) &&
                                                (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Telephone.CenterID) : centers.Contains(t.Telephone.CenterID)) &&
                                                (Statuses.Count == 0 || (Statuses.Contains((int)t.Status))) &&
                                                (serviceGroups.Count == 0 || (serviceGroups.Contains((int)t.ADSLService.GroupID))) &&
                                                (services.Count == 0 || (services.Contains((int)t.TariffID))) &&
                                                (string.IsNullOrWhiteSpace(TelNo) || t.TelephoneNo.ToString().Contains(TelNo)) &&
                                                (string.IsNullOrWhiteSpace(CustomerName) || t.Customer.FirstNameOrTitle.Contains(CustomerName) || t.Customer.LastName.Contains(CustomerName)) &&
                                                (string.IsNullOrWhiteSpace(CustomerLastName) || t.Customer.LastName.Contains(CustomerLastName)) &&
                                                (string.IsNullOrWhiteSpace(UserName) || t.UserName.Contains(UserName)) &&
                                                (!hasIP.HasValue || hasIP == t.HasIP) &&
                                                (customerType.Count == 0 || (customerType.Contains((int)t.CustomerTypeID))) &&
                                                (personType.Count == 0 || (personType.Contains((int)t.Customer.PersonType))) &&
                                                (string.IsNullOrWhiteSpace(mobileNo) || t.Customer.MobileNo.Contains(mobileNo))
                                                )
                                     .Select(t => new ADSLCustomerInfo
                                     {
                                         ID = t.ID,
                                         TelephoneNo = t.TelephoneNo.ToString(),
                                         CustomerName = t.Customer.FirstNameOrTitle + " " + ((t.Customer.LastName != null) ? t.Customer.LastName : ""),
                                         MobileNo = t.Customer.MobileNo,
                                         PersonType = DB.GetEnumDescriptionByValue(typeof(DB.PersonType), t.Customer.PersonType),
                                         ADSLCustomerType = t.ADSLCustomerType.Title,
                                         CustomerStatus = DB.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), t.CustomerOwnerStatus),
                                         Service = t.ADSLService.Title,
                                         Port = "ردیف : " + t.ADSLPort.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.ADSLPort.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.ADSLPort.Bucht.BuchtNo.ToString(),
                                         IPStatic = t.ADSLIP.IP,
                                         GroupIPStatic = t.ADSLGroupIP.StartRange,
                                         Modem = t.ADSLModemProperty.SerialNo,
                                         ExpDate = (t.ExpDate != null) ? Date.GetPersianDate((DateTime)t.ExpDate, Date.DateStringType.DateTime) : null
                                     })
                                     .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchADSLInfoCount(
            List<int> cites,
            List<int> centers,
            List<int> Statuses,
            List<int> serviceGroups,
            List<int> Services,
            string TelNo,
            string CustomerName,
            string CustomerLastName,
            string UserName,
            bool? hasIP,
            List<int> customerType,
            List<int> personType,
            string mobileNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => (cites.Count == 0 || cites.Contains(t.Telephone.Center.Region.CityID)) &&
                                                (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Telephone.CenterID) : centers.Contains(t.Telephone.CenterID)) && (Statuses.Count == 0 || (Statuses.Contains((int)t.Status))) &&
                                                (Statuses.Count == 0 || (Statuses.Contains((int)t.Status))) &&
                                                (serviceGroups.Count == 0 || (serviceGroups.Contains((int)t.ADSLService.GroupID))) &&
                                                (Services.Count == 0 || (Services.Contains((int)t.TariffID))) &&
                                                (string.IsNullOrWhiteSpace(TelNo) || t.TelephoneNo.ToString().Contains(TelNo)) &&
                                                (string.IsNullOrWhiteSpace(CustomerName) || t.CustomerOwnerName.Contains(CustomerName)) &&
                                                (string.IsNullOrEmpty(CustomerLastName) || t.Customer.LastName.Contains(CustomerLastName)) &&
                                                (string.IsNullOrWhiteSpace(UserName) || t.UserName.Contains(UserName)) &&
                                                (!hasIP.HasValue || hasIP == t.HasIP) &&
                                                (customerType.Count == 0 || (customerType.Contains((int)t.CustomerTypeID))) &&
                                                (personType.Count == 0 || (personType.Contains((int)t.Customer.PersonType))) &&
                                                (string.IsNullOrWhiteSpace(mobileNo) || t.Customer.MobileNo.Contains(mobileNo))).Count();
            }
        }

        public static ADSLInfo GetADSLInfobyUsername(string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.UserName == userName)
                    .Select(t => new ADSLInfo
                              {
                                  TelephoneNo = t.TelephoneNo.ToString(),
                                  CustomerOwnerName = (t.CustomerOwnerID != null) ? t.Customer.FirstNameOrTitle + " " + ((t.Customer.LastName != null) ? t.Customer.LastName : "") : "",
                                  ServiceID = (t.TariffID != null) ? (int)t.TariffID : 0,
                                  ServiceTitle = (t.TariffID != null) ? t.ADSLService.Title : "",
                              }).SingleOrDefault();
            }
        }

        public static void SaveADSL(ADSL aDSL)
        {
            aDSL.Detach();
            Save(aDSL);
        }

        private static bool IsValidType(object o, Type t)
        {
            try
            {
                System.Convert.ChangeType(o, t);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Save(object instance, bool isNew = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                MetaDataMember primaryKey = context.Mapping.GetTable(instance.GetType()).RowType.IdentityMembers[0];
                MetaDataMember insertDateField = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "InsertDate").SingleOrDefault();
                MetaDataMember modifyDate = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ModifyDate").SingleOrDefault();

                object obj = instance;

                if (modifyDate != null)
                    modifyDate.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                if (isNew || (primaryKey.MemberAccessor.GetBoxedValue(instance) == null || (IsValidType(primaryKey.MemberAccessor.GetBoxedValue(instance), typeof(Int64))) && Convert.ToInt64(primaryKey.MemberAccessor.GetBoxedValue(instance)) == 0))
                {
                    if (insertDateField != null)
                        insertDateField.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                    context.GetTable(instance.GetType()).InsertOnSubmit(instance);
                }
                else
                {
                    context.GetTable(instance.GetType()).Attach(instance);
                    context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, instance);
                }
                context.SubmitChanges();
            }
        }

        public static bool HasADSLbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSL aDSL = context.ADSLs.Where(t => t.TelephoneNo == telephoneNo && t.Wireless == null).SingleOrDefault();

                if (aDSL != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool HasADSLInstalbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSL aDSL = context.ADSLs.Where(t => t.TelephoneNo == telephoneNo && t.Status != (byte)DB.ADSLStatus.Discharge).SingleOrDefault();

                if (aDSL != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool HasExpiredADSLbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool hasADSL = false;
                DateTime now = DateTime.Now;

                ADSL aDSL = context.ADSLs.Where(t => t.TelephoneNo == telephoneNo && t.ExpDate > now).SingleOrDefault();

                if (aDSL != null)
                    hasADSL = true;
                else
                    hasADSL = false;

                return hasADSL;
            }
        }

        public static bool GetActiveADSLbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool hasADSL = false;
                DateTime now = DateTime.Now;

                ADSL aDSL = context.ADSLs.Where(t => t.TelephoneNo == telephoneNo && t.Status != (byte)DB.ADSLStatus.Discharge && t.Status != (byte)DB.ADSLStatus.Pending && t.Status != (byte)DB.ADSLStatus.Cut && t.Status != null).SingleOrDefault();

                if (aDSL != null)
                    hasADSL = true;
                else
                    hasADSL = false;

                return hasADSL;
            }
        }

        public static List<ADSL> GetADSLbyPortID(long portID)
        {
            List<ADSL> result = null;

            using (MainDataContext context = new MainDataContext())
            {
                List<ADSL> aDSLList = context.ADSLs.Where(t => t.ADSLPortID == portID).ToList();

                if (aDSLList.Count == 0)
                    result = null;
                else
                    result = aDSLList;
            }

            return result;
        }

        public static List<ADSL> GetADSLbyModemID(long modemID)
        {
            List<ADSL> result = null;

            using (MainDataContext context = new MainDataContext())
            {
                List<ADSL> aDSLList = context.ADSLs.Where(t => t.ModemID == modemID).ToList();

                if (aDSLList.Count == 0)
                    result = null;
                else
                    result = aDSLList;
            }

            return result;
        }

        public static string GetUserFullName(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string fullName = "";

                fullName = context.Users.Where(t => t.ID == id).Select(t => t.FirstName + " " + t.LastName).SingleOrDefault();

                if (!string.IsNullOrEmpty(fullName))
                    return fullName;
                else
                    return "";
            }
        }

        public static List<City> GetAllCity()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities
                        .ToList();
            }
        }

        public static string GetSettingValueByKey(string key)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Settings
                    .Where(t => t.Key == key)
                    .SingleOrDefault().Value;
            }
        }

        public static List<Center> GetAllCenter()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.ToList();
            }
        }
    }
}
