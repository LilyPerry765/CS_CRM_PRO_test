using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ExchangeCabinetInputDB
    {
        public static bool IsSpecialWire(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                Bucht bucht = context.Buchts.Where(t => t.ID == buchtID).Join(context.Buchts, fb => fb.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { SecondBucht = sb }).Select(t => t.SecondBucht).SingleOrDefault();
                if (bucht != null && bucht.BuchtTypeID == (byte)DB.BuchtType.PrivateWire)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static ExchangeCabinetInput GetExchangeCabinetInputByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static List<ExchangeCabinetInputConnectionInfo> GetExchangeCabinetInpuByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputConncetions.Where(t => t.RequestID == requestID).Select(t => new ExchangeCabinetInputConnectionInfo
                {
                    FromPostID = t.Post.ID,
                    FromPostNumber = t.Post.Number,
                    FromAorBType = t.Post.AorBType,
                    FromAorBTypeName = t.Post.AORBPostAndCabinet.Name,
                    FromPostContactID = t.PostContact.ID,
                    FromPostConntactNumber = t.PostContact.ConnectionNo,
                    FromConnectiontype = (t.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی"),

                    ToPostID = t.Post1.ID,
                    ToPostNumber = t.Post1.Number,
                    ToAorBTypeName = t.Post1.AORBPostAndCabinet.Name,

                    ToPostConntactID = t.PostContact1.ID,
                    ToPostConntactNumber = t.PostContact1.ConnectionNo,


                    ToCabinetInputID = t.ToCabinetInputID,

                }
                                   ).ToList();
            }
        }

        public static void DeleteExchangeCabinetInputByRequestID(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ExchangeCabinetInputConncetion> tem = context.ExchangeCabinetInputConncetions.Where(t => t.RequestID == reqeustID).ToList();
                context.ExchangeCabinetInputConncetions.DeleteAllOnSubmit(tem);
                context.SubmitChanges();
            }
        }

        public static List<ExchangeCabinetInputRequestReportInfo> GetExchangeCabinetInputInfo(ExchangeCabinetInput exchangeCabinetInput)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.CommandTimeout = 0;
                IQueryable<ExchangeCabinetInputRequestReportInfo> query = context.ExchangeCabinetInputConncetions
                                                                                 .GroupJoin(context.ADSLPAPPorts, eci => eci.FromTelephoneNo, adp => adp.TelephoneNo, (eci, adps) => new { _ExchangeCabinetInputConnection = eci, AdslPapPorts = adps })
                                                                                 .SelectMany(a => a.AdslPapPorts.DefaultIfEmpty(), (a, adp) => new { _ExchangeCabinetInputConnection = a._ExchangeCabinetInputConnection, AdslPapPort = adp })
                                                                                 .Where(t => t._ExchangeCabinetInputConnection.RequestID == exchangeCabinetInput.ID)
                                                                                 .OrderBy(t => t._ExchangeCabinetInputConnection.Bucht.CabinetInput.Cabinet.CabinetNumber)
                                                                                 .ThenBy(t => t._ExchangeCabinetInputConnection.Bucht.PostContact.Post.Number)
                                                                                 .ThenBy(t => t._ExchangeCabinetInputConnection.Bucht.PostContact.ConnectionNo)
                    .Select(t => new ExchangeCabinetInputRequestReportInfo
                    {
                        OldCabinet = t._ExchangeCabinetInputConnection.Bucht.CabinetInput.CabinetID,
                        OldCabinetNumber = t._ExchangeCabinetInputConnection.Bucht.CabinetInput.Cabinet.CabinetNumber,
                        OldPostNumber = t._ExchangeCabinetInputConnection.Bucht.PostContact.Post.Number,
                        OldPostID = t._ExchangeCabinetInputConnection.Bucht.PostContact.PostID,
                        OldPostContactNumber = t._ExchangeCabinetInputConnection.Bucht.PostContact.ConnectionNo,
                        OldPostContactID = t._ExchangeCabinetInputConnection.Bucht.ConnectionID,
                        OldCabinetInputID = t._ExchangeCabinetInputConnection.Bucht.CabinetInput.ID,
                        OldCabinetInputNumber = t._ExchangeCabinetInputConnection.Bucht.CabinetInput.InputNumber,
                        OldBuchtID = t._ExchangeCabinetInputConnection.Bucht.ID,
                        OldConnectionNo = "ام دی اف: " + t._ExchangeCabinetInputConnection.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t._ExchangeCabinetInputConnection.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t._ExchangeCabinetInputConnection.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t._ExchangeCabinetInputConnection.Bucht.BuchtNo,
                        OldColumnNo = t._ExchangeCabinetInputConnection.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        OldRowNo = t._ExchangeCabinetInputConnection.Bucht.VerticalMDFRow.VerticalRowNo,
                        OldBuchtNo = t._ExchangeCabinetInputConnection.Bucht.BuchtNo,

                        OtherConnectionNo = "ام دی اف: " + t._ExchangeCabinetInputConnection.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t._ExchangeCabinetInputConnection.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t._ExchangeCabinetInputConnection.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t._ExchangeCabinetInputConnection.Bucht.Bucht1.BuchtNo,
                        OtherColumnNo = t._ExchangeCabinetInputConnection.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        OtherRowNo = t._ExchangeCabinetInputConnection.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo,
                        OtherBuchtNo = t._ExchangeCabinetInputConnection.Bucht.Bucht1.BuchtNo,

                        OldBuchtStatus = t._ExchangeCabinetInputConnection.Bucht.Status,
                        Type = t._ExchangeCabinetInputConnection.Bucht.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM ? "پی سی ام" : "معمولی",
                        OldTelephonNo = t._ExchangeCabinetInputConnection.FromTelephoneNo != null ? t._ExchangeCabinetInputConnection.FromTelephoneNo : context.Buchts.Where(t2 => t2.CabinetInputID == t._ExchangeCabinetInputConnection.Bucht.CabinetInputID && t2.SwitchPortID != null).OrderBy(t2 => t2.BuchtNo).Take(1).Select(t2 => t2.SwitchPort.Telephones.Take(1).SingleOrDefault().TelephoneNo).SingleOrDefault(),
                        CustomerName = t._ExchangeCabinetInputConnection.Telephone != null ? ((t._ExchangeCabinetInputConnection.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t._ExchangeCabinetInputConnection.Telephone.Customer.LastName ?? string.Empty)) : string.Empty,
                        PostallCode = t._ExchangeCabinetInputConnection.Telephone != null ? t._ExchangeCabinetInputConnection.Telephone.Address.PostalCode : string.Empty,
                        OldSwitchPortID = t._ExchangeCabinetInputConnection.Bucht.SwitchPortID,
                        OldIsVIP = t._ExchangeCabinetInputConnection.Telephone.IsVIP,
                        OldIsRound = t._ExchangeCabinetInputConnection.Telephone.IsRound,

                        NewCabinet = t._ExchangeCabinetInputConnection.Bucht1.CabinetInput.CabinetID,
                        NewCabinetNumber = t._ExchangeCabinetInputConnection.Bucht1.CabinetInput.Cabinet.CabinetNumber,
                        NewCabinetInputID = t._ExchangeCabinetInputConnection.Bucht1.CabinetInput.ID,
                        NewCabinetInputNumber = t._ExchangeCabinetInputConnection.Bucht1.CabinetInput.InputNumber,
                        NewBuchtID = t._ExchangeCabinetInputConnection.Bucht1.ID,
                        NewConnectionNo = "ام دی اف: " + t._ExchangeCabinetInputConnection.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t._ExchangeCabinetInputConnection.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t._ExchangeCabinetInputConnection.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t._ExchangeCabinetInputConnection.Bucht1.BuchtNo,
                        NewColumnNo = t._ExchangeCabinetInputConnection.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        NewRowNo = t._ExchangeCabinetInputConnection.Bucht1.VerticalMDFRow.VerticalRowNo,
                        NewBuchtNo = t._ExchangeCabinetInputConnection.Bucht1.BuchtNo,

                        NewBuchtStatus = t._ExchangeCabinetInputConnection.Bucht1.Status,
                        NewPostNumber = t._ExchangeCabinetInputConnection.Post1.Number,
                        NewPostID = t._ExchangeCabinetInputConnection.Post1.ID,
                        NewPostConntactNumber = t._ExchangeCabinetInputConnection.PostContact1.ConnectionNo,
                        NewPostContactID = t._ExchangeCabinetInputConnection.PostContact1.ID,

                        AdslBuchtNo = t.AdslPapPort.BuchtNo,
                        AdslColumnNo = t.AdslPapPort.RowNo,
                        AdslRowNo = t.AdslPapPort.ColumnNo
                    });
                return query.ToList();
            }
        }
    }
}
