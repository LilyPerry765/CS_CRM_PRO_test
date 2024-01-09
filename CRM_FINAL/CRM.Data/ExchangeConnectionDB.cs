using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ExchangeGSMConnectionDB
    {
        public static List<ExchangeGSMConnection> GetExchangeGSMConnectionByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeGSMConnections.Where(t => t.RequestID == requestID).ToList();

            }
        }
    }
}
