using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Linq.Dynamic;

namespace CRM.Data
{
    public static class PostDB
    {
        //milad doran
        //public static List<PostInfo> SearchPost
        //    (

        //  List<int> City,
        //  List<int> Center,
        //  List<int> Cabinet,
        //  List<int> Post,
        //  List<int> PostType,
        //  List<int> Status,
        //  int Capacity,
        //  int Distance,
        //  int OutBorderMeter,
        //  int PostalCode,
        //  List<int> AorBType,
        //  string Address,
        //    bool? isOutBoundMeter,
        //    int? activeCondition,
        //    int activeConditionString,
        //    int? freeCondition,
        //    int freeConditionString,
        //    int startRowIndex,
        //    int pageSize,
        //    out int count
        //    )
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        IQueryable<PostInfo> postInfoQuery = context.Posts
        //            .Where(t =>
        //                    (City.Count == 0 || City.Contains(t.Cabinet.Center.Region.CityID)) &&
        //                    (Center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : Center.Contains(t.Cabinet.CenterID)) &&
        //                    (Cabinet.Count == 0 || Cabinet.Contains(t.CabinetID)) &&
        //                    (Post.Count == 0 || Post.Contains(t.ID)) &&
        //                    (PostType.Count == 0 || PostType.Contains(t.PostTypeID)) &&
        //                    (Status.Count == 0 || Status.Contains(t.Status)) &&
        //                    (Capacity == -1 || t.Capacity == Capacity) &&
        //                    (Distance == -1 || t.Distance == Distance) &&
        //                    (OutBorderMeter == -1 || t.OutBorderMeter == OutBorderMeter) &&
        //                    (AorBType.Count == 0 || AorBType.Contains((int)t.AorBType)) &&
        //                     (t.IsDelete == false) &&
        //                      (isOutBoundMeter == null || t.IsOutBorder == isOutBoundMeter) &&
        //                    (string.IsNullOrWhiteSpace(Address) || t.Address.Contains(Address))
        //                  )
        //                  .OrderBy(t => t.Cabinet.CabinetNumber)
        //                  .ThenBy(t => t.Number)
        //                  .ThenBy(t => t.AORBPostAndCabinet.Name)
        //                  .Select
        //                  (t => new PostInfo
        //                   {
        //                       ID = t.ID,
        //                       Center = t.Cabinet.Center.CenterName,
        //                       CabinetID = t.Cabinet.ID.ToString(),
        //                       CabinetNumber = t.Cabinet.CabinetNumber,
        //                       PostTypeName = t.PostType.PostTypeName.ToString(),
        //                       ABType = t.AORBPostAndCabinet.Name,
        //                       PostTypeID = t.PostTypeID,
        //                       Number = t.Number,
        //                       Distance = t.Distance.ToString(),
        //                       OutBorderMeter = t.OutBorderMeter.ToString(),
        //                       PostalCode = t.PostalCode,
        //                       PostGroupNo = t.PostGroup.GroupNo.ToString(),
        //                       Address = t.Address,
        //                       Status = t.PostStatus.Name,
        //                       Capacity = t.Capacity.ToString(),
        //                       FromPostContact = t.FromPostContact.ToString(),
        //                       ToPostContact = t.ToPostContact.ToString(),
        //                       DocumentFileID = t.DocumentFileID,
        //                       ActivePostContactCount = t.PostContacts.Where(t2 => (t2.Status == (int)DB.PostContactStatus.CableConnection && t2.ConnectionType == (int)DB.PostContactConnectionType.Noraml) || (t2.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)).Count(),
        //                       FreePostContactCount = t.PostContacts.Where(t2 => t2.Status == (int)DB.PostContactStatus.Free && t2.ConnectionType == (int)DB.PostContactConnectionType.Noraml).Count(),
        //                       AdjacentPost = string.Join(",", context.AdjacentPosts.Where(t2 => t2.PostID == t.ID).Select(t2 => t2.Post1.Number).ToList()),


        //                   }
        //                   );

