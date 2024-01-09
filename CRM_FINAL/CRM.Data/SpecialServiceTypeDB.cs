using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class SpecialServiceTypeDB
    {
        public static List<SpecialServiceType> SearchSpecialServiceType(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialServiceTypes
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title))
                          )
                    //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static SpecialServiceType GetSpecialServiceTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialServiceTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetSpecialServiceTypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialServiceTypes
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked =    false
                    }
                        )
                    .ToList();

            }
        }
        public static List<CheckableItem> GetSpecialServiceTypeCheckableForTelephone(long Telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialServiceTypes
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = context.TelephoneSpecialServiceTypes.Any(ts=>ts.TelephoneNo == Telephone && ts.SpecialServiceTypeID == t.ID)
                    }
                        )
                    .ToList();

            }
        }
        public static List<SpecialServiceType> GetSpecialServiceType()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialServiceTypes.ToList();
                    
            }
        }

        public static List<SpecialServiceType> GetSpecialService(List<int> specialServiceIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialServiceTypes.Where(t => specialServiceIDs.Contains(t.ID)).ToList();
            }
        }
    }
}