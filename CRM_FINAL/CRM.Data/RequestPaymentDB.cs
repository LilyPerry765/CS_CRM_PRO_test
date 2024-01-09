using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Data.Linq.Mapping;

namespace CRM.Data
{
    public static class RequestPaymentDB
    {
        public static RequestPayment GetRequestPaymentByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static RequestPayment GetRequestPaymentByRequsetIDandCostID(long requestID, int baseCostID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => (t.RequestID == requestID) && (t.BaseCostID == baseCostID)).SingleOrDefault();
            }
        }

        public static RequestPayment GetRequestPaymentByTelNoandCostID(long? telNo, int baseCostID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => (t.Request.TelephoneNo == telNo) && (t.BaseCostID == baseCostID)).FirstOrDefault();
            }
        }

        public static List<RequestPayment> GetRequestPaymentByRequestID(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          (requestPaymentType == null || t.PaymentType == requestPaymentType))
                                                     .ToList();
            }
        }
        public static List<RequestPayment> GetAllRequestPaymentByRequestID(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t =>
                                                          (t.RequestID == requestID) &&
                                                          (requestPaymentType == null || t.PaymentType == requestPaymentType)
                                                    )
                                              .OrderBy(filteredData => filteredData.BaseCost.Title)
                                              .ToList();
            }
        }
        public static List<RequestPayment> GetRequestPaymentDepositByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {

                CRM.Data.Request request = context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID == (int)DB.RequestType.Dayri).OrderByDescending(t => t.InsertDate).FirstOrDefault();
                if (request != null)
                {
                    return context.RequestPayments.Where(t => t.RequestID == request.ID &&
                                                              (t.IsKickedBack == false || t.IsKickedBack == null) &&
                                                              t.BaseCost.IsDeposit == true)
                                                         .ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public static List<RequestPaymentInfo> GetRequestPaymentInfoByRequestID(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          (requestPaymentType == null || t.PaymentType == requestPaymentType))
                                                     .Select(t => new RequestPaymentInfo
                                                                 {
                                                                     RequestPayment = t,
                                                                     ID = t.ID,
                                                                     BaseCostTitle = context.BaseCosts.Where(t2 => t2.ID == t.BaseCostID).Select(t2 => t2.Title).SingleOrDefault(),
                                                                     OtherCostTitle = context.OtherCosts.Where(t2 => t2.ID == t.OtherCostID).Select(t2 => t2.CostTitle).SingleOrDefault(),
                                                                     AmountSum = t.AmountSum,
                                                                     BillID = t.BillID,
                                                                     PaymentID = t.PaymentID,
                                                                     FicheNumber = t.FicheNunmber,
                                                                     IsPaid = t.IsPaid
                                                                 }
                                                      ).ToList();
            }
        }

        public static List<RequestPayment> GetNoPaidRequestPaymentByRequestID(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          (t.IsPaid == null || t.IsPaid == false) &&
                                                          (requestPaymentType == null || t.PaymentType == requestPaymentType))
                                                     .ToList();
            }
        }

        public static string GetNoPaidPaymentAmountByRequestID(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          (t.IsPaid == null || t.IsPaid == false) &&
                                                          (requestPaymentType == null || t.PaymentType == requestPaymentType))
                                                     .ToList().Sum(t => t.AmountSum).ToString();
            }
        }

        public static bool GetNoPaidRequestPaymentHasBillID(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPayment> payments = context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                           t.IsKickedBack == false &&
                                                           (t.IsPaid == null || t.IsPaid == false) &&
                                                           (requestPaymentType == null || t.PaymentType == requestPaymentType) &&
                                                           (t.BillID != null || t.BillID != ""))
                                                      .ToList();

                if (payments.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        public static List<RequestPaymentList> GetRequestPaymentList(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID && t.IsKickedBack == false && t.PaymentType == requestPaymentType)
                    .Select(t => new RequestPaymentList
                    {
                        AmountSum = t.AmountSum.ToString(),
                        BaseCostID = t.BaseCost.Title,
                        Cost = t.Cost.ToString(),
                        PaymentType = t.PaymentType.ToString(),
                        Tax = t.Tax.ToString()
                    })
                    .ToList();
            }
        }

        public static long GetAmountSumforAllPayment(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return Convert.ToInt64(context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                                          t.IsKickedBack == false &&
                                                                          (t.IsPaid == null || t.IsPaid == false) &&
                                                                          (requestPaymentType == null || t.PaymentType == requestPaymentType))
                                                              .ToList().Sum(t => t.AmountSum));
            }
        }

        public static long GetAmountSumforPaidPayment(long requestID, int? requestPaymentType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return Convert.ToInt64(context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                                          t.IsKickedBack == false &&
                                                                          (t.IsPaid == true) &&
                                                                          (requestPaymentType == null || t.PaymentType == requestPaymentType))
                                                              .ToList().Sum(t => t.AmountSum));
            }
        }

        public static bool PaidAllPaymentsbyRequestID(long requestID)
        {
            bool result = false;

            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPayment> paymentNotPaidList = context.RequestPayments
                                                                 .Where(t =>
                                                                            (t.IsPaid == null || t.IsPaid == false) &&
                                                                            t.RequestID == requestID &&
                                                                            t.PaymentType == (byte)DB.PaymentType.Cash &&
                                                                            t.AmountSum != 0
                                                                        )
                                                                  .ToList();

                if (paymentNotPaidList.Count == 0)
                    result = true;
                else
                    result = false;
            }

            return result;
        }

        public static bool PaidAllInstalmentPaymentsbyRequestID(long requestID)
        {
            bool result = false;

            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPayment> paymentNotPaidList = context.RequestPayments.Where(t => (t.IsPaid == null || t.IsPaid == false) && t.RequestID == requestID && t.PaymentType == (byte)DB.PaymentType.Instalment && t.AmountSum != 0).ToList();

                if (paymentNotPaidList.Count == 0)
                    result = true;
                else
                    result = false;
            }

            return result;
        }

        public static bool PaidAllPaymentsbyPaymentID(string billID, string paymentID)
        {
            bool result = false;

            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPayment> paymentNotPaidList = context.RequestPayments.Where(t => (t.IsPaid == null || t.IsPaid == false) && t.BillID == billID && t.PaymentID == paymentID).ToList();

                if (paymentNotPaidList.Count == 0)
                    result = true;
                else
                    result = false;
            }

            return result;
        }

        public static RequestPayment GetPrePaymentOfRequestPayment(long requestPayment)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RelatedRequestPaymentID == requestPayment).OrderByDescending(t => t.ID).FirstOrDefault();
            }
        }

        public static List<RequestPayment> SearchRequestPayment()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Select(t => t).ToList();

            }

        }

        public static List<RequestPayment> SearchByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID).ToList();
            }
        }

        //milad doran
        //public static List<RequestPayment> SearchPaymentRequest(
        //    string telephoneno,
        //    List<int> BaseCosts,
        //      List<int> PaymentTypes,
        //      List<int> PaymentWays,
        //      long RequestID,
        //      string FicheNumber,
        //      //string PaymentID,
        //      //string BillID,
        //      List<int> Banks,
        //     bool? isPaid,
        //    bool? isAccepted,
        //    int startRowIndex,
        //    int pageSize)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.RequestPayments.Where(t => (string.IsNullOrWhiteSpace(telephoneno) || t.Request.TelephoneNo.ToString().Contains(telephoneno)) &&
        //            (BaseCosts.Count == 0 || (BaseCosts.Contains((int)t.BaseCostID))) &&
        //            (PaymentTypes.Count == 0 || (PaymentTypes.Contains((int)t.PaymentType))) &&
        //            (PaymentWays.Count == 0 || (PaymentWays.Contains((int)t.PaymentWay))) &&
        //            (Banks.Count == 0 || (Banks.Contains((int)t.BankID))) &&
        //            (string.IsNullOrWhiteSpace(FicheNumber) || t.FicheNunmber.Contains(FicheNumber)) &&
        //             //(string.IsNullOrWhiteSpace(PaymentID) || t.PaymentID.Contains(PaymentID)) &&
        //             //(string.IsNullOrWhiteSpace(BillID) || t.BillID.Contains(BillID)) &&
        //             (RequestID == -1 || RequestID == t.RequestID) &&
        //             (!isPaid.HasValue || isPaid == t.IsPaid) &&
        //             (!isAccepted.HasValue || isAccepted == t.IsAccepted)).Skip(startRowIndex).Take(pageSize).ToList();

        //    }
        //}


        //TODO:rad 13950124
        public static List<RequestPaymentInfo> SearchPaymentRequest(
                                                                        string telephoneno, List<int> baseCosts, List<int> paymentTypes,
                                                                        List<int> paymentWays, long requestId, string ficheNumber,
            //string PaymentID,
            //string BillID,
                                                                        List<int> Banks, bool? isPaid, bool? isAccepted,
                                                                        int startRowIndex, int pageSize, out int count
                                                                    )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPaymentInfo> result = new List<RequestPaymentInfo>();

                var query = context.RequestPayments.Where(t =>
                                                            (string.IsNullOrEmpty(telephoneno) || t.Request.TelephoneNo.ToString().Contains(telephoneno)) &&
                                                            (baseCosts.Count == 0 || (baseCosts.Contains((int)t.BaseCostID))) &&
                                                            (paymentTypes.Count == 0 || (paymentTypes.Contains((int)t.PaymentType))) &&
                                                            (paymentWays.Count == 0 || (paymentWays.Contains((int)t.PaymentWay))) &&
                                                            (Banks.Count == 0 || (Banks.Contains((int)t.BankID))) &&
                                                            (string.IsNullOrEmpty(ficheNumber) || t.FicheNunmber.Contains(ficheNumber)) &&
                                                                //(string.IsNullOrWhiteSpace(PaymentID) || t.PaymentID.Contains(PaymentID)) &&
                                                                //(string.IsNullOrWhiteSpace(BillID) || t.BillID.Contains(BillID)) &&
                                                            (requestId == -1 || requestId == t.RequestID) &&
                                                            (!isPaid.HasValue || isPaid == t.IsPaid) &&
                                                            (!isAccepted.HasValue || isAccepted == t.IsAccepted)
                                                         )
                                                  .Select(rp => new RequestPaymentInfo
                                                                {
                                                                    ID = rp.ID,
                                                                    BillID = rp.BillID,
                                                                    PaymentID = rp.PaymentID,
                                                                    RequestID = rp.RequestID,
                                                                    TelephoneNo = rp.Request.TelephoneNo.ToString(),
                                                                    BaseCostTitle = rp.BaseCost.Title,
                                                                    PaymentWayTitle = Helpers.GetEnumDescription(rp.PaymentWay, typeof(DB.PaymentWay)),
                                                                    PaymentTypeTitle = Helpers.GetEnumDescription(rp.PaymentType, typeof(DB.PaymentType)),
                                                                    BankName = rp.Bank.BankName,
                                                                    Cost = rp.Cost.ToString(),
                                                                    Tax = rp.Tax,
                                                                    AmountSum = rp.AmountSum,
                                                                    FicheNumber = rp.FicheNunmber,
                                                                    FicheDate = rp.FicheDate.ToPersian(Date.DateStringType.Compelete),
                                                                    IsPaid = rp.IsPaid,
                                                                    IsAccepted = rp.IsAccepted
                                                                }
                                                         )
                                                  .AsQueryable();
                count = query.Count();

                result = query.Skip(startRowIndex)
                              .Take(pageSize)
                              .ToList();

                return result;
            }
        }

        //TODO:rad 13950316
        public static List<RequestPaymentInfo> SearchPaymentRequest(
                                                                        string telephoneno, List<int> baseCosts, List<int> paymentTypes,
                                                                        List<int> paymentWays, long requestId, string ficheNumber,
                                                                        List<int> Banks, List<int> citiesId, List<int> centersId, bool? isPaid, bool? isAccepted,
                                                                        DateTime? fromInsertDate, DateTime? toInsertDate,
                                                                        bool forPrint, int startRowIndex, int pageSize, out int count
                                                                    )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPaymentInfo> result = new List<RequestPaymentInfo>();

                var query = context.RequestPayments.Where(t =>
                                                            (string.IsNullOrEmpty(telephoneno) || t.Request.TelephoneNo.ToString().Contains(telephoneno)) &&
                                                            (citiesId.Count == 0 || citiesId.Contains(t.Request.Center.Region.CityID)) &&
                                                            (centersId.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centersId.Contains(t.Request.CenterID)) &&
                                                            (baseCosts.Count == 0 || (baseCosts.Contains((int)t.BaseCostID))) &&
                                                            (paymentTypes.Count == 0 || (paymentTypes.Contains((int)t.PaymentType))) &&
                                                            (paymentWays.Count == 0 || (paymentWays.Contains((int)t.PaymentWay))) &&
                                                            (Banks.Count == 0 || (Banks.Contains((int)t.BankID))) &&
                                                            (string.IsNullOrEmpty(ficheNumber) || t.FicheNunmber.Contains(ficheNumber)) &&
                                                            (requestId == -1 || requestId == t.RequestID) &&
                                                            (!isPaid.HasValue || isPaid == t.IsPaid) &&
                                                            (!isAccepted.HasValue || isAccepted == t.IsAccepted) &&
                                                            (!fromInsertDate.HasValue || fromInsertDate <= t.InsertDate) &&
                                                            (!toInsertDate.HasValue || toInsertDate >= t.InsertDate)
                                                         )
                                                  .Select(rp => new RequestPaymentInfo
                                                                {
                                                                    City = rp.Request.Center.Region.City.Name,
                                                                    Center = rp.Request.Center.CenterName,
                                                                    CustomerName = (rp.Request.CustomerID.HasValue) ? rp.Request.Customer.FirstNameOrTitle + " " + rp.Request.Customer.LastName : "-----",
                                                                    InsertDate = rp.InsertDate.ToPersian(Date.DateStringType.Compelete),
                                                                    ID = rp.ID,
                                                                    BillID = rp.BillID,
                                                                    PaymentID = rp.PaymentID,
                                                                    RequestID = rp.RequestID,
                                                                    TelephoneNo = rp.Request.TelephoneNo.ToString(),
                                                                    BaseCostTitle = rp.BaseCost.Title,
                                                                    PaymentWayTitle = Helpers.GetEnumDescription(rp.PaymentWay, typeof(DB.PaymentWay)),
                                                                    PaymentTypeTitle = Helpers.GetEnumDescription(rp.PaymentType, typeof(DB.PaymentType)),
                                                                    BankName = rp.Bank.BankName,
                                                                    Cost = rp.Cost.ToString(),
                                                                    Tax = rp.Tax,
                                                                    AmountSum = rp.AmountSum,
                                                                    FicheNumber = rp.FicheNunmber,
                                                                    FicheDate = rp.FicheDate.ToPersian(Date.DateStringType.Compelete),
                                                                    IsPaid = rp.IsPaid,
                                                                    IsAccepted = rp.IsAccepted
                                                                }
                                                         )
                                                  .AsQueryable();
                if (forPrint)
                {
                    result = query.ToList();

                    count = result.Count;
                }
                else
                {
                    count = query.Count();

                    result = query.Skip(startRowIndex)
                                  .Take(pageSize)
                                  .ToList()
                                  .OrderBy(rpi => rpi.City)
                                  .ThenBy(rpi => rpi.Center)
                                  .ThenByDescending(rpi => rpi.InsertDate)
                                  .ThenBy(rpi => rpi.CustomerName)
                                  .ToList();
                }

                return result;
            }
        }

        public static int SearchPaymentRequestCount(
            string telephoneno,
            List<int> BaseCosts,
              List<int> PaymentTypes,
              List<int> PaymentWays,
              long RequestID,
              string FicheNumber,
            //string PaymentID,
            //string BillID,
              List<int> Banks,
             bool? isPaid,
            bool? isAccepted)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => (string.IsNullOrWhiteSpace(telephoneno) || t.Request.TelephoneNo.ToString().Contains(telephoneno)) &&
                    (BaseCosts.Count == 0 || (BaseCosts.Contains((int)t.BaseCostID))) &&
                    (PaymentTypes.Count == 0 || (PaymentTypes.Contains((int)t.PaymentType))) &&
                    (PaymentWays.Count == 0 || (PaymentWays.Contains((int)t.PaymentWay))) &&
                    (Banks.Count == 0 || (Banks.Contains((int)t.BankID))) &&
                    (string.IsNullOrWhiteSpace(FicheNumber) || t.FicheNunmber.Contains(FicheNumber)) &&
                    //(string.IsNullOrWhiteSpace(PaymentID) || t.PaymentID.Contains(PaymentID)) &&
                    //(string.IsNullOrWhiteSpace(BillID) || t.BillID.Contains(BillID)) &&
                     (RequestID == -1 || RequestID == t.RequestID) &&
                      (!isPaid.HasValue || isPaid == t.IsPaid) &&
                      (!isAccepted.HasValue || isAccepted == t.IsAccepted)).Count();

            }
        }

        public static System.Collections.IEnumerable SearchRequest(List<int> list, List<CheckableItem> list_2, List<int> list_3, long requestIDs, string p, string p_2, string p_3)
        {
            throw new NotImplementedException();
        }

        public static List<RequestPayment> GetPaymentsbyPaymentID(string billID, string paymentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.BillID == billID && t.PaymentID == paymentID).ToList();
            }
        }

        public static string GetPaymentSumAmountbyPaymentID(string billID, string paymentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPayment> paymentList = context.RequestPayments.Where(t => t.BillID == billID && t.PaymentID == paymentID).ToList();

                if (paymentList.Count != 0)
                    return paymentList.Sum(t => t.AmountSum).ToString();
                else
                    return "";
            }
        }

        public static List<CheckableItem> GetPaymentIDsCheckablebyBillID(long requestID, string billID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.BillID == billID && t.RequestID == requestID)
                    .Select(t => new CheckableItem
                    {
                        ID = 1,
                        Name = t.PaymentID,
                        IsChecked = false
                    }).Distinct().OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static int GetRequestTypeIDbyRequestPaymentID(long requestPaymentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.ID == requestPaymentID).SingleOrDefault().Request.RequestTypeID;
            }
        }

        public static bool HasPaidRequestPaymentByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPayment> payments = context.RequestPayments.Where(t => t.RequestID == requestID && t.IsPaid == true).ToList();

                if (payments.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        public static RequestPayment GetRequestPaymentByBaseCostID(int baseCostID, long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.BaseCostID == baseCostID && t.RequestID == requestID).SingleOrDefault();
            }
        }

        public static List<RequestPayment> GetRequestPaymentsByBaseCostID(int baseCostID, long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.BaseCostID == baseCostID && t.RequestID == requestID).ToList();
            }
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

        public static List<RequestPayment> GetRequestPaymentByRequestTypeID(DB.RequestType requestType, long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => context.BaseCosts.Where(t2 => t2.RequestTypeID == (int)requestType).Select(t2 => t2.ID).ToList().Contains((int)t.BaseCostID) && t.RequestID == requestID).ToList();
            }
        }

        public static long GetTelephoneNobyRequestPaymentID(long requestPaymentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return (long)context.RequestPayments.Where(t => t.ID == requestPaymentID).SingleOrDefault().Request.TelephoneNo;
            }
        }

        public static List<CheckableItem> GetCostTypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> BaseCostList = context.BaseCosts
                                                                .Select(t => new CheckableItem
                                                                {
                                                                    ID = t.ID,
                                                                    Name = t.Title,
                                                                    IsChecked = false
                                                                }).ToList();

                List<CheckableItem> OtherCostList = context.OtherCosts.Select(t => new CheckableItem
                                                                                {
                                                                                    ID = t.ID,
                                                                                    Name = t.CostTitle,
                                                                                    IsChecked = false

                                                                                }).ToList();
                return BaseCostList.Concat(OtherCostList).ToList();
            }

        }

        public static bool HasRequestPaymentBillIDbyRequestID(long requestID)
        {
            bool result = false;

            using (MainDataContext context = new MainDataContext())
            {
                List<RequestPayment> paymentList = context.RequestPayments.Where(t => t.RequestID == requestID && t.BillID == null && t.PaymentID == null && t.PaymentType == (byte)DB.PaymentType.Cash).ToList();

                if (paymentList.Count == 0)
                    result = false;
                else
                    result = true;
            }

            return result;
        }

        public static List<RequestPayment> GetNoPaidRequestPaymentByRequestIDByPaymentType(long requestID, List<int> requestPaymentTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          (t.IsPaid == null || t.IsPaid == false) &&
                                                          (requestPaymentTypes.Contains(t.PaymentType)))
                                                     .ToList();
            }
        }
        public static List<RequestPayment> GetPaidedRequestPaymentByRequestIDByPaymentType(long requestID, List<int> requestPaymentTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          (t.IsPaid == true) &&
                                                          (requestPaymentTypes.Contains(t.PaymentType)))
                                                     .ToList();
            }
        }

        public static List<RequestPaymentList> GetRequestPaymentListByPaymentTypes(long requestID, List<int> PaymentTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID && t.IsKickedBack == false && PaymentTypes.Contains(t.PaymentType))
                    .Select(t => new RequestPaymentList
                    {
                        AmountSum = t.AmountSum.ToString(),
                        BaseCostID = t.BaseCost.Title,
                        Cost = t.Cost.ToString(),
                        PaymentType = t.PaymentType.ToString(),
                        Tax = t.Tax.ToString()
                    })
                    .ToList();
            }
        }
        public static List<RequestPaymentList> GetIsPaidRequestPaymentListByPaymentTypes(long requestID, List<int> PaymentTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID && t.IsKickedBack == false && t.IsPaid == true && PaymentTypes.Contains(t.PaymentType))
                    .Select(t => new RequestPaymentList
                    {
                        AmountSum = t.AmountSum.ToString(),
                        BaseCostID = t.BaseCost.Title,
                        Cost = t.Cost.ToString(),
                        PaymentType = t.PaymentType.ToString(),
                        Tax = t.Tax.ToString()
                    }).ToList();
            }
        }

        public static int? GetMaxFactorNumber(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID && t.IsPaid == true).Max(t => t.FactorNumber);
            }
        }

        public static List<RequestPaymentList> GetReqeustPaymentListBylistPament(List<RequestPayment> requestPayments)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.RequestPayments.Where(t => requestPayments.Select(t2 => t2.ID).Contains(t.ID)).Select(t => new Data.RequestPaymentList
                {
                    AmountSum = t.AmountSum.ToString(),
                    BaseCostID = t.BaseCost.Title,
                    Cost = t.Cost.ToString(),
                    PaymentType = t.PaymentType.ToString(),
                    Tax = t.Tax.ToString()
                })
                    .ToList();
            }


        }


        public static List<CheckableItem> GetRequestPaymentFactorByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID && t.FactorNumber != null && t.IsKickedBack == false && (t.IsPaid == null || t.IsPaid == false)).Select(t => new CheckableItem { Name = t.FactorNumber.ToString(), ID = (int)t.FactorNumber }).Distinct().ToList();
            }
        }

        public static long? GetRequestPaymentCostByFactorNumber(long requestID, int FactorNumber, List<int> requestPaymentTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          (t.IsPaid == null || t.IsPaid == false) &&
                                                          (t.FactorNumber == FactorNumber) &&
                                                          (requestPaymentTypes.Contains(t.PaymentType))).Sum(t => t.AmountSum);
            }
        }

        public static List<RequestPayment> GetNoPaidRequestPaymentByRequestIDAndFactorNumber(long requestID, int factorNumber, List<int> requestPaymentTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.RequestID == requestID &&
                                                          t.IsKickedBack == false &&
                                                          t.FactorNumber == factorNumber &&
                                                          (t.IsPaid == null || t.IsPaid == false) &&
                                                          (requestPaymentTypes.Contains(t.PaymentType))).ToList();
            }
        }

        public static void DeleteAllReqeustPayment(int baseCostID, long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var reqeustPayment = context.RequestPayments.Where(t => t.RequestID == requestID && t.BaseCostID == baseCostID && t.IsKickedBack == false && (t.IsPaid == null || t.IsPaid == false));
                context.RequestPayments.DeleteAllOnSubmit(reqeustPayment);
                context.SubmitChanges();
            }

        }

        public static bool CheckUniqueId(string FicheNunmber, out long requestID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                if (context.RequestPayments.Any(t => t.FicheNunmber == FicheNunmber))
                {
                    requestID = context.RequestPayments.Where(t => t.FicheNunmber == FicheNunmber).Take(1).Select(t => t.RequestID).SingleOrDefault();
                    return true;
                }
                else
                {
                    requestID = 0;
                    return false;
                }
            }
        }

        public static List<RequestPayment> GetInstalationPaymentbyTelephonNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Request request = context.Requests.Where(t => t.TelephoneNo == telephoneNo && t.RequestTypeID == (byte)DB.RequestType.Dayri).OrderByDescending(t => t.EndDate).Take(1).SingleOrDefault();
                if (request != null)
                {
                    return context.RequestPayments.Where(t => t.RequestID == request.ID && t.IsKickedBack == false && t.IsPaid == true).ToList();
                }
                else
                {
                    return null;
                }

            }
        }

        public static RequestPayment GetPaidedRequestPayment(long requestId, int paymentType, int specialCostId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                RequestPayment result;
                var query = context.RequestPayments.Where(t =>
                                                             (t.RequestID == requestId) &&
                                                             (t.IsKickedBack == false) &&
                                                             (t.IsPaid == true) &&
                                                             (t.PaymentType == paymentType) &&
                                                             (t.BaseCostID == specialCostId)
                                                          );
                result = query.SingleOrDefault();
                return result;
            }
        }

    }
}
