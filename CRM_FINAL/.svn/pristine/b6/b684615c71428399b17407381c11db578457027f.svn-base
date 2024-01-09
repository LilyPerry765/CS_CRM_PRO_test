using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class PCMShelfDB
    {
        public static List<PCMShelf> SearchPCMShelf(List<int> Cities, List<int> Centers, List<int> pCMRock, int ShelfNumber, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PCMShelf> result = new List<PCMShelf>();
                var query = context.PCMShelfs
                                   .Where(t =>
                                               (Cities.Count == 0 || Cities.Contains(t.PCMRock.Center.Region.CityID)) &&
                                               (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCMRock.CenterID) : Centers.Contains(t.PCMRock.CenterID)) &&
                                               (pCMRock.Count == 0 || pCMRock.Contains(t.PCMRockID) && (ShelfNumber == -1 || t.Number == ShelfNumber))
                                         )
                                   .AsQueryable();
                count = query.Count();

                result = query.OrderBy(t => t.ID)
                              .Skip(startRowIndex)
                              .Take(pageSize)
                              .ToList();

                return result;
            }
        }

        public static int SearchPCMShelfCount(
           List<int> Cities,
           List<int> Centers,
           List<int> pCMRock,
           int ShelfNumber)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMShelfs
                    .Where(t =>
                        (Cities.Count == 0 || Cities.Contains(t.PCMRock.Center.Region.CityID)) &&
                        (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCMRock.CenterID) : Centers.Contains(t.PCMRock.CenterID)) &&
                        (pCMRock.Count == 0 || pCMRock.Contains(t.PCMRockID) &&
                       (ShelfNumber == -1 || t.Number == ShelfNumber)
                          )).Count();
            }
        }

        public static PCMShelf GetPCMShelfByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMShelfs
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetPCMShelfCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMShelfs
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

        public static List<PCMShelf> GetPCMShelfByRockID(int PCMRockID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMShelfs.Where(t => t.PCMRockID == PCMRockID).OrderBy(t => t.Number).ToList();
            }
        }

        public static List<CheckableItem> GetCheckableItemPCMShelfByRockIDs(List<int> rockIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                result = context.PCMShelfs.Where(t => rockIDs.Contains(t.PCMRockID))
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

        //TODO:rad
        public static List<CheckableItem> GetPcmShelfCheckableItemsByPcmRockId(int pcmRockId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                result = context.PCMShelfs
                                .Where(ps => ps.PCMRockID == pcmRockId)
                                .OrderBy(ps => ps.Number)
                                .Select(ps => new CheckableItem
                                                  {
                                                      ID = ps.ID,
                                                      Name = ps.Number.ToString(),
                                                      IsChecked = false
                                                  }
                                       )
                                .ToList();

                return result;
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.PCMRocks.SingleOrDefault(t => t.ID == context.PCMShelfs.Where(p => p.ID == id).SingleOrDefault().PCMRockID).Center.Region.CityID;

            }
        }

        public static PCMShelf GetPCMShelfByNumber(int PCMRockID, int number)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMShelfs.Where(t => t.PCMRockID == PCMRockID && t.Number == number).SingleOrDefault();
            }
        }
    }
}