using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class OfficeDB
    {
        public static List<Office> SearchOffice(            
            List<int> cityID,
            string code,
            string name,
            string contactPersonName,
            string telephonoNo,
            string address,                        
            bool? isActive,
            int startRowIndex,
            int pageSize)
        {
          
            using (MainDataContext context = new MainDataContext())
            {
                return context.Offices
                    .Where(t =>
                            (!isActive.HasValue || isActive == t.IsActive) &&
                            (cityID.Count == 0 || cityID.Contains(t.CityID)) &&
                            (string.IsNullOrWhiteSpace(code) || t.Code.Contains(code)) &&
                            (string.IsNullOrWhiteSpace(name) || t.Title.Contains(name)) &&
                            (string.IsNullOrWhiteSpace(contactPersonName) || t.ContactPersonName.Contains(contactPersonName)) &&
                            (string.IsNullOrWhiteSpace(telephonoNo) || t.TelephoneNo.Contains(telephonoNo)) &&
                            (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)))
                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchOfficeCount(
            List<int> cityID,
            string code,
            string name,
            string contactPersonName,
            string telephonoNo,
            string address,
            bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Offices
                    .Where(t =>
                            (!isActive.HasValue || isActive == t.IsActive) &&
                            (cityID.Count == 0 || cityID.Contains(t.CityID)) &&
                            (string.IsNullOrWhiteSpace(code) || t.Code.Contains(code)) &&
                            (string.IsNullOrWhiteSpace(name) || t.Title.Contains(name)) &&
                            (string.IsNullOrWhiteSpace(contactPersonName) || t.ContactPersonName.Contains(contactPersonName)) &&
                            (string.IsNullOrWhiteSpace(telephonoNo) || t.TelephoneNo.Contains(telephonoNo)) &&
                            (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address))
                          ).Count();
            }
        }

        public static Office GetOfficeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Offices
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetOfficeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Offices
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Code.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetOfficesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Offices
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.Title,
                            IsChecked = false

                        }).ToList();
            }
        }

        public static List<OfficeEmployee> SearchOfficeEmployee(
            List<int> officeID,
            string firstName,
            string lastName,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.OfficeEmployees
                    .Where(t =>
                            (officeID.Count == 0 || officeID.Contains(t.OfficeID)) &&
                            (string.IsNullOrWhiteSpace(firstName) || t.FirstName.Contains(firstName)) &&
                            (string.IsNullOrWhiteSpace(lastName) || t.LastName.Contains(lastName)))
                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchOfficeEmployeeCount(
            List<int> officeID,
            string firstName,
            string lastName)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.OfficeEmployees
                    .Where(t =>
                            (officeID.Count == 0 || officeID.Contains(t.OfficeID)) &&
                            (string.IsNullOrWhiteSpace(firstName) || t.FirstName.Contains(firstName)) &&
                            (string.IsNullOrWhiteSpace(lastName) || t.LastName.Contains(lastName))
                          )
                    .Count();
            }
        }

        public static OfficeEmployee GetOfficeEmployeeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.OfficeEmployees
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
    }
}