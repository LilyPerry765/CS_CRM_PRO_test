using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class CableUsedChannelDB
    {
        public static List<CableUsedChannel> SearchCableUsedChannel(string cableUsedChannelName, int startRowIndex,int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableUsedChannels
                    .Where(t =>
                          (string.IsNullOrWhiteSpace(cableUsedChannelName) || t.CableUsedChannelName.Contains(cableUsedChannelName))
                          )
                    //.OrderBy(t => t.Name)
                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchCableUsedChannelCount(string cableUsedChannelName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableUsedChannels
                    .Where(t =>
                          (string.IsNullOrWhiteSpace(cableUsedChannelName) || t.CableUsedChannelName.Contains(cableUsedChannelName))
                          )
                    //.OrderBy(t => t.Name)
                    .Count();
            }
        }

        public static CableUsedChannel GetCableUsedChannelByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CableUsedChannels
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCableUsedChannelCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.CableUsedChannels
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CableUsedChannelName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
    }
}