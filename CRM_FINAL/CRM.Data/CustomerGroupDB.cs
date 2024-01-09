using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class CustomerGroupDB
    {
        public static List<CustomerGroup> SearchCustomerGroup(List<int> CustomerTypes, string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerGroups
                        .Where(t => (CustomerTypes.Count == 0 || CustomerTypes.Contains(t.CustomerTypeID) &&
                              (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title))))
                              .OrderBy(t => t.Title)
                        .ToList();
            }
        }

        public static CustomerGroup GetCustomerGroupById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerGroups
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCustomerGroupsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerGroups
                        .OrderBy(t => t.Title)
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.Title,
                            IsChecked = false

                        }).ToList();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetCustomerGroupsCheckableByCustomerTypeID(int customerTypeID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.CustomerGroups
        //                .Where(t=>t.CustomerTypeID == customerTypeID)
        //                .OrderBy(t => t.Title)
        //                .Select(t => new CheckableItem
        //                {
        //                    ID = t.ID,
        //                    Name = t.Title,
        //                    IsChecked = false

        //                }).ToList();
        //    }
        //}

        //TODO:rad 13950225
        public static List<CheckableItem> GetCustomerGroupsCheckableByCustomerTypeID(int customerTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.CustomerGroups
                                   .Where(t => t.CustomerTypeID == customerTypeID)
                                   .OrderBy(t => t.Title)
                                   .Select(t => new CheckableItem
                                                {
                                                    ID = t.ID,
                                                    Name = t.Title,
                                                    IsChecked = false

                                                }
                                           )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        public static List<CheckableItem> GetCustomerGroupsCheckableByCustomerTypeIDs(List<int> customerTypeIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CustomerGroups
                        .Where(t => (customerTypeIDs.Contains(t.CustomerTypeID)))
                        .OrderBy(t => t.Title)
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.Title,
                            IsChecked = false

                        }).ToList();
            }
        }
    }
}
