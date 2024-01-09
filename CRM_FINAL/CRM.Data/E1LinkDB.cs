using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class E1LinkDB
    {
        public static E1Link GetE1LinkByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Links.Where(t => t.ID == id).SingleOrDefault();
            }

        }

        //milad doran
        //public static bool CheckALLInvestigatePossibility(long requestID)
        //{
        //    using(MainDataContext context = new MainDataContext())
        //    {

        //        return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContactID != null);
        //    }
        //}

        //TODO:rad 13950419
        public static bool CheckALLInvestigatePossibility(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool result = true;

                //آیا همه لینک های ایوان درخواست مربوطه ، بررسی امکانات شده اند یا خیر
                result = context.E1Links
                                .Where(e1Link => e1Link.ReqeustID == requestID)
                                .All(e1Link => e1Link.InvestigatePossibilityID.HasValue);

                return result;

            }
        }

        public static bool CheckALLTechSupportDeparteman(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.ModemTypeID != null);
            }
        }
        public static bool CheckALLSwitchDate(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.SwitchDate != null);
            }
        }

        public static bool CheckALLMDF(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.MDFDate != null);
            }
        }

        public static bool CheckALLNetwork(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.NetworkDate != null);
            }
        }

        public static bool CheckALLSwitch(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.SwitchE1NumberID != null && t.SwitchInterfaceE1NumberID != null);
            }
        }
        public static bool CheckALLTechnicalSupportDate(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.TechnicalSupportDate != null);
            }
        }
        public static List<E1Link> GetE1LinkByRequestID(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.E1Links.Where(t => t.ReqeustID == requestID).ToList();
            }
        }

        public static bool CheckALLTechSupport(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.E1Links.Where(t => t.ReqeustID == requestID).All(t => t.AcessBuchtID != null && t.AcessE1NumberID != null);
            }
        }
    }
}
