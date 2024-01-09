using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class Failure117CabenitAccuracyDB
    {
        public static List<Failure117CabenitAccuracy> SearchCabenitAccuracy(List<int> centerIDs, int cabinetID, bool? removaFailure)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117CabenitAccuracies
                        .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                    (cabinetID == -1 || t.CabinetID == cabinetID) &&
                                    (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                    (!removaFailure.HasValue || (removaFailure.HasValue && removaFailure == true && t.CorrectDate != null) || (removaFailure.HasValue && removaFailure == false && t.CorrectDate == null)))
                        .ToList();
            }
        }

        public static Failure117CabenitAccuracy GetCabenitAccuracyById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117CabenitAccuracies
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static int GetCityForCabinet(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117CabenitAccuracies.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static bool CheckCabinetAccuracy(int cabinetID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117CabenitAccuracies.Any(t => t.CabinetID == cabinetID && t.CenterID == centerID && t.CorrectDate == null);
            }
        }

        public static List<Failure117PostAccuracy> SearchPostAccuracy(List<int> centerIDs, int cabinetID, int postID, bool? removaFailure)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117PostAccuracies
                        .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                    (cabinetID == -1 || t.CabinetID == cabinetID) &&
                                    (postID == -1 || t.PostID == postID) &&
                                    (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                    (!removaFailure.HasValue || (removaFailure.HasValue && removaFailure == true && t.CorrectDate != null) || (removaFailure.HasValue && removaFailure == false && t.CorrectDate == null)))
                        .ToList();
            }
        }

        public static Failure117PostAccuracy GetPostAccuracyById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117PostAccuracies
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static int GetCityForPost(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117PostAccuracies.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static bool CheckPostAccuracy(int cabinetID, int postID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117PostAccuracies.Any(t => t.CabinetID == cabinetID && t.PostID == postID && t.CenterID == centerID && t.CorrectDate == null);
            }
        }

        public static List<Failure117TelephoneAccuracy> SearchTelephoneAccuracy(List<int> centerIDs, long telephoenNo, bool? removaFailure)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117TelephoneAccuracies
                        .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                    (telephoenNo == -1 || t.TelephoneNo == telephoenNo) &&
                                    (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                    (!removaFailure.HasValue || (removaFailure.HasValue && removaFailure == true && t.CorrectDate != null) || (removaFailure.HasValue && removaFailure == false && t.CorrectDate == null)))
                        .ToList();
            }
        }

        public static Failure117TelephoneAccuracy GetTelephoneAccuracyById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117TelephoneAccuracies
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static int GetCityForTelephone(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117TelephoneAccuracies.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static bool CheckTelephoneAccuracy(long telephoneNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117TelephoneAccuracies.Any(t => t.TelephoneNo == telephoneNo && t.CenterID == centerID && t.CorrectDate == null);
            }
        }

        public static Failure117TelephoneAccuracy GetTelephoneAccuracy(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Failure117TelephoneAccuracy telephoneAccuracy = context.Failure117TelephoneAccuracies.Where(t => t.TelephoneNo == telephoneNo && t.CorrectDate == null).SingleOrDefault();

                if (telephoneAccuracy != null)
                    return telephoneAccuracy;
                else
                    return null;
            }
        }
    }
}
