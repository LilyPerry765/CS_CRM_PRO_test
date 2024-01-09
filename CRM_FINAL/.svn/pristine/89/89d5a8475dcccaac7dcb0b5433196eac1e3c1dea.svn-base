using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ChangeLocationSpecialWireDB
    {
        public static ChangeLocationSpecialWire GetChangeLocationWireByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeLocationSpecialWires.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }

        public static bool IsLastRequest(Request request)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<Request> tempRequests = new List<Request>();
                if (request.MainRequestID == null)
                {
                    tempRequests = context.Requests.Where(t => t.ID == request.ID || t.MainRequestID == request.ID).ToList();
                }
                else
                {
                    Request reqeustTemp = context.Requests.Where(t => t.ID == request.MainRequestID).SingleOrDefault();
                    tempRequests = context.Requests.Where(t => t.ID == reqeustTemp.ID || t.MainRequestID == reqeustTemp.ID).ToList();
                }

                tempRequests = tempRequests.Where(t => t.ID != request.ID).ToList();

                var changeLocationSpecialWire = context.ChangeLocationSpecialWires.Where(t => tempRequests.Select(t2 => t2.ID).Contains(t.RequestID));

                return !changeLocationSpecialWire.Any(t => t.SetupDate == null);
            }
        }
    }
}
