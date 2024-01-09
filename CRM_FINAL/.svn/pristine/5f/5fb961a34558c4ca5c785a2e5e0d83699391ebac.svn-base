using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ADSLChangePortDB
    {
        public static ADSLChangePort1 GetADSLChangePortByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangePort1s.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<ADSLChangePortReason> SearchADSLChangePortReason(string reason)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangePortReasons.Where(t => t.Description.Contains(reason)).ToList();
            }
        }

        public static ADSLChangePortReason GetADSLChangePortReasonByID(long ReasonID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangePortReasons.Where(t => t.ID == ReasonID).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLChangePortReasonCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangePortReasons.Select(t => new CheckableItem
                {
                    Name = t.Description,
                    ID = t.ID,
                    IsChecked = false
                }).ToList();
            }
        }
    }
}
