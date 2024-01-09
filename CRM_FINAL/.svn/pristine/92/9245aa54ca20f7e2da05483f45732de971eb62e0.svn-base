using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLTelephoneHistoryDB
    {
        public static List<ADSLTelephoneHistoryInfo> SearchTelephoneHistory(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long telephoneNo,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTelephoneNoHistories
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (papInfoIDs.Count == 0 || papInfoIDs.Contains((int)t.PAPInfoID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo))
                    .OrderByDescending(t => t.InstalDate)
                    .Select(t => new ADSLTelephoneHistoryInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        PAPInfo = t.PAPInfo.Title,
                        InstalDate = Date.GetPersianDate(t.InstalDate, Date.DateStringType.Short),
                        DischargeDate = Date.GetPersianDate(t.DischargeDate, Date.DateStringType.Short)
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchTelephoneHistorysCount(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLTelephoneNoHistories
                     .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                 (papInfoIDs.Count == 0 || papInfoIDs.Contains((int)t.PAPInfoID)) &&
                                 (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                 (telephoneNo == -1 || t.TelephoneNo == telephoneNo)).Count();
            }
        }

        public static ADSLTelephoneNoHistory GetADSLTelephoneHistoryforDischarge(long telephoneNo, int papID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLTelephoneNoHistory history = context.ADSLTelephoneNoHistories.Where(t => t.TelephoneNo == telephoneNo && t.PAPInfoID == papID).OrderByDescending(t => t.InstalDate).First();

                if (history.DischargeDate == null)
                    return history;
                else
                    return null;
            }
        }

        public static  List<CheckableItem> GetADSLTelNoCentersReport(){

            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CenterName,
                        IsChecked = false
                    }).ToList();
            }
        }

        public static List<CheckableItem> GetADSLTelNoPAPInfoReport()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfos
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    }).ToList();
            }
        }
    }
}
