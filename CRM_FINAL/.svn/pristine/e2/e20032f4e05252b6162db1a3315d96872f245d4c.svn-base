using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class AdslStatisticsDB
    {
        public static List<ADSLServiceInfo> SearchADSLStatistics(List<int> cities, List<int> centers, List<int> personTypes, List<int> adslCustomerGroups,
                                                       List<int> adslSaleWaies, List<int> adslServiceTypes, List<int> adslServiceGroups, List<int> bandWidth,
                                                       List<int> durations, List<int> traffics, List<int> adslServices, List<int> paymentTypes,
                                                       DateTime? fromEndDate, DateTime? toEndDate, int startsRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLServiceInfo> result = new List<ADSLServiceInfo>();
                var query = context.ADSLServices
                                   .GroupJoin(context.ADSLRequests, ads => ads.ID, adr => adr.ServiceID, (ads, adr) => new { AdslService = ads, AdslRequests = adr })
                                   .SelectMany(a => a.AdslRequests.DefaultIfEmpty(), (ads, adr) => new { AdslService = ads.AdslService, AdslRequest = adr })

                                   .GroupJoin(context.ADSLServiceBandWidths, a => a.AdslService.BandWidthID, adbw => adbw.ID, (a, adbw) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidths = adbw })
                                   .SelectMany(a => a.AdslBandWidths.DefaultIfEmpty(), (a, adbw) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = adbw })

                                   .GroupJoin(context.ADSLServiceDurations, a => a.AdslService.DurationID, adsd => adsd.ID, (a, adsd) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDurations = adsd })
                                   .SelectMany(a => a.AdslServiceDurations.DefaultIfEmpty(), (a, adsd) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDuration = adsd })

                                   .GroupJoin(context.ADSLServiceTraffics, a => a.AdslService.TrafficID, adst => adst.ID, (a, adst) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDuration = a.AdslServiceDuration, AdslServiceTraffics = adst })
                                   .SelectMany(a => a.AdslServiceTraffics.DefaultIfEmpty(), (a, adst) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDuration = a.AdslServiceDuration, AdslServiceTraffic = adst })

                                   .Where(a =>
                                                (a.AdslRequest.Request.EndDate.HasValue) &&
                                                (cities.Count == 0 || cities.Contains(a.AdslRequest.Request.Center.Region.CityID)) &&
                                                (centers.Count == 0 || centers.Contains(a.AdslRequest.Request.CenterID)) &&
                                                (personTypes.Count == 0 || personTypes.Contains((int)a.AdslRequest.Customer.PersonType)) &&
                                                (adslCustomerGroups.Count == 0 || (a.AdslRequest.CustomerGroupID.HasValue && adslCustomerGroups.Contains(a.AdslRequest.CustomerGroupID.Value))) &&
                                                    //(adslSaleWaies.Count == 0 || a.AdslService.SellChanell)-------------------???????
                                                (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(a.AdslService.TypeID)) &&
                                                (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(a.AdslService.GroupID)) &&
                                                (bandWidth.Count == 0 || bandWidth.Contains(a.AdslBandWidth.ID)) &&
                                                (durations.Count == 0 || durations.Contains(a.AdslServiceDuration.ID)) &&
                                                (traffics.Count == 0 || traffics.Contains(a.AdslServiceTraffic.ID)) &&
                                                (adslServices.Count == 0 || adslServices.Contains(a.AdslService.ID)) &&
                                                (paymentTypes.Count == 0 || paymentTypes.Contains(a.AdslService.PaymentTypeID)) &&
                                                (!fromEndDate.HasValue || fromEndDate <= a.AdslRequest.Request.EndDate) &&
                                                (!toEndDate.HasValue || toEndDate >= a.AdslRequest.Request.EndDate)
                                         )
                                   .AsQueryable();
                result = query.Select(a => new ADSLServiceInfo
                                               {
                                                   City = a.AdslRequest.Request.Center.Region.City.Name,
                                                   CenterName = a.AdslRequest.Request.Center.CenterName,
                                                   PersonTypeName = Helpers.GetEnumDescription((int)a.AdslRequest.Customer.PersonType, typeof(DB.PersonType)),
                                                   AdslCustomerGroupTitle = a.AdslRequest.ADSLCustomerGroup.Title,
                                                   SaleWay = a.AdslService.SellChanell,
                                                   AdslServiceType = Helpers.GetEnumDescription(a.AdslService.TypeID, typeof(DB.ADSLServiceCostPaymentType)),
                                                   AdslServiceGroup = a.AdslService.ADSLServiceGroup.Title,
                                                   BandWidth = a.AdslBandWidth.Title,
                                                   Duration = a.AdslServiceDuration.Title,
                                                   Traffic = a.AdslServiceTraffic.Title,
                                                   ServiceTitle = a.AdslService.Title,
                                                   ServiceCode = "-----",
                                                   PaymentType = Helpers.GetEnumDescription(a.AdslService.PaymentTypeID, typeof(DB.PaymentType)),
                                                   EndDate = a.AdslRequest.Request.EndDate,
                                                   EndDateString = a.AdslRequest.Request.EndDate.ToPersian(Date.DateStringType.Short)
                                               }
                                      )
                              .OrderByDescending(a => a.EndDate)
                              .Skip(startsRowIndex)
                              .Take(pageSize)
                              .ToList();

                //چون کانال فروش به صورت رشته ای از اعداد همراه با کاما ذخیره شده است باید اجزا آن را به معادل فارسی برگردانیم
                //حلقه زیر عمل فوق را پیاده سازی میکند
                result.ForEach(asi =>
                {
                    int[] salewaies = Helpers.StringToIntList(asi.SaleWay).ToArray();
                    if (salewaies.Length > 0)
                    {
                        foreach (int i in salewaies)
                        {
                            asi.SellChannel += Helpers.GetEnumDescription(i, typeof(DB.ADSLSaleWays)) + " , ";
                        }
                    }
                });

                count = query.Count();
                return result;
            }
        }

        public static List<ADSLServiceInfo> SearchADSLStatisticsHasGroupBy(List<int> cities, List<int> centers, List<int> personTypes, List<int> adslCustomerGroups,
                                                       List<int> adslSaleWaies, List<int> adslServiceTypes, List<int> adslServiceGroups, List<int> bandWidth,
                                                       List<int> durations, List<int> traffics, List<int> adslServices, List<int> paymentTypes,
                                                       DateTime? fromEndDate, DateTime? toEndDate, int startsRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLServiceInfo> result = new List<ADSLServiceInfo>();
                var query = context.ADSLServices
                                   .GroupJoin(context.ADSLRequests, ads => ads.ID, adr => adr.ServiceID, (ads, adr) => new { AdslService = ads, AdslRequests = adr })
                                   .SelectMany(a => a.AdslRequests.DefaultIfEmpty(), (ads, adr) => new { AdslService = ads.AdslService, AdslRequest = adr })

                                   .GroupJoin(context.ADSLServiceBandWidths, a => a.AdslService.BandWidthID, adbw => adbw.ID, (a, adbw) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidths = adbw })
                                   .SelectMany(a => a.AdslBandWidths.DefaultIfEmpty(), (a, adbw) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = adbw })

                                   .GroupJoin(context.ADSLServiceDurations, a => a.AdslService.DurationID, adsd => adsd.ID, (a, adsd) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDurations = adsd })
                                   .SelectMany(a => a.AdslServiceDurations.DefaultIfEmpty(), (a, adsd) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDuration = adsd })

                                   .GroupJoin(context.ADSLServiceTraffics, a => a.AdslService.TrafficID, adst => adst.ID, (a, adst) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDuration = a.AdslServiceDuration, AdslServiceTraffics = adst })
                                   .SelectMany(a => a.AdslServiceTraffics.DefaultIfEmpty(), (a, adst) => new { AdslService = a.AdslService, AdslRequest = a.AdslRequest, AdslBandWidth = a.AdslBandWidth, AdslServiceDuration = a.AdslServiceDuration, AdslServiceTraffic = adst })

                                   .Where(a =>
                                                (a.AdslRequest.Request.EndDate.HasValue) &&
                                                (cities.Count == 0 || cities.Contains(a.AdslRequest.Request.Center.Region.CityID)) &&
                                                (centers.Count == 0 || centers.Contains(a.AdslRequest.Request.CenterID)) &&
                                                (personTypes.Count == 0 || personTypes.Contains((int)a.AdslRequest.Customer.PersonType)) &&
                                                (adslCustomerGroups.Count == 0 || (a.AdslRequest.CustomerGroupID.HasValue && adslCustomerGroups.Contains(a.AdslRequest.CustomerGroupID.Value))) &&
                                                (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(a.AdslService.PaymentTypeID)) &&
                                                (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(a.AdslService.GroupID)) &&
                                                (bandWidth.Count == 0 || bandWidth.Contains(a.AdslBandWidth.ID)) &&
                                                (durations.Count == 0 || durations.Contains(a.AdslServiceDuration.ID)) &&
                                                (traffics.Count == 0 || traffics.Contains(a.AdslServiceTraffic.ID)) &&
                                                (adslServices.Count == 0 || adslServices.Contains(a.AdslService.ID)) &&
                                                (paymentTypes.Count == 0 || paymentTypes.Contains(a.AdslService.PaymentTypeID)) &&
                                                (!fromEndDate.HasValue || fromEndDate <= a.AdslRequest.Request.EndDate) &&
                                                (!a.AdslService.TrafficID.HasValue || a.AdslService.TrafficID.Value > 0) &&
                                                (!a.AdslService.BandWidthID.HasValue || a.AdslService.BandWidthID.Value > 0) &&
                                                (!a.AdslService.DurationID.HasValue || a.AdslService.DurationID.Value > 0) &&
                                                (!toEndDate.HasValue || toEndDate >= a.AdslRequest.Request.EndDate)
                                         )
                                    .GroupBy(a => new
                                    {
                                        AdslServiceID = a.AdslService.ID,
                                        PersonTypeID = a.AdslRequest.Customer.PersonType,
                                        CityID = a.AdslRequest.Request.Center.Region.CityID,
                                        CenterID = a.AdslRequest.Request.CenterID,
                                        AdslCustomerGroupID = a.AdslRequest.ADSLCustomerGroup.ID,
                                        AdslServiceTypeID = a.AdslService.PaymentTypeID,
                                        AdslServiceGroupID = a.AdslService.GroupID,
                                        PaymentTypeID = a.AdslService.PaymentTypeID
                                    })
                                   .AsQueryable();

                var secondQuery = query.Select(a => new ADSLServiceInfo
                                                    {
                                                        CustomersCount = context.ADSLs.Where(ad => (ad.TariffID.HasValue && ad.TariffID.Value == a.Key.AdslServiceID) &&
                                                                                                   (ad.Status == (byte)DB.ADSLStatus.Connect) &&
                                                                                                   a.Key.CenterID == ad.Telephone.CenterID).Count(),
                                                        City = context.Cities.Where(ci => ci.ID == a.Key.CityID).Select(ci => ci.Name).SingleOrDefault(),
                                                        CenterName = context.Centers.Where(ce => ce.ID == a.Key.CenterID).Select(ce => ce.CenterName).SingleOrDefault(),
                                                        PersonTypeName = Helpers.GetEnumDescription((int)a.Key.PersonTypeID, typeof(DB.PersonType)),
                                                        AdslCustomerGroupTitle = context.ADSLCustomerGroups.Where(adslcg => adslcg.ID == a.Key.AdslCustomerGroupID).Select(adslcg => adslcg.Title).SingleOrDefault(),
                                                        AdslServiceType = Helpers.GetEnumDescription(a.Key.AdslServiceTypeID, typeof(DB.ADSLServiceCostPaymentType)),
                                                        AdslServiceGroup = context.ADSLServiceGroups.Where(adsg => adsg.ID == a.Key.AdslServiceGroupID).Select(adsg => adsg.Title).SingleOrDefault(),
                                                        ServiceTitle = context.ADSLServices.Where(ads => ads.ID == a.Key.AdslServiceID).Select(ads => ads.Title).SingleOrDefault(),
                                                        BandWidth = context.ADSLServices.Where(ads => ads.ID == a.Key.AdslServiceID).Select(ads => ads.ADSLServiceBandWidth.Title).SingleOrDefault(),
                                                        Duration = context.ADSLServices.Where(ad => ad.ID == a.Key.AdslServiceID).Select(ad => ad.ADSLServiceDuration.Title).SingleOrDefault(),
                                                        Traffic = context.ADSLServices.Where(ad => ad.ID == a.Key.AdslServiceID).Select(ad => ad.ADSLServiceTraffic.Title).SingleOrDefault(),
                                                        PaymentType = Helpers.GetEnumDescription(a.Key.PaymentTypeID, typeof(DB.PaymentType)),
                                                        AdditionalTrafficDecimal = context.ADSLs.Where(ads => ads.TariffID == a.Key.AdslServiceID && ads.Status == (byte)DB.ADSLStatus.Connect && a.Key.CenterID == ads.Telephone.CenterID)
                                                                                          .GroupJoin(context.ADSLSellTraffics, ads => ads.TelephoneNo, st => st.Request.TelephoneNo, (ads, st) => new { Adsl = ads, AdslSellTraffics = st })
                                                                                          .SelectMany(b => b.AdslSellTraffics.DefaultIfEmpty(), (ads, st) => new { AdslService = ads.Adsl, AdslSellTraffic = st })
                                                                                          .Where(s => (s.AdslSellTraffic.ADSLService.TrafficID > 0) && (!fromEndDate.HasValue || fromEndDate <= s.AdslSellTraffic.Request.EndDate) && (!toEndDate.HasValue || toEndDate >= s.AdslSellTraffic.Request.EndDate))
                                                                                          .Select(s => s.AdslSellTraffic.ADSLService.ADSLServiceTraffic.Title)
                                                                                          .ToList()
                                                                                          .Sum(x => System.Convert.ToDecimal(x)),
                                                        SaleWay = context.ADSLServices.Where(ads => ads.ID == a.Key.AdslServiceID).Select(ads => ads.SellChanell).SingleOrDefault()
                                                    }
                                              )
                                      .AsQueryable();

                if (pageSize != 0)
                {
                    result = secondQuery.Skip(startsRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    result = secondQuery.ToList();
                }
                result.ForEach(adsi =>
                                      {
                                          adsi.TotalTraffic = (adsi.CustomersCount != 0) ? (adsi.CustomersCount * System.Convert.ToDecimal(adsi.Traffic)).ToString("F0") : "0";
                                          adsi.Duration = (System.Convert.ToInt32(adsi.Duration) != 0) ? string.Format("{1} {0}", "ماه", adsi.Duration) : "-----";
                                          adsi.AdditionalTraffic = (adsi.AdditionalTrafficDecimal.HasValue) ? adsi.AdditionalTrafficDecimal.Value.ToString("F0") : "0";
                                          adsi.CustomerUsedTrafficAverage = (adsi.CustomersCount == 0) ? "0" : (((System.Convert.ToDecimal(adsi.TotalTraffic) + System.Convert.ToDecimal(adsi.AdditionalTraffic)) / adsi.CustomersCount)).ToString("F0");
                                          //چون کانال فروش به صورت رشته ای از اعداد همراه با کاما ذخیره شده است باید اجزا آن را به معادل فارسی برگردانیم
                                          //حلقه زیر عمل فوق را پیاده سازی میکند
                                          int[] salewaies = Helpers.StringToIntList(adsi.SaleWay).ToArray();
                                          if (salewaies.Length > 0)
                                          {
                                              foreach (int i in salewaies)
                                              {
                                                  adsi.SellChannel += Helpers.GetEnumDescription(i, typeof(DB.ADSLSaleWays)) + " , ";
                                              }
                                          }
                                      }
                              );
                count = query.Count();

                return result;
            }
        }

        public static List<ADSLServiceInfo> SearchADSLLucrativeCustomer(List<int> cities, List<int> centers, bool isRealPerson, bool isLegalPerson, List<int> adslCustomerGroups, List<int> adslCustomerTypes,
                                                                        List<int> adslServiceTypes, List<int> adslServiceGroups, List<int> bandWidth,
                                                                        List<int> durations, List<int> traffics, List<int> adslServices, List<int> paymentTypes,
                                                                        DateTime? fromDate, DateTime? toDate,
                                                                        int startsRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLServiceInfo> result = new List<ADSLServiceInfo>();
                DateTime currentDate = DB.GetServerDate();

                var query = context.ADSLs
                                   .Where(a =>
                                               (a.Status == (int)DB.ADSLStatus.Connect) &&
                                               (!a.ADSLService.BandWidthID.HasValue || a.ADSLService.BandWidthID.Value > 0) &&
                                               (!a.ADSLService.DurationID.HasValue || a.ADSLService.DurationID.Value > 0) &&
                                               (!a.ADSLService.TrafficID.HasValue || a.ADSLService.TrafficID.Value > 0) &&
                                               (cities.Count == 0 || cities.Contains(a.Telephone.Center.Region.CityID)) &&
                                               (centers.Count == 0 || centers.Contains(a.Telephone.CenterID)) &&
                                               (
                                                isRealPerson ? a.Customer.PersonType == (byte)DB.PersonType.Person :
                                                isLegalPerson ? a.Customer.PersonType == (byte)DB.PersonType.Company :
                                                (1 == 1)
                                               ) &&
                                               (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(a.ADSLService.PaymentTypeID)) &&
                                               (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(a.ADSLService.GroupID)) &&
                                               (bandWidth.Count == 0 || (a.ADSLService.BandWidthID.HasValue && bandWidth.Contains(a.ADSLService.BandWidthID.Value))) &&
                                               (durations.Count == 0 || (a.ADSLService.DurationID.HasValue && durations.Contains(a.ADSLService.DurationID.Value))) &&
                                               (traffics.Count == 0 || (a.ADSLService.TrafficID.HasValue && traffics.Contains(a.ADSLService.TrafficID.Value))) &&
                                               (adslServices.Count == 0 || adslServices.Contains(a.ADSLService.ID)) &&
                                               (paymentTypes.Count == 0 || paymentTypes.Contains(a.ADSLService.PaymentTypeID)) &&
                                               (adslCustomerTypes.Count == 0 || (a.CustomerTypeID.HasValue && adslCustomerTypes.Contains(a.CustomerTypeID.Value))) &&
                                               (adslCustomerGroups.Count == 0 || adslCustomerGroups.Contains(context.ADSLRequests.Where(t3 => t3.Request.TelephoneNo == a.TelephoneNo).Take(1).Select(t3 => t3.CustomerGroupID ?? 0).SingleOrDefault())))
                                    .AsQueryable();

                var secondQuery = query.Select(a => new ADSLServiceInfo
                {
                    TelephoneNo = a.TelephoneNo.ToString(),
                    CustomerName = (a.CustomerOwnerID.HasValue) ? string.Format("{0} {1}", a.Customer.FirstNameOrTitle, a.Customer.LastName) : "-----",
                    City = a.Telephone.Center.Region.City.Name,
                    CenterName = a.Telephone.Center.CenterName,
                    ServiceTitle = (a.TariffID.HasValue) ? context.ADSLServices.Where(ads => ads.ID == a.TariffID).Select(ads => ads.Title).SingleOrDefault() : "-----",
                    NumberOfAllServices = context.Requests.Where(t => t.TelephoneNo == a.TelephoneNo &&
                                                                      (t.RequestTypeID == (int)DB.RequestType.ADSL || t.RequestTypeID == (int)DB.RequestType.ADSLChangeService) &&
                                                                      t.EndDate.HasValue &&
                                                                      (!fromDate.HasValue || fromDate <= t.EndDate) &&
                                                                      (!toDate.HasValue || toDate >= t.EndDate)).Count(),
                    AmountOfAllServices = context.RequestPayments.Where(t2 => t2.Request.TelephoneNo == a.TelephoneNo &&
                                                                              (t2.Request.RequestTypeID == (int)DB.RequestType.ADSL || t2.Request.RequestTypeID == (int)DB.RequestType.ADSLChangeService) &&
                                                                              t2.Request.EndDate.HasValue &&
                                                                              (!fromDate.HasValue || fromDate <= t2.Request.EndDate) &&
                                                                              (!toDate.HasValue || toDate >= t2.Request.EndDate) &&
                                                                              t2.IsKickedBack.HasValue && !t2.IsKickedBack.Value)
                                                                 .Sum(t2 => t2.AmountSum) ?? 0,
                    TotalOfAllTrafficsDeciaml = context.Requests.Where(re => re.TelephoneNo == a.TelephoneNo &&
                                                                             re.RequestTypeID == (int)DB.RequestType.ADSLSellTraffic &&
                                                                             re.EndDate.HasValue &&
                                                                             (!fromDate.HasValue || fromDate <= re.EndDate) &&
                                                                             (!toDate.HasValue || toDate >= re.EndDate) &&
                                                                             a.ADSLService.TrafficID > 0)
                                                                .Select(re => re.ADSLSellTraffic.ADSLService.ADSLServiceTraffic.Title)
                                                                .Sum(re => Convert.ToDecimal(re)),
                    AmountOfAllTraffics = context.RequestPayments.Where(rp => rp.Request.TelephoneNo == a.TelephoneNo &&
                                                                             rp.Request.RequestTypeID == (int)DB.RequestType.ADSLSellTraffic &&
                                                                             rp.Request.EndDate.HasValue &&
                                                                             (!fromDate.HasValue || fromDate <= rp.Request.EndDate) &&
                                                                             (!toDate.HasValue || toDate >= rp.Request.EndDate) &&
                                                                             rp.IsPaid.HasValue && rp.IsPaid.Value &&
                                                                             rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value)
                                                                 .Sum(rp => rp.AmountSum),
                    AmountOfAllPurchasedIp = context.RequestPayments.Where(rp => rp.Request.TelephoneNo == a.TelephoneNo &&
                                                                                 rp.Request.RequestTypeID == (int)DB.RequestType.ADSLChangeIP &&
                                                                                 rp.Request.EndDate.HasValue &&
                                                                                 (!fromDate.HasValue || fromDate <= rp.Request.EndDate) &&
                                                                                 (!toDate.HasValue || toDate >= rp.Request.EndDate) &&
                                                                                 rp.IsPaid.HasValue && rp.IsPaid.Value &&
                                                                                 rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value &&
                                                                                 rp.BaseCostID == 48)
                                                                    .Sum(rp => rp.AmountSum),
                    AmountOfAllPurchasedModem = context.RequestPayments.Where(rp => rp.Request.TelephoneNo == a.TelephoneNo &&
                                                                                    rp.Request.RequestTypeID == (int)DB.RequestType.ADSL &&
                                                                                    rp.Request.EndDate.HasValue &&
                                                                                    (!fromDate.HasValue || fromDate <= rp.Request.EndDate) &&
                                                                                    (!toDate.HasValue || toDate >= rp.Request.EndDate) &&
                                                                                    rp.Request.ADSLRequest.NeedModem.HasValue && rp.Request.ADSLRequest.NeedModem.Value &&
                                                                                    rp.IsPaid.HasValue && rp.IsPaid.Value &&
                                                                                    rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value &&
                                                                                    rp.BaseCostID == 40)
                                                                       .Sum(rp => rp.AmountSum),
                    InUseServiceByDay = a.InsertDate.HasValue ?
                                        (a.ExpDate.HasValue ?
                                                                 (int)Math.Round((a.ExpDate.Value - a.InsertDate.Value).TotalDays, MidpointRounding.AwayFromZero) :
                                                                 (int)Math.Round((currentDate - a.InsertDate.Value).TotalDays, MidpointRounding.AwayFromZero)) :
                                                                 default(int?),
                    StartDateString = a.InsertDate.ToPersian(Date.DateStringType.Short),
                    EndDateString = (a.ExpDate.HasValue) ? a.ExpDate.ToPersian(Date.DateStringType.Short) : string.Empty
                }).AsQueryable();

                if (pageSize != 0)
                {
                    result = secondQuery.Skip(startsRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    result = secondQuery.ToList();
                }
                result.ForEach(adsi =>
                                    {
                                        adsi.TotalOfAllTraffics = (adsi.TotalOfAllTrafficsDeciaml.HasValue) ? adsi.TotalOfAllTrafficsDeciaml.Value.ToString("F0") : "0";
                                        adsi.TotalAmount = (adsi.AmountOfAllPurchasedIp.HasValue ? adsi.AmountOfAllPurchasedIp.Value : 0) +
                                                           (adsi.AmountOfAllPurchasedModem.HasValue ? adsi.AmountOfAllPurchasedModem.Value : 0) +
                                                           (adsi.AmountOfAllServices.HasValue ? adsi.AmountOfAllServices.Value : 0) +
                                                           (adsi.AmountOfAllTraffics.HasValue ? adsi.AmountOfAllTraffics.Value : 0);
                                    }
                              );
                count = query.Count();
                return result;
            }
        }

        public static List<ADSLServiceSellInfo> SearchADSLServiceSell(List<int> cities, List<int> centers, List<int> personTypes,
                                                                      List<int> adslCustomerGroups, List<int> adslServiceTypes, List<int> adslServiceGroups,
                                                                      List<int> bandWidth, List<int> traffics, List<int> durations, List<int> adslServices,
                                                                      List<int> paymentTypes, List<int> jobGroups, string serviceCode, DateTime? fromInsertDate, DateTime? toInsertDate, DateTime? fromEndDate, DateTime? toEndDate,
                                                                      int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLServiceSellInfo> firstSectionResult = new List<ADSLServiceSellInfo>();
                List<ADSLServiceSellInfo> secondSectionResult = new List<ADSLServiceSellInfo>();
                List<ADSLServiceSellInfo> unionResult = new List<ADSLServiceSellInfo>();

                var firstSectionResultQuery = context.Requests
                                                     .GroupJoin(context.ADSLModemProperties, re => re.ADSLRequest.ModemSerialNoID, admp => admp.ID, (re, admp) => new { Request = re, AdslModemProperties = admp })
                                                     .SelectMany(a => a.AdslModemProperties.DefaultIfEmpty(), (a, admp) => new { Request = a.Request, AdslModemProperty = admp })

                                                     .GroupJoin(context.JobGroups, a => a.Request.ADSLRequest.JobGroupID, jg => jg.ID, (a, jg) => new { Request = a.Request, AdslModemProperty = a.AdslModemProperty, JobGroups = jg })
                                                     .SelectMany(a => a.JobGroups.DefaultIfEmpty(), (a, jg) => new { Request = a.Request, AdslModemProperty = a.AdslModemProperty, JobGroup = jg })

                                                     .Where(a =>
                                                                (a.Request.EndDate.HasValue) &&
                                                                (a.Request.RequestTypeID == (int)DB.RequestType.ADSL) &&
                                                                (cities.Count == 0 || cities.Contains(a.Request.Center.Region.CityID)) &&
                                                                (centers.Count == 0 || centers.Contains(a.Request.CenterID)) &&
                                                                (personTypes.Count == 0 || personTypes.Contains(a.Request.ADSLRequest.Customer.PersonType)) &&
                                                                (adslCustomerGroups.Count == 0 || (a.Request.ADSLRequest.CustomerGroupID.HasValue && adslCustomerGroups.Contains(a.Request.ADSLRequest.CustomerGroupID.Value))) &&
                                                                (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(a.Request.ADSLRequest.ADSLService.PaymentTypeID)) &&
                                                                (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(a.Request.ADSLRequest.ADSLService.GroupID)) &&
                                                                (bandWidth.Count == 0 || (a.Request.ADSLRequest.ADSLService.BandWidthID.HasValue && bandWidth.Contains(a.Request.ADSLRequest.ADSLService.BandWidthID.Value))) &&
                                                                (traffics.Count == 0 || (a.Request.ADSLRequest.ADSLService.TrafficID.HasValue && traffics.Contains(a.Request.ADSLRequest.ADSLService.TrafficID.Value))) &&
                                                                (durations.Count == 0 || (a.Request.ADSLRequest.ADSLService.DurationID.HasValue && durations.Contains(a.Request.ADSLRequest.ADSLService.DurationID.Value))) &&
                                                                (adslServices.Count == 0 || (a.Request.ADSLRequest.ServiceID.HasValue && adslServices.Contains(a.Request.ADSLRequest.ServiceID.Value))) &&
                                                                (paymentTypes.Count == 0 || paymentTypes.Contains(a.Request.ADSLRequest.ADSLService.PaymentTypeID)) &&
                                                                (string.IsNullOrEmpty(serviceCode) || (a.Request.ADSLRequest.ADSLService.ServiceCode.HasValue && a.Request.ADSLRequest.ADSLService.ServiceCode.Value.ToString() == serviceCode)) &&
                                                                (!fromInsertDate.HasValue || fromInsertDate <= a.Request.InsertDate) &&
                                                                (!toInsertDate.HasValue || toInsertDate >= a.Request.InsertDate) &&
                                                                (!fromEndDate.HasValue || fromEndDate <= a.Request.EndDate) &&
                                                                (!toEndDate.HasValue || toEndDate >= a.Request.EndDate) &&
                                                                    //(jobGroups.Count == 0 || (a.Request.ADSLRequest.JobGroupID.HasValue && jobGroups.Contains(a.Request.ADSLRequest.JobGroupID.Value)))
                                                                (jobGroups.Count == 0 || jobGroups.Contains(a.JobGroup.ID))
                                                            )

                                                     .Select(a => new ADSLServiceSellInfo
                                                                    {
                                                                        City = a.Request.Center.Region.City.Name,
                                                                        CenterName = a.Request.Center.CenterName,
                                                                        CustomerName = string.Format("{0} {1}", a.Request.ADSLRequest.Customer.FirstNameOrTitle, a.Request.ADSLRequest.Customer.LastName),
                                                                        TelephoneNo = a.Request.TelephoneNo,
                                                                        NationalCodeOrRecordNo = a.Request.ADSLRequest.Customer.NationalCodeOrRecordNo,
                                                                        PersonType = Helpers.GetEnumDescription(a.Request.ADSLRequest.Customer.PersonType, typeof(DB.PersonType)),
                                                                        CustomerOwnStatus = Helpers.GetEnumDescription(a.Request.ADSLRequest.CustomerOwnerStatus, typeof(DB.ADSLOwnerStatus)),
                                                                        AdslServiceTitle = a.Request.ADSLRequest.ADSLService.Title,
                                                                        ServiceCode = a.Request.ADSLRequest.ADSLService.ServiceCode,
                                                                        PortNo = a.Request.ADSLRequest.ADSLPort.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " - " +
                                                                                 a.Request.ADSLRequest.ADSLPort.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " - " +
                                                                                 a.Request.ADSLRequest.ADSLPort.Bucht.BuchtNo.ToString(),
                                                                        InsertDate = a.Request.InsertDate.ToPersian(Date.DateStringType.DateTime),
                                                                        EndDate = a.Request.EndDate.ToPersian(Date.DateStringType.DateTime),
                                                                        RequesterName = string.Format("{0} {1}", a.Request.User.FirstName, a.Request.User.LastName),
                                                                        SerialNo = a.AdslModemProperty.SerialNo,
                                                                        AdslModemTitle = a.AdslModemProperty.ADSLModem.Title,
                                                                        ADSLServiceCostPaymentType = Helpers.GetEnumDescription(a.Request.ADSLRequest.ADSLService.PaymentTypeID, typeof(DB.ADSLServiceCostPaymentType)),
                                                                        MobileNo = a.Request.ADSLRequest.Customer.MobileNo,
                                                                        AmountSum = a.Request.RequestPayments
                                                                                             .Where(rp =>
                                                                                                          (rp.IsPaid.HasValue && rp.IsPaid.Value) &&
                                                                                                          (rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value) &&
                                                                                                          (rp.BaseCostID == 44) //سرویس ADSL
                                                                                                   )
                                                                                             .Sum(rp => rp.AmountSum) ?? 0,
                                                                        WorkFlow = "دایری",
                                                                        CustomerType = a.Request.ADSLRequest.ADSLCustomerType.Title,
                                                                        JobGroupTitle = a.JobGroup.Title
                                                                    }
                                                            );

                var secondSectionResultQuery = context.Requests
                                                      .GroupJoin(context.ADSLs, re => re.TelephoneNo, ad => ad.TelephoneNo, (re, ad) => new { Request = re, Adsls = ad })
                                                      .SelectMany(a => a.Adsls.DefaultIfEmpty(), (a, ad) => new { Request = a.Request, Adsl = ad })

                                                      .GroupJoin(context.ADSLModemProperties, a => a.Request.ADSLChangeService.ModemSerialNoID, admp => admp.ID, (a, admp) => new { Adsl = a.Adsl, Request = a.Request, AdslModemProperties = admp })
                                                      .SelectMany(a => a.AdslModemProperties.DefaultIfEmpty(), (a, admp) => new { Adsl = a.Adsl, Request = a.Request, AdslModemProperty = admp })

                                                      .GroupJoin(context.JobGroups, a => a.Request.ADSLRequest.JobGroupID, jg => jg.ID, (a, jg) => new { Adsl = a.Adsl, AdslModemProperty = a.AdslModemProperty, Request = a.Request, JobGroups = jg })
                                                      .SelectMany(a => a.JobGroups.DefaultIfEmpty(), (a, jg) => new { Adsl = a.Adsl, AdslModemProperty = a.AdslModemProperty, Request = a.Request, JobGroup = jg })

                                                      .Where(a =>
                                                                (a.Request.EndDate.HasValue) &&
                                                                (a.Request.RequestTypeID == (int)DB.RequestType.ADSLChangeService) &&
                                                                (cities.Count == 0 || cities.Contains(a.Request.Center.Region.CityID)) &&
                                                                (centers.Count == 0 || centers.Contains(a.Request.CenterID)) &&
                                                                (personTypes.Count == 0 || personTypes.Contains(a.Request.Customer.PersonType)) &&
                                                                    //(adslCustomerGroups.Count == 0 || (a.Request.ADSLRequest.CustomerGroupID.HasValue && adslCustomerGroups.Contains(a.Request.ADSLRequest.CustomerGroupID.Value))) &&
                                                                (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(a.Request.ADSLChangeService.ADSLService.PaymentTypeID)) &&
                                                                (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(a.Request.ADSLChangeService.ADSLService.GroupID)) &&
                                                                (bandWidth.Count == 0 || (a.Request.ADSLChangeService.ADSLService.BandWidthID.HasValue && bandWidth.Contains(a.Request.ADSLChangeService.ADSLService.BandWidthID.Value))) &&
                                                                (traffics.Count == 0 || (a.Request.ADSLChangeService.ADSLService.TrafficID.HasValue && traffics.Contains(a.Request.ADSLChangeService.ADSLService.TrafficID.Value))) &&
                                                                (durations.Count == 0 || (a.Request.ADSLChangeService.ADSLService.DurationID.HasValue && durations.Contains(a.Request.ADSLChangeService.ADSLService.DurationID.Value))) &&
                                                                (adslServices.Count == 0 || (a.Request.ADSLChangeService.NewServiceID.HasValue && adslServices.Contains(a.Request.ADSLChangeService.NewServiceID.Value))) &&
                                                                (paymentTypes.Count == 0 || paymentTypes.Contains(a.Request.ADSLChangeService.ADSLService.PaymentTypeID)) &&
                                                                (string.IsNullOrEmpty(serviceCode) || (a.Request.ADSLChangeService.ADSLService.ServiceCode.HasValue && a.Request.ADSLChangeService.ADSLService.ServiceCode.Value.ToString() == serviceCode)) &&
                                                                (!fromInsertDate.HasValue || fromInsertDate <= a.Request.InsertDate) &&
                                                                (!toInsertDate.HasValue || toInsertDate >= a.Request.InsertDate) &&
                                                                (!fromEndDate.HasValue || fromEndDate <= a.Request.EndDate) &&
                                                                (!toEndDate.HasValue || toEndDate >= a.Request.EndDate) &&
                                                                    //  (jobGroups.Count == 0 || (a.Request.ADSLRequest.JobGroupID.HasValue && jobGroups.Contains(a.Request.ADSLRequest.JobGroupID.Value)))
                                                                (jobGroups.Count == 0 || jobGroups.Contains(a.JobGroup.ID))
                                                            )

                                                      .Select(a => new ADSLServiceSellInfo
                                                                     {
                                                                         City = a.Request.Center.Region.City.Name,
                                                                         CenterName = a.Request.Center.CenterName,
                                                                         CustomerName = string.Format("{0} {1}", a.Request.Customer.FirstNameOrTitle, a.Request.Customer.LastName),
                                                                         TelephoneNo = a.Request.TelephoneNo,
                                                                         NationalCodeOrRecordNo = a.Request.Customer.NationalCodeOrRecordNo,
                                                                         PersonType = Helpers.GetEnumDescription(a.Request.Customer.PersonType, typeof(DB.PersonType)),
                                                                         CustomerOwnStatus = string.Empty,//TODO:rad مالک یا مستاجر بودن باید در این قسمت اضافه شود . در حال حاضر در جدول مربوطه چنین فیلدی وجود ندارد
                                                                         AdslServiceTitle = a.Request.ADSLChangeService.ADSLService.Title,
                                                                         ServiceCode = a.Request.ADSLChangeService.ADSLService.ServiceCode,
                                                                         PortNo = a.Adsl.ADSLPort.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " - " +
                                                                                  a.Adsl.ADSLPort.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " - " +
                                                                                  a.Adsl.ADSLPort.Bucht.BuchtNo.ToString(),
                                                                         InsertDate = a.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                         EndDate = a.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                                         RequesterName = string.Format("{0} {1}", a.Request.User.FirstName, a.Request.User.LastName),
                                                                         SerialNo = a.AdslModemProperty.SerialNo,
                                                                         AdslModemTitle = a.Request.ADSLChangeService.ADSLModem.Title,
                                                                         ADSLServiceCostPaymentType = Helpers.GetEnumDescription(a.Request.ADSLChangeService.ADSLService.PaymentTypeID, typeof(DB.ADSLServiceCostPaymentType)),
                                                                         MobileNo = a.Request.Customer.MobileNo,
                                                                         AmountSum = a.Request.RequestPayments
                                                                                              .Where(rp =>
                                                                                                           (rp.IsPaid.HasValue && rp.IsPaid.Value) &&
                                                                                                           (rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value) &&
                                                                                                           (rp.BaseCostID == 45) //ADSL - خرید شارژ مجدد 
                                                                                                    )
                                                                                              .Sum(rp => rp.AmountSum) ?? 0,
                                                                         WorkFlow = Helpers.GetEnumDescription(a.Request.ADSLChangeService.ChangeServiceActionType, typeof(DB.ADSLChangeServiceActionType)),
                                                                         CustomerType = a.Adsl.ADSLCustomerType.Title,
                                                                         JobGroupTitle = a.JobGroup.Title
                                                                     }
                                                            );

                firstSectionResult = firstSectionResultQuery.ToList();
                secondSectionResult = secondSectionResultQuery.ToList();

                if (pageSize != 0)
                {
                    unionResult = firstSectionResult.Union(secondSectionResult).Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    unionResult = firstSectionResult.Union(secondSectionResult).ToList();
                }

                int firstCount = firstSectionResultQuery.Count();
                int secondCount = secondSectionResultQuery.Count();
                count = firstCount + secondCount;

                return unionResult;
            }
        }

        public static List<ADSLRequestTimeInfo> SearchADSLRequestTime(List<int> cities, List<int> centers, List<int> personTypes,
                                                                      List<int> adslCustomerGroups, List<int> adslServiceTypes, List<int> adslServiceGroups,
                                                                      List<int> bandWidth, List<int> traffics, List<int> durations, List<int> adslServices,
                                                                      List<int> paymentTypes, DateTime? fromDate, DateTime? toDate,
                                                                      int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLRequestTimeInfo> result = context.ADSLRequests
                                                          .GroupJoin(context.ADSLs, re => re.Request.TelephoneNo, ad => ad.TelephoneNo, (re, ad) => new { ADSLRequest = re, Adsls = ad })
                                                          .SelectMany(a => a.Adsls.DefaultIfEmpty(), (a, ad) => new { ADSLRequest = a.ADSLRequest, Adsl = ad })

                                                  .Where(a =>
                                                             (a.ADSLRequest.Request.EndDate.HasValue) &&
                                                             (cities.Count == 0 || cities.Contains(a.ADSLRequest.Request.Center.Region.CityID)) &&
                                                             (centers.Count == 0 || centers.Contains(a.ADSLRequest.Request.CenterID)) &&
                                                             (personTypes.Count == 0 || personTypes.Contains(a.ADSLRequest.Customer.PersonType)) &&
                                                             (adslCustomerGroups.Count == 0 || (a.ADSLRequest.CustomerGroupID.HasValue && adslCustomerGroups.Contains(a.ADSLRequest.CustomerGroupID.Value))) &&
                                                             (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(a.ADSLRequest.ADSLService.PaymentTypeID)) &&
                                                             (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(a.ADSLRequest.ADSLService.GroupID)) &&
                                                             (bandWidth.Count == 0 || (a.ADSLRequest.ADSLService.BandWidthID.HasValue && bandWidth.Contains(a.ADSLRequest.ADSLService.BandWidthID.Value))) &&
                                                             (traffics.Count == 0 || (a.ADSLRequest.ADSLService.TrafficID.HasValue && traffics.Contains(a.ADSLRequest.ADSLService.TrafficID.Value))) &&
                                                             (durations.Count == 0 || (a.ADSLRequest.ADSLService.DurationID.HasValue && durations.Contains(a.ADSLRequest.ADSLService.DurationID.Value))) &&
                                                             (adslServices.Count == 0 || (a.ADSLRequest.ServiceID.HasValue && adslServices.Contains(a.ADSLRequest.ServiceID.Value))) &&
                                                             (paymentTypes.Count == 0 || paymentTypes.Contains(a.ADSLRequest.ADSLService.PaymentTypeID)) &&
                                                             (!fromDate.HasValue || fromDate <= a.ADSLRequest.Request.EndDate) &&
                                                             (!toDate.HasValue || toDate >= a.ADSLRequest.Request.EndDate))
                                                  .Select(a => new ADSLRequestTimeInfo
                                                  {
                                                      City = a.ADSLRequest.Request.Center.Region.City.Name,
                                                      CenterName = a.ADSLRequest.Request.Center.CenterName,
                                                      CustomerName = string.Format("{0} {1}", a.ADSLRequest.Request.ADSLRequest.Customer.FirstNameOrTitle, a.ADSLRequest.Request.ADSLRequest.Customer.LastName),
                                                      TelephoneNo = a.ADSLRequest.Request.TelephoneNo.ToString(),
                                                      ADSLServiceTitle = a.ADSLRequest.ADSLService.Title,
                                                      PaymentDateValue = context.RequestPayments.Where(t => t.RequestID == a.ADSLRequest.ID && t.BaseCostID == 107).SingleOrDefault().PaymentDate,
                                                      PaymentDate = context.RequestPayments.Where(t => t.RequestID == a.ADSLRequest.ID && t.BaseCostID == 107).SingleOrDefault().PaymentDate.ToPersian(Date.DateStringType.Short),
                                                      MDFDate = a.ADSLRequest.MDFDate.ToPersian(Date.DateStringType.Short),
                                                      MDFDateValue = a.ADSLRequest.MDFDate,
                                                      InstallDate = a.Adsl.InstallDate.ToPersian(Date.DateStringType.Short),
                                                      InstallDateValue = a.Adsl.InstallDate,
                                                      InstalationType = (a.ADSLRequest.RequiredInstalation != null && a.ADSLRequest.RequiredInstalation == true) ? "پشتیبان خارجی" : "مشترک"
                                                  }).ToList();
                foreach (ADSLRequestTimeInfo item in result)
                {
                    item.WatingMDFDate = (item.MDFDateValue.Value.Day >= item.PaymentDateValue.Value.Day) ? (item.MDFDateValue.Value.Day - item.PaymentDateValue.Value.Day).ToString() : (((item.MDFDateValue.Value.Day) * 30) - item.PaymentDateValue.Value.Day).ToString();
                    if (!string.IsNullOrWhiteSpace(item.InstallDate))
                        item.WaitingInstallDate = (item.InstallDateValue.Value.Day >= item.MDFDateValue.Value.Day) ? (item.InstallDateValue.Value.Day - item.MDFDateValue.Value.Day).ToString() : (((item.InstallDateValue.Value.Day) * 30) - item.MDFDateValue.Value.Day).ToString();
                    else
                        item.WaitingInstallDate = "اتصال نیافته";
                }

                if (pageSize != 0)
                    result = result.Skip(startRowIndex).Take(pageSize).ToList();
                else
                    result = result.ToList();

                count = result.Count();

                return result;
            }
        }

        public static List<ADSlServiceBandWidthInfo> SearchADSlServiceBandWidthInfos(List<int> cities, List<int> centers, List<int> bandWidthes, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSlServiceBandWidthInfo> result = new List<ADSlServiceBandWidthInfo>();

                //**********************************************************************************************************
                string _cities = (cities != null && cities.Count != 0) ? string.Join(",", cities.ToArray()) : string.Empty;
                string _centers = (centers != null && centers.Count != 0) ? string.Join(",", centers.ToArray()) : string.Empty;
                string _bandWidthes = (bandWidthes != null && bandWidthes.Count != 0) ? string.Join(",", bandWidthes.ToArray()) : string.Empty;
                string _toDate = (toDate.HasValue) ? toDate.Value.ToShortDateString() : string.Empty;
                //**********************************************************************************************************
                string query = @";WITH GroupedData AS
                                        (
                                        	SELECT 
                                        		CI.ID CityID,
                                        		CE.ID CenterID,
                                        		ADB.ID BandWidthID,
                                        		COUNT(ADP.ID) InstallCount,
                                        		COUNT(CASE WHEN ADL.ExpDate <= GETDATE() THEN ADP.ID END) ActiveCount

                                        	FROM 
                                        		Telephone TE 
                                        	LEFT JOIN 
                                        		ADSL ADL ON TE.TelephoneNo = ADL.TelephoneNo
                                        	LEFT join 
                                        		ADSLService ADS ON ADS.ID = ADL.TariffID
                                        	INNER JOIN 
                                        		ADSLServiceBandWidth ADB ON ADB.ID = ADS.BandWidthID
                                        	INNER JOIN 
                                        		ADSLPorts ADP ON ADP.ID = ADL.ADSLPortID AND ADP.[Status] = 1
                                        	INNER JOIN 
                                        		Center CE ON CE.ID = TE.CenterID
                                        	INNER JOIN 
                                        		Region REG ON REG.ID = CE.RegionID
                                        	INNER JOIN 
                                        		City CI ON CI.ID = REG.CityID
                                        	WHERE 
                                        		({0} IS NULL OR LEN({0}) = 0 OR CI.ID IN (SELECT * FROM dbo.ufnSplitList({0}))) 
                                        		AND
                                        		({1} IS NULL OR LEN({1}) = 0 OR CE.ID IN (SELECT * FROM  dbo.ufnSplitList({1})))
                                                AND
                                                ({2} IS NULL OR LEN({2}) = 0 OR ADB.ID IN (SELECT * FROM  dbo.ufnSplitList({2})))
                                        		AND
                                        		({3} IS NULL OR LEN({3}) = 0 OR {3} >= ADL.InsertDate)
                                        	GROUP BY
                                        		CI.ID,CE.ID,ADB.ID
                                        )
                                        SELECT 
                                        	CI.Name CityName,
                                        	CE.CenterName CenterName,                                            
                                        	ADB.Title BandWidth,
                                        	C.InstallCount InstalledPortCount,
                                        	C.ActiveCount ActivePortCount
                                        FROM 
                                        	GroupedData C
                                        INNER JOIN
                                        	City CI ON C.CityID = CI.ID
                                        INNER JOIN 
                                        	Center CE ON CE.ID = C.CenterID
                                        INNER JOIN
                                        	ADSLServiceBandWidth ADB ON ADB.ID = C.BandWidthID
                                        ORDER BY 
                                        	CI.NAME,CE.CENTERNAME,ADB.ID"
                                        ;

                //**********************************************************************************************************

                var resultQuery = context.ExecuteQuery<ADSlServiceBandWidthInfo>(query, _cities, _centers, _bandWidthes, _toDate).AsQueryable();
                var countQuery = context.ExecuteQuery<ADSlServiceBandWidthInfo>(query, _cities, _centers, _bandWidthes, _toDate).AsQueryable();

                if (pageSize != 0)
                {
                    result = resultQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    result = resultQuery.ToList();
                }


                result.ForEach(asbw => { asbw.PersianToDate = asbw.ToDate.ToPersian(Date.DateStringType.Short); });
                count = countQuery.Count();
                return result;
            }
        }

        public static List<ADSLServiceInfo> SearchADSLServiceCollectionTimeAverage(List<int> cities, List<int> centers, List<int> personTypes,
                                                                                   int fromDaysCount, int toDaysCount, List<int> adslCustomerTypes,
                                                                                   List<int> adslServiceTypes, List<int> adslServiceGroups, List<int> adslCustomerGroups,
                                                                                   List<int> paymentTypes, List<int> bandWidthes, List<int> durations,
                                                                                   List<int> traffics, List<int> adslServices, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLServiceInfo> result = new List<ADSLServiceInfo>();
                var firstQuery = context.Requests
                                        .Join(context.ADSLs, re => re.TelephoneNo, adl => adl.TelephoneNo, (re, adl) => new { Request = re, Adsl = adl })

                                        .Where(a =>
                                                    (a.Request.RequestTypeID == (int)DB.RequestType.ADSLDischarge) && //تخلیه ای دی اس ال
                                                    (a.Adsl.TariffID.HasValue) &&
                                                    (a.Request.EndDate.HasValue) &&
                                                    (personTypes.Count == 0 || personTypes.Contains(a.Adsl.Customer.PersonType)) &&
                                                    (cities.Count == 0 || cities.Contains(a.Request.Center.Region.CityID)) &&
                                                    (centers.Count == 0 || centers.Contains(a.Request.CenterID)) &&
                                                    (fromDaysCount == 0 || (a.Request.EndDate.Value - a.Request.InsertDate).TotalDays >= fromDaysCount) &&
                                                    (toDaysCount == 0 || (a.Request.EndDate.Value - a.Request.InsertDate).TotalDays <= toDaysCount) &&
                                                    (adslCustomerTypes.Count == 0 || (a.Adsl.CustomerTypeID.HasValue && adslCustomerTypes.Contains(a.Adsl.CustomerTypeID.Value))) &&
                                                    (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(a.Adsl.ADSLService.TypeID)) &&
                                                    (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(a.Adsl.ADSLService.GroupID)) &&
                                                    (adslCustomerGroups.Count == 0 || (a.Adsl.CustomerGroupID.HasValue && adslCustomerGroups.Contains(a.Adsl.CustomerGroupID.Value))) &&
                                                    (paymentTypes.Count == 0 || paymentTypes.Contains(a.Adsl.ADSLService.PaymentTypeID)) &&
                                                    (bandWidthes.Count == 0 || (a.Adsl.ADSLService.BandWidthID.HasValue && bandWidthes.Contains(a.Adsl.ADSLService.BandWidthID.Value))) &&
                                                    (durations.Count == 0 || (a.Adsl.ADSLService.DurationID.HasValue && durations.Contains(a.Adsl.ADSLService.DurationID.Value))) &&
                                                    (traffics.Count == 0 || (a.Adsl.ADSLService.TrafficID.HasValue && traffics.Contains(a.Adsl.ADSLService.TrafficID.Value))) &&
                                                    (adslServices.Count == 0 || adslServices.Contains(a.Adsl.TariffID.Value)) &&
                                                    (!fromDate.HasValue || fromDate <= a.Request.EndDate) &&
                                                    (!toDate.HasValue || toDate >= a.Request.EndDate)
                                              )
                                        .AsQueryable();
                var secondQuery = firstQuery.Select(a => new ADSLServiceInfo
                                                            {
                                                                TelephoneNo = a.Adsl.TelephoneNo.ToString(),
                                                                CustomerName = string.Format("{0} {1}", a.Adsl.Customer.FirstNameOrTitle, a.Adsl.Customer.LastName),
                                                                ServiceTitle = a.Adsl.ADSLService.Title,
                                                                City = a.Request.Center.Region.City.Name,
                                                                Center = a.Request.Center.CenterName,
                                                                WaitingHoursUnitlExitFromMDF = (a.Request.EndDate.Value - a.Request.InsertDate).TotalHours,
                                                                RequestDate = a.Request.InsertDate.ToPersian(Date.DateStringType.DateTime),
                                                                EndDateString = a.Request.EndDate.ToPersian(Date.DateStringType.DateTime)
                                                            }
                                                    )
                                            .AsQueryable();

                if (pageSize != 0)
                {
                    result = secondQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    result = secondQuery.ToList();
                }

                //مدت زمان انتظار برای خروج از ام دی اف باید به صورت تعداد روز و تعداد ساعت محاسبه شود. چون ممکن است درخواست مشترک در کمتر از 24 ساعت اجرا شود
                //result.ForEach(asi =>
                //                    {
                //                        int days = TimeSpan.FromHours(asi.WaitingHoursUnitlExitFromMDF.Value).Days;
                //                        int hours = (int)(asi.WaitingHoursUnitlExitFromMDF.Value % 24);
                //                        asi.WaitingDurationUnitlExitFromMDF = string.Format("تعداد روز : {0} - تعداد ساعت : {1} ", days, hours);
                //                    });
                result.ForEach(asi =>
                {
                    asi.WaitingHoursUnitlExitFromMDF = Math.Round(asi.WaitingHoursUnitlExitFromMDF);
                });
                count = firstQuery.Count();

                return result;
            }
        }

        public static List<AdslPapPortStatisticsInfo> SearchAdslPapPortStatisticsInfo(List<int> cities, List<int> centers, List<int> papInfos, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<AdslPapPortStatisticsInfo> finalResult = new List<AdslPapPortStatisticsInfo>();
                var firstQuery = context.PAPInfos
                                        .GroupJoin(context.ADSLPAPPorts, pi => pi.ID, adp => adp.PAPInfoID, (pi, adp) => new { PapInfo = pi, AdslPapPorts = adp })
                                        .SelectMany(a => a.AdslPapPorts.DefaultIfEmpty(), (a, adp) => new { PapInfo = a.PapInfo, AdslPapPort = adp })
                                        .Where(a =>
                                                  (cities.Count == 0 || cities.Contains(a.AdslPapPort.Center.Region.CityID)) &&
                                                  (centers.Count == 0 || centers.Contains(a.AdslPapPort.CenterID)) &&
                                                  (papInfos.Count == 0 || papInfos.Contains(a.PapInfo.ID))
                                              )
                                        .GroupBy(a => new { CityID = a.AdslPapPort.Center.Region.CityID, CenterID = a.AdslPapPort.CenterID, PapInfoID = a.PapInfo.ID })
                                        .Select(result => new AdslPapPortStatisticsInfo
                                                        {
                                                            CityName = result.Select(subResult => subResult.AdslPapPort.Center.Region.City.Name).FirstOrDefault(),
                                                            CenterName = result.Select(subResult => subResult.AdslPapPort.Center.CenterName).FirstOrDefault(),
                                                            PapInfoTitle = result.Select(subResult => subResult.PapInfo.Title).FirstOrDefault(),
                                                            TotalCount = result.Select(subResult => subResult.PapInfo.ADSLPAPPorts.Count()).FirstOrDefault(),
                                                            InstallCount = context.ADSLPAPRequests.Where(t =>
                                                                                                             t.Request.CenterID == result.Key.CenterID &&
                                                                                                             t.PAPInfoID == result.Key.PapInfoID &&
                                                                                                             t.RequestTypeID == (int)DB.RequestType.ADSLInstalPAPCompany && //دایری
                                                                                                             (t.Request.EndDate.HasValue) &&
                                                                                                             (!fromDate.HasValue || t.Request.EndDate >= fromDate) &&
                                                                                                             (!toDate.HasValue || t.Request.EndDate <= toDate)
                                                                                                         )
                                                                                                  .Count(),
                                                            DischargeCount = context.ADSLPAPRequests.Where(t =>
                                                                                                              t.Request.CenterID == result.Key.CenterID &&
                                                                                                              t.PAPInfoID == result.Key.PapInfoID &&
                                                                                                              t.RequestTypeID == (int)DB.RequestType.ADSLDischargePAPCompany && //تخلیه
                                                                                                              (t.Request.EndDate.HasValue) &&
                                                                                                              (!fromDate.HasValue || t.Request.EndDate >= fromDate) &&
                                                                                                              (!toDate.HasValue || t.Request.EndDate <= toDate)
                                                                                                            )
                                                                                                      .Count(),
                                                            InUseCount = context.ADSLPAPPorts.Where(t =>
                                                                                                        t.CenterID == result.Key.CenterID &&
                                                                                                        t.PAPInfoID == result.Key.PapInfoID &&
                                                                                                        (t.InstallDate.HasValue) &&
                                                                                                        (!fromDate.HasValue || t.InstallDate >= fromDate) &&
                                                                                                        (!toDate.HasValue || t.InstallDate <= toDate)
                                                                                                  )
                                                                                            .Count(),
                                                            FromDate = (fromDate.HasValue) ? fromDate.Value.ToPersian(Date.DateStringType.Short) : string.Empty,
                                                            ToDate = (toDate.HasValue) ? toDate.Value.ToPersian(Date.DateStringType.Short) : string.Empty
                                                        }
                                               )
                                        .AsQueryable();
                if (pageSize != 0)
                {
                    finalResult = firstQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    finalResult = firstQuery.ToList();
                }

                count = firstQuery.Count();
                return finalResult.OrderBy(fr => fr.CityName).OrderBy(fr => fr.CenterName).ToList();
            }
        }

        public static List<AdslPortStatisticsInfo> SearchAdslPortStatisticsInfo(List<int> cities, List<int> centers, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<AdslPortStatisticsInfo> finalResult = new List<AdslPortStatisticsInfo>();

                var firstQuery = context.ADSLPorts
                                        .GroupJoin(context.Buchts, adp => adp.InputBucht, bu => bu.ID, (adp, bus) => new { AdslPort = adp, Buchts = bus })
                                        .SelectMany(a => a.Buchts.DefaultIfEmpty(), (adp, bu) => new { AdslPort = adp.AdslPort, Bucht = bu })
                                        .Where(a =>
                                                  (cities.Count == 0 || cities.Contains(a.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
                                                  (centers.Count == 0 || centers.Contains(a.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
                                              )
                                        .GroupBy(a => new
                                                    {
                                                        CityID = a.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID,
                                                        CenterID = a.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID
                                                    }
                                                )
                                        .Select(result => new AdslPortStatisticsInfo
                                                          {
                                                              CityName = result.Select(subResult => subResult.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.City.Name).FirstOrDefault(),
                                                              CenterName = result.Select(subResult => subResult.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.CenterName).FirstOrDefault(),
                                                              TotalCount = result.Count(),
                                                              InstallCount = context.ADSLRequests
                                                                                    .Where(adr =>
                                                                                                 (adr.Request.RequestTypeID == (byte)DB.RequestType.ADSL) &&
                                                                                                 (adr.Request.EndDate.HasValue) &&
                                                                                                 (adr.Request.CenterID == result.Key.CenterID) &&
                                                                                                 (!fromDate.HasValue || fromDate <= adr.Request.EndDate) &&
                                                                                                 (!toDate.HasValue || toDate >= adr.Request.EndDate)
                                                                                          )
                                                                                    .Count(),
                                                              InUseCount = result.Count(subResult => subResult.AdslPort.TelephoneNo.HasValue && subResult.AdslPort.Status == (byte)DB.ADSLPortStatus.Install),
                                                              DestructionCount = result.Count(subResult => subResult.AdslPort.Status == (byte)DB.ADSLPortStatus.Destruction),
                                                              DischargeCount = context.ADSLDischarges
                                                                                      .Where(ad =>
                                                                                                  (ad.Request.RequestTypeID == (byte)DB.RequestType.ADSLDischarge) &&
                                                                                                  (ad.Request.EndDate.HasValue) &&
                                                                                                  (ad.Request.CenterID == result.Key.CenterID) &&
                                                                                                  (!fromDate.HasValue || fromDate <= ad.Request.EndDate) &&
                                                                                                  (!toDate.HasValue || toDate >= ad.Request.EndDate)
                                                                                            )
                                                                                      .Count(),
                                                              FromDate = (fromDate.HasValue) ? fromDate.Value.ToPersian(Date.DateStringType.Short) : string.Empty,
                                                              ToDate = (toDate.HasValue) ? toDate.Value.ToPersian(Date.DateStringType.Short) : string.Empty
                                                          }
                                               )
                                        .AsQueryable();

                if (pageSize != 0)
                {
                    finalResult = firstQuery.Skip(startRowIndex).Take(pageSize).OrderBy(fr => fr.CityName).OrderBy(fr => fr.CenterName).ToList();
                }
                else
                {
                    finalResult = firstQuery.OrderBy(fr => fr.CityName).OrderBy(fr => fr.CenterName).ToList();
                }

                count = firstQuery.Count();
                return finalResult;
            }
        }

        public static List<ADSLSellItem> SearchADSLSellItems(List<int> cities, List<int> centers, long? telephoneNo, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.CommandTimeout = 1200;
                List<ADSLSellItem> finalResult = new List<ADSLSellItem>();
                var firstQuery = context.Telephones
                                        .Join(context.ADSLs, te => te.TelephoneNo, ad => ad.TelephoneNo, (te, ad) => new { Telephone = te, Adsl = ad })
                                        .Where(te =>
                                                    (cities.Count == 0 || cities.Contains(te.Telephone.Center.Region.CityID)) &&
                                                    (centers.Count == 0 || centers.Contains(te.Telephone.CenterID)) &&
                                                    (!telephoneNo.HasValue || telephoneNo == te.Telephone.TelephoneNo)//te.Telephone.TelephoneNo == 2333361619
                                              )
                                        .Select(result => new ADSLSellItem
                                                             {
                                                                 TelephoneNo = result.Telephone.TelephoneNo,
                                                                 CustomerName = string.Format("{0} {1}", result.Adsl.Customer.FirstNameOrTitle, result.Adsl.Customer.LastName),
                                                                 CityName = result.Telephone.Center.Region.City.Name,
                                                                 CenterName = result.Telephone.Center.CenterName,
                                                                 SellServiceByInternetAmount = context.Requests
                                                                                                       .Where(re =>
                                                                                                                   (re.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                                   (re.RequestTypeID == (int)DB.RequestType.ADSLChangeService) &&
                                                                                                                   (re.EndDate.HasValue) &&
                                                                                                                   (re.ADSLChangeService.ChangeServiceType == (int)DB.ADSLChangeServiceType.Internet) &&
                                                                                                                   (!fromDate.HasValue || fromDate <= re.EndDate) &&
                                                                                                                   (!toDate.HasValue || toDate >= re.EndDate)
                                                                                                             )
                                                                                                       .Select(re => re.RequestPayments.Sum(rp => rp.AmountSum))
                                                                                                       .Sum() ?? 0,
                                                                 SellServiceByPresenceAmountPart1 = context.Requests
                                                                                                           .Where(re =>
                                                                                                                     (re.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                                     (re.RequestTypeID == (int)DB.RequestType.ADSLChangeService) &&
                                                                                                                     (re.EndDate.HasValue) &&
                                                                                                                     (re.ADSLChangeService.ChangeServiceType == (int)DB.ADSLChangeServiceType.Presence) &&
                                                                                                                     (!fromDate.HasValue || fromDate <= re.EndDate) &&
                                                                                                                     (!toDate.HasValue || toDate >= re.EndDate)
                                                                                                                 )
                                                                                                           .Select(re => re.RequestPayments.Sum(rp => rp.AmountSum))
                                                                                                           .Sum() ?? 0,
                                                                 SellServiceByPresenceAmountPart2 = context.ADSLRequests
                                                                                                           .Where(adr =>
                                                                                                                        (adr.Request.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                                        (adr.Request.EndDate.HasValue) &&
                                                                                                                        (!fromDate.HasValue || fromDate <= adr.Request.EndDate) &&
                                                                                                                        (!toDate.HasValue || toDate >= adr.Request.EndDate)
                                                                                                                  )
                                                                                                           .Select(adr => adr.Request.RequestPayments.Where(rp => rp.BaseCostID == 44).Sum(rp => rp.AmountSum))
                                                                                                           .Sum() ?? 0,
                                                                 SellTrafficByInternetAmount = context.ADSLSellTraffics
                                                                                                      .Where(adst =>
                                                                                                                  (adst.Request.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                                  (adst.Request.EndDate.HasValue) &&
                                                                                                                  (adst.ChangeServiceType == (int)DB.ADSLChangeServiceType.Internet) &&
                                                                                                                  (!fromDate.HasValue || fromDate <= adst.Request.EndDate) &&
                                                                                                                  (!toDate.HasValue || toDate >= adst.Request.EndDate)
                                                                                                            )
                                                                                                      .Select(adst => adst.Request.RequestPayments.Sum(rp => rp.AmountSum))
                                                                                                      .Sum() ?? 0,
                                                                 SellTrafficByPresenceAmountPart1 = context.ADSLSellTraffics
                                                                                                           .Where(adst =>
                                                                                                                         (adst.Request.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                                         (adst.Request.EndDate.HasValue) &&
                                                                                                                         (adst.ChangeServiceType == (int)DB.ADSLChangeServiceType.Presence) &&
                                                                                                                         (!fromDate.HasValue || fromDate <= adst.Request.EndDate) &&
                                                                                                                         (!toDate.HasValue || toDate >= adst.Request.EndDate)
                                                                                                                 )
                                                                                                           .Select(adst => adst.Request.RequestPayments.Sum(rp => rp.AmountSum))
                                                                                                           .Sum() ?? 0,
                                                                 SellTrafficByPresenceAmountPart2 = context.ADSLRequests
                                                                                                           .Where(adr =>
                                                                                                                       (adr.Request.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                                       (adr.Request.EndDate.HasValue) &&
                                                                                                                       (!fromDate.HasValue || fromDate <= adr.Request.EndDate) &&
                                                                                                                       (!toDate.HasValue || toDate >= adr.Request.EndDate)
                                                                                                                 )
                                                                                                           .Select(adr => adr.Request.RequestPayments.Where(rp => rp.BaseCostID == 46).Sum(rp => rp.AmountSum))
                                                                                                           .Sum() ?? 0,
                                                                 SellModemAmount = context.ADSLRequests
                                                                                          .Where(adr =>
                                                                                                     (adr.NeedModem.HasValue && adr.NeedModem.Value) &&
                                                                                                     (adr.Request.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                     (adr.Request.EndDate.HasValue) &&
                                                                                                     (!fromDate.HasValue || fromDate <= adr.Request.EndDate) &&
                                                                                                     (!toDate.HasValue || toDate >= adr.Request.EndDate)
                                                                                                )
                                                                                          .Select(adr => adr.Request.RequestPayments.Where(rp => rp.BaseCostID == 40).Sum(rp => rp.AmountSum))
                                                                                          .Sum() ?? 0,
                                                                 SellIPAmount = context.ADSLRequests
                                                                                       .Where(adr =>
                                                                                                   (adr.Request.EndDate.HasValue) &&
                                                                                                   (adr.Request.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                   (adr.HasIP.HasValue && adr.HasIP.Value) &&
                                                                                                   (!fromDate.HasValue || fromDate <= adr.Request.EndDate) &&
                                                                                                   (!toDate.HasValue || toDate >= adr.Request.EndDate)
                                                                                             )
                                                                                       .Select(adr => adr.Request.RequestPayments.Sum(rp => rp.AmountSum))
                                                                                       .Sum() ?? 0
                                                                                +
                                                                                 context.ADSLChangeIPRequests
                                                                                        .Where(adcr =>
                                                                                                    (adcr.Request.TelephoneNo == result.Telephone.TelephoneNo) &&
                                                                                                    (adcr.Request.EndDate.HasValue) &&
                                                                                                    (!fromDate.HasValue || fromDate <= adcr.Request.EndDate) &&
                                                                                                    (!toDate.HasValue || toDate >= adcr.Request.EndDate)
                                                                                              )
                                                                                        .Select(adcr => adcr.Request.RequestPayments.Sum(rp => rp.AmountSum))
                                                                                        .Sum() ?? 0
                                                             }
                                               )
                                        .AsQueryable();

                if (pageSize != 0)
                {
                    finalResult = firstQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    finalResult = firstQuery.ToList();
                }

                //چون برخی از مبالغ از چند قسمت تشکیل میشوند باید در پایان آنها را محاسبه کرد
                finalResult.ForEach(sai =>
                                            {
                                                sai.TotalSellServiceByPresenceAmount = sai.SellServiceByPresenceAmountPart1 + sai.SellServiceByPresenceAmountPart2;
                                                sai.TotalSellTrafficByPresenceAmount = sai.SellTrafficByPresenceAmountPart1 + sai.SellTrafficByPresenceAmountPart2;
                                                sai.TotalAmount = sai.SellIPAmount +
                                                                  sai.SellModemAmount +
                                                                  sai.SellServiceByInternetAmount +
                                                                  sai.SellTrafficByInternetAmount +
                                                                  sai.TotalSellServiceByPresenceAmount +
                                                                  sai.TotalSellTrafficByPresenceAmount;
                                            }
                                   );

                count = firstQuery.Count();
                return finalResult;
            }
        }

        public static List<ADSLSellerAgentStatisticsInfo> SearchADSLSellerAgentStatisticsInfo(List<int> cities, List<int> adslSellerAgents, DateTime? fromInsertDate, DateTime? toInsertDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLSellerAgentStatisticsInfo> finalResult = new List<ADSLSellerAgentStatisticsInfo>();
                var firstQuery = context.ADSLSellerAgents
                                        .GroupJoin(context.ADSLSellerAgentRecharges, asa => asa.ID, asr => asr.SellerAgentID, (asa, asd) => new { AdslSellerAgent = asa, AdslSellerAgentRecharges = asd })
                                        .SelectMany(a => a.AdslSellerAgentRecharges.DefaultIfEmpty(), (a, asd) => new { AdslSellerAgent = a.AdslSellerAgent, AdslSellerAgentRecharge = asd })
                                      .Where(result =>
                                                     (cities.Count == 0 || cities.Contains(result.AdslSellerAgent.CityID.Value)) &&
                                                     (adslSellerAgents.Count == 0 || adslSellerAgents.Contains(result.AdslSellerAgent.ID)) &&
                                                     (!fromInsertDate.HasValue || fromInsertDate <= result.AdslSellerAgentRecharge.InsertDate) &&
                                                     (!toInsertDate.HasValue || toInsertDate >= result.AdslSellerAgentRecharge.InsertDate)
                                            )
                                     .Select(result => new ADSLSellerAgentStatisticsInfo
                                                        {
                                                            CityName = (result.AdslSellerAgent.CityID.HasValue) ? result.AdslSellerAgent.City.Name : string.Empty,
                                                            ADSLSellerAgentTitle = result.AdslSellerAgent.Title,
                                                            ADSLSellerAgentCreditCash = result.AdslSellerAgent.CreditCash,
                                                            ADSLSellerAgentCreditCashUse = result.AdslSellerAgent.CreditCashUse,
                                                            ADSLSellerAgentCreditCashRemain = result.AdslSellerAgent.CreditCashRemain,
                                                            RechargeCost = result.AdslSellerAgentRecharge.Cost,
                                                            IsSellModem = (result.AdslSellerAgent.IsSellModem.HasValue) ?
                                                                          (result.AdslSellerAgent.IsSellModem.Value) ?
                                                                          "دارد" : "ندارد" : "نا مششخص",
                                                            RechargeInsertDate = result.AdslSellerAgentRecharge.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            RechargeUser = string.Format("{0} {1}", result.AdslSellerAgentRecharge.User.FirstName, result.AdslSellerAgentRecharge.User.LastName)
                                                        }
                                            )
                                    .AsQueryable();

                if (pageSize != 0)
                {
                    finalResult = firstQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    finalResult = firstQuery.ToList();
                }
                count = firstQuery.Count();
                return finalResult;
            }
        }

        public static List<ADSLSellerAgentUserStatisticsInfo> SearchADSLSellerAgentUserStatisticsInfo(List<int> cities, List<int> adslSellerAgents, List<int> adslSellerAgentUsers, DateTime? fromInsertDate, DateTime? toInsertDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLSellerAgentUserStatisticsInfo> finalResult = new List<ADSLSellerAgentUserStatisticsInfo>();

                var firstQuery = context.ADSLSellerAgentUsers
                                        .GroupJoin(context.ADSLSellerAgentUserRecharges, asu => asu.ID, asd => asd.SellerAgentUserID, (asu, asd) => new { AdslSellerAgentUser = asu, AdslSellerAgentUserRecharges = asd })
                                        .SelectMany(a => a.AdslSellerAgentUserRecharges.DefaultIfEmpty(), (a, asd) => new { AdslSellerAgentUser = a.AdslSellerAgentUser, AdslSellerAgentUserRecharge = asd })

                                        .Where(result =>
                                                       (cities.Count == 0 || cities.Contains(result.AdslSellerAgentUser.ADSLSellerAgent.CityID.Value)) &&
                                                       (adslSellerAgentUsers.Count == 0 || adslSellerAgentUsers.Contains(result.AdslSellerAgentUser.ID)) &&
                                                       (adslSellerAgents.Count == 0 || adslSellerAgents.Contains(result.AdslSellerAgentUser.SellerAgentID)) &&
                                                       (!fromInsertDate.HasValue || fromInsertDate <= result.AdslSellerAgentUserRecharge.InsertDate) &&
                                                       (!toInsertDate.HasValue || toInsertDate >= result.AdslSellerAgentUserRecharge.InsertDate)
                                              )

                                          .Select(result => new ADSLSellerAgentUserStatisticsInfo
                                                            {
                                                                CityName = (result.AdslSellerAgentUser.ADSLSellerAgent.CityID.HasValue) ? result.AdslSellerAgentUser.ADSLSellerAgent.City.Name : string.Empty,
                                                                ADSLSellerAgentTitle = result.AdslSellerAgentUser.ADSLSellerAgent.Title,
                                                                UserName = string.Format("{0} {1}", result.AdslSellerAgentUser.User.FirstName, result.AdslSellerAgentUser.User.LastName),
                                                                CreditCash = result.AdslSellerAgentUser.CreditCash,
                                                                CreditCashUse = result.AdslSellerAgentUser.CreditCashUse,
                                                                CreditCashRemain = result.AdslSellerAgentUser.CreditCashRemain,
                                                                RechargeCost = result.AdslSellerAgentUserRecharge.Cost,
                                                                IsSellModem = (result.AdslSellerAgentUser.ADSLSellerAgent.IsSellModem.HasValue) ?
                                                                              (result.AdslSellerAgentUser.ADSLSellerAgent.IsSellModem.Value) ?
                                                                              "دارد" : "ندارد" : "نا مششخص",
                                                                RechargeInsertDate = result.AdslSellerAgentUserRecharge.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                RechargeUser = string.Format("{0} {1}", result.AdslSellerAgentUserRecharge.User.FirstName, result.AdslSellerAgentUserRecharge.User.LastName)

                                                            }
                                                 )
                                    .AsQueryable();

                if (pageSize != 0)
                {
                    finalResult = firstQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    finalResult = firstQuery.ToList();
                }
                count = firstQuery.Count();
                return finalResult;
            }
        }

        public static List<ADSLSellerAgentUserTotalStatisticsInfo> SearchADSLSellerAgentUserTotalStatisticsInfos(List<int> cities, List<int> adslSellerAgents, List<int> adslSellerAgentUsers, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLSellerAgentUserTotalStatisticsInfo> finalResult = new List<ADSLSellerAgentUserTotalStatisticsInfo>();

                var firstQuery = context.ADSLSellerAgentUsers

                                        .GroupJoin(context.RequestPayments, asau => asau.ID, rp => rp.UserID, (asau, rps) => new { AdslSellerAgentUser = asau, RequestPayments = rps })
                                        .SelectMany(a => a.RequestPayments.DefaultIfEmpty(), (a, rp) => new { AdslSellerAgentUser = a.AdslSellerAgentUser, RequestPayment = rp })

                                        .GroupJoin(context.Requests, a => a.RequestPayment.RequestID, re => re.ID, (a, res) => new { AdslSellerAgentUser = a.AdslSellerAgentUser, RequestPayment = a.RequestPayment, Requests = res })
                                        .SelectMany(a => a.Requests.DefaultIfEmpty(), (a, re) => new { AdslSellerAgentUser = a.AdslSellerAgentUser, RequestPayment = a.RequestPayment, Request = re })

                                        .Where(result =>
                                                        (cities.Count == 0 || (result.AdslSellerAgentUser.ADSLSellerAgent.CityID.HasValue && cities.Contains(result.AdslSellerAgentUser.ADSLSellerAgent.CityID.Value))) &&
                                                        (adslSellerAgents.Count == 0 || adslSellerAgents.Contains(result.AdslSellerAgentUser.SellerAgentID)) &&
                                                        (adslSellerAgentUsers.Count == 0 || adslSellerAgentUsers.Contains(result.AdslSellerAgentUser.ID)) &&
                                                        (result.Request.EndDate.HasValue)
                                              )
                                        .GroupBy(result => new { AdslSellerAgentUserID = result.AdslSellerAgentUser.ID })

                                        .Select(result => new ADSLSellerAgentUserTotalStatisticsInfo
                                                          {
                                                              CityName = result.Select(subResult => subResult.AdslSellerAgentUser.ADSLSellerAgent.City.Name).FirstOrDefault(),
                                                              UserName = string.Format("{0} {1}", result.Select(subResult => subResult.AdslSellerAgentUser.User.FirstName).FirstOrDefault(), result.Select(subResult => subResult.AdslSellerAgentUser.User.LastName).FirstOrDefault()),
                                                              ADSLSellerAgentTitle = result.Select(sub => sub.AdslSellerAgentUser.ADSLSellerAgent.Title).FirstOrDefault(),
                                                              InstallmentCost = result.Where(subResult =>
                                                                                                        (subResult.RequestPayment.PaymentType == (byte)DB.PaymentType.Instalment) &&
                                                                                                        (!fromDate.HasValue || fromDate <= subResult.RequestPayment.Request.EndDate) &&
                                                                                                        (!toDate.HasValue || toDate >= subResult.RequestPayment.Request.EndDate)
                                                                                            )
                                                                                      .Sum(subResult => subResult.RequestPayment.Cost) ?? 0,
                                                              CashCost = result.Where(subResult =>
                                                                                                        (subResult.RequestPayment.PaymentType == (byte)DB.PaymentType.Cash) && //نقدی
                                                                                                        (subResult.RequestPayment.BaseCostID != 37) &&
                                                                                                        (subResult.RequestPayment.BaseCostID != 107) &&
                                                                                                        (!fromDate.HasValue || fromDate <= subResult.RequestPayment.Request.EndDate) &&
                                                                                                        (!toDate.HasValue || toDate >= subResult.RequestPayment.Request.EndDate)
                                                                                      )
                                                                               .Sum(subResult => subResult.RequestPayment.Cost) ?? 0,
                                                              InstallCount = result.Where(subResult =>
                                                                                                        (subResult.RequestPayment.BaseCostID == 37) && //هزینه نصب
                                                                                                        (!fromDate.HasValue || fromDate <= subResult.RequestPayment.Request.EndDate) &&
                                                                                                        (!toDate.HasValue || toDate >= subResult.RequestPayment.Request.EndDate)
                                                                                         )
                                                                                   .Count(),
                                                              InstallCost = result.Where(subResult =>
                                                                                                    (subResult.RequestPayment.BaseCostID == 37) && //هزینه نصب
                                                                                                    (!fromDate.HasValue || fromDate <= subResult.RequestPayment.Request.EndDate) &&
                                                                                                    (!toDate.HasValue || toDate >= subResult.RequestPayment.Request.EndDate)
                                                                                        )
                                                                                  .Sum(subResult => subResult.RequestPayment.Cost) ?? 0,
                                                              RanjeCount = result.Where(subResult =>
                                                                                                    (subResult.RequestPayment.BaseCostID == 107) && //هزینه رانژه
                                                                                                    (!fromDate.HasValue || fromDate <= subResult.RequestPayment.Request.EndDate) &&
                                                                                                    (!toDate.HasValue || toDate >= subResult.RequestPayment.Request.EndDate)
                                                                                       )
                                                                                 .Count(),
                                                              RanjeCost = result.Where(subResult =>
                                                                                                    (subResult.RequestPayment.BaseCostID == 37) && //هزینه رانژه
                                                                                                    (!fromDate.HasValue || fromDate <= subResult.RequestPayment.Request.EndDate) &&
                                                                                                    (!toDate.HasValue || toDate >= subResult.RequestPayment.Request.EndDate)
                                                                                      )
                                                                                .Sum(subResult => subResult.RequestPayment.Cost) ?? 0,
                                                              TotalAmount = result.Where(subResult =>
                                                                                                 (!fromDate.HasValue || fromDate <= subResult.RequestPayment.Request.EndDate) &&
                                                                                                 (!toDate.HasValue || toDate >= subResult.RequestPayment.Request.EndDate)
                                                                                       )
                                                                                  .Sum(subResult => subResult.RequestPayment.AmountSum) ?? 0
                                                          }
                                               )
                                        .AsQueryable();

                if (pageSize != 0)
                {
                    finalResult = firstQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    finalResult = firstQuery.ToList();
                }

                //محاسبه برا اساس فرمولهای سمنان
                finalResult.ForEach(ast =>
                                          {
                                              ast.NetCost = ast.InstallmentCost + ast.InstallCost + ast.CashCost + ast.RanjeCost;
                                              ast.Tax = ast.TotalAmount - ast.NetCost;
                                          }
                                   );
                count = firstQuery.Count();
                return finalResult;
            }
        }

        public static List<ADSLCustomerDurationStatisticsInfo> SearchADSLCustomerStatisticsByServiceDuration(List<int> cities, List<int> centers, List<int> personTypes,
                                                                      List<int> adslCustomerGroups, List<int> adslServiceTypes, List<int> adslServiceGroups,
                                                                      List<int> bandWidth, List<int> traffics, List<int> durations, List<int> adslServices,
                                                                      List<int> paymentTypes, string serviceCode, DateTime? fromInsertDate, DateTime? toInsertDate,
                                                                      int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int allUserCount = context.ADSLs.Where(t => (t.Status == (byte)DB.ADSLStatus.Connect) &&
                                                            (cities.Count == 0 || cities.Contains(t.Telephone.Center.Region.CityID)) &&
                                                            (centers.Count == 0 || centers.Contains(t.Telephone.CenterID)) &&
                                                            (personTypes.Count == 0 || personTypes.Contains(t.Customer.PersonType)) &&
                                                            (adslCustomerGroups.Count == 0 || (t.CustomerGroupID.HasValue && adslCustomerGroups.Contains(t.CustomerGroupID.Value))) &&
                                                            (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(t.ADSLService.PaymentTypeID)) &&
                                                            (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(t.ADSLService.GroupID)) &&
                                                            (bandWidth.Count == 0 || (t.ADSLService.BandWidthID.HasValue && bandWidth.Contains(t.ADSLService.BandWidthID.Value))) &&
                                                            (traffics.Count == 0 || (t.ADSLService.TrafficID.HasValue && traffics.Contains(t.ADSLService.TrafficID.Value))) &&
                                                            (durations.Count == 0 || (t.ADSLService.DurationID.HasValue && durations.Contains(t.ADSLService.DurationID.Value))) &&
                                                            (adslServices.Count == 0 || (t.TariffID.HasValue && adslServices.Contains(t.TariffID.Value))) &&
                                                            (paymentTypes.Count == 0 || paymentTypes.Contains(t.ADSLService.PaymentTypeID)) &&
                                                            (string.IsNullOrEmpty(serviceCode) || (t.ADSLService.ServiceCode.HasValue && t.ADSLService.ServiceCode.Value.ToString() == serviceCode)) &&
                                                            (!fromInsertDate.HasValue || fromInsertDate <= t.InsertDate) &&
                                                            (!toInsertDate.HasValue || toInsertDate >= t.InsertDate)).ToList().Count;
                return context.ADSLs
                                   .Where(t => (t.Status == (byte)DB.ADSLStatus.Connect) &&
                                               (cities.Count == 0 || cities.Contains(t.Telephone.Center.Region.CityID)) &&
                                               (centers.Count == 0 || centers.Contains(t.Telephone.CenterID)) &&
                                               (personTypes.Count == 0 || personTypes.Contains(t.Customer.PersonType)) &&
                                               (adslCustomerGroups.Count == 0 || (t.CustomerGroupID.HasValue && adslCustomerGroups.Contains(t.CustomerGroupID.Value))) &&
                                               (adslServiceTypes.Count == 0 || adslServiceTypes.Contains(t.ADSLService.PaymentTypeID)) &&
                                               (adslServiceGroups.Count == 0 || adslServiceGroups.Contains(t.ADSLService.GroupID)) &&
                                               (bandWidth.Count == 0 || (t.ADSLService.BandWidthID.HasValue && bandWidth.Contains(t.ADSLService.BandWidthID.Value))) &&
                                               (traffics.Count == 0 || (t.ADSLService.TrafficID.HasValue && traffics.Contains(t.ADSLService.TrafficID.Value))) &&
                                               (durations.Count == 0 || (t.ADSLService.DurationID.HasValue && durations.Contains(t.ADSLService.DurationID.Value))) &&
                                               (adslServices.Count == 0 || (t.TariffID.HasValue && adslServices.Contains(t.TariffID.Value))) &&
                                               (paymentTypes.Count == 0 || paymentTypes.Contains(t.ADSLService.PaymentTypeID)) &&
                                               (string.IsNullOrEmpty(serviceCode) || (t.ADSLService.ServiceCode.HasValue && t.ADSLService.ServiceCode.Value.ToString() == serviceCode)) &&
                                               (!fromInsertDate.HasValue || fromInsertDate <= t.InsertDate) &&
                                               (!toInsertDate.HasValue || toInsertDate >= t.InsertDate))
                                   .GroupBy(t => new { DurationID = t.ADSLService.DurationID })
                                   .Select(t => new ADSLCustomerDurationStatisticsInfo
                                                    {
                                                        Duration = t.Select(sub => sub.ADSLService.ADSLServiceDuration.Title).FirstOrDefault(),
                                                        CustomersCount = t.Select(sub => sub.ID).Count(),
                                                        AllCustomersCount = allUserCount
                                                    }).ToList();
            }
        }

        //        public static List<ADSLSellItem> SearchADSLSellItemsVersionTsql(List<int> cities, List<int> centers, long? telephoneNo, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        //        {
        //            using (MainDataContext context = new MainDataContext())
        //            {
        //                string query = @";WITH SellTrafficInter AS
        //                                (
        //                                	SELECT 
        //                                		RE.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLSellTraffic adst 
        //                                	INNER JOIN 
        //                                		Request RE ON ADST.ID = RE.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		RE.EndDate IS NOT NULL  AND ADST.ChangeServiceType = 2 --اینترنتی
        //                                	GROUP BY 
        //                                		RE.TelephoneNo
        //                                )
        //                                ,  SellTrafficHuzuri AS
        //                                (
        //                                	SELECT 
        //                                		RE.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLSellTraffic adst 
        //                                	INNER JOIN 
        //                                		Request RE ON ADST.ID = RE.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		RE.EndDate IS NOT NULL  AND ADST.ChangeServiceType = 1 --حضوری
        //                                	GROUP BY 
        //                                		RE.TelephoneNo
        //                                )
        //                                ,SellTraficficHuzuriAdslRequest AS
        //                                (
        //                                	SELECT 
        //                                		RE.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLRequest adst 
        //                                	INNER JOIN 
        //                                		Request RE ON ADST.ID = RE.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		RE.EndDate IS NOT NULL AND RP.BaseCostID = 46
        //                                	GROUP BY 
        //                                		RE.TelephoneNo
        //                                )
        //                                , AdslChangeServiceInter AS
        //                                (
        //                                	SELECT 
        //                                		RE.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLChangeService ADC 
        //                                	INNER JOIN 
        //                                		Request RE ON RE.ID = ADC.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		RE.EndDate IS NOT NULL AND ADC.ChangeServiceType = 2 --اینترنتی
        //                                	GROUP BY 
        //                                		RE.TelephoneNo
        //                                )
        //                                , AdslChangeServiceHuzuri AS
        //                                (
        //                                	SELECT 
        //                                		RE.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLChangeService ADC 
        //                                	INNER JOIN 
        //                                		Request RE ON RE.ID = ADC.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		RE.EndDate IS NOT NULL AND ADC.ChangeServiceType = 1 --حضوری
        //                                	GROUP BY 
        //                                		RE.TelephoneNo
        //                                )
        //                                , AdslChangeServiceAdslRequest AS
        //                                (
        //                                	SELECT 
        //                                		RE.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		AdslRequest ADC 
        //                                	INNER JOIN 
        //                                		Request RE ON RE.ID = ADC.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		RE.EndDate IS NOT NULL AND RP.BaseCostID = 44
        //                                	GROUP BY 
        //                                		RE.TelephoneNo
        //                                )
        //                                ,AdslSellModem AS
        //                                (
        //                                	SELECT 
        //                                		ADR.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLRequest ADR 
        //                                	INNER JOIN 
        //                                		Request RE ON RE.ID = ADR.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		ADR.NeedModem = 1  and RE.EndDate IS NOT NULL AND RP.BaseCostID = 40
        //                                	GROUP BY 
        //                                		ADR.TelephoneNo
        //                                )
        //                                , AdslRequestSellIp AS
        //                                (	
        //                                	SELECT 
        //                                		RE.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLChangeIPRequest AIP 
        //                                	INNER JOIN 
        //                                		Request RE ON AIP.ID = RE.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		RE.EndDate IS NOT NULL
        //                                	GROUP BY 
        //                                		RE.TelephoneNo
        //                                )
        //                                , AdslHasIp AS
        //                                (
        //                                	SELECT 
        //                                		ADR.TelephoneNo,
        //                                		SUM(RP.AmountSum) AmountSum
        //                                	FROM 
        //                                		ADSLRequest ADR 
        //                                	INNER JOIN 
        //                                		Request RE ON ADR.ID = RE.ID
        //                                	INNER JOIN 
        //                                		RequestPayment RP ON RP.RequestID = RE.ID
        //                                	WHERE 
        //                                		ADR.HasIP = 1 AND RE.EndDate IS NOT NULL
        //                                	GROUP BY
        //                                		ADR.TelephoneNo
        //                                )
        //                                SELECT 
        //                                	TE.TelephoneNo,
        //                                	CONCAT(CU.FirstNameOrTitle,' ',CU.LastName) CustomerName,
        //                                	CI.NAME CityName,
        //                                	CE.CenterName,
        //                                	STI.AmountSum SellTrafficByInternetAmount, --مبلغ فروش اینترنتی حجم
        //                                	ADI.AmountSum SellServiceByInternetAmount, --مبلغ فروش اینترنتی سرویس
        //                                	ISNULL(STH.AmountSum,0) + ISNULL(STHAR.AmountSum,0) TotalSellTrafficByPresenceAmount, --مبلغ فروش حضوری حجم
        //                                	ISNULL(ADH.AmountSum,0) + ISNULL(ACSAR.AmountSum,0) TotalSellServiceByPresenceAmount, --مبلغ فروش حضوری سرویس
        //                                	ASM.AmountSum SellModemAmount, --مبلغ فروش مودم
        //                                	ISNULL(ARIP.AmountSum ,0)+ ISNULL(AIP.AmountSum  ,0) SellIPAmount--مبلغ فروش آی پی 
        //                                FROM 
        //                                	ADSL AD 
        //                                INNER JOIN 
        //                                	Telephone TE ON TE.TelephoneNo = AD.TelephoneNo
        //                                INNER JOIN 
        //                                	Customer CU ON CU.ID = AD.CustomerOwnerID 
        //                                INNER JOIN 
        //                                	Center CE ON CE.ID = TE.CenterID 
        //                                INNER JOIN 
        //                                	Region REG ON REG.ID = CE.RegionID
        //                                INNER JOIN 
        //                                	City CI ON CI.ID = REG.CityID
        //                                LEFT JOIN 
        //                                	SellTrafficInter STI ON STI.TelephoneNo = TE.TelephoneNo --فروش اینترنی حجم
        //                                LEFT JOIN 
        //                                	SellTrafficHuzuri STH ON STH.TelephoneNo = TE.TelephoneNo --فروش حضوری حجم
        //                                LEFT JOIN 
        //                                	AdslChangeServiceInter ADI ON ADI.TelephoneNo = TE.TelephoneNo --فروش اینترنی سرویس
        //                                LEFT JOIN 
        //                                	AdslChangeServiceHuzuri ADH ON ADH.TelephoneNo = TE.TelephoneNo --فروش حضوری سرویس
        //                                LEFT JOIN
        //                                	AdslSellModem ASM ON ASM.TelephoneNo = TE.TelephoneNo --مبلغ فروش مودم
        //                                LEFT JOIN 
        //                                	AdslRequestSellIp ARIP ON ARIP.TelephoneNo = TE.TelephoneNo --مبلغ درخواست آی پی
        //                                LEFT JOIN 
        //                                	AdslHasIp AIP ON AIP.TelephoneNo = TE.TelephoneNo --مبلغ سرویس های همراه با آی پی
        //                                LEFT JOIN 
        //                                	SellTraficficHuzuriAdslRequest STHAR ON STHAR.TelephoneNo = TE.TelephoneNo  --مبلغ حجمی که مشترک در دایری اولیه خریداری نموره است
        //                                LEFT JOIN 
        //                                	AdslChangeServiceAdslRequest ACSAR ON ACSAR.TelephoneNo = TE.TelephoneNo --مبلغ سرویسی که مشترک در دایری اولیه خریداری نموده است
        //                                WHERE 
        //                                	AD.TelephoneNo = 2333361619
        //                                	
        //                                ";


        //                List<ADSLSellItem> finalResult = new List<ADSLSellItem>();

        //                if (pageSize != 0)
        //                {
        //                    finalResult = firstQuery.Skip(startRowIndex).Take(pageSize).ToList();
        //                }
        //                else
        //                {
        //                    finalResult = firstQuery.ToList();
        //                }

        //                //چون برخی از مبالغ از چند قسمت تشکیل میشوند باید در پایان آنها را محاسبه کرد
        //                finalResult.ForEach(sai =>
        //                {
        //                    sai.TotalSellServiceByPresenceAmount = sai.SellServiceByPresenceAmountPart1 + sai.SellServiceByPresenceAmountPart2;
        //                    sai.TotalSellTrafficByPresenceAmount = sai.SellTrafficByPresenceAmountPart1 + sai.SellTrafficByPresenceAmountPart2;
        //                    sai.TotalAmount = sai.SellIPAmount +
        //                                      sai.SellModemAmount +
        //                                      sai.SellServiceByInternetAmount +
        //                                      sai.SellTrafficByInternetAmount +
        //                                      sai.TotalSellServiceByPresenceAmount +
        //                                      sai.TotalSellTrafficByPresenceAmount;
        //                }
        //                                   );

        //                count = firstQuery.Count();
        //                return finalResult;
        //            }
        //        }

    }
}
