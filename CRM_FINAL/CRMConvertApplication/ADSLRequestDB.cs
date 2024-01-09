using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class ADSLRequestDB
    {
        public static List<Request> GetADSLRequestinSupportStep()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.Status.RequestStepID == 102).ToList();
            }
        }

        public static List<Request> GetADSLInstallRequestinSupportStep()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.Status.RequestStepID == 303).ToList();
            }
        }

        public static List<Request> GetWirelessInstallRequestinSupportStep()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.Status.RequestStepID == 387 && t.EndDate == null).ToList();
            }
        }

        public static ADSLRequest GetADSLRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLRequests.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static Request GetRequestbyID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == id).SingleOrDefault();
            }
        }


    }
}
