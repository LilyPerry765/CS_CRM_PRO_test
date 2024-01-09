using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class PostTypeDB
    {
        public static List<PostType> SearchPostType(string postTypeName , int startRowIndex , int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostTypes
                    .Where(t =>(string.IsNullOrWhiteSpace(postTypeName) || t.PostTypeName.Contains(postTypeName))
                          )
                    .OrderBy(t => t.ID ).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchPostTypeCount(string postTypeName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostTypes
                    .Where(t => (string.IsNullOrWhiteSpace(postTypeName) || t.PostTypeName.Contains(postTypeName))
                          ).Count();
            }
        }

        public static PostType GetPostTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostTypes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetPostTypeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PostTypes
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.PostTypeName,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
    }
}