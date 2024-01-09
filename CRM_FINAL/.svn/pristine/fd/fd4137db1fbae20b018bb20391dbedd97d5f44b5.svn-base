using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class RegionDB
    {
        public static List<Region> GetRegions()
        {
            using (MainDataContext context = new MainDataContext())
            {   
                //return context.Regions.Select(t=> new Region {Centers = t.Centers,City = t.City , CityID = t.CityID,ID = t.ID, Title = t.Title })
                return context.Regions.ToList();
                // .ToList();
            }
        }
        public static List<Region> SearchRegions(List<int> cityIDs, string Title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Regions
                        .Where(t => (string.IsNullOrWhiteSpace(Title) || t.Title.Contains(Title)) &&
                                    (cityIDs.Count == 0 || cityIDs.Contains((int)t.CityID)))
                        .ToList();
            }
        }

        public static Region GetRegionById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Regions
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetRegionsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Regions
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.Title,
                            IsChecked = false
                        }).ToList();
            }
        }
        
        public static List<CheckableItem> GetRegionsCheckableByProvince(int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Regions.Where(t => t.CityID == cityID)
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.Title,
                            IsChecked = false
                        }).ToList();
            }
        }

        public static List<CheckableItem> GetRegionCheckableByCityID(int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Regions.Where(t => t.CityID == cityID).Select(t => new CheckableItem { ID = t.ID, Name = t.Title, IsChecked = false }).ToList();
            }
        }
    }
}
