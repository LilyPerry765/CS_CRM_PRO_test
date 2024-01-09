using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class PCMRockDB
    {
        public static List<PCMRock> SearchPCMRock(
            List<int> Cities,
            List<int> Centers,
             List<int> Rocks,
            int startRowIndex,
            int pageSize
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks
                    .Where(t =>
                        (Cities.Count == 0 || Cities.Contains(t.Center.Region.CityID)) &&
                        (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : Centers.Contains(t.CenterID)) &&
                        (Rocks.Count == 0 || Rocks.Contains(t.ID))
                          ).OrderBy(t => t.ID).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }
        public static int SearchPCMRockCount(
            List<int> Cities,
            List<int> Centers,
             List<int> Rocks)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks
                    .Where(t =>
                        (Cities.Count == 0 || Cities.Contains(t.Center.Region.CityID)) &&
                        (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : Centers.Contains(t.CenterID)) &&
                        (Rocks.Count == 0 || Rocks.Contains(t.ID))
                          )
                    .Count();
            }
        }

        public static PCMRock GetPCMRockByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetPCMRockCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks
                    .Where(t => (DB.CurrentUser.CenterIDs.Contains(t.CenterID)))
                    .OrderBy(t => t.Number)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Number.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
        public static List<CheckableItem> GetPCMRockCheckablebyCityCenterId(List<int> cityIDs, List<int> centerIDs)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks
                    .Where(t =>
                        (cityIDs.Count == 0 || cityIDs.Contains(t.Center.Region.CityID)) &&
                        (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centerIDs.Contains(t.CenterID)))
                        .OrderBy(t => t.Number)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Number.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static List<CheckableItem> GetPCMRockCheckableByCenterIDs(List<int> Centers)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                result = context.PCMRocks
                                .Where(t => Centers.Contains(t.CenterID))
                                .OrderBy(t => t.Number)
                                .Select(t => new CheckableItem
                                            {
                                                ID = t.ID,
                                                Name = t.Number.ToString(),
                                                IsChecked = false
                                            }
                                       )
                                .ToList();
                return result;
            }
        }
        public static List<CheckableItem> GetPCMRockCheckablebyCenterId(List<int> centerIDs)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks
                    .Where(t =>
                        (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centerIDs.Contains(t.CenterID)))
                        .OrderBy(t => t.Number)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Number.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }



        public static PCMRock GetPCMRockByNumber(int centerID, int number)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMRocks.Where(t => t.CenterID == centerID && t.Number == number).SingleOrDefault();
            }
        }
    }
}