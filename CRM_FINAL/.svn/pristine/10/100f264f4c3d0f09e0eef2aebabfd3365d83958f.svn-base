using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class BuchtTypeDB
    {
        public static List<BuchtType> SearchBuchtType( string buchtTypeName, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtTypes
                    .Where(t =>
                             //(cites.Count == 0 || cites.Contains(t.Center.Region.CityID))&&
                             //(centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID))&&
                             (string.IsNullOrWhiteSpace(buchtTypeName) || t.BuchtTypeName.Contains(buchtTypeName))
                          )
                    .Skip(startRowIndex).Take(pageSize).ToList();
                    
            }
        }
        public static int SearchBuchtTypeCount( string buchtTypeName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtTypes
                    .Where(t =>
                             //(cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                             //(centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : centers.Contains(t.CenterID)) &&
                             (string.IsNullOrWhiteSpace(buchtTypeName) || t.BuchtTypeName.Contains(buchtTypeName))
                          ).Count();
                    
            }
        }

        public static BuchtType GetBuchtTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetBuchtTypeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtTypes
                    .OrderBy(t=>t.ID)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.BuchtTypeName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
       
        //public static int GetCity(int id)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.BuchtTypes.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
        //    }
        //}

        public static List<CheckableItem> GetBuchtTypeCheckable(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtTypes
                    .OrderBy(t=>t.BuchtTypeName)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.BuchtTypeName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetSubBuchtTypeCheckable(int parentBuchtType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtTypes
                    .Where(t => t.ParentID == parentBuchtType || t.ID == parentBuchtType)
                    .OrderBy(t => t.BuchtTypeName)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.BuchtTypeName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static IEnumerable GetSubBuchtTypeCheckableByParents(List<int> parentBuchtTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BuchtTypes
                    .Where(t => parentBuchtTypes.Contains(t.ParentID ?? 0) || parentBuchtTypes.Contains(t.ID))
                    .OrderBy(t => t.BuchtTypeName)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.BuchtTypeName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
    }
}