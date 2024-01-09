using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class ProvinceDB
    {
        public static List<Province> SearchProvinces(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Provinces
                    .Where(t => (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name)))
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static Province GetProvinceByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Provinces
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetProvincesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Provinces
                    .Select(t => new CheckableItem
                            {
                                ID = t.ID,
                                Name = t.Name,
                                IsChecked = false
                            }
                        )
                    .ToList();
            }
        }

        public static Province GetProvinceByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
             return   context.Provinces.Join(context.Cities, p => p.ID, c => c.ProvinceID, (p, c) => new {province = p ,  city = c })
                                  .Join(context.Regions, c => c.city.ID, reg => reg.CityID, (c, reg) => new {province = c.province , regions = reg })
                                  .Join(context.Centers, reg => reg.regions.ID, cen => cen.RegionID, (r, cen) => new {province = r.province, center = cen })
                                  .Where(t => t.center.ID == centerID).Select(t=>t.province).SingleOrDefault();
              
            }
        }
    }
}