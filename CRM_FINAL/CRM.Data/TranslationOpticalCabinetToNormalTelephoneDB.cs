using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TranslationOpticalCabinetToNormalTelephoneDB
    {
        public static List<TranslationOpticalCabinetToNormalTelephone> GetTranslationOpticalCabinetToNormalTelephoneByRequestID(long _requestID)
        {
           using( MainDataContext context = new MainDataContext())
           {
               return context.TranslationOpticalCabinetToNormalTelephones.Where(t => t.RequestID == _requestID).ToList();
           }
        }
    }
}
