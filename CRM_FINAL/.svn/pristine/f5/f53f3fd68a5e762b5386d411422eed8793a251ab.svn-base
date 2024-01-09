using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Data.Schema;
using System.Transactions;
using System.Reflection;

namespace CRM.Data
{
    public static class RequestLogDB
    {
        //milad doran
        //public static List<RequestLogReport> SearchRequestLogs(
        //    string requestID,
        //    List<int> requestTypes,
        //    long telephoneNo,
        //    DateTime? fromDate,
        //    DateTime? toDate,
        //    string customerID,
        //    int startRowIndex,
        //    int pageSize,
        //    out int count
        //    )
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        IQueryable<RequestLog> query = context.RequestLogs
        //            .Where(t => (string.IsNullOrEmpty(requestID) || t.RequestID.ToString().Contains(requestID)) &&
        //                        (requestTypes.Count == 0 || requestTypes.Contains(t.RequestTypeID)) &&
        //                        (telephoneNo == -1 || (t.TelephoneNo == telephoneNo || t.ToTelephoneNo == telephoneNo)) &&
        //                        (!fromDate.HasValue || t.Date >= fromDate) &&
        //                        (!toDate.HasValue || t.Date <= toDate) &&
        //                        (t.LogType == null) &&
        //                        (string.IsNullOrEmpty(customerID) || t.CustomerID == customerID)
        //                        ).OrderBy(t => t.Date);

        //        count = query.Count();
        //        return query.AsEnumerable()
        //            .Select(t => new RequestLogReport
        //                        {
        //                            RequestID = t.RequestID,
        //                            RequestType = t.RequestType.Title,
        //                            TelephoneNo = t.TelephoneNo,
        //                            ToTelephone = t.ToTelephoneNo,
        //                            UserName = context.Users.Where(u => u.ID == t.UserID).Select(u => u.FirstName + " " + u.LastName).SingleOrDefault(),
        //                            Date = Date.GetPersianDate(t.Date, Date.DateStringType.Short),
        //                            CustomerID = t.CustomerID,
        //                            Name = context.Customers.Where(x => x.CustomerID == t.CustomerID).Select(x => x.FirstNameOrTitle + " " + x.LastName).SingleOrDefault(),
        //                            Description = GetDescriptiveLog(t.Description, t.RequestTypeID, t.LogType)
        //                        }
        //                    ).Skip(startRowIndex).Take(pageSize).ToList(); ;


        //    }
        //}

        //TODO:rad 13950917
        public static List<RequestLogReport> SearchRequestLogs(
                                                                string requestID, List<int> requestTypes, long telephoneNo,
                                                                DateTime? fromDate, DateTime? toDate, string customerID,
                                                                List<int> centersId,
                                                                int startRowIndex, int pageSize, out int count
                                                               )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestLogReport> result = new List<RequestLogReport>();

                var query = context.RequestLogs

                                   .GroupJoin(context.Requests, requestLog => requestLog.RequestID, request => request.ID, (requestLog, requests) => new { requestLog = requestLog, requests = requests })
                                   .SelectMany(groupedData => groupedData.requests.DefaultIfEmpty(), (groupedData, request) => new { requestLog = groupedData.requestLog, request = request })

                                   .Where(t =>
                                               (string.IsNullOrEmpty(requestID) || t.requestLog.RequestID.ToString().Contains(requestID)) &&
                                               (requestTypes.Count == 0 || requestTypes.Contains(t.requestLog.RequestTypeID)) &&
                                               (telephoneNo == -1 || (t.requestLog.TelephoneNo == telephoneNo || t.requestLog.ToTelephoneNo == telephoneNo)) &&
                                               (!fromDate.HasValue || t.requestLog.Date >= fromDate) &&
                                               (!toDate.HasValue || t.requestLog.Date <= toDate) &&
                                               (t.requestLog.LogType == null) &&
                                               (string.IsNullOrEmpty(customerID) || t.requestLog.CustomerID == customerID) &&

                                               //اگر درخواست در سیستم ما از طریق روال ثبت شده باشد قطعاً شناسه درخواست دارد در غیر این صورت ندارد
                                               (!t.requestLog.RequestID.HasValue || (centersId.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.request.CenterID) : centersId.Contains(t.request.CenterID)))
                                           )
                                   .OrderBy(t => t.requestLog.Date);

                count = query.Count();

                result = query.AsEnumerable()
                              .Select(t => new RequestLogReport
                                          {
                                              CityName = t.requestLog.RequestID.HasValue ? t.requestLog.Request.Center.Region.City.Name : "خارج از روال",
                                              CenterName = t.requestLog.RequestID.HasValue ? t.requestLog.Request.Center.CenterName : "خارج از روال",
                                              RequestID = t.requestLog.RequestID,
                                              RequestType = t.requestLog.RequestType.Title,
                                              TelephoneNo = t.requestLog.TelephoneNo,
                                              ToTelephone = t.requestLog.ToTelephoneNo,
                                              UserName = context.Users.Where(u => u.ID == t.requestLog.UserID).Select(u => u.FirstName + " " + u.LastName).SingleOrDefault(),
                                              Date = Date.GetPersianDate(t.requestLog.Date, Date.DateStringType.Short),
                                              CustomerID = t.requestLog.CustomerID,
                                              Name = context.Customers.Where(x => x.CustomerID == t.requestLog.CustomerID).Select(x => x.FirstNameOrTitle + " " + x.LastName).SingleOrDefault(),
                                              Description = GetDescriptiveLog(t.requestLog.Description, t.requestLog.RequestTypeID, t.requestLog.LogType)
                                          }
                                     )
                              .Skip(startRowIndex)
                              .Take(pageSize)
                              .ToList();

