using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLMDFDB
    {
        public static List<ADSLMDFInfo> SearchADSLMDF(List<int> cites, List<int> Centers, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => (t.BuchtTypeID == (byte)DB.BuchtType.ADSL) &&
                                (cites.Count == 0 || cites.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
                                (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : Centers.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID)))
                    .Select(t => new ADSLMDFInfo
                    {
                        ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                        MDFTitle = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo + " - " + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                        Center = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.City.Name + " : " + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.CenterName
                    }).Distinct().Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchADSLMDFCount(List<int> cites, List<int> Centers)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => (t.BuchtTypeID == (byte)DB.BuchtType.ADSL) &&
                                                 (cites.Count == 0 || cites.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
                                                 (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : Centers.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID)))
                    .Select(t => new ADSLMDFInfo
                    {
                        ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                        MDFTitle = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo + " - " + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                        Center = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.City.Name + " : " + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.CenterName
                    }).Distinct().Count();
            }
        }

        public static List<ADSLPortInfo> SearchADSLMDFPort(List<int> cityIDs, List<int> centerIDs, List<int> mDFIDs, List<int> StatusIDs, List<int> rowIDs, List<int> columnIDs, List<int> portIDs, string telephoneNo, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                    .Where(t => (cityIDs.Count == 0 || cityIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
                                (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : centerIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID)) &&
                                (mDFIDs.Count == 0 || mDFIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID)) &&
                                (rowIDs.Count == 0 || rowIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumnID)) &&
                                (columnIDs.Count == 0 || columnIDs.Contains(t.Bucht.MDFRowID)) &&
                                (portIDs.Count == 0 || portIDs.Contains(Convert.ToInt32(t.InputBucht))) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (StatusIDs.Count == 0 || StatusIDs.Contains(t.Status)))
                    .Select(t => new ADSLPortInfo
                    {
                        ID = t.ID,
                        MDFTitle = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo + " - " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                        Center = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.City.Name + " : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.CenterName,
                        Port = "ردیف : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.Bucht.BuchtNo.ToString(),
                        TelephoneNo = t.TelephoneNo.ToString(),
                        InstalADSLDate = Date.GetPersianDate(t.InstalADSLDate, Date.DateStringType.DateTime),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), t.Status)
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchADSLMDFPortCount(List<int> cityIDs, List<int> centerIDs, List<int> mDFIDs, List<int> StatusIDs, List<int> rowIDs, List<int> columnIDs, List<int> portIDs, string telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                    .Where(t => (cityIDs.Count == 0 || cityIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID)) &&
                                (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : centerIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID)) &&
                                (mDFIDs.Count == 0 || mDFIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID)) &&
                                (rowIDs.Count == 0 || rowIDs.Contains(t.Bucht.VerticalMDFRow.VerticalMDFColumnID)) &&
                                (columnIDs.Count == 0 || columnIDs.Contains(t.Bucht.MDFRowID)) &&
                                (portIDs.Count == 0 || portIDs.Contains(Convert.ToInt32(t.InputBucht))) &&
                                (string.IsNullOrWhiteSpace(telephoneNo)|| t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (StatusIDs.Count == 0 || StatusIDs.Contains(t.Status)))
                    .Count();
            }
        }

        public static List<ADSLPort> GetFreeADSLPortByCenterID(int centerID, int mDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts.Where(t => (t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID) &&
                                                    (t.Status == (byte)DB.ADSLPortStatus.Free) &&
                                                    (t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDFID)).ToList();
            }
        }

        public static List<CheckableItem> GetMDFCheckablebyCenterIDs(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => (t.BuchtTypeID == (byte)DB.BuchtType.ADSL) &&
                                (centerIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : centerIDs.Contains(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID)))
                    .Select(t => new CheckableItem
                    {
                        ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                        Name = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo + " - " + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                        IsChecked = false
                    })
                    .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static string GetMDFDescriptionbyTelephoneNo(long telephoneNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string mDFDescription = "";

                List<ADSLMDFInfo> mDFsList = context.Buchts.Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID) && (t.BuchtTypeID == (byte)DB.BuchtType.ADSL))
                                                           .Select(t => new ADSLMDFInfo
                                                           {
                                                               ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                                                               MDFTitle = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo + " - " + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description
                                                           }).Distinct().ToList();

                foreach (ADSLMDFInfo currentMDF in mDFsList)
                {
                    List<ADSLMDFRange> mDFRangeList = context.ADSLMDFRanges.Where(t => t.MDFID == (int)currentMDF.ID).ToList();

                    foreach (ADSLMDFRange range in mDFRangeList)
                    {
                        if (telephoneNo >= range.StartTelephoneNo && telephoneNo <= range.EndTelephoneNo)
                        {
                            mDFDescription = currentMDF.MDFTitle;
                            break;
                        }
                        else
                            mDFDescription = "";
                    }

                    if (!string.IsNullOrWhiteSpace(mDFDescription))
                        break;
                }

                return mDFDescription;
            }
        }
    }
}
