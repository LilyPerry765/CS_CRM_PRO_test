using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class CableTypeDB
    {
        public static List<CableType> SearchCableType(string cableTypeName ,int startRowIndex,int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableTypes
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(cableTypeName) || t.CableTypeName.Contains(cableTypeName))
                          )
                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchCableTypeCount(string cableTypeName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableTypes
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(cableTypeName) || t.CableTypeName.Contains(cableTypeName))
                          ).Count();
            }
        }

        public static CableType GetCableTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCableTypeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.CableTypes
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CableTypeName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
    }
}