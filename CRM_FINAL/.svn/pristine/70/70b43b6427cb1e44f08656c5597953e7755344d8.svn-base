using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Data.Linq.Mapping;

namespace CRM.Data
{
    public static class InstalmentRequestPaymentDB
    {
        public static List<InstalmentRequestPaymentInfo> SerachInstalmentRequestPayment()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Select(t => new InstalmentRequestPaymentInfo
                {
                    IsChequeByte = t.IsCheque,
                    ChequeNumber = t.ChequeNumber.ToString(),
                    Cost = t.Cost.ToString(),
                    TelephoneNo = t.TelephoneNo.ToString(),
                    StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                    EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short)
                }).ToList();
            }
        }

        public static List<InstalmentRequestPaymentInfo> GetInstalmentRequestPaymentList(long? TelNo, DateTime? startFromDate, DateTime? startToDate,
                                                                                        DateTime? endFromDate, DateTime? endToDate,bool? IsCheque)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t =>
                    (TelNo==0 || TelNo == t.TelephoneNo)
                    && (!startFromDate.HasValue || startFromDate <= t.StartDate)
                    && (!endToDate.HasValue || endToDate >= t.StartDate)
                    && (!endFromDate.HasValue || endFromDate <= t.EndDate)
                    && (!endToDate.HasValue || endToDate >= t.EndDate)
                    && (!IsCheque.HasValue || IsCheque == t.IsCheque)).Select(t => new InstalmentRequestPaymentInfo
                    {
                        IsChequeByte = t.IsCheque,
                        ChequeNumber = t.ChequeNumber.ToString(),
                        Cost = t.Cost.ToString(),
                        TelephoneNo = t.TelephoneNo.ToString(),
                        StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short)
                    }).ToList();
            }
        }

        public static List<InstallmentRequestPayment> GetInstalmentRequestPaymentbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
               return context.InstallmentRequestPayments.Where(t => t.TelephoneNo == telephoneNo).ToList();
            }
        }

        public static List<InstallmentRequestPayment> GetInstalmentRequestPaymentbyPaymentID(long paymentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.RequestPaymentID == paymentID).ToList();
            }
        }
    }
}
