using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class PCMPortDB
    {
        public static PCMPort GetPCMPortByID(long ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts.Where(t => t.ID == ID).SingleOrDefault();
            }
        }
        public static List<PCMPort> GetAllPCMPortByPCMID(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts.Where(t => t.PCMID == ID).ToList();
            }
        }
        public static List<PCMPortInfo> SearchPCMPort(
            List<int> CityIDs,
            List<int> CenterIDs,
            List<int> RockIDs,
            List<int> ShelfIDs,
            List<int> CardIDs,
            List<int> PortTypeIDs,
            List<int> StatusIDs,
            int portNumber,
            int startRowIndex,
            int pageSize
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts.Where(t => (CityIDs.Count == 0 || CityIDs.Contains(t.PCM.PCMShelf.PCMRock.Center.Region.CityID)) &&
                                                  (CenterIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCM.PCMShelf.PCMRock.CenterID) : CenterIDs.Contains(t.PCM.PCMShelf.PCMRock.CenterID)) &&
                                                  (RockIDs.Count == 0 || RockIDs.Contains(t.PCM.PCMShelf.PCMRockID)) &&
                                                  (ShelfIDs.Count == 0 || ShelfIDs.Contains(t.PCM.ShelfID)) &&
                                                  (CardIDs.Count == 0 || CardIDs.Contains(t.PCM.ID)) &&
                                                  (PortTypeIDs.Count == 0 || PortTypeIDs.Contains(t.PortType)) &&
                                                  (StatusIDs.Count == 0 || StatusIDs.Contains(t.Status)) &&
                                                  (portNumber == -1 || t.PortNumber == portNumber))
                                                  .OrderBy(t => t.PortNumber)
                                                  .Select(t => new PCMPortInfo
                                                      {
                                                          ID = t.ID,
                                                          PCMID = t.PCMID,
                                                          PortNumber = t.PortNumber,
                                                          PortType = t.PortType,
                                                          Status = t.Status,
                                                          PCM = "رک: " + t.PCM.PCMShelf.PCMRock.Number.ToString() + "شلف :" + t.PCM.PCMShelf.Number.ToString() + "کارت :" + t.PCM.Card + "نوع :" + t.PCM.PCMType.Name,
                                                      }
                                                      )
                                                  .Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchPCMPortCount(
          List<int> CityIDs,
          List<int> CenterIDs,
          List<int> RockIDs,
          List<int> ShelfIDs,
          List<int> CardIDs,
          List<int> PortTypeIDs,
          List<int> StatusIDs,
          int portNumber)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts.Where(t => (CityIDs.Count == 0 || CityIDs.Contains(t.PCM.PCMShelf.PCMRock.Center.Region.CityID)) &&
                                                  (CenterIDs.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCM.PCMShelf.PCMRock.CenterID) : CenterIDs.Contains(t.PCM.PCMShelf.PCMRock.CenterID)) &&
                                                  (RockIDs.Count == 0 || RockIDs.Contains(t.PCM.PCMShelf.PCMRockID)) &&
                                                  (ShelfIDs.Count == 0 || ShelfIDs.Contains(t.PCM.ShelfID)) &&
                                                  (CardIDs.Count == 0 || CardIDs.Contains(t.PCM.ID)) &&
                                                  (PortTypeIDs.Count == 0 || PortTypeIDs.Contains(t.PortType)) &&
                                                  (StatusIDs.Count == 0 || StatusIDs.Contains(t.Status)) &&
                                                  (portNumber == -1 || t.PortNumber == portNumber)).Count();
            }
        }

        public static byte? GetStatusByCheckConnectedBucht(int pCMPortID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<int> pCMPorts = new List<int> { pCMPortID };
                List<Bucht> bucht = Data.BuchtDB.getBuchtByPCMPortID(pCMPorts);
                if (bucht.Count == 0)
                {
                    return (byte)DB.PCMPortStatus.Empty;
                }
                else if (bucht.SingleOrDefault().ADSLStatus == true)
                {
                    return (byte)DB.PCMPortStatus.Connection;
                }
                else if (bucht.SingleOrDefault().ADSLStatus == false)
                {
                    return (byte)DB.PCMPortStatus.Empty;
                }

                return null;

            }
        }
        public static List<CheckableItem> GetPCMPortCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.PortNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetPCMPortOutputCheckable(List<int> PCMCard)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts
                    .Where(t => (PCMCard.Contains(t.PCMID)) && (t.PortType == (byte)DB.BuchtType.OutLine))
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.PortNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
        public static List<CheckableItem> GetPCMPortInputCheckable(List<int> PCMCard)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts
                    .Where(t => (PCMCard.Contains(t.PCMID)) && (t.PortType == (byte)DB.BuchtType.InLine))
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.PortNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static PCMPortInfo GetPCMPortInfoByID(int p)
        {
            throw new NotImplementedException();
        }
        public static List<PortMalfunction> GetCorrectPortMalfunction(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string query =
                @"SELECT PCMRock.Number As Rock , PCMShelf.Number AS Shelf, PCM.[Card] ,L.PortNumber1 AS Port,m1.TypeMalfunction AS TypeMalfaction, m1.DateMalfunction AS Failure_Date,m1.TimeMalfunction AS Failure_Time,m2.DateMalfunction AS Correct_Date,m2.TimeMalfunction AS Correct_Time ,m2.[Description], Center.CenterName FROM 
                    (SELECT malfuactionID1 , PCMID1 ,ID1 ,malfuactionID2 , PCMID2 ,  ID2,PortNumber1
	                FROM
	                (SELECT *  FROM
		                (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID1 , p.PCMID as PCMID1 , p.ID as ID1,p.PortNumber as PortNumber1
		                FROM PCMPort  as p  JOIN Malfuction m on m.PCMPortID = p.ID 
		                WHERE Status= 2 OR Status= 1) as T 
	                WHERE T.RowNumber <= 2) as T1
	                join 
	                (select * From 
	                    (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID2 , p.PCMID as PCMID2 , p.ID as ID2,p.PortNumber as PortNumber2
			                FROM PCMPort  as p  JOIN Malfuction m on m.PCMPortID = p.ID 
	                    WHERE Status= 2 OR Status= 1 ) as T 
		                WHERE T.RowNumber <= 2) as T2 ON T1.ID1 = T2.id2 AND T1.malfuactionID1 < T2.malfuactionID2

	                ) as L join Malfuction m1 ON m1.ID = L.malfuactionID1
	                    JOIN Malfuction m2 ON m2.ID = L.malfuactionID2
	                    JOIN PCM on PCM.ID = L.PCMID1
	                    join PCMShelf on PCMShelf.ID = PCM.ShelfID
	                    join PCMRock on PCMRock.ID = PCMShelf.PCMRockID
	                    join Center on PCMrock.CenterID = Center.ID";


                if (FromDate.HasValue)
                    query += " and CONVERT(date, m1.DateMalfunction  , 101 ) >= CONVERT(date, '" + FromDate.Value.ToShortDateString() + "', 101)";
                if (ToDate.HasValue)
                    query += " and CONVERT(date, m1.DateMalfunction , 101 ) <= CONVERT(date, '" + ToDate.Value.ToShortDateString() + "', 101)";
                if (CenterIDs.Count != 0)
                {
                    string CenterList = DB.MakeTheList(CenterIDs);
                    query += " and PCMRock.CenterID in " + CenterList;
                }
                return context.ExecuteQuery<PortMalfunction>(string.Format(query)).ToList();



            }
        }
        public static List<PortMalfunction> GetNotCorrectedPortMalfunction(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {

                List<PortMalfunction> result =
                    context.PCMPorts
                    .GroupJoin(context.Malfuctions, p => p.ID, m => m.PCMPortID, (p, m) => new { pcmport = p, Malfunction = p.Malfuctions })
                    .SelectMany(t1 => t1.Malfunction.DefaultIfEmpty(), (p, t1) => new { MaluFunctionPCMPort = t1, PCMPort = p.pcmport })
                    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.PCMPort.PCM.PCMShelf.PCMRock.CenterID)) &&
                            (!FromDate.HasValue || t.MaluFunctionPCMPort.DateMalfunction >= FromDate) &&
                            (!ToDate.HasValue || t.MaluFunctionPCMPort.DateMalfunction <= ToDate)&&
                            (t.PCMPort.Status == (byte)DB.PCMStatus.Destruction)

                            ).Select(t => new PortMalfunction
                            {
                                Card = t.MaluFunctionPCMPort.PCMPort.PCM.Card,
                                CenterName = t.MaluFunctionPCMPort.PCMPort.PCM.PCMShelf.PCMRock.Center.CenterName,
                                Description = t.MaluFunctionPCMPort.Description,
                                PCMType = t.PCMPort.PCM.PCMType.Name,
                                Port = t.PCMPort.PortNumber,
                                Rock = t.MaluFunctionPCMPort.PCMPort.PCM.PCMShelf.PCMRock.Number,
                                Shelf = t.MaluFunctionPCMPort.PCMPort.PCM.PCMShelf.Number,
                                TypeMalfaction = t.MaluFunctionPCMPort.MalfuctionType,
                                Failure_Date = t.MaluFunctionPCMPort.DateMalfunction,
                                Failure_Time = t.MaluFunctionPCMPort.TimeMalfunction,
                                PortNumber = t.PCMPort.PortNumber
                            }
                            ).ToList();
                return result;


            }
        }
        public static List<PortMalfunction> GetPortMalfunction(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {

                List<PortMalfunction> result1 =
                    context.PCMPorts
                    .GroupJoin(context.Malfuctions, p => p.ID, m => m.PCMPortID, (p, m) => new { pcmport = p, Malfunction = p.Malfuctions })
                    .SelectMany(t1 => t1.Malfunction.DefaultIfEmpty(), (p, t1) => new { MaluFunctionPCMPort = t1, PCMPort = p.pcmport })
                    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.PCMPort.PCM.PCMShelf.PCMRock.CenterID)) &&
                            (!FromDate.HasValue || t.MaluFunctionPCMPort.DateMalfunction >= FromDate) &&
                            (!ToDate.HasValue || t.MaluFunctionPCMPort.DateMalfunction <= ToDate) &&
                            (t.PCMPort.Status == (byte)DB.PCMStatus.Destruction)
                            ).Select(t => new PortMalfunction
                            {
                                Card = t.MaluFunctionPCMPort.PCMPort.PCM.Card,
                                CenterName = t.MaluFunctionPCMPort.PCMPort.PCM.PCMShelf.PCMRock.Center.CenterName,
                                Description = t.MaluFunctionPCMPort.Description,
                                PCMType = t.PCMPort.PCM.PCMType.Name,
                                Port = t.PCMPort.PortNumber,
                                Rock = t.MaluFunctionPCMPort.PCMPort.PCM.PCMShelf.PCMRock.Number,
                                Shelf = t.MaluFunctionPCMPort.PCMPort.PCM.PCMShelf.Number,
                                TypeMalfaction = t.MaluFunctionPCMPort.MalfuctionType,
                                Failure_Date = t.MaluFunctionPCMPort.DateMalfunction,
                                Failure_Time = t.MaluFunctionPCMPort.TimeMalfunction,
                                PortNumber = t.PCMPort.PortNumber,
                                CorrectionType = "اصلاح نشده"
                            }
                            ).ToList();
                string query =
                @"SELECT PCMRock.Number As Rock , PCMShelf.Number AS Shelf, PCM.[Card] ,L.PortNumber1 AS Port,m1.TypeMalfunction AS TypeMalfaction, m1.DateMalfunction AS Failure_Date,m1.TimeMalfunction AS Failure_Time,m2.DateMalfunction AS Correct_Date,m2.TimeMalfunction AS Correct_Time ,m2.[Description], Center.CenterName, 'اصلاح شده' CorrectionType FROM 
                    (SELECT malfuactionID1 , PCMID1 ,ID1 ,malfuactionID2 , PCMID2 ,  ID2,PortNumber1
	                FROM
	                (SELECT *  FROM
		                (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID1 , p.PCMID as PCMID1 , p.ID as ID1,p.PortNumber as PortNumber1
		                FROM PCMPort  as p  JOIN Malfuction m on m.PCMPortID = p.ID 
		                WHERE Status= 2 OR Status= 1) as T 
	                WHERE T.RowNumber <= 2) as T1
	                join 
	                (select * From 
	                    (SELECT  ROW_NUMBER() OVER ( PARTITION BY p.ID ORDER BY m.ID DESC) AS RowNumber, m.ID as malfuactionID2 , p.PCMID as PCMID2 , p.ID as ID2,p.PortNumber as PortNumber2
			                FROM PCMPort  as p  JOIN Malfuction m on m.PCMPortID = p.ID 
	                    WHERE Status= 2 OR Status= 1 ) as T 
		                WHERE T.RowNumber <= 2) as T2 ON T1.ID1 = T2.id2 AND T1.malfuactionID1 < T2.malfuactionID2

	                ) as L join Malfuction m1 ON m1.ID = L.malfuactionID1
	                    JOIN Malfuction m2 ON m2.ID = L.malfuactionID2
	                    JOIN PCM on PCM.ID = L.PCMID1
	                    join PCMShelf on PCMShelf.ID = PCM.ShelfID
	                    join PCMRock on PCMRock.ID = PCMShelf.PCMRockID
	                    join Center on PCMrock.CenterID = Center.ID
                        where 1 = 1";


                if (FromDate.HasValue)
                    query += " and m1.DateMalfunction >= CONVERT(datetime, '" + FromDate.Value.ToShortDateString() + "', 101)";
                if (ToDate.HasValue)
                    query += " and m1.DateMalfunction <= CONVERT(datetime, '" + ToDate.Value.ToShortDateString() + "', 101)";
                if (CenterIDs.Count != 0)
                {
                    string CenterList = DB.MakeTheList(CenterIDs);
                    query += " and PCMRock.CenterID in " + CenterList;
                }
                List<PortMalfunction> result2 = new List<PortMalfunction>();
                List<PortMalfunction> result = new List<PortMalfunction>();
                result2 = context.ExecuteQuery<PortMalfunction>(string.Format(query)).ToList();
                result2.ForEach(t => t.CorrectionType = "اصلاح شده");
                result =  result1.Union(result2).ToList();
                return result;


            }
        }


        public static PCMPort GetPCMPortByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMPorts.Where(t => context.Buchts.Where(b => b.ID == buchtID).SingleOrDefault().PCMPortID == t.ID).SingleOrDefault();
            }
        }
    }
}
