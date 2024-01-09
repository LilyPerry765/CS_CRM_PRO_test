using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLTelephoneAccuracyDB
    {
        public static List<ADSLTelephoneAccuracy> SearchTelephoneAccuracy(List<int> centerIDs, long telephoenNo, bool? removaFailure)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTelephoneAccuracies
                        .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                    (telephoenNo == -1 || t.TelephoneNo == telephoenNo) &&
                                    (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                    (!removaFailure.HasValue || (removaFailure.HasValue && removaFailure == true && t.CorrectDate != null) || (removaFailure.HasValue && removaFailure == false && t.CorrectDate == null)))
                        .ToList();
            }
        }

        public static ADSLTelephoneAccuracy GetTelephoneAccuracyById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTelephoneAccuracies
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static int GetCityForTelephone(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTelephoneAccuracies.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static bool CheckTelephoneAccuracy(long telephoneNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTelephoneAccuracies.Any(t => t.TelephoneNo == telephoneNo && t.CenterID == centerID && t.CorrectDate == null);
            }
        }
    }
}
