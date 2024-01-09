using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class SpaceAndPowerDB
    {
        public static SpaceAndPower GetSpaceAndPowerByRequestId(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                SpaceAndPower result = new SpaceAndPower();
                result = context.SpaceAndPowers
                                .Where(sp => sp.ID == requestId)
                                .SingleOrDefault();
                return result;
            }
        }

        public static SpaceAndPowerInfo GetSpaceAndPowerInfoByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpaceAndPowers
                .Where(t => t.ID == id)
                .Select(t => new SpaceAndPowerInfo
                {
                    ID = t.ID,
                    SpaceAndPowerCustomer = string.Format("{0} {1}", t.Customer.FirstNameOrTitle, t.Customer.LastName),
                    SpaceSize = t.SpaceSize,
                    SpaceType = DB.GetEnumDescriptionByValue(typeof(DB.SpaceType), t.SpaceType),
                    //EquipmentType = DB.GetEnumDescriptionByValue(typeof(DB.EquipmentType), t.EquipmentType),
                    EquipmentType = t.EquipmentType,
                    EquipmentWeight = t.EquipmentWeight,
                    SpaceUsage = t.SpaceUsage,
                    PowerType = DB.GetEnumDescriptionByValue(typeof(DB.PowerType), t.PowerType),
                    PowerRate = t.PowerRate,
                    HeatWasteRate = t.HeatWasteRate,
                    Duration = t.Duration,
                    RequestDescription = t.RequestDescription,
                    EnteghalFile = t.EnteghalFile,
                    EnteghalDate = t.EnteghalDate.ToPersian(Date.DateStringType.Short),
                    EnteghalUser = GetUserFullNameByUserId(t.EnteghalUserID),
                    EnteghalComment = t.EnteghalComment,
                    CircuitCommandFile = t.CircuitCommandFile,
                    SakhtemanDate = t.SakhtemanDate.ToPersian(Date.DateStringType.Short),
                    SakhtemanUser = GetUserFullNameByUserId(t.SakhtemanUserID),
                    SakhtemanComment = t.SakhtemanComment,
                    NirooFile = t.NirooFile,
                    NirooDate = t.NirooDate.ToPersian(Date.DateStringType.Short),
                    NirooUser = GetUserFullNameByUserId(t.NirooUserID),
                    NirooComment = t.NirooComment,
                    GhardadDate = t.GhardadDate.ToPersian(Date.DateStringType.Short),
                    GhardadUser = GetUserFullNameByUserId(t.GhardadUserID),
                    GhardadComment = t.GhardadComment,
                    HerasatDate = t.HerasatDate.ToPersian(Date.DateStringType.Short),
                    HerasatUser = GetUserFullNameByUserId(t.HerasatUserID),
                    HerasatComment = t.HerasatComment,
                    SooratHesabDate = t.SooratHesabDate.ToPersian(Date.DateStringType.Short),
                    SooratHesabUser = GetUserFullNameByUserId(t.SooratHesabUserID),
                    SooratHesabComment = t.SooratHesabComment,
                    FinancialScopeComment = t.FinancialScopeComment,
                    FinancialScopeDate = t.FinancialScopeDate.ToPersian(Date.DateStringType.Short),
                    FinancialScopeUser = GetUserFullNameByUserId(t.FinancialScopeUserID),
                    DesignManagerComment = t.DesignManagerComment,
                    DesignManagerDate = t.DesignManagerDate.ToPersian(Date.DateStringType.Short),
                    DesignManagerUser = GetUserFullNameByUserId(t.DesignManagerUserID),
                    SwitchDesigningOfficeComment = t.SwitchDesigningOfficeComment,
                    SwitchDesigningOfficeDate = t.SwitchDesigningOfficeDate.ToPersian(Date.DateStringType.Short),
                    SwitchDesigningOfficeUser = GetUserFullNameByUserId(t.SwitchDesigningOfficeUserID),
                    SwitchDesigningOfficeFile = t.SwitchDesigningOfficeFile,
                    DesignManagerFinalCheckComment = t.DesignManagerFinalCheckComment,
                    DesignManagerFinalCheckDate = t.DesignManagerFinalCheckDate.ToPersian(Date.DateStringType.Short),
                    DesignManagerFinalCheckUser = GetUserFullNameByUserId(t.DesignManagerFinalCheckUserID),
                    NetworkAssistantComment = t.NetworkAssistantComment,
                    NetworkAssistantDate = t.NetworkAssistantDate.ToPersian(Date.DateStringType.Short),
                    NetworkAssistantUser = GetUserFullNameByUserId(t.NetworkAssistantUserID),
                    AdministrationOfTheTelecommunicationEquipmentComment = t.AdministrationOfTheTelecommunicationEquipmentComment,
                    AdministrationOfTheTelecommunicationEquipmentDate = t.AdministrationOfTheTelecommunicationEquipmentDate.ToPersian(Date.DateStringType.Short),
                    AdministrationOfTheTelecommunicationEquipmentOperationDate = t.AdministrationOfTheTelecommunicationEquipmentOperationDate.ToPersian(Date.DateStringType.Short),
                    AdministrationOfTheTelecommunicationEquipmentUser = GetUserFullNameByUserId(t.AdministrationOfTheTelecommunicationEquipmentUserID),
                    CableAndNetworkDesignOfficeComment = t.CableAndNetworkDesignOfficeComment,
                    CableAndNetworkDesignOfficeDate = t.CableAndNetworkDesignOfficeDate.ToPersian(Date.DateStringType.Short),
                    CableAndNetworkDesignOfficeUser = GetUserFullNameByUserId(t.CableAndNetworkDesignOfficeUserID),
                    CableAndNetworkDesignOfficeFile = t.CableAndNetworkDesignOfficeFile,
                    DeviceHallComment = t.DeviceHallComment,
                    DeviceHallDate = t.DeviceHallDate.ToPersian(Date.DateStringType.Short),
                    DeviceHallUser = GetUserFullNameByUserId(t.DeviceHallUserID),
                    RigSpace = t.RigSpace,
                    HasFibre = t.HasFibre,
                    AntennaName = t.Antennas.Where(ant => ant.SpaceAndPowerID == t.ID).Select(ant => ant.Name).SingleOrDefault(),
                    AntennaCount = t.Antennas.Where(ant => ant.SpaceAndPowerID == t.ID).Select(ant => ant.Count).SingleOrDefault(),
                    AntennaHeight = t.Antennas.Where(ant => ant.SpaceAndPowerID == t.ID).Select(ant => ant.Height).SingleOrDefault()
                })
                .SingleOrDefault();
            }
        }

        public static Antenna GetAntennaBySpaceAndPowerId(long spaceAdnPowerId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Antenna result = new Antenna();
                result = context.Antennas.Where(ant => ant.SpaceAndPowerID == spaceAdnPowerId).SingleOrDefault();
                return result;
            }
        }

        public static string GetUserFullNameByUserId(int? id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Users.Where(t => t.ID == id).SingleOrDefault() != null)
                {
                    User user = context.Users.Where(t => t.ID == id).SingleOrDefault();
                    return user.FirstName + " " + user.LastName;
                }
                else
                    return "";
            }
        }

        public static string GetFileName(Guid? streamID)
        {
            if (streamID != null)
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.DocumentsFiles.Where(t => t.stream_id == streamID).SingleOrDefault().name;
                    //return "";
                }
            }
            else
                return "";
        }

        public static List<SpaceAndPowerInfo> GetSpaceAndPowerRequestsByCustomerID(long customerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<SpaceAndPowerInfo> result = new List<SpaceAndPowerInfo>();
                var query = context.SpaceAndPowers
                                   .Where(sp =>
                                              (sp.SpaceAndPowerCustomerID == customerID) &&
                                              (sp.Request.EndDate.HasValue)
                                         )
                                   .OrderByDescending(sp => sp.Request.EndDate)
                                   .Select(sp => new SpaceAndPowerInfo
                                                   {
                                                       ID = sp.ID,
                                                       RequestID = sp.Request.ID,
                                                       CityName = sp.Request.Center.Region.City.Name,
                                                       CenterID = sp.Request.Center.ID,
                                                       CenterName = sp.Request.Center.CenterName,
                                                       RequestDate = sp.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                       SpaceSize = sp.SpaceSize,
                                                       SpaceType = Helpers.GetEnumDescription(sp.SpaceType, typeof(DB.SpaceType)),
                                                       PowerType = Helpers.GetEnumDescription(sp.PowerType, typeof(DB.PowerType)),
                                                       PowerRate = sp.PowerRate,
                                                       HasAntenna = (sp.Antennas.Count > 0) ? "دارد" : "ندارد"
                                                   }
                                          )
                                   .AsQueryable();
                result = query.ToList();
                return result;
            }
        }

        public static SpaceAndPowerEquivalentDatabaseTableInfo GetSpaceAndPowerEquivalentDatabaseTableInfoByID(long spaceAndPowerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                SpaceAndPowerEquivalentDatabaseTableInfo result = new SpaceAndPowerEquivalentDatabaseTableInfo();
                var query = context.SpaceAndPowers
                                 .Where(sp => sp.ID == spaceAndPowerID)
                                 .Select(sp => new SpaceAndPowerEquivalentDatabaseTableInfo
                                                    {
                                                        EquipmentType = sp.EquipmentType,
                                                        EquipmentWeight = sp.EquipmentWeight,
                                                        Duration = sp.Duration,
                                                        HasAntenna = (sp.Antennas.Count > 0),
                                                        Antenna = (sp.Antennas.Count > 0) ? sp.Antennas.SingleOrDefault() : new Antenna(),
                                                        HasFibre = sp.HasFibre,
                                                        HeatWasteRate = sp.HeatWasteRate,
                                                        RigSpace = sp.RigSpace,
                                                        SpaceSize = sp.SpaceSize,
                                                        SpaceType = sp.SpaceType,
                                                        SpaceUsage = sp.SpaceUsage,
                                                        PowerTypes = sp.SpaceAndPowerPowerTypes.Select(spty => spty.PowerType).ToList()
                                                    }
                                        )
                                 .AsQueryable();
                result = query.SingleOrDefault();
                return result;
            }
        }

    }
}
