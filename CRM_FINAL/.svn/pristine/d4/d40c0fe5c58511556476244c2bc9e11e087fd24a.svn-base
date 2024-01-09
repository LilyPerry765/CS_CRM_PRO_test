using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class TelephoneConnectionInstallmentDB
    {
        public static long GetTelephoneConnectionInstallmnentIdByRequestPaymentId(long requestPaymentId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                long result = 0;

                result = context.TelephoneConnectionInstallments
                                .Where(tci => tci.RequestPaymentID == requestPaymentId)
                                .Select(tci => tci.ID)
                                .SingleOrDefault();

                return result;
            }
        }

        public static TelephoneConnectionInstallment GetTelephoneConnectionInstallmentById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                TelephoneConnectionInstallment result;
                var query = context.TelephoneConnectionInstallments.Where(tci => tci.ID == id);
                result = query.SingleOrDefault();
                return result;
            }
        }
    }
}
