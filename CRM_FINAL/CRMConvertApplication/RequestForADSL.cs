using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRMConvertApplication
{
    public static class RequestForADSL
    {
        public static void SaveADSLRequest(Request request, ADSLRequest ADSLRequest, bool isNew)
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

                if (ADSLRequest != null)
                {
                    if (request != null)
                        ADSLRequest.ID = request.ID;

                    ADSLRequest.Detach();
                    DB.Save(ADSLRequest, isNew);
                }               

                scope.Complete();
            }
        }
    }
}
