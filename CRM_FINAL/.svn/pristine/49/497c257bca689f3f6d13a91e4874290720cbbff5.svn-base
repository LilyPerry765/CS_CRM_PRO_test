using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class E1LinkTypeDB
    {
        public static List<E1LinkType> SearchE1LinkType(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1LinkTypes
                    .Where(t =>
                          (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                          )
                    //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static E1LinkType GetE1LinkTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1LinkTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetE1LinkTypeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.E1LinkTypes
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
    }
}