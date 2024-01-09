using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CRM.Data
{
    public static class Failure117NetworkContractorDB
    {
        public static List<Failure117NetworkContractor> SearchContractor(string title, string manager, string telephoneNo, string fax, string address)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117NetworkContractors
                        .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                    (string.IsNullOrWhiteSpace(manager) || t.Manager.Contains(manager)) &&
                                    (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.Contains(telephoneNo)) &&
                                    (string.IsNullOrWhiteSpace(fax) || t.Fax.Contains(fax)) &&
                                    (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)))
                        .ToList();
            }
        }

        public static Failure117NetworkContractor GetContractorById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117NetworkContractors
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static IEnumerable SearchContractorOfficer(int contractorID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117NetworkContractorOfficers
                    .Where(t => (t.ContractorID == contractorID))
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<Failure117NetworkContractorOfficerInfo> GetContractorOfficerName()
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<Failure117NetworkContractorCenter> contractorCenters = context.Failure117NetworkContractorCenters.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID)).ToList();
                List<int> contractorIDs = new List<int>();
                foreach (Failure117NetworkContractorCenter center in contractorCenters)
                {
                    contractorIDs.Add(center.ContractorID); 
                }
                return context.Failure117NetworkContractorOfficers
                    .Where(t=>contractorIDs.Contains(t.ContractorID))
                    .Select(t => new Failure117NetworkContractorOfficerInfo
                    {
                        ID = t.ID,
                        Name = t.Failure117NetworkContractor.Title + " : " + t.Name
                    }).ToList();
            }
        }

        public static List<Failure117NetworkContractorCenter> GetContractorCenterByContractorId(int contractorID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117NetworkContractorCenters
                    .Where(t => t.ContractorID == contractorID)
                    .ToList();
            }
        }
    }
}
