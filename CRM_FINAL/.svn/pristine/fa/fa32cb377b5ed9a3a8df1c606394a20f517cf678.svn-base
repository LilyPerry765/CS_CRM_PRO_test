using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Windows.Forms;
using System.Transactions;


namespace CRM.Data
{
    public static class CableDB
    {
        public static List<CableInfo> SearchCable
            (
              List<int> city,
               List<int> center,
               List<long> cableNumber,
               List<int> cableType,
               List<int> cableUsedChannel,
               decimal cableDiameter,
               List<int> status,
               int startRowIndex,
               int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CableInfo> result = new List<CableInfo>();
                var query = context.Cables
                                   .Where(t =>
                                           (city.Count == 0 || city.Contains(t.Center.Region.CityID)) &&
                                           (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                                           (cableNumber.Count == 0 || cableNumber.Contains(t.ID)) &&
                                           (cableType.Count == 0 || cableType.Contains(t.CableTypeID)) &&
                                           (cableUsedChannel.Count == 0 || cableUsedChannel.Contains(t.CableUsedChannelID)) &&
                                           (cableDiameter == -1 || t.CableDiameter == cableDiameter) &&
                                           (status.Count == 0 || status.Contains(t.Status))
                                         )
                                         .Select(t => new CableInfo
                                                    {
                                                        ID = t.ID,
                                                        CableDiameter = t.CableDiameter,
                                                        CableNumber = t.CableNumber,
                                                        CableTypeID = t.CableTypeID,
                                                        CableTypeName = t.CableType.CableTypeName,
                                                        CableUsedChannelID = t.CableUsedChannelID,
                                                        CableUsedChannelName = t.CableUsedChannel.CableUsedChannelName,
                                                        CenterID = t.CenterID,
                                                        CenterName = t.Center.CenterName,
                                                        FromCablePairNumber = t.FromCablePairNumber,
                                                        InsertDate = t.InsertDate,
                                                        PhysicalType = t.PhysicalType,
                                                        Status = t.Status,
                                                        ToCablePairNumber = t.ToCablePairNumber
                                                    }
                                             )
                                   .AsQueryable();
                count = query.Count();
                result = query.Skip(startRowIndex).Take(pageSize).ToList();
                return result;
            }
        }

