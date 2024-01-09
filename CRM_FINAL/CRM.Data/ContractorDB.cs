using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ContractorDB
    {
        public static Contractor GetContractorById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contractors
                    .Where(t => t.ID == id)
                    .SingleOrDefault(); 
            }
        }

        public static List<Contractor> SearchContractors
            (
            string title,
            string headerName,
            string address,
            string telephone,
            string mobile,
            string fax,
            DateTime? contractStartDate,
            DateTime? contractEndDate
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contractors
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (string.IsNullOrWhiteSpace(headerName) || t.HeaderName.Contains(headerName)) &&
                                (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)) &&
                                (string.IsNullOrWhiteSpace(telephone) || t.Telephone.Contains(telephone)) &&
                                (string.IsNullOrWhiteSpace(mobile) || t.Mobile.Contains(mobile)) &&
                                (string.IsNullOrWhiteSpace(fax  ) || t.Fax.Contains(fax)) &&
                                (!contractStartDate.HasValue || t.ContractStartDate == contractStartDate) &&
                                (!contractEndDate.HasValue || t.ContractEndDate == contractEndDate))
                    .OrderBy(t => t.Title).ToList();
            }
        }

        public static List<CheckableItem> GetContractorsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contractors.Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.Title,
                    IsChecked = false
                }
                ).ToList();
            }
        }

        public static List<Contractor> GetUndeContractContractor()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contractors
                    .Where(t => t.ContractStartDate <= DB.GetServerDate() &&
                                t.ContractEndDate >= DB.GetServerDate())
                    .ToList();
            }
        }
    }
}
