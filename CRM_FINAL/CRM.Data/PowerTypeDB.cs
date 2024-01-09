using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class PowerTypeDB
    {
        public static List<CheckableItem> GetCheckablePowerTypes()
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                result = context.PowerTypes
                                .Select(pt => new CheckableItem
                                              {
                                                  ID = pt.ID,
                                                  Name = (pt.Rate != -1) ? "عنوان : " + pt.Title + " -- " + "میزان پاور مصرفی : " + pt.Rate + " آمپر " : pt.Title,
                                                  IsChecked = false
                                              }
                                       )
                                .ToList();
                return result;
            }
        }

        public static void SavePowerTypes(long spaceAndPowerId, List<int> powerTypesId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DateTime currentDate = DB.GetServerDate();

                    //ابتدا باید پاور های مصرفی در حال حاضر رکورد فضا و پاور  را بدست بیاوریم
                    //تا با استفاده از آنها تشخیص بدهیم که چه پاورهای مصرفی جدید هستند و چه پاورهای مصرفی باید از رکورد فضا و پاور حذف شوند
                    List<int> previousPowerTypesId = new List<int>();
                    previousPowerTypesId = context.SpaceAndPowerPowerTypes
                                                  .Where(sp => sp.SpaceAndPowerID == spaceAndPowerId)
                                                  .Select(sp => sp.PowerTypeID)
                                                  .ToList();

                    List<int> mustBeDeletedPowwerTypesID = new List<int>();
                    List<SpaceAndPowerPowerType> mustBeDeletedSpaceAndPowerPowerTypes = new List<SpaceAndPowerPowerType>();
                    foreach (int pi in previousPowerTypesId)
                    {
                        if (!powerTypesId.Contains(pi))
                        {
                            mustBeDeletedPowwerTypesID.Add(pi);
                        }
                    }
                    mustBeDeletedSpaceAndPowerPowerTypes = context.SpaceAndPowerPowerTypes
                                                                  .Where(sp =>
                                                                             (sp.SpaceAndPowerID == spaceAndPowerId) &&
                                                                             (mustBeDeletedPowwerTypesID.Contains(sp.PowerTypeID))
                                                                        )
                                                                  .ToList();

                    List<SpaceAndPowerPowerType> newItems = new List<SpaceAndPowerPowerType>();
                    foreach (int ni in powerTypesId)
                    {
                        if (!previousPowerTypesId.Contains(ni))
                        {
                            SpaceAndPowerPowerType item = new SpaceAndPowerPowerType();
                            item.PowerTypeID = ni;
                            item.SpaceAndPowerID = spaceAndPowerId;
                            item.InsertDate = currentDate;
                            newItems.Add(item);
                        }
                    }

                    if (mustBeDeletedSpaceAndPowerPowerTypes.Count != 0)
                    {
                        context.SpaceAndPowerPowerTypes.DeleteAllOnSubmit<SpaceAndPowerPowerType>(mustBeDeletedSpaceAndPowerPowerTypes);
                        context.SubmitChanges();
                    }
                    if (newItems.Count != 0)
                    {
                        context.SpaceAndPowerPowerTypes.InsertAllOnSubmit<SpaceAndPowerPowerType>(newItems);
                        context.SubmitChanges();
                    }
                    scope.Complete();
                }
            }
        }

        public static List<PowerType> GetPowerTypesBySpaceAndPowerID(long spaceAndPowerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PowerType> result = new List<PowerType>();
                result = context.SpaceAndPowerPowerTypes
                                .Where(sp => sp.SpaceAndPowerID == spaceAndPowerID)
                                .Select(sp => sp.PowerType)
                                .ToList();
                return result;
            }
        }

        public static List<PowerTypeInfo> GetPowerTypeInfosBySpaceAndPowerID(long spaceAndPowerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PowerTypeInfo> result = new List<PowerTypeInfo>();
                result = context.SpaceAndPowerPowerTypes
                                .Where(sp => sp.SpaceAndPowerID == spaceAndPowerID)
                                .Select(sp => new PowerTypeInfo
                                              {
                                                  Title = sp.PowerType.Title,
                                                  Rate = (sp.PowerType.Rate != -1) ? sp.PowerType.Rate.ToString() : sp.SpaceAndPower.PowerRate
                                              }
                                       )
                                .ToList();
                result.ForEach(pti => 
                                    {
                                        pti.Rate = !string.IsNullOrEmpty(pti.Rate) ? pti.Rate : "نامشخص";
                                    }
                              );
                return result;
            }
        }

        public static PowerType GetPowerTypeByID(int powerTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                PowerType result = new PowerType();
                result = context.PowerTypes
                                .Where(pt => pt.ID == powerTypeID)
                                .SingleOrDefault();
                return result;
            }
        }

        public static List<PowerType> SearchPowerTypes(string title, int rate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PowerType> result = new List<PowerType>();
                result = context.PowerTypes.Where(pt =>
                                                    (string.IsNullOrEmpty(title) || pt.Title.Contains(title)) &&
                                                    (rate == -1 || pt.Rate == rate)
                                                  )
                                           .ToList();
                return result;
            }
        }
    }
}
