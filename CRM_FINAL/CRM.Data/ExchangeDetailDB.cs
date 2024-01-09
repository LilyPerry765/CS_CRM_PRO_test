using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class ExchangeDetailDB
    {

        public static void save(Bucht oldBucht, Bucht newBucht)
        {
            //using (TransactionScope ts = new TransactionScope())
            //{

            newBucht.ConnectionID = oldBucht.ConnectionID;
            newBucht.PortNo = oldBucht.PortNo;
            newBucht.PCMPortID = oldBucht.PCMPortID;

            newBucht.PCMPortID = oldBucht.PCMPortID;

            newBucht.CabinetInputID = oldBucht.CabinetInputID;
            newBucht.CablePairID = oldBucht.CablePairID;
            newBucht.Status = 1;
            newBucht.Detach();
            DB.Save(newBucht);

            oldBucht.ConnectionID = null;
            oldBucht.PortNo = null;
            oldBucht.PCMPortID = null;
            oldBucht.BuchtTypeID = 0;
            oldBucht.CabinetInputID = null;
            oldBucht.CablePairID = null;
            oldBucht.Status = 0;
            oldBucht.Detach();
            DB.Save(oldBucht);
            //ts.Complete();
            //}

        }
    }
}
