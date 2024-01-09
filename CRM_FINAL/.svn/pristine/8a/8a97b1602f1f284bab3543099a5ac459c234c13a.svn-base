using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class VacateSpecialWirePointsDB
    {
        public static List<VacateSpecialWirePoint> GetVacateSpecialWirePointsByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VacateSpecialWirePoints.Where(t => t.RequestID == requestID).ToList();
            }
        }
    }
}
