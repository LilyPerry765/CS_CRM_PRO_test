using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class SpecialServiceDB
    {
        public static SpecialService GetSpecialServiceByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialServices
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
    }
}