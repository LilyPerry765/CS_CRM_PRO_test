using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TranslationCentralCableMDFDB
    {
        public static TranslationCentralCableMDFDetails GetTranslationVentralCableMDFDetails(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCentralCableMDFs.Where(t => t.ID == reqeustID)
                              .Select(t => new TranslationCentralCableMDFDetails 
                              {


                                  FromOldBucht = context.Buchts.Where(t2 => t2.ID == t.FromOldBuchtID)
                                                              .Select(t2 => "ردیف:" + t2.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t2.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t2.BuchtNo).SingleOrDefault(),

                                  ToOldBucht = context.Buchts.Where(t2 => t2.ID == t.ToOldBuchtID)
                                                      .Select(t2 => "ردیف:" + t2.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t2.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t2.BuchtNo).SingleOrDefault(),

                                  FromNewBucht = context.Buchts.Where(t2 => t2.ID == t.FromNewBuchtID)
                                                        .Select(t2 => "ردیف:" + t2.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t2.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t2.BuchtNo).SingleOrDefault(),

                                  ToNewBucht = context.Buchts.Where(t2 => t2.ID == t.ToNewBuchtID)
                                                      .Select(t2 => "ردیف:" + t2.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t2.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t2.BuchtNo).SingleOrDefault(),
                               }
                              ).SingleOrDefault();
            }
        }

        public static ExchangeCentralCableMDF GetTranslationVentralCableMDFByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCentralCableMDFs.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }
    }
}
