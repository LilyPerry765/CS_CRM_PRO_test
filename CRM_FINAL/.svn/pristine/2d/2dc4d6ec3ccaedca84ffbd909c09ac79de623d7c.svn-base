using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class VerticalMDFColumnDB
    {
        public static List<VerticalMDFColumn> SearchVerticalMDFColumn(List<int> cites, List<int> center, List<int> MDFs, int mDFFrame, int verticalCloumnNo, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFColumns
                    .Where(t =>
                            (cites.Count == 0 || cites.Contains(t.MDFFrame.MDF.Center.Region.CityID)) &&
                            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.MDFFrame.MDF.CenterID) : center.Contains(t.MDFFrame.MDF.CenterID)) &&
                            (MDFs.Count == 0 || MDFs.Contains(t.MDFFrame.MDFID)) &&
                            (mDFFrame == -1 || mDFFrame == t.MDFFrame.FrameNo) &&
                            (verticalCloumnNo == -1 || t.VerticalCloumnNo == verticalCloumnNo)
                          )
                          .OrderBy(t => t.VerticalCloumnNo)
                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchVerticalMDFColumnCount(List<int> cites, List<int> center, List<int> MDFs, int mDFFrame, int verticalCloumnNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFColumns
                    .Where(t =>
                            (cites.Count == 0 || cites.Contains(t.MDFFrame.MDF.Center.Region.CityID)) &&
                            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.MDFFrame.MDF.CenterID) : center.Contains(t.MDFFrame.MDF.CenterID)) &&
                            (MDFs.Count == 0 || MDFs.Contains(t.MDFFrame.MDFID)) &&
                            (mDFFrame == -1 || mDFFrame == t.MDFFrame.FrameNo) &&
                            (verticalCloumnNo == -1 || t.VerticalCloumnNo == verticalCloumnNo)
                          )
                    .Count();
            }
        }

        public static VerticalMDFColumn GetVerticalMDFColumnByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFColumns
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetVerticalMDFColumnCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFColumns
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.VerticalCloumnNo.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetVerticalMDFColumnCheckablebyMDFIDs(List<int> mDFIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => (t.BuchtTypeID == (byte)DB.BuchtType.ADSL) &&
                                (mDFIDs.Count == 0 || mDFIDs.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID)))
                    .Select(t => new CheckableItem
                    {
                        ID = t.VerticalMDFRow.VerticalMDFColumn.ID,
                        Name = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                        IsChecked = false
                    })
                    .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<CheckableItem> GetVerticalMDFColumnCheckablebyCenterIDs(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => (t.BuchtTypeID == (byte)DB.BuchtType.ADSL)&&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID)))
                    .Select(t => new CheckableItem
                    {
                        ID = t.VerticalMDFRow.VerticalMDFColumn.ID,
                        Name = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                        IsChecked = false
                    })
                    .Distinct().OrderBy(t=>t.ID).ToList();
            }
        }

        public static List<Bucht> GetAllBuchtByColumnID(int verticalMDFColumnID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.VerticalMDFRow.VerticalMDFColumnID == verticalMDFColumnID).ToList();
            }
        }
    }
}