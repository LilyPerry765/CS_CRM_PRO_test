using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class CauseBuchtSwitchingDB
    {
        public static CauseBuchtSwitching  GetCauseBuchtSwitchingByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CauseBuchtSwitchings.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GatCheckableCauseBuchtSwitching()
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.CauseBuchtSwitchings.Select(t => new CheckableItem { ID = t.ID, Name = t.Name, IsChecked = false }).ToList();
            }
        }

        public static List<CauseBuchtSwitching> Search(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CauseBuchtSwitchings
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                          )
                    .ToList();
            }
        }
    }
}
