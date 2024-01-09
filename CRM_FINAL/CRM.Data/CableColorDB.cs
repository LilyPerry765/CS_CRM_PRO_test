using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class CableColorDB
    {
        public static List<CableColor> SearchCableColors(string color)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableColors.Where(t => t.ID != 0 && (string.IsNullOrWhiteSpace(color) || t.Color.Contains(color))).ToList();
            }
        }

        public static CableColor GetCableColorByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableColors
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCheckableItem()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableColors.Select(t => new CheckableItem { ID = t.ID , Name = t.Color , IsChecked = false }).ToList();
            }
        }
    }
}
