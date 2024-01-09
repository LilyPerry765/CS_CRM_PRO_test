using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Transactions;
using System.Threading.Tasks;

namespace CRM.Data
{
    public static class SwitchPrecodeDB
    {
        //        public static List<SwitchPrecode> SearchSwitchPrecode(byte preCodeType, byte deploymentType, byte dorshoalNumberType, byte status, string id, string center, string switch, string capacity, string operationalCapacity, string installCapacity, string specialServiceCapacity, long switchPreNo)
        //        {
        //            using (MainDataContext context = new MainDataContext())
        //            {
        //                return context.SwitchPrecodes
        //                    .Where(t => 
        //                            (preCodeType == -1 || t.PreCodeType == preCodeType) && 
        //(deploymentType == -1 || t.DeploymentType == deploymentType) && 
        //(dorshoalNumberType == -1 || t.DorshoalNumberType == dorshoalNumberType) && 
        //(status == -1 || t.Status == status) && 
        //(id == -1 || t.ID == id) && 
        //(center.Count == 0 || center.Contains(t.CenterID) && 
        //(switch.Count == 0 || switch.Contains(t.SwitchID) && 
        //(capacity == -1 || t.Capacity == capacity) && 
        //(operationalCapacity == -1 || t.OperationalCapacity == operationalCapacity) && 
        //(installCapacity == -1 || t.InstallCapacity == installCapacity) && 
        //(specialServiceCapacity == -1 || t.SpecialServiceCapacity == specialServiceCapacity) && 
        //(switchPreNo == -1 || t.SwitchPreNo == switchPreNo)
        //                          )
        //                    //.OrderBy(t => t.Name)
        //                    .ToList();
        //            }
        //        }