                return result;
            }
        }

        public static int SearchRequestLogsCount(
            string requestID,
            List<int> requestTypes,
            long telephoneNo,
            DateTime? fromDate,
            DateTime? toDate,
            string customerID
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestLogs
                    .Where(t => (string.IsNullOrEmpty(requestID) || t.RequestID.ToString().Contains(requestID)) &&
                                (requestTypes.Count == 0 || requestTypes.Contains(t.RequestTypeID)) &&
                                (telephoneNo == -1 || (t.TelephoneNo == telephoneNo || t.ToTelephoneNo == telephoneNo)) &&
                                (!fromDate.HasValue || t.Date >= fromDate) &&
                                (!toDate.HasValue || t.Date <= toDate) &&
                                (string.IsNullOrEmpty(customerID) || t.CustomerID.Contains(customerID))
                                )

                     .Count();
            }
        }

        private static string GetDescriptiveLog(System.Xml.Linq.XElement description, int requestTypeId, byte? LogType)
        {
            string result = "";
            try
            {

                if (LogType == null)
                {
                    switch (requestTypeId)
                    {
                        case (int)DB.RequestType.Dayri:
                            CRM.Data.Schema.Dayeri dayeri = LogSchemaUtility.Deserialize<CRM.Data.Schema.Dayeri>(description.ToString());
                            result = " تلفن : " + dayeri.TelephoneNo + "بوخت : " + dayeri.Bucht + "ادرس نصب : " + dayeri.InstallAddress + "آبونه : " + dayeri.Cabinet + "_" + dayeri.CabinetInput + "_" + dayeri.Post + "_" + dayeri.PostContact;
                            break;
                        case (int)DB.RequestType.EditTelephoneInstallation:
                            CRM.Data.Schema.EditTelephoneInstallation editTelephoneInstallation = LogSchemaUtility.Deserialize<CRM.Data.Schema.EditTelephoneInstallation>(description.ToString());
                            result = " تلفن : " + editTelephoneInstallation.TelephoneNo + "بوخت : " + editTelephoneInstallation.Bucht + "ادرس نصب : " + editTelephoneInstallation.InstallAddress + "آبونه : " + editTelephoneInstallation.Cabinet + "_" + editTelephoneInstallation.CabinetInput + "_" + editTelephoneInstallation.Post + "_" + editTelephoneInstallation.PostContact;
                            break;
                        case (int)DB.RequestType.PCMInstallation:
                            CRM.Data.Schema.PCMInstalationTelephone pcmInstalationTelephone = LogSchemaUtility.Deserialize<CRM.Data.Schema.PCMInstalationTelephone>(description.ToString());
                            result += string.Format("بوخت : {0} ، رک : {1} شلف : {2} کارت : {3}", pcmInstalationTelephone.Bucht, pcmInstalationTelephone.Rock.ToString(), pcmInstalationTelephone.Shelf.ToString(), pcmInstalationTelephone.Card.ToString());
                            break;
                        case (int)DB.RequestType.DeletePCMInstallation:
                            CRM.Data.Schema.DeletePCMInstalation deletePCMInstalation = LogSchemaUtility.Deserialize<CRM.Data.Schema.DeletePCMInstalation>(description.ToString());
                            result += string.Format("بوخت : {0} ، رک : {1} شلف : {2} کارت : {3}", deletePCMInstalation.Bucht, deletePCMInstalation.Rock.ToString(), deletePCMInstalation.Shelf.ToString(), deletePCMInstalation.Card.ToString());
                            break;
                        case (int)DB.RequestType.Dischargin:
                            CRM.Data.Schema.DischargeTelephone dischargin = LogSchemaUtility.Deserialize<CRM.Data.Schema.DischargeTelephone>(description.ToString());
                            result = "بوخت : " + dischargin.Bucht + "مشترک : " + dischargin.FirstNameOrTitle + " " + dischargin.LastName + " آبونه : " + dischargin.Cabinet + "_" + dischargin.CabinetInput + "_" + dischargin.Post + "_" + dischargin.PostContact + "ادرس : " + dischargin.InstallAddress;
                            break;
                        case (int)DB.RequestType.RefundDeposit:
                            CRM.Data.Schema.DischargeTelephone refundDesposit = LogSchemaUtility.Deserialize<CRM.Data.Schema.DischargeTelephone>(description.ToString());
                            result = "بوخت : " + refundDesposit.Bucht + "مشترک : " + refundDesposit.FirstNameOrTitle + " " + refundDesposit.LastName + " آبونه : " + refundDesposit.Cabinet + "_" + refundDesposit.CabinetInput + "_" + refundDesposit.Post + "_" + refundDesposit.PostContact + "ادرس : " + refundDesposit.InstallAddress;
                            break;
                        case (int)DB.RequestType.ChangeLocationCenterInside:
                        case (int)DB.RequestType.ChangeLocationCenterToCenter:
                            CRM.Data.Schema.ChangeLocation changeLocation = LogSchemaUtility.Deserialize<CRM.Data.Schema.ChangeLocation>(description.ToString());
                            string fromAbone = string.Format("{3}_{2}_{1}_{0}", changeLocation.OldCabinet, changeLocation.OldCabinetInput, changeLocation.OldPost, changeLocation.OldPostContact);
                            string toAbone = string.Format("{3}_{2}_{1}_{0}", changeLocation.NewCabinet, changeLocation.NewCabinetInput, changeLocation.NewPost, changeLocation.NewPostContact);

                            result = string.Format("از مرکز:{0}، از تلفن:{1}، ازبوخت:{2}، ازآبونه:{3}، به مرکز:{4}، به تلفن{5}، به بوخت:{6}، به آبونه:{7}", changeLocation.SourceCenterName, changeLocation.OldTelephone, changeLocation.OldConnectionNo, fromAbone, changeLocation.TargetCenterName, changeLocation.NewTelephone, changeLocation.NewConnectionNo, toAbone);

                            break;

                        case (int)DB.RequestType.ChangeNo:
                            CRM.Data.Schema.ChangeNo changeNo = LogSchemaUtility.Deserialize<CRM.Data.Schema.ChangeNo>(description.ToString());
                            result = "از تلفن : " + changeNo.OldTelephoneNo + "به تلفن : " + changeNo.NewTelephoneNo;
                            break;

                        case (int)DB.RequestType.ModifyProfile:
                            CRM.Data.Schema.ModifyProfile modifyProfile = LogSchemaUtility.Deserialize<CRM.Data.Schema.ModifyProfile>(description.ToString());
                            result = "از آبونه : " + modifyProfile.OldCabinet + "_" + modifyProfile.OldCabinetInput + "_" + modifyProfile.OldPost + "_" + modifyProfile.OldPostContact + " از بوخت : " + modifyProfile.OldConnectionNo + " به آبونه : " + modifyProfile.NewCabinet + "_" + modifyProfile.NewCabinetInput + "_" + modifyProfile.NewPost + "_" + modifyProfile.NewPostContact + " به بوخت : " + modifyProfile.NewConnectionNo;
                            break;

                        case (int)DB.RequestType.SwapTelephone:
                            CRM.Data.Schema.SwapTelephone swapTelephone = LogSchemaUtility.Deserialize<CRM.Data.Schema.SwapTelephone>(description.ToString());
                            result = "از آبونه : " + swapTelephone.FromCabinet + "_" + swapTelephone.FromCabinetInput + "_" + swapTelephone.FromPost + "_" + swapTelephone.FromPostContact + " از بوخت : " + swapTelephone.FromConnectionNo + " به آبونه : " + swapTelephone.ToCabinet + "_" + swapTelephone.ToCabinetInput + "_" + swapTelephone.ToPost + "_" + swapTelephone.ToPostContact + " به بوخت : " + swapTelephone.ToConnectionNo;
                            break;
                        case (int)DB.RequestType.BuchtSwiching:
                            CRM.Data.Schema.BuchtSwitching buchtSwitching = LogSchemaUtility.Deserialize<CRM.Data.Schema.BuchtSwitching>(description.ToString());
                            result = "از آبونه : " + buchtSwitching.OldCabinet + "_" + buchtSwitching.OldCabinetInput + "_" + buchtSwitching.OldPost + "_" + buchtSwitching.OldPostContact + " از بوخت : " + buchtSwitching.OldBuchtNo + " به آبونه : " + buchtSwitching.NewCabinet + "_" + buchtSwitching.NewCabinetInput + "_" + buchtSwitching.NewPost + "_" + buchtSwitching.NewPostContact + " به بوخت : " + buchtSwitching.NewBuchtNo;
                            break;
                        case (int)DB.RequestType.PCMToNormal:
                            CRM.Data.Schema.PCMToNormal PCMToNormal = LogSchemaUtility.Deserialize<CRM.Data.Schema.PCMToNormal>(description.ToString());
                            result = "از آبونه : " + PCMToNormal.OldCabinet + "_" + PCMToNormal.OldCabinetInput + "_" + PCMToNormal.OldPost + "_" + PCMToNormal.OldPostContact + " از بوخت : " + PCMToNormal.OldConnectionNo + " به آبونه : " + PCMToNormal.NewCabinet + "_" + PCMToNormal.NewCabinetInput + "_" + PCMToNormal.NewPost + "_" + PCMToNormal.NewPostContact + " به بوخت : " + PCMToNormal.NewConnectionNo;
                            break;
                        case (int)DB.RequestType.IncreaseBandwidth:
                            CRM.Data.Schema.BandwidthInfo increaseBandwidth = LogSchemaUtility.Deserialize<CRM.Data.Schema.BandwidthInfo>(description.ToString());
                            result = " پهنای باند قبلی : " + increaseBandwidth.PreviousBandwidth + " پهنای باند فعلی : " + increaseBandwidth.CurrentBandwidth + " مشترک : " + increaseBandwidth.ClientId;
                            break;

                        case (int)DB.RequestType.DecrementBandwidth:
                            CRM.Data.Schema.BandwidthInfo decrementBandwidth = LogSchemaUtility.Deserialize<CRM.Data.Schema.BandwidthInfo>(description.ToString());
                            result = " پهنای باند قبلی : " + decrementBandwidth.PreviousBandwidth + " پهنای باند فعلی : " + decrementBandwidth.CurrentBandwidth + " مشترک : " + decrementBandwidth.ClientId;
                            break;

                        case (int)DB.RequestType.E1:
                            CRM.Data.Schema.E1LinkLog e1LinkLog = LogSchemaUtility.Deserialize<CRM.Data.Schema.E1LinkLog>(description.ToString());
                            Address e1ddress = Data.AddressDB.GetAddressByID(e1LinkLog.InstalAddressID);
                            Customer customer = Data.CustomerDB.GetCustomerByID(e1LinkLog.CustomerID);
                            result = " آدرس : " + e1ddress.AddressContent + " ,مشترک : " + customer.FirstNameOrTitle ?? string.Empty + customer.LastName ?? string.Empty;
                            break;

                        case (int)DB.RequestType.OpenAndCloseZero:
                            {
                                CRM.Data.Schema.OpenAndCloseZero openAndCloseZeroLog = LogSchemaUtility.Deserialize<CRM.Data.Schema.OpenAndCloseZero>(description.ToString());
                                result = "کلاس قبلی : " + Helpers.GetEnumDescription(openAndCloseZeroLog.OldClassTelephone, typeof(DB.ClassTelephone)) + Environment.NewLine +
                                         "کلاس جاری : " + Helpers.GetEnumDescription(openAndCloseZeroLog.ClassTelephone, typeof(DB.ClassTelephone)) + Environment.NewLine;
                            }
                            break;
                        case (int)DB.RequestType.SpaceandPower:
                            {
                                CRM.Data.Schema.SpaceAndPower spaceAndPowerLog = LogSchemaUtility.Deserialize<CRM.Data.Schema.SpaceAndPower>(description.ToString());
                                //result = " - دارای فیبر میباشد یا خیر : " + ((spaceAndPowerLog.HasFibre) ? " بله " : " خیر ") + Environment.NewLine +
                                //         " - مدت زمان - ماه : " + (string.IsNullOrEmpty(spaceAndPowerLog.Duration) ? "نامشخص" : spaceAndPowerLog.Duration) + Environment.NewLine +
                                //         " - متراژ فضا - متر مربع : " + (string.IsNullOrEmpty(spaceAndPowerLog.SpaceSize) ? "نامشخص" : spaceAndPowerLog.SpaceSize) + Environment.NewLine +
                                //         " - نوع فضا : " + Helpers.GetEnumDescription(spaceAndPowerLog.SpaceType, typeof(DB.SpaceType)) + Environment.NewLine +
                                //         " - وزن تجهیزات : " + (string.IsNullOrEmpty(spaceAndPowerLog.EquipmentWeight) ? "نامشخص" : spaceAndPowerLog.EquipmentWeight) + Environment.NewLine +
                                //         " - نوع تجهیزات : " + Helpers.GetEnumDescription(spaceAndPowerLog.EquipmentType, typeof(DB.EquipmentType)) + Environment.NewLine +
                                //         " - کاربری فضا : " + (string.IsNullOrEmpty(spaceAndPowerLog.SpaceUsage) ? "نامشخص" : spaceAndPowerLog.SpaceUsage) + Environment.NewLine +
                                //         " - نوع پاور : " + Helpers.GetEnumDescription(spaceAndPowerLog.PowerType, typeof(DB.PowerType)) + Environment.NewLine +
                                //         " - میزان پاور مصرفی : " + (string.IsNullOrEmpty(spaceAndPowerLog.PowerRate) ? "نامشخص" : spaceAndPowerLog.PowerRate) + Environment.NewLine +
                                //         " - فضای روی دکل : " + (string.IsNullOrEmpty(spaceAndPowerLog.RigSpace) ? "نامشخص" : spaceAndPowerLog.RigSpace) + Environment.NewLine +
                                //         " - شناسه متقاضی : " + spaceAndPowerLog.SpaceAndPowerCustomerID;
                                result = " - دارای فیبر میباشد یا خیر : " + ((spaceAndPowerLog.HasFibre) ? " بله " : " خیر ") + "،" +
                                         " - مدت زمان - ماه : " + (string.IsNullOrEmpty(spaceAndPowerLog.Duration) ? "نامشخص" : spaceAndPowerLog.Duration) + " ، " +
                                         " - متراژ فضا - متر مربع : " + (string.IsNullOrEmpty(spaceAndPowerLog.SpaceSize) ? "نامشخص" : spaceAndPowerLog.SpaceSize) + "،" +
                                         " - نوع فضا : " + Helpers.GetEnumDescription(spaceAndPowerLog.SpaceType, typeof(DB.SpaceType)) + "،" +
                                         " - وزن تجهیزات : " + (string.IsNullOrEmpty(spaceAndPowerLog.EquipmentWeight) ? "نامشخص" : spaceAndPowerLog.EquipmentWeight) + " ، " +
                                         " - نوع تجهیزات : " + Helpers.GetEnumDescription(spaceAndPowerLog.EquipmentType, typeof(DB.EquipmentType)) + " ، " +
                                         " - کاربری فضا : " + (string.IsNullOrEmpty(spaceAndPowerLog.SpaceUsage) ? "نامشخص" : spaceAndPowerLog.SpaceUsage) + " ، " +
                                         " - نوع پاور : " + spaceAndPowerLog.PowerType + " ، " +
                                         " - فضای روی دکل : " + (string.IsNullOrEmpty(spaceAndPowerLog.RigSpace) ? "نامشخص" : spaceAndPowerLog.RigSpace) + " ، " +
                                         " - نام متقاضی : " + spaceAndPowerLog.CustomerName;
                            }
                            break;
                        case (int)DB.RequestType.TelephoneVisitAddress:
                            {
                                CRM.Data.Schema.TelephoneVisitAddress telephoneVisitAddress = LogSchemaUtility.Deserialize<CRM.Data.Schema.TelephoneVisitAddress>(description.ToString());
                                Post post = PostDB.GetPostByID(telephoneVisitAddress.CrossPostID ?? 0);
                                result = "شماره تلفن : " + telephoneVisitAddress.TelephoneNo.ToString() + Environment.NewLine +
                                         "شناسه آدرس : " + telephoneVisitAddress.AddressID.ToString() + Environment.NewLine +
                                         "متن آدرس: " + telephoneVisitAddress.AddressContent + Environment.NewLine +
                                         "کد پستی : " + telephoneVisitAddress.PostalCode + Environment.NewLine +
                                         "تاریخ بازدید : " + telephoneVisitAddress.VisitDate.ToPersian(Date.DateStringType.Short) + Environment.NewLine +
                                         "ساعت بازدید : " + telephoneVisitAddress.VisitHour + Environment.NewLine +
                                         "آیا خارج از مرز میباشد : " + ((telephoneVisitAddress.IsOutBound) ? "بله" : "خیر") + Environment.NewLine +
                                         "متراژ خارج از مرز : " + ((telephoneVisitAddress.OutBoundMeter.HasValue) ? telephoneVisitAddress.OutBoundMeter.Value.ToString() : "-----") + Environment.NewLine +
                                         "تاریخ ثبت خارج از مرز : " + ((telephoneVisitAddress.OutBoundEstablishDate.HasValue) ? telephoneVisitAddress.OutBoundEstablishDate.Value.ToPersian(Date.DateStringType.Short) : "-----") + Environment.NewLine +
                                         "تیر شش متری : " + ((telephoneVisitAddress.SixMeterMasts.HasValue) ? telephoneVisitAddress.SixMeterMasts.Value.ToString() : "-----") + Environment.NewLine +
                                         "تیر هشت متری : " + ((telephoneVisitAddress.EightMeterMasts.HasValue) ? telephoneVisitAddress.EightMeterMasts.Value.ToString() : "-----") + Environment.NewLine +
                                         "پست عبوری : " + ((post != null) ? post.Number.ToString() : "-----") + Environment.NewLine +
                                         "عرض عبوری : " + ((telephoneVisitAddress.ThroughWidth.HasValue) ? telephoneVisitAddress.ThroughWidth.Value.ToString() : "-----") + Environment.NewLine +
                                         "متراژ سیم هوایی : " + ((telephoneVisitAddress.AirCableMeter.HasValue) ? telephoneVisitAddress.AirCableMeter.Value.ToString() : "-----") + Environment.NewLine +
                                         "متراژ سیم : " + ((telephoneVisitAddress.CableMeter.HasValue) ? telephoneVisitAddress.CableMeter.Value.ToString() : "-----");
                            }
                            break;
                        case (int)DB.RequestType.ChangeTelephoneRound:
                            {
                                ChangeTelephoneRoundLog changeTelephoneLog = LogSchemaUtility.Deserialize<ChangeTelephoneRoundLog>(description.ToString());
                                result = "توضیحات : " + changeTelephoneLog.Description + Environment.NewLine +
                                         "آیا تلفن رند شده است : " + ((changeTelephoneLog.IsRound) ? "بله" : "خیر") + Environment.NewLine +
                                         "نوع رند قبلی : " + ((changeTelephoneLog.PreviousRoundType.HasValue) ? Helpers.GetEnumDescription(changeTelephoneLog.PreviousRoundType, typeof(DB.RoundType)) : "-----") + Environment.NewLine +
                                         "نوع رند جاری : " + ((changeTelephoneLog.CurrentRoundType.HasValue) ? Helpers.GetEnumDescription(changeTelephoneLog.CurrentRoundType, typeof(DB.RoundType)) : "-----");
                            }
                            break;
                        case (int)DB.RequestType.ChangeName:
                            CRM.Data.Schema.ChangeName changeName = LogSchemaUtility.Deserialize<CRM.Data.Schema.ChangeName>(description.ToString());
                            Customer oldCustoemr = Data.CustomerDB.GetCustomerByID(changeName.OldCustomerID);
                            Customer newCustoemr = Data.CustomerDB.GetCustomerByID(changeName.NewCustomerID);
                            result = "نام قدیم : " + oldCustoemr.FirstNameOrTitle + " " + oldCustoemr.LastName + " ، نام جدید : " + newCustoemr.FirstNameOrTitle + " " + newCustoemr.LastName;
                            break;

                        case (int)DB.RequestType.CutAndEstablish:

                            CRM.Data.Schema.CutAndEstablish cutAndEstablish = LogSchemaUtility.Deserialize<CRM.Data.Schema.CutAndEstablish>(description.ToString());
                            result = "وضعیت : قطع کردن شماره";

                            break;

                        case (int)DB.RequestType.Connect:
                            CRM.Data.Schema.Connect connect = LogSchemaUtility.Deserialize<CRM.Data.Schema.Connect>(description.ToString());
                            result = "وضعیت : وصل کردن شماره";
                            break;

                        case (int)DB.RequestType.SpecialWire:
                            CRM.Data.Schema.EstablishSpecialWire establishSpecialWire = LogSchemaUtility.Deserialize<CRM.Data.Schema.EstablishSpecialWire>(description.ToString());
                            Address address = Data.AddressDB.GetAddressByID(establishSpecialWire.InstallAddressID);
                            result = "مرکز : " + establishSpecialWire.CenterName + " آدرس نصب : " + address.AddressContent + " بوخت: " + establishSpecialWire.ConnectionNo;
                            break;
                        case (int)DB.RequestType.VacateSpecialWire:
                            CRM.Data.Schema.VacateSpecialWire vacateSpecialWire = LogSchemaUtility.Deserialize<CRM.Data.Schema.VacateSpecialWire>(description.ToString());
                            Address vacateAddress = Data.AddressDB.GetAddressByID(vacateSpecialWire.OldInstallAddressID);
                            result = "مرکز : " + vacateSpecialWire.CenterName + " آدرس تخلیه : " + vacateAddress.AddressContent;
                            break;
                        case (int)DB.RequestType.ChangeLocationSpecialWire:
                            CRM.Data.Schema.ChangeLocationSpecialWire changeLocationSpecialWire = LogSchemaUtility.Deserialize<CRM.Data.Schema.ChangeLocationSpecialWire>(description.ToString());
                            Address passAddress = Data.AddressDB.GetAddressByID(changeLocationSpecialWire.OldInstallAddressID);
                            Address newInsatllAddress = Data.AddressDB.GetAddressByID(changeLocationSpecialWire.NewInstallAddressID);
                            PostContact oldPostContact = PostContactDB.GetPostContactByID(changeLocationSpecialWire.OldPostContactID);
                            Post oldPost = PostDB.GetPostByID(oldPostContact.PostID);
                            result = "مرکز : " + changeLocationSpecialWire.CenterName + " آدرس تخلیه : " + passAddress.AddressContent + " آدرس نصب جدید : " + newInsatllAddress.AddressContent + "آبونه : " + changeLocationSpecialWire.OldCabinetNumber + "_" + changeLocationSpecialWire.OldCabinetInputNumber + "_" + oldPostContact.ConnectionNo + "_" + oldPost.Number;
                            break;
                        case (int)DB.RequestType.SpecialService:
                            //CRM.Data.Schema.SpecialService specialService = LogSchemaUtility.Deserialize<CRM.Data.Schema.SpecialService>(description.ToString());
                            //if (specialService.Status == (byte)DB.StatusSpecialService.Instal)
                            //    result = SpecialServiceTypeDB.GetSpecialServiceTypeByID(specialService.SpecialServiceTypeID).Title + "  ایجاد شد";
                            //if (specialService.Status == (byte)DB.StatusSpecialService.UnInstal)
                            //    result = SpecialServiceTypeDB.GetSpecialServiceTypeByID(specialService.SpecialServiceTypeID).Title + "  حذف شد";
                            break;

                        case (int)DB.RequestType.TranslationOpticalCabinetToNormal:
                            CRM.Data.Schema.TranslationOpticalToNormal translationOpticalToNormal = LogSchemaUtility.Deserialize<CRM.Data.Schema.TranslationOpticalToNormal>(description.ToString());
                            result = string.Format("از تلفن: {0}، از بوخت: {1}، از آبونه {2}، به تلفن: {3} ، به بوخت: {4}، به آبونه {5}، ", +translationOpticalToNormal.OldTelephone, translationOpticalToNormal.OldBucht, translationOpticalToNormal.OldCabinet + "_" + translationOpticalToNormal.OldCabinetInput + "_" + translationOpticalToNormal.OldPost + "_" + translationOpticalToNormal.OldPostContact, translationOpticalToNormal.NewTelephone, translationOpticalToNormal.NewBucht, translationOpticalToNormal.NewCabinet + "_" + translationOpticalToNormal.NewCabinetInput + "_" + translationOpticalToNormal.NewPost + "_" + translationOpticalToNormal.NewPostContact);
                            //"تلفن قدیم : " + translationOpticalToNormal.OldTelephone + " بوخت قدیم : " + translationOpticalToNormal.OldBucht + " تلفن جدید : " + translationOpticalToNormal.NewTelephone + " بوخت جدید : " + translationOpticalToNormal.NewBucht;
                            break;

                        case (int)DB.RequestType.ExchangePost:
                            CRM.Data.Schema.TranslationPost translationPost = LogSchemaUtility.Deserialize<CRM.Data.Schema.TranslationPost>(description.ToString());
                            result = "ازکافو : " + translationPost.OldCabinet + " ازپست : " + translationPost.OldPost + " ازاتصالی : " + translationPost.OldPostContact + "به کافو : " + translationPost.NewCabinet + " به پست : " + translationPost.NewPost + " به اتصالی : " + translationPost.NewPostContact;
                            break;

                        case (int)DB.RequestType.TranlationPostInput:
                            CRM.Data.Schema.TranslationPostInput translationPostInput = LogSchemaUtility.Deserialize<CRM.Data.Schema.TranslationPostInput>(description.ToString());
                            result = "ازپست : " + translationPostInput.OldPost + " ازاتصالی : " + translationPostInput.OldPostContact + " به پست : " + translationPostInput.NewPost + " به پست : " + translationPostInput.NewPostContact + " به مرکزی :" + translationPostInput.NewCabinetInput;
                            break;

                        case (int)DB.RequestType.ExchangeCabinetInput:
                            CRM.Data.Schema.ExchangeCabinetInputLog translationCabinet = LogSchemaUtility.Deserialize<CRM.Data.Schema.ExchangeCabinetInputLog>(description.ToString());
                            result = string.Format("از آبونه {0}، به آبونه {1}، ", translationCabinet.OldCabinet + "_" + translationCabinet.OldCabinetInput + "_" + translationCabinet.OldPost + "_" + translationCabinet.OldPostContact, translationCabinet.NewCabinet + "_" + translationCabinet.NewCabinetInput + "_" + translationCabinet.NewPost + "_" + translationCabinet.NewPostContact);
                            break;
                        case (int)DB.RequestType.BrokenPCM:
                            CRM.Data.Schema.ExchangePCMCardInfo exchangePCMCardInfo = LogSchemaUtility.Deserialize<CRM.Data.Schema.ExchangePCMCardInfo>(description.ToString());
                            result = "از پی سی ام : " + exchangePCMCardInfo.FromMuID + " به پی سی ام : " + exchangePCMCardInfo.ToMUID;
                            break;

                        case (int)DB.RequestType.ADSL:
                            CRM.Data.Schema.ADSL ADSL = LogSchemaUtility.Deserialize<CRM.Data.Schema.ADSL>(description.ToString());

                            Customer customer1 = Data.CustomerDB.GetCustomerByID(ADSL.CustomerOwnerID);
                            ADSLService tariff = DB.SearchByPropertyName<ADSLService>("ID", ADSL.TariffID).SingleOrDefault();
                            Contractor contractor = DB.SearchByPropertyName<Contractor>("ID", ADSL.ContractorID).SingleOrDefault();

                            ADSLPortsInfo port = ADSLPortDB.GetADSlPortsInfoByID(ADSL.ADSLPortID);

                            result = " تعرفه : " + tariff.Title + "     ،     " + " پورت : " + port.PortNo;
                            break;

                        case (int)DB.RequestType.ADSLChangeService:
                            CRM.Data.Schema.ADSLChangeTariff ADSLChangeTariff = LogSchemaUtility.Deserialize<CRM.Data.Schema.ADSLChangeTariff>(description.ToString());

                            ADSLService oldService = DB.SearchByPropertyName<ADSLService>("ID", ADSLChangeTariff.OldTariffID).SingleOrDefault();
                            ADSLService newService = DB.SearchByPropertyName<ADSLService>("ID", ADSLChangeTariff.NewTariffID).SingleOrDefault();

                            result = "تعرفه قدیم : " + oldService.Title + "تعرفه جدید : " + newService.Title;
                            break;

                        default:
                            break;

                        case (int)DB.RequestType.ADSLCutTemporary:
                            CRM.Data.Schema.ADSLCutTemporary ADSLCutTemporary = LogSchemaUtility.Deserialize<CRM.Data.Schema.ADSLCutTemporary>(description.ToString());

                            switch (ADSLCutTemporary.CutType)
                            {
                                case (byte)DB.ADSLCutType.Administrative:
                                    result = "علت قطع : ادازی";
                                    break;

                                case (byte)DB.ADSLCutType.SubscriberRequest:
                                    result = "علت قطع : شخصی";
                                    break;

                                default:
                                    break;
                            }

                            break;
                    }
                }
            }
            catch
            {
            }
            return result;
        }


        public static RequestLog GetRequestLogByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestLogs
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static RequestLog GetLastRequestLogByTelephoneNo(long TelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestLogs.Where(t => t.TelephoneNo == TelephoneNo).OrderByDescending(t => t.Date).FirstOrDefault();
            }
        }
        public static RequestLog GetLastRequestLogByReqeusetID(long ID, int logType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestLogs.Where(t => t.RequestID == ID && t.LogType == logType).OrderByDescending(t => t.Date).FirstOrDefault();
            }
        }

        public static RequestLogReport GetRequestLogReport(long? telephoneNo, long? toTelephoneNo, DB.RequestType requestType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                RequestLogReport result = new RequestLogReport();

                Type logEntityType = Helpers.GetLogEntityTypeByEnumFieldName(requestType.ToString(), "CRM.Data.Schema", "CRM.Data");

                //باید ساختار اصلی متد را با رفلکشن بدست بیاوریم تا بتوانیم ورژن جنریک آن را ایجاد کنیم
                MethodInfo getDescritiveLogEntityMethodInfo = typeof(DB).GetMethod("GetDescriptiveLogEntity");

                //ایجاد ورژن جنریک متدی که ریز اطلاعات مربوط به یک لاگ را برای ما دی سر یالایز میکند
                MethodInfo genericGetDescritiveLogEntityMethod = getDescritiveLogEntityMethodInfo.MakeGenericMethod(logEntityType);

                //جستجو برای دستیابی به رکورد مورد نظر
                var query = context.RequestLogs
                                   .Where(rl =>
                                              rl.RequestTypeID == (int)requestType
                                              &&
                                              (!telephoneNo.HasValue || rl.TelephoneNo == telephoneNo)
                                              &&
                                              (!toTelephoneNo.HasValue || rl.ToTelephoneNo == toTelephoneNo)
                                         )
                                   .OrderByDescending(rl => rl.Date);

                result = query.Select(rl => new RequestLogReport
                                          {
                                              RequestType = rl.RequestType.Title,
                                              TelephoneNo = rl.TelephoneNo,
                                              ToTelephone = rl.ToTelephoneNo,
                                              UserName = context.Users.Where(u => u.ID == rl.UserID).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault(),
                                              Date = rl.Date.ToPersian(Date.DateStringType.Short),
                                              CustomerID = rl.CustomerID,
                                              Name = context.Customers.Where(cu => cu.CustomerID == rl.CustomerID).Select(cu => cu.FirstNameOrTitle + " " + cu.LastName).FirstOrDefault(),
                                              LogEntityDetails = genericGetDescritiveLogEntityMethod.Invoke(null, new object[] { rl.Description })
                                          }
                                     )
                             .FirstOrDefault();

                return result;
            }
        }

        //TODO :rad
        public static List<RequestLogReport> GetSwapTelephoneRequestLogReportList(long? telephoneNo, long? toTelephoneNo, DateTime? fromDate, DateTime? toDate, List<int> citiesID, List<int> centersID)
        {

            if (toDate.HasValue)
                toDate = toDate.Value.AddDays(1);


            using (MainDataContext context = new MainDataContext())
            {
                List<RequestLogReport> result = new List<RequestLogReport>();

                var query = context.RequestLogs
                                   .Where(rl => ((!telephoneNo.HasValue || rl.TelephoneNo == telephoneNo)
                                                 &&
                                                 (!toTelephoneNo.HasValue || rl.ToTelephoneNo == toTelephoneNo)
                                                 &&
                                                 (!fromDate.HasValue || rl.Date >= fromDate)
                                                 &&
                                                 (!toDate.HasValue || rl.Date <= toDate)
                                                 &&
                                                 (centersID.Count == 0 || centersID.Contains(context.Telephones.Where(t => t.TelephoneNo == rl.TelephoneNo.Value || t.TelephoneNo == rl.ToTelephoneNo.Value).Select(t => t.CenterID).FirstOrDefault()))
                                                 &&
                                                 (citiesID.Count == 0 || citiesID.Contains(context.Telephones.Where(t => t.TelephoneNo == rl.ToTelephoneNo.Value).Select(t => t.Center.Region.City.ID).FirstOrDefault()))
                                                 &&
                                                 rl.RequestTypeID == (int)DB.RequestType.SwapTelephone
                                               )
                                         )
                                   .OrderByDescending(rl => rl.Date.Value);

                result = query.Select(rl => new RequestLogReport
                                            {
                                                RequestType = rl.RequestType.Title,
                                                TelephoneNo = rl.TelephoneNo,
                                                ToTelephone = rl.ToTelephoneNo,
                                                UserName = context.Users.Where(u => u.ID == rl.UserID.Value).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault(),
                                                Date = rl.Date.ToPersian(Date.DateStringType.Short),
                                                CustomerID = rl.CustomerID,
                                                Name = context.Customers.Where(cu => cu.CustomerID == rl.CustomerID).Select(cu => cu.FirstNameOrTitle + " " + cu.LastName).FirstOrDefault(),
                                                LogEntityDetails = DB.GetDescriptiveLogEntity<SwapTelephone>(rl.Description)
                                            })
                              .ToList();

                return result;
            }
        }

        //TODO:rad
        public static List<RequestLogReport> GetRequestLogReportList(DB.RequestType requestType, DateTime? fromDate, DateTime? toDate, List<int> citiesID, List<int> centersID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestLogReport> result = new List<RequestLogReport>();

                Type logEntityType = Helpers.GetLogEntityTypeByEnumFieldName(requestType.ToString(), "CRM.Data.Schema", "CRM.Data");

                MethodInfo getDescriptiveLogEntityMethodInfo = typeof(DB).GetMethod("GetDescriptiveLogEntity");

                MethodInfo genericGetDescriptiveLogEntityMethodInfo = getDescriptiveLogEntityMethodInfo.MakeGenericMethod(logEntityType);

                var query = context.RequestLogs
                                   .Where(rl =>
                                            rl.RequestTypeID == (int)requestType
                                            &&
                                            (!fromDate.HasValue || rl.Date >= fromDate)
                                            &&
                                            (!toDate.HasValue || rl.Date <= toDate)
                                            &&
                                            (citiesID.Count == 0 || citiesID.Contains(context.Telephones.Where(t => t.TelephoneNo == rl.TelephoneNo.Value || t.TelephoneNo == rl.ToTelephoneNo.Value).Select(t => t.Center.Region.City.ID).FirstOrDefault()))
                                            &&
                                            (centersID.Count == 0 || centersID.Contains(context.Telephones.Where(t => t.TelephoneNo == rl.TelephoneNo.Value || t.TelephoneNo == rl.ToTelephoneNo.Value).Select(t => t.CenterID).FirstOrDefault()))
                                         )
                                   .OrderByDescending(rl => rl.Date.Value);

                result = query.Select(rl => new RequestLogReport
                                        {
                                            RequestType = rl.RequestType.Title,
                                            TelephoneNo = rl.TelephoneNo,
                                            ToTelephone = rl.ToTelephoneNo,
                                            UserName = context.Users.Where(u => u.ID == rl.UserID).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault(),
                                            Date = rl.Date.ToPersian(Date.DateStringType.Short),
                                            CustomerID = rl.CustomerID,
                                            Name = context.Customers.Where(cu => cu.CustomerID == rl.CustomerID).Select(cu => cu.FirstNameOrTitle + " " + cu.LastName).FirstOrDefault(),
                                            LogEntityDetails = genericGetDescriptiveLogEntityMethodInfo.Invoke(null, new object[] { rl.Description })
                                        }
                                    ).ToList();

                return result;

            }
        }

        public static RequestLog GetLastTelephoneNoRequestLogByRequestType(long TelephoneNo, DB.RequestType requestType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestLogs.Where(t => t.TelephoneNo == TelephoneNo && t.RequestTypeID == (int)requestType).OrderByDescending(t => t.Date).FirstOrDefault();
            }
        }

        public static List<RequestLog> GetReqeustLogByTelephone(long telephoneNo, DateTime fromDate, DateTime toDate)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.ExecuteQuery<RequestLog>(@"    WITH ShowReqeustLog(ID,RequestID ,RequestTypeID,TelephoneNo , ToTelephoneNo ,CustomerID , UserID , Date , Description)
                                                                    AS
                                                                    (
                                                                    SELECT ID,RequestID ,RequestTypeID,TelephoneNo , ToTelephoneNo ,CustomerID , UserID , Date , Description FROM CRM.dbo.RequestLog
                                                                    WHERE TelephoneNo = {2} AND Date >= {0} AND Date <= {1} AND LogType is null
                                                                    UNION ALL
                                                                             SELECT RL.ID, RL.RequestID ,RL.RequestTypeID,RL.TelephoneNo , RL.ToTelephoneNo ,RL.CustomerID , RL.UserID , RL.Date , RL.Description
                                                                             FROM ShowReqeustLog  AS SRL
                                                                             JOIN CRM.dbo.RequestLog AS RL ON RL.TelephoneNo =  SRL.ToTelephoneNo AND RL.Date >= {0}  AND RL.Date <= {1} where ShowReqeustLog.ToTelephoneNo != {2} where SRL.ToTelephoneNo != {0} AND LogType is null
                                                                    )
                                                                    SELECT * FROM ShowReqeustLog ORDER BY Date", fromDate, toDate, telephoneNo
                                                  ).ToList();
            }
        }

        public static List<RequestLog> GetReqeustLogByTelephone(long telephoneNo)
        {

            using (MainDataContext context = new MainDataContext())
            {


                return context.RequestLogs.Where(t => t.TelephoneNo == telephoneNo || t.ToTelephoneNo == telephoneNo).OrderBy(t => t.Date).ToList();
                //                return context.ExecuteQuery<RequestLog>(@"    WITH ShowReqeustLog(ID,RequestID ,RequestTypeID,TelephoneNo , ToTelephoneNo ,CustomerID , UserID , Date , Description)
                //                                                                    AS
                //                                                                    (
                //                                                                    SELECT ID,RequestID ,RequestTypeID,TelephoneNo , ToTelephoneNo ,CustomerID , UserID , Date , Description FROM CRM.dbo.RequestLog
                //                                                                    WHERE ( TelephoneNo = {0} OR ToTelephoneNo = {0}) AND LogType is null
                //                                                                    UNION ALL
                //                                                                             SELECT RL.ID, RL.RequestID ,RL.RequestTypeID,RL.TelephoneNo , RL.ToTelephoneNo ,RL.CustomerID , RL.UserID , RL.Date , RL.Description
                //                                                                             FROM ShowReqeustLog  AS SRL
                //                                                                             JOIN CRM.dbo.RequestLog AS RL ON RL.TelephoneNo =  SRL.ToTelephoneNo  where SRL.ToTelephoneNo != {0} AND LogType is null
                //                                                                    )
                //                                                                    SELECT * FROM ShowReqeustLog ORDER BY Date", telephoneNo
                //                                                  ).ToList();
            }
        }

        public static void RejectSaveLogDischargeOldTelephone(Request request, DB.RequestType requestType, DB.LogType logType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (context.RequestLogs.Any(t => t.RequestID == request.ID && t.LogType == (int)logType && t.IsReject == false))
                    {
                        context.RequestLogs.Where(t => t.RequestID == request.ID && t.LogType == (int)logType).OrderByDescending(t => t.Date).FirstOrDefault().IsReject = true;
                    }

                    context.SubmitChanges();
                    Subts.Complete();

                }
            }
        }

        public static bool ExistReqeustLog(long requestID, int requestTypeID, long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestLogs.Any(t => t.RequestID == requestID && t.RequestTypeID == requestTypeID && t.TelephoneNo == telephoneNo);
            }
        }
    }
}