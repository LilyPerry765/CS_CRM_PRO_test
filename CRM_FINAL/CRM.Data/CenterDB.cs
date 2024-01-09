using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class CenterDB
    {
        private static List<CheckableItem> _centers { get; set; }
        public static List<Center> SearchCenter(
            List<int> citeis,
            int centerCode,
            string centerName,
            string telephone,
            string address,
            bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Where(t => (citeis.Count == 0 || citeis.Contains(t.RegionID)) &&
                                (centerCode == -1 || centerCode == t.CenterCode) &&
                                (string.IsNullOrWhiteSpace(centerName) || t.CenterName.Contains(centerName)) &&
                                (string.IsNullOrWhiteSpace(telephone) || t.Telephone.Contains(telephone)) &&
                                (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)) &&
                                (!isActive.HasValue || isActive == t.IsActive))
                  .ToList();
            }
        }

        public static List<CenterInfo> GetCenters()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && (t.IsActive == true))
                                      .OrderBy(t => t.Region.City.Name)
                                      .Select(t => new CenterInfo
                                      {
                                          ID = t.ID,
                                          CenterName = t.Region.City.Name + " : " + t.CenterName
                                      }).ToList();
            }
        }

        public static List<CheckableItem> GetCenterCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID))
                    .Select(t => new CheckableItem
                                {
                                    ID = t.ID,
                                    Name = t.Region.City.Name + " : " + t.CenterName,
                                    IsChecked = false
                                }
                            )
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetCenterCheckableByCityId(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && t.Region.CityID == id)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Region.City.Name + " : " + t.CenterName,
                        IsChecked = false
                    }
                            )
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static Center GetCenterById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static int GetCenterIDByCenterCode(int centerCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.CenterCode == centerCode).SingleOrDefault().ID;
            }
        }

        public static Center GetCenterByCenterCode(int centerCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.CenterCode == centerCode).SingleOrDefault();
            }
        }

        private static Func<MainDataContext, int, IQueryable<CenterInfo>> GetCenterByCityIDQuery =
            CompiledQuery.Compile((MainDataContext context, int id) =>
                                  (
                                 context.Centers.Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && t.Region.CityID == id && t.IsActive == true)
                                      .Select(t => new CenterInfo
                                      {
                                          ID = t.ID,
                                          CenterName = t.Region.City.Name + " : " + t.CenterName
                                      })
                                   ));

        private static Func<MainDataContext, int, IQueryable<CenterInfo>> GetALLCenterByCityIDQuery =
    CompiledQuery.Compile((MainDataContext context, int id) =>
                          (
                         context.Centers.Where(t => t.Region.CityID == id && t.IsActive == true)
                              .Select(t => new CenterInfo
                              {
                                  ID = t.ID,
                                  CenterName = t.Region.City.Name + " : " + t.CenterName
                              })
                           ));

        //متد زیر را میلاد نوشته ولی قابلیت سورت ندارد
        //13941106
        //public static List<CenterInfo> GetCenterByCityId(int id)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //List<CenterInfo> centerInfo = GetCenterByCityIDQuery(context, id).ToList();
        //        return centerInfo;
        //    }
        //}

        //TODO:rad 13941106
        //public static List<CenterInfo> GetCenterByCityId(int cityId)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        List<CenterInfo> result = new List<CenterInfo>();
        //        var query = context.Centers
        //                           .Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && t.Region.CityID == cityId && t.IsActive == true)
        //                           .OrderBy(t => t.Region.City.Name)
        //                           .ThenBy(t => t.CenterName)
        //                           .Select(t => new CenterInfo
        //                                             {
        //                                                 ID = t.ID,
        //                                                 CenterName = t.Region.City.Name + " : " + t.CenterName
        //                                             }
        //                                  )
        //                         .AsQueryable();
        //        result = query.ToList();
        //        return result;
        //    }
        //}

        //بر اساس فکس 62 موردی کرمانشاه 
        //(در تمام عملیات ثبت اطلاعات که نیاز به انتخاب شهر و مرکز است  شهرستان اصلی پیش فرض باشد.)
        //TODO:rad 13950616
        public static List<CenterInfo> GetCenterByCityId(int cityId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CenterInfo> result = new List<CenterInfo>();
                var query = context.Centers
                                   .Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && t.Region.CityID == cityId && t.IsActive == true)
                                   .OrderBy(t => t.CenterCode)
                                   .Select(t => new CenterInfo
                                   {
                                       ID = t.ID,
                                       CenterName = t.Region.City.Name + " : " + t.CenterName
                                   }
                                          )
                                 .AsQueryable();
                result = query.ToList();
                return result;
            }
        }

        public static List<CenterInfo> GetAllCenterByCityId(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CenterInfo> centerInfo = GetALLCenterByCityIDQuery(context, id).ToList();
                return centerInfo;
            }
        }

        public static List<CheckableItem> GetCenterCheckableItemByCityIds(List<int> citiesId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                result = context.Centers
                                .Where(t =>
                                             DB.CurrentUser.CenterIDs.Contains(t.ID) &&
                                             citiesId.Contains(t.Region.CityID)
                                       )
                                .OrderBy(t => t.Region.City.Name)
                                .ThenBy(t => t.CenterCode)
                                .Select(t => new CheckableItem
                                            {
                                                ID = t.ID,
                                                Name = t.Region.City.Name + " : " + t.CenterName,
                                                IsChecked = false
                                            }
                                      )
                                .ToList();
                return result;
            }
        }


        public static List<CheckableItem> GetCenterIsCheckedCheckableItemByCityIds(List<int> ids)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && ids.Contains(t.Region.CityID))
                                      .Select(t => new CheckableItem
                                      {
                                          ID = t.ID,
                                          Name = t.Region.City.Name + " : " + t.CenterName,

                                          IsChecked = true
                                      }).ToList();
            }
        }

        public static List<CheckableItem> GetCenterCheckableByRegionID(int regionID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.RegionID == regionID && DB.CurrentUser.CenterIDs.Contains(t.ID))
                                      .Select(t => new CheckableItem
                                      {
                                          ID = t.ID,
                                          Name = t.CenterName,
                                          IsChecked = false
                                      }).ToList();
            }
        }

        public static List<Center> GetAllCenter()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.ToList();
            }
        }

        public static List<int> GetUserCenters(int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.UserCenters.Where(t => t.UserID == userID).Select(t => t.CenterID).ToList();
            }

        }

        public static int GetCityIDByCenterByID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.ID == centerID).SingleOrDefault().Region.City.ID;
            }
        }

        public static int GetCenterIDByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => t.ID == postID).SingleOrDefault().Cabinet.CenterID;
            }
        }

        public static Center GetCenterByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Posts.Where(t => t.ID == postID).SingleOrDefault().Cabinet.Center;

            }
        }

        public static List<CheckableItem> GetAvailableCenterCheckable()
        {
            if (_centers == null)
            {
                using (MainDataContext context = new MainDataContext())
                {
                    _centers = context.Cities.Join(context.Regions, c => c.ID, r => r.CityID, (c, r) => new { Cities = c, Regions = r })
                                   .Join(context.Centers, cr => cr.Regions.ID, ce => ce.RegionID, (cr, ce) => new { Centers = ce, Cities = cr.Cities })
                                   .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Centers.ID))
                                   .Select(t => new CheckableItem
                                   {
                                       Name = t.Centers.CenterName,
                                       ID = t.Centers.ID,
                                       IsChecked = false
                                   }).Distinct().ToList();
                }
            }

            return _centers;
        }

        public static List<string> GetCenterNameByIDs(List<int> CenterIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Where(t => CenterIDs.Count == 0 || CenterIDs.Contains(t.ID))
                    .Select(t => t.CenterName)
                    .ToList();
            }
        }

        public static string GetCenterNameByID(int CenterID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Where(t => (CenterID == 0 || CenterID == t.ID))
                    .Select(t => t.CenterName).SingleOrDefault();
            }
        }

        public static string GetCenterNameByID1(int CenterID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Where(t => CenterID == t.ID).SingleOrDefault().CenterName;
            }
        }

        public static int GetCenterIDByName(string CenterName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Where(t => string.IsNullOrEmpty(CenterName) || t.CenterName.Equals(CenterName))
                    .Select(t => t.ID).SingleOrDefault();
            }
        }

        public static Center GetCenterByCenterID(int? CenterID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Where(t => (!CenterID.HasValue || t.ID == CenterID)).SingleOrDefault();
            }
        }

        public static string GetSubsidiaryCode(long telephoneNo, int? centerID, byte SubsidiaryCodeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string subsidiaryCode = "";
                Center center = new Center();

                if (telephoneNo == null || telephoneNo == 0)
                {
                    Service1 service = new Service1();
                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());
                    center = GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                }
                else
                {
                    if (centerID != null)
                        center = GetCenterById((int)centerID);
                    else
                    {
                        Service1 service = new Service1();
                        System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());
                        center = GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                    }
                }

                switch (SubsidiaryCodeType)
                {
                    case (byte)DB.SubsidiaryCodeType.Telephone:
                        subsidiaryCode = GetSubsidiaryCodeTelephonebyCenterID(center.ID);
                        break;

                    case (byte)DB.SubsidiaryCodeType.Service:
                        subsidiaryCode = GetSubsidiaryCodeServicebyCenterID(center.ID);
                        break;

                    case (byte)DB.SubsidiaryCodeType.ADSL:

                        subsidiaryCode = GetSubsidiaryCodeADSLbyCenterID(center.ID);
                        break;

                    default:
                        break;
                }

                return subsidiaryCode;

            }
        }

        public static string GetSubsidiaryCodeTelephonebyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.ID == centerID).SingleOrDefault().SubsidiaryCode.Code.ToString();
            }
        }

        public static string GetSubsidiaryCodeServicebyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.ID == centerID).SingleOrDefault().SubsidiaryCode1.Code.ToString();
            }
        }

        public static string GetSubsidiaryCodeADSLbyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.ID == centerID).SingleOrDefault().SubsidiaryCode2.Code.ToString();
            }
        }

        public static int GetCenterIDbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().CenterID;
            }
        }

        public static List<CheckableItem> GetCentersCheckable(bool checkAll = false, List<int> cityIDs = null)
        {

            using (var context = new MainDataContext())
            {
                if (cityIDs == null)
                    cityIDs = new List<int>();
                return context.Centers
                          .Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && ((cityIDs.Count == 0) || cityIDs.Contains(t.Region.CityID)))
                          .OrderBy(t => t.Region.City.Name)
                          .ThenBy(t => t.CenterName)
                          .Select(t => new CheckableItem
                          {
                              Name = string.Format("{0}: {1}", t.Region.City.Name, t.CenterName),
                              ID = t.ID,
                              IsChecked = checkAll
                          }
                                  )
                          .ToList();
            }
        }

        public static string GetCenterFullName(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers
                    .Where(t => centerID == t.ID).Select(t => t.Region.City.Name + " - " + t.CenterName).SingleOrDefault();
            }
        }

        public static Center GetCenterByCabinetInputID(long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.ID == cabinetInputID).SingleOrDefault().Cabinet.Center;
            }
        }

        public static string GetCenterNamebyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.ID == centerID).SingleOrDefault().Region.City.Name + " : " + context.Centers.Where(t => t.ID == centerID).SingleOrDefault().CenterName;
            }
        }

        public static List<Center> GetCentersHaveLocation()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.Latitude != null && t.Longitude != null).ToList();
            }
        }

        public static List<CenterInfo> GetCenterByCityIdWithoutCoordinates(int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID) && t.Region.CityID == cityID && t.Latitude == null && t.Longitude == null)
                                      .Select(t => new CenterInfo
                                      {
                                          ID = t.ID,
                                          CenterName = t.Region.City.Name + " : " + t.CenterName
                                      }).ToList();
            }
        }

        //public static List<string> GetCityCenterNameListbyCenterIDs(List<int> centerIDs)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        //return context.Centers.Where(t=>centerIDs.conta)
        //    }
        //}

        public static List<int> GetCenterCodeByCenterIDs(List<int> list)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => list.Contains(t.ID)).Select(t => (int)t.CenterCode).ToList();
            }
        }

        public static int GetCityIDByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int regionID = context.Centers.Where(t => t.ID == centerID).SingleOrDefault().RegionID;
                return context.Regions.Where(t => t.ID == regionID).SingleOrDefault().CityID;
            }
        }
    }
}
