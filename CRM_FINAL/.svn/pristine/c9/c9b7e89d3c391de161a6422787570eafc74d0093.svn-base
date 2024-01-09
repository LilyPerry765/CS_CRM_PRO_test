using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class SettingDB
    {
        public static Setting GetSettingById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Settings
                    //.Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static Setting GetSettingByKey(string key)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Settings
                    .Where(t => t.Key == key)
                    .SingleOrDefault();
            }
        }

        public static string GetSettingValueByKey(string key)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Settings
                    .Where(t => t.Key == key)
                    .SingleOrDefault().Value;
            }
        }

        public static List<Setting> SearchSettings(string settingName, long settingValue)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //return context.Settings
                    //.Where(t => (string.IsNullOrWhiteSpace(settingName) || t.Name.Contains(settingName)) &&
                    //            (settingValue == -1 || t.Value == settingValue))
                    //.OrderBy(t => t.Name).ToList();
                    return null;
            }
        }

        public static List<CheckableItem> GetSettingsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Settings.Select(t => new CheckableItem
                {
                    //ID = t.ID,
                    //Name = t.Name,
                    IsChecked = false
                }).ToList();
            }
        }
    }
}
