using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLModemPropertyDB
    {
        public static ADSLModemPropertyInfo GetADSLModemPropertyById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties
                    .Where(t => t.ID == id).Select(t => new ADSLModemPropertyInfo
                    {
                        ID = t.ID,
                        CenterName = t.Center.CenterName,
                        SerialNo = t.SerialNo,
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLModemStatus), (int)t.Status),
                        ModemModel = t.ADSLModem.Model + " : " + t.ADSLModem.Title,
                        ModemStatus = t.Status,
                        ADSLModemID = t.ADSLModemID,
                        CenterID = t.CenterID,
                    }).SingleOrDefault();
            }
        }

        public static ADSLModemProperty GetADSLModemPropertiesById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties
                    .Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<ADSLModemPropertyInfo> SearchADSLModemProperties(List<int> cities, List<int> models, string telephoneNo, string serialNo, string mACAddress, List<int> soldStatus, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where(t =>
                    (cities.Count == 0 || cities.Contains((int)t.Center.Region.CityID)) &&
                    (models.Count == 0 || models.Contains((int)t.ADSLModemID)) &&
                    (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                    (string.IsNullOrWhiteSpace(serialNo) || t.SerialNo.Contains(serialNo)) &&
                    (string.IsNullOrWhiteSpace(mACAddress) || t.MACAddress.Contains(mACAddress)) &&
                    (soldStatus.Count == 0 || soldStatus.Contains((int)t.Status)))
                    .Select(t => new ADSLModemPropertyInfo
                    {
                        ID = t.ID,
                        CenterName = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        ModemModel = t.ADSLModem.Model + " : " + t.ADSLModem.Title,
                        SerialNo = t.SerialNo,
                        MACAddress = t.MACAddress,
                        TelephoneNo = t.TelephoneNo.ToString(),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.ADSLModemStatus), (int)t.Status)
                    }).Skip(startRowIndex).Take(pageSize).ToList(); ;
            }
        }

        public static int SearchADSLModemPropertiesCount(List<int> cities, List<int> models, string telephoneNo, string serialNo, string mACAddress, List<int> soldStatus)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where(t =>
                    (cities.Count == 0 || cities.Contains((int)t.Center.Region.CityID)) &&
                    (models.Count == 0 || models.Contains((int)t.ADSLModemID)) &&
                    (string.IsNullOrWhiteSpace(telephoneNo) || t.TelephoneNo.ToString().Contains(telephoneNo)) &&
                    (string.IsNullOrWhiteSpace(serialNo) || t.SerialNo.Contains(serialNo)) &&
                    (string.IsNullOrWhiteSpace(mACAddress) || t.MACAddress.Contains(mACAddress)) &&
                    (soldStatus.Count == 0 || soldStatus.Contains((int)t.Status))).Count();
            }
        }

        public static List<ADSLModemPropertyInfo> GetADSLModemPropertiesList(int modelID, int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where
                    (t => t.Center.Region.City.ID == cityID && t.ADSLModemID == modelID && t.Status != (byte)DB.ADSLModemStatus.Sold)
                    .Select(t => new ADSLModemPropertyInfo
                    {
                        ID = t.ID,
                        Title = t.SerialNo
                    }).ToList();
            }
        }

        public static string GetSerialNobyID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where(t => t.ID == id).SingleOrDefault().SerialNo;
            }
        }

        public static bool GetADSLModemPropertybySerialNo(int modemID, string serialNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLModemProperty modemProperty = context.ADSLModemProperties.Where(t => t.ADSLModemID == modemID && (string.Equals(t.SerialNo, serialNo))).SingleOrDefault();

                if (modemProperty != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool GetADSLModemPropertybyMACAddress(int centerID, int modemID, string mACAddress)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLModemProperty modemProperty = context.ADSLModemProperties.Where(t => t.CenterID == centerID && t.ADSLModemID == modemID && (string.Equals(t.MACAddress, mACAddress))).SingleOrDefault();

                if (modemProperty != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool HasADSLModembyTelephoneNo(long telephoneNo)
        {
            bool result = true;

            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLModemProperty> modemList = context.ADSLModemProperties.Where(t => t.TelephoneNo == telephoneNo).ToList();

                if (modemList.Count == 0)
                    result = false;
                else
                    result = true;
            }

            return result;
        }

        public static List<CheckableItem> GetADSLModemPropertySerialNoCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where(t => t.SerialNo != null).Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.SerialNo,
                        IsChecked = false
                    })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLModemPropertyNotSoldCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where(t => t.SerialNo != null && t.Status == (byte)DB.ADSLModemStatus.NotSold).Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.SerialNo,
                    IsChecked = false
                })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLModemPropertyNotSoldCheckablebyModemID(int modemID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where(t => t.ADSLModemID == modemID && t.SerialNo != null && t.Status == (byte)DB.ADSLModemStatus.NotSold).Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.SerialNo,
                        IsChecked = false
                    })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLModemPropertyNotSoldCheckablebyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.Where(t => t.CenterID == centerID && t.SerialNo != null && t.Status == (byte)DB.ADSLModemStatus.NotSold).Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.SerialNo,
                        IsChecked = false
                    })
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }
    }
}
