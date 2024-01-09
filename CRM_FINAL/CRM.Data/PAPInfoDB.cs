using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Globalization;

namespace CRM.Data
{
    public static class PAPInfoDB
    {
        public static List<PAPInfo> SearchPAPInfo(string title, string address)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfos
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static PAPInfo GetPAPInfoByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfos
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetPAPInfoCheckable()
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

        public static List<PAPInfoSpaceandPower> SearchPAPInfoSpace(int papInfoId, List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoSpaceandPowers
                    .Where(t => (t.PAPInfoID == papInfoId) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (t.EndDate == null))
                    .OrderBy(t => t.ID)
                    .ToList();
            }
        }

        public static List<PAPInfoSpaceandPower> SearchPAPInfoSpaceByCenterID(int papInfoId, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoSpaceandPowers
                    .Where(t => (t.PAPInfoID == papInfoId) &&
                                (t.CenterID == centerID))
                    .OrderByDescending(t => t.FromDate)
                    .ToList();
            }
        }

        public static PAPInfoSpaceandPower GetPAPInfoSpaceandPower(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoSpaceandPowers.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<PAPInfoSpaceandPower> GetPAPInfoSpaceandPowerwithCenterID(int papID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == papID && t.CenterID == centerID).ToList();
            }
        }

        public static PAPInfoSpaceandPower GetLastSpaceandPowerwithCenterID(int papID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == papID && t.CenterID == centerID && t.EndDate == null).SingleOrDefault();
            }
        }

        public static void GetSumPAPInfoSpaceandPowerbyID(int papID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == papID).Sum(t => t.ACPower);
            }
        }

        public static List<PapInfo> GetPapInfo()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfos.Where(t => t.LoginStatus == true)
                                       .Select(t => new PapInfo
                                        {
                                            ID = t.ID,
                                            Title = t.Title
                                        }).ToList();
            }
        }

        public static List<PapInfoReport> GetPAPInfoReport(List<int> CenterIDs, DateTime FromDate, DateTime ToDate, int PapCompanyID)
        {

            using (MainDataContext context = new MainDataContext())
            {

                return context.ADSLTelephoneNoHistories
                    .Where(t => (t.InstalDate >= FromDate) &&
                                (t.InstalDate <= ToDate && (t.DischargeDate >= ToDate || t.DischargeDate == null)) &&
                                (CenterIDs.Count == 0 || CenterIDs.Contains(t.CenterID)) &&
                                ((int)t.PAPInfoID == PapCompanyID)).OrderBy(t => t.InstalDate)
                    .Select(t => new PapInfoReport
                    {
                        CenterID = t.CenterID,
                        CenterName = t.Center.CenterName,
                        CompanyName = t.PAPInfo.Title,
                        DischargeDate = t.DischargeDate,
                        InstallDate = t.InstalDate,
                        Money = 0,
                        PAPInfoID = t.PAPInfo.ID,
                        TelNo = t.TelephoneNo.ToString(),
                        StartDate = (DateTime)t.InstalDate,
                        PersianInstallDate = Date.GetPersianDate(t.InstalDate, Date.DateStringType.Short),
                        EndDate = (!t.DischargeDate.HasValue) ? (DateTime)ToDate : ((t.DischargeDate < ToDate) ? (DateTime)t.DischargeDate : (DateTime)ToDate),
                        PersianDischargeDate = Date.GetPersianDate(t.DischargeDate, Date.DateStringType.Short)
                    }).ToList();
            }
        }
        public static List<PapInfoGroupByCenterID> GetPAPInfoGroupByCenterIDReport(List<PapInfoReport> PapInfo)
        {
            return PapInfo
                         .GroupBy(t => new { CenterID = t.CenterID })
                         .Select(t => new PapInfoGroupByCenterID
                         {
                             CenterID = t.Key.CenterID,
                             ADSLCustomerCost = t.Sum(x => x.Money),
                             ADSLCUstomerCount = t.Count()
                         })
                         .ToList();
        }
        public static PapInfoGroupByPapInfoID GetPAPInfoGroupByPapInfoReport(List<PapInfoReport> PapInfo)
        {
            return PapInfo
                         .GroupBy(t => new { PapInfoID = t.PAPInfoID })
                         .Select(t => new PapInfoGroupByPapInfoID
                         {
                             PapInfoID = t.Key.PapInfoID,
                             ADSLCustomerCost = t.Sum(x => x.Money),
                             ADSLCUstomerCount = t.Count()
                         })
                         .SingleOrDefault();
        }

        public static CycleDate GetCycleDate(int? CycleId)
        {
            DateTime today = DB.GetServerDate();
            string todayShamsi = Date.GetPersianDate(today, Date.DateStringType.Short);
            string[] todayShamsiAry = todayShamsi.Split('/');
            int firstDayofMonth = Convert.ToInt32(todayShamsiAry[2]);
            int MounthofYear = Convert.ToInt32(todayShamsiAry[1]);
            DateTime firstDayMiladi = (MounthofYear <= 6) ? today.AddDays(-((MounthofYear - 1) * 31) - (firstDayofMonth - 1)) : today.AddDays(-((((MounthofYear - 1) - 6) * 30) + (6 * 31)) - (firstDayofMonth - 1));

            CycleDate _CycleDate = new CycleDate();
            DateTime FDT = firstDayMiladi;
            DateTime LDT = firstDayMiladi;
            if (CycleId != null)
            {
                if (CycleId <= 3)
                {
                    FDT = FDT.AddDays(((int)CycleId - 1) * 2 * 31);
                    LDT = LDT.AddDays(((int)CycleId * 2 * 31) - 1);
                }
                else
                {
                    FDT = FDT.AddDays((3 * 2 * 31 + ((int)(CycleId - 3) - 1) * 2 * 30));
                    if (CycleId == 6)
                        LDT = LDT.AddYears(1).AddDays(-1);
                    else
                        LDT = LDT.AddDays((3 * 2 * 31 + ((int)(CycleId - 3) * 2 * 30) - 1));

                }
                _CycleDate.FromDate = FDT;
                _CycleDate.ToDate = LDT;
            }
            return _CycleDate;
        }

        public static List<SpaceAndPowerPapCompany> GetSpaceAndpowerPAPCompanyReport(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate, int? CycleID, int? PapCompanyID)
        {

            CycleDate _CycleDate = new CycleDate();
            if (!CycleID.HasValue)
            {
                _CycleDate.FromDate = FromDate;
                _CycleDate.ToDate = (!ToDate.HasValue) ? DB.GetServerDate() : ToDate;
            }
            else if (CycleID.HasValue)
            {
                _CycleDate = GetCycleDate(CycleID);
            }



            using (MainDataContext context = new MainDataContext())
            {

                return context.PAPInfoSpaceandPowers
                    .Where(t =>
                                (!_CycleDate.FromDate.HasValue || ((t.FromDate >= _CycleDate.FromDate) && (t.FromDate <= _CycleDate.ToDate)))
                                && (t.EndDate == null)
                             && (CenterIDs.Count == 0 || CenterIDs.Contains(t.CenterID))
                             && (!PapCompanyID.HasValue || (int)t.PAPInfoID == PapCompanyID))
                             .Select(t => new SpaceAndPowerPapCompany
                             {
                                 CenterName = t.Center.CenterName,
                                 CompanyName = t.PAPInfo.Title,
                                 ACPower = t.ACPower,
                                 DCPower = t.DCPower,
                                 StartDate = t.FromDate,
                                 PAPInfoID = t.PAPInfo.ID,
                                 CityName = t.Center.Region.City.Name,
                                 Space = t.Space

                             }
                             ).ToList();

            }

        }

        public static List<TinyBillingOptions> GetTinyBillingOptionsReportGroupByCenter(List<int> CenterIDs, string FromDate, string ToDate, int PapCompanyID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TinyBillingOptions> result = new List<TinyBillingOptions>();


                string query = @"SELECT T2.CostID,T2.CenterID,T2.CenterName,T2.City
                                , SUM(T2.Space ) as SpaceField
                                ,SUM(T2.DCPower) as DCPowerFiels
                                ,SUM(T2.ACPower) as ACPowerField
                                ,SUM(T2.SpaceCost)/SUM(T2.DuringInUse) as SpaceCost
                                ,SUM(T2.ACPowerCost)/SUM(T2.DuringInUse) as ACPowerCost
                                ,SUM(T2.DCPowerCost)/SUM(T2.DuringInUse) as DCPowerCost
                               
                                from(
                                select * ,((CASE WHEN T.CostID = 2 THEN T.Space ELSE 0 END ) *  CAST(T.Value as decimal)  * T.DuringInUse) as SpaceCost 
		                                 ,((CASE WHEN T.CostID = 3 THEN T.ACPower ELSE 0 END )  * CAST(T.Value as decimal)  * T.DuringInUse) as ACPowerCost
		                                 ,((CASE WHEN T.CostID = 4 THEN T.DCPower else 0 END )   * CAST(T.Value as decimal)  * T.DuringInUse) as DCPowerCost
                                from (
                                SELECT ps.CostID,(CASE WHEN ps.Value IS NULL THEN 0 else ps.Value END) as Value,pp.Space ,pp.ACPower,pp.DCPower,pp.ID, 


                                (DATEDIFF(dd,
								 (CASE WHEN (DATEDIFF(dd,pp.FromDate,'" + FromDate + @"')) > 0 THEN '" + FromDate + @"' ELSE pp.FromDate end)
								,(CASE WHEN (DATEDIFF(dd,pp.EndDate,'" + ToDate + @"')) > 0  THEN pp.EndDate 
                                       WHEN (pp.EndDate IS null) AND (ps.EndDate IS NULL OR DATEDIFF(dd,ps.EndDate,'" + ToDate + @"')< 0)  THEN '" + ToDate + @"'
									   ELSE pp.EndDate end) ) + 1) as DuringInUse
							                                ,pp.CenterID , pp.PAPInfoID,City.Name as City,Center.CenterName 
								
                                FROM PAPInfoSpaceandPower pp
                                 JOIN (SELECT * from PAPInfoCostHistory pc1
			                                where
			                                (((pc1.StartDate IS NOT NULL  AND  pc1.EndDate  IS NOT NULL AND  '05/22/2013' BETWEEN pc1.StartDate AND  pc1.EndDate) OR ('" + ToDate + @"' >= pc1.StartDate AND  pc1.EndDate IS null))
												and pc1.CostID IN (2,3,4))
											)
											ps on pp.FromDate=pp.FromDate
                                            
                                            LEFT JOIN Center on Center.ID = pp.CenterID
                                            LEFT JOIN Region on Region.ID = Center.RegionID
                                            LEFT JOIN City on City.ID = Region .CityID
                                            WHERE pp.PAPInfoID = " + PapCompanyID + @" and pp.FromDate <='" + ToDate + @"'
                                            
                                ) as T  ) as T2  ";
                if (CenterIDs.Count > 0)
                    query += " where T2.CenterID in " + DB.MakeTheList(CenterIDs);
                query += " group BY T2.CostID,T2.CenterID,t2.City,t2.CenterName";


                result = context.ExecuteQuery<TinyBillingOptions>(string.Format(query)).ToList();
                return result;
            }
        }
        public static List<TinyBillingOptions> GetTinyBillingOptionsReport(List<int> CenterIDs, string FromDate, string ToDate, int PapCompanyID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TinyBillingOptions> result = new List<TinyBillingOptions>();
                string query = @"SELECT T2.CostID
                                , SUM(T2.Space ) as SpaceField
                                ,SUM(T2.DCPower) as DCPowerFiels
                                ,SUM(T2.ACPower) as ACPowerField
                                ,SUM(T2.SpaceCost)/SUM(T2.DuringInUse) as SpaceCost
                                ,SUM(T2.ACPowerCost)/SUM(T2.DuringInUse) as ACPowerCost
                                ,SUM(T2.DCPowerCost)/SUM(T2.DuringInUse) as DCPowerCost
                                from(
                                select * ,((CASE WHEN T.CostID = 2 THEN T.Space ELSE 0 END ) *  CAST(T.Value as bigint)  * T.DuringInUse) as SpaceCost 
		                                 ,((CASE WHEN T.CostID = 3 THEN T.ACPower ELSE 0 END )  * CAST(T.Value as bigint)  * T.DuringInUse) as ACPowerCost
		                                 ,((CASE WHEN T.CostID = 4 THEN T.DCPower else 0 END )   * CAST(T.Value as bigint)  * T.DuringInUse) as DCPowerCost
                                from (
                                SELECT ps.CostID,(CASE WHEN ps.Value IS NULL THEN 0 else ps.Value END) as Value,pp.Space ,pp.ACPower,pp.DCPower,pp.ID, (DATEDIFF(dd,
								 (CASE WHEN (DATEDIFF(dd,ps.StartDate,pp.FromDate)) > 0 THEN pp.FromDate ELSE ps.StartDate end)
								,(CASE WHEN (DATEDIFF(dd,ps.EndDate,pp.EndDate)) > 0  THEN ps.EndDate 
                                       WHEN (pp.EndDate IS null) AND (ps.EndDate IS NULL OR DATEDIFF(dd,ps.EndDate,'" + ToDate + "')< 0)  THEN '" + ToDate + @"'
									   ELSE pp.EndDate end) ) + 1) as DuringInUse
							                                ,pp.CenterID , pp.PAPInfoID,City.Name as City,Center.CenterName 
                                FROM PAPInfoSpaceandPower pp
                                 JOIN (SELECT * from PAPInfoCostHistory pc1
			                                where
			                                ((pc1.StartDate IS NOT NULL  AND  pc1.EndDate  IS NOT NULL AND  '" + FromDate + "' BETWEEN pc1.StartDate AND  pc1.EndDate) OR ('" + ToDate + @"' >= pc1.StartDate AND  pc1.EndDate IS null)
												and pc1.CostID IN (2,3,4))
											)
											ps on (ps.EndDate IS NOT NULL  and pp.FromDate   BETWEEN ps.StartDate AND ps.EndDate ) OR (ps.EndDate IS NULL AND pp.FromDate >= ps.StartDate)
                                            
                                            LEFT JOIN Center on Center.ID = pp.CenterID
                                            LEFT JOIN Region on Region.ID = Center.RegionID
                                            LEFT JOIN City on City.ID = Region .CityID
                                            WHERE pp.PAPInfoID = " + PapCompanyID + @" and pp.FromDate <='" + ToDate + @"'AND ps.CostID in (2,3,4)
                                            
                                ) as T  ) as T2  ";
                if (CenterIDs.Count > 0)
                    query += " where T2.CenterID in " + DB.MakeTheList(CenterIDs);
                query += " group BY T2.CostID";


                result = context.ExecuteQuery<TinyBillingOptions>(string.Format(query)).ToList();
                return result;
            }
        }

        public static Dictionary<int, int> GetADSLPAPRequestGroupByPapInfo(List<int> CenterIDs, DateTime FromDate, DateTime ToDate, int PapCompanyID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests
                    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.Request.CenterID)) &&
                               (t.Request.EndDate != null && t.Request.EndDate >= FromDate && t.Request.EndDate <= ToDate) &&
                               (t.PAPInfoID == PapCompanyID))
                     .GroupBy(t => new
                     {
                         PapInfoID = t.PAPInfoID
                     })
                    .Select(t => new
                    {
                        PapInfoID = t.Key.PapInfoID,
                        Count = t.Count()
                    }).ToDictionary(x => x.PapInfoID, x => x.Count);
            }
        }
        public static Dictionary<int, int> GetADSLPAPRequest(List<int> CenterIDs, DateTime FromDate, DateTime ToDate, int PapCompanyID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPRequests
                    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.Request.CenterID)) &&
                               (t.Request.EndDate != null && t.Request.EndDate >= FromDate && t.Request.EndDate <= ToDate) &&
                               (t.PAPInfoID == PapCompanyID))
                     .GroupBy(t => new
                     {
                         CenterID = t.Request.CenterID
                     })
                    .Select(t => new
                    {
                        CenterID = t.Key.CenterID,
                        Count = t.Count()
                    }).ToDictionary(x => x.CenterID, x => x.Count);
            }
        }

        public static int GetPAPInfoIDbyUserName(string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int userID = context.Users.Where(t => t.UserName == userName).SingleOrDefault().ID;
                return context.PAPInfoUsers.Where(t => t.ID == userID).SingleOrDefault().PAPInfoID;
            }
        }

        public static string GetPAPInfoName(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfos.Where(t => t.ID == id).SingleOrDefault().Title;
            }
        }

        public static List<PAPInfo> GetAllPAP()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfos.ToList();
            }
        }
    }
}
