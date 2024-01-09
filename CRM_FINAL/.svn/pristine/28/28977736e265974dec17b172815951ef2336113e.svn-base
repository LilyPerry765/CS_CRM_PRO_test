using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestForSpecialServiceDB
    {
        public static void SaveRequest(Request request, List<SpecialService> specialServiceList, List<RequestLog> requestLogList, Telephone telephone, bool isNew)
        {
            //using (TransactionScope ts = new TransactionScope())
            //{
            //    if (isNew)
            //        request.ID = DB.GenerateRequestID();
            //    request.Detach();
            //    DB.Save(request, isNew);

            //    if (specialServiceList.Count != 0)
            //    {
            //        foreach (SpecialService item in specialServiceList)
            //        {
            //            if (item != null)
            //            {
            //                item.RequestID = request.ID;
                            
            //                item.Detach();
            //                if (item.ID != 0)
            //                    DB.Save(item, isNew);
            //                else
            //                    DB.Save(item, true);
            //            }
            //        }
            //    }

            //    if (requestLogList != null)
            //    {
            //        if (requestLogList.Count != 0)
            //            foreach (RequestLog item in requestLogList)
            //            {
            //                item.Date = DB.GetServerDate();
            //                item.Detach();
            //                DB.Save(item, true);
            //            }
            //    }

            //    if (telephone != null)
            //    {
            //        telephone.Detach();
            //        DB.Save(telephone, isNew);
            //    }

            //    ts.Complete();
            //}
        }

        public static void SaveSingleRequest(Request request, SpecialService specialService, List<RequestLog> requestLogList, Telephone telephone, bool isNew)
        {
            //using (TransactionScope ts = new TransactionScope())
            //{
            //    if (isNew)
            //        request.ID = DB.GenerateRequestID();
            //    request.Detach();
            //    DB.Save(request, isNew);

            //    specialService.RequestID = request.ID;
            //    specialService.Detach();
            //    DB.Save(specialService, isNew);

            //    if (requestLogList != null)
            //    {
            //        if (requestLogList.Count != 0)
            //            foreach (RequestLog item in requestLogList)
            //            {
            //                item.Date = DB.GetServerDate();
            //                item.Detach();
            //                DB.Save(item, true);
            //            }
            //    }

            //    if (telephone != null)
            //    {
            //        telephone.Detach();
            //        DB.Save(telephone, isNew);
            //    }

            //    ts.Complete();
            //}
        }
    }
}
