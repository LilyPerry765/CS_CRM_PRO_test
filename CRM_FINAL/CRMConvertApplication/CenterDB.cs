using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class CenterDB
    {
        public static Center GetCenterByCenterCode(int centerCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.CenterCode == centerCode).SingleOrDefault();
            }
        }

        public static int GetCenterIDByCenterCode(int centerCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Centers.Where(t => t.CenterCode == centerCode).SingleOrDefault().ID;
            }
        }
    }
}
