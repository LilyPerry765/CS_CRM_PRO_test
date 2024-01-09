using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public  class TechnicalSpecificationsOfADSLDB
    {


       public static TechnicalSpecificationsOfADSLInfo Search(long requestID)
        {
            using (MainDataContext context =new MainDataContext())
            {


                return context.ADSLDischarges.Where(t => t.ID == requestID).Select(t => new TechnicalSpecificationsOfADSLInfo
                {
                    //PortNo = t.ADSL.ADSLPort.PortNo,
                    //InputBucht = DB.GetConnectionByBuchtID(t.ADSL.ADSLPort.InputBucht),
                    //OutPutBucht = DB.GetConnectionByBuchtID(t.ADSL.ADSLPort.OutBucht),
                    //CustomerBucht = DB.GetConnectionByBuchtID(t.ADSL.ADSLPort.Bucht1.Bucht1.ID),// Relation BuchtIDConnectedOtherBucht  From Bucht with ID From Bucht
                    //CustomerPort = t.ADSL.ADSLPort.Bucht.SwitchPort.PortNo, // Relation InputBucht From ADSLPort with ID From Bucht
                }).SingleOrDefault();
            }
        }
    }
}
