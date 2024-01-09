using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ADSLDischargeDB
    {
        public static ADSLDischarge GetADSLDischargeByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLDischarges.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<ADSLDischargeReason> SearchADSLDischargeReason()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLDischargeReasons.ToList();
            }

        }

        public static List<ADSLDischargeReason> SearchADSLDischargeReasonByTitle(string reason)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLDischargeReasons.Where(t => t.Title.Contains(reason)).ToList();
            }
        }

        public static ADSLDischargeReason GetADSLDischargeReasonByID(int ReasonID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLDischargeReasons.Where(t => t.ID == ReasonID).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLDischargeReasonCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLDischargeReasons.Select(t => new CheckableItem
                {
                    Name = t.Title,
                    ID = (int)t.ID,
                    IsChecked = false
                }).ToList();
            }
        }
    }
}
