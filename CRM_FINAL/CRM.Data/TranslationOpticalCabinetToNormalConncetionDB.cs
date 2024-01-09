using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TranslationOpticalCabinetToNormalConncetionDB
    {
        public static void DeleteTranslationOpticalCabinetToNormalConncetionByRequestID(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TranslationOpticalCabinetToNormalConncetion> tem = context.TranslationOpticalCabinetToNormalConncetions.Where(t => t.RequestID == reqeustID).ToList();
                context.TranslationOpticalCabinetToNormalConncetions.DeleteAllOnSubmit(tem);
                context.SubmitChanges();
            }
        }

        public static List<TranslationOpticalCabinetToNormalConnctionInfo> GetTranslationOpticalCabinetToNormalConncetionInfoByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationOpticalCabinetToNormalConncetions.Where(t => t.RequestID == requestID)
                          .Select(t => new TranslationOpticalCabinetToNormalConnctionInfo
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
                              ToConnectiontype = (t.PostContact1.ConnectionType != (int)DB.PostContactConnectionType.Noraml ? "پی سی ام" : "معمولی"),
                              ToCabinetInputID = t.ToCabinetInputID,
                          }
                                   ).ToList();
            }
        }

        public static List<TranslationOpticalCabinetToNormalConncetion> GetTranslationOpticalCabinetToNormalConncetionByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationOpticalCabinetToNormalConncetions.Where(t => t.RequestID == requestID).ToList();

            }
        }


        public static List<TranslationOpticalCabinetToNormalInfo> GetTranslationOpticalCabinetToNormalConncetionInfoByRequestIDs(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TranslationOpticalCabinetToNormalInfo> oldTranslationOpticalCabinetToNormalInfo = new List<TranslationOpticalCabinetToNormalInfo>();
                // List<TranslationOpticalCabinetToNormalInfo> newTranslationOpticalCabinetToNormalInfo = new List<TranslationOpticalCabinetToNormalInfo>();

                oldTranslationOpticalCabinetToNormalInfo = context.TranslationOpticalCabinetToNormalConncetions.Where(t => requestIDs.Contains(t.RequestID))
                .Select(t => new TranslationOpticalCabinetToNormalInfo
                {
                    CustomerAddress = t.Address.AddressContent,
                    PostallCode = t.Address.PostalCode,
                    OldTelephonNo = t.FromTelephoneNo,
                    NewTelephonNo = t.ToTelephoneNo,
                    CustomerName = (t.Customer.FirstNameOrTitle ?? "") + " " + (t.Customer.LastName ?? ""),
                    OldCabinet = t.Bucht.CabinetInput.CabinetID,
                    OldCabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                    OldPostNumber = t.Post.Number,
                    OldPostContactNumber = t.PostContact.ConnectionNo,
                    OldPostContactID = t.PostContact.ID,
                    OldCabinetInputID = t.Bucht.CabinetInputID,
                    OldCabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                    OldBuchtID = t.Bucht.ID,
                    OldConnectionNo = "ام دی اف: " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.Bucht.BuchtNo,
                    OtherConnectionNo = "ام دی اف: " + t.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.Bucht.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.Bucht.Bucht1.BuchtNo,
                    OldBuchtStatus = t.Bucht.Status,
                    OldSwitchPortID = t.Bucht.SwitchPortID,
                    OldIsVIP = t.Telephone.IsVIP,
                    OldIsRound = t.Telephone.IsRound,
                    NewCabinet = t.CabinetInput1.CabinetID,
                    NewCabinetNumber = t.CabinetInput1.Cabinet.CabinetNumber,
                    NewCabinetInputID = t.CabinetInput1.ID,
                    NewCabinetInputNumber = t.CabinetInput1.InputNumber,
                    NewBuchtID = t.Bucht1.ID,
                    NewPostNumber = t.Post1.Number,
                    NewPostConntactNumber = t.PostContact1.ConnectionNo,
                    NewPostContactID = t.PostContact1.ID,
                    NewConnectionNo = "ام دی اف: " + t.Bucht1.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.Bucht1.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.Bucht1.BuchtNo,
                    NewBuchtStatus = t.Bucht1.Status,


                }).ToList();

                //List<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo> translationOpticalCabinetToNormalConncetion = context.TranslationOpticalCabinetToNormalConncetions.Where(t => requestIDs.Contains(t.RequestID)).Select(t => new TranslationOpticalCabinetToNormalConnctionInfo
                //          {
                //              FromPostID = t.FromPostID,
                //              FromPostNumber = t.Post.Number,

                //              FromPostContactID = t.FromPostContactID,
                //              FromPostConntactNumber = t.PostContact.ConnectionNo,

                //              ToPostID = t.ToPostID,
                //              ToPostNumber = t.Post1.Number,

                //              ToPostConntactID = t.ToPostConntactID,
                //              ToPostConntactNumber = t.PostContact1.ConnectionNo,

                //              ToCabinetInputID = t.ToCabinetInputID,

                //          }
                //                   ).ToList();

                //// load old cabinet information
                //oldTranslationOpticalCabinetToNormalInfo = context.Buchts
                //                               .GroupJoin(context.Buchts, gob => gob.BuchtIDConnectedOtherBucht, gotb => gotb.ID, (gob, gotb) => new { GBaseBcuht = gob, GOtherBucht = gotb })
                //                               .SelectMany(t => t.GOtherBucht.DefaultIfEmpty(), (gob, t) => new { OtherBucht = t, Bucht = gob.GBaseBcuht })
                //                               .GroupJoin(context.Telephones, bu => bu.Bucht.SwitchPortID, tel => tel.SwitchPortID, (bu, tel) => new { b1 = bu, t1 = tel })
                //                               .SelectMany(t => t.t1.DefaultIfEmpty(), (b, t) => new { Bucht = b.b1, telephone = t })
                //                               .Where(t => translationOpticalCabinetToNormalConncetion.Select(x => x.FromPostContactID).Contains(t.Bucht.Bucht.ConnectionID))
                //                               .Select(t => new TranslationOpticalCabinetToNormalInfo
                //                               {
                //                                   OldCabinet = t.Bucht.Bucht.CabinetInput.CabinetID,
                //                                   OldCabinetNumber = t.Bucht.Bucht.CabinetInput.Cabinet.CabinetNumber,
                //                                   OldPostNumber = t.Bucht.Bucht.PostContact.Post.Number,
                //                                   OldPostContactNumber = t.Bucht.Bucht.PostContact.ConnectionNo,
                //                                   OldPostContactID = t.Bucht.Bucht.ConnectionID,
                //                                   OldCabinetInputID = t.Bucht.Bucht.CabinetInput.ID,
                //                                   OldCabinetInputNumber = t.Bucht.Bucht.CabinetInput.InputNumber,
                //                                   OldBuchtID = t.Bucht.Bucht.ID,
                //                                   OldConnectionNo = "ام دی اف: " + t.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.Bucht.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.Bucht.Bucht.BuchtNo,
                //                                   OtherConnectionNo = "ام دی اف: " + t.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.Bucht.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.Bucht.OtherBucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.Bucht.OtherBucht.BuchtNo,
                //                                   OldBuchtStatus = t.Bucht.Bucht.Status,
                //                                   OldTelephonNo = t.telephone.TelephoneNo,
                //                                   OldSwitchPortID = t.Bucht.Bucht.SwitchPortID,
                //                                   OldIsVIP = t.telephone.IsVIP,
                //                                   OldIsRound = t.telephone.IsRound,
                //                                   CustomerName = (t.telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.telephone.Customer.LastName ?? string.Empty),
                //                                   CustomerAddress = t.telephone.Address.AddressContent,
                //                                   PostallCode = t.telephone.Address.PostalCode,

                //                               }).OrderBy(t => t.OldCabinetInputNumber).ToList();

                //// load new cabinet information
                //newTranslationOpticalCabinetToNormalInfo = context.Buchts
                //                 .GroupJoin(context.Telephones, bu => bu.SwitchPortID, tel => tel.SwitchPortID, (bu, tel) => new { b1 = bu, t1 = tel })
                //                 .SelectMany(t => t.t1.DefaultIfEmpty(), (b, t) => new { Bucht = b.b1, telephone = t })
                //                 .Where(t => translationOpticalCabinetToNormalConncetion.Select(x => x.ToCabinetInputID).Contains(t.Bucht.CabinetInputID))
                //                 .Select(t => new TranslationOpticalCabinetToNormalInfo
                //                 {
                //                     NewCabinet = t.Bucht.CabinetInput.CabinetID,
                //                     NewCabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                //                     NewCabinetInputID = t.Bucht.CabinetInput.ID,
                //                     NewCabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                //                     NewBuchtID = t.Bucht.ID,
                //                     NewConnectionNo = "ام دی اف: " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number + " , " + "ردیف:" + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه: " + t.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی: " + t.Bucht.BuchtNo,
                //                     NewBuchtStatus = t.Bucht.Status,
                //                 }).OrderBy(t => t.NewCabinetInputNumber).ToList();


                //// merge old information with new infromation
                //int count = oldTranslationOpticalCabinetToNormalInfo.Count();
                //if (count >= newTranslationOpticalCabinetToNormalInfo.Count())

                //for (int i = 0; i < count; i++)
                //{
                //    oldTranslationOpticalCabinetToNormalInfo[i].NewCabinet = newTranslationOpticalCabinetToNormalInfo[i].NewCabinet;
                //    oldTranslationOpticalCabinetToNormalInfo[i].NewCabinetNumber = newTranslationOpticalCabinetToNormalInfo[i].NewCabinetNumber;
                //    oldTranslationOpticalCabinetToNormalInfo[i].NewCabinetInputID = newTranslationOpticalCabinetToNormalInfo[i].NewCabinetInputID;
                //    oldTranslationOpticalCabinetToNormalInfo[i].NewCabinetInputNumber = newTranslationOpticalCabinetToNormalInfo[i].NewCabinetInputNumber;
                //    oldTranslationOpticalCabinetToNormalInfo[i].NewBuchtStatus = newTranslationOpticalCabinetToNormalInfo[i].NewBuchtStatus;
                //    oldTranslationOpticalCabinetToNormalInfo[i].NewBuchtID = newTranslationOpticalCabinetToNormalInfo[i].NewBuchtID;
                //    oldTranslationOpticalCabinetToNormalInfo[i].NewConnectionNo = newTranslationOpticalCabinetToNormalInfo[i].NewConnectionNo;
                //}

                //// load new telehoneNo
                //List<TranslationOpticalCabinetToNormalTelephone> translationOpticalCabinetToNormalTelephones = context.TranslationOpticalCabinetToNormalTelephones.Where(t => requestIDs.Contains(t.RequestID)).ToList();

                //// assign new telephone
                //translationOpticalCabinetToNormalTelephones.ForEach(item =>
                //{
                //    if (oldTranslationOpticalCabinetToNormalInfo.Any(t => t.OldTelephonNo == item.TelephoneNo))
                //    {
                //        oldTranslationOpticalCabinetToNormalInfo.Where(t => t.OldTelephonNo == item.TelephoneNo).SingleOrDefault().NewTelephonNo = item.NewTelephoneNo;
                //        oldTranslationOpticalCabinetToNormalInfo.Where(t => t.OldTelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeID = item.NewSwitchPrecodeID;
                //        oldTranslationOpticalCabinetToNormalInfo.Where(t => t.OldTelephonNo == item.TelephoneNo).SingleOrDefault().OldCounter = item.OldCounter;
                //        oldTranslationOpticalCabinetToNormalInfo.Where(t => t.OldTelephonNo == item.TelephoneNo).SingleOrDefault().NewCounter = item.NewCounter;
                //        oldTranslationOpticalCabinetToNormalInfo.Where(t => t.OldTelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeNumber = context.SwitchPrecodes.Where(t => t.ID == item.NewSwitchPrecodeID).SingleOrDefault().SwitchPreNo;
                //    }
                //});



                //    translationOpticalCabinetToNormalConncetion.ForEach(item =>
                //    {
                //        if (oldTranslationOpticalCabinetToNormalInfo.Any(t => t.NewCabinetInputID == item.ToCabinetInputID))
                //        {
                //            oldTranslationOpticalCabinetToNormalInfo.Where(t => t.NewCabinetInputID == item.ToCabinetInputID).SingleOrDefault().NewPostNumber = item.ToPostNumber;
                //            oldTranslationOpticalCabinetToNormalInfo.Where(t => t.NewCabinetInputID == item.ToCabinetInputID).SingleOrDefault().NewPostConntactNumber = item.ToPostConntactNumber;
                //            oldTranslationOpticalCabinetToNormalInfo.Where(t => t.NewCabinetInputID == item.ToCabinetInputID).SingleOrDefault().NewPostContactID = item.ToPostConntactID;
                //        }
                //    });

                return oldTranslationOpticalCabinetToNormalInfo.ToList();
            }
        }

        public static List<TranslationOpticalCabinetToNormalInfo> GetTranslationOpticalCabinetToNormalConncetionInfoSummariesByRequestIDs(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TranslationOpticalCabinetToNormalInfo> oldTranslationOpticalCabinetToNormalInfo = new List<TranslationOpticalCabinetToNormalInfo>();
                oldTranslationOpticalCabinetToNormalInfo = context.TranslationOpticalCabinetToNormalConncetions
                                                                  .GroupJoin(context.ADSLPAPPorts, to => to.FromTelephoneNo, adp => adp.TelephoneNo, (to, adps) => new { TranslationOpticalCabinetToNormalConncetions = to, AdslPapPorts = adps })
                                                                  .SelectMany(a => a.AdslPapPorts.DefaultIfEmpty(), (a, adp) => new { TranslationOpticalCabinetToNormalConncetions = a.TranslationOpticalCabinetToNormalConncetions, Adsl = adp })

                                                                  .Where(t => requestIDs.Contains(t.TranslationOpticalCabinetToNormalConncetions.RequestID))
                                                                  .Select(t => new TranslationOpticalCabinetToNormalInfo
                                                                                {
                                                                                    OldTelephonNo = t.TranslationOpticalCabinetToNormalConncetions.FromTelephoneNo,
                                                                                    NewTelephonNo = t.TranslationOpticalCabinetToNormalConncetions.ToTelephoneNo,
                                                                                    NewColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                                                    NewRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.VerticalMDFRow.VerticalRowNo,
                                                                                    NewBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht1.BuchtNo,
                                                                                    OldColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                                                    OldRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.VerticalMDFRow.VerticalRowNo,
                                                                                    OldBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtNo,
                                                                                    PcmCabinetInputColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht2.ColumnNo,
                                                                                    PcmCabinetInputRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht2.RowNo,
                                                                                    PcmCabinetInputBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht2.BuchtNo,

                                                                                    //PcmCabinetInputColumnNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                                                    //                          t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo :
                                                                                    //                          context.Buchts
                                                                                    //                                 .Where(b =>
                                                                                    //                                            (b.CabinetInputID == t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInputID) &&
                                                                                    //                                            (b.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                                                    //                                       )
                                                                                    //                                 .Select(b => b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                                                                    //                                 .SingleOrDefault(),
                                                                                    //PcmCabinetInputRowNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                                                    //                       t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo :
                                                                                    //                       context.Buchts
                                                                                    //                              .Where(b =>
                                                                                    //                                         (b.CabinetInputID == t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInputID) &&
                                                                                    //                                         (b.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                                                    //                                    )
                                                                                    //                              .Select(b => b.VerticalMDFRow.VerticalRowNo)
                                                                                    //                              .SingleOrDefault(),
                                                                                    //PcmCabinetInputBuchtNo = t.TranslationOpticalCabinetToNormalConncetions.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                                                    //                         t.TranslationOpticalCabinetToNormalConncetions.Bucht.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo :
                                                                                    //                         context.Buchts
                                                                                    //                                .Where(b =>
                                                                                    //                                           (b.CabinetInputID == t.TranslationOpticalCabinetToNormalConncetions.Bucht.CabinetInputID) &&
                                                                                    //                                           (b.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                                                    //                                      )
                                                                                    //                                .Select(b => b.BuchtNo)
                                                                                    //                                .SingleOrDefault(),
                                                                                    AdslBuchtNo = t.Adsl.BuchtNo,
                                                                                    AdslColumnNo = t.Adsl.RowNo,
                                                                                    AdslRowNo = t.Adsl.ColumnNo
                                                                                }
                                                                         )
                                                                  .ToList();
                return oldTranslationOpticalCabinetToNormalInfo;
            }
        }
    }
}
