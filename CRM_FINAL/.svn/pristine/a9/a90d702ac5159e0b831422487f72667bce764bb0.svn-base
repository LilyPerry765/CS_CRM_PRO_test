using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ChangeLocationSpecialWirePointsDB
    {
        public static List<ChangeLocationSpecialWirePoint> GetChangeLocationSpecialWirePointsByRequestID(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeLocationSpecialWirePoints.Where(t => t.RequestID == reqeustID).ToList();
            }
        }
    }
}
