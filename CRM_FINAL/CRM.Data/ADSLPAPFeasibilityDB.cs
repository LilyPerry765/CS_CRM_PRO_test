using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLPAPFeasibilityDB
    {
        public static List<ADSLPAPFeasibilityInfo> SearchPAPFeasibilties(
            List<int> papInfoIDs,
            List<int> cityIDs,
            long telephoneNo,
            DateTime? date,
            List<int> statusIDs,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPFeasibilities
                    .Where(t => (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (cityIDs.Count == 0 || cityIDs.Contains(t.CityID)) &&
                                (CityDB.GetAvailableCity().Contains(t.CityID)) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                (!date.HasValue || t.Date.Date == date) &&
                                (statusIDs.Count == 0 || statusIDs.Contains(t.Status)))
                    .OrderByDescending(t => t.ID)
                    .Select(t => new ADSLPAPFeasibilityInfo
                    {
                        ID = t.ID,
                        PAPInfo = t.PAPInfo.Title,
                        CityName = t.City.Name,
                        Date = Date.GetPersianDate(t.Date, Date.DateStringType.DateTime),
                        TelephoneNo = t.TelephoneNo.ToString(),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.FeasibilityStatus), t.Status)
                    }
                    ).Skip(startRowIndex).Take(pageSize).ToList();
                return x;
            }
        }

        public static int SearchPAPFeasibiltiesCount(
            List<int> papInfoIDs,
            List<int> cityIDs,
            long telephoneNo,
            DateTime? date,
            List<int> statusIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPFeasibilities
                     .Where(t => (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                 (cityIDs.Count == 0 || cityIDs.Contains(t.CityID)) &&
                                 (CityDB.GetAvailableCity().Contains(t.CityID)) &&
                                 (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                 (!date.HasValue || t.Date.Date == date) &&
                                 (statusIDs.Count == 0 || statusIDs.Contains(t.Status))
                        ).Count();
                return x;
            }
        }

        public static int GetFeasibilitytNo(int papUserID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPFeasibilities.Where(t => t.PAPUserID == papUserID && t.Date.Date == DB.GetServerDate().Date).ToList().Count;
            }
        }
    }
}
