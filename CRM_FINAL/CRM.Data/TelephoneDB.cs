using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Transactions;

namespace CRM.Data
{
    public static class TelephoneDB
    {
        public static List<TelephoneInfo> SearchTelephone(
            List<int> preCodeType,
            List<int> roundType,
            List<int> status,
            List<int> switchPrecode,
            List<int> switchPort,
            List<int> center,
            bool? isVIP,
            bool? isRound,
            long telephoneNo,
            bool? isRequest,
            int? cusstomerTypeID,
            int? cusstomerGroupID,
            List<int> usageTypes,
            int startRowIndex,
            int pageSize,
            out int count
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<TelephoneInfo> var =
                    context.Telephones
                     .Where(t =>
                             (preCodeType.Count == 0 || preCodeType.Contains(t.SwitchPrecode.PreCodeType)) &&
                             (roundType.Count == 0 || roundType.Contains((int)t.RoundType)) &&
                             (status.Count == 0 || status.Contains(t.Status)) &&
                             (switchPrecode.Count == 0 || switchPrecode.Contains((int)t.SwitchPrecodeID)) &&
                             (switchPort.Count == 0 || switchPort.Contains((int)t.SwitchPortID)) &&
                                 //(center.Count == 0 || center.Contains(t.CenterID)) &&
                             (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                             (!isVIP.HasValue || t.IsVIP == isVIP) &&
                             (!isRound.HasValue || (isRound == true ? t.IsRound == true : (t.IsRound == false || t.IsRound == null))) &&
                             (telephoneNo == -1 || t.TelephoneNo.ToString().Contains(telephoneNo.ToString())) &&
                             (!cusstomerTypeID.HasValue || t.CustomerTypeID == cusstomerTypeID) &&
                             (!cusstomerGroupID.HasValue || t.CustomerGroupID == cusstomerGroupID) &&
                             (usageTypes.Count == 0 || usageTypes.Contains((int)t.UsageType)) &&
                             (isRequest == null || (isRequest == true ? context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.TelephoneNo) : !context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.TelephoneNo)))
                           )
                     .Select(t => new TelephoneInfo
                     {
                         TelephoneNo = t.TelephoneNo,
                         Center = t.Center.CenterName,
                         Customer = (t.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.Customer.LastName ?? string.Empty),
                         FirstName = t.Customer.FirstNameOrTitle,
                         LastName = t.Customer.LastName,
                         Status = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Status),
                         PreCodeType = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.SwitchPrecode.PreCodeType),
                         RoundType = DB.GetEnumDescriptionByValue(typeof(DB.RoundType), t.RoundType),
                         UsageType = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneUsageType), t.UsageType),
                         ClassTelephone = t.ClassTelephone == 0 ? string.Empty : DB.GetEnumDescriptionByValue(typeof(DB.ClassTelephone), t.ClassTelephone),
                         SwitchPrecode = t.SwitchPrecode.SwitchPreNo,
                         IsVIP = t.IsVIP,
                         IsRound = t.IsRound,
                         Address = t.Address.AddressContent,
                         ConnectDate = t.ConnectDate.ToPersian(Date.DateStringType.Short),
                         CutDate = t.CutDate.ToPersian(Date.DateStringType.Short),
                         InstallationDate = t.InstallationDate.ToPersian(Date.DateStringType.Short),
                         TelDischargeDate = t.DischargeDate.ToPersian(Date.DateStringType.Short),
                         CustomerTypeTitle = t.CustomerType.Title,
                         CustomerTypeGroupTitle = t.CustomerGroup.Title,
                     });
                count = var.Count();
                return var.Skip(startRowIndex).Take(pageSize).ToList();
            }
        }



        public static List<TelephoneInfo>
            SearchCustomerOfficeTelephone(
              List<int> cites,
       List<int> preCodeType,
       List<int> roundType,
       List<int> status,
       List<int> switchPrecode,
       List<int> center,
       bool? isVIP,
       bool? isRound,
       long telephoneNo,
       int startRowIndex,
       int pageSize,
       int? cusstomerTypeID,
       int? cusstomerGroupID,
       DateTime? installationDateFromDate,
       DateTime? installationDateToDate,
       int? priceSumComboBox,
       long priceSum,
       int? depositPriceSumComboBox,
       long depositPriceSum,
       List<int> chargingTypes,
       List<int> posessionTypes,
       List<int> usageType,
       DateTime? InitialInstallationDateFromDate,
       DateTime? InitialInstallationDateToDate,
       DateTime? FromDischargeDate,
       DateTime? ToDischargeDate,
       DateTime? FromInitialDischargeDate,
       DateTime? ToInitialDischargeDate,
       out int count,
       bool isPrint
       )
        {
            if (installationDateToDate.HasValue)
                installationDateToDate = installationDateToDate.Value.AddDays(1);

            if (InitialInstallationDateToDate.HasValue)
                InitialInstallationDateToDate = InitialInstallationDateToDate.Value.AddDays(1);


            if (ToDischargeDate.HasValue)
                ToDischargeDate = ToDischargeDate.Value.AddDays(1);

            if (ToInitialDischargeDate.HasValue)
                ToInitialDischargeDate = ToInitialDischargeDate.Value.AddDays(1);


            List<TelephoneInfo> result = new List<TelephoneInfo>();

            using (MainDataContext context = new MainDataContext())
            {

                IQueryable<TelephoneInfo> query = context.Telephones.Where(t =>
                                                                 (cites.Count == 0 || cites.Contains((int)t.Center.Region.CityID)) &&
                                                                 (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : center.Contains(t.CenterID)) &&
                                                                 (preCodeType.Count == 0 || preCodeType.Contains(t.SwitchPrecode.PreCodeType)) &&
                                                                 (roundType.Count == 0 || (t.RoundType.HasValue && roundType.Contains(t.RoundType.Value))) &&
                                                                 (status.Count == 0 || (status.Contains(-1) ? (status.Contains(t.Status) || t.Status == (int)DB.TelephoneStatus.Connecting || t.Status == (int)DB.TelephoneStatus.Cut) : status.Contains(t.Status))) &&
                                                                 (chargingTypes.Count == 0 || chargingTypes.Contains((int)t.ChargingType)) &&
                                                                 (posessionTypes.Count == 0 || posessionTypes.Contains((int)t.PosessionType)) &&
                                                                 (usageType.Count == 0 || usageType.Contains((int)t.UsageType)) &&
                                                                 (switchPrecode.Count == 0 || (t.SwitchPrecodeID.HasValue && switchPrecode.Contains((int)t.SwitchPrecodeID.Value))) &&
                                                                 (!isVIP.HasValue || t.IsVIP == isVIP) &&
                                                                 (!isRound.HasValue || (isRound == true ? t.IsRound == true : (t.IsRound == false || t.IsRound == null))) &&
                                                                 (telephoneNo == -1 || t.TelephoneNo.ToString().Contains(telephoneNo.ToString())) &&
                                                                 (!cusstomerTypeID.HasValue || t.CustomerTypeID == cusstomerTypeID) &&
                                                                 (!cusstomerGroupID.HasValue || t.CustomerGroupID == cusstomerGroupID) &&
                                                                 (!installationDateFromDate.HasValue || t.InstallationDate >= installationDateFromDate) &&
                                                                 (!installationDateToDate.HasValue || t.InstallationDate <= installationDateToDate) &&
                                                                 (!InitialInstallationDateFromDate.HasValue || t.InitialInstallationDate >= InitialInstallationDateFromDate) &&
                                                                 (!InitialInstallationDateToDate.HasValue || t.InitialInstallationDate <= InitialInstallationDateToDate) &&
                                                                 (!FromDischargeDate.HasValue || t.DischargeDate >= FromDischargeDate) &&
                                                                 (!ToDischargeDate.HasValue || t.DischargeDate <= ToDischargeDate) &&
                                                                 (!FromInitialDischargeDate.HasValue || t.InitialDischargeDate >= FromInitialDischargeDate) &&
                                                                 (!ToInitialDischargeDate.HasValue || t.InitialDischargeDate <= ToInitialDischargeDate)
                                                               ).Select(t => new TelephoneInfo
                                                                              {
                                                                                  ChargingTypeName = DB.GetEnumDescriptionByValue(typeof(DB.ChargingGroup), t.ChargingType),
                                                                                  PosessionTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PossessionType), t.PosessionType),
                                                                                  TelephoneNo = t.TelephoneNo,
                                                                                  City = t.Center.Region.City.Name,
                                                                                  Center = t.Center.CenterName,
                                                                                  Customer = (t.CustomerID != null) ? (t.Customer.FirstNameOrTitle ?? "") + " " + (t.Customer.LastName ?? "") : "",
                                                                                  FirstName = t.Customer.FirstNameOrTitle,
                                                                                  LastName = t.Customer.LastName,
                                                                                  Status = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Status),
                                                                                  PreCodeType = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.SwitchPrecode.PreCodeType),
                                                                                  RoundType = DB.GetEnumDescriptionByValue(typeof(DB.RoundType), t.RoundType),
                                                                                  ClassTelephone = DB.GetEnumDescriptionByValue(typeof(DB.ClassTelephone), t.ClassTelephone),
                                                                                  SwitchPrecodeString = (t.SwitchPrecodeID != null) ? t.SwitchPrecode.SwitchPreNo.ToString() : "",
                                                                                  IsVIP = t.IsVIP,
                                                                                  IsRound = t.IsRound,
                                                                                  Address = (t.InstallAddressID != null) ? t.Address.AddressContent : "",
                                                                                  ConnectDate = t.ConnectDate.ToPersian(Date.DateStringType.Short),
                                                                                  CutDate = t.CutDate.ToPersian(Date.DateStringType.Short),
                                                                                  InitialInstallationDate = t.InitialInstallationDate.ToPersian(Date.DateStringType.Short),
                                                                                  InstallationDate = t.InstallationDate.ToPersian(Date.DateStringType.Short),
                                                                                  TelDischargeDate = t.DischargeDate.ToPersian(Date.DateStringType.Short),
                                                                                  InitialDischargeDate = t.InitialDischargeDate.ToPersian(Date.DateStringType.Short),
                                                                                  CustomerTypeTitle = (t.CustomerTypeID != null) ? t.CustomerType.Title : "",
                                                                                  CustomerTypeGroupTitle = (t.CustomerGroupID != null) ? t.CustomerGroup.Title : "",
                                                                                  CustomerTypeID = t.CustomerTypeID,
                                                                                  CustomerGroupID = t.CustomerGroupID,
                                                                                  CauseOfCutName = t.CauseOfCutID == null ? "" : t.CauseOfCut.Name,
                                                                                  UsageType = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneUsageType), t.UsageType),
                                                                                  CauseOfTakePossession = t.CauseOfTakePossessionID != null ? t.CauseOfTakePossession.Name : "",
                                                                                  //// Price = t.Requests.Where(t2 => t2.RequestTypeID == (int)DB.RequestType.Dayri).OrderByDescending(t2 => t2.EndDate).FirstOrDefault().RequestPayments.Where(t3 => t3.BaseCost.IsDeposit == false && t3.IsPaid == true).Sum(t3 => t3.AmountSum) ?? 0,
                                                                                  //  DepositPrice = t.Requests.Where(t2 => t2.RequestTypeID == (int)DB.RequestType.Dayri).OrderByDescending(t2 => t2.EndDate).FirstOrDefault().RequestPayments.Where(t3 => t3.BaseCost.IsDeposit == true && t3.IsPaid == true).Sum(t3 => t3.AmountSum) ?? 0,
                                                                                  SpecialService = string.Join("، ", t.TelephoneSpecialServiceTypes.Select(t3 => t3.SpecialServiceType.Title).ToList()),
                                                                                  FatherName = (t.CustomerID != null) ? (t.Customer.FatherName ?? "") : "",
                                                                                  NationalID = (t.CustomerID != null) ? (t.Customer.NationalID ?? "") : "",
                                                                                  NationalCodeOrRecordNo = (t.CustomerID != null) ? (t.Customer.NationalCodeOrRecordNo ?? "") : "",
                                                                                  PostalCode = (t.InstallAddressID != null) ? t.Address.PostalCode : "",

                                                                              }
                                                                );

                if (priceSumComboBox != null && priceSum != 0)
                {
                    query = query.Where(DB.ComparisonByByPropertyName<TelephoneInfo>("Price", priceSum, (int)priceSumComboBox)).AsQueryable();
                }

                if (depositPriceSumComboBox != null && depositPriceSum != 0)
                {
                    query = query.Where(DB.ComparisonByByPropertyName<TelephoneInfo>("DepositPrice", depositPriceSum, (int)depositPriceSumComboBox)).AsQueryable();
                }


                if (isPrint)
                {
                    result = query.ToList();
                    count = result.Count();
                }
                else
                {
                    count = query.Count();
                    result = query.Skip(startRowIndex).Take(pageSize).ToList();
                }


                return result;
            }
        }

        public static int SearchCustomerOfficeTelephoneCount(
        List<int> preCodeType,
        List<int> roundType,
        List<int> status,
        List<int> switchPrecode,
        List<int> center,
        bool? isVIP,
        bool? isRound,
        long telephoneNo,
        int? cusstomerTypeID,
        int? cusstomerGroupID,
        DateTime? installationDateFromDate,
        DateTime? installationDateToDate
        )
        {
            using (MainDataContext context = new MainDataContext())
            {

                string query = string.Format(@"
                                 select count(*)
                                        from Telephone as T left join
                                            ( 
                                               select * from 
                                        	   (
                                               select ROW_NUMBER() OVER(PARTITION BY R.TelephoneNo ORDER BY IR.InstallationDate DESC) AS Row , IR.* , R.TelephoneNo , R.RequestTypeID from InstallRequest as IR join Request as R on IR.RequestID = R.ID
                                               ) as T2   where T2.Row = 1
                                             ) 
                                        as T1   on T.TelephoneNo = T1.TelephoneNo
                                 join Center on T.CenterID = Center.ID
                                 left join Customer on Customer.ID = T.CustomerID
                                 left join Address on Address.ID = t.InstallAddressID
                                 left join CustomerType on T.CustomerTypeID = CustomerType.ID
                                 left join CustomerGroup on T.CustomerGroupID = CustomerGroup.ID
                                 left join CauseOfCut on T.CauseOfCutID = CauseOfCut.ID
                                 join SwitchPrecode on T.SwitchPrecodeID = SwitchPrecode.ID where 1=1");


                if (preCodeType.Count != 0)
                    query += string.Format(" AND T.PreCodeType in ({0})", string.Join(",", preCodeType));

                if (roundType.Count != 0)
                    query += string.Format(" AND T.RoundType in ({0})", string.Join(",", roundType));

                if (status.Count != 0)
                    query += string.Format(" AND T.Status in ({0})", string.Join(",", status));

                if (switchPrecode.Count != 0)
                    query += string.Format(" AND T.SwitchPrecodeID in ({0})", string.Join(",", switchPrecode));

                if (center.Count != 0)
                    query += string.Format(" AND T.CenterID in ({0})", string.Join(",", center));

                if (isVIP.HasValue)
                    query += string.Format(" AND T.IsVIP = {0}", isVIP);

                if (isRound.HasValue)
                    query += string.Format(" AND T.IsRound = {0}", isRound == true ? 1 : 0);

                if (telephoneNo != -1)
                    query += string.Format(" AND T.TelephoneNo = {0}", telephoneNo);

                if (cusstomerTypeID.HasValue)
                    query += string.Format(" AND CustomerType.ID = {0}", cusstomerTypeID);

                if (cusstomerGroupID.HasValue)
                    query += string.Format(" AND CustomerGroup.ID = {0}", cusstomerGroupID);

                if (installationDateFromDate.HasValue)
                    query += string.Format(" AND T.InstallationDate  >=  CONVERT(datetime, '{0}', 101)", installationDateFromDate.Value.ToShortDateString());

                if (installationDateToDate.HasValue)
                    query += string.Format(" AND T.InstallationDate  <= CONVERT(datetime, '{0}', 101)", installationDateToDate.Value.ToShortDateString());


                return context.ExecuteQuery<int>(query).SingleOrDefault();

                //return context.Telephones
                //    .Where(t =>
                //            (preCodeType.Count == 0 || preCodeType.Contains((int)t.PreCodeType)) &&
                //            (roundType.Count == 0 || roundType.Contains((int)t.RoundType)) &&
                //            (status.Count == 0 || status.Contains(t.Status)) &&
                //            (switchPrecode.Count == 0 || switchPrecode.Contains((int)t.SwitchPrecodeID)) &&
                //            (center.Count == 0 || center.Contains(t.CenterID)) &&
                //            (!isVIP.HasValue || t.IsVIP == isVIP) &&
                //            (!isRound.HasValue || t.IsRound == isRound) &&
                //            (!cusstomerTypeID.HasValue || context.InstallRequests.Where(t2 => t2.Request.TelephoneNo == t.TelephoneNo).OrderByDescending(t2 => t2.InstallationDate).FirstOrDefault().TelephoneType == cusstomerTypeID) &&
                //            (!cusstomerGroupID.HasValue || context.InstallRequests.Where(t2 => t2.Request.TelephoneNo == t.TelephoneNo).OrderByDescending(t2 => t2.InstallationDate).FirstOrDefault().TelephoneTypeGroup == cusstomerGroupID) &&
                //            (telephoneNo == -1 || t.TelephoneNo.ToString().Contains(telephoneNo.ToString())) 
                //            //&&
                //           // (isRequest == null || (isRequest == true ? context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.TelephoneNo) : !context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.TelephoneNo)))
                //            ).Count();
            }
        }

        public static List<Telephone> FindTelephone(long telNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                    .Where(t => (telNo == 0 || telNo == t.TelephoneNo))
                    .ToList();
            }
        }

        public static Telephone GetTelephoneByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Telephone telephone = context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();

                if (telephone != null)
                    return telephone;
                else
                    return null;
            }
        }

        public static long GetSwitchPreCodeNumberTelephoneByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                    .Where(t => t.TelephoneNo == telephoneNo).Select(t => t.SwitchPrecode.SwitchPreNo)
                    .SingleOrDefault();
            }
        }

        public static List<Telephone> GetTelephoneBySwitchPreCodeNo(SwitchPrecode switchItem, int SwitchPrecodeID, byte precodeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                // var item = context.SwitchPrecodes.Select(s => s.SwitchID == switchItem.SwitchID && s.SwitchPreNo == preNo);
                return context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchItem.SwitchID && t.SwitchPrecode.ID == SwitchPrecodeID && t.SwitchPrecode.PreCodeType == precodeType).ToList();
                //.Where(t => t.TelephoneNo >= switchItem.StartNo && t.TelephoneNo <= switchItem.EndNo && switchItem.PreCodeType == t.SwitchPort.Switch.PreCodeType).ToList();

            }
        }

        public static int GetTelephoneBySwitchPreCodeNoCount(SwitchPrecode switchItem, int SwitchPrecodeID, byte precodeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                // var item = context.SwitchPrecodes.Select(s => s.SwitchID == switchItem.SwitchID && s.SwitchPreNo == preNo);
                return context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchItem.SwitchID && t.SwitchPrecode.ID == SwitchPrecodeID && t.SwitchPrecode.PreCodeType == precodeType).Count();
                //.Where(t => t.TelephoneNo >= switchItem.StartNo && t.TelephoneNo <= switchItem.EndNo && switchItem.PreCodeType == t.SwitchPort.Switch.PreCodeType).ToList();

            }
        }

        public static List<CheckableItem> GetTelephoneCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                    .Select(t => new CheckableItem
                    {
                        LongID = t.TelephoneNo,
                        Name = t.TelephoneNo.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
        //public static Telephone GetTelInfoByRequestID(long requestID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Contracts.Where(t => t.RequestID == requestID).Select(t => new TelRoundInfo
        //        {
        //            switchID = t.RoundSaleInfo.Telephone.SwitchPort.SwitchID,
        //            TelephoneNo = t.RoundSaleInfo.TelephoneNo,
        //            portNo = t.RoundSaleInfo.Telephone.SwitchPort.PortNo,
        //            portType = t.RoundSaleInfo.Telephone.SwitchPort.Type,
        //            switchPreNo = t.RoundSaleInfo.Telephone.SwitchPort.Switch.SwitchPreNo,
        //            switchPreCodeType = t.RoundSaleInfo.Telephone.SwitchPort.Switch.PreCodeType

        //        }
        //                                                                             ).SingleOrDefault();
        //    }
        //}

        public class TelInfo
        {
            public long TelephoneNo { get; set; }
            public long? switchPreNo { get; set; }
            public long? switchPreCodeID { get; set; }
            public string portNo { get; set; }
            public byte? switchPreCodeType { get; set; }
            public int? switchID { get; set; }
            public bool? portType { get; set; }
        }

        public static List<CheckableItem> GetTelephoneBySwitchID(int SwitchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPrecode.SwitchID == SwitchID).Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.TelephoneNo.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<Telephone> GetTelephoneListBySwitchID(int SwitchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPrecode.SwitchID == SwitchID).ToList();
            }
        }

        public static List<Telephone> GetTelephoneFromTelToTel(long FromTel, long ToTel)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo >= FromTel && t.TelephoneNo <= ToTel).ToList();
            }
        }

        public static List<TeleInfo> GetTelephoneInfoByTelePhonNo(List<Telephone> teleList)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => teleList.Select(tl => tl.TelephoneNo).Contains(t.TelephoneNo)).AsEnumerable()
                                         .Select(t => new TeleInfo
                                                      {
                                                          TelephoneNo = t.TelephoneNo,
                                                          CustomerAddressID = t.Address != null ? t.Address.ID : 0,
                                                          PostalCode = t.Address != null ? t.Address.PostalCode : "",
                                                          //TODO:rad 13950128
                                                          Address = t.InstallAddressID != null ? t.Address.AddressContent : "",
                                                          //end
                                                          TelephoneNoStatus = t.Status,
                                                          RoundType = DB.GetEnumDescriptionByValue(typeof(DB.RoundType), t.RoundType),
                                                          ClassTelephone = t.ClassTelephone == 0 ? string.Empty : DB.GetEnumDescriptionByValue(typeof(DB.ClassTelephone), t.ClassTelephone),
                                                          IsVIP = t.IsVIP,
                                                          IsRound = t.IsRound,
                                                          ConnectDate = t.ConnectDate,
                                                          InitialInstallationDate = t.InitialInstallationDate,
                                                          InitialDischargeDate = t.InitialDischargeDate,
                                                          CauseOfTakePossession = t.CauseOfTakePossessionID != null ? t.CauseOfTakePossession.Name : "",
                                                          CutDate = t.CutDate,
                                                          InstallationDate = t.InstallationDate,
                                                          TelDischargeDate = t.DischargeDate,
                                                          Status = t.Status,
                                                          TelephoneNoStatusName = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Status),
                                                          CauseOfCut = t.CauseOfCutID != null ? t.CauseOfCut.Name : string.Empty,
                                                          UsageType = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneUsageType), t.UsageType),
                                                          OldCustomer = (t.Status == (int)DB.TelephoneStatus.Discharge) ? (context.Requests.Where(x => x.TelephoneNo == t.TelephoneNo && x.RequestTypeID == (int)DB.RequestType.Dischargin).OrderByDescending(x => x.EndDate).Take(1).Select(x => (x.Customer.FirstNameOrTitle ?? string.Empty) + " " + (x.Customer.LastName ?? string.Empty)).SingleOrDefault()) : (string.Empty),
                                                          SpecialService = string.Join("، ", context.TelephoneSpecialServiceTypes.Where(t2 => t2.TelephoneNo == t.TelephoneNo).Select(t3 => t3.SpecialServiceType.Title).ToList())
                                                      }
                                                ).ToList();
            }
        }

        public static string GetCauseOfCutNameByTelephone(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                CutAndEstablish CutAndEstablishes = context.CutAndEstablishes.Where(t => t.Request.TelephoneNo == telephoneNo).OrderByDescending(t2 => t2.Request.InsertDate).FirstOrDefault();
                if (CutAndEstablishes != null)
                {
                    CauseOfCut CauseOfCut = CutAndEstablishes.CauseOfCut;
                    if (CauseOfCut != null)
                        return CauseOfCut.Name;
                    else
                        return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }



        public static TeleInfo GetTelephoneInfoByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo).Select(t => new TeleInfo { TelephoneNo = t.TelephoneNo, CustomerAddressID = t.Address.ID, Address = t.Address.AddressContent, PostalCode = t.Address.PostalCode }).SingleOrDefault();
            }
        }

        public static TelephoneSummenryInfo GetTelephoneSummneryInfoByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo)
                                         .Select(t => new TelephoneSummenryInfo
                                         {
                                             TelephoneNo = t.TelephoneNo,
                                             NationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                             CustomerName = t.Customer.FirstNameOrTitle + " " + ((t.Customer.LastName != null) ? t.Customer.LastName : ""),
                                             Address = t.Address.AddressContent,
                                             PostalCode = t.Address.PostalCode,
                                             Center = t.Center.Region.City.Name + " : " + t.Center.CenterName
                                         }).SingleOrDefault();
            }
        }

        public static TeleInfo GetTelephoneInfoByCustumerAddress(long customerAddressID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.InstallAddressID == customerAddressID).Select(t => new TeleInfo { TelephoneNo = t.TelephoneNo, CustomerAddressID = t.Address.ID, Address = t.Address.AddressContent, PostalCode = t.Address.PostalCode }).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetCheckableItemTelephoneBySwitchPreCodeID(int SwitchPreCodeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPrecodeID == SwitchPreCodeID).Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.TelephoneNo.ToString(), IsChecked = false }).ToList();
            }
        }
        public static List<CheckableItem> GetCheckableItemFreeTelephoneBySwitchPreCodeID(int SwitchPreCodeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPrecodeID == SwitchPreCodeID && t.Status == (byte)DB.TelephoneStatus.Free).OrderBy(t => t.TelephoneNo).Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.TelephoneNo.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetE1CheckableItemTelephoneBySwitchPreCodeID(int SwitchPreCodeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPrecodeID == SwitchPreCodeID && t.UsageType == (byte)DB.TelephoneUsageType.E1 && (t.Status == (int)DB.TelephoneStatus.Free || t.Status == (int)DB.TelephoneStatus.Discharge))
                    .OrderBy(t => t.TelephoneNo)
                    .Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.TelephoneNo.ToString(), IsChecked = false })
                    .ToList();
            }
        }

        public static List<Telephone> GetTelephoneBySwitchPreCodeID(int SwitchPreCodeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPrecodeID == SwitchPreCodeID).ToList();
            }
        }

        public static void SaveChangeNo(Request request, ChangeNo ChangeNo, Telephone telephoneItem)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (request != null)
                    {
                        request.Detach();
                        DB.Save(request);
                    }

                    if (ChangeNo != null)
                    {
                        ChangeNo.Detach();
                        DB.Save(ChangeNo);
                    }
                    if (!(telephoneItem == null || telephoneItem.TelephoneNo == 0))
                    {
                        telephoneItem.Detach();
                        DB.Save(telephoneItem);
                    }

                    ts.Complete();
                }
            }
        }

        public static List<CheckableObject> GetRoundTelephone(byte roundType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.RoundType == roundType).Select(t => new CheckableObject { Object = t, IsChecked = false }).ToList();
            }
        }
        public static List<CheckableObject> GetRoundTelephone(byte roundType, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.RoundType == roundType &&
                                                     t.CenterID == centerID &&
                                                     (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge))
                                         .Select(t => new CheckableObject
                                                     {
                                                         Object = t,
                                                         IsChecked = false
                                                     }
                                                 ).ToList();
            }
        }
        public static string GetTelephoneNoBySwitchPortId(int SwitchPortID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPortID == SwitchPortID).SingleOrDefault().TelephoneNo.ToString();
            }
        }
        public static Telephone GetTelephoneNoBySwitchPortID(int SwitchPortID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPortID == SwitchPortID).SingleOrDefault();
            }
        }
        public static List<Telephone> GetTelephonesNoBySwitchPortID(int SwitchPortID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.SwitchPortID == SwitchPortID).ToList();
            }
        }

        public static Telephone GetTelephoneByTelePhoneNo(long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephone).SingleOrDefault();
            }
        }

        public static List<EmptyTelephoneReport> GetEmptyTelephoneStatistic(List<int> cityIDs, List<int> centerIDs, List<int> preCodeIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //                string query = @"select
                //                        
                //                    TelephoneNo ,  NULL DisCharge , Null Redayeri, NULL Dayeri , cast(0 AS nvarchar(50)) LastCounter, Center.CenterName ,Center.ID as CenterId
                //							FROM Telephone
                //							JOIN Center on Center.id = Telephone.CenterID
                //							where Status = 0"

                //                    ;


                string query = @"  SELECT        Telephone.TelephoneNo, NULL AS DisCharge, NULL AS Redayeri, NULL AS Dayeri, CAST(0 AS nvarchar(50)) AS LastCounter, Center.CenterName, 
                         Center.ID AS CenterId, City.ID, SwitchPrecode.ID AS Expr1
                        FROM            Telephone INNER JOIN
                         Center ON Center.ID = Telephone.CenterID INNER JOIN
                         Region ON Center.RegionID = Region.ID INNER JOIN
                         City ON Region.CityID = City.ID INNER JOIN
                         SwitchPrecode ON Telephone.SwitchPrecodeID = SwitchPrecode.ID
                         WHERE        (Telephone.Status = 0)";

                if (centerIDs.Count > 0)
                {
                    string CenterList = DB.MakeTheList(centerIDs);
                    query += " and Telephone.CenterID in " + CenterList;
                }

                if (cityIDs.Count > 0)
                {
                    string cites = DB.MakeTheList(cityIDs);
                    query += " and City.ID in " + cites;
                }

                if (preCodeIDs.Count > 0)
                {
                    string preCodes = DB.MakeTheList(preCodeIDs);
                    query += " and SwitchPrecode.ID in " + preCodes;
                }

                return context.ExecuteQuery<EmptyTelephoneReport>(string.Format(query)).ToList();

            }
        }

        public static List<EmptyTelephoneReport> GetDisChargeTelephoneStatistic(List<int> cityIDs, List<int> centerIDs, List<int> preCodeIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string query =
                        @"  SELECT Res.TelephoneNo , Res.RequestDisCharge as DisCharge , res.RequestReDayeri as Redayeri, Res.RequestDayeri as Dayeri  ,  c1.CounterNo as LastCounter, Center.CenterName ,Center.ID as CenterId
                            FROM   ( 
                            SELECT Telephone.TelephoneNo , MAX(r1.EndDate) as RequestDisCharge, MAX(r2.EndDate) as RequestReDayeri,MAX(r.EndDate) as RequestDayeri, Telephone.CenterID  ,   Telephone.SwitchPrecodeID
                            from Telephone
                            Left JOIN Request r on r.TelephoneNo = Telephone.TelephoneNo AND r.RequestTypeID = 1
                            Left JOIN Request r2 on r2.TelephoneNo = Telephone.TelephoneNo AND r2.RequestTypeID = 53
                            Left JOIN Request r1 on r1.TelephoneNo = Telephone.TelephoneNo AND r1.RequestTypeID = 32
                            WHERE Telephone.Status = 5
                            group BY Telephone.TelephoneNo, Telephone.CenterID ,   Telephone.SwitchPrecodeID) Res 
                            Left JOIN ( SELECT  TelephoneNo,MAX(c.InsertDate) as InsertDate from [Counter] c
                            GROUP BY TelephoneNo,CycleID ) T ON T.TelephoneNo =Res.TelephoneNo
                            Left JOIN [Counter] c1 on c1.TelephoneNo = Res.TelephoneNo AND c1.InsertDate = T.InsertDate
                            JOIN Center on Center.id = Res.CenterID
                            JOIN  Region ON Center.RegionID = Region.ID 
                            INNER JOIN City ON Region.CityID = City.ID 
                            INNER JOIN SwitchPrecode ON Res.SwitchPrecodeID = SwitchPrecode.ID
                            where 1 = 1";
                if (centerIDs.Count > 0)
                {
                    string CenterList = DB.MakeTheList(centerIDs);
                    query += " and Res.CenterID in " + CenterList;
                }

                if (cityIDs.Count > 0)
                {
                    string cites = DB.MakeTheList(cityIDs);
                    query += " and City.ID in " + cites;
                }

                if (preCodeIDs.Count > 0)
                {
                    string preCodes = DB.MakeTheList(preCodeIDs);
                    query += " and Res.SwitchPrecodeID in " + preCodes;
                }

                return context.ExecuteQuery<EmptyTelephoneReport>(string.Format(query)).ToList();

            }
        }

        public static List<TelephoneWithOutPCMReport> GetTelephoneWithOutPCM(List<int> CenterIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //return context.Buchts
                //    .GroupJoin(context.SwitchPorts, b => b.SwitchPortID, sp => sp.ID, (b, sp) => new { bucht = b, switchPort = sp })
                //    .SelectMany(t1 => t1.switchPort.DefaultIfEmpty(), (sp, t1) => new { SwitchPort = t1, Bucht = sp.bucht })

                //    .GroupJoin(context.Telephones, sp => sp.SwitchPort.ID, t => t.SwitchPortID, (sp, t) => new { telephone = t, switchPort = sp.SwitchPort, bucht = sp.Bucht })
                //    .SelectMany(t2 => t2.telephone.DefaultIfEmpty(), (t, t2) => new { Telephone = t2, SwitchPort = t.switchPort, Bucht = t.bucht })

                //    .Where(t => (CenterIDs.Count == 0 || CenterIDs.Contains(t.Telephone.CenterID))
                //            && (t.Bucht.Status == (byte)DB.BuchtStatus.Connection && t.Bucht.PCMPortID == null))

                //    .Select(t => new TelephoneWithOutPCMReport
                //    {
                //        BuchtID = "ام دی اف:" + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + //DB.GetDescription(t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description) +
                //                                                            "ردیف:" + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() +
                //                                                            "طبقه:" + t.Bucht.VerticalMDFRow.VerticalRowNo.ToString() +
                //                                                            "اتصالی:" + t.Bucht.BuchtNo.ToString(),
                //        Cabinet = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                //        CabinetInput = t.Bucht.CabinetInput.InputNumber,
                //        CenterName = t.Telephone.Center.CenterName,
                //        CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                //        CustomerID = t.Telephone.Customer.ID.ToString(),
                //        Post = t.Bucht.PostContact.Post.Number,
                //        PostContact = t.Bucht.PostContact.ConnectionNo,
                //        TelephoneNo = t.Telephone.TelephoneNo,

                //    }).ToList();
                string query = @"SELECT
                                    Cabinet.CabinetNumber as Cabinet,CabinetInput.InputNumber,Center.CenterName,(Customer.FirstNameOrTitle + ' ' + Customer.LastName ) as CustomerName
                                    , CAST (Customer.ID AS nvarchar(15)) as CustomerID,Post.Number as Post,PostContact.ConnectionNo,Telephone.TelephoneNo,
                                      (cast(VerticalMDFColumn.VerticalCloumnNo AS nvarchar(5)) +'-'+cast (VerticalMDFRow.VerticalRowNo as nvarchar(10)) + '-' + CAST(Bucht.BuchtNo AS  nvarchar(5)) ) as BuchtID
                                    FROM Bucht 
                                    left JOIN Postcontact on Postcontact.ID = Bucht.ConnectionID
                                    LEFT JOIN Post on Post.ID = PostContact.PostID
                                    left JOIN CabinetInput  on Bucht.CabinetInputID = CabinetInput.ID
                                    LEFT JOIN Cabinet on Cabinet.ID = CabinetInput.CabinetID
                                    left JOIN SwitchPort on SwitchPort.ID = Bucht.SwitchPortID
                                    LEFT JOIN Telephone on Telephone.SwitchPortID = SwitchPort.ID
                                    LEFT JOIN Center on Center.ID = Cabinet.CenterID 
                                    LEFT JOIN VerticalMDFRow on VerticalMDFRow.ID = Bucht.MDFRowID
                                    LEFT JOIN VerticalMDFColumn on VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID
                                    LEFT JOIN MDFFrame on MDFFrame.ID = VerticalMDFColumn.MDFFrameID
                                    LEFT join mdf on MDF.ID = MDFFrame.MDFID
                                    LEFT JOIN Customer on telephone.CustomerID = Customer.ID
                                    WHERE Bucht.[Status] = 1 and Bucht.PCMPortID IS NULL ";
                if (CenterIDs.Count > 0)
                    query += " AND Cabinet.Centerid in " + DB.MakeTheList(CenterIDs);
                return context.ExecuteQuery<TelephoneWithOutPCMReport>(string.Format(query)).ToList();


            }
        }

        public static List<Telephone> GetFreeTelephoneBySwitchPreCodeNo(SwitchPrecode switchPrecodeItem, int precodeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID
                         && t.SwitchPrecodeID == switchPrecodeItem.ID
                         && t.SwitchPrecode.PreCodeType == precodeType
                         && (t.UsageType == null || t.UsageType == 0)
                         && (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge)
                         && t.InRoundSale == false
                         ).OrderBy(t => t.TelephoneNo).ToList();

            }
        }
        public static List<Telephone> GetOpticalCabinetFreeTelephoneBySwitchPreCodeNo(SwitchPrecode switchPrecodeItem, int precodeType, int? switchID, bool checkRound = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<Telephone> query = context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID
                         && t.SwitchPrecodeID == switchPrecodeItem.ID
                         && t.SwitchPrecode.PreCodeType == precodeType
                         && (t.UsageType == null || t.UsageType == 0)
                         && (checkRound == true || (t.IsRound == null || t.IsRound == false))
                         && (t.SwitchPrecode.SwitchID == switchID)
                         && (t.Status == (byte)DB.TelephoneStatus.Free || (t.Status == (byte)DB.TelephoneStatus.Discharge && t.DischargeDate <= DB.GetServerDate().AddMonths(-2)))
                         && t.InRoundSale == false
                         ).OrderBy(t => t.TelephoneNo);
                return query.ToList();

            }
        }
        public static List<Telephone> GetFreeTelephoneBySwitchPreCodeNoWithoutOptiacalBucht(SwitchPrecode switchPrecodeItem, int precodeType, int usageType, bool checkRound = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<Telephone> query = context.Telephones
                                                     .Where(t =>
                                                                t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID &&
                                                                t.SwitchPrecodeID == switchPrecodeItem.ID &&
                                                                t.SwitchPrecode.PreCodeType == precodeType &&
                                                                (t.UsageType == null || t.UsageType == usageType) &&
                                                                (checkRound == true || (t.IsRound == null || t.IsRound == false)) &&
                                                                t.InRoundSale == false &&
                                                                (t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.FixedSwitch || t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.ONUABWire) &&
                                                                (t.Status == (byte)DB.TelephoneStatus.Free || (t.Status == (byte)DB.TelephoneStatus.Discharge && t.DischargeDate <= DB.GetServerDate().AddMonths(-2)))
                                                            )
                                                    .OrderBy(t => t.TelephoneNo);
                return query.ToList();

            }
        }
        public static List<Telephone> GetFreeTelephoneBySwitchPreCodeNo(SwitchPrecode switchPrecodeItem, int precodeType, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID
                                 && t.SwitchPrecodeID == switchPrecodeItem.ID
                                 && t.SwitchPrecode.PreCodeType == precodeType
                                 && t.Status == (byte)DB.TelephoneStatus.Free)
                                 .OrderBy(t => t.TelephoneNo).Skip(startRowIndex).Take(pageSize).ToList();

            }
        }

        public static List<TelephoneInfo> GetFreeTelephoneInfoBySwitchPreCodeNo(SwitchPrecode switchPrecodeItem, int precodeType, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID
                                 && t.SwitchPrecodeID == switchPrecodeItem.ID
                                 && t.SwitchPrecode.PreCodeType == precodeType
                                 && (t.UsageType == null || t.UsageType == 0)
                                 && (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge))
                                 .OrderBy(t => t.TelephoneNo).Skip(startRowIndex).Take(pageSize)
                                 .Select(t => new TelephoneInfo
                                               {
                                                   TelephoneNo = t.TelephoneNo,
                                                   IsRound = t.IsRound,
                                                   IsVIP = t.IsVIP,
                                                   Status = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Status),
                                                   Customer = t.TakePossessions.OrderByDescending(x => x.ID).FirstOrDefault().Customer.FirstNameOrTitle + " " + t.TakePossessions.OrderByDescending(x => x.ID).FirstOrDefault().Customer.LastName,
                                                   DischargeDate = t.TakePossessions.OrderByDescending(x => x.ID).FirstOrDefault().TakePossessionDate.ToPersian(Date.DateStringType.Short)


                                               })

                                 .ToList();

            }
        }

        public static List<TelephoneInfo> GetOpticalCabinetFreeTelephoneInfoBySwitchPreCodeNo(SwitchPrecode switchPrecodeItem, int precodeType, int? switchID, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<TelephoneInfo> query = context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID
                                 && t.SwitchPrecodeID == switchPrecodeItem.ID
                                 && t.SwitchPrecode.PreCodeType == precodeType
                                 && (t.UsageType == null || t.UsageType == 0)
                                 && (t.IsRound == null || t.IsRound == false)
                                 && (t.SwitchPrecode.SwitchID == switchID)
                                 && (t.Status == (byte)DB.TelephoneStatus.Free || (t.Status == (byte)DB.TelephoneStatus.Discharge && t.DischargeDate <= DB.GetServerDate().AddMonths(-2)))
                                 )
                                 .Select(t => new TelephoneInfo
                                 {
                                     TelephoneNo = t.TelephoneNo,
                                     IsRound = t.IsRound,
                                     IsVIP = t.IsVIP,
                                     Status = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Status),
                                     Customer = (t.Status == (int)DB.TelephoneStatus.Discharge) ? (context.Requests.Where(x => x.TelephoneNo == t.TelephoneNo && x.RequestTypeID == (int)DB.RequestType.Dischargin).OrderByDescending(x => x.EndDate).Take(1).Select(x => (x.Customer.FirstNameOrTitle ?? string.Empty) + " " + (x.Customer.LastName ?? string.Empty)).SingleOrDefault()) : (string.Empty),
                                     DischargeDate = t.DischargeDate.ToPersian(Date.DateStringType.Short),
                                 }).OrderBy(t => t.TelephoneNo);

                count = query.Count();
                return query.Skip(startRowIndex).Take(pageSize).ToList();

            }
        }
        public static List<TelephoneInfo> GetFreeTelephoneInfoBySwitchPreCodeNoWithOutOpticalBucht(SwitchPrecode switchPrecodeItem, int precodeType, int usageType, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<TelephoneInfo> query = context.Telephones
                                                         .Where(t =>
                                                                    t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID &&
                                                                    t.SwitchPrecodeID == switchPrecodeItem.ID &&
                                                                    t.SwitchPrecode.PreCodeType == precodeType &&
                                                                    (t.UsageType == null || t.UsageType == usageType) &&
                                                                    (t.IsRound == null || t.IsRound == false) &&
                                                                    (t.Status == (byte)DB.TelephoneStatus.Free || (t.Status == (byte)DB.TelephoneStatus.Discharge && t.DischargeDate <= DB.GetServerDate().AddMonths(-2))) &&
                                                                    (t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.FixedSwitch || t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.ONUABWire)
                                                               )
                                                        .Select(t => new TelephoneInfo
                                                                    {
                                                                        TelephoneNo = t.TelephoneNo,
                                                                        IsRound = t.IsRound,
                                                                        IsVIP = t.IsVIP,
                                                                        Status = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Status),
                                                                        Customer = context.Requests
                                                                                          .Where(x => x.TelephoneNo == t.TelephoneNo)
                                                                                          .OrderByDescending(x => x.EndDate)
                                                                                          .Take(1)
                                                                                          .Select(x => x.Customer.FirstNameOrTitle + " " + x.Customer.LastName)
                                                                                          .SingleOrDefault(),
                                                                        DischargeDate = t.DischargeDate.ToPersian(Date.DateStringType.Short)
                                                                    }
                                                               )
                                                        .OrderBy(t => t.TelephoneNo);

                count = query.Count();
                return query.Skip(startRowIndex).Take(pageSize).ToList();

            }
        }

        public static int GetFreeTelephoneBySwitchPreCodeNoCount(SwitchPrecode switchPrecodeItem, int precodeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID &&
                         t.SwitchPrecodeID == switchPrecodeItem.ID &&
                         t.SwitchPrecode.PreCodeType == precodeType &&
                         (t.UsageType == null || t.UsageType == 0) &&
                         (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge)
                         ).Count();

            }
        }
        public static int GetFreeTelephoneBySwitchPreCodeNoWithoutOpticalBuchtCount(SwitchPrecode switchPrecodeItem, int precodeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                     .Where(t => t.SwitchPrecode.SwitchID == switchPrecodeItem.SwitchID &&
                         t.SwitchPrecodeID == switchPrecodeItem.ID &&
                         t.SwitchPrecode.PreCodeType == precodeType &&
                         (t.UsageType == null || t.UsageType == 0) &&
                         (t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.FixedSwitch || t.SwitchPrecode.Switch.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.ONUABWire) &&
                         (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge)
                         ).Count();

            }
        }

        public static List<Telephone> GetTelephoneByCustomerID(long customerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.CustomerID == customerID).ToList();
            }
        }

        public static List<CheckableItem> GetFreeE1TelephoneBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                      .Where(t => (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge) && t.SwitchPrecode.SwitchID == switchID && t.UsageType == (byte)DB.TelephoneUsageType.E1)
                      .Select(t => new CheckableItem { Name = t.TelephoneNo.ToString(), LongID = t.TelephoneNo, IsChecked = false }).ToList();
            }
        }

        public static CheckableItem GetCheckableItemTelephoneByTelephone(long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                      .Where(t => t.TelephoneNo == telephone)
                      .Select(t => new CheckableItem { Name = t.TelephoneNo.ToString(), LongID = t.TelephoneNo, IsChecked = false }).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetPrivateWireCheckableItemTelephoneBy(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                              .Where(t =>
                                        (t.Status == (byte)DB.TelephoneStatus.Free || t.Status == (byte)DB.TelephoneStatus.Discharge) &&
                                        t.CenterID == centerID &&
                                        t.UsageType == (byte)DB.TelephoneUsageType.PrivateWire
                                    )
                              .OrderBy(t => t.TelephoneNo)
                              .Select(t => new CheckableItem
                                            {
                                                Name = t.TelephoneNo.ToString(),
                                                LongID = t.TelephoneNo,
                                                IsChecked = false
                                            }
                                     )
                              .ToList();

            }
        }

        internal static bool CheckTelephoneOnOpticalCabinet(long telephoneNo, out int? cabinetID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                cabinetID = null;
                var telephone = context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();
                if (telephone != null)
                {
                    cabinetID = Data.CabinetDB.GetCabinetBySwitchID(telephone.SwitchPrecode.SwitchID);
                    if (!(cabinetID == null || cabinetID == 0))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static int GetLastCauseOfCut(long telephone, out string causeOfCutName, out bool requestAbility)
        {
            causeOfCutName = string.Empty;
            requestAbility = false;
            int result = -1;
            using (MainDataContext context = new MainDataContext())
            {

                Telephone telephoneItem = context.Telephones.Where(t => t.TelephoneNo == telephone).SingleOrDefault();
                // CutAndEstablish cutAndEstablishes = context.CutAndEstablishes.Where(t => t.Request.TelephoneNo == telephone && t.Request.EndDate != null).OrderByDescending(t => t.Request.EndDate).FirstOrDefault();

                CauseOfCut causeOfCut;
                if (telephoneItem != null)
                {
                    causeOfCut = telephoneItem.CauseOfCut;

                    if (causeOfCut != null)
                    {
                        causeOfCutName = causeOfCut.Name;
                        requestAbility = causeOfCut.RequestAbility;
                        result = causeOfCut.ID;
                    }
                }

                else
                {
                    causeOfCut = null;

                    result = -2;

                }

            }

            return result;
        }

        public static List<Telephone> GetTelephones(List<long> telephones)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => telephones.Contains(t.TelephoneNo)).ToList();
            }
        }

        public static string GetCenterNameByTelephoneNo(long TelNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == TelNo).Select(t => t.Center.CenterName).SingleOrDefault();
            }
        }

        public static string GetAddressContentByTelephoneNo(long TelNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == TelNo).Select(t => t.Address.AddressContent).SingleOrDefault();
            }
        }

        public static string GetPostalCodeByTelephoneNo(long TelNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == TelNo).Select(t => t.Address.PostalCode).SingleOrDefault();
            }
        }

        public static bool CheckTelephonNoInOpticalCabinet(long? telephone, Cabinet cabinet)
        {
            if (telephone != null)
            {

                using (MainDataContext context = new MainDataContext())
                {
                    if (cabinet.SwitchID == null || context.Telephones.Where(t => t.TelephoneNo == telephone).SingleOrDefault().SwitchPrecode.SwitchID == cabinet.SwitchID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public static int GetCenterIDbyTelephoneNoTemp(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                TelephoneTemp temp = context.TelephoneTemps.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();

                if (temp != null)
                    return (int)temp.CenterID;
                else
                    return 0;
            }
        }

        public static TelephoneInfoForRequest GetTelephoneInfoForFailure(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo)
                    .Select(t => new TelephoneInfoForRequest
                    {
                        CenterID = t.CenterID,
                        Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        PostalCode = t.Address.PostalCode,
                        Address = t.Address.AddressContent,
                        FirstName = t.Customer.FirstNameOrTitle,
                        LastName = (t.Customer.LastName != null) ? t.Customer.LastName : "",
                        CustomerName = t.Customer.FirstNameOrTitle + " " + ((t.Customer.LastName != null) ? t.Customer.LastName : ""),
                        FatherName = t.Customer.FatherName,
                        NationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                        Mobile = t.Customer.MobileNo,
                        Email = t.Customer.Email
                    }).SingleOrDefault();
            }
        }

        public static bool HasTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Telephone telephone = context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();

                if (telephone != null)
                    return true;
                else
                    return false;
            }
        }

        public static Telephone GetTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Telephone telephone = context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();

                if (telephone != null)
                    return telephone;
                else
                    return null;
            }
        }

        public static bool HasTelephoneTemp(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                TelephoneTemp telephone = context.TelephoneTemps.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();

                if (telephone != null)
                    return true;
                else
                    return false;
            }
        }

        public static TelephoneTemp GetTelephoneTemp(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                TelephoneTemp telephone = context.TelephoneTemps.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();

                if (telephone != null)
                    return telephone;
                else
                    return null;
            }
        }

        public static List<Telephone> GetTelephoneByNationalCode(string nationalCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.Customer.NationalCodeOrRecordNo == nationalCode).ToList();
            }
        }




        //milad doran
        //public static List<TranslationOpticalCabinetToNormalConnctionInfo> GetTelephoneTranslationOpticalCabinetToNormalConnctionByCabinetID(int cabinetID, bool WithPCM)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Telephones.Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
        //            .Where(t => t.Bucht.CabinetInput.CabinetID == cabinetID &&
        //                        (t.Bucht.PostContact.Status == (int)DB.PostContactStatus.CableConnection) &&
        //                  (WithPCM == true ? t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote : (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote && t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal)))
        //            .OrderBy(t => t.Bucht.PostContact.Post.Number)
        //            .ThenBy(t => t.Bucht.PostContact.ConnectionNo)
        //            .ThenBy(t => t.Bucht.BuchtNo)
        //            .Select(t => new TranslationOpticalCabinetToNormalConnctionInfo
        //            {
        //                FromTelephone = t.Telephone.TelephoneNo,
        //                FromPostContactID = t.Bucht.PostContact.ID,
        //                FromPostConntactNumber = t.Bucht.PostContact.ConnectionNo,
        //                FromPostID = t.Bucht.PostContact.Post.ID,
        //                FromPostNumber = t.Bucht.PostContact.Post.Number,
        //                FromAorBType = t.Bucht.PostContact.Post.AorBType,
        //                FromAorBTypeName = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
        //                FromConnectiontype = (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی")

        //            }).ToList();
        //    }
        //}

        //TODO:rad 13950620
        public static List<TranslationOpticalCabinetToNormalConnctionInfo> GetTelephoneTranslationOpticalCabinetToNormalConnctionByCabinetID(int cabinetID, bool WithPCM)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TranslationOpticalCabinetToNormalConnctionInfo> result = new List<TranslationOpticalCabinetToNormalConnctionInfo>();

                result = context.Telephones
                                .Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
                                .Where(t => 
                                            t.Bucht.CabinetInput.CabinetID == cabinetID &&
                                            (t.Bucht.PostContact.Status == (int)DB.PostContactStatus.CableConnection) &&
                                            (WithPCM == true ? 
                                                (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote) : 
                                                (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote && t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal)
                                            )
                                       )

                                .OrderBy(t => t.Bucht.PostContact.Post.Number)
                                .ThenBy(t => t.Bucht.PostContact.ConnectionNo)
                                .ThenBy(t => t.Bucht.BuchtNo)
                                .Select(t => new TranslationOpticalCabinetToNormalConnctionInfo
                                            {
                                                FromTelephone = t.Telephone.TelephoneNo,
                                                FromPostContactID = t.Bucht.PostContact.ID,
                                                FromPostConntactNumber = t.Bucht.PostContact.ConnectionNo,
                                                FromPostID = t.Bucht.PostContact.Post.ID,
                                                FromPostNumber = t.Bucht.PostContact.Post.Number,
                                                FromAorBType = t.Bucht.PostContact.Post.AorBType,
                                                FromAorBTypeName = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
                                                FromConnectiontype = (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی")
                                            }
                                        )
                                .ToList();
                return result;
            }
        }

        public static List<TranslationOpticalCabinetToNormalConnctionInfo> GetTelephoneByCabinetID(int cabinetID, List<int> postList, bool withPCM)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
                    .Where(t => t.Bucht.CabinetInput.CabinetID == cabinetID &&
                               postList.Contains(t.Bucht.PostContact.PostID) &&
                              (t.Bucht.PostContact.Status == (int)DB.PostContactStatus.CableConnection) &&
                               (withPCM == true ? t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote : t.Bucht.PostContact.ConnectionType == (int)DB.PostContactConnectionType.Noraml)
                           )
                    .OrderBy(t => t.Bucht.PostContact.Post.Number)
                    .ThenBy(t => t.Bucht.PostContact.ConnectionNo)
                    .ThenBy(t => t.Bucht.BuchtNo)
                    .Select(t => new TranslationOpticalCabinetToNormalConnctionInfo
                    {
                        FromTelephone = t.Telephone.TelephoneNo,
                        FromPostContactID = t.Bucht.PostContact.ID,
                        FromPostConntactNumber = t.Bucht.PostContact.ConnectionNo,
                        FromPostID = t.Bucht.PostContact.Post.ID,
                        FromPostNumber = t.Bucht.PostContact.Post.Number,
                        FromAorBType = t.Bucht.PostContact.Post.AorBType,
                        FromAorBTypeName = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
                        FromConnectiontype = (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی"),
                        isApply = false,
                    }).ToList();
            }

        }


        //public static List<ExchangeCabinetInputConnectionInfo> GetTelephoneByCabinetID(int cabinetID, bool WithPCM)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Telephones.Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
        //            .Where(t => t.Bucht.CabinetInput.CabinetID == cabinetID &&
        //                        t.Bucht.PostContact.Status == (int)DB.PostContactStatus.CableConnection &&
        //                  (WithPCM == true ? t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote : (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote && t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal)))
        //            .OrderBy(t => t.Bucht.PostContact.Post.Number)
        //            .ThenBy(t => t.Bucht.PostContact.ConnectionNo)
        //            .ThenBy(t => t.Bucht.BuchtNo)
        //            .Select(t => new ExchangeCabinetInputConnectionInfo
        //            {
        //                FromPostContactID = t.Bucht.PostContact.ID,
        //                FromPostConntactNumber = t.Bucht.PostContact.ConnectionNo,
        //                FromPostID = t.Bucht.PostContact.Post.ID,
        //                FromPostNumber = t.Bucht.PostContact.Post.Number,
        //                FromAorBType = t.Bucht.PostContact.Post.AorBType,
        //                FromAorBTypeName = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
        //                FromConnectiontype = (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی")

        //            }).ToList();
        //    }
        //}
        //public static List<ExchangeCabinetInputConnectionInfo> GetTelephoneByCabinetInputID(int fromCabinetID, long fromCabinetInputID, long toCabinetInputID, bool WithPCM)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Telephones.Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
        //            .Where(t => t.Bucht.CabinetInput.CabinetID == fromCabinetID &&
        //                        t.Bucht.CabinetInput.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == fromCabinetInputID).SingleOrDefault().InputNumber &&
        //                        t.Bucht.CabinetInput.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == toCabinetInputID).SingleOrDefault().InputNumber &&
        //                        t.Bucht.PostContact.Status == (int)DB.PostContactStatus.CableConnection &&
        //                  (WithPCM == true ? t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote : (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote && t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal)))
        //            .OrderBy(t => t.Bucht.PostContact.Post.Number)
        //            .ThenBy(t => t.Bucht.PostContact.ConnectionNo)
        //            .ThenBy(t => t.Bucht.BuchtNo)
        //            .Select(t => new ExchangeCabinetInputConnectionInfo
        //            {
        //                FromPostContactID = t.Bucht.PostContact.ID,
        //                FromPostConntactNumber = t.Bucht.PostContact.ConnectionNo,
        //                FromPostID = t.Bucht.PostContact.Post.Number,
        //                FromPostNumber = t.Bucht.PostContact.Post.Number,
        //                FromAorBType = t.Bucht.PostContact.Post.AorBType,
        //                FromAorBTypeName = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
        //                FromConnectiontype = (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی")

        //            }).ToList();
        //    }
        //}
        public static List<ExchangeCabinetInputConnectionInfo> GetExchangeCabinetInputConnectionInfoTelephoneByCabinetID(int cabinetID, bool withPost, List<int> postList, bool withPCM, long? fromCabinetInputID = null, long? toCabinetInputID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Join(context.Buchts, pc => pc.ID, b => b.ConnectionID, (pc, b) => new { PostContact = pc, Bucht = b })
                                           .GroupJoin(context.Telephones, b => b.Bucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Bucht = b.Bucht, PostContacts = b.PostContact, Telephone = t })
                                           .SelectMany(x => x.Telephone.DefaultIfEmpty(), (x, t) => new { Bucht = x.Bucht, PostContact = x.PostContacts, Telephone = t })
                    .Where(t => (t.Bucht.CabinetInput.CabinetID == cabinetID) &&
                                (withPost == true ? postList.Contains(t.PostContact.PostID) : true) &&
                                (t.PostContact.Status == (int)DB.PostContactStatus.CableConnection || t.PostContact.Status == (int)DB.PostContactStatus.PermanentBroken || t.PostContact.Status == (int)DB.PostContactStatus.NoCableConnection) &&
                                (!fromCabinetInputID.HasValue || t.Bucht.CabinetInput.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == fromCabinetInputID).SingleOrDefault().InputNumber) &&
                               (!toCabinetInputID.HasValue || t.Bucht.CabinetInput.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == toCabinetInputID).SingleOrDefault().InputNumber) &&
                                (withPCM == true ? (t.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine && t.Bucht.BuchtTypeID != (int)DB.BuchtType.OutLine) : (t.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine && t.Bucht.BuchtTypeID != (int)DB.BuchtType.OutLine && t.Bucht.Status != (int)DB.BuchtStatus.ConnectedToPCM))
                           )
                    .OrderBy(t => t.Bucht.PostContact.Post.Number)
                    .ThenBy(t => t.Bucht.PostContact.ConnectionNo)
                    .ThenBy(t => t.Bucht.BuchtNo)
                    .Select(t => new ExchangeCabinetInputConnectionInfo
                    {
                        FromPostContactID = t.Bucht.PostContact.ID,
                        FromPostConntactNumber = t.Bucht.PostContact.ConnectionNo,
                        FromPostID = t.Bucht.PostContact.Post.ID,
                        FromPostNumber = t.Bucht.PostContact.Post.Number,
                        FromAorBType = t.Bucht.PostContact.Post.AorBType,
                        FromAorBTypeName = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
                        FromConnectiontype = (t.Bucht.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی")
                    }).ToList();
            }
        }

        public static List<TeleInfo> GetAllTelphonOfCustomerByTelephonNo(Telephone telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Customer customer = context.Customers.Where(t => t.ID == telephone.CustomerID).SingleOrDefault();
                List<Customer> customers = new List<Customer>();
                if (string.IsNullOrEmpty(customer.NationalCodeOrRecordNo) && string.IsNullOrWhiteSpace(customer.NationalCodeOrRecordNo))
                {
                    customers.Add(customer);
                }
                else
                {
                    customers = context.Customers.Where(t => t.NationalCodeOrRecordNo == customer.NationalCodeOrRecordNo).ToList();
                }

                List<TeleInfo> result = new List<TeleInfo>();
                result = context.Telephones
                                .Where(t => customers.Select(t2 => t2.ID).Contains((long)t.CustomerID))
                                .AsEnumerable()
                                .Select(t => new TeleInfo
                                            {
                                                TelephoneNo = t.TelephoneNo,
                                                CustomerAddressID = t.Address != null ? t.Address.ID : 0,
                                                PostalCode = t.Address != null ? t.Address.PostalCode : "",
                                                TelephoneNoStatusName = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Status),
                                                TelephoneNoStatus = t.Status,
                                                RoundType = DB.GetEnumDescriptionByValue(typeof(DB.RoundType), t.RoundType),
                                                ClassTelephone = t.ClassTelephone == 0 ? string.Empty : DB.GetEnumDescriptionByValue(typeof(DB.ClassTelephone), t.ClassTelephone),
                                                IsVIP = t.IsVIP,
                                                IsRound = t.IsRound,
                                                ConnectDate = t.ConnectDate,
                                                CutDate = t.CutDate,
                                                InstallationDate = t.InstallationDate,
                                                InitialInstallationDate = t.InitialInstallationDate,
                                                TelDischargeDate = t.DischargeDate,
                                                Status = t.Status,
                                                CauseOfCut = (t.CauseOfCutID != null ? t.CauseOfCut.Name : ""),
                                                UsageType = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneUsageType), t.UsageType),
                                                OldCustomer = (t.Status == (int)DB.TelephoneStatus.Discharge) ?
                                                             (
                                                                context.Requests
                                                                       .Where(x => x.TelephoneNo == t.TelephoneNo && x.RequestTypeID == (int)DB.RequestType.Dischargin)
                                                                       .OrderByDescending(x => x.EndDate)
                                                                       .Take(1)
                                                                       .Select(x => (x.Customer.FirstNameOrTitle ?? string.Empty) + " " + (x.Customer.LastName ?? string.Empty))
                                                                       .SingleOrDefault()
                                                             ) : string.Empty,
                                                SpecialService = string.Join("، ", context.TelephoneSpecialServiceTypes.Where(t2 => t2.TelephoneNo == t.TelephoneNo).Select(t3 => t3.SpecialServiceType.Title).ToList())
                                            }
                                       )
                                .ToList();
                return result;
            }
        }



        public static bool CheckReserveTelephone(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo && t.Status == (int)DB.TelephoneStatus.Reserv).Any();
            }
        }

        public static List<Telephone> GetTelephoneByTelephoneNos(List<long> telehones)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => telehones.Contains(t.TelephoneNo)).ToList();
            }
        }

        public static List<Telephone> GetTelephoneBySwitchIDs(List<int> switchPorts)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => switchPorts.Contains((int)t.SwitchPortID)).ToList();
            }
        }

        internal static void UpdateTelephoneSwitchPortID(SwitchPrecode switchPrecode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.ExecuteCommand(@"update T set SwitchPortID = swi.ID from Telephone as T join SwitchPort as swi on T.TelephoneNo = swi.PortNo where T.SwitchPrecodeID = {0}  and swi.SwitchID = {1}", switchPrecode.ID, switchPrecode.SwitchID);
            }
        }
    }
}


