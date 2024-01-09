using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ADSLPortTypeDB
    {
        public static List<CheckableItem> GetADSLPortTypeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLPortTypes.Select(t => new CheckableItem { ID = t.ID, Name = t.Name, IsChecked = false }).ToList();
            }
        }
    }
}
