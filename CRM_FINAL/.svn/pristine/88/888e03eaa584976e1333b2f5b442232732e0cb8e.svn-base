using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLChangeServiceRequestDB
    {
        public static ADSLChangeService GetADSLChangeServiceByRequestID(long Id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangeServices.Where(t => t.ID == Id).SingleOrDefault();
            }
        }
    }
}
