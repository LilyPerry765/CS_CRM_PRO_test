using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class PaymentDB
    {
        public static List<RequestPayment> GetADSLSellTrafficRequest()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.Request.RequestTypeID == 83 && t.BillID != null && t.PaymentID != null && t.Request.CreatorUserID == 1 && t.Request.InsertDate.Day == DB.GetServerDate().Day).ToList();
            }
        }

        public static List<RequestPayment> GetADSLChangeServiceRequest()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.Request.RequestTypeID == 38 && t.BillID != null && t.PaymentID != null && t.Request.CreatorUserID == 1 && t.Request.InsertDate.Day == DB.GetServerDate().Day).ToList();
            }
        }

        public static Request GertRequestbyID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static ADSLSellTraffic GertADSLSellTrafficRequestbyID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellTraffics.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static ADSLChangeService GertADSLChangeServiceRequestbyID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangeServices.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static ADSL GertADSLbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();
            }
        }

        public static ADSLService GetADSLServicebyServiceID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static int? GetADSLServiceCreditbyServiceID(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == serviceID).SingleOrDefault().ADSLServiceTraffic.Credit;
            }
        }

        public static List<RequestPayment> GetRequestPaymentforInstalment()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestPayments.Where(t => t.PaymentType == 3).ToList();
            }
        }

        public static bool HasInstalment(long requestpaymentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<InstallmentRequestPayment> instalments = context.InstallmentRequestPayments.Where(t => t.RequestPaymentID == requestpaymentID).ToList();

                if (instalments != null && instalments.Count != 0)
                    return true;
                else
                    return false;
            }
        }

        public static int GetADSLServiceServiceID(long ID, int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (requestTypeID == 35)
                    return (int)context.ADSLRequests.Where(t => t.ID == ID).SingleOrDefault().ServiceID;
                else
                    if (requestTypeID == 38)
                        return (int)context.ADSLChangeServices.Where(t => t.ID == ID).SingleOrDefault().NewServiceID;
                    else
                        return 0;
            }
        }

        public static ADSLService GetADSLServicebyID(int id)
        {
            using (MainDataContext context= new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