        public static List<CheckableItem> GetSwitchPrecodeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false, Description = "(" + t.FromNumber + "-" + t.ToNumber + ")" }).ToList();
            }
        }

        public static List<CheckableItem> GetSwitchPrecodeCheckableByCenterIDs(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Where(t => centerIDs.Contains(t.Switch.CenterID)).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false, Description = "(" + t.FromNumber + "-" + t.ToNumber + ")" }).ToList();
            }
        }

        public static string Save(SwitchPrecode item, string startNo, string endNo, int switchID, bool autoPort, byte? usageType)
        {
            string ONUfeatures = string.Empty;
            Data.Switch switchitem = Data.SwitchDB.GetSwitchByID((int)switchID);
            Data.SwitchType switchType = Data.SwitchTypeDB.GetSwitchTypeBySwitchID(switchID);
            string TelephoneList = string.Empty;
            byte switchtypeID = Data.SwitchDB.GetTypeOfSwitchTypeBySwitchID(switchitem.ID);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, System.TimeSpan.FromMinutes(0)))
            {
                item.SwitchID = switchID;
                DB.Save(item);

                long startTele = Convert.ToInt64(startNo);
                long endTele = Convert.ToInt64(endNo);
                City city = Data.CityDB.GetCityByCenterID(item.CenterID);
                if (startTele <= endTele)
                {
                    List<Telephone> TelePhonList = new List<Telephone>();
                    List<SwitchPort> switchPorts = new List<SwitchPort>();
                    for (long i = startTele; i <= endTele; i++)
                    {
                        Telephone telePhone = new Telephone();
                        telePhone.RoundType = (byte?)DB.CheckTypeRoundTelephone(i);
                        if (telePhone.RoundType != null)
                        {
                            telePhone.IsRound = true;
                        }
                        telePhone.TelephoneNoIndividual = i;
                        telePhone.TelephoneNo = Convert.ToInt64(city.Code.ToString() + i.ToString());
                        telePhone.UsageType = usageType;
                        telePhone.InRoundSale = false;
                        telePhone.SwitchPrecodeID = item.ID;
                        telePhone.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;
                        telePhone.CenterID = item.CenterID;
                        telePhone.Status = (byte)DB.TelephoneStatus.Free;

                        // analyse of tehran
                        if (autoPort == false)
                        {
                            if (switchType != null && switchType.SwitchTypeValue == (byte)Data.DB.SwitchTypeCode.FixedSwitch)
                            {
                                TelephoneList += switchitem.SwitchCode.ToString() + ";" + telePhone.TelephoneNo.ToString() + ";" + item.PreCodeType.ToString() + ";";
                            }
                            else if (switchType != null && switchType.SwitchTypeValue == (byte)Data.DB.SwitchTypeCode.ONUCopper)
                            {
                                TelephoneList += switchitem.SwitchCode.ToString() + ";" + telePhone.TelephoneNo.ToString() + ";" + item.PreCodeType.ToString() + ";" + "Z" + ";" + ";" + ";" + ";" + ONUfeatures;
                            }
                            else if (switchType != null && switchType.SwitchTypeValue == (byte)Data.DB.SwitchTypeCode.ONUVWire)
                            {
                                TelephoneList += switchitem.SwitchCode.ToString() + ";" + telePhone.TelephoneNo.ToString() + ";" + item.PreCodeType.ToString() + ";" + "V" + ";" + ";" + ";" + ";" + ONUfeatures;
                            }
                            else if (switchType != null && switchType.SwitchTypeValue == (byte)Data.DB.SwitchTypeCode.ONUABWire)
                            {
                                TelephoneList += switchitem.SwitchCode.ToString() + ";" + telePhone.TelephoneNo.ToString() + ";" + item.PreCodeType.ToString() + ";" + "A" + ";" + ";" + ";" + ";" + ONUfeatures;
                            }

                            TelephoneList += Environment.NewLine;
                        }
                        else
                        {
                            SwitchPort switchPort = new SwitchPort();
                            switchPort.PortNo = city.Code.ToString() + i.ToString();
                            switchPort.Status = (byte)DB.SwitchPortStatus.Install;
                            switchPort.SwitchID = item.SwitchID;

                            if (switchtypeID == (byte)DB.SwitchTypeCode.FixedSwitch || switchtypeID == (byte)DB.SwitchTypeCode.ONUABWire)
                            {
                                switchPort.Type = true;
                            }
                            else
                            {
                                switchPort.Type = false;
                            }
                            switchPort.Detach();
                            switchPorts.Add(switchPort);
                            //  DB.Save(switchPort);
                            //  telePhone.SwitchPortID = switchPort.ID;
                        }
                        //  Enterprise.Logger.WriteInfo(telePhone.TelephoneNo.ToString());
                        telePhone.Detach();
                        TelePhonList.Add(telePhone);
                    }
                    DB.BulkInsertAll<SwitchPort>(switchPorts);

                    DB.BulkInsertAll<Telephone>(TelePhonList);

                    TelephoneDB.UpdateTelephoneSwitchPortID(item);
                    // Enterprise.Logger.WriteInfo("telephone updated");

                    //    List<SwitchPort> insertedSwitchPorts = new List<SwitchPort>();
                    //= Data.SwitchPortDB.GetSwitchPortByPortNo(TelePhonList.Select(t=>Convert.ToString(t.TelephoneNo)).ToList());

                    //using(MainDataContext context = new MainDataContext())
                    //{
                    //   insertedSwitchPorts = context.SwitchPorts.Where(t => t.SwitchID == switchID && Convert.ToInt64(t.PortNo) >= Convert.ToInt64(startNo) && Convert.ToInt64(t.PortNo) <= Convert.ToInt64(endNo)).ToList();
                    //}

                    //   DB.SaveAll(switchPorts);
                    //TelePhonList.ForEach(t => { t.SwitchPortID = switchPorts.Find(t2 => Convert.ToInt64(t2.PortNo) == t.TelephoneNo).ID; t.Detach(); });
                    //  DB.SaveAll(TelePhonList);

                }
                scope.Complete();
            }
            return TelephoneList;
        }

        public static SwitchPrecode GetSwitchPrecodeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
        public static List<SwitchPrecode> GetSwitchPrecodeBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                // برای حذف پیش شماره های ریموت به مرکز دیگر مرکز پیش شماره با مرکز سوئیچ باید برابر باشد
                return context.SwitchPrecodes.Where(t => t.SwitchID == switchID && t.CenterID == context.Switches.SingleOrDefault(s => s.ID == switchID).CenterID).ToList();
            }
        }

        public static List<CheckableItem> GetE1SwitchPrecodeCheckableItemBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                // برای حذف پیش شماره های ریموت به مرکز دیگر مرکز پیش شماره با مرکز سوئیچ باید برابر باشد
                return context.SwitchPrecodes.Where(t => t.SwitchID == switchID && t.Telephones.Any(tel => tel.UsageType == (byte)DB.TelephoneUsageType.E1) && t.CenterID == context.Switches.SingleOrDefault(s => s.ID == switchID).CenterID).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false }).ToList();
            }
        }





        //public static void Save(SwitchPrecode item, string startNo, string endNo)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        DB.Save(item);

        //            if (Convert.ToInt32(startNo) <= Convert.ToInt32(endNo))
        //            {
        //                List<Telephone> TelePhonList = new List<Telephone>();
        //                for (int i = Convert.ToInt32(startNo); i <= Convert.ToInt32(endNo); i++)
        //                {

        //                    Telephone TelePhon = new Telephone();
        //                    TelePhon.TelephoneNo = i;
        //                    TelePhon.SwitchPrecodeID = item.ID;
        //                    TelePhon.CenterID = item.CenterID;
        //                    TelePhon.Status = 0;
        //                    TelePhonList.Add(TelePhon);
        //                }

        //         DB.SaveAll(TelePhonList);
        //            }

        //        scope.Complete();
        //    }
        //}

        //public static List<CheckableItem> GetSwitchPrecodeCheckable()
        //{
        //    using(MainDataContext context = new MainDataContext())
        //    {
        //        return context.SwitchPrecodes.Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false }).ToList();
        //   }
        //}
        public static string GetProposalTelephone(long buchtID)
        {
            string ProposalTelephone = "";

            // TODO : نحوه استخراج شماره های پیش نهادی مشخص نیست

            //using (MainDataContext context = new MainDataContext())
            //{
            //    int? FeatureONUID = context.Buchts.Where(t => t.ID == buchtID).Select(t => t.FeatureONUID).SingleOrDefault();

            //    if (FeatureONUID != null)
            //    {
            //        List<int> switchPrecodeListID = DB.SearchByPropertyName<SwitchPrecode>("SwitchGroupID", FeatureONUID).Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID)).Select(t => t.ID).ToList();


            //        if (switchPrecodeListID != null)
            //        {
            //            List<long> SwitchPreNoListID = context.Telephones.Where(t => switchPrecodeListID.Contains((int)t.SwitchPrecodeID)).Select(t => t.SwitchPrecode.SwitchPreNo).Distinct().ToList();
            //            foreach (long item in SwitchPreNoListID)
            //            {
            //                ProposalTelephone += item.ToString() + "-";
            //            }
            //            if (ProposalTelephone.Length > 1)
            //                ProposalTelephone = ProposalTelephone.Substring(0, ProposalTelephone.Length - 1);
            //        }
            //    }
            //}

            return ProposalTelephone;
        }

        public static SwitchPrecode GetSwitchPrecodeByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Where(t => t.Telephones.Where(p => p.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPrecodeID == t.ID).SingleOrDefault();
            }

        }

        public static CheckableItem GetSwitchPrecodeCheckableItemByTelephoneNo(string telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Where(t => t.Telephones.Where(p => p.TelephoneNo == Convert.ToInt64(telephoneNo)).Take(1).SingleOrDefault().SwitchPrecodeID == t.ID).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false }).SingleOrDefault();
            }

        }
        /// <summary>
        /// Exist switchPrecode
        /// </summary>
        /// <param name="switchPreNo">SwitchPreNo</param>
        /// <returns>if SwitchPreCode exist return true else retuen false</returns>
        public static bool ExitSwitchPreCode(int switchPreNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Any(t => t.SwitchPreNo == switchPreNo);
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static List<CheckableItem> GetSwitchPrecodeCheckableItemBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Where(t => t.SwitchID == switchID).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false }).ToList();
            }
        }
        /// <summary>
        /// if cabinet is optical. should be displayed itself precodes 
        /// </summary>
        /// <param name="centerID"> center ID</param>
        /// <param name="opticalCabinetID">optical cabinet id</param>
        /// <returns>pre code</returns>
        public static List<CheckableItem> GetSwitchPrecodeCheckableItemByCenterID(int centerID, int? opticalCabinetID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (opticalCabinetID == null)
                {
                    return context.SwitchPrecodes.Where(t => t.CenterID == centerID && t.Switch.SwitchType.SwitchTypeValue != (byte)DB.SwitchTypeCode.ONUVWire).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false, Description = "(" + t.FromNumber + "-" + t.ToNumber + ")" }).Distinct().ToList();
                }
                else
                {
                    return context.SwitchPrecodes.Where(t => t.CenterID == centerID && t.Switch.ID == opticalCabinetID).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false, Description = "(" + t.FromNumber + "-" + t.ToNumber + ")" }).Distinct().ToList();
                }
            }
        }


        public static List<CheckableItem> GetGSMSwitchPrecodeCheckableItemByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {


                return context.SwitchPrecodes.Where(t => t.CenterID == centerID && t.Telephones.Select(t2 => t2.UsageType).Contains((byte)DB.TelephoneUsageType.GSM))
                     .Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false, Description = "(" + t.FromNumber + "-" + t.ToNumber + ")" }).Distinct().ToList();

            }
        }


        public static List<SwitchPrecode> GetSwitchPrecodeByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes
                    .Where(t => t.CenterID == centerID).ToList();

            }
        }


        public static List<SwitchPrecode> GetSwitchPrecodeByCenterIDAndType(int centerID, int precodeType, int usageType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<SwitchPrecode> result = new List<SwitchPrecode>();
                result = context.Telephones
                                .Where(t =>
                                            (t.CenterID == centerID) &&
                                            (t.SwitchPrecode.PreCodeType == precodeType) &&
                                            (t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.FixedSwitch || t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.ONUABWire) &&
                                            (t.UsageType == usageType)
                                       )
                                .Select(t => t.SwitchPrecode)
                                .Distinct()
                                .ToList();
                return result;

            }
        }

        public static List<SwitchPrecode> GetSwitchPrecodeBySwitchIDWithRemotePreCode(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.SwitchPrecodes.Where(t => t.SwitchID == switchID).ToList();
            }
        }

        public static List<Telephone> GetTelephonesByPreCodes(List<int> preCodes, int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (count != -1)
                {
                    return context.Telephones.Where(t => preCodes.Contains((int)t.SwitchPrecodeID) && (t.IsRound == false || t.IsRound == null) && (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge))
                                             .OrderBy(t => t.TelephoneNo).Take(count).ToList();
                }
                else
                {
                    return context.Telephones.Where(t => preCodes.Contains((int)t.SwitchPrecodeID) && (t.IsRound == false || t.IsRound == null) && (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge))
                                             .OrderBy(t => t.TelephoneNo).ToList();
                }
            }
        }

        public static List<SwitchPrecode> GetOpticalCabinetSwitchPrecodeByCenterIDAndType(int centerID, int preCodeType, int? switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.CenterID == centerID && t.SwitchPrecode.SwitchID == switchID && t.SwitchPrecode.PreCodeType == preCodeType).Select(t => t.SwitchPrecode).Distinct().ToList();
            }
        }

        public static List<CheckableItem> GetCheckableItemOpticalCabinetSwitchPrecodeByCenterIDAndType(int centerID, int? switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.CenterID == centerID && t.SwitchPrecode.SwitchID == switchID).Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.TelephoneNo.ToString() }).Distinct().ToList();
            }
        }

        public static List<CheckableItem> GetSwitchPrecodeCheckableByCenterIDsAndPreCodeType(List<int> centerIDs, List<int> preCodes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPrecodes.Where(t => centerIDs.Contains(t.CenterID) && preCodes.Contains(t.PreCodeType)).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false, Description = "(" + t.FromNumber + "-" + t.ToNumber + ")" }).ToList();
            }
        }

        public static int GetPrecodeTypebyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPrecode.PreCodeType;
            }
        }

        public static List<SwitchPrecodeInfo> GetSwitchPrecodes(List<int> citiesId, List<int> centersId, List<int> switchTypesId, long switchPreNo, long fromNumber, long toNumber, bool forPrint, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<SwitchPrecodeInfo> result = new List<SwitchPrecodeInfo>();
                var query = context.Switches
                                   .GroupJoin(context.SwitchPrecodes, swi => swi.ID, swp => swp.SwitchID, (swi, swps) => new { _switch = swi, _switchPrecodes = swps })
                                   .SelectMany(groupedData => groupedData._switchPrecodes.DefaultIfEmpty(), (groupedData, switchPrecode) => new { _switch = groupedData._switch, _switchPrecode = switchPrecode })

                                   .Where(primaryResult =>
                                                        (citiesId.Count == 0 || citiesId.Contains(primaryResult._switch.Center.Region.CityID)) &&
                                                            //(centersId.Count == 0 || centersId.Contains(primaryResult._switch.CenterID)) &&
                                                        (centersId.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(primaryResult._switch.CenterID) : centersId.Contains(primaryResult._switch.CenterID)) &&
                                                        (switchTypesId.Count == 0 || switchTypesId.Contains(primaryResult._switch.SwitchTypeID)) &&
                                                        (switchPreNo == -1 || primaryResult._switchPrecode.SwitchPreNo == switchPreNo) &&
                                                        (fromNumber == -1 || primaryResult._switchPrecode.FromNumber <= fromNumber) &&
                                                        (toNumber == -1 || primaryResult._switchPrecode.ToNumber >= toNumber)
                                          )

                                   .Select(finalResult => new SwitchPrecodeInfo
                                                        {
                                                            CityName = finalResult._switch.Center.Region.City.Name,
                                                            CenterName = finalResult._switch.Center.CenterName,
                                                            SwitchTypeCommercialName = finalResult._switch.SwitchType.CommercialName,
                                                            SwitchPreNo = finalResult._switchPrecode.SwitchPreNo,
                                                            FromNumber = finalResult._switchPrecode.FromNumber,
                                                            ToNumber = finalResult._switchPrecode.ToNumber
                                                        }
                                           )

                                   .OrderBy(res => res.CityName)
                                   .ThenBy(res => res.CenterName)
                                   .ThenBy(res => res.SwitchTypeCommercialName)
                                   .AsQueryable();

                if (forPrint)
                {
                    result = query.ToList();
                    count = result.Count;
                }
                else
                {
                    count = query.Count();
                    result = query.Skip(startRowIndex).Take(pageSize).ToList();
                }

                return result;
            }
        }
    }
}