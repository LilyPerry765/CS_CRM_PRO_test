using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public class WaitingPossibilityDB
    {
        public static void WaitingPossibilitySave(Request request, WaitingList waitinglist)
       {
           using (TransactionScope ts = new TransactionScope())
           {

               request.IsWaitingList = true;
               request.Detach();
               DB.Save(request);

               waitinglist.Detach();
               DB.Save(waitinglist);



               ts.Complete();
           }
       }

        public static void investigatePossibilityWaitingListSave(Request request, WaitingList waitinglist, InvestigatePossibilityWaitinglist investigatePossibilityWaitinglist)
        {
            using (TransactionScope ts = new TransactionScope())
            {

                request.IsWaitingList = true;
                request.Detach();
                DB.Save(request);

                waitinglist.Detach();
                DB.Save(waitinglist);

                investigatePossibilityWaitinglist.ID = waitinglist.ID;
                investigatePossibilityWaitinglist.Detach();
                DB.Save(investigatePossibilityWaitinglist , true);


                ts.Complete();
            }
        }
    }
}
