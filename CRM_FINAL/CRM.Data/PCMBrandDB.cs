using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class PCMBrandDB
    {

        public static List<CheckableItem> GetPCMBrandCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMBrands
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Name,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static PCMBrand GetPCMPrandByPCMID(int PCMID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMBrands.Where(t => t.PCMs.Where(p => p.ID == PCMID).SingleOrDefault().PCMBrandID == t.ID).SingleOrDefault();
            }
        }
    }
}
