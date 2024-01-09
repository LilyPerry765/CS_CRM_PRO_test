using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class PostGroupDB
    {
        public static List<PostGroupInfo> SearchPostGroup
            (
            int groupNo
             , int startRowIndex,
              int pageSize
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups
                    .Where(t =>
                            (groupNo == -1 || t.GroupNo == groupNo)
                          )
                          .Select(t => new PostGroupInfo { ID = t.ID, CenterID = t.CenterID, CenterName = t.Center.CenterName, GroupNo = t.GroupNo, Description = t.Description })
                          .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }
        public static int SearchPostGroupCount
            (
            int groupNo
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups
                    .Where(t =>
                            (groupNo == -1 || t.GroupNo == groupNo)
                          )
                    .Count();
            }
        }
        public static PostGroup GetPostGroupByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }


        public static List<CheckableItem> GetPostGroupCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID)).AsEnumerable().Select(t => new CheckableItem { ID = t.ID, Name = t.GroupNo.ToString() + DB.GetDescription(t.Description), IsChecked = false }).ToList();
            }
        }
        public static List<CheckableItem> GetPostGroupCheckableByCenter(int center)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups.Where(t => center == t.CenterID).OrderBy(t => t.GroupNo).AsEnumerable().Select(t => new CheckableItem { ID = t.ID, Name = t.GroupNo.ToString() + DB.GetDescription(t.Description), IsChecked = false }).ToList();
            }
        }
        public static List<CheckableItem> GetPostGroupCheckableByCenterIDs(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID))).OrderBy(t => t.GroupNo).Select(t => new CheckableItem { ID = t.ID, Name = t.GroupNo.ToString() + ":" + t.Center.CenterName, IsChecked = false }).ToList();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetPostGroupAndCountPostsOfPostGroupCheckableByCenter(int center)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PostGroups.Where(t => center == t.CenterID)
        //                       .OrderBy(t => t.GroupNo)
        //                       .Select(t => new CheckableItem 
        //                                       { 
        //                                            ID = t.ID,
        //                                            Name = t.GroupNo.ToString(),
        //                                            Description = context.Posts.Count(p=>p.PostGroupID == t.ID).ToString(),
        //                                            IsChecked = false
        //                                       }).ToList();
        //    }
        //}

        //TODO:rad 13950222
        public static List<CheckableItem> GetPostGroupAndCountPostsOfPostGroupCheckableByCenter(int center)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.PostGroups.Where(t => center == t.CenterID)
                                              .OrderBy(t => t.GroupNo)
                                              .Select(t => new CheckableItem
                                                        {
                                                            ID = t.ID,
                                                            Name = t.GroupNo.ToString(),
                                                            Description = context.Posts.Count(p => p.PostGroupID == t.ID).ToString(),
                                                            IsChecked = false
                                                        }
                                                      )
                                              .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static PostGroup GetPostGroupByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostGroups.Where(t => t.ID == context.Posts.Where(t2 => t2.ID == postID).SingleOrDefault().PostGroupID).SingleOrDefault();
            }
        }
    }
}