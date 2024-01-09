using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class ADSLDB
    {
        public static ADSL GetADSLbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();
            }
        }

        public static List<ADSL> GetADSLwithInstalmentService()
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<int> instalmentServiceIDList = context.ADSLServices.Where(s => s.IsInstalment == true).Select(t => t.ID).ToList();

                return context.ADSLs.Where(t => t.TariffID != null && instalmentServiceIDList.Contains((int)t.TariffID)).ToList();
            }
        }

        public static List<ADSL> GetADSLList()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLs.ToList();
            }
        }

        public static Customer GetCustomerbyID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static Telephone GetTelephonebyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();
            }
        }

        
    }
}
