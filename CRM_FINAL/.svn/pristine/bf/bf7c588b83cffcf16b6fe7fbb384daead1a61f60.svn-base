using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Transactions;

namespace CRM.Data
{
    public static class MDFFrameDB
    {
        public static List<MDFFrame> SearchMDFFrame(List<int> cites, List<int> Centers, List<int> mDF, List<int> frameIDs, int verticalRowsCount, int verticalRowCapacity, int verticalFirstColumn, int verticalLastColumn, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames
                    .Where(t =>
                            (cites.Count == 0 || cites.Contains(t.MDF.Center.Region.CityID)) &&
                            (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.MDF.CenterID) : Centers.Contains(t.MDF.CenterID)) &&
							(mDF.Count == 0 || mDF.Contains(t.MDFID)) &&
                            (frameIDs.Count == 0 || frameIDs.Contains(t.ID)) && 
                            (verticalRowsCount == -1 || t.VerticalRowsCount == verticalRowsCount) && 
                            (verticalRowCapacity == -1 || t.VerticalRowCapacity == verticalRowCapacity) && 
                            (verticalFirstColumn == -1 || t.VerticalFirstColumn == verticalFirstColumn) && 
                            (verticalLastColumn == -1 || t.VerticalLastColumn == verticalLastColumn)
						  )
                    //.OrderBy(t => t.Name)
                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchMDFFrameCount(List<int> cites, List<int> Centers, List<int> mDF, List<int> frameIDs, int verticalRowsCount, int verticalRowCapacity, int verticalFirstColumn, int verticalLastColumn)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames
                    .Where(t =>
                            (cites.Count == 0 || cites.Contains(t.MDF.Center.Region.CityID)) &&
                            (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.MDF.CenterID) : Centers.Contains(t.MDF.CenterID)) &&
                            (mDF.Count == 0 || mDF.Contains(t.MDFID)) &&
                            (frameIDs.Count == 0 || frameIDs.Contains(t.ID)) &&
                            (verticalRowsCount == -1 || t.VerticalRowsCount == verticalRowsCount) &&
                            (verticalRowCapacity == -1 || t.VerticalRowCapacity == verticalRowCapacity) &&
                            (verticalFirstColumn == -1 || t.VerticalFirstColumn == verticalFirstColumn) &&
                            (verticalLastColumn == -1 || t.VerticalLastColumn == verticalLastColumn)
                          )
                    //.OrderBy(t => t.Name)
                    .Count();
            }
        }

        public static MDFFrame GetMDFFrameByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<Bucht> GetAllBuchtByFramID(int framID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrameID == framID).ToList();
            }
        }

        public static void SaveMDFFrame(
            int verticalLastColumn,
            int verticalFirstColumn,
            int countFrames,
            int frameNo,
            int verticalRowsCount,
            int verticalRowCapacity,
            MDF mDF,
            byte buchtType,
            MDFFrame MDFFrame)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    //byte buchtStatus =(byte)DB.BuchtStatus.Free;
                    //if (mDF.Uses == (byte)DB.MDFUses.ADSL)
                    //buchtStatus = (byte)DB.BuchtStatus.ADSLFree;
                    
                    int lastColumn = (verticalLastColumn - verticalFirstColumn + 1) * countFrames;
                    int numberColumn = verticalFirstColumn;



                    for (int frame = 0; frame < countFrames; frame++)
                    {
                        MDFFrame.ID = 0;
                        MDFFrame.FrameNo = frameNo + frame;
                        MDFFrame.Detach();
                        DB.Save(MDFFrame);
                    }

                    ts.Complete();
                   
                }
            }
        }

        public static List<CheckableItem> GetMDFFrameCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.FrameNo.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }


        public static bool CheckRepeatingFrames(List<int> FrameNoList,MDF mDF)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames.Where(t => t.MDFID == mDF.ID).Any(t => FrameNoList.Contains((int)t.FrameNo));
            }
        }

        public static List<CheckableItem> GetMDFFrameCheckableByMDFIDs(List<int> MDFIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames.Where(t=>MDFIDs.Contains(t.MDFID))
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.FrameNo.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static MDF GetMDFByFramID(int frameID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs.Where(t => t.MDFFrames.Where(t2 => t2.ID == frameID).SingleOrDefault().MDFID == t.ID).SingleOrDefault();
            }
        }

        public static int GetCityIDByFrameID(int frameID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFFrames.Where(t2 => t2.ID == frameID).SingleOrDefault().MDF.Center.Region.CityID;
            }
        }
    }
}