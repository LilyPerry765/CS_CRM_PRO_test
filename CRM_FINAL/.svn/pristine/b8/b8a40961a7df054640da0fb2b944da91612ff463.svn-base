using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ExchangeGSMDB
    {
        public static ExchangeGSM GetExchangeGSMByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeGSMs.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static void DeleteExchangeGSMConncetionByRequestID(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ExchangeGSMConnection> tem = context.ExchangeGSMConnections.Where(t => t.RequestID == reqeustID).ToList();
                context.ExchangeGSMConnections.DeleteAllOnSubmit(tem);
                context.SubmitChanges();
            }
        }


        public static List<ExchangeGSMInfo> GetExchangeGSMList(ExchangeGSM exchangeGSM)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.CommandTimeout = 0;
                IQueryable<ExchangeGSMInfo> query = context.ExchangeGSMConnections
                    .GroupJoin(context.ADSLPAPPorts, to => to.FromTelephoneNo, adp => adp.TelephoneNo, (to, adps) => new { ExchangeGSMConncetion = to, AdslPapPorts = adps })
                    .SelectMany(a => a.AdslPapPorts.DefaultIfEmpty(), (a, adp) => new { ExchangeGSMConncetion = a.ExchangeGSMConncetion, Adsl = adp })
                    .Where(t => t.ExchangeGSMConncetion.RequestID == exchangeGSM.ID)
                    .OrderBy(t => t.ExchangeGSMConncetion.Cabinet.CabinetNumber)
                    .ThenBy(t => t.ExchangeGSMConncetion.Post.Number)
                    .ThenBy(t => t.ExchangeGSMConncetion.PostContact.ConnectionNo)
                    .Select(t => new ExchangeGSMInfo
                    {
                        ///////
                        Cabinet = t.ExchangeGSMConncetion.Cabinet.ID,
                        CabinetNumber = t.ExchangeGSMConncetion.Cabinet.CabinetNumber,
                        PostNumber = t.ExchangeGSMConncetion.Post.Number,
                        PostID = t.ExchangeGSMConncetion.Post.ID,
                        PostContactNumber = t.ExchangeGSMConncetion.PostContact.ConnectionNo,
                        PostContactID = t.ExchangeGSMConncetion.PostContact.ID,
                        CabinetInputID = t.ExchangeGSMConncetion.CabinetInput.ID,
                        CabinetInputNumber = t.ExchangeGSMConncetion.CabinetInput.InputNumber,
                        BuchtID = t.ExchangeGSMConncetion.Bucht.ID,
                        ConnectionNo = "ام دی اف: " + t.ExchangeGSMConncetion.Bucht.MDF + " , " + "ردیف:" + t.ExchangeGSMConncetion.Bucht.RowNo.ToString() + " " + "طبقه: " + t.ExchangeGSMConncetion.Bucht.RowNo.ToString() + " ،  " + "اتصالی: " + t.ExchangeGSMConncetion.Bucht.BuchtNo.ToString(),
                        ColumnNo = t.ExchangeGSMConncetion.Bucht.ColumnNo,
                        RowNo = t.ExchangeGSMConncetion.Bucht.RowNo,
                        BuchtNo = t.ExchangeGSMConncetion.Bucht.BuchtNo,
                         
                        OtherConnectionNo = "ام دی اف: " + t.ExchangeGSMConncetion.Bucht.Bucht1.MDF + " , " + "ردیف:" + t.ExchangeGSMConncetion.Bucht.Bucht1.ColumnNo + " " + "طبقه: " + t.ExchangeGSMConncetion.Bucht.Bucht1.RowNo + " ،  " + "اتصالی: " + t.ExchangeGSMConncetion.Bucht.Bucht1.BuchtNo,
                        OtherColumnNo = t.ExchangeGSMConncetion.Bucht.Bucht1.ColumnNo,
                        OtherRowNo = t.ExchangeGSMConncetion.Bucht.Bucht1.RowNo,
                        OtherBuchtNo = t.ExchangeGSMConncetion.Bucht.Bucht1.BuchtNo,
                        AdslBuchtNo = t.Adsl.BuchtNo,
                        AdslColumnNo = t.Adsl.RowNo,
                        AdslRowNo = t.Adsl.ColumnNo,



                        BuchtStatus = t.ExchangeGSMConncetion.Bucht.Status,
                        FromTelephonNo = t.ExchangeGSMConncetion.FromTelephoneNo,
                        ToTelephonNo = t.ExchangeGSMConncetion.ToTelephoneNo,
                        FromTelephoneStatus = t.ExchangeGSMConncetion.Telephone.Status,
                        ToTelephoneStatus = t.ExchangeGSMConncetion.Telephone1.Status,

                        FromSwitchPreCodeID = t.ExchangeGSMConncetion.Telephone.SwitchPrecodeID,
                        ToSwitchPreCodeID = t.ExchangeGSMConncetion.Telephone1.SwitchPrecodeID,
                        
                        IsVIP = t.ExchangeGSMConncetion.Telephone.IsVIP,
                        IsRound = t.ExchangeGSMConncetion.Telephone.IsRound,
                        CustomerName = t.ExchangeGSMConncetion.Telephone.Customer != null ? ((t.ExchangeGSMConncetion.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.ExchangeGSMConncetion.Telephone.Customer.LastName ?? string.Empty)) : string.Empty,
                        PostallCode = t.ExchangeGSMConncetion.Telephone.Address != null ? t.ExchangeGSMConncetion.Telephone.Address.PostalCode : string.Empty,

                        ///////

                        //////



                        TelephoneClass = Helpers.GetEnumDescription(t.ExchangeGSMConncetion.Telephone.ClassTelephone, typeof(DB.ClassTelephone)),
                        CompletionDate = context.TranslationOpticalCabinetToNormals.Where(t2 => t2.ID == t.ExchangeGSMConncetion.RequestID).Select(t2 => t2.CompletionDate).SingleOrDefault().ToPersian(Date.DateStringType.Short),
                        SpecialService = string.Join(",", context.TelephoneSpecialServiceTypes.Where(t2 => t2.TelephoneNo == t.ExchangeGSMConncetion.FromTelephoneNo).Select(t3 => t3.SpecialServiceType.Title).ToList())


                    });

                return query.ToList();
            }

        }



        public static List<ExchangeGSMConnectionInfo> GetExchangeGSMConncetionInfoByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeGSMConnections.Where(t => t.RequestID == requestID)
                          .Select(t => new ExchangeGSMConnectionInfo
                          {
                              CabinetID = t.CabinetID,
                               CabinetInputID = t.CabinetInputID,
                              CabinetNumber = t.Cabinet.CabinetNumber,
                              PostID = t.PostID,
                              PostNumber = t.Post.Number,
                              AorBType = t.Post.AorBType,
                              AorBTypeName = t.Post.AORBPostAndCabinet.Name,
                              PostConntactID = t.PostContactID,
                              PostConntactNumber = t.PostContact.ConnectionNo,
                              Connectiontype = (t.PostContact.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی"),
                          }
                        ).ToList();
            }
        }



    }
}
