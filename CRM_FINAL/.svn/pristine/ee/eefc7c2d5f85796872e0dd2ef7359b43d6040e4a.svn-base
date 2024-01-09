using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class PaymentFicheDB
    {
  
        public static List<PaymentFiche> GetPamentFicheByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PaymentFiches.Where(t => t.InstallmentID == id).ToList();
            }
        }

        public static PaymentFiche GetPaymentFicheByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PaymentFiches.Where(t => t.ID == id).SingleOrDefault(); 
            }
        }

        public static List<CheckableItem> GetPaymentFicheCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PaymentFiches.Select(t => new CheckableItem {LongID = t.ID , Name=t.FicheNunmber.ToString() , IsChecked=false}).ToList();
            }
        }
        public static List<CheckableItem> GetPaymentFicheCheckable(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PaymentFiches.Where(t=>t.Installment.Request.ID == requestID).Select(t => new CheckableItem { LongID = t.ID, Name = t.FicheNunmber.ToString(), IsChecked = false }).ToList();
            }
        }
    }
}
