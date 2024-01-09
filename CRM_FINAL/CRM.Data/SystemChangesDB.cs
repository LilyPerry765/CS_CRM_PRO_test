using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using CRM.Data;

namespace CRM.Data
{
    public static class SystemChangesDB
    {
        public static List<SystemChange> SearchSystemChanges(int version, DateTime? reqeustDate, DateTime? applyDate, string reqeustNo, string description)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SystemChanges
                    .Where(t =>
(version == -1 || t.Version == version) &&
(!reqeustDate.HasValue || t.ReqeustDate == reqeustDate) &&
(!applyDate.HasValue || t.ApplyDate == applyDate) &&
(string.IsNullOrWhiteSpace(reqeustNo) || t.ReqeustNo.Contains(reqeustNo)) &&
(string.IsNullOrWhiteSpace(description) || t.Description.Contains(description))
                          )
                    .OrderByDescending(t => t.Version)
                    .ToList();
            }
        }

        public static SystemChange GetSystemChangesByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SystemChanges
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
    }
}