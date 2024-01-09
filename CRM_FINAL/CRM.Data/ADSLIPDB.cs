using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLIPDB
    {
        public static List<ADSLIPType> SearchADSLIPGroup(List<int> typeIDs, string title, bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPTypes
                        .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                    (typeIDs.Count == 0 || typeIDs.Contains(t.Type)) &&
                                    (!isActive.HasValue || isActive == t.IsActive))
                        .ToList();
            }
        }

        public static ADSLIPType GetADSLIPTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPTypes
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLIPTypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPTypes
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<ADSLIP> SearchADSLIP(string iP, List<int> typeIDs, List<int> customerGroupIDs, long telephoneNo, List<int> status, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => (string.IsNullOrWhiteSpace(iP) || t.IP.Contains(iP)) &&
                                    (typeIDs.Count == 0 || typeIDs.Contains(t.TypeID)) &&
                                    (customerGroupIDs.Count == 0 || customerGroupIDs.Contains((int)t.CustometGroupID)) &&
                                    (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                    (status.Count == 0 || status.Contains((byte)t.Status)))
                                    .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchADSLIPCount(string iP, List<int> typeIDs, List<int> customerGroupIDs, long telephoneNo, List<int> status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => (string.IsNullOrWhiteSpace(iP) || t.IP.Contains(iP)) &&
                                    (typeIDs.Count == 0 || typeIDs.Contains(t.TypeID)) &&
                                    (customerGroupIDs.Count == 0 || customerGroupIDs.Contains((int)t.CustometGroupID)) &&
                                    (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                    (status.Count == 0 || status.Contains((byte)t.Status)))
                                    .Count(); ;
            }
        }

        public static List<ADSLGroupIP> SearchADSLGroupIP(string iPStart, string virtualRange, string blockCount, List<int> typeIDs, List<int> customerGroupIDs, long telephoneNo, List<int> status, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => (string.IsNullOrWhiteSpace(iPStart) || t.StartRange.Contains(iPStart)) &&
                                                       (string.IsNullOrWhiteSpace(virtualRange) || t.VirtualRange.Contains(virtualRange)) &&
                                                       (string.IsNullOrWhiteSpace(blockCount) || t.BlockCount.ToString().Contains(blockCount)) &&
                                                       (typeIDs.Count == 0 || typeIDs.Contains(t.TypeID)) &&
                                                       (customerGroupIDs.Count == 0 || customerGroupIDs.Contains((int)t.CustometGroupID)) &&
                                                       (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                                       (status.Count == 0 || status.Contains((byte)t.Status)))
                                            .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchADSLGroupIPCount(string iPStart, string virtualRange, string blockCount, List<int> typeIDs, List<int> customerGroupIDs, long telephoneNo, List<int> status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => (string.IsNullOrWhiteSpace(iPStart) || t.StartRange.Contains(iPStart)) &&
                                                       (string.IsNullOrWhiteSpace(virtualRange) || t.VirtualRange.Contains(virtualRange)) &&
                                                       (string.IsNullOrWhiteSpace(blockCount) || t.BlockCount.ToString().Contains(blockCount)) &&
                                                       (typeIDs.Count == 0 || typeIDs.Contains(t.TypeID)) &&
                                                       (customerGroupIDs.Count == 0 || customerGroupIDs.Contains((int)t.CustometGroupID)) &&
                                                       (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                                       (status.Count == 0 || status.Contains((byte)t.Status)))
                                            .Count(); ;
            }
        }

        public static ADSLIP GetADSLIPById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static ADSLGroupIP GetADSLGroupIPById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static bool HasIP(int groupID, string iP)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLIP aDSLIP = context.ADSLIPs.Where(t => t.TypeID == groupID && t.IP == iP).SingleOrDefault();
                if (aDSLIP == null)
                    return false;
                else
                    return true;
            }
        }

        public static List<ADSLIP> GetADSLIPFree(int customerGroupID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => (t.CustometGroupID == null || t.CustometGroupID == customerGroupID) && (t.Status == (byte)DB.ADSLIPStatus.Free || t.Status == (byte)DB.ADSLIPStatus.Discharge)).ToList();
            }
        }

        public static List<ADSLGroupIP> GetADSLGroupIPFree(int blockCount, int customerGroupID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => (t.BlockCount == blockCount) && (t.CustometGroupID == null || t.CustometGroupID == customerGroupID) &&
                                                       (t.Status == (byte)DB.ADSLIPStatus.Free || t.Status == (byte)DB.ADSLIPStatus.Discharge)).ToList();
            }
        }

        public static ADSLIPHistoryInfo GetADSLIPforHistoryById(long id, int type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLIPHistoryInfo iPHistory = new ADSLIPHistoryInfo();

                if (type == (byte)DB.ADSLIPType.Single)
                    iPHistory = context.ADSLIPs.Where(t => t.ID == id).Select(t => new ADSLIPHistoryInfo
                    {
                        IP = t.IP,
                        Type = "تکی",
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLIPStatus), t.Status),
                    }).SingleOrDefault();

                if (type == (byte)DB.ADSLIPType.Group)
                    iPHistory = context.ADSLGroupIPs.Where(t => t.ID == id).Select(t => new ADSLIPHistoryInfo
                    {
                        IP = t.StartRange,
                        Type = "گروهی",
                        BlockCount = t.BlockCount.ToString(),
                        VirtualRange = t.VirtualRange,
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLIPStatus), t.Status),
                    }).SingleOrDefault();

                return iPHistory;
            }
        }

        public static List<ADSLIPHistoryInfo> GetADSLIPHistoryListByID(long id, int type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLIPHistoryInfo> iPHistoryList = new List<ADSLIPHistoryInfo>();

                if (type == (byte)DB.ADSLIPType.Single)
                    iPHistoryList = context.ADSLIPHistories.Where(t => t.IPID == id).Select(t => new ADSLIPHistoryInfo
                    {
                        TelephoneNo = t.TelephoneNo.ToString(),
                        StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                    }).ToList();

                if (type == (byte)DB.ADSLIPType.Group)
                    iPHistoryList = context.ADSLIPHistories.Where(t => t.IPGroupID == id).Select(t => new ADSLIPHistoryInfo
                    {
                        TelephoneNo=t.TelephoneNo.ToString(),
                        StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                        EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                    }).ToList();

                return iPHistoryList;
            }
        }

        public static string GetIPValuebyID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLIPs.Where(t => t.ID == id).SingleOrDefault().IP;
            }
        }

        public static string GetIPGroupValuebyID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLGroupIPs.Where(t => t.ID == id).SingleOrDefault().StartRange;
            }
        }
    }
}
