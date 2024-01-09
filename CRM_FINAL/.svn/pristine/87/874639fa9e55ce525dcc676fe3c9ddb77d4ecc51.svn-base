using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLInstallRequestDB
    {
        public static ADSLInstallRequest GetADSLInstallRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLInstallRequests.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
