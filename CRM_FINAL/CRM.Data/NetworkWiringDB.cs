using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class NetworkWiringDB
    {
        public static NetworkWiring GetNetworkWiringByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.NetworkWirings.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<NetworkWiring> GetNetworkWiringByRequestIDs(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.NetworkWirings.Where(t => requestIDs.Contains(t.ID)).ToList();
            }
        }
    }
}
