using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class InstallmentRequestPaymentDB
    {
        public static List<InstallmentRequestPayment> GetInstallmentRequestPaymentByRequestPaymentID(long RequestPaymentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.RequestPaymentID == RequestPaymentID && (t.IsDeleted == null || t.IsDeleted == false)).ToList();
            }
        }

        public static List<InstalmentRequestPaymentInfo> GetInstallmentRequestPaymentByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.TelephoneNo == telephoneNo && (t.IsDeleted == null || t.IsDeleted == false))
                    .Select(t => new InstalmentRequestPaymentInfo
                    {
                        TelephoneNo = t.TelephoneNo.ToString(),
                        RequestID = t.RequestPayment.RequestID.ToString(),
                        Cost = t.Cost.ToString(),
                        StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                        IsPaid = (bool)t.IsPaid
                    })
                    .ToList();
            }
        }

        public static List<InstallmentRequestPayment> GetInstallmentRequestPaymentRemainByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.TelephoneNo == telephoneNo && t.IsPaid != true && (t.IsDeleted == null || t.IsDeleted == false)).OrderBy(t => t.StartDate).ToList();
            }
        }

        public static List<InstalmentRequestPaymentInfo> GetInstallmentRequestPaymentInfoRemainByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.TelephoneNo == telephoneNo && t.IsPaid != true && (t.IsDeleted == null || t.IsDeleted == false)).OrderBy(t => t.StartDate)
                    .Select(t => new InstalmentRequestPaymentInfo
                    {
                        TelephoneNo = t.TelephoneNo.ToString(),
                        RequestID = t.RequestPayment.RequestID.ToString(),
                        Cost = t.Cost.ToString(),
                        StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                        IsPaid = (bool)t.IsPaid
                    })
                    .ToList();
            }
        }

        public static string GetSumPaidInstallmenttByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<InstallmentRequestPayment> instalmentsList = context.InstallmentRequestPayments.Where(t => t.TelephoneNo == telephoneNo && t.IsPaid == true && (t.IsDeleted == null || t.IsDeleted == false)).ToList();

                if (instalmentsList.Count != 0)
                    return instalmentsList.Sum(t => t.Cost).ToString();
                return "";
            }
        }

        public static string GetSumRemainInstallmenttByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<InstallmentRequestPayment> instalmentsList = context.InstallmentRequestPayments.Where(t => t.TelephoneNo == telephoneNo && t.IsPaid == false && (t.IsDeleted == null || t.IsDeleted == false)).ToList();

                if (instalmentsList.Count != 0)
                    return instalmentsList.Sum(t => t.Cost).ToString();
                return "";
            }
        }

        public static InstallmentRequestPayment GetInstallmentRequestPaymentByID(long ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.ID == ID).SingleOrDefault();
            }
        }

        public static bool ChechExistEndDate(InstallmentRequestPayment item)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Any(t => t.EndDate == item.EndDate && t.ID != item.ID && t.RequestPaymentID == item.RequestPaymentID && (t.IsDeleted == null || t.IsDeleted == false));
            }
        }

        public static List<InstallmentRequestPayment> GetInstalmentbyRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<InstallmentRequestPayment> instalments = context.InstallmentRequestPayments.Where(t => t.RequestPayment.RequestID == requestID && (t.IsDeleted == null || t.IsDeleted == false)).ToList();

                if (instalments != null)
                    return instalments;
                else
                    return null;
            }
        }

        public static List<InstalmentRequestPaymentList> GetInstalmentRequestPaymentList(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.RequestPaymentID == requestID && (t.IsDeleted == null || t.IsDeleted == false))
                    .Select(t => new InstalmentRequestPaymentList
                    {
                        StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                        InstalmentCost = t.Cost.ToString()
                    })
                    .ToList();
            }
        }

        public static InstallmentRequestPayment GetLastPaidInstalmentbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<InstallmentRequestPayment> instalmentList = context.InstallmentRequestPayments.Where(t => t.TelephoneNo == telephoneNo && t.IsPaid == true && (t.IsDeleted == null || t.IsDeleted == false)).ToList();

                if (instalmentList != null && instalmentList.Count != 0)
                    return instalmentList.OrderByDescending(t => t.EndDate).FirstOrDefault();
                else
                    return null;
            }
        }

        public static List<InstalmentRequestPaymentInfo> GetInstallmentRequestPaymentInfo(List<InstallmentRequestPayment> instalment)
        {
            return instalment.Select(t => new InstalmentRequestPaymentInfo
            {
                TelephoneNo = t.TelephoneNo.ToString(),
                //RequestID = t.RequestPayment.RequestID.ToString(),
                Cost = t.Cost.ToString(),
                StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short)
            }).ToList();

        }

        public static List<InstalmentBillingInfo> GetInstallmentRequestPaymentInfoforCycle(DateTime startDate, DateTime endDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.EndDate > startDate && t.EndDate <= endDate && t.IsPaid != true && (t.IsDeleted == null || t.IsDeleted == false))
                                                         .GroupBy(g => new { g.TelephoneNo })
                                                         .Select(group => new InstalmentBillingInfo 
                                                         { 
                                                             TelephoneNo = (long)group.Key.TelephoneNo,
                                                             Cost = group.Sum(c => c.Cost) 
                                                         }).ToList();
            }
        }

        public static List<InstallmentRequestPayment> GetInstallmentRequestPaymentforCycle(DateTime startDate, DateTime endDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayments.Where(t => t.EndDate > startDate && t.EndDate <= endDate && t.IsPaid != true && (t.IsDeleted == null || t.IsDeleted == false)).ToList();
            }
        }
    }
}
