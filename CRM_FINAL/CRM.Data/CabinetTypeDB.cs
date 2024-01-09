using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class CabinetTypeDB
    {
        public static List<CabinetType> SearchCabinetType(
            int cabinetCapacity,
            string cabinetTypeName,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
               return context.CabinetTypes
                    .Where(t=>
                              (cabinetCapacity==-1 || t.CabinetCapacity  == cabinetCapacity) && 
                              (string.IsNullOrWhiteSpace(cabinetTypeName) || t.CabinetTypeName.Contains(cabinetTypeName))
						  ).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }
        public static int SearchCabinetTypeCount(
           int cabinetCapacity,
           string cabinetTypeName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetTypes
                    .Where(t =>
                              (cabinetCapacity == -1 || t.CabinetCapacity == cabinetCapacity) &&
                              (string.IsNullOrWhiteSpace(cabinetTypeName) || t.CabinetTypeName.Contains(cabinetTypeName))
                          ).Count();

            }
        }

        public static CabinetType GetCabinetTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }



        public static List<CheckableItem> GetCabinetTypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetTypes.Select(t => new CheckableItem
                                              {
                                                  ID = t.ID,
                                                  Name = t.CabinetTypeName,
                                                  IsChecked = false
                                              }
                                              ).OrderBy(t => t.Name).ToList();
            }

            
        }
        public static List<CabinetType> GetAllCabinetType()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetTypes.ToList();
            }


        }
    }
}