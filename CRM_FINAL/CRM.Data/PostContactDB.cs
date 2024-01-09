using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class PostContactDB
    {
        public static List<PostContactInfo> SearchPostContact(
            List<int> cites,
            List<int> centers,
            List<int> cabinets,
            List<int> post,
            List<int> connectionType,
            List<int> status,
            int connectionNo,
            List<int> PCMType,
            int startRowIndex,
            int pageSize,
            long telephoneNo,
            long requestID,
            out int count
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<PostContactInfo> PostContactInfo = context.PostContacts
                            .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (p, b) => new { PostContacts = p, Buchts = b })
                            .SelectMany(tb => tb.Buchts.DefaultIfEmpty(), (bp, tb) => new { PostContacts = bp.PostContacts, Buchts = tb })
                            .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, tel => tel.SwitchPortID, (b, tel) => new { PostContctsBucht = b, Telephone = tel })
                            .SelectMany(tt => tt.Telephone.DefaultIfEmpty(), (pb, tt) => new { PostContcts = pb.PostContctsBucht.PostContacts, Buchts = pb.PostContctsBucht.Buchts, Telephone = tt })
                            .Where(t =>
                            (t.PostContcts.Status != (byte)DB.PostContactStatus.Deleted) &&
                            (cites.Count == 0 || cites.Contains(t.PostContcts.Post.Cabinet.Center.Region.CityID)) &&
                            (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PostContcts.Post.Cabinet.CenterID) : centers.Contains(t.PostContcts.Post.Cabinet.CenterID)) &&
                            (telephoneNo == -1 || t.Telephone.TelephoneNo == telephoneNo) &&
                            (connectionType.Count == 0 || connectionType.Contains(t.PostContcts.ConnectionType ?? 255)) &&
                            (status.Count == 0 || status.Contains(t.PostContcts.Status)) &&
                            (post.Count == 0 || post.Contains(t.PostContcts.PostID)) &&
                            (cabinets.Count == 0 || cabinets.Contains(t.PostContcts.Post.CabinetID)) &&
                            (connectionNo == -1 || t.PostContcts.ConnectionNo == connectionNo) &&
                            (PCMType.Count == 0 || PCMType.Contains(t.Buchts.PCMPort.PCM.PCMTypeID)) &&
                            (requestID == -1 || context.ViewReservBuchts.Any(t3 => t3.RequestID == requestID && t3.PostContactID == t.PostContcts.ID))
                          ).Select(t => new PostContactInfo
                                  {
                                      ID = t.PostContcts.ID,
                                      CabinetID = t.PostContcts.Post.CabinetID,
                                      CabinetNumber = t.PostContcts.Post.Cabinet.CabinetNumber,
                                      CabinetInputNumber = t.Buchts.CabinetInput.InputNumber,
                                      PostID = t.PostContcts.PostID,
                                      PostNumber = Convert.ToInt32(t.PostContcts.Post.Number),
                                      AdjacentPost = string.Join(",", context.AdjacentPosts.Where(t2 => t2.PostID == t.PostContcts.PostID).Select(t2 => t2.Post1.Number).ToList()),
                                      ConnectionNo = t.PostContcts.ConnectionNo,
                                      TelephoneNo = t.Telephone.TelephoneNo,
                                      ConnectionType = t.PostContcts.ConnectionType,
                                      StatusName = t.PostContcts.PostContactStatus.Name,
                                      RequestID = context.ViewReservBuchts.Where(t3 => t3.PostContactID == t.PostContcts.ID).Take(1).Select(t3 => t3.RequestID).SingleOrDefault(),
                                      Status = t.PostContcts.Status,
                                      ConnectionTypeName = t.PostContcts.PostContactConnectionType.Name,
                                      AORBType = t.PostContcts.Post.AORBPostAndCabinet.Name,
                                      BuchtTypeID = t.Buchts.BuchtTypeID,
                                      HasADSL = context.ADSLPAPPorts.Any(t2 => t2.TelephoneNo == t.Telephone.TelephoneNo),
                                      DateMalfunction = t.PostContcts.Status == (byte)DB.PostContactStatus.PermanentBroken
                                                        ?
                                                       (context.Malfuctions.Where(m => m.PostContactID == t.PostContcts.ID).SingleOrDefault() != null ? context.Malfuctions.Where(m => m.PostContactID == t.PostContcts.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().DateMalfunction.ToPersian(Date.DateStringType.Short) : "---")
                                                        : "---",
                                      TimeMalfunction = t.PostContcts.Status == (byte)DB.PostContactStatus.PermanentBroken ? context.Malfuctions.Where(m => m.PostContactID == t.PostContcts.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().TimeMalfunction : "---",
                                      Description = t.PostContcts.Status == (byte)DB.PostContactStatus.PermanentBroken ? context.Malfuctions.Where(m => m.PostContactID == t.PostContcts.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().Description : "---",
                                      TypeMalfunction = t.PostContcts.Status == (byte)DB.PostContactStatus.PermanentBroken
                                                             ?
                                                             (context.Malfuctions.Where(m => m.PostContactID == t.PostContcts.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault() != null ? Helpers.GetEnumDescription(context.Malfuctions.Where(m => m.PostContactID == t.PostContcts.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().TypeMalfunction, typeof(DB.PostContactMalfuctionType)) : "---")
                                                             : "---",


                                  }
                                  );

                PostContactInfo = PostContactInfo.Where(t => (t.BuchtTypeID != null ? t.BuchtTypeID != (int)DB.BuchtType.OutLine : true));
                count = PostContactInfo.Count();

                return PostContactInfo.OrderBy(t => t.CabinetNumber).ThenBy(t => t.PostNumber).ThenBy(t => t.ConnectionNo).Skip(startRowIndex).Take(pageSize).Distinct().ToList();
            }
        }
        public static int SearchPostContactCount(
         List<int> cites,
         List<int> centers,
         List<int> cabinets,
         List<int> post,
         List<int> connectionType,
         List<int> status,
         int connectionNo
         )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts
                             .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (p, b) => new { PostContacts = p, Buchts = b })
                             .SelectMany(tb => tb.Buchts.DefaultIfEmpty(), (bp, tb) => new { PostContacts = bp.PostContacts, Buchts = tb })
                             .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, tel => tel.SwitchPortID, (b, tel) => new { PostContctsBucht = b, Telephone = tel })
                             .SelectMany(tt => tt.Telephone.DefaultIfEmpty(), (pb, tt) => new { PostContcts = pb.PostContctsBucht.PostContacts, Buchts = pb.PostContctsBucht.Buchts, Telephone = tt })
                             .Where(t =>
                             (t.PostContcts.Status != (byte)DB.PostContactStatus.Deleted) &&
                             (cites.Count == 0 || cites.Contains(t.PostContcts.Post.Cabinet.Center.Region.CityID)) &&
                             (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PostContcts.Post.Cabinet.CenterID) : centers.Contains(t.PostContcts.Post.Cabinet.CenterID)) &&
                             (connectionType.Count == 0 || connectionType.Contains(t.PostContcts.ConnectionType ?? 255)) &&
                             (status.Count == 0 || status.Contains(t.PostContcts.Status)) &&
                             (post.Count == 0 || post.Contains(t.PostContcts.PostID)) &&
                             (cabinets.Count == 0 || cabinets.Contains(t.PostContcts.Post.CabinetID)) &&
                             (connectionNo == -1 || t.PostContcts.ConnectionNo == connectionNo)
                           ).Count();
            }
        }




        public static List<PostContactInfo> GetPostContactsInfoReport(int cityID, int centerID, int cabinetID, int postID, long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var result = context.PostContacts
                                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (p, b) => new { PostContacts = p, Buchts = b })
                                    .SelectMany(tb => tb.Buchts.DefaultIfEmpty(), (bp, tb) => new { PostContacts = bp.PostContacts, Buchts = tb })

                                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, tel => tel.SwitchPortID, (b, tel) => new { PostContactsBucht = b, Telephone = tel })
                                    .SelectMany(tt => tt.Telephone.DefaultIfEmpty(), (pb, tt) => new { PostContactsBuchts = pb.PostContactsBucht, Telephone = tt })

                                    .Where(t =>
                                                (t.PostContactsBuchts.PostContacts.Status != (byte)DB.PostContactStatus.Deleted) &&
                                                (t.PostContactsBuchts.PostContacts.Post.Cabinet.Center.Region.CityID == cityID) &&
                                                (centerID == -1 || t.PostContactsBuchts.PostContacts.Post.Cabinet.CenterID == centerID) &&
                                                (cabinetID == -1 || t.PostContactsBuchts.PostContacts.Post.Cabinet.ID == cabinetID) &&
                                                (postID == -1 || t.PostContactsBuchts.PostContacts.Post.ID == postID) &&
                                                (postContactID == -1 || t.PostContactsBuchts.PostContacts.ID == postContactID)
                                          )
                                    .Select(t => new PostContactInfo
                                               {
                                                   City = t.PostContactsBuchts.PostContacts.Post.Cabinet.Center.Region.City.Name,
                                                   Center = t.PostContactsBuchts.PostContacts.Post.Cabinet.Center.CenterName,
                                                   CabinetNumber = t.PostContactsBuchts.PostContacts.Post.Cabinet.CabinetNumber,
                                                   CabinetInputNumber = t.PostContactsBuchts.Buchts.CabinetInput.InputNumber,
                                                   PostNumber = t.PostContactsBuchts.PostContacts.Post.Number,
                                                   AORBType = t.PostContactsBuchts.PostContacts.Post.AORBPostAndCabinet.Name,
                                                   ConnectionNo = t.PostContactsBuchts.PostContacts.ConnectionNo,
                                                   TelephoneNo = t.Telephone.TelephoneNo,
                                                   StatusName = t.PostContactsBuchts.PostContacts.PostContactStatus.Name,
                                                   Bucht = "ردیف : " + t.PostContactsBuchts.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " + "طبقه : " + t.PostContactsBuchts.Buchts.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.PostContactsBuchts.Buchts.BuchtNo,
                                               }
                                           )
                                    .ToList();

                return result;

            }
        }




        public static PostContact GetPostContactByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts
                    .Where(t => t.ID == id && t.Status != (byte)DB.PostContactStatus.Deleted)
                    .SingleOrDefault();
            }
        }
        public static PostContact GetFreePostContactByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts
                    .Where(t => t.ID == id && t.Status == (byte)DB.PostContactStatus.Free)
                    .SingleOrDefault();
            }
        }
        public static List<CheckableItem> GetPostContactCheckableByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID && t.Status != (byte)DB.PostContactStatus.Deleted)
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.ConnectionNo.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetPostContactCheckableByPostIDs(List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => postIDs.Contains(t.PostID) && t.Status != (byte)DB.PostContactStatus.Deleted)
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.ConnectionNo.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }


        public static List<PostContact> GetPostContactByPostID(int post)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == post && t.Status != (byte)DB.PostContactStatus.Deleted).OrderBy(t => t.ConnectionNo).ToList();
            }
        }

        public static List<int> GetPostContactNoByPostID(int post)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == post && t.Status != (byte)DB.PostContactStatus.Deleted).OrderBy(t => t.ConnectionNo).Select(t => t.ConnectionNo).ToList();
            }
        }

        public static List<PostContact> GetPostContactByPostID(List<int> postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => postID.Contains(t.PostID) && t.Status != (byte)DB.PostContactStatus.Deleted).ToList();
            }
        }

        public static List<PostContact> GetPostContactByListID(List<long> PostContactIDList)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => PostContactIDList.Contains(t.ID) && t.Status != (byte)DB.PostContactStatus.Deleted).ToList();
            }
        }

        public static byte? GetStatusByCheckConnectingToBucht(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Bucht bucht = Data.BuchtDB.GetBuchtByConnectionID(postContactID);
                if (bucht == null)
                {
                    return (byte)DB.PostContactStatus.Free;
                }
                else if (bucht != null && bucht.SwitchPortID != null)
                {
                    return (byte)DB.PostContactStatus.CableConnection;
                }
                else if (bucht != null && bucht.SwitchPortID == null)
                {
                    return (byte)DB.PostContactStatus.Free;
                }

                return null;
            }
        }

        public static List<PostContact> GetPCMPostContactByPostContactID(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == context.PostContacts.Where(t2 => t2.ID == postContactID).SingleOrDefault().PostID &&
                                                       t.ConnectionNo == context.PostContacts.Where(t3 => t3.ID == postContactID).SingleOrDefault().ConnectionNo &&
                                                       t.Status != (byte)DB.PostContactStatus.Deleted
                                                 ).ToList();
            }
        }

        public static List<PostContactMalfunction> GetFailurePostContactStatistic(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {



            using (MainDataContext context = new MainDataContext())
            {

                List<PostContactMalfunction> result1 =
                    context.PostContacts
                    .GroupJoin(context.Malfuctions, p => p.ID, m => m.PostContactID, (p, m) => new { postcontacts = p, Malfunction = p.Malfuctions })
                    .SelectMany(t1 => t1.Malfunction.DefaultIfEmpty(), (p, t1) => new { MaluFunctionPostContact = t1, PostContact = p.postcontacts })

                    .GroupJoin(context.Buchts, p => p.MaluFunctionPostContact.PostContact.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.PostContact.Buchts, MalFunction = b.MaluFunctionPostContact })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContactsBucht = t1, Buchts = t2 })

                    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.Buchts.PostContact.Post.Cabinet.CenterID)) &&
                            (!FromDate.HasValue || t.PostContactsBucht.MalFunction.DateMalfunction.Date >= FromDate) &&
                            (!ToDate.HasValue || t.PostContactsBucht.MalFunction.DateMalfunction.Date <= ToDate) &&
                            (t.PostContactsBucht.MalFunction.PostContact.Status == (byte)DB.PostContactStatus.PermanentBroken)
                            ).Select(t => new PostContactMalfunction
                            {
                                Cabinet = t.Buchts.CabinetInput.Cabinet.CabinetNumber,
                                Post = t.Buchts.PostContact.Post.Number,
                                PostContact = t.Buchts.PostContact.ConnectionNo,
                                CenterName = t.Buchts.PostContact.Post.Cabinet.Center.CenterName,
                                Description = t.PostContactsBucht.MalFunction.Description,
                                TypeMalfaction = t.PostContactsBucht.MalFunction.MalfuctionType,
                                Failure_Date = t.PostContactsBucht.MalFunction.DateMalfunction,
                                Failure_Time = t.PostContactsBucht.MalFunction.TimeMalfunction,
                                CorrectionType = "اصلاح نشده"
                            }
                            ).ToList();
                string query =
                @"SELECT Cabinet.CabinetNumber as Cabinet ,Post.Number as Post ,pc.ConnectionNo as PostContact ,L.malfuactionID1,L.PostContactID1,L.ID1 ,L.PCID1,L.malfuactionID2,L.PostContactID2,L.ID2 ,L.PCID2 , m1.TypeMalfunction AS TypeMalfaction, m1.DateMalfunction AS Failure_Date,m1.TimeMalfunction AS Failure_Time,m2.DateMalfunction AS Correct_Date,m2.TimeMalfunction AS Correct_Time ,m2.[Description], Center.CenterName, 'اصلاح شده' CorrectionType
                    FROM 
                    (SELECT malfuactionID1 , PostContactID1 ,ID1 ,malfuactionID2 , T2.PostContactID2 ,  ID2, PCID1,pcid2 
	                FROM
	                (SELECT *  FROM
		                (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID1 , p.ID as PostContactID1 , p.ID as ID1 , m.PostContactID as PCID1
		                FROM PostContact  as p  JOIN Malfuction m on m.PostContactID = p.ID 
		                WHERE Status= 2 OR Status= 1) as T 
	                WHERE T.RowNumber <= 2) as T1
	                join 
	                (select * From 
	                    (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID2 , p.ID as PostContactID2 , p.ID as ID2, m.PostContactID as PCID2
			                FROM PostContact as p  JOIN Malfuction m on m.PostContactID = p.ID 
	                    WHERE Status= 2 OR Status= 1 ) as T 
		                WHERE T.RowNumber <= 2) as T2 ON T1.ID1 = T2.id2 AND T1.malfuactionID1 < T2.malfuactionID2

	                ) as L join Malfuction m1 ON m1.ID = L.malfuactionID1
	                    JOIN Malfuction m2 ON m2.ID = L.malfuactionID2
						join postcontact pc ON pc.ID = L.PCID1
	                    LEFT join Bucht b on b.ConnectionID  = pc.ID
	                    join Post  on Post.ID = pc.PostID 
	                    join Cabinet  on Cabinet.ID = Post.CabinetID
						JOIN Center on Cabinet.CenterID = Center.ID ";


                if (FromDate.HasValue)
                    query += " and Convert(date , m1.DateMalfunction) >= CONVERT(date, '" + FromDate.Value.ToShortDateString() + "')";
                if (ToDate.HasValue)
                    query += " and Convert(date , m1.DateMalfunction) <= CONVERT(date, '" + ToDate.Value.ToShortDateString() + "')";
                if (CenterIDs.Count != 0)
                {
                    string CenterList = DB.MakeTheList(CenterIDs);
                    query += " and Cabinet.CenterID in " + CenterList;
                }
                List<PostContactMalfunction> result2 = new List<PostContactMalfunction>();
                List<PostContactMalfunction> result = new List<PostContactMalfunction>();
                result2 = context.ExecuteQuery<PostContactMalfunction>(string.Format(query)).ToList();
                result = result1.Union(result2).ToList();
                return result;


            }
        }
        public static List<PostContactMalfunction> GetFailureNotCorrectPostContactStatistic(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return
                     context.PostContacts
                     .GroupJoin(context.Malfuctions, p => p.ID, m => m.PostContactID, (p, m) => new { postcontacts = p, Malfunction = p.Malfuctions })
                     .SelectMany(t1 => t1.Malfunction.DefaultIfEmpty(), (p, t1) => new { MaluFunctionPostContact = t1, PostContact = p.postcontacts })

                     .GroupJoin(context.Buchts, p => p.MaluFunctionPostContact.PostContact.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.PostContact.Buchts, MalFunction = b.MaluFunctionPostContact })
                     .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContactsBucht = t1, Buchts = t2 })

                     .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.Buchts.PostContact.Post.Cabinet.CenterID)) &&
                             (!FromDate.HasValue || t.PostContactsBucht.MalFunction.DateMalfunction.Date >= FromDate) &&
                             (!ToDate.HasValue || t.PostContactsBucht.MalFunction.DateMalfunction.Date <= ToDate)
                             &&
                             (t.PostContactsBucht.MalFunction.PostContact.Status == (byte)DB.PostContactStatus.PermanentBroken)
                             )
                             .Select(t => new PostContactMalfunction
                             {
                                 Cabinet = t.Buchts.CabinetInput.Cabinet.CabinetNumber,
                                 Post = t.Buchts.PostContact.Post.Number,
                                 PostContact = t.Buchts.PostContact.ConnectionNo,
                                 CenterName = t.Buchts.PostContact.Post.Cabinet.Center.CenterName,
                                 Description = t.PostContactsBucht.MalFunction.Description,
                                 TypeMalfaction = t.PostContactsBucht.MalFunction.MalfuctionType,
                                 Failure_Date = t.PostContactsBucht.MalFunction.DateMalfunction,
                                 Failure_Time = t.PostContactsBucht.MalFunction.TimeMalfunction,
                                 CorrectionType = "اصلاح نشده"
                             }
                             ).ToList();
            }
        }
        public static List<PostContactMalfunction> GetFailureCorrectPostContactStatistic(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string query =
                    @"SELECT Cabinet.CabinetNumber as Cabinet ,Post.Number as Post ,pc.ConnectionNo as PostContact ,L.malfuactionID1,L.PostContactID1,L.ID1 ,L.PCID1,L.malfuactionID2,L.PostContactID2,L.ID2 ,L.PCID2 , m1.TypeMalfunction AS TypeMalfaction, m1.DateMalfunction AS Failure_Date,m1.TimeMalfunction AS Failure_Time,m2.DateMalfunction AS Correct_Date,m2.TimeMalfunction AS Correct_Time ,m2.[Description], Center.CenterName, 'اصلاح شده' CorrectionType
                    FROM 
                    (SELECT malfuactionID1 , PostContactID1 ,ID1 ,malfuactionID2 , T2.PostContactID2 ,  ID2, PCID1,pcid2 
	                FROM
	                (SELECT *  FROM
		                (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID1 , p.ID as PostContactID1 , p.ID as ID1 , m.PostContactID as PCID1
		                FROM PostContact  as p  JOIN Malfuction m on m.PostContactID = p.ID ) as T 
	                WHERE T.RowNumber <= 2) as T1
	                join 
	                (select * From 
	                    (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID2 , p.ID as PostContactID2 , p.ID as ID2, m.PostContactID as PCID2
			                FROM PostContact as p  JOIN Malfuction m on m.PostContactID = p.ID ) as T 
		                WHERE T.RowNumber <= 2) as T2 ON T1.ID1 = T2.id2 AND T1.malfuactionID1 < T2.malfuactionID2

	                ) as L join Malfuction m1 ON m1.ID = L.malfuactionID1
	                    JOIN Malfuction m2 ON m2.ID = L.malfuactionID2
						join postcontact pc ON pc.ID = L.PCID1
	                    LEFT join Bucht b on b.ConnectionID  = pc.ID
	                    join Post  on Post.ID = pc.PostID 
	                    join Cabinet  on Cabinet.ID = Post.CabinetID
						JOIN Center on Cabinet.CenterID = Center.ID ";


                if (FromDate.HasValue)
                    query += " and CONVERT(date,m1.DateMalfunction) >= CONVERT(date, '" + FromDate.Value.ToShortDateString() + "')";
                if (ToDate.HasValue)
                    query += " and CONVERT(date,m1.DateMalfunction) <= CONVERT(date, '" + ToDate.Value.ToShortDateString() + "')";
                if (CenterIDs.Count != 0)
                {
                    string CenterList = DB.MakeTheList(CenterIDs);
                    query += " and Cabinet.CenterID in " + CenterList;
                }
                return context.ExecuteQuery<PostContactMalfunction>(string.Format(query)).ToList();
            }
        }


        public static CheckableItem GetPostContactCheckableItemByID(long id)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.ID == id && t.Status != (byte)DB.PostContactStatus.Deleted).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false }).SingleOrDefault();
            }
        }
        /// <summary>
        /// return PostContects all post have status or not have status
        /// </summary>
        /// <param name="Posts">post list</param>
        /// <param name="status">status list</param>
        /// <param name="isStatus">true include status, false not include status</param>
        /// <returns></returns>
        public static List<PostContact> GetPostContactByStatus(List<Post> posts, List<byte> status, bool isStatus = true)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<PostContact> query;
                if (isStatus == true)
                    query = context.PostContacts.Where(t => posts.Select(p => p.ID).Contains(t.PostID) && status.Contains(t.Status));
                else
                    query = context.PostContacts.Where(t => posts.Select(p => p.ID).Contains(t.PostID) && !status.Contains(t.Status));

                return query.ToList();

            }
        }

        public static int GetPostContactConutOfCableConnectionByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID && t.Status != (byte)DB.PostContactStatus.Deleted)
                    .Where(t => t.Status == (byte)DB.PostContactStatus.CableConnection)
                    .Count();
            }

        }

        public static AboneInfo GetAboneInfoByPostContactID(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.ID == postContactID && t.Status != (byte)DB.PostContactStatus.Deleted)
                    .Select(t => new AboneInfo
                                  {
                                      PostContactID = t.ID,
                                      ConnectionNo = t.ConnectionNo,
                                      CabinetID = t.Post.CabinetID,
                                      CabinetNumber = t.Post.Cabinet.CabinetNumber,
                                      CabinetInputID = context.Buchts.Where(t2 => t2.ConnectionID == t.ID).SingleOrDefault().CabinetInput.ID,
                                      CabinetInputNumber = context.Buchts.Where(t2 => t2.ConnectionID == t.ID).SingleOrDefault().CabinetInput.InputNumber,
                                      PostID = t.PostID,
                                      PostNumber = t.Post.Number,

                                  }).SingleOrDefault();
            }
        }

        public static List<PostContact> GetPostContactByPostIDs(List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => postIDs.Contains(t.PostID) && t.Status != (byte)DB.PostContactStatus.Deleted).ToList();
            }
        }

        public static PostContact GetPostContact(int postID, int connectionNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID && t.ConnectionNo == connectionNo && t.Status != (byte)DB.PostContactStatus.Deleted).SingleOrDefault();
            }
        }

        public static List<PostContact> GetPostContactByPostID(int postID, int connectionNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID &&
                    t.ConnectionNo == connectionNo &&
                    t.Status != (byte)DB.PostContactStatus.Deleted
                    ).ToList();
            }
        }

        public static List<PostContact> GetPostContactByIDs(List<long> IDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => IDs.Contains(t.ID) && t.Status != (byte)DB.PostContactStatus.Deleted).ToList();
            }
        }

        public static List<PostContact> GetPostContactConnctionNo(List<int> connectionNos, int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID &&
                                                       connectionNos.Contains(t.ConnectionNo) &&
                                                       t.Status != (byte)DB.PostContactStatus.Deleted
                                                 ).ToList();
            }
        }

        public static List<CheckableItem> GetFreePostContactByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.GroupJoin(context.Buchts, pc => pc.ID, b => b.ConnectionID, (pc, b) => new { PostContact = pc, Bucht = b })
                              .SelectMany(t => t.Bucht.DefaultIfEmpty(), (p, b) => new { PostContact = p.PostContact, Bucht = b })
                              .Where(t => (t.PostContact.PostID == postID) &&
                              (t.PostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml || t.PostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal) &&
                              (t.PostContact.Status == (byte)DB.PostContactStatus.Free))
                              .OrderBy(t => t.PostContact.ConnectionNo)
                              .OrderBy(t => t.Bucht.PortNo)
                              .Select(t => new CheckableItem
                              {
                                  LongID = t.PostContact.ID,
                                  Name = t.PostContact.ConnectionNo.ToString(),
                                  Description = t.Bucht.PCMPort.PortNumber.ToString(),
                                  IsChecked = false,
                              }).ToList();
            }
        }

        //TODO:milad doran
        //public static List<CheckableItem> GetFreePostContactByPostIDWithOutPCM(int postID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PostContacts.Where(t => t.PostID == postID &&
        //            t.ConnectionType == (byte)DB.PostContactConnectionType.Noraml &&
        //            t.Status == (byte)DB.PostContactStatus.Free)
        //                      .OrderBy(t => t.ConnectionNo)
        //                      .Select(t => new CheckableItem
        //                      {
        //                          LongID = t.ID,
        //                          Name = t.ConnectionNo.ToString(),
        //                          IsChecked = false,
        //                      }).ToList();
        //    }
        //}

        //TODO:rad 13950115
        public static List<CheckableItem> GetFreePostContactByPostIDWithOutPCM(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.PostContacts
                                   .Where(t =>
                                                t.PostID == postID &&
                                                t.ConnectionType == (byte)DB.PostContactConnectionType.Noraml &&
                                                t.Status == (byte)DB.PostContactStatus.Free
                                         )
                                   .OrderBy(t => t.ConnectionNo)
                                   .Select(t => new CheckableItem
                                               {
                                                   LongID = t.ID,
                                                   Name = t.ConnectionNo.ToString(),
                                                   IsChecked = false,
                                               }
                                           )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        //milad doran
        //public static List<CheckableItem> GetFreePostContactCheckableByPostID(int postID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PostContacts.Where(t => t.PostID == postID &&
        //            t.Status == (byte)DB.PostContactStatus.Free &&
        //               t.ConnectionType != (byte)DB.PostContactConnectionType.PCMRemote)
        //               .OrderBy(t => t.ConnectionNo)
        //               .Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal) ? "پی سی ام" : "معمولی" }).ToList();
        //    }
        //}

        //TODO:rad 13950620
        public static List<CheckableItem> GetFreePostContactCheckableByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                result = context.PostContacts
                                .Where(t =>
                                            t.PostID == postID &&
                                            t.Status == (byte)DB.PostContactStatus.Free &&
                                            t.ConnectionType != (byte)DB.PostContactConnectionType.PCMRemote
                                       )
                                .OrderBy(t => t.ConnectionNo)
                                .Select(t => new CheckableItem
                                            {
                                                LongID = t.ID,
                                                Name = t.ConnectionNo.ToString(),
                                                IsChecked = false,
                                                Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal) ? "پی سی ام" : "معمولی"
                                            }
                                        )
                                .ToList();

                return result;
            }
        }

        //milad doran
        //public static List<CheckableItem> GetConnectionPostContactCheckableByPostID(int postID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PostContacts.Where(t => t.PostID == postID &&
        //            t.ConnectionType == (byte)DB.PostContactConnectionType.Noraml &&
        //            t.Status == (byte)DB.PostContactStatus.CableConnection)
        //            .OrderBy(t => t.ConnectionNo)
        //            .Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : "معمولی" }).ToList();

        //    }
        //}

        //TODO:rad 13950620
        public static List<CheckableItem> GetConnectionPostContactCheckableByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                result = context.PostContacts
                                .Where(t =>
                                              t.PostID == postID &&
                                              t.ConnectionType == (byte)DB.PostContactConnectionType.Noraml &&
                                              t.Status == (byte)DB.PostContactStatus.CableConnection
                                       )
                                .OrderBy(t => t.ConnectionNo)
                                .Select(t => new CheckableItem
                                              {
                                                  LongID = t.ID,
                                                  Name = t.ConnectionNo.ToString(),
                                                  IsChecked = false,
                                                  Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : "معمولی"
                                              }
                                       )
                                .ToList();

                return result;
            }
        }

        public static List<CheckableItem> GetConnectionPostContactWithRemotPCMCheckableByPostID(int postID, bool withPCM)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID &&
                               (withPCM == true ? (t.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote || (t.ConnectionType == (int)DB.PostContactConnectionType.Noraml && t.Status == (int)DB.PostContactStatus.CableConnection)) : (t.ConnectionType == (int)DB.PostContactConnectionType.Noraml && t.Status == (int)DB.PostContactStatus.CableConnection))
                        )
                    .OrderBy(t => t.ConnectionNo)
                    .Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : "معمولی" }).ToList();

            }
        }

        public static List<CheckableItem> GetConnectionPostContactWithPCMCheckableByPostID(int postID, bool withPCM)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID &&
                                t.Status == (int)DB.PostContactStatus.CableConnection &&
                               (withPCM == true ? t.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote : t.ConnectionType == (int)DB.PostContactConnectionType.Noraml))
                   .OrderBy(t => t.ConnectionNo)
                    .Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : "معمولی" }).ToList();

            }
        }

        public static List<PostContactInfo> GetFreePostContactInfoByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.Post.CabinetID == cabinetID &&
                    t.Status == (byte)DB.PostContactStatus.Free &&
                    t.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                       .OrderBy(t => t.Post.Number).ThenBy(t => t.ConnectionNo)
                       .Select(t => new PostContactInfo
                       {
                           ID = t.ID,
                           ConnectionNo = t.ConnectionNo,
                           PostID = t.PostID,
                           PostNumber = t.Post.Number,
                           ABType = t.Post.AorBType,
                           ABTypeName = t.Post.AORBPostAndCabinet.Name
                       }
                                               ).ToList();
            }
        }

        //milad doran
        //public static List<PostContactInfo> GetFreePostContactInfoByCabinetIDWithPCM(int cabinetID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PostContacts.Where(t => t.Post.CabinetID == cabinetID &&
        //            t.Status == (byte)DB.PostContactStatus.Free)
        //               .OrderBy(t => t.Post.Number)
        //               .ThenBy(t => t.ConnectionNo)
        //               .Select(t => new PostContactInfo
        //               {
        //                   ID = t.ID,
        //                   ConnectionNo = t.ConnectionNo,
        //                   PostID = t.PostID,
        //                   PostNumber = t.Post.Number,
        //                   ABType = t.Post.AorBType,
        //                   ABTypeName = t.Post.AORBPostAndCabinet.Name,
        //                   ToConnectiontype = (t.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی"),
        //                   ConnectionType = t.ConnectionType,
        //                   CabinetInputID = t.Buchts.Take(1).Select(t2 => t2.CabinetInputID).SingleOrDefault(),
        //               }).ToList();
        //    }
        //}

        //TODO:rad 13950620
        public static List<PostContactInfo> GetFreePostContactInfoByCabinetIDWithPCM(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PostContactInfo> result = new List<PostContactInfo>();

                result = context.PostContacts.Where(t => 
                                                        t.Post.CabinetID == cabinetID &&
                                                        t.Status == (byte)DB.PostContactStatus.Free
                                                    )
                                             .OrderBy(t => t.Post.Number)
                                             .ThenBy(t => t.ConnectionNo)
                                             .Select(t => new PostContactInfo
                                                            {
                                                                ID = t.ID,
                                                                ConnectionNo = t.ConnectionNo,
                                                                PostID = t.PostID,
                                                                PostNumber = t.Post.Number,
                                                                ABType = t.Post.AorBType,
                                                                ABTypeName = t.Post.AORBPostAndCabinet.Name,
                                                                ToConnectiontype = (t.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی"),
                                                                ConnectionType = t.ConnectionType,
                                                                CabinetInputID = t.Buchts.Take(1).Select(t2 => t2.CabinetInputID).SingleOrDefault(),
                                                            }
                                                     )
                                             .ToList();

                return result;
            }
        }

        public static List<PostContactInfo> GetFreePostContactInfoByCabinetID(int cabinetID, List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.Post.CabinetID == cabinetID &&
                    postIDs.Contains(t.Post.ID) &&
                    t.Status == (byte)DB.PostContactStatus.Free)
                       .OrderBy(t => t.Post.Number)
                       .ThenBy(t => t.ConnectionNo)
                       .Select(t => new PostContactInfo
                       {
                           ID = t.ID,
                           ConnectionNo = t.ConnectionNo,
                           PostID = t.PostID,
                           PostNumber = t.Post.Number,
                           ABType = t.Post.AorBType,
                           ABTypeName = t.Post.AORBPostAndCabinet.Name,
                           ToConnectiontype = (t.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی"),
                           ConnectionType = t.ConnectionType,
                           CabinetInputID = t.Buchts.Take(1).Select(t2 => t2.CabinetInputID).SingleOrDefault(),
                       }).ToList();
            }
        }


        public static List<PostContactInfo> GetBrokenPostContactByCabinetID(int cabinetID, List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.Post.CabinetID == cabinetID &&
                    postIDs.Contains(t.Post.ID) &&
                    (t.Status == (byte)DB.PostContactStatus.PermanentBroken) &&
                    t.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                       .OrderBy(t => t.Post.Number)
                       .ThenBy(t => t.ConnectionNo)
                       .Select(t => new PostContactInfo
                       {
                           ID = t.ID,
                           ConnectionNo = t.ConnectionNo,
                           PostID = t.PostID,
                           PostNumber = t.Post.Number,
                           ABType = t.Post.AorBType,
                           ABTypeName = t.Post.AORBPostAndCabinet.Name,
                           Status = t.Status,
                       }).ToList();
            }
        }
        public static List<PostContact> GetFreePostContactByCabinetID(int cabinetID, List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts
                    .Where(t => t.Post.CabinetID == cabinetID &&
                    postIDs.Contains(t.Post.ID) &&
                    t.Status == (byte)DB.PostContactStatus.Free &&
                    t.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                    .ToList();
            }
        }

        public static List<PostContact> GetPostContactByPostIDAndConnectionNo(int postID, int connectionNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.PostID == postID && t.ConnectionNo == connectionNo).ToList();
            }
        }

        public static List<PostContact> GetPCMPostContactByPostIDs(List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => postIDs.Contains(t.PostID) && t.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal).ToList();
            }
        }
    }
}