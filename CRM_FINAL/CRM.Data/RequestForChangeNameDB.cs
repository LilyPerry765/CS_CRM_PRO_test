using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForChangeNameDB
    {
        public static void SaveChangeNameRequest(Request request, ChangeName changeName, RequestLog requestLog, Telephone telephone, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                DB.Save(request, isNew);

                if (changeName != null)
                {
                    changeName.ID = request.ID;
                    changeName.Detach();
                    DB.Save(changeName, isNew);
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

        public static void SaveInquiryRequest(Inquiry inquiry, Request request, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                inquiry.RequestID = request.ID;
                inquiry.Insertdate = DB.GetServerDate();

                inquiry.Detach();
                DB.Save(inquiry, isNew);

                ts.Complete();
            }
        }
    }
}
