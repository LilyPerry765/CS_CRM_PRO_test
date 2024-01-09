using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class TelephoneCycleFicheDB
    {
        public static TelephoneCycleFiche GetTelephoneCycle(long telephoneNo, byte subsidiaryCodeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                TelephoneCycleFiche cycle = context.TelephoneCycleFiches.Where(t => t.TelephoneNo == telephoneNo && t.SubsidiaryCodeType == subsidiaryCodeType).SingleOrDefault();

                if (cycle != null)
                    return cycle;
                else
                    return null;
            }
        }
    }
}
