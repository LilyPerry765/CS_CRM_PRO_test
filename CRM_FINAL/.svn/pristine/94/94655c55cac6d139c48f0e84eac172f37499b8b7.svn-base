using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLSetupContactInformationDB
    {
        public static ADSLSetupContactInformation GetContactInformationById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSetupContactInformations.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<ADSLSetupContactInformation> GetContactInformantionbyUserID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSetupContactInformations.Where(t => t.RequestID == requestID).ToList();

            }
        }
    }
}
