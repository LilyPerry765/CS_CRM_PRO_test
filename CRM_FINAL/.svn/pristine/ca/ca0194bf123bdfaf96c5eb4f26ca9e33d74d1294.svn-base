using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class MDFDB
    {
        public static List<MDF> SearchMDF(List<int> cites, List<int> center, List<int> MDFs, int lastNoHorizontalFrames, int LastNoVerticalFrames, List<int> type, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<MDF> result = new List<MDF>();
                var query = context.MDFs
                                   .Where(t =>
                                           (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                                           (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                                           (MDFs.Count == 0 || MDFs.Contains(t.ID)) &&
                                           (lastNoHorizontalFrames == -1 || t.LastNoHorizontalFrames == lastNoHorizontalFrames) &&
                                           (LastNoVerticalFrames == -1 || t.LastNoVerticalFrames == LastNoVerticalFrames) &&
                                           (type.Count == 0 || type.Contains(t.Type))
                                         )
                                   .AsQueryable();
                result = query.Skip(startRowIndex).Take(pageSize).ToList();
                count = query.Count();
                return result;
            }
        }

        public static int SearchMDFCount(List<int> cites, List<int> center, List<int> MDFs, int lastNoHorizontalFrames, int LastNoVerticalFrames, List<int> type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs
                    .Where(t =>
                            (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                            (MDFs.Count == 0 || MDFs.Contains(t.ID)) &&
                            (lastNoHorizontalFrames == -1 || t.LastNoHorizontalFrames == lastNoHorizontalFrames) &&
                            (LastNoVerticalFrames == -1 || t.LastNoVerticalFrames == LastNoVerticalFrames) &&
                            (type.Count == 0 || type.Contains(t.Type))
                          )
                    .Count();
            }
        }

        public static MDF GetMDFByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static int GetMDFByRowID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFRows
                    .Where(t => t.ID == id)
                    .Select(t => t.VerticalMDFColumn.MDFFrame.MDF.ID).SingleOrDefault();

            }
        }

        public static MDF GetMDFInfoByRowID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {


                var s = context.MDFs
                    .Join(context.MDFFrames, m => m.ID, mf => mf.MDFID, (m, mf) => new { mdf = m, mdfFram = mf })
                    .Join(context.VerticalMDFColumns, MDFMDFFrame => MDFMDFFrame.mdfFram.ID, c => c.MDFFrameID, (MDFMDFFrame, c) => new { MDFMDFFrame = MDFMDFFrame, column = c })
                    .Join(context.VerticalMDFRows, column => column.column.ID, r => r.VerticalMDFColumnID, (column, r) => new { MDFMDFFrameColumn = column, row = r })
                    .Where(t => t.row.ID == id)
                    .Select(x => x.MDFMDFFrameColumn.MDFMDFFrame.mdf).SingleOrDefault();

                return s;

            }
        }

        public static List<CheckableItem> GetMDFCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID)).AsEnumerable()
                              .Select(t => new CheckableItem
                               {
                                   ID = t.ID,
                                   Name = t.Number.ToString() + DB.GetDescription(t.Description),
                                   IsChecked = false
                               })
                             .OrderBy(t => t.Name)
                             .ToList();
            }

        }

        public static List<CheckableItem> GetMDFByType(byte type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs.Where(t => t.Type == type).Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false }).ToList();
            }
        }

        public static string GetMDFBtBuchtID(long buchtID)
        {
            string mDFInfo = string.Empty;
            using (MainDataContext context = new MainDataContext())
            {

                mDFInfo = context.Buchts.Where(t => t.ID == buchtID).AsEnumerable().Select(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + DB.GetDescription(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description)).SingleOrDefault().ToString();
                //int? FeatureONUID = context.Buchts.Where(t => t.ID == buchtID).Select(t => t.FeatureONUID).SingleOrDefault();
                //if (FeatureONUID != null)
                //{
                //    mDFInfo += " مشخصه اونو:" + DB.SearchByPropertyName<SwitchGroup>("ID", FeatureONUID).SingleOrDefault().Name;
                //}
                return mDFInfo;
            }
        }

        public static int GetMDFIDByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).Select(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID).SingleOrDefault();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetMDFCheckableByCenterID(int centerID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.MDFs.AsEnumerable().Where(t => t.CenterID == centerID)
        //                      .OrderBy(t => t.Number)
        //                      .Select(t => new CheckableItem
        //                      {
        //                          ID = t.ID,
        //                          Name = t.Number.ToString() + DB.GetDescription(t.Description),
        //                          IsChecked = false
        //                      })                             
        //                     .ToList();
        //    }
        //}

        //TODO:rad 13950219
        public static List<CheckableItem> GetMDFCheckableByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.MDFs
                                   .AsEnumerable()
                                   .Where(t => t.CenterID == centerID)
                                   .OrderBy(t => t.Number)
                                   .Select(t => new CheckableItem
                                                {
                                                    ID = t.ID,
                                                    Name = t.Number.ToString() + DB.GetDescription(t.Description),
                                                    IsChecked = false
                                                }
                                           )
                                  .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        public static List<CheckableItem> GetMDFCheckableByCenterIDs(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs.AsEnumerable().Where(t => centerIDs.Count == 0 || centerIDs.Contains(t.CenterID))
                              .Select(t => new CheckableItem
                              {
                                  ID = t.ID,
                                  Name = t.Number.ToString() + DB.GetDescription(t.Description),
                                  IsChecked = false
                              })
                             .OrderBy(t => t.Name)
                             .ToList();
            }
        }

        public static int? GetLastFram(int MDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames.Where(t => t.MDFID == MDFID).Max(t => t.FrameNo);
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static MDF GetMDFByFrameID(int frameID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var mDFFrame = context.MDFFrames.Where(f => f.ID == frameID).SingleOrDefault();
                return context.MDFs.Where(t => mDFFrame.MDFID == t.ID).SingleOrDefault();
            }
        }

        public static List<VerticalBuchtReportInfo> GetVerticalBuchtStatistic(int CenterID, int BuchtTypeID, string MDFID, string ColumnID, string RowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //
                string query = @"SELECT bt.BuchtTypeName,c.CabinetNumber,ci.InputNumber,p.Number,pc.ConnectionNo,t.TelephoneNo,t.CustomerID,b.[Status],vmc.VerticalCloumnNo  ,vmr.VerticalRowNo , b.BuchtNo,m.Number,p.Number as PostNumber, pc.ConnectionNo
                                    FROM MDF m
                                    JOIN MDFFrame mf on mf.MDFID = m.ID
                                    JOIN VerticalMDFColumn  vmc ON vmc.MDFFrameID = mf.ID
                                    JOIN VerticalMDFRow vmr ON vmr.VerticalMDFColumnID = vmc.ID
                                    JOIN Bucht b ON b.MDFRowID = vmr.ID
                                    LEFT JOIN Telephone t ON t.SwitchPortID = b.SwitchPortID
                                    LEFT JOIN BuchtType bt ON bt.ID = b.BuchtTypeID
                                    LEFT JOIN CabinetInput ci ON ci.ID = b.CabinetInputID
                                    Left JOIN Cabinet c ON c.ID = ci.CabinetID
                                    left JOIN PostContact pc ON pc.ID = b.ConnectionID
                                    LEFT JOIN Post p ON p.ID = pc.PostID
                                    LEFT JOIN Customer cu ON cu.ID = t.CustomerID
                                    where m.CenterID = {0} And b.BuchtTypeID = {1} and vmc.ID = {2} and vmr.ID = " + RowID + " order by b.BuchtNo "; //" + ColumnID";


                //if (BuchtTypeIDs.Count > 0)
                //{
                //    string BuchtTypeList = MakeTheList(BuchtTypeIDs);
                //    query += " and b.BuchtTypeID in " + BuchtTypeList;
                //}
                //if (MDFID != null)
                //    query += " and m.ID = " + MDFID;
                //if (ColumnID != null)
                //    query += " and vmc.ID = " + ColumnID;
                //if (RowID != null)
                //    query += " and vmr.ID = " + RowID;


                //query += " order by b.BuchtNo ";

                return context.ExecuteQuery<VerticalBuchtReportInfo>(string.Format(query, CenterID, BuchtTypeID, ColumnID)).ToList();

            }
        }

        private static string MakeTheList(List<int> List)
        {
            string result = string.Empty;
            foreach (int item in List)
            {
                result += item + ",";
            }
            result = result.Substring(0, result.Length - 1);
            result = new StringBuilder("(").Append(result).Append(")").ToString();
            return result;
        }

        public static List<MDF> GetMDFbyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs.Where(t => t.CenterID == centerID).ToList();
            }
        }
    }
}
