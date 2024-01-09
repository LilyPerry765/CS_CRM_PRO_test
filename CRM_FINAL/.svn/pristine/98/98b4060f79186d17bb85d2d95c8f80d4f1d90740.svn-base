using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class QuotaJobTitleDB
    {
        public static List<QuotaJobTitle> SearchQuotaJobTitle(string jobTitle)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.QuotaJobTitles
                    .Where(t =>
                          (string.IsNullOrWhiteSpace(jobTitle) || t.Title.Contains(jobTitle))
                          )
                    //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static QuotaJobTitle GetQuotaJobTitleByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.QuotaJobTitles
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetQuotaJobTitleCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.QuotaJobTitles
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
    }
}