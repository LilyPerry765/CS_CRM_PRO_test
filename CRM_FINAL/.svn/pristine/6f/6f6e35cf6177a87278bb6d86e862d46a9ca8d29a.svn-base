using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class  ChangeAddressDB
    {
        public static ChangeAddress GetChangeAddressByID(long ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeAddresses.Where(t => t.ID == ID).SingleOrDefault();
            }
        }
    }
}
