using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public class CabinetInputDB
    {

        public static List<CabinetInputInfo> CabinetInputSearch(
            List<int> city,
            List<int> center,
            List<int> cabinets,
            int fromCabinetInput,
            int toCabinetInput,
            List<int> PCMType,
            int startRowIndex,
            int pageSize,
            List<int> cabinetInputStatus,
            List<int> buchtStatus,
            out int count,
            long telephoneNo,
            long requestID
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<CabinetInputInfo> query = context.CabinetInputs
                      .GroupJoin(context.Buchts, ci => ci.ID, b => b.CabinetInputID, (ci, b) => new { CabinetInputGroup = ci, BuchtGroup = b })
                      .SelectMany(t => t.BuchtGroup.DefaultIfEmpty(), (ci, t) => new { CabinetInput = ci.CabinetInputGroup, Bucht = t })
                      .GroupJoin(context.ADSLPAPPorts, b3 => b3.Bucht.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { ADSLPAPPort = p, CabinetInput = b3.CabinetInput, Bucht = b3.Bucht })
                      .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { ADSLPAPPort = p1, CabinetInput = bp.CabinetInput, Bucht = bp.Bucht })
                      .GroupJoin(context.ViewReservBuchts, a => a.Bucht.ID, rv => rv.BuchtID, (a, rv) => new { ADSLPAPPort = a.ADSLPAPPort, CabinetInput = a.CabinetInput, Bucht = a.Bucht, ViewReservBucht = rv })
                      .SelectMany(a => a.ViewReservBucht.DefaultIfEmpty(), (a, rv) => new { ADSLPAPPort = a.ADSLPAPPort, CabinetInput = a.CabinetInput, Bucht = a.Bucht, ViewReservBucht = rv })
                      .GroupJoin(context.Malfuctions, a => a.CabinetInput.ID, rv => rv.CabinetInputID, (a, m) => new { ADSLPAPPort = a.ADSLPAPPort, CabinetInput = a.CabinetInput, Bucht = a.Bucht, ViewReservBucht = a.ViewReservBucht, malfuction = m.OrderByDescending(tm => tm.ID).Take(1).SingleOrDefault() })
                   .Where(t =>
                       (city.Count == 0 || city.Contains((int)t.Bucht.CityID))
                    && (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains((int)t.Bucht.CenterID) : center.Contains((int)t.Bucht.CenterID))
                    && (cabinets.Count == 0 || cabinets.Contains(t.CabinetInput.CabinetID))
                    && (cabinetInputStatus.Count == 0 || cabinetInputStatus.Contains(t.CabinetInput.Status))
                    && (fromCabinetInput == -1 || t.CabinetInput.InputNumber >= fromCabinetInput)
                    && (toCabinetInput == -1 || t.CabinetInput.InputNumber <= toCabinetInput)
                    && (PCMType.Count == 0 || PCMType.Contains(t.Bucht.PCMPort.PCM.PCMTypeID))
                    && (buchtStatus.Count == 0 || buchtStatus.Contains(t.Bucht.Status))
                    && (telephoneNo == -1 || t.Bucht.TelephoneNo == telephoneNo)
                    && (requestID == -1 || t.ViewReservBucht.RequestID == requestID)
                     )
                   .Select(t => new CabinetInputInfo
                   {
                       ID = t.CabinetInput.ID,
                       CabinetID = t.CabinetInput.CabinetID,
                       CabinetNumber = t.CabinetInput.Cabinet.CabinetNumber,
                       InputNumber = t.CabinetInput.InputNumber,
                       InsertDate = t.CabinetInput.InsertDate,
                       Status = t.CabinetInput.Status,
                       Direction = t.CabinetInput.Direction,
                       RequestID = t.ViewReservBucht.RequestID,
                       DirectionName = t.CabinetInput.CabinetInputDirection.Name,
                       StatusName = t.CabinetInput.CabinetInputStatus.Name,
                       Cable = t.Bucht.CablePair.CablePairNumber + " / " + t.Bucht.CablePair.Cable.CableNumber,
                       ColumnNo = t.Bucht.ColumnNo,
                       RowNo = t.Bucht.RowNo,
                       BuchtNo = t.Bucht.BuchtNo,
                       TelephoneNo = t.Bucht.TelephoneNo,
                       //DateMalfunction = t.CabinetInput.Status == (byte)DB.CabinetInputStatus.Malfuction ? (context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault() != null ? context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().DateMalfunction.ToPersian(Date.DateStringType.Short) : string.Empty) : "---",
                       //TimeMalfunction = t.CabinetInput.Status == (byte)DB.CabinetInputStatus.Malfuction ? (context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault() != null ? context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().TimeMalfunction : string.Empty) : "---",
                       //Description = t.CabinetInput.Status == (byte)DB.CabinetInputStatus.Malfuction ? (context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault() != null ? context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().Description : string.Empty) : "---",
                       //TypeMalfunction = t.CabinetInput.Status == (byte)DB.CabinetInputStatus.Malfuction ? (context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault() != null ? Helpers.GetEnumDescription(context.Malfuctions.Where(m => m.CabinetInputID == t.CabinetInput.ID).OrderByDescending(m => m.ID).Take(1).SingleOrDefault().TypeMalfunction, typeof(DB.CabinetInputMalfuctionType)) : string.Empty) : "---",
                       DateMalfunction = t.malfuction != null ? t.malfuction.DateMalfunction.ToPersian(Date.DateStringType.Short) : string.Empty,
                       TimeMalfunction = t.malfuction != null ? t.malfuction.TimeMalfunction : string.Empty,
                       Description = t.malfuction != null ? t.malfuction.Description : string.Empty,
                       TypeMalfunction = t.malfuction != null ? Helpers.GetEnumDescription(t.malfuction.TypeMalfunction, typeof(DB.CabinetInputMalfuctionType)) : string.Empty,


                       BuchtStatus = Helpers.GetEnumDescription(t.Bucht.Status, typeof(DB.BuchtStatus)),
                   });
                count = query.Count();

                return query.Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int CabinetInputCount(
            List<int> city,
            List<int> center,
            List<int> cabinets,
            int fromCabinetInput,
            int toCabinetInput,
            List<int> cabinetInputStatus,
                         List<int> PCMType
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context
                   .CabinetInputs
                   .GroupJoin(context.Buchts, ci => ci.ID, b => b.CabinetInputID, (ci, b) => new { CabinetInputGroup = ci, BuchtGroup = b })
                   .SelectMany(t => t.BuchtGroup.DefaultIfEmpty(), (ci, t) => new { CabinetInput = ci.CabinetInputGroup, Bucht = t })
                   .GroupJoin(context.Telephones, b => b.Bucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { buchtTelephoneGrpup = b, telephoneGroup = t })
                   .SelectMany(t => t.telephoneGroup.DefaultIfEmpty(), (t, b) => new { CabinetInput = t.buchtTelephoneGrpup.CabinetInput, Bucht = t.buchtTelephoneGrpup.Bucht, Telephone = b })
                   .Where(t => center.Contains(t.CabinetInput.Cabinet.CenterID)
                    && (cabinets.Count == 0 || cabinets.Contains(t.CabinetInput.Cabinet.ID))
                    && (cabinetInputStatus.Count == 0 || cabinetInputStatus.Contains(t.CabinetInput.Status))
                    && (fromCabinetInput == -1 || t.CabinetInput.InputNumber >= fromCabinetInput)
                    && (toCabinetInput == -1 || t.CabinetInput.InputNumber <= toCabinetInput)
                    && (PCMType.Count == 0 || PCMType.Contains(t.Bucht.PCMPort.PCM.PCMTypeID))).Count();
            }
        }
        //public static int CabinetInputCount(
        // List<int> city,
        // List<int> center,
        // List<int> cabinets,
        // int fromCabinetInput,
        // int toCabinetInput,
        // List<int> cabinetInputStatus,
        // )
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context
        //            .CabinetInputs
        //            .Where(t =>
        //            (city.Count == 0 || city.Contains(t.Cabinet.Center.Region.CityID)) &&
        //            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : center.Contains(t.Cabinet.CenterID)) &&
        //            (cabinetInputStatus.Count == 0 || cabinetInputStatus.Contains(t.Status)) &&
        //             (cabinets.Count == 0 || cabinets.Contains(t.Cabinet.ID)) &&
        //            (fromCabinetInput == -1 ? true : t.InputNumber >= fromCabinetInput) &&
        //            (toCabinetInput == -1 ? true : t.InputNumber <= toCabinetInput)
        //            ).Count();



        //    }
        //}


        public static CabinetInput GetCabinetInputByID(long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.ID == cabinetInputID).SingleOrDefault();
            }
        }
        public static List<CabinetInput> GetCabinetInput()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.ToList();
            }
        }

        public static List<CheckableItem> GetCabinetInputByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //milad doran
                //return context.CabinetInputs.Where(t => t.CabinetID == cabinetID).OrderBy(t => t.InputNumber).Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = false }).ToList();

                //TODO:rad 13941212
                List<CheckableItem> result = new List<CheckableItem>();
                result = context.CabinetInputs
                                .Where(t => t.CabinetID == cabinetID)
                                .OrderBy(t => t.InputNumber)
                                .Select(t => new CheckableItem
                                                {
                                                    LongID = t.ID,
                                                    Name = t.InputNumber.ToString(),
                                                    IsChecked = false
                                                }
                                        )
                                .ToList();
                return result;
            }
        }


        public static List<CheckableItem> GetCabinetInputChechable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.Status == (byte)DB.CabinetInputStatus.healthy).OrderBy(t => t.InputNumber).Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = false }).ToList();
            }
        }
        public static List<CheckableItem> GetCabinetInputChechableByID(long InputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.ID == InputID).Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = false }).ToList();
            }
        }
        public static List<CheckableItem> GetCabinetInputChechableByID(List<long> InputIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => InputIDs.Contains(t.ID)).OrderBy(t => t.InputNumber).Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetCabinetInputWithBuchtsByCabinetIDCheckable(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs
                               .Where(ci => ci.Cabinet.ID == cabinetID && ci.Status == (byte)DB.CabinetInputStatus.healthy)
                               .GroupJoin(context.Buchts, ci => ci.ID, b => b.CabinetInputID, (ci, b) => new { cabinetInput = ci, buchtJoin = b })
                               .SelectMany(t => t.buchtJoin.DefaultIfEmpty(), (t1, t2) => new { t1 = t1.cabinetInput, bucht = t2 })
                               .Where(t => t.t1.Buchts.Count() == 0 || t.t1.Buchts.Count() == 1 && t.t1.Buchts.SingleOrDefault().Status == (int)DB.BuchtStatus.Free)
                               .Select(t => new CheckableItem { LongID = t.t1.ID, Name = t.t1.InputNumber.ToString(), IsChecked = false })
                               .Distinct()
                               .ToList();

            }
        }

        public static List<CabinetInputInfoWithBuchts> GetCabinetInputWithBuchtsByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                var query = context.CabinetInputs
                               .Where(ci => ci.CabinetID == cabinetID && ci.Status == (byte)DB.CabinetInputStatus.healthy)
                               .Join(context.Buchts, ci => ci.ID, b => b.CabinetInputID, (ci, b) => new { cabinetInput = ci, buchtJoin = b })
                              .Select(t => new CabinetInputInfoWithBuchts { CabinetInput = t.cabinetInput, Bucht = t.buchtJoin }).ToList();
                return query;
            }
        }

        public static List<CabinetInputInfoWithBuchts> GetAllCabinetInputWithBuchtsByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                var query = context.CabinetInputs
                               .Where(ci => ci.CabinetID == cabinetID && ci.Status == (byte)DB.CabinetInputStatus.healthy)
                               .GroupJoin(context.Buchts, ci => ci.ID, b => b.CabinetInputID, (ci, b) => new { cabinetInput = ci, buchtJoin = b })
                               .SelectMany(t => t.buchtJoin.DefaultIfEmpty(), (cabinetInput, bucht) => new { cabinetInput = cabinetInput.cabinetInput, bucht = bucht })
                               .Select(t => new CabinetInputInfoWithBuchts { CabinetInput = t.cabinetInput, Bucht = t.bucht }).ToList();
                return query;
            }
        }


        public static List<CabinetInput> GetFromCabinetInputToCabinetID(long fromCabinetInputID, long toCabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.ID >= fromCabinetInputID && t.ID <= toCabinetInputID).ToList();
            }
        }

        public static List<CheckableItem> GetCabinetInputFreeByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.CabinetInputs
                    .Where(t => t.CabinetID == cabinetID
                    && !context.Buchts.Where(t3 => t3.CabinetInput.CabinetID == cabinetID).Select(t2 => t2.CabinetInputID).Contains(t.ID))
                    .OrderBy(t => t.InputNumber)
                    .Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = true }).ToList();

            }
        }
        public static List<CheckableItem> GetCabinetInputConnectedByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.Buchts.Where(t => t.CabinetInput.CabinetID == cabinetID).Select(t => new CheckableItem { LongID = t.CabinetInput.ID, Name = t.CabinetInput.InputNumber.ToString(), IsChecked = true }).ToList();
                //return context.CabinetInputs
                //    .Where(t => t.CabinetID == cabinetID)
                //    .Where(t => context.Buchts.Where(t3 => t3.CabinetInput.CabinetID == cabinetID).Select(t2 => t2.CabinetInputID).Contains(t.ID)).OrderBy(t => t.InputNumber)
                //    .Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = true }).ToList();

            }
        }
        public static List<CheckableItem> GetCheckableCabinetInputByCabinetIDs(List<int> cabinetIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => cabinetIDs.Contains(t.CabinetID)).OrderBy(t => t.InputNumber).Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = false }).ToList();
            }
        }
        public static List<CabinetInput> GetCabinetInputByCabinetIDs(List<int> cabinetIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => cabinetIDs.Contains(t.CabinetID)).OrderBy(t => t.InputNumber).ToList();
            }
        }
        public static List<CheckableItem> GetCabinetInputCheckableItemByCabinetIDs(List<int> cabinetIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => cabinetIDs.Contains(t.CabinetID))
                                            .OrderBy(t => t.InputNumber)
                                            .Select(t => new CheckableItem
                                            {
                                                LongID = t.ID,
                                                Name = t.InputNumber.ToString(),
                                                IsChecked = false
                                            }).ToList();
            }
        }
        public static List<ID> GetCabinetIDByCabinetIDs(List<int> cabinetIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => cabinetIDs.Contains(t.CabinetID)).Select(t => new ID { _ID = t.CabinetID }).ToList();
            }
        }
        /// <summary>
        /// Because ID may be omitted for get to used the CabinetInput range of numbers  
        /// </summary>
        public static List<CabinetInput> GetCabinetInputFromIDToID(long fromCabinetInputID, long toCabinetInputID, bool freeCabinetInput = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<CabinetInput> temp = context.CabinetInputs.Where(t =>
                                                                               t.CabinetID == context.CabinetInputs.Where(ci => ci.ID == fromCabinetInputID).SingleOrDefault().CabinetID &&
                                                                               t.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == fromCabinetInputID).SingleOrDefault().InputNumber &&
                                                                               t.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == toCabinetInputID).SingleOrDefault().InputNumber
                                                                            );
                if (freeCabinetInput) temp = temp.Where(t => !context.Buchts.Select(t2 => t2.CabinetInputID).Contains(t.ID));
                return temp.OrderBy(t => t.InputNumber).ToList();
            }
        }


        public static List<GroupingCabinetInput> GetGroupingCabinetInput(List<int> cities, List<int> centers, List<int> cabinets)
        {

            using (MainDataContext context = new MainDataContext())
            {
                //WHERE (t5.ID IN (1102)) AND (t3.ID IN (22)) AND (t4.CityID IN (21))
                string whereString = string.Empty;
                if (centers.Count == 0)
                {
                    whereString = "WHERE t3.ID IN (" + string.Join(",", DB.CurrentUser.CenterIDs) + ")";
                }
                else
                {
                    whereString = "WHERE t3.ID IN (" + string.Join(",", centers) + ")";
                }

                if (cabinets.Count != 0)
                {
                    whereString = whereString + " AND  [t5].ID IN (" + string.Join(",", cabinets) + ")";
                }
                if (cities.Count != 0)
                {
                    whereString = whereString + " AND  t4.CityID IN (" + string.Join(",", cities) + ")";
                }

                if (cities.Count != 0)
                {
                    whereString = whereString + " AND  t4.CityID IN (" + string.Join(",", cities) + ")";
                }



                string queryString = @" DECLARE  @table1 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , MDFID int , VerticalCloumnID int , VerticalRowID int)
                                        DECLARE  @table2 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , groupid int)
                                        DECLARE  @firstrows TABLE(id int ,  groupid int)
                                        INSERT @table1 SELECT t6.ID ,t1.ID , t1.InputNumber,t1.CabinetID , t11.ID , t9.ID , t8.ID
                                        FROM     dbo.VerticalMDFColumn AS t9 INNER JOIN
                                                                 dbo.VerticalMDFRow AS t8 ON t9.ID = t8.VerticalMDFColumnID INNER JOIN
                                                                 dbo.MDFFrame AS t10 ON t10.ID = t9.MDFFrameID INNER JOIN
                                                                 dbo.MDF AS t11 ON t11.ID = t10.MDFID INNER JOIN
                                                                 dbo.Cabinet AS t5 INNER JOIN
                                                                 dbo.CabinetInput AS t1 ON t5.ID = t1.CabinetID INNER JOIN
                                                                 dbo.Bucht AS t2 ON t1.ID = t2.CabinetInputID INNER JOIN
                                                                 dbo.Center AS t3 ON t5.CenterID = t3.ID INNER JOIN
                                                                 dbo.Region AS t4 ON t3.RegionID = t4.ID INNER JOIN
                                                                 dbo.CablePair AS t6 ON t2.CablePairID = t6.ID INNER JOIN
                                                                 dbo.Cable AS t7 ON t6.CableID = t7.ID ON t8.ID = t2.MDFRowID "
                                                 +
                                                 whereString
                                                 + @" INSERT @firstrows
                                                                    SELECT id, ROW_NUMBER() OVER (ORDER BY id) groupid
                                                                    FROM @table1 a
                                                                    WHERE id - 1 NOT IN (SELECT b.id FROM @table1 b WHERE b.MDFID = a.MDFID and b.VerticalCloumnID = a.VerticalCloumnID and b.VerticalRowID = a.VerticalRowID)

                                                                INSERT @table2 SELECT id , CabinetInputID , InputNumber,CabinetID ,(SELECT MAX(b.groupid)
                                                                           FROM @firstrows b
                                                                           WHERE b.id <= a.id) groupid
                                                                FROM @table1 a

                                                                SELECT         ROW_NUMBER() OVER (ORDER BY MIN(t1.InputNumber)) AS ID,t5.ID AS CabinetID,t5.CabinetNumber as CabinetNumber, t7.ID as CableID,CASE WHEN Description IS NOT NULL THEN CONVERT(varchar , [t11].[Number] ) + '( ' + [t11].[Description] +' )'  ELSE CONVERT(varchar ,[t11].[Number]) END AS [MDF] , MIN(t1.InputNumber) AS FromInputNumber, MAX(t1.InputNumber) AS ToInputNumber,  t9.VerticalCloumnNo as VerticalCloumnNo ,t8.VerticalRowNo as VerticalRowNo, MIN(t2.BuchtNo) AS FromBuchtNo, MAX(t2.BuchtNo) AS ToBuchtNo,CONVERT(varchar, t7.CableNumber) as CableNumber , MIN(t6.CablePairNumber) AS FromCablePairNumber, MAX(t6.CablePairNumber) AS ToCablePairNumber
                                                                FROM            dbo.VerticalMDFColumn AS t9 INNER JOIN
                                                                                         dbo.VerticalMDFRow AS t8 ON t9.ID = t8.VerticalMDFColumnID INNER JOIN
                                                                                         dbo.MDFFrame AS t10 ON t10.ID = t9.MDFFrameID INNER JOIN
                                                                                         dbo.MDF AS t11 ON t11.ID = t10.MDFID INNER JOIN
                                                                                         dbo.Cabinet AS t5 INNER JOIN
                                                                                         @table2 AS t1 ON t5.ID = t1.CabinetID INNER JOIN
                                                                                         dbo.Bucht AS t2 ON t1.CabinetInputID = t2.CabinetInputID INNER JOIN
                                                                                         dbo.Center AS t3 ON t5.CenterID = t3.ID INNER JOIN
                                                                                         dbo.Region AS t4 ON t3.RegionID = t4.ID INNER JOIN
                                                                                         dbo.CablePair AS t6 ON t2.CablePairID = t6.ID INNER JOIN
                                                                                         dbo.Cable AS t7 ON t6.CableID = t7.ID ON t8.ID = t2.MDFRowID "
                                                  +
                                                    whereString
                                                  + @" GROUP BY t5.ID,t5.CabinetNumber, [t11].[Number],[t11].[Description] , t7.CableNumber, t7.ID, t8.VerticalRowNo, t9.VerticalCloumnNo ,t1.groupid ";

                //if(centers.Count == 0 &&  cites.Count == 0) 
                //{
                //    centers = DB.CurrentUser.CenterIDs;

                //}
                //else if(centers.Count == 0 &&  cites.Count != 0)
                //{
                //    centers = DB.
                //}                 
                //if (cables.Count == 0) cables = context.Cables.Where(c => centers.Contains(c.CenterID)).Select(c => c.ID).ToList();

                var temp = context.ExecuteQuery<GroupingCabinetInput>(queryString).ToList();

                return temp.ToList();


                //TODO:rad 13950220
                //چون دستور بالا ناخونا بود آن را از پروفایلر دیدم و مرتب کردم
                //DECLARE  @table1 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , MDFID int , VerticalCloumnID int , VerticalRowID int)
                //
                //DECLARE  @table2 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , groupid int)
                //
                //DECLARE  @firstrows TABLE(id int ,  groupid int)
                //
                //INSERT @table1 
                //SELECT 
                //	cb.ID CableID,
                //	ci.ID CabinetInputID, 
                //	ci.InputNumber ,
                //	ci.CabinetID , 
                //	m.ID MdfId, 
                //	vc.ID ColumnId, 
                //	vr.ID RowId
                //FROM     
                //	dbo.VerticalMDFColumn AS vc 
                //INNER JOIN
                //    dbo.VerticalMDFRow AS vr ON vc.ID = vr.VerticalMDFColumnID 
                //INNER JOIN
                //    dbo.MDFFrame AS mf ON mf.ID = vc.MDFFrameID 
                //INNER JOIN
                //    dbo.MDF AS m ON m.ID = mf.MDFID 
                //INNER JOIN
                //    dbo.Cabinet AS cab 
                //INNER JOIN
                //    dbo.CabinetInput AS ci ON cab.ID = ci.CabinetID 
                //INNER JOIN
                //    dbo.Bucht AS bu ON ci.ID = bu.CabinetInputID 
                //INNER JOIN
                //    dbo.Center AS ce ON cab.CenterID = ce.ID 
                //INNER JOIN
                //    dbo.Region AS reg ON ce.RegionID = reg.ID 
                //INNER JOIN
                //    dbo.CablePair AS cp ON bu.CablePairID = cp.ID 
                //INNER JOIN
                //    dbo.Cable AS cb ON cp.CableID = cb.ID ON vr.ID = bu.MDFRowID 
                //WHERE 
                //	ce.ID IN (8) 
                //	AND  
                //	cab.ID IN (2611) 
                //	AND  
                //	reg.CityID IN (1) 
                //	AND  
                //	reg.CityID IN (1) 
                //
                //
                //INSERT @firstrows
                //SELECT 
                //	id, 
                //	ROW_NUMBER() OVER (ORDER BY id) groupid
                //FROM 
                //	@table1 a
                //WHERE 
                //	id - 1 NOT IN (
                //					SELECT 
                //						b.id 
                //					FROM 
                //						@table1 b 
                //					WHERE 
                //						b.MDFID = a.MDFID 
                //						and 
                //						b.VerticalCloumnID = a.VerticalCloumnID 
                //						and 
                //						b.VerticalRowID = a.VerticalRowID
                //				   )
                //
                //INSERT @table2 
                //SELECT 
                //	id , 
                //	CabinetInputID , 
                //	InputNumber,
                //	CabinetID ,
                //	(
                //		SELECT 
                //			MAX(b.groupid)
                //        FROM 
                //			@firstrows b
                //        WHERE 
                //			b.id <= a.id
                //	) groupid
                //FROM @table1 a
                //
                //SELECT         
                //	ROW_NUMBER() OVER (ORDER BY MIN(t1.InputNumber)) AS ID,
                //	cab.ID AS CabinetID,
                //	cab.CabinetNumber as CabinetNumber, 
                //	cb.ID as CableID,
                //	CASE WHEN Description IS NOT NULL THEN CONVERT(varchar , m.[Number] ) + '( ' + m.[Description] +' )'  ELSE CONVERT(varchar ,m.[Number]) END AS [MDF] , 
                //	MIN(t1.InputNumber) AS FromInputNumber, 
                //	MAX(t1.InputNumber) AS ToInputNumber,  
                //	vc.VerticalCloumnNo as VerticalCloumnNo ,
                //	vr.VerticalRowNo as VerticalRowNo, 
                //	MIN(bu.BuchtNo) AS FromBuchtNo, 
                //	MAX(bu.BuchtNo) AS ToBuchtNo,
                //	CONVERT(varchar, cb.CableNumber) as CableNumber , 
                //	MIN(cp.CablePairNumber) AS FromCablePairNumber, 
                //	MAX(cp.CablePairNumber) AS ToCablePairNumber
                //FROM
                //	dbo.VerticalMDFColumn AS vc 
                //INNER JOIN
                //	dbo.VerticalMDFRow AS vr ON vc.ID = vr.VerticalMDFColumnID 
                //INNER JOIN                         
                //	dbo.MDFFrame AS mf ON mf.ID = vc.MDFFrameID 
                //INNER JOIN
                //	dbo.MDF AS m ON m.ID = mf.MDFID 
                //INNER JOIN
                //	dbo.Cabinet AS cab
                //INNER JOIN
                //	@table2 AS t1 ON cab.ID = t1.CabinetID 
                //INNER JOIN
                //	dbo.Bucht AS bu ON t1.CabinetInputID = bu.CabinetInputID 
                //INNER JOIN
                //	dbo.Center AS ce ON cab.CenterID = ce.ID 
                //INNER JOIN
                //	dbo.Region AS reg ON ce.RegionID = reg.ID
                //INNER JOIN
                //	dbo.CablePair AS cp ON bu.CablePairID = cp.ID 
                //INNER JOIN
                //	dbo.Cable AS cb ON cp.CableID = cb.ID ON vr.ID = bu.MDFRowID 
                //WHERE 
                //	ce.ID IN (8) 
                //	AND  
                //	cab.ID IN (2611) 
                //	AND  
                //	reg.CityID IN (1) 
                //	AND  
                //	reg.CityID IN (1) 
                //GROUP BY 
                //	cab.ID,
                //	cab.CabinetNumber, 
                //	m.[Number],
                //	m.[Description] , 
                //	cb.CableNumber, 
                //	cb.ID, 
                //	vr.VerticalRowNo, 
                //	vc.VerticalCloumnNo ,
                //	t1.groupid 
            }
        }

        public static List<GroupingCabinetInput> GetGroupingCableDetallReport(List<int> cities, List<int> centers, List<int> cabinets, int cable)
        {

            using (MainDataContext context = new MainDataContext())
            {
                context.CommandTimeout = 0;
                //WHERE (t5.ID IN (1102)) AND (t3.ID IN (22)) AND (t4.CityID IN (21))
                string whereString = string.Empty;
                if (centers.Count == 0)
                {
                    whereString = "WHERE t3.ID IN (" + string.Join(",", DB.CurrentUser.CenterIDs) + ")";
                }
                else
                {
                    whereString = "WHERE t3.ID IN (" + string.Join(",", centers) + ")";
                }

                if (cabinets.Count != 0)
                {
                    whereString = whereString + " AND  [t5].ID IN (" + string.Join(",", cabinets) + ")";
                }
                if (cities.Count != 0)
                {
                    whereString = whereString + " AND  t4.CityID IN (" + string.Join(",", cities) + ")";
                }

                if (cable != -1)
                {
                    whereString = whereString + " AND  t7.CableNumber IN (" + string.Join(",", cable) + ")";
                }


                string queryString = @" DECLARE  @table1 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , MDFID int , VerticalCloumnID int , VerticalRowID int)
                                        DECLARE  @table2 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , groupid int)
                                        DECLARE  @firstrows TABLE(id int ,  groupid int)
                                        INSERT @table1 SELECT t6.ID ,t1.ID , t1.InputNumber,t1.CabinetID , t11.ID , t9.ID , t8.ID
                                        FROM     dbo.VerticalMDFColumn AS t9 INNER JOIN
                                                                 dbo.VerticalMDFRow AS t8 ON t9.ID = t8.VerticalMDFColumnID INNER JOIN
                                                                 dbo.MDFFrame AS t10 ON t10.ID = t9.MDFFrameID INNER JOIN
                                                                 dbo.MDF AS t11 ON t11.ID = t10.MDFID INNER JOIN
                                                                 dbo.Cabinet AS t5 INNER JOIN
                                                                 dbo.CabinetInput AS t1 ON t5.ID = t1.CabinetID INNER JOIN
                                                                 dbo.Bucht AS t2 ON t1.ID = t2.CabinetInputID INNER JOIN
                                                                 dbo.Center AS t3 ON t5.CenterID = t3.ID INNER JOIN
                                                                 dbo.Region AS t4 ON t3.RegionID = t4.ID INNER JOIN
                                                                 dbo.CablePair AS t6 ON t2.CablePairID = t6.ID INNER JOIN
                                                                 dbo.Cable AS t7 ON t6.CableID = t7.ID ON t8.ID = t2.MDFRowID "
                                                 +
                                                 whereString
                                                 + @" INSERT @firstrows
                                                                    SELECT id, ROW_NUMBER() OVER (ORDER BY id) groupid
                                                                    FROM @table1 a
                                                                    WHERE id - 1 NOT IN (SELECT b.id FROM @table1 b WHERE b.MDFID = a.MDFID and b.VerticalCloumnID = a.VerticalCloumnID and b.VerticalRowID = a.VerticalRowID)

                                                                INSERT @table2 SELECT id , CabinetInputID , InputNumber,CabinetID ,(SELECT MAX(b.groupid)
                                                                           FROM @firstrows b
                                                                           WHERE b.id <= a.id) groupid
                                                                FROM @table1 a

                                                                SELECT         ROW_NUMBER() OVER (ORDER BY MIN(t1.InputNumber)) AS ID,t5.ID AS CabinetID,t5.CabinetNumber as CabinetNumber, t7.ID as CableID,CASE WHEN Description IS NOT NULL THEN CONVERT(varchar , [t11].[Number] ) + '( ' + [t11].[Description] +' )'  ELSE CONVERT(varchar ,[t11].[Number]) END AS [MDF] , MIN(t1.InputNumber) AS FromInputNumber, MAX(t1.InputNumber) AS ToInputNumber,  t9.VerticalCloumnNo as VerticalCloumnNo ,t8.VerticalRowNo as VerticalRowNo, MIN(t2.BuchtNo) AS FromBuchtNo, MAX(t2.BuchtNo) AS ToBuchtNo,CONVERT(varchar, t7.CableNumber) as CableNumber , MIN(t6.CablePairNumber) AS FromCablePairNumber, MAX(t6.CablePairNumber) AS ToCablePairNumber
                                                                FROM            dbo.VerticalMDFColumn AS t9 INNER JOIN
                                                                                         dbo.VerticalMDFRow AS t8 ON t9.ID = t8.VerticalMDFColumnID INNER JOIN
                                                                                         dbo.MDFFrame AS t10 ON t10.ID = t9.MDFFrameID INNER JOIN
                                                                                         dbo.MDF AS t11 ON t11.ID = t10.MDFID INNER JOIN
                                                                                         dbo.Cabinet AS t5 INNER JOIN
                                                                                         @table2 AS t1 ON t5.ID = t1.CabinetID INNER JOIN
                                                                                         dbo.Bucht AS t2 ON t1.CabinetInputID = t2.CabinetInputID INNER JOIN
                                                                                         dbo.Center AS t3 ON t5.CenterID = t3.ID INNER JOIN
                                                                                         dbo.Region AS t4 ON t3.RegionID = t4.ID INNER JOIN
                                                                                         dbo.CablePair AS t6 ON t2.CablePairID = t6.ID INNER JOIN
                                                                                         dbo.Cable AS t7 ON t6.CableID = t7.ID ON t8.ID = t2.MDFRowID "
                                                  +
                                                    whereString
                                                  + @" GROUP BY t5.ID,t5.CabinetNumber, [t11].[Number],[t11].[Description] , t7.CableNumber, t7.ID, t8.VerticalRowNo, t9.VerticalCloumnNo ,t1.groupid ";

                //if(centers.Count == 0 &&  cites.Count == 0) 
                //{
                //    centers = DB.CurrentUser.CenterIDs;

                //}
                //else if(centers.Count == 0 &&  cites.Count != 0)
                //{
                //    centers = DB.
                //}                 
                //if (cables.Count == 0) cables = context.Cables.Where(c => centers.Contains(c.CenterID)).Select(c => c.ID).ToList();

                var temp = context.ExecuteQuery<GroupingCabinetInput>(queryString).ToList();

                return temp.ToList();
            }
        }
        public static List<GroupingCabinetInput> GetGroupingCabinetInputWithBuchtType(List<int> cities, List<int> centers, List<int> cabinets)
        {

            using (MainDataContext context = new MainDataContext())
            {
                //WHERE (t5.ID IN (1102)) AND (t3.ID IN (22)) AND (t4.CityID IN (21))
                string whereString = string.Empty;
                if (centers.Count == 0)
                {
                    whereString = "WHERE t3.ID IN (" + string.Join(",", DB.CurrentUser.CenterIDs) + ")";
                }
                else
                {
                    whereString = "WHERE t3.ID IN (" + string.Join(",", centers) + ")";
                }

                if (cabinets.Count != 0)
                {
                    whereString = whereString + " AND  [t5].ID IN (" + string.Join(",", cabinets) + ")";
                }
                if (cities.Count != 0)
                {
                    whereString = whereString + " AND  t4.CityID IN (" + string.Join(",", cities) + ")";
                }



                string queryString = @" DECLARE  @table1 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , MDFID int , VerticalCloumnID int , VerticalRowID int, BuchtTypeName nvarchar(50))
                                        DECLARE  @table2 TABLE(id int , CabinetInputID bigint , InputNumber int,CabinetID int , groupid int,BuchtTypeName nvarchar(50))
                                        DECLARE  @firstrows TABLE(id int ,  groupid int)

                                        INSERT @table1 SELECT t6.ID ,t1.ID , t1.InputNumber,t1.CabinetID , t11.ID , t9.ID , t8.ID, t12.BuchtTypeName
                                        FROM     dbo.VerticalMDFColumn AS t9 INNER JOIN
                                                                 dbo.VerticalMDFRow AS t8 ON t9.ID = t8.VerticalMDFColumnID INNER JOIN
                                                                 dbo.MDFFrame AS t10 ON t10.ID = t9.MDFFrameID INNER JOIN
                                                                 dbo.MDF AS t11 ON t11.ID = t10.MDFID INNER JOIN
                                                                 dbo.Cabinet AS t5 INNER JOIN
                                                                 dbo.CabinetInput AS t1 ON t5.ID = t1.CabinetID INNER JOIN
                                                                 dbo.Bucht AS t2 ON t1.ID = t2.CabinetInputID INNER JOIN
																 dbo.BuchtType as t12 on t2.BuchtTypeID = t12.ID INNER JOIN
                                                                 dbo.Center AS t3 ON t5.CenterID = t3.ID INNER JOIN
                                                                 dbo.Region AS t4 ON t3.RegionID = t4.ID INNER JOIN
                                                                 dbo.CablePair AS t6 ON t2.CablePairID = t6.ID INNER JOIN
                                                                 dbo.Cable AS t7 ON t6.CableID = t7.ID ON t8.ID = t2.MDFRowID "
                                                 +
                                                 whereString
                                                 + @" INSERT @firstrows
                                                                    SELECT id, ROW_NUMBER() OVER (ORDER BY id) groupid
                                                                    FROM @table1 a
                                                                    WHERE id - 1 NOT IN (SELECT b.id FROM @table1 b WHERE b.MDFID = a.MDFID and b.VerticalCloumnID = a.VerticalCloumnID and b.VerticalRowID = a.VerticalRowID AND a.BuchtTypeName = b.BuchtTypeName)

                                                                INSERT @table2 SELECT id , CabinetInputID , InputNumber,CabinetID ,(SELECT MAX(b.groupid)
                                                                           FROM @firstrows b
                                                                           WHERE b.id <= a.id) groupid, BuchtTypeName
                                                                FROM @table1 a

                                                                SELECT         ROW_NUMBER() OVER (ORDER BY MIN(t1.InputNumber)) AS ID,t5.ID AS CabinetID,t5.CabinetNumber as CabinetNumber, t7.ID as CableID
																,CASE WHEN Description IS NOT NULL THEN CONVERT(varchar , [t11].[Number] ) + '( ' + [t11].[Description] +' )'  ELSE CONVERT(varchar ,[t11].[Number]) END AS [MDF]
															    , MIN(t1.InputNumber) AS FromInputNumber, MAX(t1.InputNumber) AS ToInputNumber,  t9.VerticalCloumnNo as VerticalCloumnNo ,t8.VerticalRowNo as VerticalRowNo
																, MIN(t2.BuchtNo) AS FromBuchtNo, MAX(t2.BuchtNo) AS ToBuchtNo,CONVERT(varchar, t7.CableNumber) as CableNumber , MIN(t6.CablePairNumber) AS FromCablePairNumber
																, MAX(t6.CablePairNumber) AS ToCablePairNumber , t12.BuchtTypeName
                                                                FROM            dbo.VerticalMDFColumn AS t9 INNER JOIN
                                                                                         dbo.VerticalMDFRow AS t8 ON t9.ID = t8.VerticalMDFColumnID INNER JOIN
                                                                                         dbo.MDFFrame AS t10 ON t10.ID = t9.MDFFrameID INNER JOIN
                                                                                         dbo.MDF AS t11 ON t11.ID = t10.MDFID INNER JOIN
                                                                                         dbo.Cabinet AS t5 INNER JOIN
                                                                                         @table2 AS t1 ON t5.ID = t1.CabinetID INNER JOIN
                                                                                         dbo.Bucht AS t2 ON t1.CabinetInputID = t2.CabinetInputID INNER JOIN
																						 dbo.BuchtType as t12 on t2.BuchtTypeID = t12.ID INNER JOIN 
                                                                                         dbo.Center AS t3 ON t5.CenterID = t3.ID INNER JOIN
                                                                                         dbo.Region AS t4 ON t3.RegionID = t4.ID INNER JOIN
                                                                                         dbo.CablePair AS t6 ON t2.CablePairID = t6.ID INNER JOIN
                                                                                         dbo.Cable AS t7 ON t6.CableID = t7.ID ON t8.ID = t2.MDFRowID "
                                                  +
                                                    whereString
                                                  + @" GROUP BY t5.ID,t5.CabinetNumber, [t11].[Number],[t11].[Description] , t7.CableNumber, t7.ID, t8.VerticalRowNo, t9.VerticalCloumnNo ,t1.groupid ,t12.BuchtTypeName";



                var temp = context.ExecuteQuery<GroupingCabinetInput>(queryString).ToList();

                return temp.ToList();
            }
        }
        public class CabinetInputInfoWithBuchts
        {
            public Bucht Bucht { get; set; }
            public CabinetInput CabinetInput { get; set; }
        }
        public static List<CabinetInputMalfunction> GetFailureCabinetInputStatistic(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate, int cabinetNumber)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //خرابی مرکزی ها که هنوز اصلاح نشده اند
                List<CabinetInputMalfunction> incorrectCabinetInputMalfaction =
                    context.CabinetInputs
                    .GroupJoin(context.Malfuctions, p => p.ID, m => m.CabinetInputID, (p, m) => new { cabinetinputs = p, Malfunction = p.Malfuctions })
                    .SelectMany(t1 => t1.Malfunction.DefaultIfEmpty(), (p, t1) => new { MaluFunctionCabinetInput = t1, CabinetInput = p.cabinetinputs })

                    .GroupJoin(context.Buchts, p => p.MaluFunctionCabinetInput.CabinetInput.ID, b => b.CabinetInputID, (b, p) => new { Bucht = b.CabinetInput.Buchts, MalFunction = b.MaluFunctionCabinetInput })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { CabinetInputsBucht = t1, Buchts = t2 })

                    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.Buchts.CabinetInput.Cabinet.CenterID)) &&
                            (!FromDate.HasValue || t.CabinetInputsBucht.MalFunction.DateMalfunction.Date >= FromDate) &&
                            (!ToDate.HasValue || t.CabinetInputsBucht.MalFunction.DateMalfunction.Date <= ToDate) &&
                            (t.Buchts.PostContact.Status == (byte)DB.CabinetInputStatus.Malfuction) &&
                            (cabinetNumber == -1 || cabinetNumber == t.CabinetInputsBucht.MalFunction.CabinetInput.Cabinet.CabinetNumber)
                            ).Select(t => new CabinetInputMalfunction
                            {
                                Cabinet = t.Buchts.CabinetInput.Cabinet.CabinetNumber,
                                BuchtNo = t.Buchts.BuchtNo,
                                CabinetInput = t.CabinetInputsBucht.MalFunction.CabinetInput.InputNumber,
                                VerticalRowNo = t.Buchts.VerticalMDFRow.VerticalRowNo,
                                VerticalCloumnNo = t.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                CenterName = t.Buchts.CabinetInput.Cabinet.Center.CenterName,
                                Description = t.CabinetInputsBucht.MalFunction.Description,
                                TypeMalfaction = t.CabinetInputsBucht.MalFunction.MalfuctionType,
                                Failure_Date = t.CabinetInputsBucht.MalFunction.DateMalfunction,
                                Failure_Time = t.CabinetInputsBucht.MalFunction.TimeMalfunction,
                                CorrectionType = "اصلاح نشده"
                            }
                            ).ToList();

                //خرابی مرکزی ها که اصلاح شده اند
                string query =
                                @"SELECT 
                                	Cabinet.CabinetNumber as Cabinet ,
                                	L.malfuactionID1,
                                	L.CabinetInputID1,
                                	L.ID1 ,
                                	L.CIID1,
                                	L.malfuactionID2,
                                	L.CabinetInputID2,
                                	L.ID2 ,
                                	L.CIID2 ,
                                	m1.TypeMalfunction AS TypeMalfaction,
                                	m1.DateMalfunction AS Failure_Date,
                                	m1.TimeMalfunction AS Failure_Time,
                                	m2.DateMalfunction AS Correct_Date,
                                	m2.TimeMalfunction AS Correct_Time ,
                                	m2.[Description], 
                                	Center.CenterName, 
                                	N'اصلاح شده' CorrectionType, 
                                	vmr.VerticalRowNo , 
                                	vmc.VerticalCloumnNo
                                FROM 
                                    (SELECT 
                                		t1.malfuactionID1 ,
                                		t1.CabinetInputID1 ,
                                		t1.ID1 ,
                                		t2.malfuactionID2 , 
                                		T2.CabinetInputID2 ,  
                                		t2.ID2, 
                                		t1.CIID1,
                                		t2.CIID2 
                                     FROM
                                	    (SELECT 
                                			*  
                                		 FROM
                                		    (
                                			  SELECT  
                                				ROW_NUMBER() OVER ( PARTITION BY ca.ID ORDER BY m.ID DESC) AS RowNumber, 
                                				m.ID as malfuactionID1 , 
                                				ca.ID as CabinetInputID1 , 
                                				ca.ID as ID1 , 
                                				m.CabinetInputID as CIID1
                                		      FROM 
                                				CabinetInput  as ca  
                                			  JOIN 
                                				Malfuction m on m.CabinetInputID = ca.ID ) as T 
                                	    WHERE 
                                		   T.RowNumber <= 2) as T1
                                	JOIN 
                                	    (
                                		  SELECT 
                                			* 
                                		  FROM 
                                	        (
                                				SELECT  
                                					ROW_NUMBER() OVER ( PARTITION BY ca.ID ORDER BY m.ID DESC) AS RowNumber, 
                                					m.ID as malfuactionID2 , 
                                					ca.ID as CabinetInputID2 , 
                                					ca.ID as ID2, 
                                					m.CabinetInputID as CIID2
                                		        FROM 
                                					CabinetInput as ca  
                                				JOIN 
                                					Malfuction m on m.CabinetInputID = ca.ID ) as T 
                                		   WHERE 
                                				T.RowNumber <= 2) as T2 ON T1.ID1 = T2.ID2 AND T1.malfuactionID1 < T2.malfuactionID2
                                
                                	    ) as L 
                                	JOIN 
                                		Malfuction m1 ON m1.ID = L.malfuactionID1
                                	JOIN 
                                		Malfuction m2 ON m2.ID = L.malfuactionID2
                                	JOIN 
                                		CabinetInput CI ON CI.ID = L.CIID1
                                	LEFT JOIN 
                                		Bucht b on b.ConnectionID  = CI.ID
                                	JOIN 
                                		cabinet on Cabinet.ID = CI.CabinetID
                                	JOIN 
                                		Center on Cabinet.CenterID = Center.ID
                                	JOIN 
                                		VerticalMDFRow vmr on vmr.ID = b.MDFRowID
                                	JOIN 
                                		VerticalMDFColumn vmc ON vmc.ID = vmr.VerticalMDFColumnID
                                	WHERE 
                                		{0} = -1 OR LEN({0}) = 0 OR {0} IS NULL OR Cabinet.CabinetNumber = {0}";


                if (FromDate.HasValue)
                    query += " and CONVERT(date , m1.DateMalfunction) >= CONVERT(date , '" + FromDate.Value.ToShortDateString() + "')";
                if (ToDate.HasValue)
                    query += " and CONVERT(date , m1.DateMalfunction) <= CONVERT(date , '" + ToDate.Value.ToShortDateString() + "')";
                if (CenterIDs.Count != 0)
                {
                    string CenterList = DB.MakeTheList(CenterIDs);
                    query += " and cabinet.CenterID in " + CenterList;
                }

                List<CabinetInputMalfunction> correctCabinetInputMalfaction = new List<CabinetInputMalfunction>();
                List<CabinetInputMalfunction> finalResult = new List<CabinetInputMalfunction>();
                correctCabinetInputMalfaction = context.ExecuteQuery<CabinetInputMalfunction>(query, cabinetNumber).ToList();

                //تجمیع دو لیست اصلاح شده و اصلاح نشده به عنوان نتیجه نهایی
                finalResult = incorrectCabinetInputMalfaction.Union(correctCabinetInputMalfaction).ToList();
                return finalResult;
            }
        }
        public static List<CabinetInputMalfunction> GetFailureNotCorrectCabinetInputStatistic(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return
                     context.CabinetInputs
                    .GroupJoin(context.Malfuctions, p => p.ID, m => m.CabinetInputID, (p, m) => new { cabinetinputs = p, Malfunction = p.Malfuctions })
                    .SelectMany(t1 => t1.Malfunction.DefaultIfEmpty(), (p, t1) => new { MaluFunctionCabinetInput = t1, CabinetInput = p.cabinetinputs })

                    .GroupJoin(context.Buchts, p => p.MaluFunctionCabinetInput.CabinetInput.ID, b => b.CabinetInputID, (b, p) => new { Bucht = b.CabinetInput.Buchts, MalFunction = b.MaluFunctionCabinetInput })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { CabinetInputsBucht = t1, Buchts = t2 })

                    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.Buchts.CabinetInput.Cabinet.CenterID)) &&
                            (!FromDate.HasValue || t.CabinetInputsBucht.MalFunction.DateMalfunction.Date >= FromDate) &&
                            (!ToDate.HasValue || t.CabinetInputsBucht.MalFunction.DateMalfunction.Date <= ToDate) &&
                            (t.Buchts.PostContact.Status == (byte)DB.CabinetInputStatus.Malfuction)
                            ).Select(t => new CabinetInputMalfunction
                            {
                                Cabinet = t.Buchts.CabinetInput.Cabinet.CabinetNumber,
                                BuchtNo = t.Buchts.BuchtNo,
                                CabinetInput = t.CabinetInputsBucht.MalFunction.CabinetInput.InputNumber,
                                VerticalRowNo = t.Buchts.VerticalMDFRow.VerticalRowNo,
                                VerticalCloumnNo = t.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                CenterName = t.Buchts.CabinetInput.Cabinet.Center.CenterName,
                                Description = t.CabinetInputsBucht.MalFunction.Description,
                                TypeMalfaction = t.CabinetInputsBucht.MalFunction.MalfuctionType,
                                Failure_Date = t.CabinetInputsBucht.MalFunction.DateMalfunction,
                                Failure_Time = t.CabinetInputsBucht.MalFunction.TimeMalfunction,
                                CorrectionType = "اصلاح نشده"
                            }
                            ).ToList();
            }
        }
        public static List<CabinetInputMalfunction> GetFailureCorrectCabinetInputStatistic(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string query =
                    @"SELECT Cabinet.CabinetNumber as Cabinet ,L.malfuactionID1,L.CabinetInputID1 as cabinetInput,L.ID1 ,L.CIID1,L.malfuactionID2,L.CabinetInputID2,L.ID2 ,L.CIID2 , m1.TypeMalfunction AS TypeMalfaction, m1.DateMalfunction AS Failure_Date,m1.TimeMalfunction AS Failure_Time,m2.DateMalfunction AS Correct_Date,m2.TimeMalfunction AS Correct_Time ,m2.[Description], Center.CenterName, 'اصلاح شده' CorrectionType
                    , vmr.VerticalRowNo , vmc.VerticalCloumnNo,b.BuchtNo
                    FROM 
                    (SELECT malfuactionID1 , CabinetInputID1 ,ID1 ,malfuactionID2 , T2.CabinetInputID2 ,  ID2, CIID1,CIID2 
	                FROM
	                (SELECT *  FROM
		                (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID1 , p.ID as CabinetInputID1 , p.ID as ID1 , m.CabinetInputID as CIID1
		                FROM CabinetInput  as p  JOIN Malfuction m on m.CabinetInputID = p.ID 
		                WHERE Status= 2 OR Status= 1) as T 
	                WHERE T.RowNumber <= 2) as T1
	                join 
	                (select * From 
	                    (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID2 , p.ID as CabinetInputID2 , p.ID as ID2, m.CabinetInputID as CIID2
			                FROM CabinetInput as p  JOIN Malfuction m on m.CabinetInputID = p.ID 
	                    WHERE Status= 2 OR Status= 1 ) as T 
		                WHERE T.RowNumber <= 2) as T2 ON T1.ID1 = T2.id2 AND T1.malfuactionID1 < T2.malfuactionID2

	                ) as L join Malfuction m1 ON m1.ID = L.malfuactionID1
	                    JOIN Malfuction m2 ON m2.ID = L.malfuactionID2
						join CabinetInput CI ON CI.ID = L.CIID1
	                    join Bucht b on b.ConnectionID  = CI.ID
	                    JOIN cabinet on Cabinet.ID = CI.CabinetID
						JOIN Center on Cabinet.CenterID = Center.ID
						JOIN VerticalMDFRow vmr on vmr.ID = b.MDFRowID
						JOIN VerticalMDFColumn vmc ON vmc.ID = vmr.VerticalMDFColumnID ";


                if (FromDate.HasValue)
                    query += " and CONVERT(date,m1.DateMalfunction) >= CONVERT(date, '" + FromDate.Value.ToShortDateString() + "')";
                if (ToDate.HasValue)
                    query += " and CONVERT(date,m1.DateMalfunction) <= CONVERT(date, '" + ToDate.Value.ToShortDateString() + "')";
                if (CenterIDs.Count != 0)
                {
                    string CenterList = DB.MakeTheList(CenterIDs);
                    query += " and cabinet.CenterID in " + CenterList;
                }
                return context.ExecuteQuery<CabinetInputMalfunction>(string.Format(query)).ToList();
            }
        }




        public static CabinetInput GetCabinetInputByNumber(int oldCabientID, int newCabinetID, long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.CabinetID == newCabinetID && t.InputNumber == context.CabinetInputs.Where(t2 => t2.ID == cabinetInputID).SingleOrDefault().InputNumber).SingleOrDefault();
            }
        }

        public static List<CabinetInput> GetCabinetInputByIDs(List<long> IDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => IDs.Contains(t.ID)).ToList();
            }
        }


        public static int GetCabinetInputCountByCabinetID(int caibnetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.ID == caibnetID).Select(t => t.ToInputNo - t.FromInputNo + 1).SingleOrDefault() ?? 0;
            }
        }

        //milad doran
        //public static List<CabinetInput> GetFreeCabinetInputByCabinetID(int cabinetID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Buchts.Where(t => t.CabinetInput.CabinetID == cabinetID &&
        //                                         t.Status == (int)DB.BuchtStatus.Free &&
        //                                         t.CabinetInput.Status == (int)DB.CabinetInputStatus.healthy &&
        //                                         !t.SwitchPortID.HasValue)
        //           .OrderBy(t => t.CabinetInput.InputNumber)
        //           .Select(t => t.CabinetInput).ToList();
        //    }
        //}

        //TODO:rad 13950620
        public static List<CabinetInput> GetFreeCabinetInputByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CabinetInput> result = new List<CabinetInput>();

                result = context.Buchts.Where(t =>
                                                 t.CabinetInput.CabinetID == cabinetID &&
                                                 t.Status == (int)DB.BuchtStatus.Free &&
                                                 t.CabinetInput.Status == (int)DB.CabinetInputStatus.healthy &&
                                                 !t.SwitchPortID.HasValue
                                              )
                                        .OrderBy(t => t.CabinetInput.InputNumber)
                                        .Select(t => t.CabinetInput)
                                        .ToList();

                return result;
            }
        }

        public static List<CheckableItem> GetFreeCabinetInputCheckableByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t =>
                                                 t.CabinetInput.CabinetID == cabinetID &&
                                                 t.Status == (int)DB.BuchtStatus.Free &&
                                                 t.CabinetInput.Status == (int)DB.CabinetInputStatus.healthy &&
                                                 !t.SwitchPortID.HasValue
                                            )
                                      .OrderBy(t => t.CabinetInput.InputNumber)
                                      .Select(t => new CheckableItem
                                                {
                                                    LongID = t.CabinetInputID,
                                                    Name = t.CabinetInput.InputNumber.ToString()
                                                }
                                             )
                                      .ToList();
            }
        }

        public static List<CabinetInput> GetFreeCabinetInputByCabinetInputID(int cabinetID, long fromCabinetInputID, long toCabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CabinetInput.CabinetID == cabinetID &&
                                                 t.CabinetInput.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == fromCabinetInputID).SingleOrDefault().InputNumber &&
                                                 t.CabinetInput.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == toCabinetInputID).SingleOrDefault().InputNumber &&
                                                 t.Status == (int)DB.BuchtStatus.Free &&
                                                 t.CabinetInput.Status == (int)DB.CabinetInputStatus.healthy &&
                                                 !t.SwitchPortID.HasValue)
                   .OrderBy(t => t.CabinetInput.InputNumber)
                   .Select(t => t.CabinetInput).ToList();
            }
        }

        public static CabinetInput GetCabinetInputByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).Select(t => t.CabinetInput).SingleOrDefault();
            }
        }

        public static long GetPCMCabinetinputIDByPostContactID(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return (long)context.Buchts.Where(t => t.ConnectionID == postContactID).SingleOrDefault().CabinetInputID;
            }
        }
    }
}