using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForCutAndEstablishDB
    {
        public static void SaveRequest(Request request, CutAndEstablish cutAndEstablish, RequestLog requestLog, Telephone telephone, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (isNew)
                {
                    request.ID = DB.GenerateRequestID();
                    request.Detach();
                    DB.Save(request, isNew);
                }
                else
                {
                    request.Detach();
                    DB.Save(request, isNew);
                }
                if (cutAndEstablish != null)
                {
                    cutAndEstablish.ID = request.ID;
                    cutAndEstablish.Detach();
                    DB.Save(cutAndEstablish, isNew);
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
