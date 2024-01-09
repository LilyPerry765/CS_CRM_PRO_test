using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ExchangeBrokenPCMDB
    {
        public static List<ExchangeBrokenPCM> GetExchangeBrokenPCMByRequestID(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.ExchangeBrokenPCMs.Where(t => t.RequestID == requestID).ToList();
            }
        }

        public static List<PCMBuchtTelephonReportInfo> GetExchangeBrokenPCMInfoByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeBrokenPCMs.Where(t => t.RequestID == requestID).Select(t=> new PCMBuchtTelephonReportInfo {
                    ID = t.ID,
                    OldTelephonNo  = t.TelephoneNo,
                    OldRock = t.PCM.PCMShelf.PCMRock.Number,
                    OldShelf= t.PCM.PCMShelf.Number,
                    OldCard=  t.PCM.Card,
                    OldColumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    OldRowNo    = t.Bucht.VerticalMDFRow.VerticalRowNo,
                    OldBuchtNo  = t.Bucht.BuchtNo,
                    NewRock  = t.PCM1.PCMShelf.PCMRock.Number, 
                    NewShelf = t.PCM1.PCMShelf.Number, 
                    NewCard  = t.PCM1.Card,
                    NewColumnNo = t.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    NewRowNo    = t.Bucht1.VerticalMDFRow.VerticalRowNo,
                    NewBuchtNo = t.Bucht1.BuchtNo,
                })
                .OrderBy(t=>t.OldColumnNo)
                .ThenBy(t=>t.OldRowNo)
                .ThenBy(t=>t.OldBuchtNo)
                .ToList();
            }
        }
    }
}
