using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public  class E1PositionDB
    {
        public static int SearchE1PositionCount(List<int> cities, List<int> centerIDs, List<int> DDFs, List<int> Bays, int number)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Positions.Where(t =>
                                            (cities.Count == 0 || cities.Contains(t.E1Bay.E1DDF.Center.Region.CityID)) &&
                                            (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.E1Bay.E1DDF.CenterID) : centerIDs.Contains(t.E1Bay.E1DDF.CenterID)) &&
                                            (DDFs.Count == 0 || DDFs.Contains(t.E1Bay.DDFID)) &&
                                             (Bays.Count == 0 || Bays.Contains(t.BayID)) &&
                                              (number == -1 || t.Number == number)).Count();
            }
        }

        public static List<E1Position> SearchPosition(List<int> cities, List<int> centerIDs, List<int> DDFs, List<int> Bays, int number, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Positions.Where(t =>
                                            (cities.Count == 0 || cities.Contains(t.E1Bay.E1DDF.Center.Region.CityID)) &&
                                            (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.E1Bay.E1DDF.CenterID) : centerIDs.Contains(t.E1Bay.E1DDF.CenterID)) &&
                                            (DDFs.Count == 0 || DDFs.Contains(t.E1Bay.DDFID)) &&
                                             (Bays.Count == 0 || Bays.Contains(t.BayID)) &&
                                              (number == -1 || t.Number == number)).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static E1Position GetE1PositionByID(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Positions.Where(t => t.ID == ID).SingleOrDefault();
            }
        }

        public static int GetCityByPositionID(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Positions.Where(t => t.ID == ID).SingleOrDefault().E1Bay.E1DDF.Center.Region.City.ID;
            }
        }

        public static Center GetCenterByPositionID(int positionID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Positions.Where(t => t.ID == positionID).SingleOrDefault().E1Bay.E1DDF.Center;
            }
        }

        public static List<CheckableItem> GetPositionCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Positions.Where(t => DB.CurrentUser.CenterIDs.Contains(t.E1Bay.E1DDF.CenterID)).Select(t=> new CheckableItem { ID = t.ID , Name = t.Number.ToString() , IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetPositionCheckableByBayIDs(List<int> bayIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Positions.Where(t => bayIDs.Contains(t.BayID)).Select(t => new CheckableItem { ID = t.ID , Name = t.Number.ToString() , IsChecked = false }).ToList();
            }
        }
    }
}
