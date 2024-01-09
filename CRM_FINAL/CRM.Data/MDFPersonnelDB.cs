using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class MDFPersonnelDB
    {
        public static List<MDFPersonnel> SearchMDFPersonnels(string firstName, string lastName, List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFPersonnels
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (string.IsNullOrWhiteSpace(firstName) || t.FirstName.Contains(firstName)) &&
                                (string.IsNullOrWhiteSpace(lastName) || t.LastName.Contains(lastName)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)))
                    .OrderBy(t => t.CenterID)
                    .ToList();
            }
        }

        public static MDFPersonnel GetMDFPersonnelByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFPersonnels
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetMDFPersonnelsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFPersonnels.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID))
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.FirstName + " " + t.LastName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<MDFPersonnel> GetMDFPersonnelByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFPersonnels.Where(t => t.CenterID == centerID).ToList();

            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFPersonnels.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static string GetMDFPersonalFullName(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                MDFPersonnel person = context.MDFPersonnels.Where(t => t.ID == id).SingleOrDefault();

                if (person != null)
                    return person.FirstName + " " + person.LastName;
                else
                    return "";
            }
        }
    }
}