        //        if (activeCondition != null && activeConditionString != 0)
        //        {
        //            postInfoQuery = postInfoQuery.Where(DB.ComparisonByByPropertyName<PostInfo>("ActivePostContactCount", activeConditionString, (int)activeCondition)).AsQueryable();
        //        }
        //        if (freeCondition != null && freeConditionString != 0)
        //        {
        //            postInfoQuery = postInfoQuery.Where(DB.ComparisonByByPropertyName<PostInfo>("FreePostContactCount", freeConditionString, (int)freeCondition)).AsQueryable();
        //        }


        //        count = postInfoQuery.Count();
        //        return postInfoQuery.Skip(startRowIndex).Take(pageSize).ToList();
        //    }
        //}


        //TODO:rad 13950227
        public static List<PostInfo> SearchPost(
                                                List<int> cities, List<int> centers, List<int> cabinets, List<int> posts, List<int> postTypes,
                                                List<int> statues, int capacity, int distance, int outBorderMeter, int postalCode,
                                                List<int> aOrBTypes, string address, bool? isOutBoundMeter, int? activeCondition, int activeConditionString,
                                                int? freeCondition, int freeConditionString, int startRowIndex, int pageSize, out int count
                                                )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PostInfo> result = new List<PostInfo>();

                IQueryable<PostInfo> postInfoQuery = context.Posts
                                                            .Where(t =>
                                                                        (cities.Count == 0 || cities.Contains(t.Cabinet.Center.Region.CityID)) &&
                                                                        (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : centers.Contains(t.Cabinet.CenterID)) &&
                                                                        (cabinets.Count == 0 || cabinets.Contains(t.CabinetID)) &&
                                                                        (posts.Count == 0 || posts.Contains(t.ID)) &&
                                                                        (postTypes.Count == 0 || postTypes.Contains(t.PostTypeID)) &&
                                                                        (statues.Count == 0 || statues.Contains(t.Status)) &&
                                                                        (capacity == -1 || t.Capacity == capacity) &&
                                                                        (distance == -1 || t.Distance == distance) &&
                                                                        (outBorderMeter == -1 || t.OutBorderMeter == outBorderMeter) &&
                                                                        (aOrBTypes.Count == 0 || aOrBTypes.Contains((int)t.AorBType)) &&
                                                                        (t.IsDelete == false) &&
                                                                        (isOutBoundMeter == null || t.IsOutBorder == isOutBoundMeter) &&
                                                                        (string.IsNullOrEmpty(address) || t.Address.Contains(address))
                                                                   )
                                                            .OrderBy(t => t.Cabinet.CabinetNumber)
                                                            .ThenBy(t => t.Number)
                                                            .ThenBy(t => t.AORBPostAndCabinet.Name)
                                                            .Select(t => new PostInfo
                                                                        {
                                                                            ID = t.ID,
                                                                            Center = t.Cabinet.Center.CenterName,
                                                                            CabinetID = t.Cabinet.ID.ToString(),
                                                                            CabinetNumber = t.Cabinet.CabinetNumber,
                                                                            PostTypeName = t.PostType.PostTypeName.ToString(),
                                                                            ABType = t.AORBPostAndCabinet.Name,
                                                                            PostTypeID = t.PostTypeID,
                                                                            Number = t.Number,
                                                                            Distance = t.Distance.ToString(),
                                                                            OutBorderMeter = t.OutBorderMeter.ToString(),
                                                                            PostalCode = t.PostalCode,
                                                                            PostGroupNo = t.PostGroup.GroupNo.ToString(),
                                                                            Address = t.Address,
                                                                            Status = t.PostStatus.Name,
                                                                            Capacity = t.Capacity.ToString(),
                                                                            FromPostContact = t.FromPostContact.ToString(),
                                                                            ToPostContact = t.ToPostContact.ToString(),
                                                                            DocumentFileID = t.DocumentFileID,

                                                                            //****************************************************************************************************************************************************************
                                                                            ActivePostContactCount = t.PostContacts
                                                                                                      .Where(t2 =>
                                                                                                                  (t2.Status == (int)DB.PostContactStatus.CableConnection && t2.ConnectionType == (int)DB.PostContactConnectionType.Noraml) ||
                                                                                                                  (t2.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote && t2.Status != (int)DB.PostContactStatus.Deleted)
                                                                                                             )
                                                                                                      .Count(),
                                                                            //****************************************************************************************************************************************************************
                                                                            FreePostContactCount = t.PostContacts
                                                                                                    .Where(t2 =>
                                                                                                                  t2.Status == (int)DB.PostContactStatus.Free &&
                                                                                                                  t2.ConnectionType == (int)DB.PostContactConnectionType.Noraml
                                                                                                           )
                                                                                                    .Count(),
                                                                            //****************************************************************************************************************************************************************
                                                                            AdjacentPost = string.Join(",",
                                                                                                       context.AdjacentPosts
                                                                                                              .Where(t2 => t2.PostID == t.ID)
                                                                                                              .Select(t2 => t2.Post1.Number)
                                                                                                              .ToList()
                                                                                                      )
                                                                            //****************************************************************************************************************************************************************
                                                                        }
                                                                   );

