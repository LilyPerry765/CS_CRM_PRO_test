using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class InstalmentDB
    {
        public static List<InstallmentRequestPayment_Temp> GetInstalmentTemp()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayment_Temps.ToList();
            }
        }

        public static InstallmentRequestPayment_Temp GetInstalmentTempbyID(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallmentRequestPayment_Temps.Where(t=>t.Phone==telephoneNo).SingleOrDefault();
            }
        }
    }
}
