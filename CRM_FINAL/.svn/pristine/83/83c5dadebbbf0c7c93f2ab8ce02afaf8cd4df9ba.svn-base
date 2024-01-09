using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class InstallLineDB
    {
        public static void SaveInstallLine(Request request,Counter contor,InstallLine line, Telephone tel)
        {
            using (TransactionScope ts = new TransactionScope())
            {

                   request.Detach();
                   DB.Save(request);

                   if (contor.TelephoneNo != 0)
                   {
                       contor.Detach();
                       DB.Save(contor);
                   }
                   if (line.WiringID != 0)
                   {

                       line.CounterID = contor.ID;
                       line.Detach();
                       DB.Save(line);
                   }

                   if (tel != null && tel.TelephoneNo != 0)
                   {
                       tel.Detach();
                       DB.Save(tel);
                   }

                   ts.Complete();
               

              
               
            }
        }

        public static void UpdateInstallLine(Request request, Counter contor, InstallLine line , bool isLine=true)
        {
            using (TransactionScope ts = new TransactionScope())
            {

                request.Detach();
                DB.Save(request);

                contor.Detach();
                DB.Save(contor);

                if (isLine)
                {
                    line.CounterID = contor.ID;
                    line.Detach();
                    DB.Save(line);
                }


                ts.Complete();




            }
        }

        public static InstallLine GetInstallLineByWiringID(long wiringID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallLines.Where(t => t.WiringID == wiringID).SingleOrDefault();
            }
        }

        public static void DeleteInstallLineByissueWiringID(long issueWiringID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                var installLine = context.InstallLines.Where(t => t.Wiring.IssueWiringID == issueWiringID);
                context.InstallLines.DeleteAllOnSubmit(installLine);
                context.SubmitChanges();
            } 
        }
    }

    
}
