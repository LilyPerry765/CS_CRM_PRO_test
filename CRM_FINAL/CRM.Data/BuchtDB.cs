using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class BuchtDB
    {

        public static Bucht GetBuchetByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<BuchtInfo> GetCenterBuchts(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t =>
                               t.CablePair.Cable.CenterID == centerID && t.CabinetInput.CabinetID.ToString() != string.Empty
                           )
                     .Select(b => new BuchtInfo
                     {
                         BuchtID = b.ID,
                         BuchtNo = b.BuchtNo,
                         BuchtTypeID = b.BuchtTypeID,
                         Status = b.Status,
                         VerticalRowNo = b.VerticalMDFRow.VerticalRowNo,
                         VerticalCloumnNo = b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                         MDFID = b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                         MDFDescription = b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + "-" + b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                         CenterID = b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                         NormalConnectionNo = b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + "-" + b.VerticalMDFRow.VerticalRowNo + "-" + b.BuchtNo,
                         PCMConnectionNo = b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + "-" + b.VerticalMDFRow.VerticalRowNo + "-" + b.BuchtNo + "," + b.PortNo.ToString(),
                         CablePairID = b.CablePairID,
                         CablePairNumber = b.CablePair.CablePairNumber,
                         CabinetInputID = b.CabinetInputID,
                         InputNumber = b.CabinetInput.InputNumber,
                         CabinetID = b.CabinetInput.CabinetID,
                         CabinetNumber = b.CabinetInput.Cabinet.CabinetNumber.ToString(),//+b.CabinetInput.Cabinet.ABType.ToString() ?? ""
                         PCMDeviceID = b.PCMPortID,
                         MUID = b.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + "-" + b.PCMPort.PCM.PCMShelf.Number.ToString() + "-" + b.PCMPort.PCM.Card.ToString(),
                         PCMChannelNo = b.PortNo,
                     })
                            .OrderBy(t => t.BuchtID)
                            .ToList();
            }
        }

        public static List<Bucht> SearchBucht(List<int> status, List<int> reserveStatus, List<int> mdfRowId, List<int> cabinetId, long telphoneNo, long buchtNo, long pcmId, List<int> onuLinkId, long privatecableId, DateTime? reverseDuedate, List<int> buchtTypeId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t =>
                                (status.Count == 0 || status.Contains(t.Status)) &&
                                (reserveStatus.Count == 0 || reserveStatus.Contains(t.Status)) &&
                                (mdfRowId.Count == 0 || mdfRowId.Contains(t.MDFRowID)) &&
                                (cabinetId.Count == 0 || cabinetId.Contains(t.CabinetInput.CabinetID)) &&
                                (telphoneNo == -1 || telphoneNo == (context.Telephones.Where(s => s.TelephoneNo == telphoneNo && s.SwitchPortID == t.SwitchPortID).Select(s => s.TelephoneNo).Take(1).SingleOrDefault())) &&
                                (buchtNo == -1 || t.BuchtNo == buchtNo) &&
                                (pcmId == -1 || t.PCMPortID == pcmId) &&
                                (onuLinkId.Count == 0 || onuLinkId.Contains(t.CabinetInput.CabinetID)) &&
                                (privatecableId == -1 || t.CabinetInput.CabinetID == privatecableId) &&
                                (buchtTypeId.Count == 0 || buchtTypeId.Contains(t.BuchtTypeID))
                    //&& (!reverseDuedate.HasValue || t.ReserveDueDate == reverseDuedate)
                            ).OrderBy(t => t.ID)
                            .ToList();
            }
        }

        public static Bucht GetBuchetByID(long? id)
        {
            if (id == null) return null;
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<Bucht> SearchBucht(List<int> MdfRowId, byte buchtType, byte status, long connectionId, long buchtNo, byte pcmChannelNo, List<int> pcmDeviceId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t =>
                        (MdfRowId.Count == 0 || MdfRowId.Contains(t.MDFRowID)) &&
                        (buchtType == -1 || t.BuchtTypeID == buchtType) &&
                        (status == -1 || t.Status == status) &&
                        (connectionId == -1 || t.ConnectionID == connectionId) &&
                        (buchtNo == -1 || t.BuchtNo == buchtNo) &&
                        (pcmChannelNo == -1 || t.PortNo == pcmChannelNo) &&
                        (pcmDeviceId.Count == 0 || pcmDeviceId.Contains(t.PCMPortID.Value))

                        ).OrderBy(t => t.ID)
                        .ToList();



            }
        }

        public static List<BuchtInfo> GetBuchtInfoByID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.Buchts.Where(b => b.ID == buchtID)
                      .Select(b => new BuchtInfo
                      {
                          BuchtID = b.ID,
                          BuchtNo = b.BuchtNo,
                          BuchtTypeID = b.BuchtTypeID,
                          Status = b.Status,
                          VerticalRowNo = b.VerticalMDFRow.VerticalRowNo,
                          VerticalCloumnNo = b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                          MDFID = b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                          MDFDescription = b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + "-" + b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                          CenterID = b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                          NormalConnectionNo = b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + "-" + b.VerticalMDFRow.VerticalRowNo + "-" + b.BuchtNo,
                          PCMConnectionNo = b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + "-" + b.VerticalMDFRow.VerticalRowNo + "-" + b.BuchtNo + "," + b.PortNo.ToString(),
                          CablePairID = b.CablePairID,
                          CablePairNumber = b.CablePair.CablePairNumber,
                          CabinetInputID = b.CabinetInputID,
                          InputNumber = b.CabinetInput.InputNumber,
                          CabinetID = b.CabinetInput.CabinetID,
                          CabinetNumber = b.CabinetInput.Cabinet.CabinetNumber.ToString(),//+b.CabinetInput.Cabinet.ABType.ToString() ?? ""
                          PCMDeviceID = b.PCMPortID,
                          MUID = b.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + "-" + b.PCMPort.PCM.PCMShelf.Number.ToString() + "-" + b.PCMPort.PCM.Card.ToString(),
                          PCMChannelNo = b.PortNo,


                      }).ToList();


            }


        }

        public static string GetMDFByBuchtID(long BuchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(b => b.ID == BuchtID).Select(b => b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + "-" + b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description).SingleOrDefault();

            }
        }

        public static List<BuchtInfo> GetMDFBuchtByCenterID(int mdfID, int centerID, byte buchtType)
        {
            List<BuchtInfo> lst = new List<BuchtInfo>();
            using (MainDataContext context = new MainDataContext())
            {

                lst = context.MDFs.Join(context.MDFFrames, m => m.ID, mf => mf.MDFID, (m, mf) => new { mdf = m, farme = mf })
                             .Join(context.VerticalMDFColumns, f => f.farme.ID, vc => vc.MDFFrameID, (f, vc) => new { frame = f, col = vc })
                             .Join(context.VerticalMDFRows, v => v.col.ID, vr => vr.VerticalMDFColumnID, (column, vr) => new { column = column, row = vr })
                             .Join(context.Buchts, vr => vr.row.ID, b => b.MDFRowID, (vr, b) => new { bucht = b, row = vr })
                             .Where(x => x.row.column.frame.mdf.CenterID == centerID && x.row.column.frame.mdf.ID == mdfID && x.bucht.BuchtTypeID == buchtType)
                             .Select(x => new BuchtInfo
                             {
                                 BuchtID = x.bucht.ID,
                                 BuchtNo = x.bucht.BuchtNo,
                                 BuchtTypeID = x.bucht.BuchtTypeID,
                                 Status = x.bucht.Status,
                                 VerticalRowNo = x.row.row.VerticalRowNo,
                                 VerticalCloumnNo = x.row.column.col.VerticalCloumnNo,
                                 MDFID = x.row.column.frame.mdf.ID,
                                 //MDFDescription = x.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + "-" + b.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description,
                                 CenterID = x.row.column.frame.mdf.CenterID,
                                 NormalConnectionNo = x.row.column.col.VerticalCloumnNo.ToString() + "-" + x.row.row.VerticalRowNo.ToString() + "-" + x.bucht.BuchtNo.ToString(),
                                 PCMConnectionNo = x.row.column.col.VerticalCloumnNo.ToString() + "-" + x.row.row.VerticalRowNo.ToString() + "-" + x.bucht.BuchtNo.ToString() + "," + x.bucht.PortNo.ToString(),


                             }).ToList();




            }
            return lst;
        }

        public static List<Bucht> GetBuchtByCabinetIDandBuchtType(List<int> cabinetId, byte buchtType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(b => b.BuchtTypeID == buchtType && cabinetId.Contains(b.CabinetInput.CabinetID)).ToList();
            }
        }

        public static List<Bucht> GetBuchtByCabinetIDs(List<int> cabinetsId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(b => cabinetsId.Contains(b.CabinetInput.CabinetID)).ToList();
            }
        }

        public static List<Bucht> GetBuchtByCabinetIDANDPostContact(List<int> cabinetId, long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(b => cabinetId.Contains(b.CabinetInput.CabinetID) && b.ConnectionID == postContactID).ToList();
            }
        }

        public static List<Bucht> GetBuchtByCenterID(int? centerId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (centerId != null)
                    return context.Buchts.Where(b => b.CablePair.Cable.CenterID == centerId).ToList();
                else
                    return context.Buchts.Where(t => t.SwitchPortID != null).ToList();

            }
        }

        public static DB.SourceType GetBuchtTypebyCabinetIDAndMDFID(int cabinetID, int mDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var MDFType = context.MDFs.Where(t => t.ID == mDFID).Select(t => t.Type).Single();
                var cabinetType = context.Cabinets.Where(t => t.ID == cabinetID).Select(t => t.CabinetTypeID).SingleOrDefault();
                if (MDFType == 1)
                {
                    return DB.SourceType.ONU;
                }
                else if (cabinetType == 8)
                {
                    return DB.SourceType.SpecialCables;
                }
                else
                {
                    return 0;
                }


            }
        }

        public static List<Bucht> GetBuchtFromIDToID(long firstBucht, long LastBucht, int row = -1, bool freeBuchtofCablePair = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<Bucht> temp = context.Buchts.Where(t => t.ID >= firstBucht && t.ID <= LastBucht).Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine);
                if (row != -1) temp = temp.Where(t => t.MDFRowID == row);
                if (freeBuchtofCablePair) temp = temp.Where(t => t.CablePairID == null);
                return temp.ToList();
            }
        }

        public static List<Bucht> GetBuchtFromBuchtIDToBuchtID(long firstBucht, long lastBucht, byte status)
        {
            using (MainDataContext context = new MainDataContext())
            {

                IQueryable<Bucht> fromBucht = context.Buchts.Where(t => t.ID == firstBucht);
                IQueryable<Bucht> toBucht = context.Buchts.Where(t => t.ID == lastBucht);

                return context.Buchts.Where(t => t.MDFRowID == fromBucht.SingleOrDefault().MDFRowID && t.BuchtNo >= fromBucht.SingleOrDefault().BuchtNo && t.BuchtNo <= toBucht.SingleOrDefault().BuchtNo)
                    .Where(t => t.Status == status && t.CabinetInputID == null && t.CablePairID == null && t.ConnectionID == null).OrderBy(t => t.BuchtNo).ToList();
            }
        }

        public static List<Bucht> GetAllBuchtFromBuchtIDToBuchtID(long firstBucht, long lastBucht)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<Bucht> fromBucht = context.Buchts.Where(t => t.ID == firstBucht);
                IQueryable<Bucht> toBucht = context.Buchts.Where(t => t.ID == lastBucht);
                return context.Buchts.Where(t => t.MDFRowID == fromBucht.SingleOrDefault().MDFRowID && t.BuchtNo >= fromBucht.SingleOrDefault().BuchtNo && t.BuchtNo <= toBucht.SingleOrDefault().BuchtNo).OrderBy(t => t.BuchtNo).ToList();
            }
        }

        public static List<CheckableItem> GetBuchtChechable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Select(t => new CheckableItem { LongID = t.ID, Name = t.BuchtNo.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetBuchtChechablebyColumnIDs(List<int> columnIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => (t.BuchtTypeID == (byte)DB.BuchtType.ADSL) &&
                                (columnIDs.Count == 0 || columnIDs.Contains(t.MDFRowID)))
                    .Select(t => new CheckableItem { LongID = t.ID, Name = t.BuchtNo.ToString(), IsChecked = false }).Distinct().OrderBy(t => t.ID).ToList();
            }
        }

        public static List<Bucht> GetBuchtByListConnectionID(List<long> postContectIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => postContectIDs.Contains((long)t.ConnectionID)).ToList();
            }
        }

        public static List<Bucht> GetBuchtByCablePairIDs(List<long> cablePairList)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => cablePairList.Contains((long)t.CablePairID)).ToList();
            }
        }

        public static List<Bucht> GetBuchtByMDFID(int MDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var bucht = context.Buchts.Where(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == MDFID && (t.Status == (byte)DB.BuchtStatus.Free || t.Status == (byte)DB.BuchtStatus.ADSLFree)).ToList();
                return bucht;
            }
        }

        public static List<Bucht> GetBuchtByCabinetInputIDs(List<long> cabinetInputIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => cabinetInputIDs.Contains((long)t.CabinetInputID)).OrderBy(t => t.CabinetInput.InputNumber).ToList();
            }
        }
        public static List<Bucht> GetBuchtByCabinetInputIDsOrderByInputNumber(List<long> cabinetInputIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => cabinetInputIDs.Contains((long)t.CabinetInputID)).OrderBy(t => t.CabinetInput.InputNumber)
                    .ToList();
            }
        }
        public static List<ConnectionInfo> GetBuchtInfoByCabinetInputIDsOrderByInputNumber(List<long> cabinetInputIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => cabinetInputIDs.Contains((long)t.CabinetInputID)).OrderBy(t => t.CabinetInput.InputNumber)
                        .Select(t => new ConnectionInfo
                        {
                            BuchtID = t.ID,
                            VerticalRowNo = t.VerticalMDFRow.VerticalRowNo,
                            VerticalRowID = t.VerticalMDFRow.VerticalMDFColumn.ID,
                            VerticalColumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                            VerticalColumnID = t.VerticalMDFRow.ID,
                            BuchtNo = t.BuchtNo
                        }).ToList();
            }
        }
        public static List<Bucht> getBuchtByPCMPortID(List<int> PCMPortList)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => PCMPortList.Contains((int)t.PCMPortID)).ToList();
            }
        }
        public static List<Bucht> GetBuchtByADSLPortID(List<long> ADSLPortList, byte ADSLPortType)
        {

            throw new Exception("خطا در دریافت اطلاعات بوخت ها");

            //using (MainDataContext context = new MainDataContext())
            //{
            //    IQueryable<ADSLPort> ADSLPorts = context.ADSLPorts.Where(t => ADSLPortList.Contains(t.ID) && t.t);

            //    return context.Buchts.Where(t => ADSLPorts.).ToList();
            //}
        }

        public static List<Bucht> GetBuchetByListBuchtIDs(List<long> NewOutBuchtListID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => NewOutBuchtListID.Contains(t.ID)).ToList();
            }
        }

        public static Bucht GetBuchetBySwitchPortand(int switchPortID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                    .Where(t => (t.SwitchPortID == switchPortID))
                    .SingleOrDefault();
            }
        }


        public static Bucht GetBuchtByConnectionID(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ConnectionID == postContactID).SingleOrDefault();
            }
        }

        public static List<Bucht> GetBuchtsFromCablePairIDToCablePairID(long fromCablePairID, long toCablePairID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CablePairID >= fromCablePairID && t.CablePairID <= toCablePairID).ToList();
            }
        }

        public static List<Bucht> GetBuchetByFromCabinetIDToCabinetID(long FromCabinetID, long ToCabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CabinetInputID >= FromCabinetID && t.CabinetInputID <= ToCabinetID).ToList();
            }
        }

        public static List<Bucht> GetBuchtByCableID(long cableID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CablePair.CableID == cableID).ToList();
            }
        }

        public static Bucht GetBuchtBySwitchPortID(int switchPortID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.SwitchPortID == switchPortID).SingleOrDefault();
            }
        }

        public static List<Bucht> GetBuchtsBySwitchPortID(int switchPortID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.SwitchPortID == switchPortID).ToList();
            }
        }

        public static Bucht GetBuchtByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static Bucht GetBuchtByConnectionIDAndBuchtType(long PostContactID, int buchtTypeID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ConnectionID == PostContactID && t.BuchtTypeID == buchtTypeID).SingleOrDefault();
            }
        }

        public static List<Bucht> GetbuchtsConnectedToCablePairs(List<CablePair> cablePairs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => cablePairs.Select(c => c.ID).Contains(t.CablePairID ?? 0)).ToList();
            }
        }

        public static List<Bucht> GetBuchtByPostIDs(List<int> postIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => postIDs.Contains(t.PostContact.PostID)).ToList();
            }
        }

        public static List<Bucht> GetBuchtFromCablePairIDToCablePairID(long fromCablePairID, long toCablePairID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<CablePair> temp = context.CablePairs.Where(t =>
                                                                               t.CableID == context.CablePairs.Where(ci => ci.ID == fromCablePairID).SingleOrDefault().CableID &&
                                                                               t.CablePairNumber >= context.CablePairs.Where(ci => ci.ID == fromCablePairID).SingleOrDefault().CablePairNumber &&
                                                                               t.CablePairNumber <= context.CablePairs.Where(ci => ci.ID == toCablePairID).SingleOrDefault().CablePairNumber
                                                                            );


                return context.Buchts.Where(t => temp.Select(c => c.ID).Contains((long)t.CablePairID)).ToList();
            }
        }

        public static List<Bucht> GetPCMBuchtByCabinetInputID(List<long> cabinetInputIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => cabinetInputIDs.Contains((long)t.CabinetInputID) && (t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine)).ToList();
            }
        }

        public static long? GetLastBuchtNoByVerticalRowID(int VerticalRowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Buchts.Where(t => t.MDFRowID == VerticalRowID).Count() == 0)
                {
                    return null;
                }
                else
                {
                    return context.Buchts.Where(t => t.MDFRowID == VerticalRowID).Max(t => t.BuchtNo);
                }



            }
        }

        internal static long GetBuchtNoByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).SingleOrDefault().BuchtNo;
            }
        }

        public static List<Bucht> GetBuchtByCabinetID(long cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CabinetInputID == cabinetID).ToList();
            }
        }

        public static List<Bucht> GetBuchtByPCMID(int PCMID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.PCMPort.PCMID == PCMID).ToList();
            }
        }

        public static List<Bucht> GetBuchtByConnectionIDs(List<long> connections)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => connections.Contains((long)t.ConnectionID)).ToList();
            }
        }

        public static Bucht GetBuchtByCabinetInputID(long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CabinetInputID == cabinetInputID).SingleOrDefault();
            }
        }

        public static List<ConnectionForPCM> GetBuchtTheNumber(long BuchtID, int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<Bucht> bucht = context.Buchts.Where(t => t.ID == BuchtID);
                IQueryable<MDF> mDF = context.Buchts.Where(t => t.ID == BuchtID).Select(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF);

                int index = context.Buchts.Where(t =>
                                                    (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDF.SingleOrDefault().ID) &&
                                                    t.Status == (byte)DB.BuchtStatus.Free &&
                                                    t.CabinetInputID == null &&
                                                    t.PCMPortID == null
                                                 )
                                          .OrderBy(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo)
                                          .ThenBy(t => t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                          .ThenBy(t => t.VerticalMDFRow.VerticalRowNo)
                                          .ThenBy(t => t.BuchtNo)
                                          .ToList()
                                          .FindIndex(t => t.ID == bucht.SingleOrDefault().ID);

                return context.Buchts.Where(t =>
                                                 (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDF.SingleOrDefault().ID) &&
                                                 t.Status == (byte)DB.BuchtStatus.Free &&
                                                 t.CabinetInputID == null &&
                                                 t.PCMPortID == null
                                            )
                                      .OrderBy(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo)
                                      .ThenBy(t => t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                      .ThenBy(t => t.VerticalMDFRow.VerticalRowNo)
                                      .ThenBy(t => t.BuchtNo)
                                      .Skip(index).Take(count)
                                      .Select(t => new ConnectionForPCM
                                                 {
                                                     BuchtID = t.ID,
                                                     BuchtNo = t.BuchtNo.ToString(),
                                                     MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                                                     MDF = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                                                     VerticalCloumnID = t.VerticalMDFRow.VerticalMDFColumnID,
                                                     VerticalRowID = t.VerticalMDFRow.ID,
                                                     VerticalRowNo = t.VerticalMDFRow.VerticalRowNo.ToString(),
                                                     VerticalCloumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                                 }
                                             )
                                    .ToList();
            }
        }

        public static List<ConnectionForPCM> GetBuchtTheNumberByOutBucht(long BuchtID, int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<Bucht> bucht = context.Buchts.Where(t => t.ID == BuchtID);
                IQueryable<MDF> mDF = context.Buchts.Where(t => t.ID == BuchtID).Select(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF);

                int index = context.Buchts.Where(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDF.SingleOrDefault().ID &&
                                                t.Status == (byte)DB.BuchtStatus.Free &&
                                                t.CabinetInputID == null &&
                                                t.PCMPortID == null)
                                .OrderBy(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo)
                                .ThenBy(t => t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                .ThenBy(t => t.VerticalMDFRow.VerticalRowNo)
                                .ThenBy(t => t.BuchtNo)
                                .ToList().FindIndex(t => t.ID == bucht.SingleOrDefault().ID);



                return context.Buchts.Where(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID == mDF.SingleOrDefault().ID &&
                                                 t.Status == (byte)DB.BuchtStatus.Free &&
                                                 t.CabinetInputID == null &&
                                                 t.PCMPortID == null
                                            )
                                 .OrderBy(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.FrameNo)
                                 .ThenBy(t => t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                 .ThenBy(t => t.VerticalMDFRow.VerticalRowNo)
                                 .ThenBy(t => t.BuchtNo)
                                 .Skip(index + 1).Take(count)
                                 .Select(t => new ConnectionForPCM
                                 {
                                     BuchtID = t.ID,
                                     BuchtNo = t.BuchtNo.ToString(),
                                     MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                                     MDF = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                                     VerticalCloumnID = t.VerticalMDFRow.VerticalMDFColumnID,
                                     VerticalRowID = t.VerticalMDFRow.ID,
                                     VerticalRowNo = t.VerticalMDFRow.VerticalRowNo.ToString(),
                                     VerticalCloumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                 }).ToList();




            }
            //using (MainDataContext context = new MainDataContext())
            //{

            //    return context.Buchts.Where(t => t.ID > BuchtID ).Take(count)
            //                     .Select(t => new ConnectionForPCM
            //                     {
            //                         BuchtID = t.ID,
            //                         BuchtNo = t.BuchtNo.ToString(),
            //                         MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
            //                         MDF = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
            //                         VerticalCloumnID = t.VerticalMDFRow.VerticalMDFColumnID,
            //                         VerticalRowID = t.VerticalMDFRow.ID,
            //                         VerticalRowNo = t.VerticalMDFRow.VerticalRowNo.ToString(),
            //                         VerticalCloumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
            //                     }).ToList();




            //}
        }

        public static List<ConnectionForPCM> GetConnectionForPCM(List<long> IDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => IDs.Contains(t.ID))
                                     .AsEnumerable().Select((t, i) => new ConnectionForPCM
                                       {
                                           rowIndex = i + 1,
                                           BuchtID = t.ID,
                                           BuchtNo = t.BuchtNo.ToString(),
                                           MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                                           MDF = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                                           VerticalCloumnID = t.VerticalMDFRow.VerticalMDFColumnID,
                                           VerticalRowID = t.VerticalMDFRow.ID,
                                           VerticalRowNo = t.VerticalMDFRow.VerticalRowNo.ToString(),
                                           VerticalCloumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                       }).ToList();
            }

        }

        public static List<Bucht> GetBuchtAllByCabinetID(int CabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CabinetInput.CabinetID == CabinetID).ToList();
            }
        }

        public static List<Bucht> GetBuchtByIDs(List<long> buchtIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => buchtIDs.Contains(t.ID)).ToList();
            }
        }

        public static string GetConnectionByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).Select(t => "ردیف:" + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.BuchtNo).SingleOrDefault();
            }
        }

        public static Bucht GetBuchtbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Bucht bucht = null;
                int switchPortID = 0;

                if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault() != null)
                {
                    if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID != null)
                        switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                    if (switchPortID != 0)
                        bucht = context.Buchts.Where(t => t.SwitchPortID == switchPortID).SingleOrDefault();
                }

                return bucht;
            }
        }

        public static Bucht GetPCMCabinetInputBucht(long CabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.CabinetInputID == CabinetInputID && t.BuchtTypeID == (int)DB.BuchtType.CustomerSide && t.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).SingleOrDefault();
            }
        }

        public static string GetPCMCabinetInputBuchtInput(long CabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Bucht bucht = context.Buchts.Where(t => t.CabinetInputID == CabinetInputID && t.BuchtTypeID == (int)DB.BuchtType.CustomerSide && t.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).SingleOrDefault();
                return " ردیف : " + bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " طبقه : " + bucht.VerticalMDFRow.VerticalRowNo + " اتصالی : " + bucht.BuchtNo;

            }
        }

        public static List<Bucht> GetBuchtBySwitchPortIDs(List<long> switchPortIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => switchPortIDs.Contains((long)t.SwitchPortID)).ToList();
            }
        }
    }

    public class BuchtInfo
    {
        public int CenterID { get; set; }
        public int MDFID { get; set; }
        public string MDFDescription { get; set; }
        public int VerticalCloumnNo { get; set; }
        public int VerticalRowNo { get; set; }
        public long BuchtNo { get; set; }
        public string MDFHorizentalID { get; set; }
        public int BuchtTypeID { get; set; }
        public int Status { get; set; }
        public long BuchtID { get; set; }
        public string NormalConnectionNo { get; set; }
        public string PCMConnectionNo { get; set; }
        public long? CablePairID { get; set; }
        public int? CablePairNumber { get; set; }
        public long? CabinetInputID { get; set; }
        public int? InputNumber { get; set; }
        public int? CabinetID { get; set; }
        public string CabinetNumber { get; set; }
        public byte? PCMChannelNo { get; set; }
        public int? PCMDeviceID { get; set; }
        public string MUID { get; set; }
    }


}