                if (activeCondition != null && activeConditionString != 0)
                {
                    postInfoQuery = postInfoQuery.Where(DB.ComparisonByByPropertyName<PostInfo>("ActivePostContactCount", activeConditionString, (int)activeCondition)).AsQueryable();
                }
                if (freeCondition != null && freeConditionString != 0)
                {
                    postInfoQuery = postInfoQuery.Where(DB.ComparisonByByPropertyName<PostInfo>("FreePostContactCount", freeConditionString, (int)freeCondition)).AsQueryable();
                }

                count = postInfoQuery.Count();

                result = postInfoQuery.Skip(startRowIndex)
                                      .Take(pageSize)
                                      .ToList();

                return result;
            }
        }

        public static int SearchPostCount
         (

       List<int> City,
       List<int> Center,
       List<int> Cabinet,
       List<int> Post,
       List<int> PostType,
       List<int> PostGroup,
       List<int> Status,
       int Capacity,
       int Distance,
       int OutBorderMeter,
       int PostalCode,
       List<int> AorBType,
       string Address,
       bool? isOutBoundMeter
         )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts
                    .Where(t =>
                            (City.Count == 0 || City.Contains(t.Cabinet.Center.Region.CityID)) &&
                            (Center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : Center.Contains(t.Cabinet.CenterID)) &&
                            (Cabinet.Count == 0 || Cabinet.Contains(t.CabinetID)) &&
                            (Post.Count == 0 || Post.Contains(t.ID)) &&
                            (PostType.Count == 0 || PostType.Contains(t.PostTypeID)) &&
                            (PostGroup.Count == 0 || PostGroup.Contains((int)t.PostGroupID)) &&
                            (Status.Count == 0 || Status.Contains(t.Status)) &&
                            (Capacity == -1 || t.Capacity == Capacity) &&
                            (Distance == -1 || t.Distance == Distance) &&
                            (OutBorderMeter == -1 || t.OutBorderMeter == OutBorderMeter) &&
                            (AorBType.Count == 0 || AorBType.Contains((int)t.AorBType)) &&
                            (isOutBoundMeter == null || t.IsOutBorder == isOutBoundMeter) &&
                            (t.IsDelete == false) &&
                            (string.IsNullOrWhiteSpace(Address) || t.Address.Contains(Address))
                          ).Count();
            }
        }

        public static Post GetPostByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) && (t.IsDelete == false))
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }



        public static List<CheckableItem> GetPostCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) && (t.IsDelete == false)).Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false, Description = " (" + t.AORBPostAndCabinet.Name + ")" }).ToList();
            }
        }


        public static List<CheckableItem> GetPostCheckableByCabinetID(List<int> Ids)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) && (t.IsDelete == false))
                    .Where(p => Ids.Contains(p.CabinetID))
                    .OrderBy(p => p.Number)
                    .Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false, Description = " (" + t.AORBPostAndCabinet.Name + ")" })
                    .ToList();
            }
        }

        public static List<Post> GetPostsByCabinetID(List<int> Ids)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) && (t.IsDelete == false)).Where(p => Ids.Contains(p.CabinetID)).ToList();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetPostCheckableByCabinetID(int CabinetID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Posts
        //            .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) &&
        //                  t.CabinetID == CabinetID &&
        //                  t.Status == (byte)DB.PostStatus.Dayer && (t.IsDelete == false))
        //            .OrderBy(t => t.Number)
        //            .Select(p => new CheckableItem
        //           {
        //               ID = p.ID,
        //               Name = p.Number.ToString(),
        //               Description = p.AORBPostAndCabinet.Name,
        //               IsChecked = false
        //           }).ToList();
        //    }
        //}

        //TODO:rad 13950620
        public static List<CheckableItem> GetPostCheckableByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                result = context.Posts
                                .Where(t =>
                                            DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) &&
                                            t.CabinetID == cabinetID &&
                                            t.Status == (byte)DB.PostStatus.Dayer &&
                                            (t.IsDelete == false)
                                       )
                                .OrderBy(t => t.Number)
                                .Select(p => new CheckableItem
                                            {
                                                ID = p.ID,
                                                Name = p.Number.ToString(),
                                                Description = p.AORBPostAndCabinet.Name,
                                                IsChecked = false
                                            }
                                       )
                                .ToList();
                return result;
            }
        }

        //milad doran
        //public static List<CheckableItem> GetPostCheckableByCabinetIDWithAB(int CabinetID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Posts
        //            .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) &&
        //                  t.CabinetID == CabinetID &&
        //                  t.Status == (byte)DB.PostStatus.Dayer && (t.IsDelete == false))
        //            .OrderBy(t => t.Number)
        //            .Select(p => new CheckableItem
        //            {
        //                ID = p.ID,
        //                Name = p.Number.ToString() + "(" + p.AORBPostAndCabinet.Name + ")",
        //                Description = p.AORBPostAndCabinet.Name,
        //                IsChecked = false
        //            }).ToList();
        //    }
        //}

        //TODO:rad 13950222
        public static List<CheckableItem> GetPostCheckableByCabinetIDWithAB(int CabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.Posts
                                   .Where(t =>
                                               (DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID)) &&
                                               (t.CabinetID == CabinetID) &&
                                               (t.Status == (byte)DB.PostStatus.Dayer) &&
                                               (t.IsDelete == false)
                                          )
                                   .OrderBy(t => t.Number)
                                   .Select(p => new CheckableItem
                                               {
                                                   ID = p.ID,
                                                   Name = p.Number.ToString() + "(" + p.AORBPostAndCabinet.Name + ")",
                                                   Description = p.AORBPostAndCabinet.Name,
                                                   IsChecked = false
                                               }
                                           )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        //TODO:rad 13950609
        public static bool HasConnectedPostContact(int postId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool result = false;

                result = context.PostContacts.Where(postContact =>
                                                                 (postContact.Status != (byte)DB.PostContactStatus.Deleted) &&
                                                                 (postContact.PostID == postId)
                                                   )
                                             .Any(postContact => postContact.Status == (byte)DB.PostContactStatus.CableConnection); //اتصالی پست در وضعیت متصل
                return result;
            }
        }

        //TODO:rad 13950609
        public static bool HasReservedPostContact(int postId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool result = false;

                result = context.PostContacts.Where(postContact =>
                                                                (postContact.Status != (byte)DB.PostContactStatus.Deleted) &&
                                                                (postContact.PostID == postId)
                                                   )
                                              .Any(postContact => postContact.Status == (byte)DB.PostContactStatus.FullBooking); //اتصالی پست در وضعیت رزرو

                return result;
            }
        }

        public static List<CheckableItem> GetPostCheckableByPostContact(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) && (t.IsDelete == false)).Where(p => p.PostContacts.Where(t2 => t2.ID == postContactID).SingleOrDefault().PostID == p.ID)
                    .Select(p => new CheckableItem
                {
                    ID = p.ID,
                    Name = p.Number.ToString(),
                    Description = " (" + p.AORBPostAndCabinet.Name + ")",
                    IsChecked = false
                }).ToList();
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => t.ID == id).SingleOrDefault().Cabinet.Center.Region.CityID;
            }
        }

        public static List<CheckableItem> GetPostCheckableByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts
                    .Where(t => centerID == t.Cabinet.CenterID && (t.IsDelete == false))
                    .OrderBy(t => t.Number)
                    .Select(p => new CheckableItem
                    {
                        ID = p.ID,
                        Name = p.Number.ToString(),
                        Description = " (" + p.AORBPostAndCabinet.Name + ")",
                        IsChecked = false
                    }).ToList();
            }
        }

        public static List<Post> GetAllPostsByPostContactList(List<long> postContactIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => context.PostContacts.Where(p => postContactIDs.Contains(p.ID) && (t.IsDelete == false)).Select(p => p.PostID).Distinct().Contains(t.ID)).ToList();
            }
        }

        public static List<Post> GetAllpostInCabinet(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => t.CabinetID == cabinetID && (t.IsDelete == false)).ToList();
            }
        }

        public static List<Post> GetTheNumberPostByStartID(int postID, int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => t.Number >= context.Posts.Where(p => p.ID == postID).SingleOrDefault().Number &&
                                                t.CabinetID == context.Posts.Where(p => p.ID == postID).SingleOrDefault().CabinetID && (t.IsDelete == false))
                                    .OrderBy(t => t.Number)
                                    .Take(count).ToList();
            }
        }

        public static List<Post> GetTheNumberPostByStartID(int cabinetID, int postID, int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => t.Number >= context.Posts.Where(p => p.ID == postID).SingleOrDefault().Number &&
                                                t.CabinetID == cabinetID && (t.IsDelete == false))
                                    .OrderBy(t => t.Number)
                                    .Take(count).ToList();
            }
        }
        public static Post GetPosByPostContactID(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.ID == postContactID && (t.Post.IsDelete == false)).Select(t => t.Post).SingleOrDefault();
            }
        }

        //TODO:milad doran
        //public static List<CheckableItem> GetPostCheckableByBuchtID(long buchtID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Posts.Where(t => t.CabinetID == context.CabinetInputs.Where(t3 => t3.ID == context.Buchts.Where(t2 => t2.ID == buchtID).SingleOrDefault().CabinetInputID).SingleOrDefault().CabinetID)
        //               .Where(t => t.Status == (byte)DB.PostStatus.Dayer && (t.IsDelete == false))
        //               .Select(t => new CheckableItem
        //                            {
        //                                 ID = t.ID,
        //                                 Name = t.Number.ToString(),
        //                                 Description = " (" + t.AORBPostAndCabinet.Name + ")",
        //                                 IsChecked = false,

        //                            }).ToList();
        //    }
        //}

        //TODO:rad 13950115
        public static List<CheckableItem> GetPostCheckableByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.Posts
                                   .Where(t =>
                                                t.CabinetID == context.CabinetInputs
                                                                      .Where(t3 =>
                                                                                    t3.ID == context.Buchts.Where(t2 => t2.ID == buchtID).SingleOrDefault().CabinetInputID
                                                                            )
                                                                      .SingleOrDefault()
                                                                      .CabinetID
                                          )
                                   .Where(t => t.Status == (byte)DB.PostStatus.Dayer && (t.IsDelete == false))
                                   .Select(t => new CheckableItem
                                               {
                                                   ID = t.ID,
                                                   Name = t.Number.ToString(),
                                                   Description = " (" + t.AORBPostAndCabinet.Name + ")",
                                                   IsChecked = false
                                               }
                                          )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        public static List<Post> GetPostsHaveLocation()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => t.Latitude != null && t.Longitude != null && (t.IsDelete == false)).ToList();
            }
        }

        public static IEnumerable GetPostCheckableByCabinetIDWithoutCoordinates(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) && t.CabinetID == cabinetID && t.Latitude == null && t.Longitude == null && (t.IsDelete == false))
                              .Select(t => new CheckableItem
                              {
                                  ID = t.ID,
                                  Name = t.Number.ToString(),
                                  Description = " (" + t.AORBPostAndCabinet.Name + ")",
                                  IsChecked = false

                              }).ToList();
            }
        }

        public static string GetAdjacentPost(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string result = string.Empty;
                context.AdjacentPosts.Where(t => t.PostID == postID).ToList().ForEach(t => { result += t.Post1.Number.ToString() + "-"; });
                if (result != string.Empty)
                    result = result.Remove(result.Length - 1);
                return result;
            }
        }

        public static List<AdjacentPostList> GetAdjacentPostList(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.AdjacentPosts.Where(t => t.PostID == ID)
                    .Select(t => new AdjacentPostList
                    {
                        ID = t.ID,
                        PostID = t.PostID,
                        PostNumber = t.Post.Number,
                        AdjacentPostID = t.AdjacentPostID,
                        AdjacentPostNumber = t.Post1.Number,

                    }).ToList();
            }
        }

        public static List<AdjacentPostList> GetAllAdjacentPostList(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.AdjacentPosts.Where(t => t.PostID == ID || t.AdjacentPostID == ID)
                    .Select(t => new AdjacentPostList
                    {
                        ID = t.ID,
                        PostID = t.PostID,
                        PostNumber = t.Post.Number,
                        AdjacentPostID = t.AdjacentPostID,
                        AdjacentPostNumber = t.Post1.Number,

                    }).ToList();
            }
        }

        //milad doran
        //public static int GetPostCountByCabinetID(int cabinetID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        var x = context.Posts.Where(t => t.CabinetID == cabinetID && (t.IsDelete == false)).GroupBy(t => t.Number);
        //        return x.Count();

        //    }
        //}

        //TODO:rad 13950222
        public static int GetPostCountByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int result;

                var groupedQuery = context.Posts
                                          .Where(post =>
                                                       (post.CabinetID == cabinetID) &&
                                                       (post.IsDelete == false)
                                                )
                                          .GroupBy(post => post.Number);

                result = groupedQuery.Count();

                return result;
            }
        }

        public static double GetDetailPostCountByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                int abCount = context.Posts.Where(t => t.CabinetID == cabinetID && t.Status != (byte)DB.PostStatus.Broken && t.AorBType == (byte)DB.AORBPostAndCabinet.AORB && t.IsDelete == false).Count();
                int aCount = context.Posts.Where(t => t.CabinetID == cabinetID && t.Status != (byte)DB.PostStatus.Broken && t.AorBType == (byte)DB.AORBPostAndCabinet.A && t.IsDelete == false).Count();
                int bCount = context.Posts.Where(t => t.CabinetID == cabinetID && t.Status != (byte)DB.PostStatus.Broken && t.AorBType == (byte)DB.AORBPostAndCabinet.B && t.IsDelete == false).Count();

                return abCount + (aCount * 0.5) + (bCount * 0.5);

            }
        }

        //TODO:rad
        public static string GetAorBTypeByPostID(int postId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string result = string.Empty;
                var tempResult = context.Posts.Where(p => p.ID == postId).Select(p => p.AorBType).SingleOrDefault();
                result = Helpers.GetEnumDescription((int)tempResult, typeof(DB.AORBPostAndCabinet));
                return result;
            }
        }

        public static List<InvestigatePossibilityWaitinglist> GetPostInWaitingListByIDs(List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilityWaitinglists.Where(t => t.PostID != null && postIDs.Contains((int)t.PostID)).ToList();
            }
        }

        public static List<InvestigatePossibilityWaitinglist> GetCabinetInWaitingListByIDs(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilityWaitinglists.Where(t => cabinetID == t.CabinetID).ToList();
            }
        }

        public static List<Post> GetPostByIDs(List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => postIDs.Contains(t.ID)).ToList();
            }
        }

        public static bool CheckIsAdjacentPost(int postID, int adjacentPostID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.AdjacentPosts.Any(t => t.PostID == postID && t.AdjacentPostID == adjacentPostID);
            }
        }

        public static List<AdjacentPost> GetAllAdjacentPost(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.AdjacentPosts.Where(t => t.PostID == postID).ToList();
            }
        }

        public static AdjacentPost GetAdjacentPostWithAdjacentPostID(int postID, int adjacentPostID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.AdjacentPosts.Where(t => t.PostID == postID && t.AdjacentPostID == adjacentPostID).SingleOrDefault();
            }
        }

        public static void DeleteAdjacentPostByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                AdjacentPost item = context.AdjacentPosts.Where(t => t.ID == id).SingleOrDefault();
                context.AdjacentPosts.DeleteOnSubmit(item);
                context.SubmitChanges();
            }
        }

        public static AdjacentPost GetAdjacentPostByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.AdjacentPosts.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}