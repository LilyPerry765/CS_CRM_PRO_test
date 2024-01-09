using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLServiceDB
    {
        public static List<ADSLService> SearchADSLService(
            string title,
            List<int> typeIDs,
            List<int> groupIDs,
            long price,
            List<int> bandWidthIDs,
            List<int> trafficLimitationIDs,
            List<int> durationIDs,
            bool? isRequiredLicense,
            bool? isOnlineRegister,
            bool? hasModem,
            bool? isActive,
            DateTime? fromStartDate,
            DateTime? untilStartDate,
            DateTime? fromEndDate,
            DateTime? untilEndDate,
            int? serviceCode,
            out int totalRecords,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLService> result = new List<ADSLService>();
                var query = context.ADSLServices
                                   .Where(t =>
                                           (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                           (typeIDs.Count == 0 || typeIDs.Contains(t.TypeID)) &&
                                           (groupIDs.Count == 0 || groupIDs.Contains(t.GroupID)) &&
                                           (bandWidthIDs.Count == 0 || bandWidthIDs.Contains((int)t.BandWidthID)) &&
                                           (trafficLimitationIDs.Count == 0 || trafficLimitationIDs.Contains((int)t.TrafficID)) &&
                                           (durationIDs.Count == 0 || durationIDs.Contains((int)t.DurationID)) &&
                                           (price == -1 || t.Price == price) &&
                                           (!isRequiredLicense.HasValue || isRequiredLicense == t.IsRequiredLicense) &&
                                           (!isOnlineRegister.HasValue || isOnlineRegister == t.IsOnlineRegister) &&
                                           (!hasModem.HasValue || hasModem == t.IsModemMandatory) &&
                                           (!isActive.HasValue || isActive == t.IsActive) &&
                                           (!fromStartDate.HasValue || t.StartDtae >= fromStartDate) &&
                                           (!untilStartDate.HasValue || t.StartDtae <= untilStartDate) &&
                                           (!fromEndDate.HasValue || t.EndDate >= fromEndDate) &&
                                           (!untilEndDate.HasValue || t.EndDate <= untilEndDate) &&
                                           (!serviceCode.HasValue || (t.ServiceCode == serviceCode))
                                         )
                                   .AsQueryable();

                totalRecords = query.Count();

                result = query.Skip(startRowIndex).Take(pageSize).ToList();
                return result;
            }
        }

        public static int SearchADSLServiceCount(
            string title,
            List<int> typeIDs,
            List<int> groupIDs,
            long price,
            List<int> bandWidthIDs,
            List<int> trafficLimitationIDs,
            List<int> durationIDs,
            bool? isRequiredLicense,
            bool? isOnlineRegister,
            bool? hasModem,
            bool? isActive,
            DateTime? fromStartDate,
            DateTime? untilStartDate,
            DateTime? fromEndDate,
            DateTime? untilEndDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices
                .Where(t =>
                        (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                        (typeIDs.Count == 0 || typeIDs.Contains(t.TypeID)) &&
                        (groupIDs.Count == 0 || groupIDs.Contains(t.GroupID)) &&
                        (bandWidthIDs.Count == 0 || bandWidthIDs.Contains((int)t.BandWidthID)) &&
                        (trafficLimitationIDs.Count == 0 || trafficLimitationIDs.Contains((int)t.TrafficID)) &&
                        (durationIDs.Count == 0 || durationIDs.Contains((int)t.DurationID)) &&
                        (price == -1 || t.Price == price) &&
                        (!isRequiredLicense.HasValue || isRequiredLicense == t.IsRequiredLicense) &&
                        (!isOnlineRegister.HasValue || isOnlineRegister == t.IsOnlineRegister) &&
                        (!hasModem.HasValue || hasModem == t.IsModemMandatory) &&
                        (!isActive.HasValue || isActive == t.IsActive) &&
                        (!fromStartDate.HasValue || t.StartDtae >= fromStartDate) &&
                        (!untilStartDate.HasValue || t.StartDtae <= untilStartDate) &&
                        (!fromEndDate.HasValue || t.EndDate >= fromEndDate) &&
                        (!untilEndDate.HasValue || t.EndDate <= untilEndDate))
                .Count();
            }
        }

        public static ADSLService GetADSLServiceById(int? id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == id).SingleOrDefault();
            }

        }

        public static ADSLServiceInfo GetADSLServiceInfoById(int? id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == id)
                                          .Select(t => new ADSLServiceInfo
                                          {
                                              ID = t.ID,
                                              Title = t.Title,
                                              ServiceGroup = t.ADSLServiceGroup.Title,
                                              ServiceType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), t.TypeID),
                                              PaymentTypeID = t.PaymentTypeID,
                                              PaymentType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPaymentType), t.PaymentTypeID),
                                              NewtworkType = t.ADSLServiceNetwork.Title,
                                              BandWidth = (t.BandWidthID != 0 || t.BandWidthID != -1) ? t.ADSLServiceBandWidth.Title + " کیلو بایت" : t.ADSLServiceBandWidth.Title,
                                              Traffic = (t.TrafficID != 0 || t.TrafficID != -1) ? t.ADSLServiceTraffic.Title + " گیگا بایت" : t.ADSLServiceTraffic.Title,
                                              DurationID = (int)t.DurationID,
                                              Duration = (t.DurationID != 0 || t.DurationID != -1) ? t.ADSLServiceDuration.Title + " ماه" : t.ADSLServiceDuration.Title,
                                              GroupID = t.GroupID,
                                              CustomerGroupID = (int)t.ADSLServiceGroup.CustomerGroupID,
                                              Price = t.Price.ToString() + " ريا ل",
                                              Tax = t.Tax.ToString() + " درصد",
                                              Abonman = (t.Abonman != null && t.DurationID != 0) ? (t.Abonman * Convert.ToInt32(t.ADSLServiceDuration.Title)).ToString() + " ریا ل" : "0 ریا ل",
                                              PriceSum = t.PriceSum.ToString() + " ریا ل",
                                              IsRequiredLicense = t.IsRequiredLicense,
                                              IsInstalment = t.IsInstalment,
                                              IsModemMandatory = t.IsModemMandatory,
                                              IsModemInstallment = t.IsModemInstallment,
                                              ModemDiscount = (t.ModemDiscount == null) ? 0 : (int)t.ModemDiscount,
                                              IPDiscount = (t.IPDiscount == null) ? 0 : (int)t.IPDiscount
                                          }).SingleOrDefault();
            }

        }

        public static ADSLServiceInfo GetADSLAdditionalTrafficInfoById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == id)
                                          .Select(t => new ADSLServiceInfo
                                          {
                                              ID = t.ID,
                                              Title = t.Title,
                                              ServiceGroup = t.ADSLServiceGroup.Title,
                                              ServiceType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), t.TypeID),
                                              PaymentTypeID = t.PaymentTypeID,
                                              PaymentType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPaymentType), t.PaymentTypeID),
                                              Traffic = (t.TrafficID != 0 || t.TrafficID != -1) ? t.ADSLServiceTraffic.Title + " گیگا بایت" : t.ADSLServiceTraffic.Title,
                                              DurationID = (int)t.DurationID,
                                              Duration = (t.DurationID != 0 || t.DurationID != -1) ? t.ADSLServiceDuration.Title + " ماه" : t.ADSLServiceDuration.Title,
                                              Price = t.Price.ToString() + " ريا ل",
                                              Abonman = (t.Abonman != null && t.DurationID != 0 && t.DurationID != -1) ? (t.Abonman * Convert.ToInt32(t.ADSLServiceDuration.Title)).ToString() + " ریا ل" : "0 ریا ل",
                                              Tax = t.Tax.ToString() + " درصد",
                                              PriceSum = t.PriceSum.ToString() + " ریا ل",
                                              IsRequiredLicense = t.IsRequiredLicense
                                          }).SingleOrDefault();
            }

        }

        public static List<ADSLService> GetADSLServicebyIBSngName(string iBSngName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.IBSngGroupName == iBSngName).ToList();
            }
        }

        public static List<ADSLService> GetADSLServiceByPropertiesIds(int? customerGroupID, int typeID, int groupId, int bandWithId, int trafficId, int durationId, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (customerGroupID == null || t.ADSLServiceGroup.CustomerGroupID == customerGroupID) &&
                                                       (typeID == -1 || t.PaymentTypeID == typeID) &&
                                                       (groupId == -1 || t.GroupID == groupId) &&
                                                       (bandWithId == -1 || t.BandWidthID == bandWithId) &&
                                                       (trafficId == -1 || t.TrafficID == trafficId) &&
                                                       (durationId == -1 || t.DurationID == durationId) &&
                                                       (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (t.IsActive == true) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (!wireless || t.ForWireless == wireless)
                                                       ).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceCheckableByPropertiesIds(List<int> customerGroupsID, List<int> typeID, List<int> groupId, List<int> bandWithId, List<int> trafficId, List<int> durationId, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                DateTime now = DB.GetServerDate();
                var query = context.ADSLServices.Where(t =>
                                                       (customerGroupsID.Count == 0 || (t.ADSLServiceGroup.CustomerGroupID.HasValue && customerGroupsID.Contains(t.ADSLServiceGroup.CustomerGroupID.Value))) &&
                                                       (typeID.Count == 0 || typeID.Contains(t.PaymentTypeID)) &&
                                                       (groupId.Count == 0 || groupId.Contains(t.GroupID)) &&
                                                       (bandWithId.Count == 0 || (t.BandWidthID.HasValue && bandWithId.Contains(t.BandWidthID.Value))) &&
                                                       (trafficId.Count == 0 || (t.TrafficID.HasValue && trafficId.Contains(t.TrafficID.Value))) &&
                                                       (durationId.Count == 0 || (t.DurationID.HasValue && durationId.Contains(t.DurationID.Value))) &&
                                                       (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (t.IsActive == true) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (!wireless || t.ForWireless == wireless)
                                                      )
                                                .AsQueryable();
                result = query.Select(ads => new CheckableItem
                                                 {
                                                     ID = ads.ID,
                                                     Name = ads.Title,
                                                     IsChecked = false
                                                 }
                                     )
                              .OrderBy(ci => ci.Name)
                              .ToList();
                return result;
            }
        }

        public static List<ADSLService> GetADSLServiceByPropertiesIdsAgent(int? customerGroupID, int typeID, int groupId, int bandWithId, int trafficId, int durationId, List<int> serviceAccessList, List<int> serviceGroupAccessList, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (customerGroupID == null || t.ADSLServiceGroup.CustomerGroupID == customerGroupID) &&
                                                       (typeID == -1 || t.PaymentTypeID == typeID) &&
                                                       (groupId == -1 || t.GroupID == groupId) &&
                                                       (bandWithId == -1 || t.BandWidthID == bandWithId) &&
                                                       (trafficId == -1 || t.TrafficID == trafficId) &&
                                                       (durationId == -1 || t.DurationID == durationId) &&
                                                       (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (t.IsActive == true) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (serviceAccessList.Count == 0 || serviceAccessList.Contains(t.ID)) &&
                                                       (serviceGroupAccessList.Count == 0 || serviceGroupAccessList.Contains(t.GroupID)) &&
                                                       (!wireless || t.ForWireless == wireless)
                                                       ).ToList();
            }
        }

        public static List<ADSLService> GetADSLService()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => (t.IsActive == true) && (t.TypeID == (byte)DB.ADSLServiceType.Service)).ToList();
            }
        }

        public static List<ADSLService> GetADSLServiceByGroupID(int groupID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => (t.GroupID == groupID) &&
                                                       (t.IsActive == true) &&
                                                       (t.TypeID == (byte)DB.ADSLServiceType.Service)).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceByGroupIDs(List<int> groupIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => (groupIDs.Contains(t.GroupID)) &&                                                       
                                                       (t.TypeID == (byte)DB.ADSLServiceType.Service))
                                            .Select(t => new CheckableItem
                                          {
                                              ID = t.ID,
                                              Name = t.Title,
                                              IsChecked = false
                                          }).OrderBy(t => t.Name).ToList();
            }
        }

        public static List<ADSLService> GetADSLAdditionalTraffic()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => (t.IsActive == true) && (t.TypeID == (byte)DB.ADSLServiceType.Traffic)).ToList();
            }
        }

        public static List<ADSLService> GetADSLServiceOnline()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => (t.IsActive == true) && (t.TypeID == (byte)DB.ADSLServiceType.Service) && (t.IsOnlineRegister == true)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLService(bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (!wireless || t.ForWireless == wireless)
                                                       ).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLServicebyCustomerGroup(int? customerGroup, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                    // (t.ADSLServiceGroup.ADSLCustomerGroup.ID == customerGroup) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (!wireless || t.ForWireless == wireless)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLServicebyCustomerGroupwithPrice(long? price, int? customerGroup, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (t.IsInstalment == true || !price.HasValue || t.PriceSum >= price) &&
                    // (t.ADSLServiceGroup.ADSLCustomerGroup.ID == customerGroup) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now)&&
                                                       (!wireless || t.ForWireless == wireless)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedPostPaidADSLServicebyCustomerGroupwithPrice(long? price, int? customerGroup, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (!price.HasValue || t.PriceSum >= price) &&
                    // (t.ADSLServiceGroup.ADSLCustomerGroup.ID == customerGroup) &&
                                                       (t.PaymentTypeID == (byte)DB.PaymentType.Instalment) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now)&&
                                                       (!wireless || t.ForWireless == wireless)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLServicebyCustomerGroupAgent(int customerGroup, List<int> serviceAccessList, List<int> serviceGroupAccessList, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                    //  (t.ADSLServiceGroup.ADSLCustomerGroup.ID == customerGroup) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (serviceAccessList.Contains(t.ID)) &&
                                                       (serviceGroupAccessList.Contains(t.GroupID)) &&
                                                       (!wireless || t.ForWireless == wireless)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLServicebyCustomerGroupAgentwithPrice(long? price, int customerGroup, List<int> serviceAccessList, List<int> serviceGroupAccessList, bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (t.IsInstalment == true || !price.HasValue || t.PriceSum >= price) &&
                    //  (t.ADSLServiceGroup.ADSLCustomerGroup.ID == customerGroup) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (serviceAccessList.Contains(t.ID)) &&
                                                       (serviceGroupAccessList.Contains(t.GroupID))&&
                                                       (!wireless || t.ForWireless == wireless)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedPostPaidADSLServicebyCustomerGroupAgentwithPrice(long? price, int customerGroup, List<int> serviceAccessList, List<int> serviceGroupAccessList)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                                                       (!price.HasValue || t.PriceSum >= price) &&
                                                       (t.PaymentTypeID == (byte)DB.PaymentType.Instalment) &&
                    //  (t.ADSLServiceGroup.ADSLCustomerGroup.ID == customerGroup) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (serviceAccessList.Contains(t.ID)) &&
                                                       (serviceGroupAccessList.Contains(t.GroupID))).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLAdditionalService(bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Traffic) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (t.IsActive == true) && (!wireless || t.ForWireless == wireless)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLServiceforInternet(int groupID, long? priceSum)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t =>
                     (t.IsInstalment == false) &&
                         //(!priceSum.HasValue || t.PriceSum >= priceSum) &&
                     (t.GroupID == groupID) &&
                     (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                     (t.StartDtae == null || t.StartDtae < now) &&
                     (t.EndDate == null || t.EndDate > now) &&
                     (t.IsActive == true) &&
                     (t.ADSLServiceGroup.IsActive == true) &&
                     (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                     (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now)).ToList();
            }
        }

        public static List<ADSLService> GetAllowedADSLAdditionalServiceforInternet()
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t => (t.TypeID == (byte)DB.ADSLServiceType.Traffic) &&
                                                       (t.StartDtae == null || t.StartDtae < now) &&
                                                       (t.EndDate == null || t.EndDate > now) &&
                                                       (t.ADSLServiceGroup.IsActive == true) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now) &&
                                                       (t.IsActive == true) &&
                                                       (t.IsOnlineRegister == true)).ToList();
            }
        }

        public static List<ADSLService> GetADSLServicebyGroupID(int groupId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.IsActive == true && t.GroupID == groupId).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.TypeID == (byte)DB.ADSLServiceType.Service && t.IsActive == true)
                                          .Select(t => new CheckableItem
                                          {
                                              ID = t.ID,
                                              Name = t.Title,
                                              IsChecked = false
                                          })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceCheckableByTrafficIDs(List<int> TrafficIDs, List<int> GroupIds, List<int> DurationIds, List<int> BandwidthIds)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.TypeID == (byte)DB.ADSLServiceType.Service && t.IsActive == true &&
                    (TrafficIDs.Count == 0 || TrafficIDs.Contains((int)t.TrafficID))
                    && (GroupIds.Count == 0 || GroupIds.Contains((int)t.GroupID))
                    && (DurationIds.Count == 0 || DurationIds.Contains((int)t.DurationID))
                    && (BandwidthIds.Count == 0 || BandwidthIds.Contains((int)t.BandWidthID)))
                                          .Select(t => new CheckableItem
                                          {
                                              ID = t.ID,
                                              Name = t.Title,
                                              IsChecked = false
                                          })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }


        public static List<CheckableItem> GetAllADSLServiceCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.TypeID == (byte)DB.ADSLServiceType.Service)
                                          .Select(t => new CheckableItem
                                          {
                                              ID = t.ID,
                                              Name = t.Title,
                                              IsChecked = false
                                          })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetAllADSLServiceCheckablebyGroupID(List<int> groupIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.TypeID == (byte)DB.ADSLServiceType.Service && groupIDs.Contains(t.GroupID))
                                          .Select(t => new CheckableItem
                                          {
                                              ID = t.ID,
                                              Name = t.Title,
                                              IsChecked = false
                                          })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceCheckableNew()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.TypeID == 1).Select(t => new CheckableItem
                                          {
                                              ID = t.ID,
                                              Name = t.Title,
                                              IsChecked = false
                                          })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceCheckableNewAll()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.Title,
                    IsChecked = false
                })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceGroupCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroups
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

        public static List<CheckableItem> GetADSLServiceGroupCheckablebyCustomerGroupID(int customerGroupID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroups.Where(t => /*t.CustomerGroupID == customerGroupID &&*/ t.IsActive == true && (t.TimeoutDate == null || t.TimeoutDate > DB.GetServerDate()))

                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceGroupCheckablebyCustomerGroupIDAgent(int customerGroupID, List<int> serviceGroupAccessList)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroups.Where(t => /* t.CustomerGroupID == customerGroupID && */ t.IsActive == true && (t.TimeoutDate == null || t.TimeoutDate > DB.GetServerDate()) && (serviceGroupAccessList.Contains(t.ID)))

                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceGroupCheckablebytypeID(int typeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroups.Where(t => t.IsActive == true && (t.TimeoutDate == null || t.TimeoutDate > DB.GetServerDate()))
                 .Join(context.ADSLServices, group => group.ID, service => service.GroupID, (group, service) => new { Group = group, Service = service }).Where(t => t.Service.PaymentTypeID == typeID)
                     .Select(t => new CheckableItem
                     {
                         ID = t.Group.ID,
                         Name = t.Group.Title,
                         IsChecked = false
                     })
                     .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<ADSLServiceBandWidth> SearchADSLServiceBandWidths(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceBandWidths
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (t.ID != -1))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static ADSLServiceBandWidth GetADSLServiceBandWidthByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceBandWidths
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLServiceBandWidthCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceBandWidths
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = (t.ID == 0 || t.ID == -1) ? t.Title : t.Title + " کیلو بایت",
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceBandWithCheckablebyGroupID(int groupID, int typeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceBandWidths.
                    Join(context.ADSLServices, bandWidth => bandWidth.ID, service => service.BandWidthID, (bandWidth, service) => new { BandWidth = bandWidth, Service = service })
                    .Where(t => t.Service.GroupID == groupID)
                     .Intersect(context.ADSLServiceBandWidths
                     .Join(context.ADSLServices, bandWidth => bandWidth.ID, service => service.BandWidthID, (bandWidth, service) => new { BandWidth = bandWidth, Service = service })
                     .Where(t => t.Service.PaymentTypeID == typeID))
                     .Select(t => new CheckableItem
                     {
                         ID = t.BandWidth.ID,
                         Name = (t.BandWidth.ID != 0 || t.BandWidth.ID != -1) ? t.BandWidth.Title : t.BandWidth.Title + " کیلو بایت",
                         IsChecked = false
                     })
                     .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(List<int> groupID, List<int> typeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceBandWidths.Join(context.ADSLServices, bandWidth => bandWidth.ID, service => service.BandWidthID, (bandWidth, service) => new { BandWidth = bandWidth, Service = service })
                    .Where(t => groupID.Count == 0 || groupID.Contains((int)t.Service.GroupID))
                     .Intersect(context.ADSLServiceBandWidths.Join(context.ADSLServices, bandWidth => bandWidth.ID, service => service.BandWidthID, (bandWidth, service) => new { BandWidth = bandWidth, Service = service })
                     .Where(t => typeID.Count == 0 || typeID.Contains((int)t.Service.PaymentTypeID)))
                     .Select(t => new CheckableItem
                     {
                         ID = t.BandWidth.ID,
                         Name = (t.BandWidth.ID != 0 || t.BandWidth.ID != -1) ? t.BandWidth.Title : t.BandWidth.Title + " کیلو بایت",
                         IsChecked = false
                     }).Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceBandWithCheckablebyGroupIDsAndPaymentTypeID(List<int> groupID, int? typeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceBandWidths.Join(context.ADSLServices, bandWidth => bandWidth.ID, service => service.BandWidthID, (bandWidth, service) => new { BandWidth = bandWidth, Service = service })
                    .Where(t => groupID.Count == 0 || groupID.Contains((int)t.Service.GroupID))
                     .Intersect(context.ADSLServiceBandWidths.Join(context.ADSLServices, bandWidth => bandWidth.ID, service => service.BandWidthID, (bandWidth, service) => new { BandWidth = bandWidth, Service = service })
                     .Where(t => !typeID.HasValue || typeID == (int)t.Service.PaymentTypeID))
                     .Select(t => new CheckableItem
                     {
                         ID = t.BandWidth.ID,
                         Name = (t.BandWidth.ID != 0 || t.BandWidth.ID != -1) ? t.BandWidth.Title : t.BandWidth.Title + " کیلو بایت",
                         IsChecked = false
                     }).Distinct().OrderBy(t => t.ID).ToList();
            }
        }



        public static List<ADSLServiceTraffic> SearchADSLServiceTraffics(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceTraffics
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (t.ID != -1))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static ADSLServiceTraffic GetADSLServiceTraffiBycID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceTraffics
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLServiceTrafficCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceTraffics
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = (t.ID == 0 || t.ID == -1) ? t.Title : t.Title + " گیگا بایت",
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceTrafficCheckablebyDurationID(int durationID, int bandWidthID, int groupID, int typeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceTraffics.Join(context.ADSLServices, traffic => traffic.ID, service => service.TrafficID, (traffic, service) => new { Traffic = traffic, Service = service }).Where(t => t.Service.DurationID == durationID)
                     .Intersect(context.ADSLServiceTraffics.Join(context.ADSLServices, traffic => traffic.ID, service => service.TrafficID, (traffic, service) => new { Traffic = traffic, Service = service }).Where(t => t.Service.BandWidthID == bandWidthID))
                     .Intersect(context.ADSLServiceTraffics.Join(context.ADSLServices, traffic => traffic.ID, service => service.TrafficID, (traffic, service) => new { Traffic = traffic, Service = service }).Where(t => t.Service.GroupID == groupID))
                     .Intersect(context.ADSLServiceTraffics.Join(context.ADSLServices, traffic => traffic.ID, service => service.TrafficID, (traffic, service) => new { Traffic = traffic, Service = service }).Where(t => t.Service.PaymentTypeID == typeID))
                     .Select(t => new CheckableItem
                     {
                         ID = t.Traffic.ID,
                         Name = (t.Traffic.ID != 0 || t.Traffic.ID != -1) ? t.Traffic.Title : t.Traffic.Title + " گیگا بایت",
                         IsChecked = false
                     })
                     .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceTrafficCheckablebyDurationIDs(List<int> durationIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceTraffics.Join(context.ADSLServices, traffic => traffic.ID, service => service.TrafficID, (traffic, service) =>
                    new { Traffic = traffic, Service = service }).Where(t => durationIDs.Count == 0 || durationIDs.Contains((int)t.Service.DurationID))
                     .Select(t => new CheckableItem
                     {
                         ID = t.Traffic.ID,
                         Name = (t.Traffic.ID != 0 || t.Traffic.ID != -1) ? t.Traffic.Title : t.Traffic.Title + " گیگا بایت",
                         IsChecked = false
                     })
                     .Distinct().OrderBy(t => t.ID).ToList();
            }
        }



        public static List<ADSLServiceDuration> SearchADSLServiceDurations(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceDurations
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (t.ID != -1))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static ADSLServiceDuration GetADSLServiceDurationByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceDurations
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static int? GetADSLServiceDurationByServiceID(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == serviceID).SingleOrDefault().DurationID;
            }
        }

        public static string GetADSLServiceDurationTitleByServiceID(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == serviceID).SingleOrDefault().ADSLServiceDuration.Title;
            }
        }

        public static List<CheckableItem> GetADSLServiceDurationCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceDurations
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = (t.ID == 0 || t.ID == -1) ? t.Title : t.Title + " ماه",
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceDurationCheckablebyBandWidthID(int bandWidthID, int groupID, int typeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceDurations.Join(context.ADSLServices, duration => duration.ID, service => service.DurationID, (duration, service) => new { Duration = duration, Service = service }).Where(t => t.Service.BandWidthID == bandWidthID)
                     .Intersect(context.ADSLServiceDurations.Join(context.ADSLServices, duration => duration.ID, service => service.DurationID, (duration, service) => new { Duration = duration, Service = service }).Where(t => t.Service.GroupID == groupID))
                     .Intersect(context.ADSLServiceDurations.Join(context.ADSLServices, duration => duration.ID, service => service.DurationID, (duration, service) => new { Duration = duration, Service = service }).Where(t => t.Service.PaymentTypeID == typeID))
                     .Select(t => new CheckableItem
                     {
                         ID = t.Duration.ID,
                         Name = (t.Duration.ID != 0 || t.Duration.ID != -1) ? t.Duration.Title : t.Duration.Title + " ماه",
                         IsChecked = false
                     })
                     .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceDurationForIpTime(int maxDurationID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceDurations.Where(t => t.ID != 0 && t.ID != -1 && t.ID <= maxDurationID)
                    .Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.Title + " ماه",
                    IsChecked = false
                })
                    .ToList(); ;
            }
        }

        public static List<ADSLServiceGiftProfile> SearchADSLServiceGiftProfiles(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGiftProfiles
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static ADSLServiceGiftProfile GetADSLServiceGiftProfileByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGiftProfiles
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLServiceGiftProfileCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGiftProfiles
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

        public static List<ADSLServiceNetwork> SearchADSLServiceNetworks(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceNetworks
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static ADSLServiceNetwork GetADSLServiceNetworkByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceNetworks
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLServiceNetworkCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceNetworks
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

        public static string GetADSLServiceDuration(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == serviceID).SingleOrDefault().ADSLServiceDuration.Title;
            }
        }

        public static string GetADSLServiceDurationTitle(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == serviceID && t.DurationID != -1 && t.DurationID != 0).SingleOrDefault().ADSLServiceDuration.Title;

            }
        }

        public static string GetADSLServiceCreditbyServiceID(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int trafficID = Convert.ToInt32(context.ADSLServices.Where(t => t.ID == serviceID).SingleOrDefault().TrafficID);

                if (trafficID == -1 || trafficID == 0)
                    return "";
                else
                    return context.ADSLServiceTraffics.Where(t => t.ID == trafficID).SingleOrDefault().Credit.ToString();
            }
        }

        public static int GetADSLServiceCustomerGroupIDbyServiceID(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return (int)context.ADSLServices.Where(t => t.ID == serviceID).SingleOrDefault().ADSLServiceGroup.CustomerGroupID;
            }
        }

        public static string GetADSLServiceTrafficbyServiceID(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int trafficID = Convert.ToInt32(context.ADSLServices.Where(t => t.ID == serviceID).SingleOrDefault().TrafficID);

                if (trafficID == -1 || trafficID == 0)
                    return "";
                else
                    return context.ADSLServiceTraffics.Where(t => t.ID == trafficID).SingleOrDefault().Title;
            }
        }

        public static List<ADSLService> GetADSLExtentionServiceInfoById(long? priceSum, int customerGroupID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime now = DB.GetServerDate();
                return context.ADSLServices.Where(t =>
                     (!priceSum.HasValue || t.PriceSum >= priceSum) &&
                         //  (t.ADSLServiceGroup.CustomerGroupID == customerGroupID) &&
                     (t.TypeID == (byte)DB.ADSLServiceType.Service) &&
                     (t.StartDtae == null || t.StartDtae < now) &&
                     (t.EndDate == null || t.EndDate > now) &&
                     (t.IsActive == true) &&
                     (t.ADSLServiceGroup.IsActive == true) &&
                     (t.ADSLServiceGroup.StartDate == null || t.ADSLServiceGroup.StartDate < now) &&
                     (t.ADSLServiceGroup.TimeoutDate == null || t.ADSLServiceGroup.TimeoutDate > now)).ToList();
            }
        }

        public static List<CheckableItem> GetADSLServiceDurationByBandwidtIDs(List<int> BandwidthIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => BandwidthIDs.Count == 0 || BandwidthIDs.Contains((int)t.BandWidthID))
                    .Select(t => new CheckableItem
                    {
                        Name = t.ADSLServiceDuration.Title,
                        ID = (int)t.DurationID,
                        IsChecked = false
                    }).Distinct().ToList();
            }
        }



    }
}
