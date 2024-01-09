using Enterprise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CRM.Data.ServiceHost
{

    public class ServiceHostDB
    {
        public static CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneInfo GetTelephoneInfo(long TelephonNo)
        {
            try
            {
                // Properties.Settings.Default["CRMConnectionString"] = "111";
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    return context.Telephones.Where(t => t.TelephoneNo == TelephonNo)
                                      .GroupJoin(context.Buchts, tel => tel.SwitchPortID, bu => bu.SwitchPortID, (tel, bu) => new { GTele = tel, Gbucht = bu })
                                      .SelectMany(t => t.Gbucht.DefaultIfEmpty(), (gt, t) => new { telephon = gt, bucht = t })
                                      .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneInfo
                                      {
                                          IsInstalition = (t.telephon.GTele.Status == (byte)DB.TelephoneStatus.Connecting ? (byte)DB.TelephoneStatus.Connecting : (t.telephon.GTele.Status == (byte)DB.TelephoneStatus.Cut ? (byte)DB.TelephoneStatus.Cut : default(byte?))),
                                          HasADSL = context.ADSLPAPPorts.Any(t2 => t2.TelephoneNo == t.telephon.GTele.TelephoneNo),
                                          TelephoneType = (t.telephon.GTele.CustomerGroup.Title.Contains("همگانی ") == true ? t.telephon.GTele.CustomerGroupID : (t.telephon.GTele.CustomerGroup.Title.Contains("اعتباری") == true ? t.telephon.GTele.CustomerGroupID : default(int?))),
                                          TechType = (t.bucht.PCMPortID.HasValue ? (int)DB.CRMServiceTechTelephoneType.PCM :
                                                     (t.telephon.GTele.UsageType == (int)DB.TelephoneUsageType.GSM ? (int)DB.CRMServiceTechTelephoneType.GSM :
                                                     (t.bucht.CabinetInput.Cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet ? (int)DB.CRMServiceTechTelephoneType.OpticalCabinet :
                                                     (t.bucht.CabinetInput.Cabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL ? (int)DB.CRMServiceTechTelephoneType.Wll : (int)DB.CRMServiceTechTelephoneType.Normal))))


                                      }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetTelephoneInfo throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.RequestType> GetRequestType()
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    return context.RequestTypes.Where(t => t.Abonman == true).Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestType { Code = t.ID, Title = t.Title }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetRequestType throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.CenterInfo> GetCenter()
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    return context.Centers.Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.CenterInfo { Code = t.ID, CityCode = t.Region.CityID, Title = t.CenterName }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetCenter throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.CityInfo> GetCity()
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    return context.Cities.Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.CityInfo { Code = t.ID, Title = t.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetCity throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.TechTelephoneType> GetTechTelephoneType()
        {
            try
            {
                List<CRM.Data.ServiceHost.ServiceHostCustomClass.TechTelephoneType> list = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.TechTelephoneType>();

                System.Reflection.FieldInfo[] fieldInfos = typeof(CRM.Data.DB.CRMServiceTechTelephoneType).GetFields().Where(t => t.IsLiteral).ToArray();

                foreach (System.Reflection.FieldInfo item in fieldInfos)
                    list.Add(new CRM.Data.ServiceHost.ServiceHostCustomClass.TechTelephoneType()
                    {
                        Code = Convert.ToInt32(item.GetRawConstantValue()),
                        Title = (item.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as System.ComponentModel.DescriptionAttribute).Description,
                    }
                );

                return list;
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetTechTelephoneType throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneType> GetTelephoneType()
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    return context.CustomerTypes.Where(t => t.IsShow == true).Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneType { Code = t.ID, Title = t.Title }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetTelephoneType throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneGroupType> GetTelephoneGroupType()
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    return context.CustomerGroups.Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneGroupType { Code = t.ID, TelephoneType = t.CustomerTypeID, Title = t.Title }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetTelephoneGroupType throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.CauseOfCutInfo> GetCauseOfCut()
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    return context.CauseOfCuts.Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.CauseOfCutInfo { Code = t.ID, Title = t.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetCauseOfCut throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> GetChangeTelephone(DateTime FromDateTime, DateTime ToDateTime, List<int> centerCode, List<int> requestTypeIDs)
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                FromDateTime = FromDateTime.Date;
                ToDateTime = ToDateTime.Date.AddDays(1);
                using (MainDataContext context = new MainDataContext(connectionString))
                {
                    context.CommandTimeout = 0;
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephone = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneInstallRequests = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneChangeLocations = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneChangeName = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneTakePossessions = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneChangeNos = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneChangeAddresses = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneCutAndEstablishes = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneSpecialServices = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneTranslationOpticalCabinetToNormalConncetions = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneRefundDeposits = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneSpecialWires = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneChangeLocationSpecialWires = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneVacateSpecialWires = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneCenterToCenterTranslations = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> changeTelephoneSwapTelephones = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> zeroStatus = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> titleIn118 = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> E1 = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> VacateE1s = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> EditCustomerList = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> EditAddressList = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
                    List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> EditTelephoneList = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();

                    #region InstallRequest
                    changeTelephoneInstallRequests = context.InstallRequests.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                         t.Request.EndDate <= ToDateTime &&
                                                                         (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                         (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                   )
                                           .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                           {
                                               ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                               RequestID = t.Request.ID,
                                               RequestTypeName = t.Request.RequestType.Title,
                                               TelephoneNo = (long)t.Request.TelephoneNo,
                                               CenterName = t.Request.Center.CenterName,
                                               CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                               CorrespondenceAddress = t.Address1.AddressContent,
                                               InstallAddress = t.Address.AddressContent,
                                               InstallAddressPostalCode = t.Address.PostalCode,
                                               InsertDate = (DateTime)t.Request.InsertDate,
                                               EndDate = (DateTime)t.Request.EndDate,
                                               FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                               LastName = t.Request.Customer.LastName,
                                               NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                               FatherName = t.Request.Customer.FatherName,
                                               MobileNo = t.Request.Customer.MobileNo,
                                               BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                               PersonType = t.Request.Customer.PersonType,
                                               TelephoneType = t.TelephoneType,
                                               TelephoneTypeGroup = t.TelephoneTypeGroup,
                                               RequestType = t.Request.RequestTypeID,
                                               PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                               IsOutBound = t.Request.VisitAddresses != null ? t.Request.VisitAddresses.OrderByDescending(t2 => t2.InsertDate).Take(1).SingleOrDefault().IsOutBound : false,
                                               //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost 
                                               //              {
                                               //                  AmountSum = t2.AmountSum,
                                               //                  BaseCostTitle = t2.BaseCost.Title,
                                               //                  Cost = t2.Cost,
                                               //                  FicheDate = t2.FicheDate,
                                               //                  FicheNunmber = t2.FicheNunmber,
                                               //                  IsKickedBack = t2.IsKickedBack,
                                               //                  IsPaid = t2.IsPaid,
                                               //                  OtherCostTitle = t2.OtherCost.CostTitle,
                                               //                  PaymentDate = t2.PaymentDate,
                                               //                  RequestID = t2.RequestID,

                                               //              }).ToList(),
                                           }
                                           ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneInstallRequests).ToList();

                    #endregion InstallRequest

                    #region TakePossessions
                    changeTelephoneTakePossessions = context.TakePossessions.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                         t.Request.EndDate <= ToDateTime &&
                                                                         (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                         (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                   )
                                           .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                           {
                                               ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                               RequestID = t.Request.ID,
                                               RequestTypeName = t.Request.RequestType.Title,
                                               TelephoneNo = (long)t.Request.TelephoneNo,
                                               CenterName = t.Request.Center.CenterName,
                                               CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                               CorrespondenceAddress = t.Address1.AddressContent,
                                               InstallAddress = t.Address.AddressContent,
                                               InstallAddressPostalCode = t.Address.PostalCode,
                                               InsertDate = (DateTime)t.Request.InsertDate,
                                               EndDate = (DateTime)t.Request.EndDate,
                                               FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                               LastName = t.Request.Customer.LastName,
                                               NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                               BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                               FatherName = t.Request.Customer.FatherName,
                                               MobileNo = t.Request.Customer.MobileNo,
                                               PersonType = t.Request.Customer.PersonType,
                                               RequestType = t.Request.RequestTypeID,
                                               PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                               //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                               //{
                                               //    AmountSum = t2.AmountSum,
                                               //    BaseCostTitle = t2.BaseCost.Title,
                                               //    Cost = t2.Cost,
                                               //    FicheDate = t2.FicheDate,
                                               //    FicheNunmber = t2.FicheNunmber,
                                               //    IsKickedBack = t2.IsKickedBack,
                                               //    IsPaid = t2.IsPaid,
                                               //    OtherCostTitle = t2.OtherCost.CostTitle,
                                               //    PaymentDate = t2.PaymentDate,
                                               //    RequestID = t2.RequestID,

                                               //}).ToList(),
                                           }
                                           ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneTakePossessions).ToList();

                    #endregion TakePossessions

                    #region ChangeLocation
                    changeTelephoneChangeLocations = context.ChangeLocations.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                NewTelephoneNo = t.NewTelephone,
                                                CenterName = t.Request.Center.CenterName,

                                                InstallAddress = t.Address1.AddressContent,
                                                InstallAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address3.PostalCode,
                                                CorrespondenceAddress = t.Address3.AddressContent,


                                                NewCorrespondenceAddressPostalCode = t.Address.PostalCode,
                                                NewCorrespondenceAddress = t.Address.AddressContent,
                                                NewInstallAddress = t.Address2.AddressContent,
                                                NewInstallAddressPostalCode = t.Address2.PostalCode,

                                                NewFirstNameOrTitle = t.Customer.FirstNameOrTitle,
                                                NewLastName = t.Customer.LastName,
                                                NewNationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                                NewPersonType = t.Customer.PersonType,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                IsOutBound = t.Request.VisitAddresses != null ? t.Request.VisitAddresses.OrderByDescending(t2 => t2.InsertDate).Take(1).SingleOrDefault().IsOutBound : false,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeLocations).ToList();
                    #endregion ChangeLocation

                    #region ChangeName

                    changeTelephoneChangeName = context.ChangeNames.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                        t.Request.EndDate <= ToDateTime &&
                                                                        (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                        (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                  )
                                          .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                          {
                                              ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                              RequestID = t.Request.ID,
                                              RequestTypeName = t.Request.RequestType.Title,
                                              TelephoneNo = (long)t.Request.TelephoneNo,
                                              CenterName = t.Request.Center.CenterName,

                                              FirstNameOrTitle = t.Customer1.FirstNameOrTitle,
                                              LastName = t.Customer1.LastName,
                                              NationalCodeOrRecordNo = t.Customer1.NationalCodeOrRecordNo,
                                              PersonType = t.Customer1.PersonType,
                                              BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                              FatherName = t.Request.Customer.FatherName,
                                              MobileNo = t.Request.Customer.MobileNo,

                                              NewFirstNameOrTitle = t.Customer.FirstNameOrTitle,
                                              NewLastName = t.Customer.LastName,
                                              NewNationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                              NewPersonType = t.Customer.PersonType,

                                              InsertDate = (DateTime)t.Request.InsertDate,
                                              EndDate = (DateTime)t.Request.EndDate,

                                              RequestType = t.Request.RequestTypeID,
                                              PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                              //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                              //{
                                              //    AmountSum = t2.AmountSum,
                                              //    BaseCostTitle = t2.BaseCost.Title,
                                              //    Cost = t2.Cost,
                                              //    FicheDate = t2.FicheDate,
                                              //    FicheNunmber = t2.FicheNunmber,
                                              //    IsKickedBack = t2.IsKickedBack,
                                              //    IsPaid = t2.IsPaid,
                                              //    OtherCostTitle = t2.OtherCost.CostTitle,
                                              //    PaymentDate = t2.PaymentDate,
                                              //    RequestID = t2.RequestID,

                                              //}).ToList(),
                                          }
                                          ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeName).ToList();

                    #endregion ChangeName

                    #region ChangeNos
                    changeTelephoneChangeNos = context.ChangeNos.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.OldTelephoneNo,
                                                NewTelephoneNo = t.NewTelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,
                                                FirstNameOrTitle = t.Customer.FirstNameOrTitle,
                                                LastName = t.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeNos).ToList();
                    #endregion ChangeNos

                    #region SwapTelephones
                    changeTelephoneSwapTelephones = context.SwapTelephones.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.FromTelephoneNo,
                                                NewTelephoneNo = t.ToTelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneSwapTelephones).ToList();
                    #endregion SwapTelephones

                    #region ChangeAddresses
                    changeTelephoneChangeAddresses = context.ChangeAddresses.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InstallAddress = t.Address1.AddressContent,
                                                InstallAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address.PostalCode,
                                                CorrespondenceAddress = t.Address.AddressContent,

                                                NewCorrespondenceAddressPostalCode = t.Address2.PostalCode,
                                                NewCorrespondenceAddress = t.Address2.AddressContent,
                                                NewInstallAddress = t.Address3.AddressContent,
                                                NewInstallAddressPostalCode = t.Address3.PostalCode,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,

                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeAddresses).ToList();
                    #endregion ChangeAddresses

                    #region CutAndEstablishes
                    changeTelephoneCutAndEstablishes = context.CutAndEstablishes.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,

                                                CauseOfCut = t.CauseOfCut.ID,
                                                CauseOfCutTitle = t.CauseOfCut.Name,

                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneCutAndEstablishes).ToList();
                    #endregion CutAndEstablishes

                    #region SpecialServices
                    changeTelephoneSpecialServices = context.SpecialServices.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,

                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneSpecialServices).ToList();
                    #endregion SpecialServices

                    #region RefundDeposits
                    changeTelephoneRefundDeposits = context.RefundDeposits.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                         t.Request.EndDate <= ToDateTime &&
                                                                         (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                         (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                   )
                                           .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                           {
                                               ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                               RequestID = t.Request.ID,
                                               RequestTypeName = t.Request.RequestType.Title,
                                               TelephoneNo = (long)t.Request.TelephoneNo,
                                               CenterName = t.Request.Center.CenterName,
                                               CorrespondenceAddressPostalCode = t.Address.PostalCode,
                                               CorrespondenceAddress = t.Address.AddressContent,
                                               InstallAddress = t.Address.AddressContent,
                                               InstallAddressPostalCode = t.Address.PostalCode,
                                               InsertDate = (DateTime)t.Request.InsertDate,
                                               EndDate = (DateTime)t.Request.EndDate,
                                               FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                               LastName = t.Request.Customer.LastName,
                                               NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                               BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                               FatherName = t.Request.Customer.FatherName,
                                               MobileNo = t.Request.Customer.MobileNo,
                                               PersonType = t.Request.Customer.PersonType,
                                               RequestType = t.Request.RequestTypeID,
                                               PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                               //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                               //{
                                               //    AmountSum = t2.AmountSum,
                                               //    BaseCostTitle = t2.BaseCost.Title,
                                               //    Cost = t2.Cost,
                                               //    FicheDate = t2.FicheDate,
                                               //    FicheNunmber = t2.FicheNunmber,
                                               //    IsKickedBack = t2.IsKickedBack,
                                               //    IsPaid = t2.IsPaid,
                                               //    OtherCostTitle = t2.OtherCost.CostTitle,
                                               //    PaymentDate = t2.PaymentDate,
                                               //    RequestID = t2.RequestID,

                                               //}).ToList(),
                                           }
                                           ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneRefundDeposits).ToList();

                    #endregion RefundDeposits

                    #region changeTelephoneTranslationOpticalCabinetToNormalConncetions
                    changeTelephoneTranslationOpticalCabinetToNormalConncetions = context.TranslationOpticalCabinetToNormalConncetions
                                                                                         .Join(context.RequestLogs, toc => toc.RequestID, rl => rl.RequestID, (toc, rl) => new { _RequestLog = rl, _TranslationOpticalCabinetToNormalConnections = toc })
                                                                                         .Where(t =>
                                                                                                     (t._TranslationOpticalCabinetToNormalConnections.Request.EndDate >= FromDateTime && t._TranslationOpticalCabinetToNormalConnections.Request.EndDate <= ToDateTime) &&
                                                                                                     (centerCode.Count() == 0 || centerCode.Contains((int)t._TranslationOpticalCabinetToNormalConnections.Request.CenterID)) &&
                                                                                                     (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t._TranslationOpticalCabinetToNormalConnections.Request.RequestTypeID)) &&
                                                                                                     (t._RequestLog.TelephoneNo == t._TranslationOpticalCabinetToNormalConnections.FromTelephoneNo)
                                                                                                )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                //ID = t.Request.RequestLogs.Where(t3 => t3.TelephoneNo == t.FromTelephoneNo).Take(1).SingleOrDefault().ID,
                                                ID = t._RequestLog.ID,
                                                RequestID = t._TranslationOpticalCabinetToNormalConnections.Request.ID,
                                                RequestTypeName = t._TranslationOpticalCabinetToNormalConnections.Request.RequestType.Title,
                                                TelephoneNo = (long)t._TranslationOpticalCabinetToNormalConnections.FromTelephoneNo,
                                                NewTelephoneNo = t._TranslationOpticalCabinetToNormalConnections.ToTelephoneNo,
                                                CenterName = t._TranslationOpticalCabinetToNormalConnections.Request.Center.CenterName,

                                                InstallAddress = t._TranslationOpticalCabinetToNormalConnections.Address.AddressContent,
                                                InstallAddressPostalCode = t._TranslationOpticalCabinetToNormalConnections.Address.PostalCode,
                                                CorrespondenceAddressPostalCode = t._TranslationOpticalCabinetToNormalConnections.Address1.PostalCode,
                                                CorrespondenceAddress = t._TranslationOpticalCabinetToNormalConnections.Address1.AddressContent,


                                                FirstNameOrTitle = t._TranslationOpticalCabinetToNormalConnections.Customer.FirstNameOrTitle,
                                                LastName = t._TranslationOpticalCabinetToNormalConnections.Customer.LastName,
                                                NationalCodeOrRecordNo = t._TranslationOpticalCabinetToNormalConnections.Customer.NationalCodeOrRecordNo,
                                                PersonType = t._TranslationOpticalCabinetToNormalConnections.Customer.PersonType,
                                                BirthDateOrRecordDate = t._TranslationOpticalCabinetToNormalConnections.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t._TranslationOpticalCabinetToNormalConnections.Request.Customer.FatherName,
                                                MobileNo = t._TranslationOpticalCabinetToNormalConnections.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t._TranslationOpticalCabinetToNormalConnections.Request.InsertDate,
                                                EndDate = (DateTime)t._TranslationOpticalCabinetToNormalConnections.Request.EndDate,
                                                RequestType = t._TranslationOpticalCabinetToNormalConnections.Request.RequestTypeID,
                                                PreCodeTypeID = t._TranslationOpticalCabinetToNormalConnections.Request.Telephone != null ? t._TranslationOpticalCabinetToNormalConnections.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneTranslationOpticalCabinetToNormalConncetions).ToList();
                    #endregion changeTelephoneTranslationOpticalCabinetToNormalConncetions

                    #region changeTelephoneCenterToCenterTranslations
                    changeTelephoneCenterToCenterTranslations = context.CenterToCenterTranslationTelephones.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.TelephoneNo,
                                                NewTelephoneNo = t.NewTelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneCenterToCenterTranslations).ToList();
                    #endregion changeTelephoneCenterToCenterTranslations

                    #region SpecialWires
                    changeTelephoneSpecialWires = context.SpecialWires.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,

                                                TelephoneType = t.CustomerTypeID,
                                                TelephoneTypeGroup = t.CustomerGroupID,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                IsOutBound = t.Request.VisitAddresses != null ? t.Request.VisitAddresses.OrderByDescending(t2 => t2.InsertDate).Take(1).SingleOrDefault().IsOutBound : false,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneSpecialWires).ToList();
                    #endregion SpecialWires

                    #region ChangeLocationSpecialWires
                    changeTelephoneChangeLocationSpecialWires = context.ChangeLocationSpecialWires.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,

                                                NewInstallAddress = t.Address1.AddressContent,
                                                NewInstallAddressPostalCode = t.Address1.PostalCode,


                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneChangeLocationSpecialWires).ToList();
                    #endregion ChangeLocationSpecialWires

                    #region VacateSpecialWires
                    changeTelephoneVacateSpecialWires = context.VacateSpecialWires.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,


                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(changeTelephoneVacateSpecialWires).ToList();
                    #endregion changeTelephoneTranslationOpticalCabinetToNormalConncetions

                    #region zeroStatus
                    zeroStatus = context.ZeroStatus.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                PersonType = t.Request.Customer.PersonType,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                ClassTelephoneTitle = Helpers.GetEnumDescription(t.ClassTelephone, typeof(DB.ClassTelephone)),
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(zeroStatus).ToList();
                    #endregion zeroStatus

                    #region TitleIn118s
                    titleIn118 = context.TitleIn118s.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,

                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,

                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(titleIn118).ToList();
                    #endregion zeroStatus

                    #region E1
                    E1 = context.E1s.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,
                                                CorrespondenceAddressPostalCode = t.Address1.PostalCode,
                                                CorrespondenceAddress = t.Address1.AddressContent,
                                                InstallAddress = t.Address.AddressContent,
                                                InstallAddressPostalCode = t.Address.PostalCode,
                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                TelephoneTypeGroup = t.TelephoneTypeGroup,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                IsOutBound = t.Request.VisitAddresses != null ? t.Request.VisitAddresses.OrderByDescending(t2 => t2.InsertDate).Take(1).SingleOrDefault().IsOutBound : false,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(E1).ToList();
                    #endregion E1

                    #region VacateE1s
                    VacateE1s = context.VacateE1s.Where(t => t.Request.EndDate >= FromDateTime &&
                                                                          t.Request.EndDate <= ToDateTime &&
                                                                          (centerCode.Count() == 0 || centerCode.Contains((int)t.Request.CenterID)) &&
                                                                          (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                                    )
                                            .Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                            {
                                                ID = t.Request.RequestLogs.Take(1).SingleOrDefault().ID,
                                                RequestID = t.Request.ID,
                                                RequestTypeName = t.Request.RequestType.Title,
                                                TelephoneNo = (long)t.Request.TelephoneNo,
                                                CenterName = t.Request.Center.CenterName,
                                                InsertDate = (DateTime)t.Request.InsertDate,
                                                EndDate = (DateTime)t.Request.EndDate,
                                                FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                LastName = t.Request.Customer.LastName,
                                                NationalCodeOrRecordNo = t.Request.Customer.NationalCodeOrRecordNo,
                                                BirthDateOrRecordDate = t.Request.Customer.BirthDateOrRecordDate,
                                                FatherName = t.Request.Customer.FatherName,
                                                MobileNo = t.Request.Customer.MobileNo,
                                                PersonType = t.Request.Customer.PersonType,
                                                RequestType = t.Request.RequestTypeID,
                                                PreCodeTypeID = t.Request.Telephone != null ? t.Request.Telephone.SwitchPrecode.PreCodeType : (byte?)null,
                                                //RequestCost = t.Request.RequestPayments.Select(t2 => new CRM.Data.ServiceHost.ServiceHostCustomClass.RequestCost
                                                //{
                                                //    AmountSum = t2.AmountSum,
                                                //    BaseCostTitle = t2.BaseCost.Title,
                                                //    Cost = t2.Cost,
                                                //    FicheDate = t2.FicheDate,
                                                //    FicheNunmber = t2.FicheNunmber,
                                                //    IsKickedBack = t2.IsKickedBack,
                                                //    IsPaid = t2.IsPaid,
                                                //    OtherCostTitle = t2.OtherCost.CostTitle,
                                                //    PaymentDate = t2.PaymentDate,
                                                //    RequestID = t2.RequestID,

                                                //}).ToList(),
                                            }
                                            ).ToList();

                    changeTelephone = changeTelephone.Union(VacateE1s).ToList();
                    #endregion VacateE1s

                    #region EditCustomer
                    if (requestTypeIDs.Contains((int)DB.RequestType.EditCustomer))
                    {
                        EditCustomerList = context.RequestLogs.Where(t => t.Date >= FromDateTime && t.Date <= ToDateTime &&
                                                                          t.RequestTypeID == (int)DB.RequestType.EditCustomer
                                                                    )
                                                .AsEnumerable().Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                                {
                                                    ID = t.ID,
                                                    RequestType = (int)DB.RequestType.EditCustomer,
                                                    RequestTypeName = Helpers.GetEnumDescription((int)DB.RequestType.EditCustomer, typeof(DB.RequestType)),
                                                    EndDate = (DateTime)t.Date,
                                                    FirstNameOrTitle = t.Description.Element("FirstNameOrTitle") != null ? t.Description.Element("FirstNameOrTitle").Value : string.Empty,
                                                    LastName = t.Description.Element("LastName") != null ? t.Description.Element("LastName").Value : string.Empty,
                                                    NationalCodeOrRecordNo = t.Description.Element("NationalCodeOrRecordNo") != null ? t.Description.Element("NationalCodeOrRecordNo").Value : string.Empty,
                                                    PersonType = t.Description.Element("PersonType") != null ? Convert.ToByte(t.Description.Element("PersonType").Value) : default(byte?),
                                                    BirthDateOrRecordDate = t.Description.Element("BirthDateOrRecordDate") != null ? Convert.ToDateTime(t.Description.Element("BirthDateOrRecordDate").Value) : default(DateTime?),
                                                    MobileNo = t.Description.Element("MobileNo") != null ? t.Description.Element("MobileNo").Value : string.Empty,
                                                    FatherName = t.Description.Element("FatherName") != null ? t.Description.Element("FatherName").Value : string.Empty,
                                                }
                                                ).ToList();

                        changeTelephone = changeTelephone.Union(EditCustomerList).ToList();
                    }
                    #endregion EditCustomer

                    #region EditAddress
                    if (requestTypeIDs.Contains((int)DB.RequestType.EditAddress))
                    {
                        EditAddressList = context.RequestLogs.Where(t => t.Date >= FromDateTime && t.Date <= ToDateTime &&
                                                                          t.RequestTypeID == (int)DB.RequestType.EditAddress
                                                                    )
                                                .AsEnumerable().Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                                {
                                                    ID = t.ID,
                                                    RequestType = (int)DB.RequestType.EditAddress,
                                                    RequestTypeName = Helpers.GetEnumDescription((int)DB.RequestType.EditAddress, typeof(DB.RequestType)),
                                                    EndDate = (DateTime)t.Date,
                                                    InstallAddress = t.Description.Element("AddressContent") != null ? t.Description.Element("AddressContent").Value : string.Empty,
                                                    InstallAddressPostalCode = t.Description.Element("PostalCode") != null ? t.Description.Element("PostalCode").Value : string.Empty,
                                                }
                                                ).ToList();

                        changeTelephone = changeTelephone.Union(EditAddressList).ToList();
                    }
                    #endregion EditAddress

                    #region EditTelephone
                    if (requestTypeIDs.Contains((int)DB.RequestType.EditTelephone))
                    {
                        EditTelephoneList = context.RequestLogs.Where(t => t.Date >= FromDateTime && t.Date <= ToDateTime &&
                                                                          t.RequestTypeID == (int)DB.RequestType.EditTelephone
                                                                    )
                                                .AsEnumerable().Select(t => new CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone
                                                {
                                                    ID = t.ID,
                                                    TelephoneNo = t.TelephoneNo ?? 0,
                                                    RequestType = (int)DB.RequestType.EditTelephone,
                                                    RequestTypeName = Helpers.GetEnumDescription((int)DB.RequestType.EditTelephone, typeof(DB.RequestType)),
                                                    EndDate = (DateTime)t.Date,
                                                    TelephoneType = t.Description.Element("CustomerTypeID") != null ? Convert.ToInt32(t.Description.Element("CustomerTypeID").Value) : default(int?),
                                                    TelephoneTypeGroup = t.Description.Element("CustomerGroupID") != null ? Convert.ToInt32(t.Description.Element("CustomerGroupID").Value) : default(int?),

                                                }
                                                ).ToList();

                        changeTelephone = changeTelephone.Union(EditTelephoneList).ToList();
                    }
                    #endregion EditTelephone

                    return changeTelephone;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetChangeTelephone throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");

            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.CashPaymentInfo> GetCashPaymentInfo(DateTime fromDate, DateTime toDate, List<int> centersCode)
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                toDate = toDate.Date.AddDays(1);
                List<CRM.Data.ServiceHost.ServiceHostCustomClass.CashPaymentInfo> result = new List<ServiceHostCustomClass.CashPaymentInfo>();
                using (MainDataContext context = new MainDataContext())
                {
                    context.CommandTimeout = 0;
                    result = context.RequestPayments
                                    .Join(context.InstallRequests, rp => rp.RequestID, ir => ir.RequestID, (rp, ir) => new { _RequestPayment = rp, _InstallRequest = ir })

                                    .Where(joinedData =>
                                                        (joinedData._RequestPayment.Request.RequestTypeID == (int)DB.RequestType.Dayri) && //درخواست دایری
                                                        (joinedData._InstallRequest.Request.EndDate.HasValue) && //درخواست به اتمام رسیده باشد
                                                        (joinedData._InstallRequest.Request.EndDate >= fromDate) &&
                                                        (joinedData._InstallRequest.Request.EndDate <= toDate) &&
                                                        (joinedData._RequestPayment.PaymentType == (byte)DB.PaymentType.Cash) && //هزینه های نقدی
                                                        (joinedData._RequestPayment.IsPaid.HasValue && joinedData._RequestPayment.IsPaid.Value) && //هزینه پرداخت شده باشد
                                                        ((joinedData._RequestPayment.IsKickedBack == null) || (joinedData._RequestPayment.IsKickedBack.HasValue && joinedData._RequestPayment.IsKickedBack.Value == false)) && //بازپرداخت نشده باشد
                                                        (joinedData._InstallRequest.MethodOfPaymentForTelephoneConnection.HasValue && joinedData._InstallRequest.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Cash) &&

                                                        //هزینه سیم کشی خارج از مرز اگرچه به صورت نقدی پرداخت میشود، متد جاری فقط هزینه اتصال تلفن را برمیگرداند
                                                        (!joinedData._RequestPayment.BaseCost.UseOutBound) && 
                                                        
                                                        (centersCode.Count == 0 || centersCode.Contains(joinedData._InstallRequest.Request.CenterID))
                                           )

                                    .Select(filteredData => new CRM.Data.ServiceHost.ServiceHostCustomClass.CashPaymentInfo
                                                            {
                                                                CityName = filteredData._InstallRequest.Request.Center.Region.City.Name,
                                                                CenterName = filteredData._InstallRequest.Request.Center.CenterName,
                                                                RequestID = filteredData._InstallRequest.RequestID,
                                                                TelephoneNo = (long)filteredData._InstallRequest.Request.TelephoneNo,
                                                                PaymentDate = (DateTime)filteredData._RequestPayment.PaymentDate,
                                                                Cost = 0 //بر اساس فکس بهینه چنانچه هزینه اتصال تلفن نقدی پرداخت شده باشد ، باید مقدار صفر در این فیلد قرار گیرد
                                                            }
                                            )

                                    .ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetCashPaymentInfo throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

        public static List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneConnectionInstallmentInfo> GetTelephoneConnectionInstallmentInfo(DateTime fromDate, DateTime toDate, List<int> centersCode)
        {
            try
            {
                string connectionString = Data.Properties.Settings.Default.ServiceHost_Kermanshah_ConnectionString;
                toDate = toDate.Date.AddDays(1);
                List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneConnectionInstallmentInfo> result = new List<ServiceHostCustomClass.TelephoneConnectionInstallmentInfo>();
                using (MainDataContext context = new MainDataContext())
                {
                    context.CommandTimeout = 0;
                    result = context.RequestPayments
                                    .Join(context.InstallRequests, rp => rp.RequestID, ir => ir.RequestID, (rp, ir) => new { _RequestPayment = rp, _InstallRequest = ir })

                                    .Join(context.TelephoneConnectionInstallments, a => a._RequestPayment.ID, tci => tci.RequestPaymentID, (joinedData, tci) => new
                                                                                                                                                                {
                                                                                                                                                                    _InstallRequest = joinedData._InstallRequest,
                                                                                                                                                                    _RequestPayment = joinedData._RequestPayment,
                                                                                                                                                                    _TelephoneConnectionInstallment = tci
                                                                                                                                                                }
                                         )

                                    .Where(joinedData =>
                                                        (joinedData._RequestPayment.Request.RequestTypeID == (int)DB.RequestType.Dayri) && //درخواست دایری
                                                        (joinedData._InstallRequest.Request.EndDate.HasValue) && //درخواست به اتمام رسیده باشد
                                                        (joinedData._InstallRequest.Request.EndDate >= fromDate) &&
                                                        (joinedData._InstallRequest.Request.EndDate <= toDate) &&
                                                        (joinedData._RequestPayment.PaymentType == (byte)DB.PaymentType.Instalment) && //هزینه های قسطی
                                                        (joinedData._InstallRequest.MethodOfPaymentForTelephoneConnection.HasValue && joinedData._InstallRequest.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Installment) &&
                                                        (centersCode.Count == 0 || centersCode.Contains(joinedData._InstallRequest.Request.CenterID))
                                           )

                                    .Select(filteredData => new CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneConnectionInstallmentInfo
                                    {
                                        CityName = filteredData._InstallRequest.Request.Center.Region.City.Name,
                                        CenterName = filteredData._InstallRequest.Request.Center.CenterName,
                                        RequestID = filteredData._InstallRequest.RequestID,
                                        TelephoneNo = (long)filteredData._InstallRequest.Request.TelephoneNo,
                                        Cost = filteredData._TelephoneConnectionInstallment.InstallmentsCount //بر اساس فکس بهینه چنانچه هزینه اتصال تلفن قسطی پرداخت شده باشد ، باید تعداد اقساط در این فیلد قرار گیرد
                                    }
                                            )

                                    .ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetTelephoneConnectionInstallmentInfo throw following exception : {0}", ex);
                throw new Exception("خطا در دریافت اطلاعات");
            }
        }

    }
}
