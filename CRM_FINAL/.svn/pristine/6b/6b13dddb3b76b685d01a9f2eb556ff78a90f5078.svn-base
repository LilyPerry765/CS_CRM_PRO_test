using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class TelecomminucationServicePaymentDB
    {
        public static List<TelecomminucationServicePaymentInfo> GetTelecomminucationServicePaymentInfos(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TelecomminucationServicePaymentInfo> result = new List<TelecomminucationServicePaymentInfo>();
                result = context.TelecomminucationServicePayments
                                .Where(tsp => tsp.RequestID == requestId)
                                .Select(tsp => new TelecomminucationServicePaymentInfo
                                            {
                                                AmountSum = tsp.AmountSum,
                                                Discount = tsp.Discount,
                                                ID = tsp.ID,
                                                NetAmount = tsp.NetAmount,
                                                NetAmountWithDiscount = tsp.NetAmountWithDiscount,
                                                Quantity = tsp.Quantity,
                                                TaxAndTollAmount = tsp.TaxAndTollAmount,
                                                TelecomminucationServiceID = tsp.TelecomminucationServiceID,
                                                TelecomminucationServiceTitle = tsp.TelecomminucationService.Title,
                                                UnitPrice = tsp.TelecomminucationService.UnitPrice,
                                                RequestID = tsp.RequestID
                                            }
                                       )
                                .ToList();
                return result;
            }
        }

        public static List<TelecomminucationServicePaymentStatisticsInfo> GetTelecomminucationServicePaymentStatisticsInfoByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.TelecomminucationServicePayments.Where(tps => tps.RequestID == requestID).AsQueryable();
                List<TelecomminucationServicePaymentStatisticsInfo> result = new List<TelecomminucationServicePaymentStatisticsInfo>();
                result = query.Select(tsp => new TelecomminucationServicePaymentStatisticsInfo
                                            {
                                                TelecomminucationServiceTitle = tsp.TelecomminucationService.Title,
                                                Quantity = tsp.Quantity,
                                                UnitMeasure = tsp.TelecomminucationService.UnitMeasure.Name,
                                                UnitPrice = tsp.TelecomminucationService.UnitPrice,
                                                NetAmount = tsp.NetAmount,
                                                Discount = tsp.Discount,
                                                NetAmountWithDiscount = tsp.NetAmountWithDiscount,
                                                TaxAndTollAmount = tsp.TaxAndTollAmount,
                                                AmountSum = tsp.AmountSum
                                            }
                                     )
                              .ToList();
                return result;
            }
        }

        public static List<TelecomminucationServicePayment> GetTelecomminucationServicePaymentsByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TelecomminucationServicePayment> result = new List<TelecomminucationServicePayment>();
                result = context.TelecomminucationServicePayments.Where(tsp => tsp.RequestID == requestID).ToList();
                return result;
            }
        }


        //TODO:rad
        public static List<TelecomminucationServicePaymentReportInfo> SearchTelecomminucationServicePayments(string nationalCodeOrRecordNo, long requestId, out List<CustomerReportInfo> customers)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CustomerReportInfo> customersInfo = new List<CustomerReportInfo>();
                List<TelecomminucationServicePaymentReportInfo> result = new List<TelecomminucationServicePaymentReportInfo>();
                //باید درخواست مربوط به شناسه درخواست در فیلترها را باید بدست بیاوریم ، تا متناسب با نوع آن عمل کنیم
                Request currentRequest = context.Requests.Where(re => re.ID == requestId).SingleOrDefault();

                IQueryable<TelecomminucationServicePayment> query = Enumerable.Empty<TelecomminucationServicePayment>().AsQueryable();
                query = context.TelecomminucationServicePayments
                               .Where(tsp =>
                                            (requestId == -1 || tsp.RequestID == requestId) &&
                                            (string.IsNullOrEmpty(nationalCodeOrRecordNo) || (tsp.Request.Customer.NationalCodeOrRecordNo == nationalCodeOrRecordNo))
                                      );
                //پر کردن لیست مشترکینی که درخواستشان دارای کالا وخدمات مخابرات بوده است
                customersInfo = query.Select(tsp => new CustomerReportInfo
                {
                    ID = tsp.Request.Customer.ID,
                    AddressContent = (tsp.Request.Customer.AddressID.HasValue) ? tsp.Request.Customer.Address.AddressContent : string.Empty,
                    ProvinceName = (tsp.Request.Customer.AddressID.HasValue) ? tsp.Request.Customer.Address.Center.Region.City.Province.Name : string.Empty,
                    CityName = (tsp.Request.Customer.AddressID.HasValue) ? tsp.Request.Customer.Address.Center.Region.City.Name : string.Empty,
                    CustomerName = string.Format("{0} {1}", tsp.Request.Customer.FirstNameOrTitle, tsp.Request.Customer.LastName),
                    PostalCode = (tsp.Request.Customer.AddressID.HasValue) ? tsp.Request.Customer.Address.PostalCode : string.Empty,
                    UrgentTelNo = tsp.Request.Customer.UrgentTelNo,
                    NationalCodeOrRecordNo = tsp.Request.Customer.NationalCodeOrRecordNo,
                    NationalID = tsp.Request.Customer.NationalID
                }
                                              )
                                    .Distinct()
                                    .ToList();
                customers = customersInfo;
                //**************************************************************************************************************************************************

                //باید بر اساس نوع درخواست مابقی ستون های گزارش پر شود
                //چون ستونی که در پایگاه داده مقدار آدرس به ازای هر رکورد از گزارش را دارا میباشد در نوع درخواستهای گوناگون ، متفاوت است
                switch (currentRequest.RequestTypeID)
                {
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            //صورتحساب هایی را باید برگرداند که فقط مربوط به درخواست فضا و پاور میباشد
                            result = query.Join(context.SpaceAndPowers, tsp => tsp.RequestID, sp => sp.ID, (tsp, sp) => new { _TelecomminucationServicePayment = tsp, _SpaceAndPower = sp })
                                          .Select(tsp => new TelecomminucationServicePaymentReportInfo
                                          {
                                              CustomerID = tsp._SpaceAndPower.Request.CustomerID.Value,
                                              TelecomminucationServiceTitle = tsp._TelecomminucationServicePayment.TelecomminucationService.Title,
                                              Quantity = tsp._TelecomminucationServicePayment.Quantity,
                                              UnitMeasureName = tsp._TelecomminucationServicePayment.TelecomminucationService.UnitMeasure.Name,
                                              UnitPrice = tsp._TelecomminucationServicePayment.TelecomminucationService.UnitPrice,
                                              NetAmount = tsp._TelecomminucationServicePayment.NetAmount,
                                              Discount = tsp._TelecomminucationServicePayment.Discount,
                                              NetAmountWithDiscount = tsp._TelecomminucationServicePayment.NetAmountWithDiscount,
                                              TaxAndTollAmount = tsp._TelecomminucationServicePayment.TaxAndTollAmount,
                                              AmountSum = tsp._TelecomminucationServicePayment.AmountSum,
                                              RequestID = tsp._TelecomminucationServicePayment.RequestID,
                                              AddressContent = tsp._TelecomminucationServicePayment.Request.SpaceAndPower.Address.AddressContent
                                          }
                                                  )
                                          .ToList();
                            break;
                        }
                    case (int)DB.RequestType.E1:
                        {
                            //صورتحساب هایی را باید برگرداند که فقط مربوط به درخواست ایوان میباشد
                            result = query.Join(context.E1s, tsp => tsp.RequestID, e1 => e1.RequestID, (tsp, e1) => new { _TelecomminucationServicePayment = tsp, _E1 = e1 })
                                          .Select(tsp => new TelecomminucationServicePaymentReportInfo
                                          {
                                              CustomerID = tsp._E1.Request.CustomerID.Value,
                                              TelecomminucationServiceTitle = tsp._TelecomminucationServicePayment.TelecomminucationService.Title,
                                              Quantity = tsp._TelecomminucationServicePayment.Quantity,
                                              UnitMeasureName = tsp._TelecomminucationServicePayment.TelecomminucationService.UnitMeasure.Name,
                                              UnitPrice = tsp._TelecomminucationServicePayment.TelecomminucationService.UnitPrice,
                                              NetAmount = tsp._TelecomminucationServicePayment.NetAmount,
                                              Discount = tsp._TelecomminucationServicePayment.Discount,
                                              NetAmountWithDiscount = tsp._TelecomminucationServicePayment.NetAmountWithDiscount,
                                              TaxAndTollAmount = tsp._TelecomminucationServicePayment.TaxAndTollAmount,
                                              AmountSum = tsp._TelecomminucationServicePayment.AmountSum,
                                              RequestID = tsp._TelecomminucationServicePayment.RequestID,
                                              AddressContent = tsp._TelecomminucationServicePayment.Request.E1.Address.AddressContent
                                          }
                                                 )
                                          .ToList();
                            break;
                        }
                }
                result.ForEach(tspr =>
                {
                    tspr.AddressContent = !string.IsNullOrEmpty(tspr.AddressContent) ? tspr.AddressContent : "نامشخص";
                }
                              );
                return result;
            }
        }

        //TODO:rad
        public static List<TelecomminucationServicePaymentReportInfo> SearchTelecomminucationServicePayments(long requestID, out CustomerReportInfo customer, int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                CustomerReportInfo customerInfo = new CustomerReportInfo();
                List<TelecomminucationServicePaymentReportInfo> result = new List<TelecomminucationServicePaymentReportInfo>();

                IQueryable<TelecomminucationServicePayment> query = Enumerable.Empty<TelecomminucationServicePayment>().AsQueryable();
                query = context.TelecomminucationServicePayments
                               .Where(tsp => tsp.RequestID == requestID);
                switch (requestTypeID)
                {
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            customerInfo = query.Select(tsp => new CustomerReportInfo
                            {
                                ID = tsp.Request.SpaceAndPower.Customer.ID,
                                AddressContent = (tsp.Request.SpaceAndPower.Customer.AddressID.HasValue) ? tsp.Request.SpaceAndPower.Customer.Address.AddressContent : string.Empty,
                                ProvinceName = (tsp.Request.SpaceAndPower.Customer.AddressID.HasValue) ? tsp.Request.SpaceAndPower.Customer.Address.Center.Region.City.Province.Name : string.Empty,
                                CityName = (tsp.Request.SpaceAndPower.Customer.AddressID.HasValue) ? tsp.Request.SpaceAndPower.Customer.Address.Center.Region.City.Name : string.Empty,
                                CustomerName = string.Format("{0} {1}", tsp.Request.SpaceAndPower.Customer.FirstNameOrTitle, tsp.Request.SpaceAndPower.Customer.LastName),
                                PostalCode = (tsp.Request.SpaceAndPower.Customer.AddressID.HasValue) ? tsp.Request.SpaceAndPower.Customer.Address.PostalCode : string.Empty,
                                UrgentTelNo = tsp.Request.SpaceAndPower.Customer.UrgentTelNo,
                                NationalCodeOrRecordNo = tsp.Request.SpaceAndPower.Customer.NationalCodeOrRecordNo,
                                NationalID = tsp.Request.SpaceAndPower.Customer.NationalID
                            }
                                                       )
                                                 .Take(1)
                                                 .SingleOrDefault();

                            break;
                        }
                }
                customer = customerInfo;

                result = query.Select(tsp => new TelecomminucationServicePaymentReportInfo
                {
                    CustomerID = tsp.Request.SpaceAndPower.Customer.ID,
                    TelecomminucationServiceTitle = tsp.TelecomminucationService.Title,
                    Quantity = tsp.Quantity,
                    UnitMeasureName = tsp.TelecomminucationService.UnitMeasure.Name,
                    UnitPrice = tsp.TelecomminucationService.UnitPrice,
                    NetAmount = tsp.NetAmount,
                    Discount = tsp.Discount,
                    NetAmountWithDiscount = tsp.NetAmountWithDiscount,
                    TaxAndTollAmount = tsp.TaxAndTollAmount,
                    AmountSum = tsp.AmountSum,
                    RequestID = tsp.RequestID
                }
                                     ).ToList();
                return result;
            }
        }

        //TODO:rad
        public static List<CheckableItem> GetCustomersOfTelecomminucationService()
        {
            using (MainDataContext context = new MainDataContext())
            {
                /*در این متد مشترکینی برگردانده میشوند که دارای درخواستی بوده اند که
                 * کالا و خدامات مخابرات داشته است
                 */
                List<CheckableItem> result = new List<CheckableItem>();
                var query = context.TelecomminucationServicePayments
                                   .Select(tsp => tsp.Request.Customer)
                                   .Distinct()
                                   .AsQueryable();
                result = query.Select(cu => new CheckableItem
                                            {
                                                Name = string.Format("{0} {1}", cu.FirstNameOrTitle, cu.LastName),
                                                LongID = cu.ID,
                                                IsChecked = false
                                            }
                                     )
                              .ToList();
                return result.OrderBy(ci => ci.Name).ToList();
            }
        }

    }
}
