using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class ADSLChangeCustomerOwnerCharacteristicsDB
    {
       public static ADSLChangeCustomerOwnerCharacteristic GetADSLChangeCustomerOwnerCharacteristicsByID(long ID)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.ADSLChangeCustomerOwnerCharacteristics.Where(t =>(t.ID == ID)).SingleOrDefault();
           }
       }

       //public static ADSLChangeCustomerOwnerCharacteristic GetADSLChangeCustomerOwnerCharacteristicbytelNo(long TelNo)
       //{
       //    using (MainDataContext context = new MainDataContext())
       //    {
       //        return context.ADSLChangeCustomerOwnerCharacteristics.Where(t => (t.Customer.)).SingleOrDefault();
       //    }
       //}
    }
}