//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;
//////using System.Collections;
//////using System.Data.Linq;

//////namespace CRM.Data
//////{
//////    public static class TelephoneDB
//////    {

//////        public static List<Telephone> FindTelephone(long telNo)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Telephones
//////                    .Where(t => (telNo == 0 || telNo == t.TelephoneNo))
//////                    .ToList();
//////            }
//////        }

//////        public static Telephone GetTelephoneByTelephoneNo(long telNo)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Telephones
//////                    .Where(t => t.TelephoneNo == telNo)
//////                    .SingleOrDefault();
//////            }
//////        }

//////        public static List<Telephone> GetTelephoneBySwitchPreCodeNo(Switch switchItem)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Telephones
//////                    .Where(t => t.TelephoneNo >= switchItem.StartNo && t.TelephoneNo <= switchItem.EndNo && switchItem.PreCodeType == t.SwitchPort.Switch.PreCodeType).ToList();

//////            }
//////        }

//////        //public static Telephone GetTelInfoByRequestID(long requestID)
//////        //{
//////        //    using (MainDataContext context = new MainDataContext())
//////        //    {
//////        //        return context.Contracts.Where(t => t.RequestID == requestID).Select(t => new TelRoundInfo
//////        //        {
//////        //            switchID = t.RoundSaleInfo.Telephone.SwitchPort.SwitchID,
//////        //            TelephoneNo = t.RoundSaleInfo.TelephoneNo,
//////        //            portNo = t.RoundSaleInfo.Telephone.SwitchPort.PortNo,
//////        //            portType = t.RoundSaleInfo.Telephone.SwitchPort.Type,
//////        //            switchPreNo = t.RoundSaleInfo.Telephone.SwitchPort.Switch.SwitchPreNo,
//////        //            switchPreCodeType = t.RoundSaleInfo.Telephone.SwitchPort.Switch.PreCodeType

//////        //        }
//////        //                                                                             ).SingleOrDefault();
//////        //    }
//////        //}

//////        public class TelInfo
//////        {
//////            public long TelephoneNo { get; set; }
//////            public long? switchPreNo { get; set; }
//////            public string portNo { get; set; }
//////            public byte? switchPreCodeType { get; set; }
//////            public int? switchID { get; set; }
//////            public bool? portType { get; set; }
//////        }
//////    }
//////}