using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLServiceCenterDB
    {
        public static List<ADSLServiceCenter> GetADSLServiceCenterByServiceId(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceCenters
                    .Where(t => t.ADSLServiceID == serviceID)
                    .ToList();
            }
        }

        public static List<ADSLServiceGroupCenter> GetADSLServiceGroupCenterByIServiceGroupId(int serviceGroupID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroupCenters
                    .Where(t => t.ServiceGroupID == serviceGroupID)
                    .ToList();
            }
        }
    }
}
