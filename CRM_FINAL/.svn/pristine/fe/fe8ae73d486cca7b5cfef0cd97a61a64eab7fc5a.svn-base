using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class ADSLIPDB
    {
        public static ADSLGroupIP GetADSLGroupIP(string iP)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => t.StartRange == iP).SingleOrDefault();
            }
        }

        public static ADSLIP GetADSLIP(string iP)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => t.IP == iP).SingleOrDefault();
            }
        }

        public static List<ADSLIP> GetADSLIPExpired()
        {
            DateTime yesterday = DB.GetServerDate().AddDays(-1);

            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => t.ExpDate.Value.Year == yesterday.Year && t.ExpDate.Value.Month == yesterday.Month && t.ExpDate.Value.Day == yesterday.Day).ToList();
            }
        }

        public static List<ADSLIP> GetADSLIPExpired20()
        {
            DateTime day20 = DB.GetServerDate().AddDays(-20);

            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => t.ExpDate.Value.Year == day20.Year && t.ExpDate.Value.Month == day20.Month && t.ExpDate.Value.Day == day20.Day).ToList();
            }
        }

        public static List<ADSLGroupIP> GetADSLGroupIPExpired()
        {
            DateTime yesterday = DB.GetServerDate().AddDays(-1);

            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => t.ExpDate.Value.Year == yesterday.Year && t.ExpDate.Value.Month == yesterday.Month && t.ExpDate.Value.Day == yesterday.Day).ToList();
            }
        }

        public static List<ADSLGroupIP> GetADSLGroupIPExpired20()
        {
            DateTime day20 = DB.GetServerDate().AddDays(-20);

            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => t.ExpDate.Value.Year == day20.Year && t.ExpDate.Value.Month == day20.Month && t.ExpDate.Value.Day == day20.Day).ToList();
            }
        }

        public static ADSLIPHistory GetHistorybyTelephoneNoandIP(long telephoneNo, long ipID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPHistories.Where(t => t.TelephoneNo == telephoneNo && t.IPID == ipID && t.EndDate == null).SingleOrDefault();
            }
        }

        public static bool HasHistorybyTelephoneNoandIP(long telephoneNo, long ipID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLIPHistory history= context.ADSLIPHistories.Where(t => t.TelephoneNo == telephoneNo && t.IPID == ipID).SingleOrDefault();

                if (history != null)
                    return false;
                else
                    return true;
            }
        }

        public static ADSLIPHistory GetHistorybyTelephoneNoandGroupIP(long telephoneNo, long groupIpID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPHistories.Where(t => t.TelephoneNo == telephoneNo && t.IPGroupID == groupIpID && t.EndDate == null).SingleOrDefault();
            }
        }

        public static bool HasHistorybyTelephoneNoandGroupIP(long telephoneNo, long groupIpID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLIPHistory history = context.ADSLIPHistories.Where(t => t.TelephoneNo == telephoneNo && t.IPGroupID == groupIpID).SingleOrDefault();

                if (history != null)
                    return false;
                else
                    return true;
            }
        }

        public static List<ADSLGroupIP> GetADSLIP30DaysExprire()
        {
            DateTime now = DB.GetServerDate();
            DateTime Day30 = DateTime.Now.AddDays(-30);

            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => t.ExpDate != null && t.ExpDate < now ).ToList();
            }
        }
    }
}
