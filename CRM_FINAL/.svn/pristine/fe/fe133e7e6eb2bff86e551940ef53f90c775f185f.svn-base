using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ExchangeCabinetDB
    {
        public static ExchangeCabinetInput GetExchangeCabinet(long _requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs.Where(t => t.ID == _requestID).SingleOrDefault();
            }
        }

        public static List<ExchangeCabinetConnectionInfo> GetExchangeCabinetByRequestID(long requestID)
        {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.ExchangeCabinetConncetions.Where(t => t.RequestID == requestID)
                               .Select(t => new ExchangeCabinetConnectionInfo
                               {
                                   FromTelephonNo = t.FromTelephoneNo,
                                   FromBuchtNo = t.Bucht.BuchtNo,
                                   FromColumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                   FromRowNo = t.Bucht.VerticalMDFRow.VerticalRowNo,
                                   FromBuchtID = t.FromBuchtID,
                                   FromBuchtStatus = t.Bucht.Status,
                                   FromCabinet = t.CabinetInput.CabinetID,
                                   FromCabinetNumber = t.CabinetInput.Cabinet.CabinetNumber,
                                   FromCabinetInputID = t.CabinetInput.ID,
                                   FromCabinetInputNumber = t.CabinetInput.InputNumber,
                                   FromPostContactID = t.FromPostContactID,
                                   FromPostContactNumber = t.PostContact.ConnectionNo,
                                   FromIsRound = t.Telephone.IsRound,
                                   FromIsVIP = t.Telephone.IsVIP,
                                   FromPostNumber = t.PostContact.Post.Number,
                                   FromSwitchPortID = t.Telephone.SwitchPortID,



                                   ToBuchtNo = t.Bucht1.BuchtNo,
                                   ToColumnNo = t.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                   ToRowNo = t.Bucht1.VerticalMDFRow.VerticalRowNo,
                                   ToBuchtID = t.FromBuchtID,
                                   ToBuchtStatus = t.Bucht1.Status,
                                   ToCabinet = t.CabinetInput1.CabinetID,
                                   ToCabinetNumber = t.CabinetInput1.Cabinet.CabinetNumber,
                                   ToCabinetInputID = t.CabinetInput1.ID,
                                   ToCabinetInputNumber = t.CabinetInput1.InputNumber,
                                   ToPostContactID = t.FromPostContactID,
                                   ToPostConntactNumber = t.PostContact1.ConnectionNo,
                                   ToPostNumber = t.PostContact1.Post.Number,

                                   CustomerName = string.Format("{0} {1}", t.Customer.FirstNameOrTitle, t.Customer.LastName),
                               }).ToList();
                }
        }
    }
}
