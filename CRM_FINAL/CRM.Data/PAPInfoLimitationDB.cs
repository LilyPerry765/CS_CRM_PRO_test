using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CRM.Data
{
    public static class PAPInfoLimitationDB
    {
        public static IEnumerable SearchPAPInfoLimitation(int papID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoLimitations
                    .Where(t => (t.PAPInfoID == papID))
                    .OrderBy(t => t.City.Name)
                    .ToList();
            }
        }

        public static int GetRequestNoLimitation(int papID, int cityID)
        {
            int instalNo = -1;
            using (MainDataContext context = new MainDataContext())
            {
                if (context.PAPInfoLimitations.Where(t => t.PAPInfoID == papID && t.CityID == cityID).SingleOrDefault() != null)
                    instalNo = (int)context.PAPInfoLimitations.Where(t => t.PAPInfoID == papID && t.CityID == cityID).SingleOrDefault().InstallRequestNo;
            }

            return instalNo;
        }

        public static int GetDischargeNoLimitation(int papID, int cityID)
        {
            int dischargeNo = -1;
            using (MainDataContext context = new MainDataContext())
            {
                if (context.PAPInfoLimitations.Where(t => t.PAPInfoID == papID && t.CityID == cityID).SingleOrDefault() != null)
                    dischargeNo = (int)context.PAPInfoLimitations.Where(t => t.PAPInfoID == papID && t.CityID == cityID).SingleOrDefault().DischargeRequestNo;
            }

            return dischargeNo;
        }

        public static int GetFeasibilityNoLimitation(int papID, int cityID)
        {
            int feasibilityNo = -1;
            using (MainDataContext context = new MainDataContext())
            {
                if (context.PAPInfoLimitations.Where(t => t.PAPInfoID == papID && t.CityID == cityID).SingleOrDefault() != null)
                    feasibilityNo = (int)context.PAPInfoLimitations.Where(t => t.PAPInfoID == papID && t.CityID == cityID).SingleOrDefault().FeasibilityNo;
            }

            return feasibilityNo;
        }
    }
}
