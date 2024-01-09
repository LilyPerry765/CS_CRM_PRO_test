using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class RequestListDB
    {
        public static List<ChangeTelephone> GetChangeTelephone(
            bool? isOutBound,
            DateTime? fromDateTime,
            DateTime? toDateTime,
            List<int> centerIDs,
            List<int> requestTypeIDs,
            List<int> causeOfCut,
            List<int> causeOfChangeNo,
            List<int> causeOfRefundDeposit,
            List<int> causeOfTakePossession,
            long telephoneNo,
            List<int> cusstomerType,
            List<int> cusstomerGroup,
            List<int> zeroBlock,
            bool? isChangeName,
            List<int> preCodeType,
            int startRowIndex,
            int pageSize,
            out int count
            )
        {
            try
            {

                using (MainDataContext context = new MainDataContext())
                {
                    context.CommandTimeout = 0;
                    List<ChangeTelephone> changeTelephone = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneInstallRequests = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneChangeLocations = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneChangeName = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneTakePossessions = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneChangeNos = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneChangeAddresses = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneCutAndEstablishes = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneSpecialServices = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneTranslationOpticalCabinetToNormalConncetions = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneRefundDeposits = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneSpecialWires = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneVacateSpecialWires = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneChangeLocationSpecialWires = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneCenterToCenterTranslations = new List<ChangeTelephone>();
                    List<ChangeTelephone> changeTelephoneSwapTelephones = new List<ChangeTelephone>();
                    List<ChangeTelephone> zeroStatus = new List<ChangeTelephone>();
                    List<ChangeTelephone> titleIn118 = new List<ChangeTelephone>();
                    List<ChangeTelephone> E1 = new List<ChangeTelephone>();
                    List<ChangeTelephone> VacateE1s = new List<ChangeTelephone>();


                    #region InstallRequest
                    changeTelephoneInstallRequests = context.InstallRequests
                                                            .Where(t => (t.Request.EndDate.HasValue) &&
                                                                        (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                        (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                        (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                        (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                        (cusstomerType.Count() == 0 || cusstomerType.Contains(t.TelephoneType)) &&
                                                                        (cusstomerGroup.Count() == 0 || cusstomerGroup.Contains((int)t.TelephoneTypeGroup)) &&
                                                                        (zeroBlock.Count() == 0 || zeroBlock.Contains((int)t.ClassTelephone)) &&
                                                                        (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                        (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )
                                                                   )
                                                            .Select(t => new ChangeTelephone
                                                                        {
                                                                            RequestID = t.Request.ID,
                                                                            RequestTypeName = t.Request.RequestType.Title,
                                                                            TelephoneNo = (long)t.Request.TelephoneNo,
                                                                            CenterName = t.Request.Center.CenterName,
                                                                            CityName = t.Request.Center.Region.City.Name,
                                                                            CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                                            CorrespondenceAddress = t.Address1.AddressContent,
                                                                            InstallAddress = t.Address.AddressContent,
                                                                            InstallAddressPostalCode = t.Address.PostalCode,
                                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                                            FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                                            LastName = t.Request.Customer.LastName,
                                                                            NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                                            PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),
                                                                            PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                            TelephoneType = t.TelephoneType,
                                                                            TelephoneTypeGroup = t.TelephoneTypeGroup,
                                                                            TelephoneTypeTitle = t.CustomerType.Title,
                                                                            TelephoneTypeGroupTitle = t.CustomerGroup.Title,
                                                                            ClassTelephoneTitle = Helpers.GetEnumDescription(t.ClassTelephone, typeof(DB.ClassTelephone)),
                                                                            RequestType = t.Request.RequestTypeID,
                                                                            IsOutBound = t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound,
                                                                            RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                                {
                                                                                                                                    AmountSum = t2.AmountSum,
                                                                                                                                    BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                    Cost = t2.Cost,
                                                                                                                                    FicheDate = t2.FicheDate,
                                                                                                                                    FicheNunmber = t2.FicheNunmber,
                                                                                                                                    IsKickedBack = t2.IsKickedBack,
                                                                                                                                    IsPaid = t2.IsPaid,
                                                                                                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                    PaymentDate = t2.PaymentDate,
                                                                                                                                    RequestID = t2.RequestID,

                                                                                                                                }
                                                                                                                           )
                                                                                                                   .ToList()
                                                                        }
                                                                 )
                                                           .ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneInstallRequests).ToList();

                    #endregion InstallRequest

                    #region TakePossessions
                    changeTelephoneTakePossessions = context.TakePossessions
                                                            .Where(t => (t.Request.EndDate.HasValue) &&
                                                                        (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                        (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                        (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                        (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                        (causeOfTakePossession.Count() == 0 || causeOfTakePossession.Contains((int)t.CauseOfTakePossessionID)) &&
                                                                        (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                        (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )
                                                                   )
                                                            .Select(t => new ChangeTelephone
                                                                        {
                                                                            RequestID = t.Request.ID,
                                                                            RequestTypeName = t.Request.RequestType.Title,
                                                                            TelephoneNo = (long)t.Request.TelephoneNo,
                                                                            PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                            CenterName = t.Request.Center.CenterName,
                                                                            CityName = t.Request.Center.Region.City.Name,
                                                                            CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                                            CorrespondenceAddress = t.Address1.AddressContent,
                                                                            InstallAddress = t.Address.AddressContent,
                                                                            InstallAddressPostalCode = t.Address.PostalCode,
                                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                                            FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                                            LastName = t.Request.Customer.LastName,
                                                                            NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                                            PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),
                                                                            CauseOfTakePossession = t.CauseOfTakePossessionID,
                                                                            CauseOfTakePossessionTitle = t.CauseOfTakePossession.Name,
                                                                            RequestType = t.Request.RequestTypeID,
                                                                            IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,
                                                                            RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                                {
                                                                                                                                    AmountSum = t2.AmountSum,
                                                                                                                                    BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                    Cost = t2.Cost,
                                                                                                                                    FicheDate = t2.FicheDate,
                                                                                                                                    FicheNunmber = t2.FicheNunmber,
                                                                                                                                    IsKickedBack = t2.IsKickedBack,
                                                                                                                                    IsPaid = t2.IsPaid,
                                                                                                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                    PaymentDate = t2.PaymentDate,
                                                                                                                                    RequestID = t2.RequestID
                                                                                                                                }
                                                                                                                            )
                                                                                                                     .ToList(),
                                                                        }
                                                                   )
                                                             .ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneTakePossessions).ToList();

                    #endregion TakePossessions

                    #region ChangeLocation
                    changeTelephoneChangeLocations = context.ChangeLocations
                                                            .Where(t => (t.Request.EndDate.HasValue) &&
                                                                        (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                        (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                        (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                        (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                        (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                        (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID)) &&
                                                                        (!isChangeName.HasValue || ((isChangeName == true ? (t.NewCustomerID != null) : (t.NewCustomerID == null))))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )
                                                                    )
                                                            .Select(t => new ChangeTelephone
                                                                        {
                                                                            RequestID = t.Request.ID,
                                                                            RequestTypeName = t.Request.RequestType.Title,
                                                                            TelephoneNo = t.OldTelephone ?? 0,
                                                                            PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                            NewTelephoneNo = t.NewTelephone,
                                                                            CenterName = t.Request.Center.CenterName,
                                                                            CityName = t.Request.Center.Region.City.Name,
                                                                            IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                                            InstallAddress = t.Address1.AddressContent,
                                                                            InstallAddressPostalCode = t.Address1.PostalCode,
                                                                            CorrespondenceAddressPostalCode = t.Address3.PostalCode,
                                                                            CorrespondenceAddress = t.Address3.AddressContent,


                                                                            NewCorrespondenceAddressPostalCode = t.Address.AddressContent,
                                                                            NewCorrespondenceAddress = t.Address.AddressContent,
                                                                            NewInstallAddress = t.Address2.AddressContent,
                                                                            NewInstallAddressPostalCode = t.Address2.PostalCode,

                                                                            NewFirstNameOrTitle = t.Customer.FirstNameOrTitle,
                                                                            NewLastName = t.Customer.LastName,
                                                                            NewNationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                                                            NewPersonType = Helpers.GetEnumDescription(t.Customer.PersonType, typeof(DB.PersonType)),

                                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                                            FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                                            LastName = t.Request.Customer.LastName,
                                                                            NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                                            PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),
                                                                            RequestType = t.Request.RequestTypeID,
                                                                            RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                                {
                                                                                                                                    AmountSum = t2.AmountSum,
                                                                                                                                    BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                    Cost = t2.Cost,
                                                                                                                                    FicheDate = t2.FicheDate,
                                                                                                                                    FicheNunmber = t2.FicheNunmber,
                                                                                                                                    IsKickedBack = t2.IsKickedBack,
                                                                                                                                    IsPaid = t2.IsPaid,
                                                                                                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                    PaymentDate = t2.PaymentDate,
                                                                                                                                    RequestID = t2.RequestID,

                                                                                                                                }
                                                                                                                            )
                                                                                                                     .ToList()
                                                                        }
                                                            )
                                                       .ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeLocations).ToList();
                    #endregion ChangeLocation

                    #region ChangeName

                    changeTelephoneChangeName = context.ChangeNames
                                                       .Where(t => (t.Request.EndDate.HasValue) &&
                                                                    (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                    (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                    (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )
                                                              )
                                                        .Select(t => new ChangeTelephone
                                                                     {
                                                                         RequestID = t.Request.ID,
                                                                         RequestTypeName = t.Request.RequestType.Title,
                                                                         TelephoneNo = (long)t.Request.TelephoneNo,
                                                                         PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                         CenterName = t.Request.Center.CenterName,
                                                                         CityName = t.Request.Center.Region.City.Name,
                                                                         IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                                         FirstNameOrTitle = t.Customer1.FirstNameOrTitle,
                                                                         LastName = t.Customer1.LastName,
                                                                         NationalCodeOrRecordNo = t.Customer1.NationalCodeOrRecordNo,
                                                                         PersonType = Helpers.GetEnumDescription(t.Customer1.PersonType, typeof(DB.PersonType)),

                                                                         NewFirstNameOrTitle = t.Customer.FirstNameOrTitle,
                                                                         NewLastName = t.Customer.LastName,
                                                                         NewNationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                                                         NewPersonType = Helpers.GetEnumDescription(t.Customer.PersonType, typeof(DB.PersonType)),

                                                                         InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                         EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),

                                                                         RequestType = t.Request.RequestTypeID,
                                                                         RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                            {
                                                                                                                                AmountSum = t2.AmountSum,
                                                                                                                                BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                Cost = t2.Cost,
                                                                                                                                FicheDate = t2.FicheDate,
                                                                                                                                FicheNunmber = t2.FicheNunmber,
                                                                                                                                IsKickedBack = t2.IsKickedBack,
                                                                                                                                IsPaid = t2.IsPaid,
                                                                                                                                OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                PaymentDate = t2.PaymentDate,
                                                                                                                                RequestID = t2.RequestID,

                                                                                                                            }
                                                                                                                        )
                                                                                                                 .ToList(),
                                                                     }
                                                            )
                                                      .ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeName).ToList();

                    #endregion ChangeName

                    #region ChangeNos
                    changeTelephoneChangeNos = context.ChangeNos
                                                      .Where(t => (t.Request.EndDate.HasValue) &&
                                                                    (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                    (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                    (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                    (causeOfChangeNo.Count() == 0 || causeOfChangeNo.Contains((int)t.CauseOfChangeNoID)) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                             )
                                                      .Select(t => new ChangeTelephone
                                                                   {
                                                                       RequestID = t.Request.ID,
                                                                       RequestTypeName = t.Request.RequestType.Title,
                                                                       TelephoneNo = (long)t.OldTelephoneNo,
                                                                       PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                       NewTelephoneNo = t.NewTelephoneNo,
                                                                       CenterName = t.Request.Center.CenterName,
                                                                       CityName = t.Request.Center.Region.City.Name,
                                                                       IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                                       InstallAddress = t.Address.AddressContent,
                                                                       InstallAddressPostalCode = t.Address.PostalCode,
                                                                       CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                                       CorrespondenceAddress = t.Address1.AddressContent,


                                                                       FirstNameOrTitle = t.Customer.FirstNameOrTitle,
                                                                       LastName = t.Customer.LastName,
                                                                       NationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                                                       PersonType = Helpers.GetEnumDescription(t.Customer.PersonType, typeof(DB.PersonType)),
                                                                       CauseOfChangeNo = t.CauseOfChangeNoID,
                                                                       CauseOfChangeNoTitle = t.CauseOfChangeNo.Name,
                                                                       InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                       EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                                       RequestType = t.Request.RequestTypeID,
                                                                       RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                            {
                                                                                                                                AmountSum = t2.AmountSum,
                                                                                                                                BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                Cost = t2.Cost,
                                                                                                                                FicheDate = t2.FicheDate,
                                                                                                                                FicheNunmber = t2.FicheNunmber,
                                                                                                                                IsKickedBack = t2.IsKickedBack,
                                                                                                                                IsPaid = t2.IsPaid,
                                                                                                                                OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                PaymentDate = t2.PaymentDate,
                                                                                                                                RequestID = t2.RequestID,

                                                                                                                            }
                                                                                                                     )
                                                                                                                .ToList(),
                                                                   }
                                                            )
                                                     .ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeNos).ToList();
                    #endregion ChangeNos

                    #region SwapTelephones
                    changeTelephoneSwapTelephones = context.SwapTelephones
                                                           .Where(t => (t.Request.EndDate.HasValue) &&
                                                                        (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                        (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                        (telephoneNo == -1 || (t.FromTelephoneNo == telephoneNo || t.ToTelephoneNo == telephoneNo)) &&
                                                                        (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                        (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                        (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                  )
                                                            .Select(t => new ChangeTelephone
                                                                        {
                                                                            RequestID = t.Request.ID,
                                                                            RequestTypeName = t.Request.RequestType.Title,
                                                                            TelephoneNo = (long)t.FromTelephoneNo,
                                                                            PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                            NewTelephoneNo = t.ToTelephoneNo,
                                                                            CenterName = t.Request.Center.CenterName,
                                                                            CityName = t.Request.Center.Region.City.Name,
                                                                            IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                                            FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                                            LastName = t.Request.Customer.LastName,
                                                                            NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                                            PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                                            RequestType = t.Request.RequestTypeID,
                                                                            RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                                {
                                                                                                                                    AmountSum = t2.AmountSum,
                                                                                                                                    BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                    Cost = t2.Cost,
                                                                                                                                    FicheDate = t2.FicheDate,
                                                                                                                                    FicheNunmber = t2.FicheNunmber,
                                                                                                                                    IsKickedBack = t2.IsKickedBack,
                                                                                                                                    IsPaid = t2.IsPaid,
                                                                                                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                    PaymentDate = t2.PaymentDate,
                                                                                                                                    RequestID = t2.RequestID,

                                                                                                                                }
                                                                                                                          )
                                                                                                                   .ToList(),
                                                                        }
                                                                )
                                                         .ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneSwapTelephones).ToList();
                    #endregion SwapTelephones

                    #region ChangeAddresses
                    changeTelephoneChangeAddresses = context.ChangeAddresses
                                                            .Where(t => (t.Request.EndDate.HasValue) &&
                                                                       (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                       (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                       (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                       (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                       (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                       (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                   )
                                                            .Select(t => new ChangeTelephone
                                                                        {
                                                                            RequestID = t.Request.ID,
                                                                            RequestTypeName = t.Request.RequestType.Title,
                                                                            TelephoneNo = (long)t.Request.TelephoneNo,
                                                                            PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                            CenterName = t.Request.Center.CenterName,
                                                                            CityName = t.Request.Center.Region.City.Name,
                                                                            IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                                            FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                                            LastName = t.Request.Customer.LastName,
                                                                            NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                                            PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                                            InstallAddress = t.Address1.AddressContent,
                                                                            InstallAddressPostalCode = t.Address1.PostalCode,
                                                                            CorrespondenceAddressPostalCode = t.Address.PostalCode,
                                                                            CorrespondenceAddress = t.Address.AddressContent,

                                                                            NewCorrespondenceAddressPostalCode = t.Address2.AddressContent,
                                                                            NewCorrespondenceAddress = t.Address2.AddressContent,
                                                                            NewInstallAddress = t.Address3.AddressContent,
                                                                            NewInstallAddressPostalCode = t.Address3.PostalCode,

                                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),

                                                                            RequestType = t.Request.RequestTypeID,
                                                                            RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                                {
                                                                                                                                    AmountSum = t2.AmountSum,
                                                                                                                                    BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                    Cost = t2.Cost,
                                                                                                                                    FicheDate = t2.FicheDate,
                                                                                                                                    FicheNunmber = t2.FicheNunmber,
                                                                                                                                    IsKickedBack = t2.IsKickedBack,
                                                                                                                                    IsPaid = t2.IsPaid,
                                                                                                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                    PaymentDate = t2.PaymentDate,
                                                                                                                                    RequestID = t2.RequestID,

                                                                                                                                }
                                                                                                                            )
                                                                                                                    .ToList(),
                                                                        }
                                                                )
                                                        .ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeAddresses).ToList();
                    #endregion ChangeAddresses

                    #region CutAndEstablishes
                    changeTelephoneCutAndEstablishes = context.CutAndEstablishes
                                                              .Where(t => (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (causeOfCut.Count() == 0 || causeOfCut.Contains((int)t.CutType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                                               .Select(t => new ChangeTelephone
                                                                            {
                                                                                RequestID = t.Request.ID,
                                                                                RequestTypeName = t.Request.RequestType.Title,
                                                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                                                CenterName = t.Request.Center.CenterName,
                                                                                CityName = t.Request.Center.Region.City.Name,
                                                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                                                LastName = t.Request.Customer.LastName,
                                                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),

                                                                                CauseOfCut = t.CauseOfCut.ID,
                                                                                CauseOfCutTitle = t.CauseOfCut.Name,

                                                                                RequestType = t.Request.RequestTypeID,
                                                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                                                                                                    {
                                                                                                                                        AmountSum = t2.AmountSum,
                                                                                                                                        BaseCostTitle = t2.BaseCost.Title,
                                                                                                                                        Cost = t2.Cost,
                                                                                                                                        FicheDate = t2.FicheDate,
                                                                                                                                        FicheNunmber = t2.FicheNunmber,
                                                                                                                                        IsKickedBack = t2.IsKickedBack,
                                                                                                                                        IsPaid = t2.IsPaid,
                                                                                                                                        OtherCostTitle = t2.OtherCost.CostTitle,
                                                                                                                                        PaymentDate = t2.PaymentDate,
                                                                                                                                        RequestID = t2.RequestID,

                                                                                                                                    }
                                                                                                                              )
                                                                                                                     .ToList(),
                                                                            }
                                                               ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneCutAndEstablishes).ToList();
                    #endregion CutAndEstablishes

                    #region SpecialServices
                    changeTelephoneSpecialServices = context.SpecialServices
                                                            .Where(t => (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,
                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),

                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneSpecialServices).ToList();
                    #endregion SpecialServices

                    #region RefundDeposits
                    changeTelephoneRefundDeposits = context.RefundDeposits.Where(t =>
                                                                         (t.Request.EndDate.HasValue) &&
                                                                         (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                         (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                         (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                         (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                         (causeOfRefundDeposit.Count() == 0 || causeOfRefundDeposit.Contains((int)t.CauseOfRefundDepositID)) &&
                                                                         (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                         (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                   )
                                           .Select(t => new ChangeTelephone
                                           {
                                               RequestID = t.Request.ID,
                                               RequestTypeName = t.Request.RequestType.Title,
                                               TelephoneNo = (long)t.Request.TelephoneNo,
                                               PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                               CenterName = t.Request.Center.CenterName,
                                               CityName = t.Request.Center.Region.City.Name,
                                               IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,
                                               CorrespondenceAddressPostalCode = t.Address.PostalCode,
                                               CorrespondenceAddress = t.Address.AddressContent,
                                               InstallAddress = t.Address.AddressContent,
                                               InstallAddressPostalCode = t.Address.PostalCode,
                                               InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                               EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                               FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                               LastName = t.Request.Customer.LastName,
                                               NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                               CauseOfRefundDeposit = t.CauseOfRefundDepositID,
                                               CauseOfRefundDepositTitle = t.CauseOfRefundDeposit.Name,
                                               PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),
                                               RequestType = t.Request.RequestTypeID,
                                               RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                               {
                                                   AmountSum = t2.AmountSum,
                                                   BaseCostTitle = t2.BaseCost.Title,
                                                   Cost = t2.Cost,
                                                   FicheDate = t2.FicheDate,
                                                   FicheNunmber = t2.FicheNunmber,
                                                   IsKickedBack = t2.IsKickedBack,
                                                   IsPaid = t2.IsPaid,
                                                   OtherCostTitle = t2.OtherCost.CostTitle,
                                                   PaymentDate = t2.PaymentDate,
                                                   RequestID = t2.RequestID,

                                               }).ToList(),
                                           }
                                           ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneRefundDeposits).ToList();

                    #endregion RefundDeposits

                    #region changeTelephoneTranslationOpticalCabinetToNormalConncetions
                    changeTelephoneTranslationOpticalCabinetToNormalConncetions = context.TranslationOpticalCabinetToNormalConncetions
                                                                          .Where(t =>
                                                                          (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.FromTelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                NewTelephoneNo = t.ToTelephoneNo,
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,


                                                FirstNameOrTitle = t.Customer.FirstNameOrTitle,
                                                LastName = t.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Customer.PersonType, typeof(DB.PersonType)),

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneTranslationOpticalCabinetToNormalConncetions).ToList();
                    #endregion changeTelephoneTranslationOpticalCabinetToNormalConncetions

                    #region changeTelephoneCenterToCenterTranslations
                    changeTelephoneCenterToCenterTranslations = context.CenterToCenterTranslationTelephones.Where(t =>
                                                                          (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.TelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                NewTelephoneNo = t.NewTelephoneNo,
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneCenterToCenterTranslations).ToList();
                    #endregion changeTelephoneCenterToCenterTranslations

                    #region SpecialWires
                    changeTelephoneSpecialWires = context.SpecialWires.Where(t =>
                                                                          (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,


                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneSpecialWires).ToList();
                    #endregion changeTelephoneTranslationOpticalCabinetToNormalConncetions

                    #region ChangeLocationSpecialWires
                    changeTelephoneChangeLocationSpecialWires = context.ChangeLocationSpecialWires
                                                                          .Where(t =>
                                                                          (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,

                                                NewInstallAddress = t.Address1.AddressContent,
                                                NewInstallAddressPostalCode = t.Address1.PostalCode,


                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeLocationSpecialWires).ToList();
                    #endregion ChangeLocationSpecialWires

                    #region VacateSpecialWires
                    changeTelephoneVacateSpecialWires = context.VacateSpecialWires.Where(t =>
                                                                          (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                           (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,


                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneVacateSpecialWires).ToList();
                    #endregion changeTelephoneTranslationOpticalCabinetToNormalConncetions

                    #region zeroStatus
                    zeroStatus = context.ZeroStatus.Where(t => (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (zeroBlock.Count() == 0 || zeroBlock.Contains((int)t.ClassTelephone)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                RequestType = t.Request.RequestTypeID,
                                                ClassTelephoneTitle = Helpers.GetEnumDescription(t.ClassTelephone, typeof(DB.ClassTelephone)),
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(zeroStatus).ToList();
                    #endregion zeroStatus

                    #region TitleIn118s
                    titleIn118 = context.TitleIn118s.Where(t => (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )

                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Request.Telephone.SwitchPrecode.PreCodeType),
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                IsOutBound = t.Request.VisitAddresses.OrderByDescending(va => va.ID).Take(1).SingleOrDefault().IsOutBound,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),

                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(titleIn118).ToList();
                    #endregion zeroStatus

                    #region E1
                    E1 = context.E1s.Where(t => (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                           (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                           (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )
                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = t.Request.TelephoneNo ?? 0,
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,
                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),
                                                TelephoneTypeGroup = t.TelephoneTypeGroup,
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(E1).ToList();
                    #endregion zeroStatus

                    #region VacateE1s
                    VacateE1s = context.VacateE1s.Where(t => (t.Request.EndDate.HasValue) &&
                                                                          (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                          (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                          (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                          (telephoneNo == -1 || t.Request.TelephoneNo == telephoneNo) &&
                                                                          (preCodeType.Count == 0 || preCodeType.Contains(t.Request.Telephone.SwitchPrecode.PreCodeType)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                          &&
                                                                          (
                                                                            !isOutBound.HasValue || ((isOutBound == true) ?
                                                                            (t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == true) :
                                                                            (!t.Request.VisitAddresses.Any() || t.Request.VisitAddresses.OrderByDescending(t2 => t2.ID).Take(1).SingleOrDefault().IsOutBound == false))
                                                                          )
                                                                    )
                                            .Select(t => new ChangeTelephone
                                            {
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,
                                                CityName = t.Request.Center.Region.City.Name,
                                                InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = Helpers.GetEnumDescription(t.Request.Customer.PersonType, typeof(DB.PersonType)),
                                                RequestType = t.Request.RequestTypeID,
                                                RequestCost = t.Request.RequestPayments.Select(t2 => new RequestCost
                                                {
                                                    AmountSum = t2.AmountSum,
                                                    BaseCostTitle = t2.BaseCost.Title,
                                                    Cost = t2.Cost,
                                                    FicheDate = t2.FicheDate,
                                                    FicheNunmber = t2.FicheNunmber,
                                                    IsKickedBack = t2.IsKickedBack,
                                                    IsPaid = t2.IsPaid,
                                                    OtherCostTitle = t2.OtherCost.CostTitle,
                                                    PaymentDate = t2.PaymentDate,
                                                    RequestID = t2.RequestID,

                                                }).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(VacateE1s).ToList();
                    #endregion zeroStatus

                    count = changeTelephone.Count();

                    if (pageSize == 0)
                    {
                        return changeTelephone.ToList();
                    }
                    else
                    {
                        return changeTelephone.Skip(startRowIndex).Take(pageSize).ToList();
                    }


                }
            }
            catch
            {
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<RequestStatistics> GetRequestStatistics(DateTime? fromDateTime, DateTime? toDateTime, List<int> cites, List<int> centers, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime? oneDayAddedToOriginalToDate = null;
                if (toDateTime.HasValue)
                {
                    oneDayAddedToOriginalToDate = toDateTime.Value.AddDays(1);
                }

                List<RequestStatistics> result = new List<RequestStatistics>();

                IQueryable<RequestStatistics> query = context.Centers.Where(t =>
                                                                                (cites.Count == 0 || cites.Contains(t.Region.CityID)) &&
                                                                                (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.ID) : centers.Contains(t.ID))
                                                                            )
                                                                    .Select(t => new RequestStatistics
                                                                           {
                                                                               CenterID = t.ID,
                                                                               Center = t.CenterName,
                                                                               CityName = t.Region.City.Name,
                                                                               FromDate = fromDateTime.ToPersian(Date.DateStringType.Short),
                                                                               ToDate = toDateTime.ToPersian(Date.DateStringType.Short),

                                                                               //**********************************************************************************************************************************************
                                                                               Dayri = context.Requests.Where(t2 =>
                                                                                                                    (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.Dayri) &&
                                                                                                                    (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                    (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                              )
                                                                                                       .Count(), //Dayeri count
                                                                               //**********************************************************************************************************************************************
                                                                               ChangeAddress = context.Requests.Where(t2 =>
                                                                                                                            (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.ChangeAddress) &&
                                                                                                                            (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                            (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                      )
                                                                                                                .Count(), //ChangeAddress count
                                                                               //**********************************************************************************************************************************************
                                                                               ChangeLocationCenterInside = context.Requests.Where(t2 =>
                                                                                                                                        (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside) &&
                                                                                                                                        (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                                        (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                                   )
                                                                                                                            .Count(), //ChangeLocationCenterInside count
                                                                               //**********************************************************************************************************************************************
                                                                               ChangeLocationCenterToCenter = context.Requests.Where(t2 =>
                                                                                                                                            (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterToCenter) &&
                                                                                                                                            (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                                            (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                                     )
                                                                                                                              .Count(), //ChangeLocationCenterToCenter  count
                                                                               //**********************************************************************************************************************************************
                                                                               ChangeNo = context.Requests.Where(t2 =>
                                                                                                                        (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.ChangeNo) &&
                                                                                                                        (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                        (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                 )
                                                                                                          .Count(), //ChangeNo count
                                                                               //**********************************************************************************************************************************************
                                                                               ChangeTitleIn118 = context.Requests.Where(t2 =>
                                                                                                                                (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.ChangeTitleIn118) &&
                                                                                                                                (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                                (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                        )
                                                                                                                  .Count(), //ChangeTitleIn118 count
                                                                               //**********************************************************************************************************************************************
                                                                               Connect = context.Requests.Where(t2 =>
                                                                                                                        (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.Connect) &&
                                                                                                                        (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                        (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                )
                                                                                                         .Count(), //Connect count
                                                                               //**********************************************************************************************************************************************
                                                                               CutAndEstablish = context.Requests.Where(t2 =>
                                                                                                                              (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.CutAndEstablish) &&
                                                                                                                              (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                              (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                        )
                                                                                                                 .Count(), //CutAndEstablish count
                                                                               //**********************************************************************************************************************************************
                                                                               Dischargin = context.Requests.Where(t2 =>
                                                                                                                         (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.Dischargin) &&
                                                                                                                         (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                         (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                   )
                                                                                                            .Count(),//Dischargin count
                                                                               //**********************************************************************************************************************************************
                                                                               E1 = context.Requests.Where(t2 =>
                                                                                                                 (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.E1) &&
                                                                                                                 (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                 (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                           )
                                                                                                    .Count(), //E1 count
                                                                               //**********************************************************************************************************************************************
                                                                               OpenAndCloseZero = context.Requests.Where(t2 =>
                                                                                                                              (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.OpenAndCloseZero) &&
                                                                                                                              (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                              (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                        )
                                                                                                                   .Count(), //OpenAndCloseZero count
                                                                               //**********************************************************************************************************************************************
                                                                               RemoveTitleIn118 = context.Requests.Where(t2 =>
                                                                                                                               (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.RemoveTitleIn118) &&
                                                                                                                               (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                               (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                        )
                                                                                                                  .Count(), //RemoveTitleIn118 count
                                                                               //**********************************************************************************************************************************************
                                                                               SpecialService = context.Requests.Where(t2 =>
                                                                                                                            (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.SpecialService) &&
                                                                                                                            (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                            (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                       )
                                                                                                                .Count(), //SpecialService count
                                                                               //**********************************************************************************************************************************************
                                                                               SpecialWire = context.Requests.Where(t2 =>
                                                                                                                         (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.SpecialWire) &&
                                                                                                                         (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                         (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                    )
                                                                                                             .Count(), //SpecialWire count
                                                                               //**********************************************************************************************************************************************
                                                                               TranslationOpticalCabinetToNormal = context.Requests.Where(t2 =>
                                                                                                                                            t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.TranslationOpticalCabinetToNormal &&
                                                                                                                                            (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                                            (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                                          )
                                                                                                                                   .Count(), //TranslationOpticalCabinetToNormal count
                                                                               //**********************************************************************************************************************************************
                                                                               TitleIn118 = context.Requests.Where(t2 =>
                                                                                                                        (t2.CenterID == t.ID && t2.EndDate.HasValue && t2.RequestTypeID == (int)DB.RequestType.TitleIn118) &&
                                                                                                                        (!fromDateTime.HasValue || t2.EndDate >= fromDateTime) &&
                                                                                                                        (!oneDayAddedToOriginalToDate.HasValue || t2.EndDate <= oneDayAddedToOriginalToDate)
                                                                                                                   )
                                                                                                            .Count(), //TitleIn118 count
                                                                           });

                //**********************************************************************************************************************************************
                count = query.Count();
                //**********************************************************************************************************************************************

                result = query.Skip(startRowIndex)
                              .Take(pageSize)
                              .ToList();

                return result;
            }
        }


        public static List<RequestThatHasTelecomminucationServiceStatistics> SearchRequestThatHasTelecomminucationService(List<int> cities, List<int> centers, List<long> customers, int requestType, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestThatHasTelecomminucationServiceStatistics> result = new List<RequestThatHasTelecomminucationServiceStatistics>();
                var query = context.Requests
                                   .Where(re =>
                                            (context.TelecomminucationServicePayments.Select(tps => tps.RequestID).ToList().Contains(re.ID)) &&
                                            (re.EndDate.HasValue) &&
                                            (cities.Count == 0 || cities.Contains(re.Center.Region.CityID)) &&
                                            (centers.Count == 0 || centers.Contains(re.CenterID)) &&
                                            (requestType == -1 || re.RequestTypeID == requestType) &&
                                            (!fromDate.HasValue || re.EndDate >= fromDate) &&
                                            (!toDate.HasValue || re.EndDate <= toDate) &&
                                            (customers.Count == 0 || customers.Contains(re.CustomerID.Value))
                                         )
                                   .AsQueryable();
                var secondQuery = query.Select(a => new RequestThatHasTelecomminucationServiceStatistics
                                                    {
                                                        RequestID = a.ID,
                                                        CenterName = a.Center.CenterName,
                                                        RequestTypeTitle = a.RequestType.Title,
                                                        InsertDate = a.InsertDate.ToPersian(Date.DateStringType.Short),
                                                        EndDate = a.EndDate.ToPersian(Date.DateStringType.Short),
                                                        CustomerName = string.Format("{0} {1}", a.Customer.FirstNameOrTitle, a.Customer.LastName),
                                                        PersonType = Helpers.GetEnumDescription(a.Customer.PersonType, typeof(DB.PersonType))
                                                    }
                                                )
                                        .AsQueryable();
                count = query.Count();
                if (pageSize != 0)
                {
                    result = secondQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    result = secondQuery.ToList();
                }
                return result;
            }
        }
    }
}
