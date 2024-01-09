using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLCustomerTypeDB
    {
        public static List<ADSLCustomerType> SearchADSLCustomerTypes(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerTypes
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)))
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        public static ADSLCustomerType GetADSLCustomerTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLCustomerTypesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerTypes
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<ADSLCustomerType> GetCustomerTypeList()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerTypes.ToList();
            }
        }
    }
}
