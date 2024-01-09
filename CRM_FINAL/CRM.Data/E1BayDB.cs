using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class E1BayDB
    {

        public static int SearchE1BayCount(List<int> cities,List<int> centerIDs, List<int> DDFs, int number)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Bays.Where(t =>
                                            (cities.Count == 0 || cities.Contains(t.E1DDF.Center.Region.CityID)) &&
                                            (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.E1DDF.CenterID) : centerIDs.Contains(t.E1DDF.CenterID)) &&
                                            (DDFs.Count == 0 || DDFs.Contains(t.DDFID)) &&
                                            (number == -1 || t.Number == number)).Count();
            }
        }

        public static List<E1Bay> SearchE1Bay(List<int> cities, List<int> centerIDs, List<int> DDFs, int number, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Bays.Where(t =>
                                            (cities.Count == 0 || cities.Contains(t.E1DDF.Center.Region.CityID)) &&
                                            (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.E1DDF.CenterID) : centerIDs.Contains(t.E1DDF.CenterID)) &&
                                            (DDFs.Count == 0 || DDFs.Contains(t.DDFID)) &&
                                            (number == -1 || t.Number == number)).ToList();
            }
        }

        public static E1Bay GetE1BayByID(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Bays.Where(t => t.ID == ID).SingleOrDefault();
            }
        }

        public static int GetCity(int e1BayID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Bays.Where(t => t.ID == e1BayID).SingleOrDefault().E1DDF.Center.Region.CityID;
            }
        }

        public static List<CheckableItem> GetBayCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Bays.Where(t => DB.CurrentUser.CenterIDs.Contains(t.E1DDF.CenterID)).Select( t=> new CheckableItem{ ID = t.ID , Name = t.Number.ToString() , IsChecked = false}).ToList();
            }
        }

        public static List<CheckableItem> GetBayCheckableByDDFID(int DDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Bays.Where(t => t.DDFID == DDFID).Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetBayCheckableByDDFIDs(List<int> DDFIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Bays.Where(t => DDFIDs.Contains(t.E1DDF.ID)).Select(t => new CheckableItem { ID = t.ID , Name = t.Number.ToString() , IsChecked = false }).ToList();
            }
        }
    }
}
