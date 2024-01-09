using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class UnitMeasureDB
    {
        public static List<UnitMeasure> GetUnitMeasures()
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<UnitMeasure> result = new List<UnitMeasure>();
                result = context.UnitMeasures.OrderBy(u=>u.Name).ToList();
                return result;
            }
        }
    }
}
