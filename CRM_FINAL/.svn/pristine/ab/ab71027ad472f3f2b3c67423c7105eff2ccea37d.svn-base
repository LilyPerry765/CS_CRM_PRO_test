using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class CauseOfChangeNoDB
    {
        public static List<CheckableItem> GetCauseOfChangeNoCheckableItem()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CauseOfChangeNos.Select(t => new CheckableItem { ID = t.ID, Name = t.Name, IsChecked = false }).OrderByDescending(ch => ch.Name).ToList();
            }
        }

        public static List<CauseOfChangeNo> Search(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CauseOfChangeNos
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name))
                          )
                    .ToList();
            }
        }

        public static CauseOfChangeNo GetCauseOfChangeNoID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CauseOfChangeNos
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

    }
}
