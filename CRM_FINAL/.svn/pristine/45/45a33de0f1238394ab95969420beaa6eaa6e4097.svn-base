using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
   public static  class PCMDeviceDB
    {

       public static List<CheckableItem> GetPCMDeviceCheckable()
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.PCMDevices.Select(t => new CheckableItem { ID = t.ID, Name = t.MUID.ToString(), IsChecked = false }).ToList();
           }
       }



       public static List<PCMInfo> GetPCMDeviceByCenterID(int centerId)
       {
           using (MainDataContext context = new MainDataContext())
           {
               var x = context.Buchts.Where(b => b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerId && b.BuchtTypeID == (byte)DB.BuchtType.InLine).Select(b => b.ID).ToList();

               return context.PCMDevices.
                   Where(p => x.Contains(p.BuchtID ?? -1))
                                        .Select(p => new PCMInfo
                                        {
                                            CenterID = centerId,
                                            MUID = p.MUID,
                                            PCMTypeID = p.PCMTypeID,
                                            // PCMTypeName = p.PCMType.PCMTypeName,
                                            Status = p.Status,
                                            ID = p.ID,

                                        }).Distinct()
                                        .ToList();
           }
       }

       public static List<PCMInfo> GetPCMInfoByID(long ID)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.PCMDevices.
                   Where(p => p.ID == ID)
                                        .Select(p => new PCMInfo
                                        {
                                            MUID = p.MUID,
                                            PCMTypeID = p.PCMTypeID,
                                            // PCMTypeName = p.PCMType.PCMTypeName,
                                            Status = p.Status,
                                            ID = p.ID,

                                        }).Distinct()
                                        .ToList();
           }
       }


        //public static List<PCMDevice> SearchPCMDevice(List<int> status, List<int> pCMType, string mUID, string installPostalCode, string installAddress)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PCMDevices
        //            .Where(t =>
        //                    (status.Count == 0 || status.Contains(t.Status)) &&
        //                    (pCMType.Count == 0 || pCMType.Contains(t.PCMTypeID)) &&
        //                    (string.IsNullOrWhiteSpace(mUID) || t.MUID.Contains(mUID)) &&
        //                    (string.IsNullOrWhiteSpace(installPostalCode) || t.InstallPostalCode.Contains(installPostalCode)) &&
        //                    (string.IsNullOrWhiteSpace(installAddress) || t.InstallAddress.Contains(installAddress))
        //                  )
        //            .ToList();
        //    }
        //}

        //public static PCMDeviceInfo GetPCMDeviceByID(int id)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PCMDevices.Where(t => t.ID == id).Select(t => new PCMDeviceInfo
        //        {
        //            ID = t.ID,
        //            BuchtID = t.BuchtID,
        //            PCMTypeID = t.PCMTypeID,
        //            LineNumber = t.LineNumber,
        //            MUID = t.MUID,
        //            InstallPostalCode = t.InstallPostalCode,
        //            InstallAddress = t.InstallAddress,
        //            Status = t.Status,
        //        }).SingleOrDefault();
        //    }
        //}



        //public static RemoteConnection GetRemoteConnectionInfoByBuchtID(long BuchtID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Buchts.Where(t => t.ID == BuchtID)
        //                             .Select(t => new RemoteConnection
        //                             {
        //                                 BuchtNo = t.BuchtNo,
        //                                 Row = t.VerticalMDFRow.ID,
        //                                 Column = t.VerticalMDFRow.VerticalMDFColumnID
        //                             }).SingleOrDefault();
        //    }
        //}

        //public static List<PCMDevice> GetLinerNumberInfo(int PCMID)
        //{
        //    if (PCMID == 0)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        using (MainDataContext context = new MainDataContext())
        //        {
        //            return context.PCMDevices.Where(t => t.ID == PCMID).ToList();
        //        }
        //    }
        //}
        //public static List<CommunicationsLine> CommunicationsRemoteInfo(string mUID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PCMDevices.Where(t => t.MUID == mUID).Select(t => new CommunicationsLine
        //        {
        //            PCMDeviceID = t.ID,
        //            LineNumber = t.LineNumber,
        //            BuchtID = t.BuchtID,
        //            MUID = t.MUID,
        //            VerticalRowID = t.Bucht.MDFRowID,
        //            VerticalRowNo = t.Bucht.VerticalMDFRow.VerticalRowNo,
        //            BuchtType = t.Bucht.BuchtTypeID,
        //            BuchtNo = t.Bucht.BuchtNo,

        //            CableID = t.Bucht.CablePair.CableID,
        //            CablePairID = t.Bucht.CablePairID,
        //            CabinetID = t.Bucht.CabinetInput.CabinetID,
        //            PostID = t.Bucht.PostContact.PostID,
        //            ConnectionID = t.Bucht.ConnectionID,

        //            ConnectionIDBucht = t.Bucht.ConnectionIDBucht,
        //            PCMChannelNo = t.Bucht.PCMChannelNo,
        //            StatusBucht = t.Bucht.Status,

        //            VerticalColumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //            VerticalColumnID = t.Bucht.VerticalMDFRow.VerticalMDFColumnID
        //        }).ToList();
        //    }
        //}

        //public static List<CommunicationsLine> CommunicationInfoByPCMDeviceID(string mUID, int lineNumber, long buchtID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Buchts.Where(t => t.PCMDevice.MUID == mUID && t.PCMDevice.LineNumber == lineNumber && t.ID != buchtID).Select(t => new CommunicationsLine
        //        {
        //            PCMDeviceID = t.PCMDeviceID,
        //            PCMChannelNo = t.PCMChannelNo,
        //            VerticalRowID = t.MDFRowID,
        //            VerticalRowNo = t.VerticalMDFRow.VerticalRowNo,
        //            VerticalColumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //            VerticalColumnID = t.VerticalMDFRow.VerticalMDFColumnID,
        //            BuchtID = t.ID,
        //            BuchtType = t.BuchtTypeID,
        //            BuchtNo = t.BuchtNo,

        //        }).ToList();
        //    }
        //}

        //public static void Save(PCMDevice pCMDeviceData, List<CommunicationsLine> remoteCommunicationsLine)
        // {
        //     TODO: SubmintALL
        //     using (TransactionScope scope = new TransactionScope())
        //     {

        //         for (int i = 0; i <= remoteCommunicationsLine.Count - 1 && remoteCommunicationsLine[i].VerticalColumnID!=null ; i++)
        //         {
        //             pCMDeviceData.ID = 0;
        //             pCMDeviceData.LineNumber = remoteCommunicationsLine[i].LineNumber;
        //             pCMDeviceData.Detach();
        //             DB.Save(pCMDeviceData);

        //             Bucht bucht = BuchtDB.GetBuchetByID((long)remoteCommunicationsLine[i].BuchtID);
        //             if (remoteCommunicationsLine[i].ConnectionID != null)
        //             {
        //                 bucht.ID = (long)remoteCommunicationsLine[i].BuchtID;
        //                 bucht.ConnectionID = remoteCommunicationsLine[i].ConnectionID;
        //                 bucht.CabinetInputID = remoteCommunicationsLine[i].CabinetID??-1;
        //                 bucht.MDFRowID = remoteCommunicationsLine[i].VerticalRowID;
        //                 bucht.BuchtTypeID = 4;
        //                 bucht.PCMDeviceID = pCMDeviceData.ID;
        //                 bucht.Detach();
        //                 DB.Save(bucht);
        //             }

        //             else if (remoteCommunicationsLine[i].CablePairID != null)
        //             {

        //                 bucht.ID = (long)remoteCommunicationsLine[i].BuchtID;
        //                 bucht.ConnectionIDBucht = (long)remoteCommunicationsLine[i].BuchtID;
        //                 bucht.MDFRowID = remoteCommunicationsLine[i].VerticalRowID;
        //                 bucht.BuchtTypeID = 4;
        //                 bucht.CablePairID = (long)remoteCommunicationsLine[i].CablePairID;
        //                 bucht.PCMDeviceID = pCMDeviceData.ID;
        //                 bucht.Detach();
        //                 DB.Save(bucht);
        //             }

        //             pCMDeviceData.BuchtID = bucht.ID;
        //             pCMDeviceData.Detach();
        //             DB.Save(pCMDeviceData);
        //         }
        //         scope.Complete();
        //     }

            
           
        // }



        //public static void Update(List<PCMDevice> pCMDeviceList, object pCMDeviceDataContext, List<CommunicationsLine> remoteCommunicationsLine, List<CommunicationsLine> remoteCommunicationsLineOld)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        PCMDeviceInfo PCMDeviceInfo = pCMDeviceDataContext as PCMDeviceInfo;
        //        for (int i = 0; i <= pCMDeviceList.Count - 1; i++)
        //        {
        //            PCMDevice PCMDeviceList = pCMDeviceList[i];

        //            PCMDeviceList.InstallAddress = PCMDeviceInfo.InstallAddress;
        //            PCMDeviceList.InstallPostalCode = PCMDeviceInfo.InstallPostalCode;
        //            PCMDeviceList.MUID = PCMDeviceInfo.MUID;
        //            PCMDeviceList.PCMTypeID = PCMDeviceInfo.PCMTypeID;
        //            PCMDeviceList.Status = (byte)PCMDeviceInfo.Status;
        //            PCMDeviceList.Detach();
        //            DB.Save(PCMDeviceList);
        //        }
        //        for (int i = 0; i <= remoteCommunicationsLine.Count - 1; i++)
        //        {
        //            if (remoteCommunicationsLine[i].PCMDeviceID == null)
        //            {
        //                PCMDevice PCMDeviceNew = new PCMDevice();
        //                PCMDeviceNew.ID = 0;
        //                PCMDeviceNew.LineNumber = remoteCommunicationsLine[i].LineNumber;
        //                PCMDeviceNew.InstallAddress = PCMDeviceInfo.InstallAddress;
        //                PCMDeviceNew.InstallPostalCode = PCMDeviceInfo.InstallPostalCode;
        //                PCMDeviceNew.MUID = PCMDeviceInfo.MUID;
        //                PCMDeviceNew.Status = (byte)PCMDeviceInfo.Status;
        //                PCMDeviceNew.PCMTypeID = PCMDeviceInfo.PCMTypeID;
        //                PCMDeviceNew.Detach();
        //                DB.Save(PCMDeviceNew);

        //                Bucht bucht = BuchtDB.GetBuchetByID((long)remoteCommunicationsLine[i].BuchtID);
        //                if (remoteCommunicationsLine[i].ConnectionID != null)
        //                {
        //                    bucht.ID = (long)remoteCommunicationsLine[i].BuchtID;
        //                    bucht.ConnectionID = remoteCommunicationsLine[i].ConnectionID;
        //                    bucht.CabinetInputID = remoteCommunicationsLine[i].CabinetID ?? -1;
        //                    bucht.MDFRowID = remoteCommunicationsLine[i].VerticalRowID;
        //                    bucht.BuchtTypeID = 4;
        //                    bucht.PCMDeviceID = PCMDeviceNew.ID;
        //                    bucht.Detach();
        //                    DB.Save(bucht);
        //                }

        //                else if (remoteCommunicationsLine[i].CablePairID != null)
        //                {

        //                    bucht.ID = (long)remoteCommunicationsLine[i].BuchtID;
        //                    bucht.ConnectionIDBucht = (long)remoteCommunicationsLine[i].BuchtID;
        //                    bucht.MDFRowID = remoteCommunicationsLine[i].VerticalRowID;
        //                    bucht.BuchtTypeID = 4;
        //                    bucht.CablePairID = (long)remoteCommunicationsLine[i].CablePairID;
        //                    bucht.PCMDeviceID = PCMDeviceNew.ID;
        //                    bucht.Detach();
        //                    DB.Save(bucht);
        //                }

        //                PCMDeviceNew.BuchtID = bucht.ID;
        //                PCMDeviceNew.Detach();
        //                DB.Save(PCMDeviceNew);

        //            }
        //            else
        //            {
        //                PCMDevice PCMDeviceNew = Data.PCMDeviceDB.GetPCMDeviceByIDWithoutCountLineNumber(remoteCommunicationsLine[i].PCMDeviceID);


        //                if (remoteCommunicationsLine[i].ConnectionID != null)
        //                {
        //                    Bucht buchtOld = BuchtDB.GetBuchetByID((long)remoteCommunicationsLineOld[i].BuchtID);
        //                    buchtOld.ConnectionID = null;
        //                    buchtOld.CabinetInputID = null;
        //                    buchtOld.CablePairID = null;
        //                    buchtOld.PCMDeviceID = null;
        //                    buchtOld.ConnectionIDBucht = null;
        //                    buchtOld.BuchtTypeID = 255;
        //                    buchtOld.PCMDeviceID = null;
        //                    buchtOld.Detach();
        //                    DB.Save(buchtOld);

        //                    Bucht bucht = BuchtDB.GetBuchetByID((long)remoteCommunicationsLine[i].BuchtID);
        //                    bucht.ConnectionID = remoteCommunicationsLine[i].ConnectionID;
        //                    bucht.CabinetInputID = remoteCommunicationsLine[i].CabinetID ?? -1;
        //                    bucht.BuchtTypeID = 4;
        //                    bucht.PCMDeviceID = PCMDeviceNew.ID;
        //                    bucht.Detach();
        //                    DB.Save(bucht);
        //                    PCMDeviceNew.BuchtID = bucht.ID;
        //                }

        //                else if (remoteCommunicationsLine[i].CablePairID != null)
        //                {
        //                    Bucht buchtOld = BuchtDB.GetBuchetByID((long)remoteCommunicationsLineOld[i].BuchtID);
        //                    buchtOld.ConnectionID = null;
        //                    buchtOld.CabinetInputID = null;
        //                    buchtOld.CablePairID = null;
        //                    buchtOld.PCMDeviceID = null;
        //                    buchtOld.ConnectionIDBucht = null;
        //                    buchtOld.BuchtTypeID = 255;
        //                    buchtOld.PCMDeviceID = null;
        //                    buchtOld.Detach();
        //                    DB.Save(buchtOld);

        //                    Bucht bucht = BuchtDB.GetBuchetByID((long)remoteCommunicationsLine[i].BuchtID);
        //                    bucht.ConnectionIDBucht = (long)remoteCommunicationsLine[i].BuchtID;
        //                    bucht.BuchtTypeID = 4;
        //                    bucht.CablePairID = (long)remoteCommunicationsLine[i].CablePairID;
        //                    bucht.PCMDeviceID = PCMDeviceNew.ID;
        //                    bucht.Detach();
        //                    DB.Save(bucht);
        //                    PCMDeviceNew.BuchtID = bucht.ID;
        //                }


        //                PCMDeviceNew.Detach();
        //                DB.Save(PCMDeviceNew);

        //            }
        //        }
        //        scope.Complete();
        //    }



        //}

        //public static int GetCountLineByMuID(string mUID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PCMDevices.Where(t => t.MUID == mUID).Count();
        //    }
        //}

        //public static List<PCMDevice> GetPCMDeviceByMUID(string mUID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PCMDevices.Where(t => t.MUID == mUID).ToList();
        //    }
        //}
        //private static PCMDevice GetPCMDeviceByIDWithoutCountLineNumber(int? PCMDeviceID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PCMDevices.Where(t => t.ID == PCMDeviceID).SingleOrDefault();
        //    }
        //}

        //public static int GetCountConnectionByPCMDevice(int pCMDeviceID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Buchts.Where(t => t.PCMDeviceID == pCMDeviceID).ToList().Count();
        //    }
        //}

        //public static PCMDevice GetPCMDeviceByIDReturnPCMDevice(int PCMDevice)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PCMDevices.Where(t => t.ID == PCMDevice).SingleOrDefault();
        //    }
        //}
    }
