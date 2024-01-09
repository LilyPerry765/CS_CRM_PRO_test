using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForzeroStatusDB
    {
        public static void SaveRequest(Request request, ZeroStatus zeroStatus, RequestLog requestLog, Telephone telephone, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                DB.Save(request, isNew);

                if (zeroStatus != null)
                {
                    zeroStatus.ID = request.ID;                    

                    zeroStatus.Detach();
                    DB.Save(zeroStatus, isNew);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                }

                if (telephone != null)
                {
                    telephone.Detach();
                    DB.Save(telephone, isNew);
                }

                ts.Complete();
            }
        }
    }
}
