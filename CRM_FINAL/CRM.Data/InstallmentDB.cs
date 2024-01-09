using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class InstallmentDB
    {
        public static Installment GetInstallmentByID(long id)
        {
            using (MainDataContext context =new  MainDataContext())
            {
                return context.Installments.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<Installment> GetInstallmentByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Installments.Where(t => t.ID == requestID).ToList();
            }
        }
    }
}
