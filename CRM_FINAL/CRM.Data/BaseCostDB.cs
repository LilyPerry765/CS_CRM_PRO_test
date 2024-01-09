using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class BaseCostDB
    {
        public static List<BaseCost> SearchBaseCost(
            //List<int> workUnit,
            List<int> chargingType,
            bool? isActive,
            List<int> requestType,
            DateTime? fromDate,
            DateTime? toDate,
            bool? isPerodic,
            List<int> quotaDiscount,
            string costTitle)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts
                    .Where(t =>
                        //(workUnit.Count==0 || workUnit.Contains((int)t.WorkUnit)) && 
                            (chargingType.Count == 0 || chargingType.Contains(t.ChargingGroup)) &&
                            (!isActive.HasValue || t.IsActive == isActive) &&
                            (requestType.Count == 0 || requestType.Contains((int)t.RequestTypeID)) &&
                            (!fromDate.HasValue || t.FromDate == fromDate) &&
                            (!toDate.HasValue || t.ToDate == toDate) &&
                            (!isPerodic.HasValue || t.IsPerodic == isPerodic) &&
                            (quotaDiscount.Count == 0 || quotaDiscount.Contains(t.QuotaDiscountID ?? -1)) &&
                            (string.IsNullOrWhiteSpace(costTitle) || t.Title.Contains(costTitle))
                          )
                    .ToList();
            }
        }

        public static BaseCost GetBaseCostByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetBaseCostCheckable()
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.BaseCosts
        //            .Select(t => new CheckableItem
        //            {
        //                ID = t.ID,
        //                Name = t.Title,
        //                IsChecked = false
        //            }).ToList();
        //    }
        //}

        //TODO:rad 13950313
        public static List<CheckableItem> GetBaseCostCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.BaseCosts
                                   .Select(t => new CheckableItem
                                                {
                                                    ID = t.ID,
                                                    Name = t.Title,
                                                    IsChecked = false
                                                }
                                          )
                                   .OrderBy(ci => ci.Name)
                                   .AsQueryable();

                result = query.ToList();
                return result;
            }
        }


        public static List<BaseCost> GetBaseCostByRequestTypeID(int requestTypeID)
        {
            DateTime dateTime = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {
                List<BaseCost> result = new List<BaseCost>();
                List<int> specialCostsId = Enum.GetValues(typeof(DB.SpecialCostID)).Cast<int>().ToList();
                result = context.BaseCosts
                                .Where(t =>
                                             (t.RequestTypeID == requestTypeID) &&
                                             (t.IsActive == true) &&
                                             (!specialCostsId.Contains(t.ID)) &&
                                             (t.FromDate.HasValue == false || t.FromDate.Value.Date <= dateTime.Date) &&
                                             (t.ToDate.HasValue == false || t.ToDate.Value.Date >= dateTime.Date)
                                       )
                                .ToList();
                return result;
            }
        }

        public static BaseCost GetModemCostForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSL && t.Cost == 0 && t.Title.Contains("مودم")).SingleOrDefault();
            }
        }

        public static BaseCost GetServiceCostForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSL && t.Cost == 0 && t.Title.Contains("سرویس")).SingleOrDefault();
            }
        }

        public static BaseCost GetAdditionalServiceCostForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSL && t.Cost == 0 && t.Title.Contains("ترافیک")).SingleOrDefault();
            }
        }

        public static BaseCost GetSellTrafficCostForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSLSellTraffic && t.Cost == 0 && t.Title.Contains("ترافیک")).SingleOrDefault();
            }
        }

        public static BaseCost GetChangeNumberForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSLChangePlace && t.Title.Contains("ADSl - تعویض شماره")).SingleOrDefault();
            }
        }

        public static BaseCost GetServiceCostForADSLChangeService()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSLChangeService && t.Cost == 0 && t.Title.Contains("سرویس")).SingleOrDefault();
            }
        }

        public static BaseCost GetAdditionalServiceCostForADSLChangeTariff()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSLChangeService && t.Cost == 0 && t.Title.Contains("ترافیک")).SingleOrDefault();
            }
        }

        public static BaseCost GetInstallCostForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSL && t.Title.Contains("رانژه")).SingleOrDefault();
            }
        }

        public static BaseCost GetIPCostForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSL && t.Title.Contains("IP استاتیک")).SingleOrDefault();
            }
        }

        public static BaseCost GetInstalCostForADSL()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.ADSL && t.Title.Contains("هزینه نصب و حضور کارشناس")).SingleOrDefault();
            }
        }
        public static BaseCost GetInstalCostForWireless()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == (byte)DB.RequestType.Wireless && t.Title.Contains("هزینه نصب و حضور کارشناس")).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetBaseCostCheckableByRequestTypeID(int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BaseCosts.Where(t => t.RequestTypeID == requestTypeID && t.IsActive == true)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    }).ToList();
            }
        }

        public static bool IsOutBoundBaseCost(int Id, int requestTypeId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime dateTime = DB.GetServerDate();

                List<int> specialCostsId = Enum.GetValues(typeof(DB.SpecialCostID)).Cast<int>().ToList();

                bool result = false;

                var query = context.BaseCosts
                                   .Where(t =>
                                                (t.ID == Id) &&
                                                (t.RequestTypeID == requestTypeId) &&
                                                (t.IsActive == true) &&
                                                (!specialCostsId.Contains(t.ID)) &&
                                                (t.FromDate.HasValue == false || t.FromDate.Value.Date <= dateTime.Date) &&
                                                (t.ToDate.HasValue == false || t.ToDate.Value.Date >= dateTime.Date) &&
                                                (t.UseOutBound)
                                          );
                result = query.Any();
                return result;
            }
        }
    }
}