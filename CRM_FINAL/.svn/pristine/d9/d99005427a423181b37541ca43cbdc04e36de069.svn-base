using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForChangeAddress
    {
        public static void SaveChangeAddressRequest(Request request, ChangeAddress changeAddress, RequestLog requestLog, Telephone telephone, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                DB.Save(request, isNew);

                if (changeAddress != null)
                {
                    changeAddress.ID = request.ID;
                    changeAddress.Detach();
                    DB.Save(changeAddress, isNew);
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