#region CustomClass
   public class PCMInfo
   {
       public int CenterID { get; set; }
       public int ID { get; set; }
       public int PCMTypeID { get; set; }
       public bool UsageFor { get; set; }
       public string MUID { get; set; }
       public long? ConnectionID { get; set; }
       public byte? Status { get; set; }
       public string PCMTypeName { get; set; }
       public byte PCMActionID { get; set; }
      // public byte? NumberOFChannel { get; set; }
       public int PCMDeviceID { get; set; }
       public string Rock { get; set; }
       public string Shelf { get; set; }
       public string Card { get; set; }
       public string Port { get; set; }
    }

   public class RemoteConnection
   {
       public int Column { get; set; }
       public int Row { get; set; }
       public long BuchtNo { get; set; }
   }

    public class PCMDeviceInfo
    {
     
        public int ID{get;set;}
        public long? BuchtID{get;set;}
        public int PCMTypeID{get;set;}
        public byte? LineNumber{get;set;}
        public string MUID{get;set;}
        public string InstallPostalCode{get;set;}
        public string InstallAddress{get;set;}
        public byte Status{get;set;}
        public int CountLineNumber{get;set;}

    }
    public class CommunicationsLine
    {
        public int? PCMDeviceID { get; set; }
        public byte? LineNumber { get; set; }
        public long? BuchtID { get; set; }
        public string MUID { get; set; }
        public int VerticalRowID { get; set; }
        public int? VerticalRowNo { get; set; }
        public int BuchtType{get;set;}
        public long? BuchtNo{get;set;}

        public long? CableID { get; set; }
        public long? CablePairID{get;set;}
        public int? CabinetID{get;set;}
        public int? PostID { get; set; }
        public long? ConnectionID{get;set;}

        public long? ConnectionIDBucht{get;set;}
        public byte? PCMChannelNo{get;set;}
        public int? StatusBucht{get;set;}
        public int? VerticalColumnNo { get; set; }
        public int? VerticalColumnID { get; set; }
        public byte PCMChannalNO { get; set; }
    }
 #endregion CustomClass
}
