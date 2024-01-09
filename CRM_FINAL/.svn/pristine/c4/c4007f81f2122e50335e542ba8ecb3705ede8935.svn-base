using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class SpecialConditionDB
    {
        public static SpecialCondition GetSpecialConditionByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialConditions.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }
    }
}
