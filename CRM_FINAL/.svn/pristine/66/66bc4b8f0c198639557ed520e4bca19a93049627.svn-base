using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{ 
    public class RequestForWirelessDB
    {
        public static void SaveWirelessRequest(Request request, WirelessRequest WirelessRequest, ADSLIP iPStatic, ADSLGroupIP groupIPStatic, Wireless wireless, RequestLog requestLog, bool isNew)
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

                    if (WirelessRequest != null)
                    {
                        if (request != null)
                            WirelessRequest.ID = request.ID;

                        WirelessRequest.Detach();
                        DB.Save(WirelessRequest, isNew);
                    }

                    if (iPStatic != null)
                    {
                        iPStatic.Detach();
                        DB.Save(iPStatic);
                    }

                    if (groupIPStatic != null)
                    {
                        groupIPStatic.Detach();
                        DB.Save(groupIPStatic);
                    }

                    if (wireless != null)
                    {
                        wireless.Detach();

                        if (DB.SearchByPropertyName<Wireless>("TelephoneNo", wireless.TelephoneNo).SingleOrDefault() == null)
                            DB.Save(wireless, true);
                        else
                            DB.Save(wireless, false);
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

        public static void SaveWirelessChangeServiceRequest(Request request, WirelessChangeService wirelessChangeService,  bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                DB.Save(request, isNew);

                if (wirelessChangeService != null)
                {
                    wirelessChangeService.ID = request.ID;
                    wirelessChangeService.Detach();
                    DB.Save(wirelessChangeService, isNew);
                }

                scope.Complete();
            }
        }

    }
}
