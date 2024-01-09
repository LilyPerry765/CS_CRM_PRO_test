using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class E1NumberDB
    {

        public static int SearchE1NumberCount(List<int> cities, List<int> centerIDs, List<int> DDFs, List<int> Bays, List<int> positions, int number)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Numbers.Where(t =>
                                            (cities.Count == 0 || cities.Contains(t.E1Position.E1Bay.E1DDF.Center.Region.CityID)) &&
                                            (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.E1Position.E1Bay.E1DDF.CenterID) : centerIDs.Contains(t.E1Position.E1Bay.E1DDF.CenterID)) &&
                                            (DDFs.Count == 0 || DDFs.Contains(t.E1Position.E1Bay.DDFID)) &&
                                            (Bays.Count == 0 || Bays.Contains(t.E1Position.BayID)) &&
                                            (positions.Count == 0 || positions.Contains(t.PositionID)) &&
                                            (number == -1 || t.Number == number)).Count();
            }
        }


        public static List<E1Number> SearchNumber(List<int> cities, List<int> centerIDs, List<int> DDFs, List<int> Bays, List<int> positions, int number, int startRowIndex, int pageSize)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Numbers.Where(t =>
                                            (cities.Count == 0 || cities.Contains(t.E1Position.E1Bay.E1DDF.Center.Region.CityID)) &&
                                            (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.E1Position.E1Bay.E1DDF.CenterID) : centerIDs.Contains(t.E1Position.E1Bay.E1DDF.CenterID)) &&
                                            (DDFs.Count == 0 || DDFs.Contains(t.E1Position.E1Bay.DDFID)) &&
                                            (Bays.Count == 0 || Bays.Contains(t.E1Position.BayID)) &&
                                            (positions.Count == 0 || positions.Contains(t.PositionID)) &&
                                            (number == -1 || t.Number == number)).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static E1Number GetE1NumberByID(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Numbers.Where(t => t.ID == ID).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetE1NumberCheckableByPositionID(int positionID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Numbers.Where(t => t.PositionID == positionID)
                    .Where(t=>t.Status == (byte)DB.E1NumberStatus.Free)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Number.ToString(),
                        IsChecked = false
                    }).ToList();
            }
        }
    }
}
