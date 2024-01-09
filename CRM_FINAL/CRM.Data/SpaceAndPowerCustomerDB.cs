using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace CRM.Data
{
    public static class SpaceAndPowerCustomerDB
    {
        public static IEnumerable SearchCustomer(
            string title,
            string address,
            string email,
            string telephoneNo,
            string mobile,
            string fax,
            DateTime? fromInsertDate,
            DateTime? toInsertDate,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpaceAndPowerCustomers
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)) &&
                                (string.IsNullOrWhiteSpace(email) || t.Email.Contains(email)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (string.IsNullOrWhiteSpace(mobile) || t.Mobile.ToString().Contains(mobile)) &&
                                (string.IsNullOrWhiteSpace(fax) || t.Fax.ToString().Contains(fax))&&
                                (!fromInsertDate.HasValue || t.InsertDate >= fromInsertDate) &&
                                (!toInsertDate.HasValue || t.InsertDate <= toInsertDate))
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        public static int SearchCustomerCount(
            string title,
            string address,
            string email,
            string telephoneNo,
            string mobile,
            string fax,
            DateTime? fromInsertDate,
            DateTime? toInsertDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpaceAndPowerCustomers
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)) &&
                                (string.IsNullOrWhiteSpace(email) || t.Email.Contains(email)) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (string.IsNullOrWhiteSpace(mobile) || t.Mobile.ToString().Contains(mobile)) &&
                                (string.IsNullOrWhiteSpace(fax) || t.Fax.ToString().Contains(fax)) &&
                                (!fromInsertDate.HasValue || t.InsertDate >= fromInsertDate) &&
                                (!toInsertDate.HasValue || t.InsertDate <= toInsertDate))
                    .OrderBy(t => t.Title)
                    .Count();
            }
        }

        public static SpaceAndPowerCustomer GetCustomerByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpaceAndPowerCustomers
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
        //public static List<CheckableItem> GetSpacePowerCustomerCheckable()
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.SpaceAndPowerCustomers
        //            .Select(t => new CheckableItem
        //            {
        //                ID = t.ID,
        //                Name = t.Title,
        //                IsChecked = false
        //            }
        //                )
        //            .ToList();
        //    }
        //}
    }
}
