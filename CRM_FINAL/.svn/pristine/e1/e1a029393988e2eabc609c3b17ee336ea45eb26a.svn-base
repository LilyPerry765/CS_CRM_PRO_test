using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data 
{
    public static class AddressDB
    {
        public static List<Address> SearchAddresses(List<int> centerIDs, string postalCode, string address, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Addresses
                    .Where(t => ((centerIDs.Count == 0) || (centerIDs.Contains(t.CenterID))) &&
                                (string.IsNullOrWhiteSpace(postalCode) || t.PostalCode.Contains(postalCode)) &&
                                (string.IsNullOrWhiteSpace(address) || t.AddressContent.Contains(address)) &&
                                (t.Center.IsActive == true))
                    .OrderBy(t => t.PostalCode)
                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }
        
        public static int SearchAddressCount(List<int> centerIDs, string postalCode, string address)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Addresses
                    .Where(t => ((centerIDs.Count == 0) || (centerIDs.Contains(t.CenterID))) &&
                                (string.IsNullOrWhiteSpace(postalCode) || t.PostalCode.Contains(postalCode)) &&
                                (string.IsNullOrWhiteSpace(address) || t.AddressContent.Contains(address)))
                    .OrderBy(t => t.PostalCode)
                    .Count();
            }
        }

        public static Address GetAddressByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Addresses.Where(t => (t.ID == id)).SingleOrDefault();
            }
        }

        public static int GetCity(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Addresses.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static List<Address> GetAddressByPostalCode(string postCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Addresses.Where(t => t.PostalCode == postCode).OrderBy(t=>t.ID).Take(1).ToList();
            }
        }

        public static bool ExistAddressPostalCode(string postCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Addresses.Any(t => t.PostalCode == postCode);
            }
        }
        public static int GetAddressByPostalCodeCount(string postCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Addresses.Where(t => t.PostalCode == postCode).Count();
            }
        }

       
    }
}
