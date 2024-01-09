using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLInstallCostCenterDB
    {
        public static List<ADSLInstalCostCenter> GetADSLInstallCostList(List<int> centers, long cost)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLInstalCostCenters.Where
                  (t =>
                      ((centers.Count == 0 || (centers.Contains((int)t.CenterID))) &&
                      (cost == -1 || t.InstallADSLCost == cost))).ToList();
            }
        }

        public static ADSLInstalCostCenter GetADSLInstallCostById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLInstalCostCenters.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static ADSLInstallCostCenterInfo GetADSLInstallCostInfoById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLInstalCostCenters.Where(t => t.ID == id)
                    .Select(t => new ADSLInstallCostCenterInfo
                    {
                        ID = t.ID,
                        CenterName = t.Center.CenterName,
                        Cost = t.InstallADSLCost
                    })
                    .SingleOrDefault();
            }
        }

        public static List<ADSLInstalCostCenter> GetAdsLInstallCostByCityId(int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLInstalCostCenters.Where(t => t.Center.Region.City.ID == cityID).ToList();
                //.Select(
                //t => new ADSLInstallCostCenterInfo
                //{
                //    Center=t.Center.CenterName,
                //    Cost=t.InstallADSLCost
                //}).ToList();
            }
        }

        public static ADSLInstalCostCenter GetADSLInstallCostByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLInstalCostCenters.Where(t => t.CenterID == centerID).SingleOrDefault();
            }
        }

        public static List<long?> GetTelNos()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Join(context.ADSLInstallRequests, r => r.ID, i => i.ID, (r, i) => new { R = r, I = i }).Select
                    (t => t.R.TelephoneNo).ToList();
            }
        }

    }
}
