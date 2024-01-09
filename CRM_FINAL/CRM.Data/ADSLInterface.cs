using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLInterface
    {

       public static ADSLBucht GetADSLBuchtByTelephone(long telphoneNo)
        {
           using(MainDataContext context = new MainDataContext())
           {
               return context.ADSLPAPPorts
                   .Where(t=>t.TelephoneNo == telphoneNo)
                   .Select(t => new ADSLBucht 
               { 
                   ColumnNo = t.RowNo,
                   RowNo = t.ColumnNo,
                   BuchtNo = t.BuchtNo ,
                   PAPName = t.PAPInfo.Title
               }
               ).SingleOrDefault();
           }
        }


    }
}
