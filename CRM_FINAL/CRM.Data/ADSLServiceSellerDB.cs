using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLServiceSellerDB
    {
        public static List<ADSLServiceSeller> GetADSLServiceSellerByServiceId(int serviceID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceSellers
                    .Where(t => t.ADSLServiceID == serviceID)
                    .ToList();
            }
        }

        public static List<int> GetADSLServiceIDsbySellerID(int sellerAgentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceSellers
                    .Where(t => t.SellerAgentID == sellerAgentID)
                    .Select(t => t.ADSLServiceID)
                    .ToList();
            }
        }

        /// <summary>
        /// این متد مقادیر شناسه های سرویس های ای دی اس ال را بدون تکرار برمیگرداند
        /// </summary>
        /// <param name="sellerAgentID"></param>
        /// <returns></returns>
        public static List<int> GetADSLServicesIdBySellerAgentIdWithoutDuplicatedServicesId(int sellerAgentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceSellers
                              .Where(t => t.SellerAgentID == sellerAgentID)
                              .Select(t => t.ADSLServiceID)
                              .Distinct()
                              .ToList();
            }
        }

        public static List<ADSLServiceGroupSeller> GetADSLServiceGroupSellerByIServiceGroupId(int serviceGroupID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroupSellers
                    .Where(t => t.ServiceGroupID == serviceGroupID)
                    .ToList();
            }
        }

        public static List<int> GetADSLServiceGroupIDsbySellerID(int sellerAgentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServiceGroupSellers
                    .Where(t => t.SellerAgentID == sellerAgentID)
                    .Select(t => t.ServiceGroupID)
                    .ToList();
            }
        }
    }
}
