using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class PostContactEquipmentDB
    {
        public static List<PostContactEquipment> GetPostContactEquipmentByID(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContactEquipments.Where(t => t.PostContactID == postContactID).ToList();
            }
        }
    }
}
