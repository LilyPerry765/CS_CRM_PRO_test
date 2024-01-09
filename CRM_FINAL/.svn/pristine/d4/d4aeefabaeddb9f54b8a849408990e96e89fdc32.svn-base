using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ExchangeCenralCableMDFDB
    {
        public static List<ExchangeCenralCableMDFInfo> SearchExchangeCenralCableMDF(List<int> mDF, List<int> cabinet, List<int> status, string requestLetterNo, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCentralCableMDFs.Where(t =>
                    (string.IsNullOrEmpty(requestLetterNo) || context.Requests.Where(r => r.ID == t.ID).SingleOrDefault().RequestLetterNo == requestLetterNo)
                    )
                    .Select(t => new ExchangeCenralCableMDFInfo
                    {
                        ID = t.ID,
                      
                        InsertDate = t.InsertDate,
                        StatusTitle = context.Requests.Where(r => r.ID == t.ID).SingleOrDefault().Status.Title,
                        RequestLetterNo = context.Requests.Where(r => r.ID == t.ID).SingleOrDefault().RequestLetterNo,
                    }
                    ).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchExchangeCenralCableMDFCount(List<int> mDF, List<int> cabinet, List<int> status, string requestLetterNo)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.ExchangeCentralCableMDFs.Where(t =>(string.IsNullOrEmpty(requestLetterNo) || context.Requests.Where(r => r.ID == t.ID).SingleOrDefault().RequestLetterNo == requestLetterNo)
                    ).Count();
            }
        }

        public static ExchangeCentralCableMDF GetExchangeCentralCableByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCentralCableMDFs.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        public static ExchangeCenralCableMDFTechnicaliInfo GetExchangeCenralCableMDFTechnicaliInfo(ExchangeCentralCableMDF _exchangeCentralCableMDF)
        {
            ExchangeCenralCableMDFTechnicaliInfo exchangeCenralCableMDFTechnicaliInfo = new ExchangeCenralCableMDFTechnicaliInfo();
            using (MainDataContext context = new MainDataContext())
            {
                exchangeCenralCableMDFTechnicaliInfo.Cabinets = context.Cabinets.Where(t => t.CenterID == context.Requests.Single(r => r.ID == _exchangeCentralCableMDF.ID).CenterID).Select(t => new CheckableItem { ID = t.ID, Name = t.CabinetNumber.ToString(), IsChecked = false }).ToList();
               // exchangeCenralCableMDFTechnicaliInfo.CabinetInputs = context.CabinetInputs.Where(t => t.CabinetID == _exchangeCentralCableMDF.Bucht.CabinetID).Select(t => new CheckableItem { LongID = t.ID, Name = t.InputNumber.ToString(), IsChecked = false }).ToList();

                exchangeCenralCableMDFTechnicaliInfo.Cables = context.Cables.Where(t => t.CenterID == context.Requests.Single(r => r.ID == _exchangeCentralCableMDF.ID).CenterID).Select(t => new CheckableItem { LongID = t.ID, Name = t.CableNumber.ToString(), IsChecked = false }).ToList();
            //    exchangeCenralCableMDFTechnicaliInfo.CablePairs = context.CablePairs.Where(t => t.CableID == _exchangeCentralCableMDF.CableID).Select(t => new CheckableItem { LongID = t.ID, Name = t.CablePairNumber.ToString(), IsChecked = false }).ToList();
            }

            return exchangeCenralCableMDFTechnicaliInfo;
        }

        public static void DeleteExchangeCenralCableMDFDBConncetionByRequestID(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ExchangeCentralCableMDFConncetion> tem = context.ExchangeCentralCableMDFConncetions.Where(t => t.RequestID == reqeustID).ToList();
                context.ExchangeCentralCableMDFConncetions.DeleteAllOnSubmit(tem);
                context.SubmitChanges();
            }
        }

        public static List<ExchangeCentralCableMDFConncetion> GetExchangeCentralCableConnectionByID(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCentralCableMDFConncetions.Where(t => t.RequestID == requestID).ToList();
            }
        }

        public static List<TranslationCentralCabinetInfo> GetExchangeInfo(ExchangeCentralCableMDF exchangeCentralCableMDF)
        {
            using (MainDataContext context = new MainDataContext())
            {
                    context.CommandTimeout = 0;
                    IQueryable<TranslationCentralCabinetInfo> query = context.ExchangeCentralCableMDFConncetions
                    .GroupJoin(context.ADSLPAPPorts, to => to.TelephoneNo, adp => adp.TelephoneNo, (to, adps) => new { exchangeCentralCableMDFConncetions = to, AdslPapPorts = adps })
                    .SelectMany(a => a.AdslPapPorts.DefaultIfEmpty(), (a, adp) => new { exchangeCentralCableMDFConncetions = a.exchangeCentralCableMDFConncetions, Adsl = adp })
                    .Where(t => t.exchangeCentralCableMDFConncetions.RequestID == exchangeCentralCableMDF.ID)
                    .OrderBy(t => t.exchangeCentralCableMDFConncetions.Bucht.ColumnNo)
                    .ThenBy(t => t.exchangeCentralCableMDFConncetions.Bucht.RowNo)
                    .ThenBy(t => t.exchangeCentralCableMDFConncetions.Bucht.BuchtNo)
                    .Select(t => new TranslationCentralCabinetInfo
                    {
                        ///////
                        OldCabinet = t.exchangeCentralCableMDFConncetions.Bucht.CabinetInput.CabinetID,
                        OldCabinetNumber = t.exchangeCentralCableMDFConncetions.Bucht.CabinetInput.Cabinet.CabinetNumber,
                        OldPostNumber = t.exchangeCentralCableMDFConncetions.Bucht.PostContact.Post.Number,
                        OldPostID = t.exchangeCentralCableMDFConncetions.Bucht.PostContact.PostID,
                        OldPostContactNumber = t.exchangeCentralCableMDFConncetions.Bucht.PostContact.ConnectionNo,
                        OldPostContactID = t.exchangeCentralCableMDFConncetions.Bucht.PostContact.ID,
                        OldCabinetInputID = t.exchangeCentralCableMDFConncetions.Bucht.CabinetInput.ID,
                        OldCabinetInputNumber = t.exchangeCentralCableMDFConncetions.Bucht.CabinetInput.InputNumber,
                        OldBuchtID = t.exchangeCentralCableMDFConncetions.Bucht.ID,
                        OldConnectionNo = "ام دی اف: " + t.exchangeCentralCableMDFConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.exchangeCentralCableMDFConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.exchangeCentralCableMDFConncetions.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.exchangeCentralCableMDFConncetions.Bucht.BuchtNo,
                        OldColumnNo = t.exchangeCentralCableMDFConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        OldRowNo = t.exchangeCentralCableMDFConncetions.Bucht.VerticalMDFRow.VerticalRowNo,
                        OldBuchtNo = t.exchangeCentralCableMDFConncetions.Bucht.BuchtNo,

                        OtherConnectionNo = "ام دی اف: " + t.exchangeCentralCableMDFConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.exchangeCentralCableMDFConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.exchangeCentralCableMDFConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.exchangeCentralCableMDFConncetions.Bucht.Bucht1.BuchtNo,
                        OtherColumnNo = t.exchangeCentralCableMDFConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        OtherRowNo = t.exchangeCentralCableMDFConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo,
                        OtherBuchtNo = t.exchangeCentralCableMDFConncetions.Bucht.Bucht1.BuchtNo,
                        AdslBuchtNo = t.Adsl.BuchtNo,
                        AdslColumnNo = t.Adsl.RowNo,
                        AdslRowNo = t.Adsl.ColumnNo,



                        OldBuchtStatus = t.exchangeCentralCableMDFConncetions.Bucht.Status,
                        OldTelephonNo = t.exchangeCentralCableMDFConncetions.TelephoneNo,
                        OldSwitchPortID = t.exchangeCentralCableMDFConncetions.Bucht.SwitchPortID,
                        OldIsVIP = t.exchangeCentralCableMDFConncetions.Telephone.IsVIP,
                        OldIsRound = t.exchangeCentralCableMDFConncetions.Telephone.IsRound,
                        CustomerName = t.exchangeCentralCableMDFConncetions.Telephone.Customer != null ? ((t.exchangeCentralCableMDFConncetions.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.exchangeCentralCableMDFConncetions.Telephone.Customer.LastName ?? string.Empty)) : string.Empty,
                        PostallCode = t.exchangeCentralCableMDFConncetions.Telephone.Address != null ? t.exchangeCentralCableMDFConncetions.Telephone.Address.PostalCode : string.Empty,
                 
                        ///////

                        //////

                        NewCabinet = t.exchangeCentralCableMDFConncetions.Bucht1.CabinetInput.CabinetID,
                        NewCabinetNumber = t.exchangeCentralCableMDFConncetions.Bucht1.CabinetInput.Cabinet.CabinetNumber,
                        NewCabinetInputID = t.exchangeCentralCableMDFConncetions.Bucht1.CabinetInput.ID,
                        NewCabinetInputNumber = t.exchangeCentralCableMDFConncetions.Bucht1.CabinetInput.InputNumber,
                        NewBuchtID = t.exchangeCentralCableMDFConncetions.Bucht1.ID,
                        NewConnectionNo = "ام دی اف: " + t.exchangeCentralCableMDFConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.exchangeCentralCableMDFConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.exchangeCentralCableMDFConncetions.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.exchangeCentralCableMDFConncetions.Bucht1.BuchtNo,
                        NewColumnNo = t.exchangeCentralCableMDFConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        NewRowNo = t.exchangeCentralCableMDFConncetions.Bucht1.VerticalMDFRow.VerticalRowNo,
                        NewBuchtNo = t.exchangeCentralCableMDFConncetions.Bucht1.BuchtNo,

                        /////////

                        CompletionDate = context.TranslationOpticalCabinetToNormals.Where(t2 => t2.ID == t.exchangeCentralCableMDFConncetions.RequestID).Select(t2 => t2.CompletionDate).SingleOrDefault().ToPersian(Date.DateStringType.Short),
                        SpecialService = string.Join(",", context.TelephoneSpecialServiceTypes.Where(t2 => t2.TelephoneNo == t.exchangeCentralCableMDFConncetions.TelephoneNo).Select(t3 => t3.SpecialServiceType.Title).ToList())


                    });

                return query.ToList();
            }
        }
    }

}
