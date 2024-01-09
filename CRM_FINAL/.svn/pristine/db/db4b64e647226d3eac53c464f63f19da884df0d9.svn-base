using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLPAPPortDB
    {
        public static List<PAPPortInfo> SearchPAPPorts(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long portNo,
            long telephoneNo,
            List<int> statusIDs,
            DateTime? fromDate,
            DateTime? toDate,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPPorts
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (portNo == -1 || t.PortNo == portNo) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                (statusIDs.Count == 0 || statusIDs.Contains(t.Status)) &&
                                (!fromDate.HasValue || t.InstallDate >= fromDate) &&
                                (!toDate.HasValue || t.InstallDate <= toDate))
                    .OrderByDescending(t => t.PAPInfoID)
                    .Select(t => new PAPPortInfo
                    {
                        ID = t.ID,
                        PAPInfo = t.PAPInfo.Title,
                        Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        PortNo = t.PortNo.ToString(),
                        TelephoneNo = t.TelephoneNo.ToString(),
                        InstallDate = Date.GetPersianDate(t.InstallDate, Date.DateStringType.Short),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPPortStatus), t.Status)
                    }
                    ).Skip(startRowIndex).Take(pageSize).ToList();
                return x;
            }
        }

        public static int SearchPAPPortsCount(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long portNo,
            long telephoneNo,
            List<int> statusIDs,
            DateTime? fromDate,
            DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPPorts
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (portNo == -1 || t.PortNo == portNo) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                (statusIDs.Count == 0 || statusIDs.Contains(t.Status) &&
                                (!fromDate.HasValue || t.InstallDate >= fromDate) &&
                                (!toDate.HasValue || t.InstallDate <= toDate))
                        ).Count();
                return x;
            }
        }

        public static List<PAPPortInfo> SearchPAPBuchts(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long rowNo,
            long columnNo,
            long buchtNo,
            long telephoneNo,
            List<int> statusIDs,
            DateTime? fromDate,
            DateTime? toDate,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPPorts
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (rowNo == -1 || t.RowNo == rowNo) &&
                                (columnNo == -1 || t.ColumnNo == columnNo) &&
                                (buchtNo == -1 || t.BuchtNo == buchtNo) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                (statusIDs.Count == 0 || statusIDs.Contains(t.Status)) &&
                                (!fromDate.HasValue || t.InstallDate >= fromDate) &&
                                (!toDate.HasValue || t.InstallDate <= toDate))
                    .OrderByDescending(t => t.PAPInfoID)
                    .Select(t => new PAPPortInfo
                    {
                        ID = t.ID,
                        PAPInfo = t.PAPInfo.Title,
                        Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        RowNo = t.RowNo.ToString(),
                        ColumnNo = t.ColumnNo.ToString(),
                        BuchtNo = t.BuchtNo.ToString(),
                        TelephoneNo = t.TelephoneNo.ToString(),
                        InstallDate = Date.GetPersianDate(t.InstallDate, Date.DateStringType.Short),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPPortStatus), t.Status)
                    }).Skip(startRowIndex).Take(pageSize).ToList();
                return x;
            }
        }

        public static int SearchPAPBuchtsCount(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long rowNo,
            long columnNo,
            long buchtNo,
            long telephoneNo,
            List<int> statusIDs,
            DateTime? fromDate,
            DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPPorts
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (rowNo == -1 || t.RowNo == rowNo) &&
                                (columnNo == -1 || t.ColumnNo == columnNo) &&
                                (buchtNo == -1 || t.BuchtNo == buchtNo) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                (statusIDs.Count == 0 || statusIDs.Contains(t.Status)) &&
                                (!fromDate.HasValue || t.InstallDate >= fromDate) &&
                                (!toDate.HasValue || t.InstallDate <= toDate)
                        ).Count();
                return x;
            }
        }

        public static List<PAPPortGroupInfo> SearchPAPBuchtsGroup(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long rowNo,
            long columnNo,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPPorts
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (rowNo == -1 || rowNo == t.RowNo) &&
                                (columnNo == -1 || columnNo == t.ColumnNo))
                    .OrderByDescending(t => t.PAPInfoID)
                    .GroupBy(t => new
                    {
                        Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        CenterID = t.CenterID,
                        PAP = t.PAPInfo.Title,
                        PAPID = t.PAPInfoID,
                        Row = t.RowNo.ToString(),
                        Column = t.ColumnNo.ToString()
                    })
                    .Select(t => new PAPPortGroupInfo
                    {
                        PAPInfo = t.Key.PAP,
                        Center = t.Key.Center,
                        RowNo = t.Key.Row,
                        ColumnNo = t.Key.Column,
                        FromBuchtNo = context.ADSLPAPPorts.Where(a => a.PAPInfoID == t.Key.PAPID && a.CenterID == t.Key.CenterID && a.RowNo.ToString() == t.Key.Row && a.ColumnNo.ToString() == t.Key.Column).OrderBy(a => a.BuchtNo).ToList().FirstOrDefault().BuchtNo.ToString(),
                        ToBuchtNo = context.ADSLPAPPorts.Where(a => a.PAPInfoID == t.Key.PAPID && a.CenterID == t.Key.CenterID && a.RowNo.ToString() == t.Key.Row && a.ColumnNo.ToString() == t.Key.Column).OrderByDescending(a => a.BuchtNo).ToList().FirstOrDefault().BuchtNo.ToString(),
                        Free = context.ADSLPAPPorts.Where(a => a.PAPInfoID == t.Key.PAPID && a.CenterID == t.Key.CenterID && a.RowNo.ToString() == t.Key.Row && a.ColumnNo.ToString() == t.Key.Column && (a.Status == (byte)DB.ADSLPAPPortStatus.Free || a.Status == (byte)DB.ADSLPAPPortStatus.Discharge)).ToList().Count.ToString(),
                        Connect = context.ADSLPAPPorts.Where(a => a.PAPInfoID == t.Key.PAPID && a.CenterID == t.Key.CenterID && a.RowNo.ToString() == t.Key.Row && a.ColumnNo.ToString() == t.Key.Column && a.Status == (byte)DB.ADSLPAPPortStatus.Instal).ToList().Count.ToString(),
                        Reserve = context.ADSLPAPPorts.Where(a => a.PAPInfoID == t.Key.PAPID && a.CenterID == t.Key.CenterID && a.RowNo.ToString() == t.Key.Row && a.ColumnNo.ToString() == t.Key.Column && a.Status == (byte)DB.ADSLPAPPortStatus.Reserve).ToList().Count.ToString(),
                        Broken = context.ADSLPAPPorts.Where(a => a.PAPInfoID == t.Key.PAPID && a.CenterID == t.Key.CenterID && a.RowNo.ToString() == t.Key.Row && a.ColumnNo.ToString() == t.Key.Column && a.Status == (byte)DB.ADSLPAPPortStatus.Broken).ToList().Count.ToString(),
                    }).Skip(startRowIndex).Take(pageSize).ToList();
                return x;
            }
        }

        public static int SearchPAPBuchtsGroupCount(
            List<int> papInfoIDs,
            List<int> centerIDs,
            long rowNo,
            long columnNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.ADSLPAPPorts
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (rowNo == -1 || rowNo == t.RowNo) &&
                                (columnNo == -1 || columnNo == t.ColumnNo)).GroupBy(t => new
                                {
                                    Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                                    CenterID = t.CenterID,
                                    PAP = t.PAPInfo.Title,
                                    PAPID = t.PAPInfoID,
                                    Row = t.RowNo.ToString(),
                                    Column = t.ColumnNo.ToString()
                                }).Count();
                return x;
            }
        }

        public static ADSLPAPPort GetADSLPAPPortById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static PAPPortInfo GetPAPPortInfoByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts.Where(t => t.ID == id)
                                           .Select(t => new PAPPortInfo
                                           {
                                               ID = t.ID,
                                               PAPInfo = t.PAPInfo.Title,
                                               Center = t.Center.CenterName,
                                               PortNo = t.PortNo.ToString(),
                                               TelephoneNo = t.TelephoneNo.ToString(),
                                               Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPPortStatus), t.Status)
                                           })
                                           .SingleOrDefault();
            }
        }

        public static List<PAPPortInfo> SearchADSLPAPPortForWeb(
            int papId,
            List<int> centerIDs,
            int centerID,
            long telephoneNo,
            long portNo,
            int status
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts
                    .Where(t => (centerID == 0 || t.CenterID == centerID) &&
                                (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                (papId == -1 || t.PAPInfoID == papId) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                (portNo == -1 || t.PortNo == portNo) &&
                                (status == -1 || t.Status == status))
                    .OrderByDescending(t => t.PAPInfoID).ThenBy(t => t.CenterID).ThenBy(t => t.PortNo)
                    .Select(t => new PAPPortInfo
                    {
                        ID = t.ID,
                        PAPInfo = t.PAPInfo.Title,
                        Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        PortNo = t.PortNo.ToString(),
                        TelephoneNo = (t.TelephoneNo.ToString() == null) ? "" : t.TelephoneNo.ToString(),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPPortStatus), t.Status)
                    }).ToList();
            }
        }

        public static List<PAPPortInfo> SearchADSLPAPBuchtForWeb(
            int papId,
            List<int> centerIDs,
            int centerID,
            string telephoneNo,
            long rowNo,
            long columnNo,
            long buchtNo,
            int status
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts
                    .Where(t => (centerID == 0 || t.CenterID == centerID) &&
                                (centerIDs.Contains(t.CenterID)) &&
                                (papId == -1 || t.PAPInfoID == papId) &&
                                (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (rowNo == -1 || t.RowNo == rowNo) &&
                                (columnNo == -1 || t.ColumnNo == columnNo) &&
                                (buchtNo == -1 || t.BuchtNo == buchtNo) &&
                                (status == -1 || t.Status == status))
                    .OrderByDescending(t => t.PAPInfoID).ThenBy(t => t.CenterID).ThenBy(t => t.PortNo)
                    .Select(t => new PAPPortInfo
                    {
                        ID = t.ID,
                        PAPInfo = t.PAPInfo.Title,
                        Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        RowNo = t.RowNo.ToString(),
                        ColumnNo = t.ColumnNo.ToString(),
                        BuchtNo = t.BuchtNo.ToString(),
                        TelephoneNo = (t.TelephoneNo.ToString() == null) ? "" : t.TelephoneNo.ToString(),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPPortStatus), t.Status)
                    }).ToList();
            }
        }

        public static bool GetPortStatus(int papInfoID, long portNo, int centerID)
        {
            bool isResulet;
            using (MainDataContext context = new MainDataContext())
            {
                byte status = context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.PortNo == portNo && t.CenterID == centerID).SingleOrDefault().Status;
                switch (status)
                {
                    case (byte)DB.ADSLPAPPortStatus.Free:
                        isResulet = true;
                        break;

                    case (byte)DB.ADSLPAPPortStatus.Instal:
                        isResulet = false;
                        break;

                    case (byte)DB.ADSLPAPPortStatus.Discharge:
                        isResulet = true;
                        break;

                    case (byte)DB.ADSLPAPPortStatus.Broken:
                        isResulet = false;
                        break;

                    default:
                        isResulet = false;
                        break;
                }

                return isResulet;
            }
        }

        public static bool GetBuchtStatus(int papInfoID, int rowNo, int columnNo, long buchtNo, int centerID)
        {
            bool isResulet;
            using (MainDataContext context = new MainDataContext())
            {
                byte status = context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.RowNo == rowNo && t.ColumnNo == columnNo && t.BuchtNo == buchtNo && t.CenterID == centerID).SingleOrDefault().Status;
                switch (status)
                {
                    case (byte)DB.ADSLPAPPortStatus.Free:
                        isResulet = true;
                        break;

                    case (byte)DB.ADSLPAPPortStatus.Instal:
                        isResulet = false;
                        break;

                    case (byte)DB.ADSLPAPPortStatus.Discharge:
                        isResulet = true;
                        break;

                    case (byte)DB.ADSLPAPPortStatus.Broken:
                        isResulet = false;
                        break;

                    case (byte)DB.ADSLPAPPortStatus.Reserve:
                        isResulet = false;
                        break;

                    default:
                        isResulet = false;
                        break;
                }

                return isResulet;
            }
        }

        public static bool HasPAPPort(int papInfoID, long portNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.PortNo == portNo && t.CenterID == centerID).SingleOrDefault();
                if (port == null)
                    return false;
                else
                    return true;
            }
        }

        public static bool HasPAPBucht(int papInfoID, int rowNo, int columnNo, long buchtNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.RowNo == rowNo && t.ColumnNo == columnNo && t.BuchtNo == buchtNo && t.CenterID == centerID).SingleOrDefault();
                if (port == null)
                    return false;
                else
                    return true;
            }
        }

        public static bool HasTelephoneBucht(long telephoneNo, int papInfoID, int rowNo, int columnNo, long buchtNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort bucht = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.PAPInfoID == papInfoID && t.RowNo == rowNo && t.ColumnNo == columnNo && t.BuchtNo == buchtNo && t.CenterID == centerID).SingleOrDefault();
                if (bucht == null)
                    return true;
                else
                    return false;
            }
        }

        public static bool HasPAPTelephone(long telephoneNo, int papInfoID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.PAPInfoID == papInfoID && t.CenterID == centerID).SingleOrDefault();
                if (port == null)
                    return false;
                else
                    return true;
            }
        }

        public static long GetTelephonePortNo(long telephoneNo, int papInfoID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return (long)context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.PAPInfoID == papInfoID && t.CenterID == centerID).SingleOrDefault().PortNo;
            }
        }

        public static ADSLPAPPort GetADSLPAPPortByPortNoAndCenter(int papInfoID, long portNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.PortNo == portNo && t.CenterID == centerID).SingleOrDefault();
            }
        }

        public static ADSLPAPPort GetADSLPAPPortByBuchtNoAndCenter(int papInfoID, int rowNo, int columnNo, long buchtNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.RowNo == rowNo && t.ColumnNo == columnNo && t.BuchtNo == buchtNo && t.CenterID == centerID).SingleOrDefault();
                if (port != null)
                    return port;
                else
                    return null;
            }
        }

        public static int GetCity(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPAPPorts.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static string GetActiveADSLPAPbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string papName = string.Empty;

                ADSLPAPPort aDSLPAP = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.Status == (byte)DB.ADSLPAPPortStatus.Instal).SingleOrDefault();

                if (aDSLPAP != null)
                {
                    PAPInfo pap = PAPInfoDB.GetPAPInfoByID(aDSLPAP.PAPInfoID);
                    if (pap != null)
                        papName = pap.Title;
                }

                return papName;
            }
        }

        public static bool HasADSLbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLPAPPort> portList = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.Status == (byte)DB.ADSLPAPPortStatus.Instal).ToList();

                if (portList != null && portList.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public static ADSLPAPPort GetADSLBuchtbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.Status == (byte)DB.ADSLPAPPortStatus.Instal).SingleOrDefault();

                if (port != null)
                    return port;
                else
                    return null;
            }
        }

        public static List<CheckableItem> GetADSLPAPBuchtRowbyPAPInfoID(int papInfoID, int centerID, long? portID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (portID != null)
                {
                    return context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.CenterID == centerID && (t.Status == (byte)DB.ADSLPAPPortStatus.Free || t.Status == (byte)DB.ADSLPAPPortStatus.Discharge) || t.ID == portID)
                                                .Select(t => new CheckableItem
                                                {
                                                    ID = (int)t.RowNo,
                                                    Name = t.RowNo.ToString(),
                                                    IsChecked = false
                                                }).Distinct().OrderBy(t => t.ID).ToList();
                }
                else
                {
                    return context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.CenterID == centerID && (t.Status == (byte)DB.ADSLPAPPortStatus.Free || t.Status == (byte)DB.ADSLPAPPortStatus.Discharge))
                                            .Select(t => new CheckableItem
                                            {
                                                ID = (int)t.RowNo,
                                                Name = t.RowNo.ToString(),
                                                IsChecked = false
                                            }).Distinct().OrderBy(t => t.ID).ToList();
                }
            }
        }

        public static List<CheckableItem> GetADSLPAPBuchtColumnbyPAPInfoID(int papInfoID, int centerID, int rowNo, long? portID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (portID != null)
                {
                    return context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.CenterID == centerID && t.RowNo == rowNo && (t.Status == (byte)DB.ADSLPAPPortStatus.Free || t.Status == (byte)DB.ADSLPAPPortStatus.Discharge) || t.ID == portID)
                                               .Select(t => new CheckableItem
                                               {
                                                   ID = (int)t.ColumnNo,
                                                   Name = t.ColumnNo.ToString(),
                                                   IsChecked = false
                                               }).Distinct().OrderBy(t => t.ID).ToList();
                }
                else
                {
                    return context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.CenterID == centerID && t.RowNo == rowNo && (t.Status == (byte)DB.ADSLPAPPortStatus.Free || t.Status == (byte)DB.ADSLPAPPortStatus.Discharge))
                                               .Select(t => new CheckableItem
                                               {
                                                   ID = (int)t.ColumnNo,
                                                   Name = t.ColumnNo.ToString(),
                                                   IsChecked = false
                                               }).Distinct().OrderBy(t => t.ID).ToList();
                }
            }
        }

        public static List<CheckableItem> GetADSLPAPBuchtBuchtbyPAPInfoID(int papInfoID, int centerID, int rowNo, int columnNo, long? portID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (portID != null)
                {
                    return context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.CenterID == centerID && t.RowNo == rowNo && t.ColumnNo == columnNo && (t.Status == (byte)DB.ADSLPAPPortStatus.Free || t.Status == (byte)DB.ADSLPAPPortStatus.Discharge) || t.ID == portID)
                                               .Select(t => new CheckableItem
                                               {
                                                   ID = (int)t.BuchtNo,
                                                   Name = t.BuchtNo.ToString(),
                                                   IsChecked = false
                                               }).Distinct().OrderBy(t => t.ID).ToList();
                }
                else
                {
                    return context.ADSLPAPPorts.Where(t => t.PAPInfoID == papInfoID && t.CenterID == centerID && t.RowNo == rowNo && t.ColumnNo == columnNo && (t.Status == (byte)DB.ADSLPAPPortStatus.Free || t.Status == (byte)DB.ADSLPAPPortStatus.Discharge))
                                               .Select(t => new CheckableItem
                                               {
                                                   ID = (int)t.BuchtNo,
                                                   Name = t.BuchtNo.ToString(),
                                                   IsChecked = false
                                               }).Distinct().OrderBy(t => t.ID).ToList();
                }
            }
        }

        public static Bucht GetADSLBuchtbyPAPInfoID(int papInfoID, int centerID, int rowNo, int columnNo, int buchtNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Bucht bucht = null;

                bucht = context.Buchts.Where(t => t.PAPInfoID == papInfoID && t.BuchtTypeID == (byte)DB.BuchtType.ADSL && t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description == "ADSL" && t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID && t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo == rowNo && t.VerticalMDFRow.VerticalRowNo == columnNo && t.BuchtNo == buchtNo).SingleOrDefault();

                if (bucht != null)
                    return bucht;
                else
                    return null;
            }
        }

        public static List<PAPTechnicalReportInfo> GetPAPTechnicalReportInfoList(List<int> centerIDs, List<int> papIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, Telephone = t })
                                     .Join(context.ADSLPAPPorts, t => t.Telephone.TelephoneNo, p => p.TelephoneNo, (t, p) => new { telephone = t, port = p })
                                     .Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.port.CenterID)) &&
                                                (!fromDate.HasValue || t.port.InstallDate >= fromDate) &&
                                                (!toDate.HasValue || t.port.InstallDate <= toDate) &&
                                                (papIDs.Count == 0 || papIDs.Contains(t.port.PAPInfoID)) &&
                                                (t.port.Status == (byte)DB.ADSLPAPPortStatus.Instal)).OrderBy(t => t.port.PAPInfoID)
                                     .Select(t => new PAPTechnicalReportInfo
                                     {
                                         Center = t.port.Center.Region.City.Name + " : " + t.port.Center.CenterName,
                                         TelephoneNo = t.port.TelephoneNo.ToString(),
                                         Cabinet = t.telephone.bucht.CabinetInput.Cabinet.CabinetNumber,
                                         CabinetInput = t.telephone.bucht.CabinetInput.InputNumber,
                                         Post = t.telephone.bucht.PostContact.Post.Number,
                                         PostConnection = t.telephone.bucht.PostContact.ConnectionNo,
                                         Bucht = "ردیف:" + t.telephone.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " طبقه:" + t.telephone.bucht.VerticalMDFRow.VerticalRowNo.ToString() + " اتصالی: " + t.telephone.bucht.BuchtNo.ToString(),
                                         BuchtRow = t.telephone.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                         BuchtColumn = t.telephone.bucht.VerticalMDFRow.VerticalRowNo,
                                         BuchtBucht = (int)t.telephone.bucht.BuchtNo,
                                         Port = "ردیف:" + t.port.RowNo + " طبقه:" + t.port.ColumnNo + " اتصالی:" + t.port.BuchtNo,
                                         PortRow = (int)t.port.RowNo,
                                         PortColumn = (int)t.port.ColumnNo,
                                         PortBucht = (int)t.port.BuchtNo,
                                         InstallDate = Date.GetPersianDate(t.port.InstallDate, Date.DateStringType.Short),
                                         PAPName = t.port.PAPInfo.Title
                                     }).ToList();
            }
        }

        public static bool HasFreePortbyCenterIDandPAPID(int papID, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLPAPPort> list = context.ADSLPAPPorts.Where(t => t.PAPInfoID == papID && t.CenterID == centerID &&
                                                                         (t.Status == (byte)DB.ADSLPAPPortStatus.Free || t.Status == (byte)DB.ADSLPAPPortStatus.Discharge)).ToList();

                if (list != null && list.Count != 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool HasADSLPAPPortbyTelephoneNo(long telephoneNo, int papID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.PAPInfoID == papID).SingleOrDefault();

                if (port != null)
                    return true;
                else
                    return false;
            }
        }

        public static int GetADSLIRANPAPIDKermanshah(int papID)
        {
            int papElkaID = 0;
            switch (papID)
            {
                case 1:
                    papElkaID = 12;
                    break;
                case 2:
                    papElkaID = 7;
                    break;
                case 3:
                    papElkaID = 5;
                    break;
                case 4:
                    papElkaID = 8;
                    break;
                case 5:
                    papElkaID = 1;
                    break;
                case 6:
                    papElkaID = 4;
                    break;
                case 7:
                    papElkaID = 11;
                    break;
                case 9:
                    papElkaID = 10;
                    break;
                case 11:
                    papElkaID = 3;
                    break;
                case 16:
                    papElkaID = 12;
                    break;
                case 18:
                    papElkaID = 12;
                    break;
                case 20:
                    papElkaID = 6;
                    break;
                default:
                    break;
            }

            return papElkaID;
        }
    }
}
