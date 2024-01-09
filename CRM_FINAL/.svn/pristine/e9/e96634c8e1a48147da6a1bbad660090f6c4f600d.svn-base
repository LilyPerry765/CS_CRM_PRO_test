using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class Failue117CableTypeDB
    {
        public static List<Failure117CableType> SearchCableTypes(string type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117CableTypes.Where(t => t.ID != 0 && (string.IsNullOrWhiteSpace(type) || t.Type.Contains(type))).ToList();
            }
        }

        public static Failure117CableType GetCableTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117CableTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
    }
}
