using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class CablePairDB
    {
        public static List<CablePairInfo> SearchCablePair(
            List<int> city,
            List<int> center,
            List<long> cableNumber,
            int fromCablePairNumber,
            int toCablePairNumber,
            List<int> status,
            int startRowIndex,
            int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CablePairInfo> result = new List<CablePairInfo>();
                var query = context.CablePairs
                                   .Where(t =>
                                            (city.Count == 0 || city.Contains(t.Cable.Center.Region.CityID)) &&
                                            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cable.CenterID) : center.Contains(t.Cable.CenterID)) &&
                                            (cableNumber.Count == 0 || cableNumber.Contains(t.CableID)) &&
                                            (fromCablePairNumber == -1 ? true : t.CablePairNumber >= fromCablePairNumber) &&
                                            (toCablePairNumber == -1 ? true : t.CablePairNumber <= toCablePairNumber) &&
                                            (status.Count == 0 || status.Contains(t.Status))
                                         )
                                   .GroupJoin(context.Buchts, cp => cp.ID, b => b.CablePairID, (cp, b) => new { cp = cp, Bucht = b })
                                   .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (cpb, t2) => new { cp = cpb.cp, bucht = t2 })
                                   .Select(t3 => new CablePairInfo
                                                {
                                                    CityName = t3.cp.Cable.Center.Region.City.Name,
                                                    CenterName = t3.cp.Cable.Center.CenterName,
                                                    ID = t3.cp.ID,
                                                    CableID = t3.cp.CableID,
                                                    CableNumber = t3.cp.Cable.CableNumber,
                                                    CablePairNumber = t3.cp.CablePairNumber,
                                                    Status = t3.cp.Status,
                                                    InsertDate = t3.cp.InsertDate,
                                                    Connection = "ردیف : " + t3.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " +
                                                                 "طبقه : " + t3.bucht.VerticalMDFRow.VerticalRowNo + " ،  " +
                                                                 "اتصالی : " + t3.bucht.BuchtNo,
                                                }
                                            )
                                   .AsQueryable();
                count = query.Count();
                result = query.Skip(startRowIndex).Take(pageSize).ToList();
                return result;
            }
        }

        public static int SearchCablePairCount(
          List<int> city,
          List<int> center,
          List<long> cableNumber,
          int fromCablePairNumber,
          int toCablePairNumber,
          List<int> status
             )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs
                         .Where(t =>
                            (city.Count == 0 || city.Contains(t.Cable.Center.Region.CityID)) &&
                            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cable.CenterID) : center.Contains(t.Cable.CenterID)) &&
                            (cableNumber.Count == 0 || cableNumber.Contains(t.CableID)) &&
                            (fromCablePairNumber == -1 ? true : t.CablePairNumber >= fromCablePairNumber) &&
                            (toCablePairNumber == -1 ? true : t.CablePairNumber <= toCablePairNumber) &&
                            (status.Count == 0 || status.Contains(t.Status))
                          )
                          .GroupJoin(context.Buchts, cp => cp.ID, b => b.CablePairID, (cp, b) => new { cp = cp, Bucht = b })
                          .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (cpb, t2) => new { cp = cpb.cp, bucht = t2 }).Count();
            }
        }
        public static void GetBuchtByCablePairID(long cablePairID, out Bucht bucht, out  int verticalMDFColumnID, out int verticalMDFRowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bucht = context.Buchts.Where(t => t.CablePairID == cablePairID).SingleOrDefault();
                verticalMDFColumnID = context.Buchts.Where(t => t.CablePairID == cablePairID).Select(t => t.VerticalMDFRow.VerticalMDFColumn.ID).SingleOrDefault();
                verticalMDFRowID = context.Buchts.Where(t => t.CablePairID == cablePairID).Select(t => t.VerticalMDFRow.ID).SingleOrDefault();
            }
        }

        public static string GetConnectionInfoByCablePairID(long cablePairID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CablePairID == cablePairID).Select(t => "ردیف:" + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + "طبقه:" + t.VerticalMDFRow.VerticalRowNo.ToString() + "اتصالی:" + t.BuchtNo.ToString()).SingleOrDefault().ToString();
            }
        }

        public static CablePair GetCablePairByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCablePairCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs
                     .OrderBy(t => t.CablePairNumber)
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.CablePairNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetCablePairCheckableByCableID(long cableID, bool IsNullCabinetInputID = false)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.CablePairs.Where(t => t.CableID == cableID)
        //            .Where(t => IsNullCabinetInputID == false || t.CabinetInputID == null)
        //            .OrderBy(t => t.CablePairNumber)
        //            .Select(t => new CheckableItem
        //            {
        //                LongID = t.ID,
        //                Name = t.CablePairNumber.ToString(),
        //                IsChecked = false
        //            }
        //                )
        //            .ToList();
        //    }
        //}

        //TODO:rad 13950220
        public static List<CheckableItem> GetCablePairCheckableByCableID(long cableID, bool IsNullCabinetInputID = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.CablePairs.Where(t => t.CableID == cableID)
                                              .Where(t => IsNullCabinetInputID == false || t.CabinetInputID == null)
                                              .OrderBy(t => t.CablePairNumber)
                                              .Select(t => new CheckableItem
                                                            {
                                                                LongID = t.ID,
                                                                Name = t.CablePairNumber.ToString(),
                                                                IsChecked = false
                                                            }
                                                      )
                                              .AsQueryable();

                result = query.ToList();

                return result;
            }
        }
        public static List<CablePair> GetCablePairByCableID(long cableID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs.OrderBy(t => t.CablePairNumber).Where(t => t.CableID == cableID)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetFreeCablePairCheckableByCableID(long cableID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs.Where(t => t.CableID == cableID && t.Status == (byte)DB.CablePairStatus.Free)
                     .OrderBy(t => t.CablePairNumber)
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.CablePairNumber.ToString(),
                        IsChecked = false
                    }
                    )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetConnectCablePairCheckableByCableID(long cableID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs.Where(t => t.CableID == cableID && t.Status == (byte)DB.CablePairStatus.ConnectedToBucht)
                     .OrderBy(t => t.CablePairNumber)
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.CablePairNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        //public static void GetBuchtByCablePairID(long cablePairID, out Bucht bucht, out  int verticalMDFColumnID, out int verticalMDFRowID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        bucht = context.Buchts.Where(t => t.CablePairID == cablePairID).SingleOrDefault();
        //        verticalMDFColumnID =context.Buchts.Where(t => t.CablePairID == cablePairID).Select(t => t.VerticalMDFRow.VerticalMDFColumn.ID).SingleOrDefault();
        //        verticalMDFRowID = context.Buchts.Where(t => t.CablePairID == cablePairID).Select(t => t.VerticalMDFRow.ID).SingleOrDefault();
        //    }
        //}


        /// <summary>
        /// Because ID may be omitted for get to used the CabinetInput range of numbers  
        /// </summary>
        public static List<CablePair> GetCablePairFromIDToToID(long fromID, long ToID, bool FreeCablePair = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<CablePair> temp = context.CablePairs.Where(t =>
                                                                          t.CableID == context.CablePairs.Where(cp => cp.ID == fromID).SingleOrDefault().CableID &&
                                                                          t.CablePairNumber >= context.CablePairs.Where(cp => cp.ID == fromID).SingleOrDefault().CablePairNumber &&
                                                                          t.CablePairNumber <= context.CablePairs.Where(cp => cp.ID == ToID).SingleOrDefault().CablePairNumber)
                                                                .OrderBy(t => t.CablePairNumber);
                if (FreeCablePair) temp.Where(t => t.Status == (byte)DB.CablePairStatus.Free);
                return temp.ToList();
            }
        }
        public static List<CablePair> GetCablePairFromIDToToIDFreeOfBucht(long fromID, long ToID, bool FreeCablePair = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<CablePair> temp = context.CablePairs.Where(t => t.ID >= fromID && t.ID <= ToID).OrderBy(t => t.CablePairNumber);
                if (FreeCablePair) temp = temp.Where(t => context.Buchts.Select(t2 => t2.CablePairID).Contains(t.ID) && t.CabinetInputID == null);
                return temp.ToList();
            }
        }

        public static List<GroupingCablePair> GetGroupingCablePair(List<int> cites, List<int> centers, List<long> cables)
        {



            using (MainDataContext context = new MainDataContext())
            {
                // WHERE [t2].CenterID IN (22) AND  [t2].ID IN (22) AND ([t4].[CityID] IN (21)) 
                string whereString = string.Empty;
                if (centers.Count == 0)
                {
                    whereString = "WHERE [t2].CenterID IN (" + string.Join(",", DB.CurrentUser.CenterIDs) + ")";
                }
                else
                {
                    whereString = "WHERE [t2].CenterID IN (" + string.Join(",", centers) + ")";
                }

                if (cables.Count != 0)
                {
                    whereString = whereString + " AND  [t2].ID IN (" + string.Join(",", cables) + ")";
                }
                if (cites.Count != 0)
                {
                    whereString = whereString + " AND  [t4].[CityID] IN (" + string.Join(",", cites) + ")";
                }



                string queryString = @"DECLARE  @table1 TABLE(id int ,  CablePairNumber int,CableID int,MDFID int,VerticalCloumnID int,VerticalRowID int)
                                                DECLARE  @table2 TABLE(id int ,  CablePairNumber int,CableID int , groupid int)
                                                DECLARE  @firstrows TABLE(id int ,  groupid int)
                                                INSERT @table1 SELECT t1.ID , t1.CablePairNumber,CableID,t8.ID,t6.ID,t5.ID
                                                FROM [dbo].[Bucht] AS [t0]    LEFT OUTER JOIN [dbo].[CablePair] AS [t1] ON [t1].[ID] = [t0].[CablePairID]   
                                                 LEFT OUTER JOIN [dbo].[Cable] AS [t2] ON [t2].[ID] = [t1].[CableID]    
                                                 LEFT OUTER JOIN [dbo].[Center] AS [t3] ON [t3].[ID] = [t2].[CenterID]   
                                                 LEFT OUTER JOIN [dbo].[Region] AS [t4] ON [t4].[ID] = [t3].[RegionID]    
                                                 INNER JOIN [dbo].[VerticalMDFRow] AS [t5] ON [t5].[ID] = [t0].[MDFRowID]    
                                                 INNER JOIN [dbo].[VerticalMDFColumn] AS [t6] ON [t6].[ID] = [t5].[VerticalMDFColumnID]    
                                                 INNER JOIN [dbo].[MDFFrame] AS [t7] ON [t7].[ID] = [t6].[MDFFrameID]   
                                                 INNER JOIN [dbo].[MDF] AS [t8] ON [t8].[ID] = [t7].[MDFID]  "
                                                 +
                                                 whereString
                                                    + @" INSERT @firstrows
                                                    SELECT id, ROW_NUMBER() OVER (ORDER BY id) groupid
                                                    FROM @table1 a
                                                    WHERE id - 1 NOT IN (SELECT b.id FROM @table1 b where b.MDFID = a.MDFID and b.VerticalCloumnID = a.VerticalCloumnID and b.VerticalRowID = a.VerticalRowID)

                                                INSERT @table2 SELECT id,CablePairNumber,CableID ,(SELECT MAX(b.groupid)
                                                           FROM @firstrows b
                                                           WHERE b.id <= a.id) groupid
                                                FROM @table1 a


                                                SELECT ROW_NUMBER() OVER (ORDER BY MIN([t1].[CablePairNumber])) ID,t2.CableNumber as CableNumber,[t1].[CableID] AS [CableID] , MIN([t1].[CablePairNumber]) AS [FromCablePairNumber], MAX([t1].[CablePairNumber]) AS [ToCablePairNumber], MIN([t0].[BuchtNo]) AS [FromBuchtNo], MAX([t0].[BuchtNo]) AS [ToBuchtNo],CASE WHEN Description IS NOT NULL THEN CONVERT(varchar , [t8].[Number] ) + '( ' + [t8].[Description] +' )'  ELSE CONVERT(varchar ,[t8].[Number]) END AS [MDF] , [t8].[ID] AS [MDFID],CONVERT(VARCHAR,[t6].[VerticalCloumnNo]) AS VerticalCloumnNo,[t6].[ID] AS [VerticalCloumnID], CONVERT(VARCHAR,[t5].[VerticalRowNo]) AS VerticalRowNo,[t5].[ID] AS [VerticalRowID], [t2].[CableNumber],[t1].[CableID] AS [CableID]
                                                FROM [dbo].[Bucht] AS [t0]  
                                                  LEFT OUTER JOIN @table2 AS [t1] ON [t1].[ID] = [t0].[CablePairID]   
                                                  LEFT OUTER JOIN [dbo].[Cable] AS [t2] ON [t2].[ID] = [t1].[CableID]    
                                                  LEFT OUTER JOIN [dbo].[Center] AS [t3] ON [t3].[ID] = [t2].[CenterID]   
                                                  LEFT OUTER JOIN [dbo].[Region] AS [t4] ON [t4].[ID] = [t3].[RegionID]    
                                                  INNER JOIN [dbo].[VerticalMDFRow] AS [t5] ON [t5].[ID] = [t0].[MDFRowID]    
                                                  INNER JOIN [dbo].[VerticalMDFColumn] AS [t6] ON [t6].[ID] = [t5].[VerticalMDFColumnID]    
                                                  INNER JOIN [dbo].[MDFFrame] AS [t7] ON [t7].[ID] = [t6].[MDFFrameID]   
                                                  INNER JOIN [dbo].[MDF] AS [t8] ON [t8].[ID] = [t7].[MDFID] "
                                                  +
                                                  whereString
                                                  + @"  GROUP BY [t8].[ID], [t8].[Number],[t8].[Description], [t6].[ID], [t6].[VerticalCloumnNo], [t5].[ID], [t5].[VerticalRowNo], [t1].[CableID], [t2].[CableNumber] , t1.groupid";

                var temp = context.ExecuteQuery<GroupingCablePair>(queryString);

                return temp.ToList();
            }

            //using (MainDataContext context = new MainDataContext())
            //{
            //    var temp = context
            //        .Buchts
            //        .Where(t => cites.Count == 0 || cites.Contains(t.CablePair.Cable.Center.Region.CityID))
            //        .Where(t => centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CablePair.Cable.CenterID) : centers.Contains(t.CablePair.Cable.CenterID))
            //        .Where(t => cables.Count == 0 || cables.Contains(t.CablePair.CableID))
            //        .GroupBy(t => new
            //        {
            //            MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
            //            MDF = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number,
            //            VerticalCloumnID = t.VerticalMDFRow.VerticalMDFColumn.ID,
            //            VerticalCloumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
            //            VerticalRowID = t.VerticalMDFRow.ID,
            //            VerticalRowNo = t.VerticalMDFRow.VerticalRowNo,
            //            CableID = t.CablePair.CableID,
            //            CableNo = t.CablePair.Cable.CableNumber,
            //            time = t.CablePair.InsertDate,

            //        })
            //        .Select(g => new GroupingCablePair
            //        {

            //            MDF = g.Key.MDF,
            //            CableNumber = g.Key.CableNo,
            //            FromCablePair = g.Min(t => t.CablePair.CablePairNumber),
            //            ToCablePair = g.Max(t => t.CablePair.CablePairNumber),
            //            Column = g.Key.VerticalCloumnNo,
            //            Row = g.Key.VerticalRowNo,
            //            FromConnection = g.Min(t => t.BuchtNo),
            //            ToConnection = g.Max(t => t.BuchtNo)

            //        });
            //    return temp.ToList();
            //}
        }

        public static List<CheckableItem> GetCalePairConnectedToBuchtByCableID(long cableID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs
                    .Where(t => context.Buchts.Where(t2 => t2.CablePair.CableID == cableID).Select(t2 => t2.CablePairID).Contains(t.ID) && t.CabinetInputID == null).OrderBy(t => t.CablePairNumber)
                    .Select(t => new CheckableItem { LongID = t.ID, Name = t.CablePairNumber.ToString(), IsChecked = false }).ToList();
            }
        }





        public static List<CablePair> GetCablePairFromCabinetInputIDToCabinetInputID(long fromCabinetInputID, long toCabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs.Where(t => t.CabinetInputID >= fromCabinetInputID && t.CabinetInputID <= toCabinetInputID).OrderBy(t => t.CablePairNumber).ToList();
            }
        }

        public static int? GetMaxCablePairNumber(long cableID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs.Where(t => t.CableID == cableID).Select(t => (int?)t.CablePairNumber).Max();
            }
        }

        public static List<CablePair> GetCablePairsIDs(List<long> cablePairIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs.Where(t => cablePairIDs.Contains(t.ID)).ToList();
            }
        }


        public static List<CablePair> GetCablePairByCabinetInputs(List<long> cabinetInputIds)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CablePairs.Where(t => cabinetInputIds.Contains((long)t.CabinetInputID)).ToList();
            }
        }

        public static List<CablePair> GetCablePairByCablePairIDToCablePairID(long fromCablePairID, long toCablePairID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<CablePair> temp = context.CablePairs.Where(t =>
                                                                               t.CableID == context.CablePairs.Where(ci => ci.ID == fromCablePairID).SingleOrDefault().CableID &&
                                                                               t.CablePairNumber >= context.CablePairs.Where(ci => ci.ID == fromCablePairID).SingleOrDefault().CablePairNumber &&
                                                                               t.CablePairNumber <= context.CablePairs.Where(ci => ci.ID == toCablePairID).SingleOrDefault().CablePairNumber
                                                                            );


                return temp.OrderBy(t => t.CablePairNumber).ToList();
            }
        }
    }
}