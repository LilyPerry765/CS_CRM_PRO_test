using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TranslationOpticalCabinetToNormalDB
    {
        public static TranslationOpticalCabinetToNormal GetTranslationOpticalCabinetToNormal(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationOpticalCabinetToNormals.Where(t => t.ID == requestID).SingleOrDefault();
            }
        }

        //milad doran
        //public static List<TranslationOpticalCabinetToNormalInfo> GetEquivalentCabinetInputs(TranslationOpticalCabinetToNormal translationOpticalCabinetToNormal)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        context.CommandTimeout = 0;
        //        IQueryable<TranslationOpticalCabinetToNormalInfo> query = context.TranslationOpticalCabinetToNormalConncetions

        //                                                                         .GroupJoin(context.ADSLPAPPorts, to => to.FromTelephoneNo, adp => adp.TelephoneNo, (to, adps) => new { TranslationOpticalCabinetToNormalConncetions = to, AdslPapPorts = adps })
        //                                                                         .SelectMany(a => a.AdslPapPorts.DefaultIfEmpty(), (a, adp) => new { TranslationOpticalCabinetToNormalConncetions = a.TranslationOpticalCabinetToNormalConncetions, Adsl = adp })

        //                                                                         .Where(t => t.TranslationOpticalCabinetToNormalConncetions.RequestID == translationOpticalCabinetToNormal.ID)

        //                                                                         .OrderBy(t => t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.Cabinet.CabinetNumber)

        //                                                                         .ThenBy(t => t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.Post.Number)

        //                                                                         .ThenBy(t => t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.ConnectionNo)

        //        .Select(t => new TranslationOpticalCabinetToNormalInfo
        //         {
        //             ///////
        //             OldCabinet = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.CabinetID,
        //             OldCabinetNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.Cabinet.CabinetNumber,
        //             OldPostNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.Post.Number,
        //             OldPostID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.PostID,
        //             OldPostContactNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.ConnectionNo,
        //             OldPostContactID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.ID,
        //             OldCabinetInputID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.ID,
        //             OldCabinetInputNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.InputNumber,
        //             OldBuchtID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.ID,
        //             OldConnectionNo = "ام دی اف: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtNo,
        //             OldColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //             OldRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalRowNo,
        //             OldBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtNo,

        //             OtherConnectionNo = "ام دی اف: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.BuchtNo,
        //             OtherColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //             OtherRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo,
        //             OtherBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.BuchtNo,
        //             AdslBuchtNo = t.Adsl.BuchtNo,
        //             AdslColumnNo = t.Adsl.RowNo,
        //             AdslRowNo = t.Adsl.ColumnNo,



        //             OldBuchtStatus = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Status,
        //             OldTelephonNo = t.TranslationOpticalCabinetToNormalConncetions.FromTelephoneNo,
        //             OldSwitchPortID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.SwitchPortID,
        //             OldIsVIP = t.TranslationOpticalCabinetToNormalConncetions.Telephone.IsVIP,
        //             OldIsRound = t.TranslationOpticalCabinetToNormalConncetions.Telephone.IsRound,
        //             CustomerName = t.TranslationOpticalCabinetToNormalConncetions.Telephone.Customer != null ? ((t.TranslationOpticalCabinetToNormalConncetions.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.TranslationOpticalCabinetToNormalConncetions.Telephone.Customer.LastName ?? string.Empty)) : string.Empty,
        //             PostallCode = t.TranslationOpticalCabinetToNormalConncetions.Telephone.Address != null ? t.TranslationOpticalCabinetToNormalConncetions.Telephone.Address.PostalCode : string.Empty,
        //             AfterCustomerName = t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Customer != null ? ((t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Customer.LastName ?? string.Empty)) : string.Empty,
        //             AfterPostallCode = t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Address != null ? t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Address.PostalCode : string.Empty,
        //             OldCounter = t.TranslationOpticalCabinetToNormalConncetions.FromCounter,

        //             ///////

        //             //////

        //             NewCabinet = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.CabinetID,
        //             NewCabinetNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.Cabinet.CabinetNumber,
        //             NewCabinetInputID = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.ID,
        //             NewCabinetInputNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.InputNumber,
        //             NewBuchtID = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.ID,
        //             NewConnectionNo = "ام دی اف: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.BuchtNo,
        //             NewColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //             NewRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalRowNo,
        //             NewBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.BuchtNo,

        //             NewBuchtStatus = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.Status,
        //             NewTelephonNo = t.TranslationOpticalCabinetToNormalConncetions.ToTelephoneNo,
        //             NewPreCodeID = (int?)t.TranslationOpticalCabinetToNormalConncetions.ToSwitchPrecodeID,
        //             NewCounter = t.TranslationOpticalCabinetToNormalConncetions.ToCounter,
        //             NewPreCodeNumber = context.SwitchPrecodes.Where(t2 => t2.ID == t.TranslationOpticalCabinetToNormalConncetions.ToSwitchPrecodeID).Select(t2 => t2.SwitchPreNo).SingleOrDefault(),
        //             NewPostNumber = t.TranslationOpticalCabinetToNormalConncetions.Post1.Number,
        //             NewPostID = t.TranslationOpticalCabinetToNormalConncetions.Post1.ID,
        //             NewPostConntactNumber = t.TranslationOpticalCabinetToNormalConncetions.PostContact1.ConnectionNo,
        //             NewPostContactID = t.TranslationOpticalCabinetToNormalConncetions.PostContact1.ID,

        //             /////////


        //             TelephoneClass = Helpers.GetEnumDescription(t.TranslationOpticalCabinetToNormalConncetions.Telephone1.ClassTelephone, typeof(DB.ClassTelephone)),
        //             CompletionDate = context.TranslationOpticalCabinetToNormals.Where(t2 => t2.ID == t.TranslationOpticalCabinetToNormalConncetions.RequestID).Select(t2 => t2.CompletionDate).SingleOrDefault().ToPersian(Date.DateStringType.Short),
        //             SpecialService = string.Join(",", context.TelephoneSpecialServiceTypes.Where(t2 => t2.TelephoneNo == t.TranslationOpticalCabinetToNormalConncetions.ToTelephoneNo).Select(t3 => t3.SpecialServiceType.Title).ToList())


        //         });

        //        return query.ToList();
        //    }

        //}

        //TODO:rad 13950624


        //TODO:rad 13950623
        public static List<TranslationOpticalCabinetToNormalInfo> GetEquivalentCabinetInputs(TranslationOpticalCabinetToNormal translationOpticalCabinetToNormal)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.CommandTimeout = 0;
                IQueryable<TranslationOpticalCabinetToNormalInfo> query = context.TranslationOpticalCabinetToNormalConncetions

                                                                                 .GroupJoin(context.ADSLPAPPorts, to => to.FromTelephoneNo, adp => adp.TelephoneNo, (to, adps) => new { TranslationOpticalCabinetToNormalConncetions = to, AdslPapPorts = adps })
                                                                                 .SelectMany(a => a.AdslPapPorts.DefaultIfEmpty(), (a, adp) => new { TranslationOpticalCabinetToNormalConncetions = a.TranslationOpticalCabinetToNormalConncetions, Adsl = adp })

                                                                                 .Where(t => t.TranslationOpticalCabinetToNormalConncetions.RequestID == translationOpticalCabinetToNormal.ID)

                                                                                 .OrderBy(t => t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.Cabinet.CabinetNumber)

                                                                                 .ThenBy(t => t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.Post.Number)

                                                                                 .ThenBy(t => t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.ConnectionNo)

                .Select(t => new TranslationOpticalCabinetToNormalInfo
                {
                    ///////
                    OldCabinet = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.CabinetID,
                    OldCabinetNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.Cabinet.CabinetNumber,
                    OldPostNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.Post.Number,
                    OldPostID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.PostID,
                    OldPostContactNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.ConnectionNo,
                    OldPostContactID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PostContact.ID,
                    OldCabinetInputID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.ID,
                    OldCabinetInputNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInput.InputNumber,
                    OldBuchtID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.ID,
                    OldConnectionNo = "ام دی اف: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtNo,
                    OldColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    OldRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalRowNo,
                    OldBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtNo,

                    OldPcmRockId = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPort.PCM.PCMShelf.PCMRockID,
                    OldPcmRockNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPort.PCM.PCMShelf.PCMRock.Number,
                    OldPcmShelfId = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPort.PCM.ShelfID,
                    OldPcmShelfNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPort.PCM.PCMShelf.Number,
                    OldPcmId = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPort.PCMID,
                    OldPcmCard = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPort.PCM.Card,
                    OldPcmPortId = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPortID,
                    OldPcmPortNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht.PCMPort.PortNumber,

                    OtherConnectionNo = "ام دی اف: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.BuchtNo,
                    OtherColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    OtherRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo,
                    OtherBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.BuchtNo,
                    AdslBuchtNo = t.Adsl.BuchtNo,
                    AdslColumnNo = t.Adsl.RowNo,
                    AdslRowNo = t.Adsl.ColumnNo,

                    OldBuchtStatus = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Status,
                    OldTelephonNo = t.TranslationOpticalCabinetToNormalConncetions.FromTelephoneNo,
                    OldSwitchPortID = t.TranslationOpticalCabinetToNormalConncetions.Bucht.SwitchPortID,
                    OldIsVIP = t.TranslationOpticalCabinetToNormalConncetions.Telephone.IsVIP,
                    OldIsRound = t.TranslationOpticalCabinetToNormalConncetions.Telephone.IsRound,
                    CustomerName = t.TranslationOpticalCabinetToNormalConncetions.Telephone.Customer != null ? ((t.TranslationOpticalCabinetToNormalConncetions.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.TranslationOpticalCabinetToNormalConncetions.Telephone.Customer.LastName ?? string.Empty)) : string.Empty,
                    PostallCode = t.TranslationOpticalCabinetToNormalConncetions.Telephone.Address != null ? t.TranslationOpticalCabinetToNormalConncetions.Telephone.Address.PostalCode : string.Empty,
                    AfterCustomerName = t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Customer != null ? ((t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Customer.LastName ?? string.Empty)) : string.Empty,
                    AfterPostallCode = t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Address != null ? t.TranslationOpticalCabinetToNormalConncetions.Telephone1.Address.PostalCode : string.Empty,
                    OldCounter = t.TranslationOpticalCabinetToNormalConncetions.FromCounter,

                    NewCabinet = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.CabinetID,
                    NewCabinetNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.Cabinet.CabinetNumber,
                    NewCabinetInputID = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.ID,
                    NewCabinetInputNumber = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.CabinetInput.InputNumber,
                    NewBuchtID = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.ID,
                    NewConnectionNo = "ام دی اف: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.TranslationOpticalCabinetToNormalConncetions.Bucht1.BuchtNo,
                    NewColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    NewRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalRowNo,
                    NewBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.BuchtNo,

                    NewBuchtStatus = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.Status,
                    NewTelephonNo = t.TranslationOpticalCabinetToNormalConncetions.ToTelephoneNo,
                    NewPreCodeID = (int?)t.TranslationOpticalCabinetToNormalConncetions.ToSwitchPrecodeID,
                    NewCounter = t.TranslationOpticalCabinetToNormalConncetions.ToCounter,
                    NewPreCodeNumber = context.SwitchPrecodes.Where(t2 => t2.ID == t.TranslationOpticalCabinetToNormalConncetions.ToSwitchPrecodeID).Select(t2 => t2.SwitchPreNo).SingleOrDefault(),
                    NewPostNumber = t.TranslationOpticalCabinetToNormalConncetions.Post1.Number,
                    NewPostID = t.TranslationOpticalCabinetToNormalConncetions.Post1.ID,
                    NewPostConntactNumber = t.TranslationOpticalCabinetToNormalConncetions.PostContact1.ConnectionNo,
                    NewPostContactID = t.TranslationOpticalCabinetToNormalConncetions.PostContact1.ID,

                    /////////


                    TelephoneClass = Helpers.GetEnumDescription(t.TranslationOpticalCabinetToNormalConncetions.Telephone1.ClassTelephone, typeof(DB.ClassTelephone)),
                    CompletionDate = context.TranslationOpticalCabinetToNormals.Where(t2 => t2.ID == t.TranslationOpticalCabinetToNormalConncetions.RequestID).Select(t2 => t2.CompletionDate).SingleOrDefault().ToPersian(Date.DateStringType.Short),
                    SpecialService = string.Join(",", context.TelephoneSpecialServiceTypes.Where(t2 => t2.TelephoneNo == t.TranslationOpticalCabinetToNormalConncetions.ToTelephoneNo).Select(t3 => t3.SpecialServiceType.Title).ToList())


                });

                return query.ToList();
            }

        }

        /// <summary>
        /// .این متد لیست پی سی ام ها آزاد شده بعد از عملیات برگردان به کافو نوری ، را برمیگرداند
        /// </summary>
        /// <param name="citiesId"></param>
        /// <param name="centersId"></param>
        /// <param name="fromCompletionDate"></param>
        /// <param name="toCompletionDate"></param>
        /// <param name="forPrint"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<TranslationOpticalCabinetToNormalInfo> GetReleasedPcmAfterTranslationOpticalCabinetToNormal(List<int> citiesId, List<int> centersId, DateTime? fromCompletionDate, DateTime? toCompletionDate, bool forPrint, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (toCompletionDate.HasValue)
                {
                    toCompletionDate = toCompletionDate.Value.AddDays(1);
                }

                List<TranslationOpticalCabinetToNormalInfo> result = new List<TranslationOpticalCabinetToNormalInfo>();

                var query = context.TranslationOpticalCabinetToNormals
                                   .Join(context.TranslationOpticalCabinetToNormalConncetions, tn => tn.ID, tc => tc.RequestID, (tn, tc) => new { _translationOptical = tn, _translationOpticalConnection = tc })

                                   .Join(context.RequestLogs, joinedData => joinedData._translationOptical.Request.ID, rl => rl.RequestID, (a, rl) => new
                                                                                                                                                        {
                                                                                                                                                            _translationOptical = a._translationOptical,
                                                                                                                                                            _translationOpticalConnection = a._translationOpticalConnection,
                                                                                                                                                            _requestLog = rl
                                                                                                                                                        }
                                        )

                                   .Where(joinedData =>
                                       //(joinedData._translationOpticalConnection.Bucht.BuchtTypeID == (int)DB.BuchtType.InLine) &&
                                            (joinedData._translationOptical.Request.EndDate.HasValue) && //درخواست نهایی شده باشد
                                            (joinedData._translationOptical.Type != (byte)DB.TranslationOpticalCabinetToNormalType.Slight) && //نوع برگردان جزیی نباشد
                                            (joinedData._translationOpticalConnection.PostContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal) && 
                                            (joinedData._requestLog.TelephoneNo == joinedData._translationOpticalConnection.FromTelephoneNo) &&
                                            (citiesId.Count == 0 || citiesId.Contains(joinedData._translationOptical.Request.Center.Region.CityID)) &&
                                            (centersId.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(joinedData._translationOptical.Request.CenterID) : centersId.Contains(joinedData._translationOptical.Request.CenterID)) &&
                                            (!fromCompletionDate.HasValue || fromCompletionDate <= joinedData._translationOptical.Request.EndDate) && //تاریخ نهایی شدن درخواست را به عنوان تاریخ برگردان در نظر میگیریم
                                            (!toCompletionDate.HasValue || toCompletionDate >= joinedData._translationOptical.Request.EndDate)
                                         )

                                    .AsEnumerable()

                                    .Select(filteredData => new TranslationOpticalCabinetToNormalInfo
                                                            {
                                                                CityName = filteredData._translationOptical.Request.Center.Region.City.Name,
                                                                CenterName = filteredData._translationOptical.Request.Center.CenterName,
                                                                CompletionDate = filteredData._translationOptical.CompletionDate.ToPersian(Date.DateStringType.Short),
                                                                OldTelephonNo = filteredData._translationOpticalConnection.FromTelephoneNo,
                                                                NewTelephonNo = filteredData._translationOpticalConnection.ToTelephoneNo,

                                                                OldConnectionNo = (filteredData._requestLog.Description.Element("OldBucht") != null) ? filteredData._requestLog.Description.Element("OldBucht").Value : "",

                                                                OldPcmRockNumber = (filteredData._requestLog.Description.Element("OldPcmRockNumber") != null) ?
                                                                                    Convert.ToInt32(filteredData._requestLog.Description.Element("OldPcmRockNumber").Value) :
                                                                                    default(int?),

                                                                OldPcmShelfNumber = (filteredData._requestLog.Description.Element("OldPcmShelfNumber") != null) ?
                                                                                    Convert.ToInt32(filteredData._requestLog.Description.Element("OldPcmShelfNumber").Value) :
                                                                                    default(int?),

                                                                OldPcmCard = (filteredData._requestLog.Description.Element("OldPcmCard") != null) ?
                                                                             Convert.ToInt32(filteredData._requestLog.Description.Element("OldPcmCard").Value) :
                                                                             default(int?),

                                                                OldPcmPortNumber = (filteredData._requestLog.Description.Element("OldPcmPortNumber") != null) ?
                                                                                    Convert.ToInt32(filteredData._requestLog.Description.Element("OldPcmPortNumber").Value) :
                                                                                    default(int?),

                                                                OldPostNumber = (filteredData._requestLog.Description.Element("OldPost") != null) ?
                                                                                 Convert.ToInt32(filteredData._requestLog.Description.Element("OldPost").Value) :
                                                                                 default(int?),

                                                                OldPostContactNumber = (filteredData._requestLog.Description.Element("OldPostContact") != null) ?
                                                                                        Convert.ToInt32(filteredData._requestLog.Description.Element("OldPostContact").Value) :
                                                                                        default(int?),

                                                                OldCabinetNumber = (filteredData._requestLog.Description.Element("OldCabinet") != null) ?
                                                                                   Convert.ToInt32(filteredData._requestLog.Description.Element("OldCabinet").Value) :
                                                                                   default(int?),

                                                                OldCabinetInputNumber = (filteredData._requestLog.Description.Element("OldCabinetInput") != null) ?
                                                                                        Convert.ToInt64(filteredData._requestLog.Description.Element("OldCabinetInput").Value) :
                                                                                        default(long?),

                                                                NewPostNumber = (filteredData._requestLog.Description.Element("NewPost") != null) ?
                                                                                 Convert.ToInt32(filteredData._requestLog.Description.Element("NewPost").Value) :
                                                                                 default(int?),

                                                                NewPostConntactNumber = (filteredData._requestLog.Description.Element("NewPostContact") != null) ?
                                                                                        Convert.ToInt32(filteredData._requestLog.Description.Element("NewPostContact").Value) :
                                                                                        default(int?),

                                                                NewCabinetNumber = (filteredData._requestLog.Description.Element("NewCabinet") != null) ?
                                                                                    Convert.ToInt32(filteredData._requestLog.Description.Element("NewCabinet").Value) :
                                                                                    default(int?),

                                                                NewCabinetInputNumber = (filteredData._requestLog.Description.Element("NewCabinetInput") != null) ?
                                                                                        Convert.ToInt32(filteredData._requestLog.Description.Element("NewCabinetInput").Value) :
                                                                                        default(int?),

                                                                NewConnectionNo = (filteredData._requestLog.Description.Element("NewBucht") != null) ? filteredData._requestLog.Description.Element("NewBucht").Value : "",
                                                            }
                                            );

                if (forPrint)
                {
                    result = query.ToList();
                    count = result.Count;
                }
                else
                {
                    count = query.Count();
                    result = query.Skip(startRowIndex).Take(pageSize).ToList();
                }

                return result;
            }
        }

        public static bool CheckAllBuchtCabinetIsFree(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Buchts.Where(t => t.CabinetInput.Cabinet.ID == cabinetID && t.Status == (byte)DB.BuchtStatus.Free).Count() != 0)
                    return false;
                else
                    return true;
            }
        }

        public static int GetCountCabinetInput(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.CabinetID == cabinetID).Count();
            }
        }



        public static TranslationOpticalCabinetToNormalInfo GetTranslationOpticalCabinetToNormalInfo(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationOpticalCabinetToNormals.Where(t => t.ID == requestID)
                           .Select(t => new TranslationOpticalCabinetToNormalInfo
                           {
                               OldCabinetNumber = t.Cabinet.CabinetNumber,
                               NewCabinetNumber = t.Cabinet1.CabinetNumber,
                           }).SingleOrDefault();
            }
        }

        public static bool ExistPCMInCabinet(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Any(t => t.CabinetInput.Cabinet.ID == cabinetID && t.Status == (byte)DB.BuchtStatus.AllocatedToInlinePCM);

            }
        }

        public static bool ExistSpecialWireInCabinet(int cabinetID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, telephone = t })
                     .Where(t => t.bucht.CabinetInput.Cabinet.ID == cabinetID)
                     .Any(t => t.telephone.UsageType == (int)DB.TelephoneUsageType.E1
                         || t.telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire);
            }
        }

        public static bool ExistSpecialWireInPostContacts(List<long> postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, telephone = t })
                     .Where(t => postContactID.Contains(t.bucht.PostContact.ID))
                     .Any(t => t.telephone.UsageType == (int)DB.TelephoneUsageType.E1
                         || t.telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire);
            }
        }

        public static bool ExistPCMInPostContacts(List<long> postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Any(t => postContactID.Contains(t.ID) && (t.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote));

            }
        }

        public static bool ExistPostCntactReserve(List<long> postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Any(t => postContactID.Contains(t.ID) &&
                    (t.Status == (int)DB.PostContactStatus.FullBooking
                    //||
                    //t.Status == (int)DB.PostContactStatus.ExchangeCenterToCenter ||
                    //t.Status == (int)DB.PostContactStatus.ExchangeCentralCableMDF ||
                    //t.Status == (int)DB.PostContactStatus.ExchangePost)
                    )
                    );

            }
        }

        public static bool ExistTelephoneInRequest(List<long> telephones)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.TranslationOpticalCabinetToNormalConncetions.Where(t => telephones.Contains((long)t.FromTelephoneNo) && t.Request.EndDate == null && t.Request.IsCancelation == false).Any();
            }
        }

        public static List<TranslationOpticalCabinetToNormalInfo> SearchTranslationOpticalCabinetToNormalGeneralInformation(List<int> cities, List<int> centers, List<int> fromCabinets, List<int> toCabinets, List<int> fromCabinetUsageTypes, List<int> toCabinetUsageTypes, DateTime? fromDate, DateTime? toDate, long requestId, long fromTelephoneNo, long toTelephoneNo, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TranslationOpticalCabinetToNormalInfo> result = new List<TranslationOpticalCabinetToNormalInfo>();
                if (toDate.HasValue)
                {
                    toDate = toDate.Value.AddDays(1);
                }
                var query = context.TranslationOpticalCabinetToNormals
                                 .Join(context.TranslationOpticalCabinetToNormalConncetions, tn => tn.ID, tc => tc.RequestID, (tn, tc) => new { TranslationOpticalCabinetToNormal = tn, TranslationOpticalCabinetToNormalConncetion = tc })
                                 .Where(a =>
                                           (a.TranslationOpticalCabinetToNormal.Request.EndDate.HasValue) &&
                                           (cities.Count == 0 || cities.Contains(a.TranslationOpticalCabinetToNormal.Request.Center.Region.CityID)) &&
                                           (centers.Count == 0 || centers.Contains(a.TranslationOpticalCabinetToNormal.Request.CenterID)) &&
                                           (fromCabinets.Count == 0 || fromCabinets.Contains(a.TranslationOpticalCabinetToNormal.OldCabinetID)) &&
                                           (toCabinets.Count == 0 || toCabinets.Contains(a.TranslationOpticalCabinetToNormal.NewCabinetID)) &&
                                           (fromCabinetUsageTypes.Count == 0 || fromCabinetUsageTypes.Contains(a.TranslationOpticalCabinetToNormal.Cabinet.CabinetUsageType)) &&
                                           (toCabinetUsageTypes.Count == 0 || toCabinetUsageTypes.Contains(a.TranslationOpticalCabinetToNormal.Cabinet1.CabinetUsageType)) &&
                                           (!fromDate.HasValue || fromDate <= a.TranslationOpticalCabinetToNormal.CompletionDate) &&
                                           (!toDate.HasValue || toDate >= a.TranslationOpticalCabinetToNormal.CompletionDate) &&
                                           (requestId == -1 || requestId == a.TranslationOpticalCabinetToNormalConncetion.RequestID) &&
                                           (fromTelephoneNo == -1 || fromTelephoneNo == a.TranslationOpticalCabinetToNormalConncetion.FromTelephoneNo) &&
                                           (toTelephoneNo == -1 || toTelephoneNo == a.TranslationOpticalCabinetToNormalConncetion.ToTelephoneNo)
                                       )
                                .Select(a => new TranslationOpticalCabinetToNormalInfo
                                                 {
                                                     RequestID = a.TranslationOpticalCabinetToNormalConncetion.RequestID,
                                                     CityName = a.TranslationOpticalCabinetToNormal.Request.Center.Region.City.Name,
                                                     CenterName = a.TranslationOpticalCabinetToNormal.Request.Center.CenterName,
                                                     OldTelephonNo = a.TranslationOpticalCabinetToNormalConncetion.FromTelephoneNo,
                                                     NewTelephonNo = a.TranslationOpticalCabinetToNormalConncetion.ToTelephoneNo,
                                                     CustomerName = string.Format("{0} {1}", a.TranslationOpticalCabinetToNormalConncetion.Customer.FirstNameOrTitle, a.TranslationOpticalCabinetToNormalConncetion.Customer.LastName),
                                                     OldCabinetNumber = a.TranslationOpticalCabinetToNormal.Cabinet.CabinetNumber,
                                                     NewCabinetNumber = a.TranslationOpticalCabinetToNormal.Cabinet1.CabinetNumber,
                                                     OldCabinetUsageType = Helpers.GetEnumDescription(a.TranslationOpticalCabinetToNormal.OldCabinetUsageTypeID, typeof(DB.CabinetUsageType)),
                                                     NewCabinetUsageType = Helpers.GetEnumDescription(a.TranslationOpticalCabinetToNormal.NewCabinetUsageTypeID, typeof(DB.CabinetUsageType)),
                                                     SpecialService = string.Join(" , ", a.TranslationOpticalCabinetToNormalConncetion.Telephone1.TelephoneSpecialServiceTypes.Select(st => st.SpecialServiceType.Title).ToArray()),
                                                     TelephoneClass = Helpers.GetEnumDescription(a.TranslationOpticalCabinetToNormalConncetion.Telephone1.ClassTelephone, typeof(DB.ClassTelephone)),
                                                     CauseOfCut = a.TranslationOpticalCabinetToNormalConncetion.Telephone1.CauseOfCut.Name,
                                                     HasAdsl = context.ADSLPAPPorts.Where(adp => adp.TelephoneNo == a.TranslationOpticalCabinetToNormalConncetion.ToTelephoneNo).Any(),
                                                     CompletionDate = a.TranslationOpticalCabinetToNormal.CompletionDate.ToPersian(Date.DateStringType.Short)
                                                 }
                                        )
                                .AsQueryable();
                count = query.Count();
                if (pageSize != 0)
                {
                    result = query.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    result = query.ToList();
                }
                return result;

            }
        }
    }
}
