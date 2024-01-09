using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class VerticalMDFRowDB
    {
        //milad doran
        //public static List<VerticalMDFRow> SearchVerticalMDFRow(List<int> cites, List<int> center, List<int> MDFs, int verticalMDFColumn, int verticalRowNo, int rowCapacity, int startRowIndex, int pageSize)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.VerticalMDFRows
        //            .Where(t =>
        //                  (cites.Count == 0 || cites.Contains(t.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
        //                  (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.VerticalMDFColumn.MDFFrame.MDF.CenterID) : center.Contains(t.VerticalMDFColumn.MDFFrame.MDF.CenterID)) &&
        //                  (MDFs.Count == 0 || MDFs.Contains(t.VerticalMDFColumn.MDFFrame.MDFID)) &&
        //                  (verticalMDFColumn == -1 || t.VerticalMDFColumn.VerticalCloumnNo == verticalMDFColumn) && 
        //                  (verticalRowNo == -1 || t.VerticalRowNo == verticalRowNo) && 
        //                  (rowCapacity == -1 || t.RowCapacity == rowCapacity)
        //                  ).OrderBy(t=>t.VerticalRowNo)
        //            .Skip(startRowIndex).Take(pageSize).ToList();
        //    }
        //}

        //TODO:rad 13950331
        public static List<VerticalMDFRowInfo> SearchVerticalMDFRow(List<int> cites, List<int> center, List<int> mdfsID, int mdfFrameNo, int verticalMDFColumn, int verticalRowNo, int rowCapacity, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<VerticalMDFRowInfo> result = new List<VerticalMDFRowInfo>();

                var query = context.VerticalMDFRows
                                   .Where(t =>
                                               (cites.Count == 0 || cites.Contains(t.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
                                               (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.VerticalMDFColumn.MDFFrame.MDF.CenterID) : center.Contains(t.VerticalMDFColumn.MDFFrame.MDF.CenterID)) &&
                                               (mdfsID.Count == 0 || mdfsID.Contains(t.VerticalMDFColumn.MDFFrame.MDFID)) &&
                                               (mdfFrameNo == -1 || mdfFrameNo == t.VerticalMDFColumn.MDFFrame.FrameNo) &&
                                               (verticalMDFColumn == -1 || t.VerticalMDFColumn.VerticalCloumnNo == verticalMDFColumn) &&
                                               (verticalRowNo == -1 || t.VerticalRowNo == verticalRowNo) &&
                                               (rowCapacity == -1 || t.RowCapacity == rowCapacity)
                                         )
                                   .OrderBy(t => t.VerticalRowNo)
                                   .AsQueryable();

                count = query.Count();

                result = query.Select(vmr => new VerticalMDFRowInfo
                                            {
                                                ID = vmr.ID,
                                                VerticalMDFColumnID = vmr.VerticalMDFColumnID,
                                                VerticalRowNo = vmr.VerticalRowNo,
                                                RowCapacity = vmr.RowCapacity,
                                                CityName = vmr.VerticalMDFColumn.MDFFrame.MDF.Center.Region.City.Name,
                                                CenterName = vmr.VerticalMDFColumn.MDFFrame.MDF.Center.CenterName,
                                                MDFNumberWithDescription = vmr.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + "(" + vmr.VerticalMDFColumn.MDFFrame.MDF.Description + ")",
                                                MDFFrameNo = vmr.VerticalMDFColumn.MDFFrame.FrameNo,
                                                VerticalMDFColumnNo = vmr.VerticalMDFColumn.VerticalCloumnNo
                                            }
                                     )
                              .Skip(startRowIndex)
                              .Take(pageSize)
                              .ToList();

                return result;
            }
        }

        public static int SearchVerticalMDFRowCount(List<int> cites, List<int> center, List<int> MDFs, int verticalMDFColumn, int verticalRowNo, int rowCapacity)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFRows
                    .Where(t =>
                          (cites.Count == 0 || cites.Contains(t.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
                          (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.VerticalMDFColumn.MDFFrame.MDF.CenterID) : center.Contains(t.VerticalMDFColumn.MDFFrame.MDF.CenterID)) &&
                          (MDFs.Count == 0 || MDFs.Contains(t.VerticalMDFColumn.MDFFrame.MDFID)) &&
                          (verticalMDFColumn == -1 || t.VerticalMDFColumn.VerticalCloumnNo == verticalMDFColumn) &&
                          (verticalRowNo == -1 || t.VerticalRowNo == verticalRowNo) &&
                          (rowCapacity == -1 || t.RowCapacity == rowCapacity)
                          )
                    .Count();
            }
        }

        public static VerticalMDFRow GetVerticalMDFRowByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFRows
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetVerticalMDFRowCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFRows
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.VerticalRowNo.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetVerticalMDFRowCheckablebyRowIDs(List<int> rowIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => (t.BuchtTypeID == (byte)DB.BuchtType.ADSL) &&
                                (rowIDs.Count == 0 || rowIDs.Contains(t.VerticalMDFRow.VerticalMDFColumnID)))
                    .Select(t => new CheckableItem
                    {
                        ID = t.VerticalMDFRow.ID,
                        Name = t.VerticalMDFRow.VerticalRowNo.ToString(),
                        IsChecked = false
                    })
                    .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static int GetCenterByVerticalMDFRowID(int verticalRowID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFRows.Where(r => r.ID == verticalRowID).Select(r => r.VerticalMDFColumn.MDFFrame.MDF.CenterID).SingleOrDefault();

            }
        }

        public static List<Bucht> GetAllBuchtByRowID(int verticalMDFRowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.VerticalMDFRow.ID == verticalMDFRowID).ToList();
            }
        }
    }
}