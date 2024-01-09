using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class CityDB
    {
        private static List<CheckableItem> _cities { get; set; }

        public static List<City> SearchCity(List<int> provinceIDs, string cityName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities
                        .Where(t => (string.IsNullOrWhiteSpace(cityName) || t.Name.Contains(cityName)) &&
                                    (provinceIDs.Count == 0 || provinceIDs.Contains(t.ProvinceID)))
                        .ToList();
            }
        }

        private static Func<MainDataContext, int, IQueryable<City>> GetCityByIdQuery = CompiledQuery.Compile((MainDataContext context, int id) => (context.Cities.Where(t => t.ID == id)));

        public static City GetCityById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                City city = GetCityByIdQuery(context, id).SingleOrDefault();
                return city;
            }
        }

        public static City GetCitybyCityID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCitiesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities
                        .Select(t => new CheckableItem
                                      {
                                          ID = t.ID,
                                          Name = t.Name,
                                          IsChecked = false
                                      }
                                ).ToList();
            }
        }

        public static List<CheckableItem> GetAvailableCityCheckable()
        {
            //using (MainDataContext context = new MainDataContext())
            //{
            //    List<City> citiesList = new List<City>();

            //    foreach (int centerId in DB.CurrentUser.CenterIDs)
            //    {
            //        Center currentCenter = Data.CenterDB.GetCenterById(centerId);
            //        Region currentRegion = Data.RegionDB.GetRegionById(currentCenter.RegionID);
            //        City currentCity = DB.SearchByPropertyName<City>("ID", currentRegion.CityID).SingleOrDefault();

            //        if (citiesList.Where(t => t.ID == currentCity.ID).Count() == 0)
            //            citiesList.Add(currentCity);
            //    }

            //    return citiesList.Distinct().ToList();
            //}
            if (_cities == null)
            {
                using (MainDataContext context = new MainDataContext())
                {
                    _cities = context.Cities.Join(context.Regions, c => c.ID, r => r.CityID, (c, r) => new { Cities = c, Regions = r })
                                   .Join(context.Centers, cr => cr.Regions.ID, ce => ce.RegionID, (cr, ce) => new { Centers = ce, Cities = cr.Cities })
                                   .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Centers.ID))
                                   .Select(t => new CheckableItem
                                   {
                                       Name = t.Cities.Name,
                                       ID = t.Cities.ID,
                                       IsChecked = false
                                   }).Distinct().ToList();


                }
            }

            return _cities;
        }

        public static List<int> GetAvailableCity()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities.Join(context.Regions, c => c.ID, r => r.CityID, (c, r) => new { Cities = c, Regions = r })
                               .Join(context.Centers, cr => cr.Regions.ID, ce => ce.RegionID, (cr, ce) => new { Centers = ce, Cities = cr.Cities })
                               .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Centers.ID))
                               .Select(t => t.Cities.ID)
                               .Distinct().ToList();
            }
        }

        public static List<CheckableItem> GetCitiesCheckableByProvince(int province)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities.Where(t => t.ProvinceID == province)
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.Name,
                            IsChecked = false

                        }
                                ).ToList();
            }
        }

        public static City GetCityByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities
                    .Join(context.Regions, ci => ci.ID, re => re.CityID, (ci, re) => new { cities = ci, region = re })
                    .Join(context.Centers, re => re.region.ID, ce => ce.RegionID, (re, ce) => new { center = ce, citie = re.cities })
                    .Where(t => t.center.ID == centerID)
                    .Select(t => t.citie)
                    .SingleOrDefault();
            }
        }

        public static List<City> GetAllCity()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities
                        .ToList();
            }
        }

        public static string GetCityNameByID(int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities
                    .Where(t => cityID == t.ID).SingleOrDefault().Name;

            }
        }

        public static List<string> GetCityNameByIDs(List<int> CityIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cities
                    .Where(t => CityIDs.Count == 0 || CityIDs.Contains(t.ID))
                    .Select(t => t.Name)
                    .ToList();
            }
        }
    }
}
