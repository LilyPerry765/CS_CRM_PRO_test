using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class E1CodeTypeDB
    {
        public static List<E1CodeType> SearchE1CodeType(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1CodeTypes
                    .Where(t =>
(string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                          )
                    //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static E1CodeType GetE1CodeTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1CodeTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetE1CodeTypeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.E1CodeTypes
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