using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CRM.Data
{
    public static class ADSLPortDB
    {
        public static ObservableCollection<ADSLPortsInfo> GetPortsInfoByADSLEquipmentID(List<int> ADSLEquipmentIDList, string portNo, string telephoneNo, string address, List<int> ADSLPortStatus)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return new ObservableCollection<ADSLPortsInfo>(
                    context.ADSLPorts
                    .Where(t => (ADSLEquipmentIDList.Count == 0 || ADSLEquipmentIDList.Contains((int)t.ADSLEquipmentID)) &&
                                (string.IsNullOrEmpty(portNo) || t.PortNo == portNo) &&
                                (string.IsNullOrEmpty(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                (string.IsNullOrEmpty(address) || t.Address == address) &&
                                (ADSLPortStatus.Count == 0 || ADSLPortStatus.Contains(t.Status)))
                    .Select(t => new ADSLPortsInfo
                    {
                        EquipmentName = t.ADSLEquipment.Equipment,
                        PortID = t.ID,
                        PortNo = t.PortNo,
                        CenterName = t.ADSLEquipment.Center.CenterName,
                        //Address=t.Address,
                        //TelephoneNo = t.Bucht.SwitchPort.Telephones.Select(tel => tel.TelephoneNo).Take(1).SingleOrDefault(),
                        //InputBucht = t.InputBucht,
                        //OutBucht = t.OutBucht,
                        //InputConnection = DB.GetConnectionByBuchtID(t.InputBucht),
                        //OutputConnection = DB.GetConnectionByBuchtID(t.OutBucht),
                        StatusID = t.Status
                    }
                    ));
            }
        }

        public static ADSLPortInfo GetADSlPortInfoByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => t.ID == id)
                 .Select(t => new ADSLPortInfo
                 {
                     ID = t.ID,
                     Port = "ردیف : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.Bucht.BuchtNo.ToString(),
                     MDFTitle = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo + " - " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                     Center = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.City.Name + " : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.CenterName,
                     Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), t.Status),
                     //InputBucht = (long)t.InputBucht,
                     //OutputBucht = (long)t.OutBucht,
                     //InputBuchtConnection = DB.GetConnectionByBuchtID((long)t.InputBucht),
                     //OutputBuchtConnection = DB.GetConnectionByBuchtID((long)t.OutBucht)
                 }).SingleOrDefault();
            }
        }

        public static ADSLPortsInfo GetADSlPortsInfoByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => t.ID == id)
                 .Select(t => new ADSLPortsInfo
                 {
                     ID = t.ID,
                     PortNo = "ردیف : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.Bucht.BuchtNo.ToString(),
                     MDFTitle = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo + " - " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                     Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), t.Status),
                     //InputBucht = (long)t.InputBucht,
                     //OutputBucht = (long)t.OutBucht,
                     //InputBuchtConnection = DB.GetConnectionByBuchtID((long)t.InputBucht),
                     //OutputBuchtConnection = DB.GetConnectionByBuchtID((long)t.OutBucht)
                 }).SingleOrDefault();
            }
        }

        public static ADSLPort GetADSlPortByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<ADSLPortsInfo> GetADSLPortInfo10(int centerID, long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int mDFResultID = 0;
                List<ADSLMDFInfo> mDFsList = context.Buchts.Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID) && (t.BuchtTypeID == (byte)DB.BuchtType.ADSL)).Select(t => new ADSLMDFInfo { ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID }).Distinct().ToList();

                foreach (ADSLMDFInfo currentMDF in mDFsList)
                {
                    List<ADSLMDFRange> mDFRangeList = context.ADSLMDFRanges.Where(t => t.MDFID == (int)currentMDF.ID).ToList();

                    foreach (ADSLMDFRange range in mDFRangeList)
                    {
                        if (telephoneNo >= range.StartTelephoneNo && telephoneNo <= range.EndTelephoneNo)
                        {
                            mDFResultID = (int)currentMDF.ID;
                            break;
                        }
                        else
                            mDFResultID = 0;
                    }

                    if (mDFResultID != 0)
                        break;
                }

                return context.ADSLPorts.Where(t => t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDFResultID && t.Status == (byte)DB.ADSLPortStatus.Free)
                  .Select(t => new ADSLPortsInfo
                  {
                      ID = t.ID,
                      PortNo = "ردیف : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.Bucht.BuchtNo.ToString(),
                      StatusID = t.Status,
                      Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), t.Status),
                      //EquipmentName = t.ADSLEquipment.Equipment,
                      //InputBucht = t.InputBucht,
                      //OutBucht = t.OutBucht,
                      //InputBuchtConnection = DB.GetConnectionByBuchtID(t.InputBucht),
                      //OutputBuchtConnection = DB.GetConnectionByBuchtID(t.OutBucht)
                  }).Take(10).ToList();
            }
        }

        public static List<ADSLPortsInfo> GetADSLPortsInfoByEquipmentID(int ADSLEquipmentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => t.ADSLEquipmentID == ADSLEquipmentID)
                 .Select(t => new ADSLPortsInfo
                 {
                     ID = t.ID,
                     PortNo = t.PortNo,
                     StatusID = t.Status,
                     Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), t.Status),
                     EquipmentName = t.ADSLEquipment.Equipment
                 }).ToList();
            }
        }

        public static List<ADSLPort> GetADSLPortsByEquipmentID(int ADSLEquipmentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => t.ADSLEquipmentID == ADSLEquipmentID).ToList();
            }
        }

        public static List<ADSLPort> GetADSLPortsByEquipmentIDandStatus(int ADSLEquipmentID, byte status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => (t.ADSLEquipmentID == ADSLEquipmentID) && (t.Status == status) /*&& (t.InputBucht != null) && (t.OutBucht != null)*/).ToList();
            }
        }

        public static List<ADSLPortsInfo> GetADSLPortsInfoByEquipmentIDandStatus(int ADSLEquipmentID, byte status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => (t.ADSLEquipmentID == ADSLEquipmentID) && (t.Status == status) /* && (t.InputBucht != null) && (t.OutBucht != null)*/)
                 .Select(t => new ADSLPortsInfo
                 {
                     ID = t.ID,
                     PortNo = t.PortNo,
                     StatusID = t.Status,
                     Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), t.Status),
                     EquipmentName = t.ADSLEquipment.Equipment
                 })
                 .ToList();
            }
        }

        public static List<ADSLPortsInfo> GetADSLPortsInfoByColumnIDandStatus(int centerID, int rowID, int columnID, byte status, long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int mDFResultID = 0;
                List<ADSLMDFInfo> mDFsList = context.Buchts.Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID) && (t.BuchtTypeID == (byte)DB.BuchtType.ADSL)).Select(t => new ADSLMDFInfo { ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID }).Distinct().ToList();

                foreach (ADSLMDFInfo currentMDF in mDFsList)
                {
                    List<ADSLMDFRange> mDFRangeList = context.ADSLMDFRanges.Where(t => t.MDFID == (int)currentMDF.ID).ToList();

                    foreach (ADSLMDFRange range in mDFRangeList)
                    {
                        if (telephoneNo >= range.StartTelephoneNo && telephoneNo <= range.EndTelephoneNo)
                        {
                            mDFResultID = (int)currentMDF.ID;
                            break;
                        }
                        else
                            mDFResultID = 0;
                    }

                    if (mDFResultID != 0)
                        break;
                }

                return context.ADSLPorts.Where(t => (t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDFResultID && t.Status == (byte)DB.ADSLPortStatus.Free) &&
                                                    (t.Bucht.VerticalMDFRow.VerticalMDFColumnID == rowID) &&
                                                    (t.Bucht.MDFRowID == columnID)).GroupBy(t => t.Bucht.BuchtNo)
                                        .Select(group => group.First())
                                        .Select(t => new ADSLPortsInfo
                                        {
                                            ID = t.ID,
                                            PortNo = "ردیف : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.Bucht.BuchtNo.ToString(),
                                            StatusID = t.Status,
                                            Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), t.Status),
                                            //EquipmentName = t.ADSLEquipment.Equipment,
                                            //InputBucht = t.InputBucht,
                                            //OutBucht = t.OutBucht,
                                            //InputBuchtConnection = DB.GetConnectionByBuchtID(t.InputBucht),
                                            //OutputBuchtConnection = DB.GetConnectionByBuchtID(t.OutBucht)
                                        }).ToList();
            }
        }

        public static ADSLPortsInfo GetADSlPortFullInfo(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts
                 .Where(t => t.ID == id)
                 .Select(t => new ADSLPortsInfo
                 {
                     ID = t.ID,
                     CenterID = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                     Center = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.CenterName,
                     //EquipmentID = (int)t.ADSLEquipmentID,
                     //EquipmentName = t.ADSLEquipment.Equipment,
                     //EquipmentType = DB.GetEnumDescriptionByValue(typeof(DB.ADSLEquimentType), (int)t.ADSLEquipment.EquipmentType),
                     //LocationInstall = DB.GetEnumDescriptionByValue(typeof(DB.ADSLEquimentLocationInstall), (int)t.ADSLEquipment.LocationInstall),
                     //Product = DB.GetEnumDescriptionByValue(typeof(DB.ADSLEquimentProduct), (int)t.ADSLEquipment.Product),
                     //Address = t.Address,
                     PortNo = "ردیف : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.Bucht.BuchtNo.ToString(),
                     StatusID = t.Status,
                     Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPortStatus), (int)t.Status),
                     TelephoneNo = t.TelephoneNo,
                     InstalADSLDate = Date.GetPersianDate(t.InstalADSLDate, Date.DateStringType.Short)
                 }).SingleOrDefault();
            }
        }

        public static List<ADSLPort> GetFreePortListbyeCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts.Where(t => t.Status == (byte)DB.ADSLPortStatus.Free && t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID).ToList();
            }
        }

        public static List<CheckableItem> GetADSLPortCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts.Select(t => new CheckableItem
                {
                    ID = Convert.ToInt32(t.ID),
                    Name = t.PortNo,
                    IsChecked = false
                })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLPortRowCheckable(int centerID, long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int mDFResultID = 0;
                List<ADSLMDFInfo> mDFsList = context.Buchts.Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID) && (t.BuchtTypeID == (byte)DB.BuchtType.ADSL)).Select(t => new ADSLMDFInfo { ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID }).Distinct().ToList();

                foreach (ADSLMDFInfo currentMDF in mDFsList)
                {
                    List<ADSLMDFRange> mDFRangeList = context.ADSLMDFRanges.Where(t => t.MDFID == (int)currentMDF.ID).ToList();

                    foreach (ADSLMDFRange range in mDFRangeList)
                    {
                        if (telephoneNo >= range.StartTelephoneNo && telephoneNo <= range.EndTelephoneNo)
                        {
                            mDFResultID = (int)currentMDF.ID;
                            break;
                        }
                        else
                            mDFResultID = 0;
                    }

                    if (mDFResultID != 0)
                        break;
                }

                return context.Buchts
                    .Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDFResultID))
                    .Select(t => new CheckableItem
                    {
                        ID = t.VerticalMDFRow.VerticalMDFColumn.ID,
                        Name = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                        IsChecked = false
                    })
                    .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<CheckableItem> GetADSLPortColumnCheckable(int centerID, int rowNO, long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int mDFResultID = 0;
                List<ADSLMDFInfo> mDFsList = context.Buchts.Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID) && (t.BuchtTypeID == (byte)DB.BuchtType.ADSL)).Select(t => new ADSLMDFInfo { ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID }).Distinct().ToList();

                foreach (ADSLMDFInfo currentMDF in mDFsList)
                {
                    List<ADSLMDFRange> mDFRangeList = context.ADSLMDFRanges.Where(t => t.MDFID == (int)currentMDF.ID).ToList();

                    foreach (ADSLMDFRange range in mDFRangeList)
                    {
                        if (telephoneNo >= range.StartTelephoneNo && telephoneNo <= range.EndTelephoneNo)
                        {
                            mDFResultID = (int)currentMDF.ID;
                            break;
                        }
                        else
                            mDFResultID = 0;
                    }

                    if (mDFResultID != 0)
                        break;
                }

                return context.Buchts
                    .Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDFResultID) &&
                                (t.VerticalMDFRow.VerticalMDFColumnID == rowNO))
                    .Select(t => new CheckableItem
                    {
                        ID = t.VerticalMDFRow.ID,
                        Name = t.VerticalMDFRow.VerticalRowNo.ToString(),
                        IsChecked = false
                    })
                    .Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static ADSLPort GetADSLPortByID(long Id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPorts.Where(t => t.ID == Id).SingleOrDefault();
            }
        }

        public static bool HasADSLPortbyTelephoneNo(long telephoneNo)
        {
            bool result = true;

            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLPort> portList = context.ADSLPorts.Where(t => t.TelephoneNo == telephoneNo).ToList();

                if (portList.Count == 0)
                    result = false;
                else
                    result = true;
            }

            return result;
        }
    }
}
