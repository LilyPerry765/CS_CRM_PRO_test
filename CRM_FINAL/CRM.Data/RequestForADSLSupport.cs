using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForADSLSupport
    {
        public static void SaveADSLSupportRequest(Request request, ADSLSupportRequest aDSLSupportRequest, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    if (isNew)
                        request.ID = DB.GenerateRequestID();
                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (aDSLSupportRequest != null)
                {
                    if (request != null)
                        aDSLSupportRequest.ID = request.ID;

                    aDSLSupportRequest.Detach();
                    DB.Save(aDSLSupportRequest, isNew);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                }

                scope.Complete();
            }
        }
    }
}
