using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLPAPCabinetAccuracyDB
    {
        public static List<ADSLPAPCabinetAccuracy> SearchCabenitAccuracy(List<int> centerIDs, int cabinetID, bool? removaFailure)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPCabinetAccuracies
                        .Where(t => (cabinetID == -1 || t.CabinetID == cabinetID) &&
                                    (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                    (!removaFailure.HasValue || (removaFailure.HasValue && removaFailure == true && t.CorrectDate != null) || (removaFailure.HasValue && removaFailure == false && t.CorrectDate == null)))
                        .ToList();
            }
        }

        public static ADSLPAPCabinetAccuracy GetCabenitAccuracyById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPCabinetAccuracies
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPCabinetAccuracies.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static bool CheckCabinetAccuracy(int cabinetID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPCabinetAccuracies.Any(t => t.CabinetID == cabinetID && t.CenterID == centerID && t.CorrectDate == null);
            }
        }
    }
}
