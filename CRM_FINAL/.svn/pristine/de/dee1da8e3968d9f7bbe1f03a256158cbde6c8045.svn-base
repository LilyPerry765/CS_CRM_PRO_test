using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TranslationCabinetDB
    {
        public static TranslationCabinetDetails GetTranslationCabinetInfo(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs.Where(t => t.ID == requestID).Select(t=> new TranslationCabinetDetails
                     {
                         
                            NewCabinet = t.Cabinet1.CabinetNumber,
                            OldCabinet = t.Cabinet.CabinetNumber,
                          //  OldFromBuchtInfo = t.CabinetInput1.Buchts.

                            FromNewCabinetInput = t.CabinetInput2.InputNumber,

                            ToNewCabinetInput = t.CabinetInput3.InputNumber,
                             
                            FromOldCabinetInput = t.CabinetInput.InputNumber,
                            ToOldCabinetInput = t.CabinetInput1.InputNumber,
                     }
                     ).SingleOrDefault();
            }
        }

        public static ExchangeCabinetInput GetTranslationCabinetByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

    
    }
}
