using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TechnicalRequestListDB
    {

        public static List<TechnicalRequestInfo> GetChangeTelephone(long requestID, DateTime? fromDateTime, DateTime? toDateTime, List<int> centerIDs, List<int> requestTypeIDs, long oldTelephoneNo, long newTelephoneNo, int startRowIndex, int pageSize, out int count)
        {

            List<TechnicalRequestInfo> changeTelephone = new List<TechnicalRequestInfo>();
            List<TechnicalRequestInfo> translationOpticalCabinetToNormals = new List<TechnicalRequestInfo>();
            List<TechnicalRequestInfo> tranlationPostInput = new List<TechnicalRequestInfo>();
            List<TechnicalRequestInfo> tranlationPost = new List<TechnicalRequestInfo>();
            List<TechnicalRequestInfo> PCMToNormal = new List<TechnicalRequestInfo>();
            List<TechnicalRequestInfo> swapTelephones = new List<TechnicalRequestInfo>();
            List<TechnicalRequestInfo> swapPCM = new List<TechnicalRequestInfo>();
            List<TechnicalRequestInfo> buchtSwiching = new List<TechnicalRequestInfo>();

            using (MainDataContext context = new MainDataContext())
            {
                context.CommandTimeout = 0;

                #region TranslationOpticalCabinetToNormal
                translationOpticalCabinetToNormals = context.TranslationOpticalCabinetToNormalConncetions.Where(t =>
                                                                    (t.Request.EndDate.HasValue) &&
                                                                    (requestID == -1 || t.Request.ID == requestID) &&
                                                                    (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                    (oldTelephoneNo == -1 || t.FromTelephoneNo == oldTelephoneNo) &&
                                                                    (newTelephoneNo == -1 || t.ToTelephoneNo == newTelephoneNo) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                               )
                                                        .Select(t => new TechnicalRequestInfo
                                                        {
                                                            ID = t.ID,
                                                            RequestID = t.Request.ID,
                                                            RequestTypeName = t.Request.RequestType.Title,

                                                            CenterName = t.Request.Center.CenterName,
                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),

                                                            FirstNameOrTitle = t.Telephone1.Customer.FirstNameOrTitle,
                                                            LastName = t.Telephone1.Customer.LastName,
                                                            PersonType = DB.GetEnumDescriptionByValue(typeof(DB.PersonType), t.Telephone1.Customer.PersonType),
                                                            PreCodeTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PreCodeType), t.Telephone1.SwitchPrecode.PreCodeType),

                                                            OldTelephoneNo = t.Telephone.TelephoneNo,
                                                            NewTelephoneNo = t.Telephone1.TelephoneNo,

                                                            OldPost = t.PostContact.Post.Number,
                                                            OldPostContact = t.PostContact.ConnectionNo,
                                                            OldCabinet = t.CabinetInput.Cabinet.CabinetNumber,
                                                            OldCabinetInput = t.CabinetInput.InputNumber,

                                                            NewPost = t.PostContact1.Post.Number,
                                                            NewPostContact = t.PostContact1.ConnectionNo,
                                                            NewCabinet = t.CabinetInput1.Cabinet.CabinetNumber,
                                                            NewCabinetInput = t.CabinetInput1.InputNumber,
                                                        })
                                                       .ToList();

                changeTelephone = changeTelephone.Union(translationOpticalCabinetToNormals).ToList();

                #endregion TranslationOpticalCabinetToNormal

                #region TranlationPostInput
                tranlationPostInput = context.TranslationPostInputConnections.Where(t =>
                                                                    (t.Request.EndDate.HasValue) &&
                                                                    (requestID == -1 || t.Request.ID == requestID) &&
                                                                    (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                               )
                                                        .Select(t => new TechnicalRequestInfo
                                                        {
                                                            ID = t.ID,
                                                            RequestID = t.Request.ID,
                                                            RequestTypeName = t.Request.RequestType.Title,
                                                            CenterName = t.Request.Center.CenterName,
                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),

                                                            OldPost = t.PostContact.Post.Number,
                                                            OldPostContact = t.PostContact.ConnectionNo,
                                                            OldCabinet = t.TranslationPostInput.Cabinet.CabinetNumber,

                                                            NewPost = t.PostContact1.Post.Number,
                                                            NewPostContact = t.PostContact1.ConnectionNo,
                                                            NewCabinet = t.TranslationPostInput.Cabinet1.CabinetNumber,
                                                            NewCabinetInput = t.CabinetInput.InputNumber,
                                                            OldTelephoneNo = context.RequestLogs.Where(t3 => t3.RequestID == t.RequestID).SingleOrDefault().TelephoneNo,

                                                        })
                                                       .ToList();

                changeTelephone = changeTelephone.Union(tranlationPostInput).ToList();
                #endregion TranlationPostInput

                #region tranlationPost
                tranlationPost = context.TranslationPosts.Join(context.RequestLogs, t2 => t2.RequestID, t3 => t3.RequestID, (t2, t3) => new { TranslationPosts = t2, RequestLog = t3 }).Where(t =>
                                                                    (t.TranslationPosts.Request.EndDate.HasValue) &&
                                                                    (requestID == -1 || t.TranslationPosts.Request.ID == requestID) &&
                                                                    (!fromDateTime.HasValue || t.TranslationPosts.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.TranslationPosts.Request.EndDate <= toDateTime) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.TranslationPosts.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.TranslationPosts.Request.RequestTypeID))
                                                               )
                                                        .Select(t => new TechnicalRequestInfo
                                                        {
                                                            ID = t.TranslationPosts.RequestID,
                                                            RequestID = t.TranslationPosts.Request.ID,
                                                            RequestTypeName = t.TranslationPosts.Request.RequestType.Title,
                                                            CenterName = t.TranslationPosts.Request.Center.CenterName,
                                                            InsertDate = t.TranslationPosts.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            EndDate = t.TranslationPosts.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                            OldPostContactID = t.TranslationPosts.OldPostContactID,
                                                            NewPostContactID = t.TranslationPosts.NewPostContactID,
                                                            OldPost = Convert.ToInt32(t.RequestLog.Description.Element("OldPost").Value),
                                                            OldPostContact = Convert.ToInt64(t.RequestLog.Description.Element("OldPostContact").Value),
                                                            OldCabinet = Convert.ToInt32(t.RequestLog.Description.Element("OldCabinet").Value),

                                                            NewPost = Convert.ToInt32(t.RequestLog.Description.Element("NewPost").Value),
                                                            NewPostContact = Convert.ToInt64(t.RequestLog.Description.Element("NewPostContact").Value),
                                                            NewCabinet = Convert.ToInt32(t.RequestLog.Description.Element("NewCabinet").Value),

                                                            OldTelephoneNo = t.RequestLog.TelephoneNo,
                                                        })
                                                       .ToList();
                changeTelephone = changeTelephone.Union(tranlationPost).ToList();
                #endregion tranlationPost

                #region PCMToNormal
                PCMToNormal = context.TranslationPCMToNormals.Join(context.RequestLogs, t2 => t2.ID, t3 => t3.RequestID, (t2, t3) => new { TranslationPCMToNormal = t2, RequestLog = t3 }).Where(t =>
                                                                    (t.TranslationPCMToNormal.Request.EndDate.HasValue) &&
                                                                    (requestID == -1 || t.TranslationPCMToNormal.Request.ID == requestID) &&
                                                                    (!fromDateTime.HasValue || t.TranslationPCMToNormal.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.TranslationPCMToNormal.Request.EndDate <= toDateTime) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.TranslationPCMToNormal.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.TranslationPCMToNormal.Request.RequestTypeID))
                                                               )
                                                        .Select(t => new TechnicalRequestInfo
                                                        {
                                                            ID = t.TranslationPCMToNormal.ID,
                                                            RequestID = t.TranslationPCMToNormal.Request.ID,
                                                            RequestTypeName = t.TranslationPCMToNormal.Request.RequestType.Title,
                                                            CenterName = t.TranslationPCMToNormal.Request.Center.CenterName,
                                                            InsertDate = t.TranslationPCMToNormal.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            EndDate = t.TranslationPCMToNormal.Request.EndDate.ToPersian(Date.DateStringType.Short),

                                                            OldPost = Convert.ToInt32(t.RequestLog.Description.Element("OldPost").Value),
                                                            OldPostContact = Convert.ToInt64(t.RequestLog.Description.Element("OldPostContact").Value),
                                                            OldCabinet = Convert.ToInt32(t.RequestLog.Description.Element("OldCabinet").Value),

                                                            NewPost = Convert.ToInt32(t.RequestLog.Description.Element("NewPost").Value),
                                                            NewPostContact = Convert.ToInt64(t.RequestLog.Description.Element("NewPostContact").Value),
                                                            NewCabinet = Convert.ToInt32(t.RequestLog.Description.Element("NewCabinet").Value),

                                                            OldTelephoneNo = t.RequestLog.TelephoneNo,
                                                        })
                                                       .ToList();

                changeTelephone = changeTelephone.Union(PCMToNormal).ToList();
                #endregion tranlationPost

                #region swapTelephones
                swapTelephones = context.SwapTelephones.Where(t =>
                                                                    (t.Request.EndDate.HasValue) &&
                                                                    (requestID == -1 || t.Request.ID == requestID) &&
                                                                    (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                               )
                                                        .Select(t => new TechnicalRequestInfo
                                                        {
                                                            ID = t.RequestID,
                                                            RequestID = t.Request.ID,
                                                            RequestTypeName = t.Request.RequestType.Title,
                                                            CenterName = t.Request.Center.CenterName,
                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                            OldTelephoneNo = t.FromTelephoneNo,
                                                            NewTelephoneNo = t.ToTelephoneNo,
                                                        })
                                                       .ToList();

                changeTelephone = changeTelephone.Union(swapTelephones).ToList();
                #endregion swapTelephones

                #region swapPCM
                swapPCM = context.SwapPCMs.Where(t =>
                                                                    (t.Request.EndDate.HasValue) &&
                                                                    (requestID == -1 || t.Request.ID == requestID) &&
                                                                    (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                               )
                                                        .Select(t => new TechnicalRequestInfo
                                                        {
                                                            ID = t.RequestID,
                                                            RequestID = t.Request.ID,
                                                            RequestTypeName = t.Request.RequestType.Title,
                                                            CenterName = t.Request.Center.CenterName,
                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                            OldTelephoneNo = t.FromTelephoneNo,
                                                            NewTelephoneNo = t.ToTelephoneNo,
                                                        })
                                                       .ToList();

                changeTelephone = changeTelephone.Union(swapPCM).ToList();
                #endregion swapPCM

                #region buchtSwiching
                buchtSwiching = context.BuchtSwitchings.Where(t =>
                                                                    (t.Request.EndDate.HasValue) &&
                                                                    (requestID == -1 || t.Request.ID == requestID) &&
                                                                    (!fromDateTime.HasValue || t.Request.EndDate >= fromDateTime) &&
                                                                    (!toDateTime.HasValue || t.Request.EndDate <= toDateTime) &&
                                                                    (centerIDs.Count() == 0 || centerIDs.Contains((int)t.Request.CenterID)) &&
                                                                    (requestTypeIDs.Count() == 0 || requestTypeIDs.Contains((int)t.Request.RequestTypeID))
                                                               )
                                                        .Select(t => new TechnicalRequestInfo
                                                        {
                                                            ID = t.ID,
                                                            RequestID = t.Request.ID,

                                                            FirstNameOrTitle = t.Request.Customer.FirstNameOrTitle,
                                                            LastName = t.Request.Customer.LastName,
                                                            PersonType = DB.GetEnumDescriptionByValue(typeof(DB.PersonType), t.Request.Customer.PersonType),

                                                            RequestTypeName = t.Request.RequestType.Title,
                                                            CenterName = t.Request.Center.CenterName,
                                                            InsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            EndDate = t.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                            OldTelephoneNo = t.Request.TelephoneNo,

                                                            OldColumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                            OldRowNo = t.Bucht.VerticalMDFRow.VerticalRowNo,
                                                            OldBuchtNo = t.Bucht.BuchtNo,


                                                            NewColumnNo = t.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                            NewRowNo = t.Bucht1.VerticalMDFRow.VerticalRowNo,
                                                            NewBuchtNo = t.Bucht1.BuchtNo,


                                                        })
                                                       .ToList();

                changeTelephone = changeTelephone.Union(buchtSwiching).ToList();
                #endregion buchtSwiching

            }


            count = changeTelephone.Count();

            if (pageSize == 0)
            {
                return changeTelephone.ToList();
            }
            else
            {
                return changeTelephone.Skip(startRowIndex).Take(pageSize).ToList();
            }
        }
    }
}
