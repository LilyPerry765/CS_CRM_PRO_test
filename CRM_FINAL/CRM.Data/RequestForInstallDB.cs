using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Collections.ObjectModel;


namespace CRM.Data
{
    public static class RequestForInstallDB
    {
        public static void SaveRequest(Request request, InstallRequest installInfo, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (isNew)
                 request.ID = DB.GenerateRequestID();

                request.Detach();
                DB.Save(request, isNew);

                if (installInfo != null)
                {
                    installInfo.RequestID = request.ID;
                    installInfo.Detach();
                    DB.Save(installInfo, isNew);
                }

                installInfo.RequestID = request.ID;

                ts.Complete();
            }
        }
    }
}
