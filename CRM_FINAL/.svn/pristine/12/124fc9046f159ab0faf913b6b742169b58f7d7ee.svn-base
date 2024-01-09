using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForInquiryDB
    {
        public static void SaveRequest(Inquiry inquiry, Request request, bool isNew)
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
