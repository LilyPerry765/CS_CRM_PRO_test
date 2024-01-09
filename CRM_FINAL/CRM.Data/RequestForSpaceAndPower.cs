using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForSpaceAndPower
    {
        public static void SaveSpaceAndPowerRequest(Request request, SpaceAndPower spaceAndPower, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                DB.Save(request, isNew);

                if (spaceAndPower != null)
                {
                    spaceAndPower.ID = request.ID;
                    spaceAndPower.Detach();
                    DB.Save(spaceAndPower, isNew);
                }
                
                ts.Complete();
            }
        }
    }
}