        public static int SearchCableCount
         (
            List<int> city,
            List<int> center,
            List<long> cableNumber,
            List<int> cableType,
            List<int> cableUsedChannel,
            decimal cableDiameter,
            List<int> status
         )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables
                    .Where(t =>
                            (city.Count == 0 || city.Contains(t.Center.Region.CityID)) &&
                            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                            (cableNumber.Count == 0 || cableNumber.Contains(t.ID)) &&
                            (cableType.Count == 0 || cableType.Contains(t.CableTypeID)) &&
                            (cableUsedChannel.Count == 0 || cableUsedChannel.Contains(t.CableUsedChannelID)) &&
                            (cableDiameter == -1 || t.CableDiameter == cableDiameter) &&
                            (status.Count == 0 || status.Contains(t.Status))
                          )
                    .Count();
            }
        }

        public static Cable GetCableByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCableCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.CableNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
        public static List<CheckableItem> GetCableCheckableByID(long id)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables
                    .Where(t => t.ID == id)
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.CableNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CablePair> GetCablePairByCenterID(int centerId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.Buchts.Where(b => b.CablePair.Cable.CenterID == centerId).Select(b => b.CablePairID).Distinct().ToList();
                return context.CablePairs.Where(c => x.Contains(c.ID)).ToList();
            }
        }

        public static List<CabinetInput> GetCabinetInputByCenterID(int centerId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.CabinetInputs.Where(b => b.Cabinet.CenterID == centerId).ToList();
                return x;
            }
        }

        public static int GetCity(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        //milad doran
        //public static List<CheckableItem> GetCableCheckableByCenterID(List<int> centers)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Cables.Where(t => centers.Contains((int)t.CenterID) && t.Status == (byte)DB.CableStatus.CableConnection).OrderBy(t => t.CableNumber).Select(t => new CheckableItem { LongID = t.ID, Name = t.CableNumber.ToString(), IsChecked = false }).ToList();
        //    }
        //}

        //TODO:rad 13950220
        public static List<CheckableItem> GetCableCheckableByCenterID(List<int> centers)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                var query = context.Cables.Where(t =>
                                                    centers.Contains((int)t.CenterID) &&
                                                    t.Status == (byte)DB.CableStatus.CableConnection
                                                )
                                          .OrderBy(t => t.CableNumber)
                                          .Select(t => new CheckableItem
                                                            {
                                                                LongID = t.ID,
                                                                Name = t.CableNumber.ToString(),
                                                                IsChecked = false
                                                            }
                                                 )
                                          .AsQueryable();
                result = query.ToList();
                return result;
            }
        }

        public static List<CheckableItem> GetCablesByCenterID(int center)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables.Where(t => t.CenterID == center).OrderBy(t => t.CableNumber).Select(t => new CheckableItem { LongID = t.ID, Name = t.CableNumber.ToString(), IsChecked = false }).ToList();
            }

        }
        public static List<Cable> GetCablesByCenterIDs(List<int> CenterIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables.Where(t => CenterIDs.Count == 0 || CenterIDs.Contains(t.CenterID)).OrderBy(t => t.CableNumber).ToList();
            }

        }
        public static bool LeaveCablePairFromBuchtForm(long fromCablePairID, long toCablePairID, bool checkCabinetInput = true)
        {
            if (fromCablePairID != 0 && toCablePairID != 0)
            {
                // First, the status of the paired cables to free, then bucht are connected to the cable to free

                List<CablePair> cablePairs = Data.CablePairDB.GetCablePairFromIDToToID(fromCablePairID, toCablePairID);
                if (checkCabinetInput)
                    if (cablePairs.Any(t => t.CabinetInputID != null)) { MessageBox.Show("امکان آزاد سازی زوج کابل های که به کافو متصل هستند نیست!"); return false; }


                cablePairs.ForEach((CablePair item) => { item.Status = (Byte)DB.CablePairStatus.Free; item.Detach(); });

                List<Bucht> buchts = Data.BuchtDB.GetBuchtByCablePairIDs(cablePairs.Select(t => t.ID).ToList());

                buchts.ForEach((Bucht bucht) => { bucht.CablePairID = null; bucht.CabinetInputID = null; bucht.Detach(); });

                using (TransactionScope ts = new TransactionScope())
                {
                    DB.UpdateAll(cablePairs);
                    DB.UpdateAll(buchts);
                    ts.Complete();
                }

                return true;


            }
            else
            {
                MessageBox.Show("لطفا فیلد های مورد نیاز را پر کنید");
                return false;
            }
        }
        public static bool AssignCablePairToBuchtForm(long fromCablePairID, long toCablePairID, long fromBuchtID, long toBuchtID, int row = -1)
        {
            try
            {
                if (fromCablePairID != 0 && toCablePairID != 0 && fromBuchtID != 0 && toBuchtID != 0)
                {

                    // assingmnet CablePairID and CabinetInputID to CablepairID an CabinetInputID in bucht table 

                    List<CablePair> cablePairs = Data.CablePairDB.GetCablePairFromIDToToID(fromCablePairID, toCablePairID);
                    List<Bucht> buchts = Data.BuchtDB.GetBuchtFromIDToID(fromBuchtID, toBuchtID, row);

                    if (buchts.Any(t => t.CablePairID != null))
                    {
                        MessageBox.Show("از میان بوخت های انتخاب شد، بوخت متصل به زوج سیم وجود دارد!");
                        return false;
                    }


                    if (cablePairs.Any(t => t.Status == (byte)DB.CablePairStatus.ConnectedToBucht))
                    {
                        MessageBox.Show("از زوج سیم های انتخاب شده به بوخت متصل میباشد");
                        return false;
                    }
                    if (cablePairs.Count() != buchts.Count())
                    {
                        MessageBox.Show("تعداد زوج سیم های و تعداد بوخت های آزاد برابر نمی باشد");
                        return false;
                    }

                    for (int i = 0; i < buchts.Count(); i++)
                    {
                        buchts[i].CablePairID = cablePairs[i].ID;
                        buchts[i].CabinetInputID = cablePairs[i].CabinetInputID;
                        cablePairs[i].Status = (byte)DB.CablePairStatus.ConnectedToBucht;
                        buchts[i].Detach();
                        cablePairs[i].Detach();
                    }
                    using (TransactionScope ts = new TransactionScope())
                    {
                        DB.UpdateAll(buchts);
                        DB.UpdateAll(cablePairs);
                        ts.Complete();
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("لطفا فیلد های مورد نیاز را انتخاب کنید!", "خطا");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex.InnerException);
            }

        }

        public static bool AssignFreeCablePairToFreeBuchtForm(long fromCablePairID, long toCablePairID, int row, long fromBuchtID, long toBuchtID)
        {
            try
            {
                if (fromCablePairID != 0 && toCablePairID != 0 && fromBuchtID != 0 && toBuchtID != 0)
                {

                    // assingmnet CablePairID and CabinetInputID to CablepairID an CabinetInputID in bucht table 

                    List<CablePair> cablePairs = Data.CablePairDB.GetCablePairFromIDToToID(fromCablePairID, toCablePairID, true);
                    List<Bucht> buchts = Data.BuchtDB.GetBuchtFromIDToID(fromBuchtID, toBuchtID, row, true);

                    if (buchts.Any(t => t.CablePairID != null))
                    {
                        MessageBox.Show("از میان بوخت های انتخاب شد، بوخت متصل به زوج سیم وجود دارد!");
                        return false;
                    }


                    if (cablePairs.Any(t => t.Status == (byte)DB.CablePairStatus.ConnectedToBucht))
                    {
                        MessageBox.Show("از زوج سیم های انتخاب شده به بوخت متصل میباشد");
                        return false;
                    }
                    if (cablePairs.Count() != buchts.Count())
                    {
                        MessageBox.Show("تعداد زوج سیم های و تعداد بوخت های آزاد برابر نمی باشد");
                        return false;
                    }

                    for (int i = 0; i < buchts.Count(); i++)
                    {
                        buchts[i].CablePairID = cablePairs[i].ID;
                        buchts[i].CabinetInputID = cablePairs[i].CabinetInputID;
                        cablePairs[i].Status = (byte)DB.CablePairStatus.ConnectedToBucht;
                        buchts[i].Detach();
                        cablePairs[i].Detach();
                    }
                    using (TransactionScope ts = new TransactionScope())
                    {
                        DB.UpdateAll(buchts);
                        DB.UpdateAll(cablePairs);
                        ts.Complete();
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("لطفا فیلد های مورد نیاز را انتخاب کنید!", "خطا");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex.InnerException);
            }

        }

        public static Cable GetVirtualCableByCabinetID(int cabinetID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables.Where(t => t.CabinetIDInVirtualCable == cabinetID).SingleOrDefault();
            }
        }

        public static List<CableInfo> GetCenterCables(DateTime? FromDate, DateTime? ToDate, List<int> CenterIDs, int CableNo, List<int> PhysicalTypeIDs, List<int> UsingTypeIDs, string StatusCable)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cables
                    .Where(t => (!FromDate.HasValue || t.InsertDate.Date >= FromDate)
                            && (!ToDate.HasValue || t.InsertDate.Date <= ToDate)
                            && (CenterIDs.Count == 0 || CenterIDs.Contains(t.CenterID))
                            && (PhysicalTypeIDs.Count == 0 || PhysicalTypeIDs.Contains((byte)t.PhysicalType))
                            && (UsingTypeIDs.Count == 0 || UsingTypeIDs.Contains(t.CableUsedChannelID))
                            && (StatusCable == "" || t.Status.ToString() == StatusCable)
                            && (CableNo == -1 || t.CableNumber == (int)CableNo)
                            )
                            .Select(t => new CableInfo
                            {
                                CableNumber = t.CableNumber,
                                CableTypeName = t.CableType.CableTypeName,
                                PhysicalType = t.PhysicalType,
                                CableUsedChannelName = t.CableUsedChannel.CableUsedChannelName,
                                CenterName = t.Center.CenterName,
                                FromCablePairNumber = t.FromCablePairNumber,
                                ToCablePairNumber = t.ToCablePairNumber,
                                Status = t.Status,
                                InsertDate = t.InsertDate,
                                CableDiameter = t.CableDiameter,
                                CityName = t.Center.Region.City.Name
                            }
                            )
                            .ToList();
            }
        }

        //public static List<CablePairReport> GetCablePairReport(int CenterIDs, List<int> CableNoIDs, List<int> CableStatusIDs)
        //{
        //    List<long> _CableNoIDs = CableNoIDs.Select(i => (long)i).ToList();
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Cables
        //            .GroupJoin(context.CablePairs, c => c.ID, cp => cp.CableID, (c, cp) => new { cable = c, cablePair = cp })
        //            .SelectMany(t1 => t1.cablePair.DefaultIfEmpty(), (c, t1) => new { CablePair = t1, Cable = c.cable })

        //            .GroupJoin(context.Buchts, cp => cp.CablePair.ID, b => b.CablePairID, (cp, b) => new { bucht = b, cabinetPair = cp.CablePair })
        //            .SelectMany(t2 => t2.bucht.DefaultIfEmpty(), (b, t2) => new { Bucht = t2, CablePair = b.cabinetPair })

        //            .GroupJoin(context.SwitchPorts, b => b.Bucht.SwitchPortID, s => s.ID, (b, s) => new { bucht = b, switchPort = s })
        //            .SelectMany(t3 => t3.switchPort.DefaultIfEmpty(), (b, t3) => new { SwitchPort = t3, Bucht = b.bucht })

        //            .GroupJoin(context.Telephones, b => b.Bucht.Bucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b.Bucht, telephone = t })
        //            .SelectMany(t3 => t3.telephone.DefaultIfEmpty(), (bt, t3) => new { Telephone = t3, Bucht = bt.bucht })



        //            .Where(t => (t.Bucht.CablePair.Cable.CenterID == CenterIDs)
        //                    && (CableNoIDs.Count == 0 || _CableNoIDs.Contains(t.Bucht.CablePair.Cable.ID)))
        //            //&& (CableStatusIDs.Count == 0 || CableStatusIDs.Contains(t.Bucht.CablePair.Cable.Status)))
        //                    .Select(t => new CablePairReport
        //                    {
        //                        CablePairNumber = t.Bucht.CablePair.CablePairNumber,
        //                        Cabinet = t.Bucht.Bucht.CabinetInput.Cabinet.CabinetNumber,
        //                        CabinetInput = t.Bucht.Bucht.CabinetInput.InputNumber,
        //                        BuchtNo = t.Bucht.Bucht.BuchtNo,
        //                        BuchtTypeName = t.Bucht.Bucht.BuchtType.BuchtTypeName,
        //                        CablePairStatusID = t.Bucht.CablePair.Status,
        //                        Post = t.Bucht.Bucht.PostContact.Post.Number,
        //                        PostContact = t.Bucht.Bucht.PostContact.ConnectionNo,
        //                        TelephoneNo = t.Telephone.TelephoneNo,
        //                        VerticalCloumnNo = t.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //                        VerticalRowNo = t.Bucht.Bucht.VerticalMDFRow.VerticalRowNo,
        //                        CenterName = t.Bucht.CablePair.Cable.Center.CenterName

        //                    }
        //                    ).ToList();
        //    }
        //}
        public static List<CablePairActionLogXml> GetCablePairLog(int ActionID, int CenterID, long CableNo, List<long> CablePairIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ActionLog> ActionLogTemp = context.ActionLogs.Where(t => (t.ActionID == ActionID)).ToList();
                List<CablePairActionLogXml> AllActionLog = new List<CablePairActionLogXml>();
                foreach (ActionLog item in ActionLogTemp)
                {
                    CRM.Data.Schema.CablePairStatus CablePairS = LogSchemaUtility.Deserialize<CRM.Data.Schema.CablePairStatus>(item.Description.ToString());
                    CablePairActionLogXml Temp = new CablePairActionLogXml();
                    if ((CablePairS.CenterID == CenterID) && (CablePairIDs.Contains(CablePairS.CablePairID)))
                    {
                        Temp.CablePairID = CablePairS.CablePairID;
                        Temp.CenterID = CablePairS.CenterID;
                        Temp.LogDateTime = CablePairS.LogDateTime;
                        AllActionLog.Add(Temp);
                    }
                }
                var xx = from p in AllActionLog
                         //where conditions or joins with other tables to be included here
                         group p by p.CablePairID into grp
                         let MaxLogDateTime = grp.Max(g => g.LogDateTime)

                         from p in grp
                         where p.LogDateTime == MaxLogDateTime
                         select p;

                List<CablePairActionLogXml> T = xx.ToList();
                return T;
                //return AllActionLog.OrderByDescending(t=>t.LogDateTime).Take(1).ToList();
                //return AllActionLog.OrderByDescending(i => i.LogDateTime).GroupBy(t => t.CablePairID)
                //    .Select(g => new { g, count = g.Count() })
                //    .SelectMany(t => t.g.Select(b => b).Zip(Enumerable.Range(1, t.count), (j, i) => new { j.LogDateTime, j.CablePairID })).Select(t => new CablePairActionLogXml { CablePairID = t.CablePairID, LogDateTime = t.LogDateTime }).ToList();


                //Where (t=>(CablePairIDs.Contains(t.ID))).GroupBy(.ToList();
            }
        }
        public static List<CablePairReport> GetCablePairReport(int CenterID, long CableNoID, int? buchtStatus)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts

                    .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, telephone = t })
                    .SelectMany(t3 => t3.telephone.DefaultIfEmpty(), (bt, t3) => new { Telephone = t3, Bucht = bt.bucht })

                    .Where(t => (t.Bucht.CablePair.Cable.CenterID == CenterID)
                            && (CableNoID == t.Bucht.CablePair.Cable.ID)
                            && (buchtStatus == null || buchtStatus == t.Bucht.Status))
                            .Select(t => new CablePairReport
                            {
                                CablePairNumber = t.Bucht.CablePair.CablePairNumber,
                                CablePairID = t.Bucht.CablePair.ID,
                                Cabinet = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                CabinetInput = t.Bucht.CabinetInput.InputNumber,
                                BuchtNo = t.Bucht.BuchtNo,
                                BuchtTypeName = t.Bucht.BuchtType.BuchtTypeName,
                                CablePairStatusID = t.Bucht.CablePair.Status,
                                Post = t.Bucht.PostContact.Post.Number,
                                PostContact = t.Bucht.PostContact.ConnectionNo,
                                TelephoneNo = t.Telephone.TelephoneNo,
                                VerticalCloumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                VerticalRowNo = t.Bucht.VerticalMDFRow.VerticalRowNo,
                                BuchtStatus = t.Bucht.Status.ToString()
                            }
                            ).ToList();




            }
        }
    }
}
