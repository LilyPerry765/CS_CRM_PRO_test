using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class  Interface
    {

        // برای اطلاع تخلیه شدن تلفن به قسمت های دیگر برنامه
        public static void DischargeTelephones(List<Telephone> telephones)
        {
            try
            {
                if (telephones.Count != 0)
                {
                    DateTime currentDataTime = DB.GetServerDate();
                    List<ADSLPAPPort> ADSLPAPPorts = new List<ADSLPAPPort>();
                    List<RequestLog> requestLog = new List<RequestLog>();
                    using (MainDataContext context = new MainDataContext())
                    {

                        ADSLPAPPorts = context.ADSLPAPPorts.Where(t => telephones.Select(t2 => t2.TelephoneNo).Contains((long)t.TelephoneNo)).ToList();
                       
                        ADSLPAPPorts.ForEach(t =>
                               {
                                   requestLog.Add(new RequestLog { RequestTypeID = (int)DB.RequestType.ADSLDischargePAPCompany, TelephoneNo = t.TelephoneNo, UserID = DB.currentUser.ID, Date = currentDataTime });
                                   t.TelephoneNo = null;
                                   t.InstallDate = null;
                                   t.Status = (int)DB.ADSLPAPPortStatus.Discharge;
                                   t.Detach();
                               });

                    }

                    DB.UpdateAll(ADSLPAPPorts);
                    DB.SaveAll(requestLog);

                }
            }
            catch
            {
                throw new Exception("خطا در تخلیه پورت Pap");
            }

        }
    }
}
