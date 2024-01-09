using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class CustomerTypeDB
    {
        public static List<CustomerType> SearchCustomerTypes(string title, bool? income)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerTypes
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (!income.HasValue || t.Income==income))
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        public static CustomerType GetCustomerTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCustomerTypesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerTypes
                     .OrderBy(t => t.Title)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetIsShowCustomerTypesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerTypes.Where(t=>t.IsShow == true)
                    .OrderBy(t => t.Title)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static CustomerType GetCustomerTypeByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                CustomerType result = new CustomerType();
                result = context.Telephones
                                .Where(te => te.TelephoneNo == telephoneNo)
                                .Select(te => te.CustomerType)
                                .SingleOrDefault();
                return result;
            }
        }
    }
}
