using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TelephonePBXDB
    {
        public static List<PBXTelephone> GetTelephonePBX(long telephonNo)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.TelephonePBXes
                     .Where(t => t.HeadTelephone == telephonNo)
                     .OrderBy(t=>t.priority)
                     .Select(t => new PBXTelephone 
                     { 
                        TelephoneNo = t.OtherTelephone,
                        HeadTelephoneNo  = t.HeadTelephone
                     }).ToList();
            }
        }

        public static List<PBXTelephone> GetAllCustomerTelephone(long telephonNo)
        {
            using(MainDataContext context = new MainDataContext())
            {

                return context.Telephones.Where(t => t.CustomerID == context.Telephones.Where(t2 => t2.TelephoneNo == telephonNo).SingleOrDefault().CustomerID)
                    .Select(t => new PBXTelephone 
                    {
                        TelephoneNo = t.TelephoneNo
                    }).ToList();
            }
        }

        public static bool CheckTelephoneBePBX(long teleponeNo)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.TelephonePBXes.Any(t => t.OtherTelephone == teleponeNo);
            }
        }

        public static bool CheckTelephoneBeHeadPBX(long telephonNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelephonePBXes.Any(t => t.HeadTelephone == telephonNo);
            }
        }
    }
}
