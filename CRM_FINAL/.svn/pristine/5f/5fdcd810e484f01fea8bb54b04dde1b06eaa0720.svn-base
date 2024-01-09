using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class SMSServiceDB
    {
        public static List<SMSService> SearchSMSServices(string title, bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SMSServices
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) && (!isActive.HasValue || isActive == t.IsActive))
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        public static SMSService GetSMSServiceByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SMSServices
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetSMSServicesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SMSServices
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

        public static string GetSMSMessage(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SMSServices.Where(t => t.Title == title).SingleOrDefault().Message;
            }
        }

        public static SMSService GetSMSService(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SMSServices.Where(t => t.Title == title).SingleOrDefault();
            }
        }
    }
}
