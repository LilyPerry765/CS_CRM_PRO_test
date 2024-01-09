using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class SwitchTypeDB
    {
        public static List<SwitchType> SearchSwitchType(
            string     commercialName,
            List<int>  switchType,
            bool?      isDigital,
            int        capacity,
            int        operationalCapacity,
            int        installCapacity,
            int        specialServiceCapacity,
            int        counterDigitCount,
            bool?      supportPublicNo,
            int        publicCapacity,
            List<int>  trafficTypeCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchTypes
                    .Where(t=>(string.IsNullOrWhiteSpace(commercialName) || t.CommercialName==commercialName) &&
                      (switchType.Count == 0 || switchType.Contains(Convert.ToInt32(t.SwitchTypeValue))) &&
                      (!isDigital.HasValue || isDigital == t.IsDigital) &&
                      (capacity == -1|| t.Capacity == Convert.ToInt32(capacity)) &&
                      (operationalCapacity== -1 || t.OperationalCapacity == Convert.ToInt32(operationalCapacity)) &&
                      (installCapacity== -1 || t.InstallCapacity == Convert.ToInt32(installCapacity)) &&
                      (specialServiceCapacity== -1 || t.SpecialServiceCapacity == Convert.ToInt32(specialServiceCapacity)) &&
                      (counterDigitCount== -1 || t.CounterDigitCount == Convert.ToInt32(counterDigitCount)) &&
                      (!supportPublicNo.HasValue || supportPublicNo == t.SupportPublicNo) &&
                      (publicCapacity == -1 || t.PublicCapacity == publicCapacity) &&
                      (trafficTypeCode.Count == 0 || trafficTypeCode.Contains(Convert.ToInt32(t.TrafficTypeCode)))).ToList(); 
            }
        }





        public static SwitchType GetSwitchTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchTypes.Where(t => t.ID == id).SingleOrDefault();
            }
        }


        public static List<CheckableItem> GetSwitchCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchTypes 
                    .Select(t => new CheckableItem
                                {
                                    ID = t.ID,
                                    Name = t.CommercialName,
                                    IsChecked = false
                                }
                            )
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }


        /// <summary>
        /// رکورد نوع سوئیچ را با در یافت پیش شماره باز گشت می دهد
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static SwitchType GetSwitchTypeBySwitchPrecodeID(int switchPrecode)
        {
            using (MainDataContext context = new MainDataContext())
            {
              return   context.SwitchTypes.Where(t => t.Switches.Where(s => s.SwitchPrecodes.Where(sp => sp.ID == switchPrecode).SingleOrDefault().SwitchID == t.ID).SingleOrDefault().SwitchTypeID == t.ID).SingleOrDefault();
            }
        }
        /// <summary>
        /// رکورد نوع سوئیچ را با در یافت سوئیچ باز گشت می دهد
        /// </summary>
        /// <param name="switchID"></param>
        /// <returns></returns>
        public static SwitchType GetSwitchTypeBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchTypes.Where(t => t.ID == t.Switches.Where(s => s.ID == switchID).SingleOrDefault().SwitchTypeID).SingleOrDefault();
            }
        }
    }
}
