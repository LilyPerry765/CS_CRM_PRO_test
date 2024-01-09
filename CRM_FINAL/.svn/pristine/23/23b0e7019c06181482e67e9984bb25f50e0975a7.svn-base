using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Windows.Forms;
using System.Transactions;
using System.Reflection;
using System.ComponentModel;


namespace CRM.Data
{
    public static class CabinetDB
    {
        public static List<CenterCabinetInfo> SearchCabinet(
            List<int> cites,
            List<int> center,
            List<int> cabinetType,
            List<int> cabinetUsageType,
            int fromInputNo,
            int toInputNo,
            int distanceFromCenter,
            int outBoundMeter,
            List<int> status,
            long fromPostalCode,
            long toPostalCode,
            List<int> cabinetIDs,
            string cabinetCode,
            List<int> aBType,
            string address,
            string postCode,
            bool? isOutBoundMeter,
            int? activeInput,
            int activeInputCount,
            int? activePost,
            double activePostCount,

            int? remainedQuotaReservation,
            int remainedQuotaReservationCount,
            int? quotaReservation,
            int quotaReservationCount,

            int startRowIndex,
            int pageSize,
            out int count
            )
        {
            using (MainDataContext context = new MainDataContext())
            {

                int cabinetShare = 0;
                int.TryParse(DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.ApplyCabinetShare)), out cabinetShare);

                IQueryable<CenterCabinetInfo> centerCabinetInfoQuery = context.Cabinets
                    .Where(t =>
                            (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                            (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                            (cabinetType.Count == 0 || cabinetType.Contains(t.CabinetTypeID)) &&
                            (cabinetUsageType.Count == 0 || cabinetUsageType.Contains(t.CabinetUsageType)) &&
                            (fromInputNo == -1 || t.FromInputNo == fromInputNo) &&
                            (toInputNo == -1 || t.ToInputNo == toInputNo) &&
                            (distanceFromCenter == -1 || t.DistanceFromCenter == distanceFromCenter) &&
                            (outBoundMeter == -1 || t.OutBoundMeter == outBoundMeter) &&
                            (status.Count == 0 || status.Contains(t.Status)) &&
                            (fromPostalCode == -1 || t.FromPostalCode == fromPostalCode) &&
                            (toPostalCode == -1 || t.ToPostalCode == toPostalCode) &&
                            (cabinetIDs.Count == 0 || cabinetIDs.Contains(t.ID)) &&
                            (isOutBoundMeter == null || t.IsOutBound == isOutBoundMeter) &&
                            (string.IsNullOrWhiteSpace(cabinetCode) || t.CabinetCode.Contains(cabinetCode)) &&
                            (aBType.Count == 0 || aBType.Contains((int)t.ABType)) &&
                            (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)) &&
                            (string.IsNullOrWhiteSpace(postCode) || t.PostCode.Contains(postCode))

                          )
                          .Select
                          (t => new CenterCabinetInfo
                          {
                              ID = t.ID,
                              Center = t.Center.CenterName,
                              CabinetNumber = t.CabinetNumber,
                              ABType = t.AORBPostAndCabinet.Name,
                              CabinetCode = t.CabinetCode,
                              CabinetTypeID = t.CabinetType.CabinetTypeName,
                              FirstPostID = t.Posts.Min(p => p.Number).ToString(),
                              LastPostID = t.Posts.Max(p => p.Number).ToString(),
                              CabinetUsageType = t.CabinetUsageType1.Name,
                              FromInputNo = t.FromInputNo.ToString(),
                              ToInputNo = t.ToInputNo.ToString(),
                              DistanceFromCenter = t.DistanceFromCenter.ToString(),
                              IsOutBound = t.IsOutBound.ToString(),
                              OutBoundMeter = t.IsOutBound.ToString(),
                              Address = t.Address.ToString(),
                              PostCode = t.PostCode.ToString(),
                              FromPostalCode = t.FromPostalCode.ToString(),
                              ToPostalCode = t.ToPostalCode.ToString(),
                              Status = t.CabinetStatus.Name,
                              Capacity = t.Capacity.ToString(),
                              // RemainedQuotaReservation = (context.Buchts.Where(t2 => t2.CabinetInput.CabinetID == t.ID && t2.Status == (byte)DB.BuchtStatus.Free).Count()) - (t.CabinetInputs.Where(t3 => t3.CabinetID == t.ID && t.Status == (int)DB.CabinetInputStatus.healthy).Count() * cabinetShare / 100),
                              RemainedQuotaReservation = (context.Buchts.Where(bu => bu.CabinetInput.CabinetID == t.ID && bu.Status == (byte)DB.BuchtStatus.Free && bu.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy && bu.PCMPortID == null).Count())
                                                       - (context.Buchts.Where(bu => bu.CabinetInput.CabinetID == t.ID && bu.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy && bu.PCMPortID == null).Count() * cabinetShare / 100),
                              QuotaReservation = (context.Buchts.Where(bu => bu.CabinetInput.CabinetID == t.ID && bu.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy && bu.PCMPortID == null).Count() * cabinetShare / 100),
                              PostCount = ((t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.AorBType == (byte)DB.AORBPostAndCabinet.AORB && t5.IsDelete == false).Count())
                                                 + (t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.AorBType == (byte)DB.AORBPostAndCabinet.A && t5.IsDelete == false).Count() * 0.5)
                                                 + (t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.AorBType == (byte)DB.AORBPostAndCabinet.B && t5.IsDelete == false).Count() * 0.5)),


