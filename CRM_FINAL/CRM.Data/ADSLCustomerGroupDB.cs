using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLCustomerGroupDB
    {
        public static List<ADSLCustomerGroup> GetCustomerGroupList()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerGroups.ToList();
            }
        }

        public static ADSLCustomerGroup GetADSLCustomerGroupById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerGroups
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<ADSLCustomerGroup> SearchADSLCustomerGroup(string title, string ISPName, string keyWord)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerGroups
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (string.IsNullOrWhiteSpace(ISPName) || t.ISPName.Contains(ISPName)) &&
                                (string.IsNullOrWhiteSpace(keyWord) || t.KeyWord.Contains(keyWord)))
                    .OrderBy(t => t.Title).ToList();
            }
        }

        public static List<CheckableItem> GetADSLCustomerGroupCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerGroups.Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.Title,
                    IsChecked = false
                }
                ).ToList();
            }
        }

        public static List<ADSLCustomerGroup> GetCustomerGroupbyServiceGroupList(List<int> serviceGroupListIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLServiceGroup> serviceGroupList = context.ADSLServiceGroups.Where(t => serviceGroupListIDs.Contains(t.ID)).ToList();
                List<ADSLCustomerGroup> custometGroupList = new List<ADSLCustomerGroup>();

                foreach (ADSLServiceGroup currentItem in serviceGroupList)
                {
                    if (currentItem.CustomerGroupID != null)
                    {
                        ADSLCustomerGroup customerGroup = context.ADSLCustomerGroups.Where(t => t.ID == (int)currentItem.CustomerGroupID).SingleOrDefault();
                        
                        if (!custometGroupList.Select(t => t.ID).Contains(customerGroup.ID))
                            custometGroupList.Add(customerGroup);
                    }
                }

                return custometGroupList;
            }
        }

        public static List<CheckableItem> GetCustomerGroupsCheckableByADSlServiceGroupIds(List<int> ADSlServiceGroupIds)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLCustomerGroups.Join(context.ADSLServiceGroups, c => c.ID, s => s.CustomerGroupID, (c, s) => new { ADSLCustomerGroup = c, ADSLServiceGroup = s })
                    .Where(t => ADSlServiceGroupIds.Count == 0 || ADSlServiceGroupIds.Contains((int)t.ADSLServiceGroup.ID))
                    .Select(t => new CheckableItem
                    {
                        ID=t.ADSLCustomerGroup.ID,
                        Name=t.ADSLCustomerGroup.Title,
                        IsChecked=false
                    }).ToList();
            }
        }
    }
}
