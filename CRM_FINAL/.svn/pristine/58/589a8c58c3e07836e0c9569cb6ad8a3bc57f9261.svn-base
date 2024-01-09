using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class RoleDB
    {
        public static List<RoleInfo> SearchRoles(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Where(t => (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name)) && t.IsVisible == true)
                    .OrderBy(t => t.Name)
                    .Select(t => new RoleInfo
                    {
                        ID = t.ID,
                        Name = t.Name,
                        Description = t.Description,
                        IsServiceRole = t.IsServiceRole,
                        UesrCount = t.Users.Count
                    })
                    .ToList();
            }
        }

        public static Role GetRoleByID1(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static string GetRoleNameByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Where(t => t.ID == id).SingleOrDefault().Name;
            }
        }

        public static RoleInfo GetRoleByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Where(t => t.ID == id)
                    .Select(t => new RoleInfo
                    {
                        ID = t.ID,
                        Name = t.Name,
                        Description = t.Description,
                        IsServiceRole = t.IsServiceRole,
                        UesrCount = t.Users.Count
                    })
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetRolesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Name,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<ResourceInfo> GetResourceInfosByRoleID(int roleId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ResourceInfo> result;

                var query = context.RoleResources
                                   .Where(rrs => rrs.RoleID == roleId)
                                   .Select(filteredData => new ResourceInfo
                                                           {
                                                               ID = filteredData.Resource.ID,
                                                               Name = filteredData.Resource.Name,
                                                               Description = filteredData.Resource.Description
                                                           }
                                          );
                result = query.ToList();
                return result;
            }
        }
    }
}