                              ActivePostCount = ((t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.Status != (byte)DB.PostStatus.Broken && t5.AorBType == (byte)DB.AORBPostAndCabinet.AORB && t5.IsDelete == false).Count())
                                                 + (t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.Status != (byte)DB.PostStatus.Broken && t5.AorBType == (byte)DB.AORBPostAndCabinet.A && t5.IsDelete == false).Count() * 0.5)
                                                 + (t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.Status != (byte)DB.PostStatus.Broken && t5.AorBType == (byte)DB.AORBPostAndCabinet.B && t5.IsDelete == false).Count() * 0.5)),

                              DeactivePostCount = ((t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.Status == (byte)DB.PostStatus.Broken && t5.AorBType == (byte)DB.AORBPostAndCabinet.AORB && t5.IsDelete == false).Count())
                                                 + (t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.Status == (byte)DB.PostStatus.Broken && t5.AorBType == (byte)DB.AORBPostAndCabinet.A && t5.IsDelete == false).Count() * 0.5)
                                                 + (t.Posts.Where(t5 => t5.CabinetID == t.ID && t5.Status == (byte)DB.PostStatus.Broken && t5.AorBType == (byte)DB.AORBPostAndCabinet.B && t5.IsDelete == false).Count() * 0.5)),

                              InputCount = t.CabinetInputs.Count(),
                              ActiveInputCount = context.Buchts.Where(b => b.CabinetInput.CabinetID == t.ID && (b.Status == (int)DB.BuchtStatus.Connection || b.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM) && !b.PCMPortID.HasValue).Count(),
                              DeactiveInputCount = context.Buchts.Where(b => b.CabinetInput.CabinetID == t.ID && b.Status == (int)DB.BuchtStatus.Free && !b.PCMPortID.HasValue).Count()
                                                      - context.Buchts.Where(b => b.CabinetInput.CabinetID == t.ID && (b.Status == (int)DB.BuchtStatus.Destroy || b.CabinetInput.Status == (int)DB.CabinetInputStatus.Malfuction) && !b.PCMPortID.HasValue).Count(),
                              BrokenInputCount = context.Buchts.Where(b => b.CabinetInput.CabinetID == t.ID && (b.Status == (int)DB.BuchtStatus.Destroy || b.CabinetInput.Status == (int)DB.CabinetInputStatus.Malfuction) && !b.PCMPortID.HasValue).Count(),
                              InputReservation = context.Buchts.Where(b => b.CabinetInput.CabinetID == t.ID && (b.Status == (int)DB.BuchtStatus.Reserve) && !b.PCMPortID.HasValue).Count(),

                              WaitingListCount = t.InvestigatePossibilityWaitinglists.Where(iwp => iwp.WaitingList.Status == false && (iwp.WaitingList.WaitingListType == (byte)DB.WatingListType.investigatePossibility)).Count(),
                              PCMCount = context.Buchts.Where(b => b.CabinetInput.CabinetID == t.ID && b.Status == (byte)DB.BuchtStatus.AllocatedToInlinePCM).Count(),
                              ADSLCount = context.Buchts.Where(T4 => T4.CabinetInput.CabinetID == t.ID)
                                                           .Join(context.Telephones, bu => bu.SwitchPortID, te => te.SwitchPortID, (bu, te) => new { bucht = bu, tele = te })
                                                           .Join(context.ADSLPAPPorts, te2 => te2.tele.TelephoneNo, adsl => adsl.TelephoneNo, (te2, adsl) => new { telephone = te2.tele, ADSLPAPPort = adsl }).Count(),

                          });

                if (activeInput != null && activeInputCount != 0)
                {
                    centerCabinetInfoQuery = centerCabinetInfoQuery.Where(DB.ComparisonByByPropertyName<CenterCabinetInfo>("ActiveInputCount", activeInputCount, (int)activeInput)).AsQueryable();
                }
                if (activePost != null && activePostCount != 0)
                {
                    centerCabinetInfoQuery = centerCabinetInfoQuery.Where(DB.ComparisonByByPropertyName<CenterCabinetInfo>("ActivePostCount", activePostCount, (int)activePost)).AsQueryable();
                }


                if (remainedQuotaReservation != null && remainedQuotaReservationCount != 0)
                {
                    centerCabinetInfoQuery = centerCabinetInfoQuery.Where(DB.ComparisonByByPropertyName<CenterCabinetInfo>("RemainedQuotaReservation", remainedQuotaReservationCount, (int)remainedQuotaReservation)).AsQueryable();
                }
                if (quotaReservation != null && quotaReservationCount != 0)
                {
                    centerCabinetInfoQuery = centerCabinetInfoQuery.Where(DB.ComparisonByByPropertyName<CenterCabinetInfo>("QuotaReservation", quotaReservationCount, (int)quotaReservation)).AsQueryable();
                }


                count = centerCabinetInfoQuery.Count();
                return centerCabinetInfoQuery.OrderBy(t => t.CabinetNumber)
                          .Skip(startRowIndex)
                          .Take(pageSize)
                          .ToList();
            }
        }

        public static int SearchCabinetCount(
    List<int> cites,
    List<int> center,
    List<int> cabinetType,
    List<int> cabinetUsageType,
    int fromInputNo,
    int toInputNo,
    int distanceFromCenter,
    int outBoundMeter,
    List<int> status,
    long fromPostalCode,
    long toPostalCode,
    List<int> cabinetIDs,
    string cabinetCode,
    List<int> aBType,
    string address,
    string postCode,
    bool? isOutBoundMeter
    )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets
                    .Where(t =>
                            (cites.Count == 0 || cites.Contains(t.Center.Region.CityID)) &&
                           (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                            (cabinetType.Count == 0 || cabinetType.Contains(t.CabinetTypeID)) &&
                            (cabinetUsageType.Count == 0 || cabinetUsageType.Contains(t.CabinetUsageType)) &&
                            (fromInputNo == -1 || t.FromInputNo == fromInputNo) &&
                            (toInputNo == -1 || t.ToInputNo == toInputNo) &&
                            (distanceFromCenter == -1 || t.DistanceFromCenter == distanceFromCenter) &&
                            (outBoundMeter == -1 || t.OutBoundMeter == outBoundMeter) &&
                            (status.Count == 0 || status.Contains(t.Status)) &&
                            (fromPostalCode == -1 || t.FromPostalCode == fromPostalCode) &&
                            (toPostalCode == -1 || t.ToPostalCode == toPostalCode) &&
                            (cabinetIDs.Count == 0 || cabinetIDs.Contains(t.ID)) &&
                            (isOutBoundMeter == null || t.IsOutBound == isOutBoundMeter) &&
                            (string.IsNullOrWhiteSpace(cabinetCode) || t.CabinetCode.Contains(cabinetCode)) &&
                            (aBType.Count == 0 || aBType.Contains((int)t.ABType)) &&
                            (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)) &&
                            (string.IsNullOrWhiteSpace(postCode) || t.PostCode.Contains(postCode))
                          ).Count();
            }
        }


        public static Cabinet GetCabinetByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
        public static CheckableItem GetCheckableItemCabinetByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets
                    .Where(t => t.ID == id)
                    .AsEnumerable()
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
                        IsChecked = false
                    })
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCabinetCheckable(bool all = false)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets
                    .Where(t => all == false || t.Status == (int)DB.CabinetStatus.Install)
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID))
                    .AsEnumerable()
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetCabinetCheckableByID(int cabinetID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets
                    .Where(t => t.ID == cabinetID)
                    .AsEnumerable()
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
        public static List<Cabinet> GetCabinet()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.Status == (int)DB.CabinetStatus.Install).ToList();
            }
        }

        public static int GetCabinetNumberByCabinetInputID(long CabinetInputID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Join(context.CabinetInputs, c => c.ID, i => i.CabinetID, (c, i) => new { Cabinet = c, CabinetInput = i })
                    .Where(t => t.CabinetInput.ID == CabinetInputID).Select(t => t.Cabinet.CabinetNumber).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetNormalCabinetCheckableByType(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                t.CenterID == centerID &&
                                t.Status == (int)DB.CabinetStatus.Install &&
                               (t.CabinetUsageType != (int)DB.CabinetUsageType.OpticalCabinet && t.CabinetUsageType != (int)DB.CabinetUsageType.WLL)
                           )
                    .OrderBy(t => t.CabinetNumber)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString(),
                        Description = t.CabinetUsageType1.Name,
                        IsChecked = false
                    }
                    )
                    .ToList();
            }
        }
        public static List<CheckableItem> GetCabinetCheckableByType(int centerID)
        {
            if (centerID == 0)
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.Cabinets
                        .Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                               t.Status == (int)DB.CabinetStatus.Install)
                        .OrderBy(t => t.CabinetNumber)
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.CabinetNumber.ToString(),
                            Description = t.CabinetUsageType1.Name,
                            IsChecked = false
                        }
                        )
                        .ToList();
                }
            }
            else
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.Cabinets.Where(t => t.CenterID == centerID &&
                        t.Status == (int)DB.CabinetStatus.Install
                        ).OrderBy(t => t.CabinetNumber).AsEnumerable()
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
                            Description = t.CabinetUsageType1.Name,
                            IsChecked = false
                        }
                            )
                        .ToList();
                }
            }
        }

        //milad doran
        //public static List<CheckableItem> GetCabinetCheckableByCenterID(int centerID)
        //{
        //    if (centerID == 0)
        //    {
        //        using (MainDataContext context = new MainDataContext())
        //        {
        //            return context.Cabinets.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) && t.Status == (int)DB.CabinetStatus.Install)
        //                .OrderBy(t => t.CabinetNumber)
        //                .AsEnumerable()
        //                .Select(t => new CheckableItem
        //                {
        //                    ID = t.ID,
        //                    Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
        //                    IsChecked = false
        //                }
        //                    )
        //                .ToList();
        //        }
        //    }
        //    else
        //    {
        //        using (MainDataContext context = new MainDataContext())
        //        {
        //            return context.Cabinets.Where(t => t.CenterID == centerID && t.Status == (int)DB.CabinetStatus.Install)
        //                .OrderBy(t => t.CabinetNumber)
        //                .AsEnumerable()
        //                .Select(t => new CheckableItem
        //                {
        //                    ID = t.ID,
        //                    Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
        //                    IsChecked = false
        //                }
        //                    )
        //                .ToList();
        //        }
        //    }
        //}

        //TODO:rad 13950221
        public static List<CheckableItem> GetCabinetCheckableByCenterID(int centerID)
        {
            List<CheckableItem> result = new List<CheckableItem>();
            if (centerID == 0)
            {
                using (MainDataContext context = new MainDataContext())
                {

                    result = context.Cabinets.Where(t =>
                                                        (DB.CurrentUser.CenterIDs.Contains(t.CenterID)) &&
                                                        (t.Status == (int)DB.CabinetStatus.Install)
                                                   )
                                             .OrderBy(t => t.CabinetNumber)
                                             .AsEnumerable()
                                             .Select(t => new CheckableItem
                                                         {
                                                             ID = t.ID,
                                                             Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
                                                             IsChecked = false
                                                         }
                                                     )
                                             .ToList();
                }
            }
            else
            {
                using (MainDataContext context = new MainDataContext())
                {
                    result = context.Cabinets.Where(t =>
                                                        (t.CenterID == centerID) &&
                                                        (t.Status == (int)DB.CabinetStatus.Install)
                                                   )
                                             .OrderBy(t => t.CabinetNumber)
                                             .AsEnumerable()
                                             .Select(t => new CheckableItem
                                                        {
                                                            ID = t.ID,
                                                            Name = t.CabinetNumber.ToString() + DB.GetDescription(t.CabinetCode),
                                                            IsChecked = false
                                                        }
                                                    )
                                             .ToList();
                }
            }
            return result;
        }

        public static List<CheckableItem> GetCabinetCheckableByCenterIDByCabinetUsageType(int centerID, List<int> cabinetUsageTypes)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.CenterID == centerID &&
                        t.Status == (int)DB.CabinetStatus.Install &&
                        cabinetUsageTypes.Contains(t.CabinetUsageType))
                    .OrderBy(t => t.CabinetNumber)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString(),
                        Description = t.CabinetUsageType1.Name,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }

        }


        public static List<CheckableItem> GetCabinetCheckableByCenterIDWithoutGeographic()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) && t.Latitude == null && t.Longitude == null)
                    .OrderBy(t => t.CabinetNumber)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }

        }
        public static List<Cabinet> GetCabinetByCenterID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(c => c.CenterID == id).ToList();
            }
        }

        public static List<Cabinet> GetCabinetByCenterIDs(List<int> ids)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(c => ids.Contains(c.CenterID)).ToList();
            }
        }

        public static List<Cabinet> GetCabinetListByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(c => c.ID == id).ToList();
            }
        }

        public static Cabinet GetCabinetListByCabinetNumber(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(c => c.CabinetNumber == id).SingleOrDefault();
            }
        }
        public static List<Cabinet> GetCabinetListByTypeID(int cabinetTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.CabinetTypeID == cabinetTypeID).ToList();
            }
        }

        /// <summary>
        /// لیست ورودی های کافو که متصل نیستن را بر میگرداند
        /// </summary>
        /// <param name="CabinetID"></param>
        /// <returns></returns>
        public static List<CheckableItem> GetFreeCabinetInputByCabinetID(int CabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //// first get list of cabinet inputs than connected to bucht
                //// then get list of cabinet inputs than is not in it list
                //return context.CabinetInputs.Where(t => t.CabinetID == CabinetID && t.Status == (byte)DB.CabinetInputStatus.healthy)
                //                            .Where(t => !context.Buchts.Where(b => b.CabinetInput.CabinetID == CabinetID && b.CabinetInputID != null && b.ConnectionID != null).Select(b => b.CabinetInputID).Contains(t.ID))
                //                            .OrderBy(t => t.InputNumber)
                //                            .Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(),  IsChecked = false })
                //                            .ToList();

                return context.Buchts.Where(b =>
                                                b.Status == (int)DB.BuchtStatus.Free &&
                                                b.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy &&
                                                b.CabinetInput.CabinetID == CabinetID &&
                                                b.ConnectionID == null &&
                                                b.PCMPortID == null
                                            )
                                      .OrderBy(t => t.CabinetInput.InputNumber)
                                      .Select(t => new CheckableItem 
                                                    { 
                                                        LongID = t.CabinetInputID, 
                                                        Name = t.CabinetInput.InputNumber.ToString(), 
                                                        IsChecked = false 
                                                    }
                                             )
                                      .ToList();
            }

        }

        /// <summary>
        /// لیست ورودی های کافو که متصل نیستن را بر میگرداند
        /// به همراه تلفن در کافو نوری
        /// </summary>
        /// <param name="CabinetID"></param>
        /// <returns></returns>
        public static List<CheckableItem> GetFreeCabinetInputByCabinetIDWithTelephon(int CabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                // first get list of cabinet inputs than connected to bucht
                // then get list of cabinet inputs than is not in it list
                return context.CabinetInputs.Join(context.Buchts, ci => ci.ID, b => b.CabinetInputID, (ci, b) => new { CabinetInput = ci, Bucht = b })
                                            .Where(t => t.Bucht.ConnectionID == null &&
                                                        t.Bucht.SwitchPortID == null &&
                                                        t.Bucht.Status == (byte)DB.BuchtStatus.Free &&
                                                        t.CabinetInput.CabinetID == CabinetID &&
                                                        t.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy
                                                   )
                                            .OrderBy(t => t.CabinetInput.InputNumber)
                                            .Select(t => new CheckableItem
                                                            {
                                                                LongID = t.CabinetInput.ID,
                                                                Name = t.CabinetInput.InputNumber.ToString(),
                                                                IsChecked = false
                                                            }).ToList();

            }

        }

        /// <summary>
        /// لیست ورودی های کافو که متصل هستند را بر میگرداند
        /// </summary>
        /// <param name="CabinetID"></param>
        /// <returns></returns>
        public static List<CheckableItem> GetConnectCabinetInputByCabinetID(int CabinetID)
        {
            List<int> cabinetIDList = new List<int>();
            cabinetIDList.Add(CabinetID);
            List<long?> cabinetIDListConnect = Data.BuchtDB.GetBuchtByCabinetIDs(cabinetIDList).Where(t => t.CabinetInputID != null).Select(t => t.CabinetInputID).ToList();
            return Data.CabinetInputDB.GetCabinetInputByCabinetID(CabinetID).Where(t => cabinetIDListConnect.Contains(t.LongID)).ToList();

        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static List<CheckableItem> GetCabinetCheckableByCenterIDs(List<int> centers)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                result = context.Cabinets.Where(t =>
                                                    centers.Contains(t.CenterID)
                                                )
                                         .OrderBy(t => t.CabinetNumber)
                                         .AsEnumerable()
                                         .Select(t => new CheckableItem
                                                        {
                                                            ID = t.ID,
                                                            Name = t.CabinetNumber + DB.GetDescription(t.CabinetCode),
                                                            IsChecked = false
                                                        }
                                                )
                                         .ToList();
                return result;
            }
        }

        public static List<CheckableItem> GetCabinetCheckableByCentersIdAndCabinetUsageTypesId(List<int> centers, List<int> usageTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                var query = context.Cabinets
                                 .Where(cb =>
                                             (cb.Status == (int)DB.CabinetStatus.Install) &&
                                             (centers.Count == 0 || centers.Contains(cb.CenterID)) &&
                                             (usageTypes.Count == 0 || usageTypes.Contains(cb.CabinetUsageType))
                                       )
                                 .OrderBy(cb => cb.Center.CenterName)
                                 .ThenBy(cb => cb.CabinetNumber)
                                 .Select(cb => new CheckableItem
                                                    {
                                                        ID = cb.ID,
                                                        Name = string.Format("{0} -- {1}", cb.Center.CenterName, cb.CabinetNumber),
                                                        IsChecked = false
                                                    }
                                        )
                                 .AsQueryable();
                result = query.ToList();
                return result;
            }
        }

        public static bool LeaveCabinetInputFromBucht(long fromCabinetInputID, long toCabinetInputID)
        {
            if (fromCabinetInputID != 0 && toCabinetInputID != 0)
            {

                List<Bucht> buchts = Data.BuchtDB.GetBuchetByFromCabinetIDToCabinetID(fromCabinetInputID, toCabinetInputID);
                List<CablePair> CablePairs = Data.CablePairDB.GetCablePairFromCabinetInputIDToCabinetInputID(fromCabinetInputID, toCabinetInputID);

                if (buchts.Any(t => t.SwitchPortID != null)) { MessageBox.Show("در میان بوخت ها بوخت متصل به مشترک وجود دارد. امکان تغییر این بوخت نیست."); return false; }

                if (buchts.Any(t => t.Status == (byte)DB.BuchtStatus.AllocatedToInlinePCM)) { MessageBox.Show("در میان بوخت ها بوخت متصل ورودی پی سی ام وجود دارد. امکان تغییر این بوخت نیست."); return false; }

                CablePairs.ForEach((CablePair CablePair) => { CablePair.CabinetInputID = null; CablePair.Detach(); });

                buchts.ForEach((Bucht bucht) => { bucht.CabinetInputID = null; bucht.Detach(); });
                using (TransactionScope ts = new TransactionScope())
                {

                    DB.UpdateAll(buchts);
                    DB.UpdateAll(CablePairs);
                    ts.Complete();
                }
                return true;
            }
            else
            {
                MessageBox.Show("لطفا همه فیلد های مورد نیاز را پر کنید");
                return false;
            }
        }

        public static bool AssignCabinetInputToCable(long fromCabinetInputID, long toCabinetInputID, long fromCablePairID, long toCablePairID)
        {
            if (fromCabinetInputID != 0 && toCabinetInputID != 0 && fromCablePairID != 0 && toCablePairID != 0)
            {
                // Because the  CabinetInputID column is in the table's Bucht and CablePair. while assingnmet, both table will be update

                List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(fromCabinetInputID, toCabinetInputID);
                List<CablePair> CablePairs = Data.CablePairDB.GetCablePairFromIDToToID(fromCablePairID, toCablePairID);
                List<Bucht> buchts = Data.BuchtDB.GetbuchtsConnectedToCablePairs(CablePairs);

                if (CablePairs.Any(t => t.CabinetInputID != null))
                {
                    MessageBox.Show("از میان زوج کابل های انتخاب شده، متصل به مرکزی وجود دارد،");
                    return false;
                }
                if (cabinetInputs.Count != CablePairs.Count)
                {
                    MessageBox.Show("تعداد مرکزی های انتخاب شده برابر با تعداد زوج کابل نمی باشد");
                    return false;
                }
                if (CablePairs.Count != buchts.Count)
                {
                    MessageBox.Show("تعداد زوج کابل های انتخاب شده برابر با تعداد زوج کابل های متصل به بوخت نمی باشد");
                    return false;
                }


                for (int i = 0; i < buchts.Count; i++)
                {
                    buchts[i].CabinetInputID = CablePairs[i].CabinetInputID = cabinetInputs[i].ID;
                    buchts[i].Detach();
                    CablePairs[i].Detach();
                }


                using (TransactionScope ts = new TransactionScope())
                {
                    DB.UpdateAll(buchts);
                    DB.UpdateAll(CablePairs);
                    ts.Complete();
                }
                return true;
            }
            else
            {
                MessageBox.Show("لطفا همه فیلد های مورد نیاز را پر کنید");
                return false;
            }
        }

        public static bool AssignFreeCabinetInputToFreeCable(long fromCabinetInputID, long toCabinetInputID, long fromCablePairID, long toCablePairID)
        {
            if (fromCabinetInputID != 0 && toCabinetInputID != 0 && fromCablePairID != 0 && toCablePairID != 0)
            {
                // Because the  CabinetInputID column is in the table's Bucht and CablePair. while assingnmet, both table will be update

                List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(fromCabinetInputID, toCabinetInputID, true);

                List<CablePair> CablePairs = Data.CablePairDB.GetCablePairFromIDToToIDFreeOfBucht(fromCablePairID, toCablePairID);

                List<Bucht> buchts = Data.BuchtDB.GetBuchtByCablePairIDs(CablePairs.Select(t => t.ID).ToList());

                if (CablePairs.Any(t => t.CabinetInputID != null))
                {
                    MessageBox.Show("از میان زوج کابل های انتخاب شده، متصل به مرکزی وجود دارد،");
                    return false;
                }
                if (cabinetInputs.Count != CablePairs.Count)
                {
                    MessageBox.Show("تعداد مرکزی های انتخاب شده برابر با تعداد زوج کابل نمی باشد");
                    return false;
                }
                if (CablePairs.Count != buchts.Count)
                {
                    MessageBox.Show("تعداد زوج کابل های انتخاب شده برابر با تعداد زوج کابل های متصل به بوخت نمی باشد");
                    return false;
                }


                for (int i = 0; i < buchts.Count; i++)
                {
                    buchts[i].CabinetInputID = CablePairs[i].CabinetInputID = cabinetInputs[i].ID;
                    buchts[i].Detach();
                    CablePairs[i].Detach();
                }


                using (TransactionScope ts = new TransactionScope())
                {
                    DB.UpdateAll(buchts);
                    DB.UpdateAll(CablePairs);
                    ts.Complete();
                }
                return true;
            }
            else
            {
                MessageBox.Show("لطفا همه فیلد های مورد نیاز را پر کنید");
                return false;
            }
        }

        /// <summary>
        /// A compiled query that returns a list of Orders.  
        /// Since it is static, it will be re-used over and over again, but since the query is parameterized, it is very versatile.
        /// </summary>
        private static Func<MainDataContext, long, IQueryable<Bucht>> GetBuchtByTelephoneQuery =
            CompiledQuery.Compile((MainDataContext context, long Telephone) =>
                                  (
                                    context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == Telephone).SingleOrDefault().SwitchPortID)
                                   ));

        private static Func<MainDataContext, long, IQueryable<Telephone>> GetTelephoneQuery =
    CompiledQuery.Compile((MainDataContext context, long TelephoneNo) =>
                          (
                           context.Telephones.Where(tel => tel.TelephoneNo == TelephoneNo)
                           ));

        public static List<BuchtAndTelephonOfOpticalCabinet> GetGroupsBuchtAndTelephonOfOpticalCabinet(int cabintID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<long> input = context.Buchts.Where(b => b.CabinetInput.CabinetID == cabintID).Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { telephone = t, bucht = b }).Select(t2 => t2.telephone.TelephoneNo).ToList();
                var der = input.GroupBy(TelephoneNoGroupBy => input.Where(candidate => candidate >= TelephoneNoGroupBy)
                                                                         .OrderBy(candidate => candidate)
                                                                         .TakeWhile((candidate, index) => candidate == TelephoneNoGroupBy + index)
                                                                         .Last()
                                    ).Select((seq, index) => new BuchtAndTelephonOfOpticalCabinet
                                    {
                                        ID = index + 1,
                                        FromTelephone = seq.Min(),
                                        ToTelephone = seq.Max(),
                                        //BuchtTypeName = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().BuchtType.BuchtTypeName,
                                        //BuchtTypeNameID = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().BuchtType.ID,

                                        //FromBuchtID = GetBuchtByTelephoneQuery(context, seq.Min()).SingleOrDefault().ID,
                                        //FromBuchtNo = GetBuchtByTelephoneQuery(context, seq.Min()).SingleOrDefault().BuchtNo.ToString(),
                                        //FromVerticalCloumnID = GetBuchtByTelephoneQuery(context, seq.Min()).SingleOrDefault().VerticalMDFRow.VerticalMDFColumnID,
                                        //FromVerticalCloumnNo = GetBuchtByTelephoneQuery(context, seq.Min()).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                        //FromVerticalRowID = GetBuchtByTelephoneQuery(context, seq.Min()).SingleOrDefault().VerticalMDFRow.ID,
                                        //FromVerticalRowNo = GetBuchtByTelephoneQuery(context, seq.Min()).SingleOrDefault().VerticalMDFRow.VerticalRowNo.ToString(),

                                        //ToBuchtID = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().ID,
                                        //ToBuchtNo = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().BuchtNo.ToString(),
                                        //ToVerticalCloumnID = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().VerticalMDFRow.VerticalMDFColumnID,
                                        //ToVerticalCloumnNo = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                        //ToVerticalRowID = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().VerticalMDFRow.ID,
                                        //ToVerticalRowNo = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().VerticalMDFRow.VerticalRowNo.ToString(),

                                        //MDFID = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                                        //MDFNumber = GetBuchtByTelephoneQuery(context, seq.Max()).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),

                                        //SwitchCode = GetTelephoneQuery(context, seq.Max()).SingleOrDefault().SwitchPort.Switch.SwitchCode.ToString(),
                                        //SwitchID = GetTelephoneQuery(context, seq.Max()).SingleOrDefault().SwitchPort.SwitchID,
                                        //SwitchPrecodeID = GetTelephoneQuery(context, seq.Max()).SingleOrDefault().SwitchPrecode.ID,
                                        SwitchPreNo = GetTelephoneQuery(context, seq.Max()).SingleOrDefault().SwitchPrecode.SwitchPreNo.ToString()
                                    }).ToList();


                //var der = context.Buchts.Where(b => b.CabinetInput.CabinetID == cabintID)
                //                 .Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { telephone = t, bucht = b }).Select(t2 => t2.telephone.TelephoneNo).ToList()
                //                 .GroupBy(TelephoneNoGroupBy => context.Buchts.Where(b => b.CabinetInput.CabinetID == cabintID)
                //                                                              .Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { telephone = t, bucht = b }).Select(t2 => t2.telephone.TelephoneNo).ToList()
                //                                                              .Where(candidate => candidate >= TelephoneNoGroupBy)
                //                                                              .OrderBy(candidate => candidate)
                //                                                              .TakeWhile((candidate, index) => candidate == TelephoneNoGroupBy + index)
                //                                                              .Last()
                //                         ).Select(seq => new BuchtAndTelephonOfOpticalCabinet
                //                                                  {
                //                                                      FromTelephone = seq.Min(),
                //                                                      ToTelephone = seq.Max(),
                //                                                      BuchtTypeName = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().BuchtType.BuchtTypeName,
                //                                                      BuchtTypeNameID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().BuchtType.ID,

                //                                                      FromBuchtID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Min()).SingleOrDefault().SwitchPortID).SingleOrDefault().ID,
                //                                                      FromBuchtNo = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Min()).SingleOrDefault().SwitchPortID).SingleOrDefault().BuchtNo.ToString(),
                //                                                      FromVerticalCloumnID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Min()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumnID,
                //                                                      FromVerticalCloumnNo = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Min()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                //                                                      FromVerticalRowID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Min()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.ID,
                //                                                      FromVerticalRowNo = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Min()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalRowNo.ToString(),

                //                                                      ToBuchtID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().ID,
                //                                                      ToBuchtNo = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().BuchtNo.ToString(),
                //                                                      ToVerticalCloumnID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumnID,
                //                                                      ToVerticalCloumnNo = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                //                                                      ToVerticalRowID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.ID,
                //                                                      ToVerticalRowNo = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalRowNo.ToString(),

                //                                                      MDFID = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                //                                                      MDFNumber = context.Buchts.Where(t => t.SwitchPortID == context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPortID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                //                                                      SwitchCode = context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPort.Switch.SwitchCode.ToString(),
                //                                                      SwitchID = context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPort.SwitchID,
                //                                                      SwitchPrecodeID = context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPrecode.ID,
                //                                                      SwitchPreNo = context.Telephones.Where(tel => tel.TelephoneNo == seq.Max()).SingleOrDefault().SwitchPrecode.SwitchPreNo.ToString()
                //                                                  }).ToList();


                //var der = input.GroupBy(TelephoneNoGroupBy => input.Where(candidate => candidate >= TelephoneNoGroupBy)
                //                                                              .OrderBy(candidate => candidate)
                //                                                              .TakeWhile((candidate, index) => candidate == TelephoneNoGroupBy + index)
                //                                                              .Last()
                //                         ).Select(seq => new BuchtAndTelephonOfOpticalCabinet
                //                                                  {
                //                                                      FromTelephone = seq.Min(),
                //                                                      ToTelephone = seq.Max(),
                //                                                       BuchtTypeName = context.Buchts.Where(t=>t.SwitchPort.Telephones.

                //                                                  }).ToList();

                //var temp =  context.Buchts.Where(b => b.CabinetInput.CabinetID == cabintID).Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { telephone = t, bucht = b }).Select(t2 => t2.telephone.TelephoneNo)
                //                                                          .GroupBy(TelephoneNoGroupBy => context.Buchts.Where(b => b.CabinetInput.CabinetID == cabintID).Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { telephone = t, bucht = b }).Select(t2 => t2.telephone.TelephoneNo)
                //                                                                                                        .Where(candidate => candidate >= TelephoneNoGroupBy)
                //                                                                                                        .OrderBy(candidate => candidate)
                //                                                                                                        .TakeWhile((candidate, index) => candidate == TelephoneNoGroupBy + index)
                //                                                                                                        .Last()
                //                       ).Select(seq => new {MaxTelephone = seq.Max() , MinTelephone = seq.Min() })
                //                       .Join(context.Telephones, seq => seq.MaxTelephone, t => t.TelephoneNo, (seq, t) => new { seq = seq, MaxTele = t })
                //                       .Join(context.Buchts , seq => seq.MaxTele.SwitchPortID ,  b => b.SwitchPortID , (seq, b) => new {seq = seq.seq, MaxTeleBucht = b })
                //                       .Join(context.Telephones, seq => seq.seq.MinTelephone, t => t.TelephoneNo, (seq, t) => new { seq = seq.seq, MinTele = t, MaxTeleBucht = seq.MaxTeleBucht })
                //                       .Join(context.Buchts, seq => seq.MinTele.SwitchPortID, b => b.SwitchPortID, (seq, b) => new { seq = seq.seq, MinTeleBucht = b, MaxTeleBucht = seq.MaxTeleBucht , MinTele = seq.MinTele})
                //                       .Select((t) => new BuchtAndTelephonOfOpticalCabinet
                //                                                {

                //                                                   // ID = index + 1,
                //                                                    FromTelephone = t.seq.MinTelephone,
                //                                                    ToTelephone = t.seq.MaxTelephone,
                //                                                    BuchtTypeName = t.MinTeleBucht.BuchtType.BuchtTypeName,
                //                                                    BuchtTypeNameID = t.MinTeleBucht.BuchtType.ID,
                //                                                    FromBuchtID = t.MinTeleBucht.ID,
                //                                                    FromBuchtNo = t.MinTeleBucht.BuchtNo.ToString(),
                //                                                    ToBuchtID = t.MaxTeleBucht.ID,
                //                                                    ToBuchtNo = t.MaxTeleBucht.BuchtNo.ToString(),
                //                                                    FromVerticalCloumnID = t.MinTeleBucht.VerticalMDFRow.VerticalMDFColumnID,
                //                                                    FromVerticalCloumnNo = t.MinTeleBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                //                                                    ToVerticalCloumnID = t.MaxTeleBucht.VerticalMDFRow.VerticalMDFColumnID,
                //                                                    ToVerticalCloumnNo = t.MaxTeleBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                //                                                    FromVerticalRowID = t.MinTeleBucht.VerticalMDFRow.ID,
                //                                                    FromVerticalRowNo = t.MinTeleBucht.VerticalMDFRow.VerticalRowNo.ToString(),
                //                                                    ToVerticalRowID = t.MaxTeleBucht.VerticalMDFRow.ID,
                //                                                    ToVerticalRowNo = t.MaxTeleBucht.VerticalMDFRow.VerticalRowNo.ToString(),
                //                                                    MDFID = t.MinTeleBucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                //                                                    MDFNumber = t.MinTeleBucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                //                                                    SwitchCode = t.MinTeleBucht.SwitchPort.Switch.SwitchCode.ToString(),
                //                                                    SwitchID = t.MinTeleBucht.SwitchPort.SwitchID,
                //                                                    SwitchPrecodeID = t.MinTele.SwitchPrecode.ID,
                //                                                    SwitchPreNo = t.MinTele.SwitchPrecode.SwitchPreNo.ToString()

                //                                                }).ToList();






                return der.ToList();
            }
        }

        //public static List<BuchtAndTelephonOfOpticalCabinet> GetGroupsTelephonOfOpticalCabinet(int cabintID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        List<long> input = context.Telephones.Where(b => b.CabinetID == cabintID).Select(t2 => t2.TelephoneNo).ToList();
        //        var der = input.GroupBy(TelephoneNoGroupBy => input.Where(candidate => candidate >= TelephoneNoGroupBy)
        //                                                                 .OrderBy(candidate => candidate)
        //                                                                 .TakeWhile((candidate, index) => candidate == TelephoneNoGroupBy + index)
        //                                                                 .Last()
        //                            ).Select((seq, index) => new BuchtAndTelephonOfOpticalCabinet
        //                            {
        //                                ID = index + 1,
        //                                FromTelephone = seq.Min(),
        //                                ToTelephone = seq.Max(),
        //                                SwitchPreNo = GetTelephoneQuery(context, seq.Max()).SingleOrDefault().SwitchPrecode.SwitchPreNo.ToString()
        //                            }).ToList();
        //        return der.ToList();
        //    }
        //}

        public static List<InputInfo> GetInputStatistic(int CenterIds, List<int> CabinetIDs, int CabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts


                    .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, telephone = t })
                    .SelectMany(t1 => t1.telephone.DefaultIfEmpty(), (bt, t1) => new { Bucht = bt.bucht, Telephone = t1 })

                    .Where(t => (CenterIds == t.Bucht.CabinetInput.Cabinet.CenterID)
                        && (CabinetIDs.Count == 0 || CabinetIDs.Contains(t.Bucht.CabinetInput.Cabinet.ID))
                        && (CabinetInputID == -1 || (t.Bucht.CabinetInput.InputNumber == (int)CabinetInputID))
                    )
                    .Select(t => new InputInfo
                    {
                        BuchtID = t.Bucht.ID,
                        CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                        TeleNo = t.Telephone.TelephoneNo.ToString(),
                        Status = t.Bucht.CabinetInput.Status,
                        CustomerName = (t.Telephone.Customer.FirstNameOrTitle ?? "") + " " + (t.Telephone.Customer.LastName ?? ""),
                        InputNumber = t.Bucht.CabinetInput.InputNumber,
                        CenterName = t.Bucht.CabinetInput.Cabinet.Center.CenterName,
                        BuchtTypeName = t.Bucht.BuchtType.BuchtTypeName,
                        VerticalCloumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        VerticalRowNo = t.Bucht.VerticalMDFRow.VerticalRowNo,
                        BuchtNo = t.Bucht.BuchtNo,
                        Capacity = t.Bucht.CabinetInput.Cabinet.Capacity,
                        BuchtStatus = t.Bucht.Status,
                        CabinetInputID = t.Bucht.CabinetInput.ID
                    }
                    ).ToList();
            }
        }

        public static Cabinet GetCabinetByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => context.Posts.Where(p => p.ID == postID).SingleOrDefault().CabinetID == t.ID).SingleOrDefault();
            }
        }

        public static Cabinet GetCabinetByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.ID == context.CabinetInputs.Where(t3 => t3.ID == context.Buchts.Where(t2 => t2.ID == buchtID).SingleOrDefault().CabinetInputID).SingleOrDefault().CabinetID).SingleOrDefault();
            }
        }

        public static int? GetCabinetBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.SwitchID == switchID).Select(t => t.ID).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetOpticalAndWLLCabinetCheckableByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.CenterID == centerID && (t.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || t.CabinetUsageType == (int)DB.CabinetUsageType.WLL))
                    .OrderBy(t => t.CabinetNumber)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString(),
                        Description = t.CabinetUsageType1.Name,
                        IsChecked = false
                    }
                    )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetNormalCabinetCheckableByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.CenterID == centerID && t.CabinetUsageType != (int)DB.CabinetUsageType.OpticalCabinet && t.CabinetUsageType != (int)DB.CabinetUsageType.WLL)
                    .OrderBy(t => t.CabinetNumber)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString(),
                        Description = t.CabinetUsageType1.Name,
                        IsChecked = false
                    }
                    )
                    .ToList();
            }
        }

        public static List<Cabinet> GetCabinetsHaveLocation()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.Latitude != null && t.Longitude != null).ToList();
            }
        }

        public static List<CheckableItem> GetCabinetCheckableByCenterIDWithoutCoordinates(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(t => t.CenterID == centerID && t.Latitude == null && t.Longitude == null)
                    .OrderBy(t => t.CabinetNumber)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CabinetNumber.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static int GetRemainedQuotaReservationByCabinetID(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int cabinetShare = 0;
                int.TryParse(DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.ApplyCabinetShare)), out cabinetShare);

                //return (context.Buchts.Where(t2 => t2.CabinetInput.CabinetID == cabinetID && t2.Status == (byte)DB.BuchtStatus.Free).Count()) - (context.CabinetInputs.Where(t3 => t3.CabinetID == cabinetID && t3.Status == (int)DB.CabinetInputStatus.healthy).Count() * cabinetShare / 100);

                return (context.Buchts.Where(bu => bu.CabinetInput.CabinetID == cabinetID && bu.Status == (byte)DB.BuchtStatus.Free && bu.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy && bu.PCMPortID == null).Count())
                    - (context.Buchts.Where(bu => bu.CabinetInput.CabinetID == cabinetID && bu.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy && bu.PCMPortID == null).Count() * cabinetShare / 100);
            }
        }

        public static Cabinet GetcabinetbyTelephonNo(long telephoneNo)
        {
            Switch switchItem = SwitchDB.GetSwitchByTelephonNo(telephoneNo);
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Cabinets.Any(t => t.SwitchID == switchItem.ID))
                {
                    return null;
                }
                else
                {
                    return context.Cabinets.Where(t => t.SwitchID == switchItem.ID).SingleOrDefault();
                }
            }
        }

        public static Cabinet GetCabinetByCabinetInputID(long cabinetInput)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.ID == cabinetInput).Select(t => t.Cabinet).SingleOrDefault();
            }
        }

        public static string ExistPostNumberInCabinet(List<int> postNumbers, int cabinetID)
        {
            string postNumber = string.Empty;

            using (MainDataContext context = new MainDataContext())
            {
                if (context.Posts.Any(t => t.CabinetID == cabinetID && postNumbers.Contains(t.Number)))
                {
                    postNumber = string.Join(",", context.Posts.Where(t => t.CabinetID == cabinetID && postNumbers.Contains(t.Number)).Select(t => t.Number.ToString()).ToList());
                }
            }

            return postNumber;
        }

        public static int GetCabinetInputCount(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.CabinetID == cabinetID).Count();
            }
        }
    }
}