using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public  class TranslationPCMToNormalDB
    {
        public static TranslationPCMToNormal GetTranslationPCMToNormalByID(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.TranslationPCMToNormals.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

    }
}
