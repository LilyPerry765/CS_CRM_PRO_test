using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;


namespace CRM.Data
{
    public static class DocumentRequestTypeDB
    {
        public static List<DocumentRequestType> SearchDocumentRequestType(
            List<int> documentType,
            List<int> requestType,
            List<int> announce,
            List<int> needForCustomerType,
            List<int> chargingType,
            List<int> telephoneType,
            List<int> telephonePosessionType,
            List<int> orderType,
            List<int> status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.DocumentRequestTypes
                    .Where(t =>
                            (documentType.Count == 0 || documentType.Contains((int)t.DocumentTypeID)) &&
                            (requestType.Count == 0 || requestType.Contains((int)t.RequestTypeID)) &&
                            (announce.Count == 0 || announce.Contains((int)t.AnnounceID)) &&
                            (needForCustomerType.Count == 0 || needForCustomerType.Contains((int)t.NeedForCustomerType)) &&
                            (chargingType.Count == 0 || chargingType.Contains((int)t.ChargingType)) &&
                            (telephoneType.Count == 0 || telephoneType.Contains((int)t.TelephoneType)) &&
                            (telephonePosessionType.Count == 0 || telephonePosessionType.Contains((int)t.TelephonePosessionType)) &&
                            (orderType.Count == 0 || orderType.Contains((int)t.OrderType)) &&
                            (status.Count == 0 || status.Contains((int)t.Status))
                            )
                    .ToList();
            }
        }

        public static List<DocumentRequestType> GetDocumentsByRequestType(DateTime requestDate, int requestTypeID, bool CustomerType)
        {
            using (MainDataContext context = new MainDataContext())
            {

                Announce announce = context.Announces.Where(a => requestDate >= a.StartDate && requestDate <= a.EndDate).OrderByDescending(t => t.ID).FirstOrDefault();

                return context.DocumentRequestTypes.Where(d => d.AnnounceID == announce.ID && d.NeedForCustomerType != Convert.ToInt16(CustomerType)).ToList();

            }
        }

        public static List<DocumentRequestTypeInfo> GetDocumentInfo()
        {
            using (MainDataContext context = new MainDataContext())
            {
                //var query = from d in context.DocumentRequestTypes
                //            join t in context.DocumentTypes
                //            on d.DocumentTypeID equals t.ID
                //            join a in context.Announces
                //            on d.AnnounceID equals a.ID

                //            select new DocumentRequestTypeInfo { doc = d, type = t, announce = a };

                IQueryable<DocumentRequestTypeInfo> query = context.DocumentRequestTypes.Join(context.DocumentTypes, d => d.DocumentTypeID, t => t.ID, (d, t) => new { d = d, t = t })
                                                                                        .Join(context.Announces, d => d.d.AnnounceID, a => a.ID, (d, a) => new { d = d.d, t = d.t, a = a })
                                                                                        .Select(t => new DocumentRequestTypeInfo { doc = t.d, type = t.t, announce = t.a });

                return query.ToList();

            }
        }
        public static List<DocumentRequestTypeInfo> GetDocumentInfoByAnnouncesID(int announcesID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<DocumentRequestTypeInfo> query = context.DocumentRequestTypes.Join(context.DocumentTypes, d => d.DocumentTypeID, t => t.ID, (d, t) => new { d = d, t = t })
                                                                                        .Join(context.Announces, d => d.d.AnnounceID, a => a.ID, (d, a) => new { d = d.d, t = d.t, a = a })
                                                                                        .Where(t => t.a.ID == announcesID)
                                                                                        .Select(t => new DocumentRequestTypeInfo { doc = t.d, type = t.t, announce = t.a });

                return query.ToList();

            }
        }
        public static List<DocumentsByCustomer> GetNeedDocumentsForRequest(int requestTypeID, DateTime requestDate, byte personType)
        {

            using (MainDataContext context = new MainDataContext())
            {
                List<DocumentsByCustomer> l = new List<DocumentsByCustomer>();
                // var x = context.Announces.Where(a => requestDate <= a.EndDate && requestDate >= a.StartDate).Take(1).SingleOrDefault().ID;

                var x = context.Announces.Where(a => requestDate <= a.EndDate && requestDate >= a.StartDate).OrderByDescending(t => t.EndDate).Take(1).SingleOrDefault();
                if (x == null) { throw new Exception("آییین نامه ای  برای تاریخ فعلی یافت نشد"); }

                IQueryable<DocumentsByCustomer> documentRequestType = context.DocumentRequestTypes
                    .Where(t => t.RequestTypeID == requestTypeID &&
                                t.AnnounceID == x.ID &&
                                (t.NeedForCustomerType == 2 || t.NeedForCustomerType == personType))
                                .Select(t => new DocumentsByCustomer
                {
                    AnnounceID = t.AnnounceID,
                    AnnounceTitle = t.Announce.AnnounceTitle,
                    StartDate = t.Announce.StartDate,
                    EndDate = t.Announce.EndDate,
                    IssueDate = t.Announce.IssueDate,
                    IssueNumber = t.Announce.IssueNumber,
                    AnnounceStatus = t.Announce.Status,
                    NeedForCustomerType = t.NeedForCustomerType,
                    RequestTypeID = t.RequestTypeID,
                    OrderType = t.OrderType,
                    TelephonePosessionType = t.TelephonePosessionType,
                    ChargingType = t.ChargingType,
                    TelephoneType = t.TelephoneType,
                    DocumentRequestTypeInsertDate = t.InsertDate,
                    DocumentRequestTypeStatus = t.Status,
                    DocumentRequestTypeID = t.ID,
                    DocumentTypeID = t.DocumentTypeID,
                    DocumentName = t.DocumentType.DocumentName,
                    ExistOnce = t.DocumentType.ExistOnce,
                    TypeID = t.DocumentType.TypeID,
                    IsRelatedTo3PercentQuota = t.DocumentType.IsRelatedTo3PercentQuota,
                    IsRelatedToRoundContract = t.DocumentType.IsRelatedToRoundContract ?? false,
                    IsForcible = t.IsForcible

                });

                return documentRequestType.ToList();


            }
        }


        //public static List<GetUsedDocsResult> GetUsedDocs2()
        //{

        //    using (MainDataContext context = new MainDataContext())
        //    {


        //      return context.GetUsedDocs().Select(t => t).ToList();


        //    }
        //}


        public static List<UsedDocs> GetUsedDocs()
        {

            using (MainDataContext context = new MainDataContext())
            {

                return context.ReferenceDocuments.Select(t => new UsedDocs
                {
                    PersonType = t.Request.Customer.PersonType,
                    DocumentRequestTypeID = t.RequestDocument.DocumentRequestTypeID,
                    AnnounceID = t.RequestDocument.DocumentRequestType.AnnounceID,
                    DocumentTypeID = t.RequestDocument.DocumentRequestType.DocumentTypeID,
                    RequestTypeID = t.RequestDocument.DocumentRequestType.RequestTypeID,
                    RequestID = t.RequestID ?? -1,
                    RequestDate = t.Request.RequestDate,
                    CustomerID = t.Request.CustomerID,
                    RequestDocumentID = t.RequestDocumentID,
                    DocumentName = t.RequestDocument.DocumentRequestType.DocumentType.DocumentName,
                    TypeID = t.RequestDocument.DocumentRequestType.DocumentType.TypeID,
                    IsRelatedTo3PercentQuota = (t.RequestDocument.DocumentRequestType.DocumentType.IsRelatedTo3PercentQuota ?? false),
                    IsRelatedToRoundContract = (t.RequestDocument.DocumentRequestType.DocumentType.IsRelatedToRoundContract ?? false)

                }).OrderBy(t => t.CustomerID).ThenBy(t => t.TypeID).ThenByDescending(t => t.RequestDate).ToList();

            }
        }

        public static List<UsedForms> GetUsedForms()
        {

            using (MainDataContext context = new MainDataContext())
            {

                return context.RequestForms.Select(t => new UsedForms
                {
                    ID = t.ID,
                    file_stream = t.file_stream,
                    RequestID = t.RequestID,
                    name = t.name,
                    FormID = t.FormID


                }).ToList();

            }
        }

        public static List<Forms> GetForms()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FormTemplates.Select(t => new Forms
                {
                    ID = t.ID,
                    Title = t.Title,
                    Template = t.Template,
                    TimeSpam = t.TimeStamp,
                    RequestTypeID = t.RequestTypeID
                }).ToList();
            }
        }

        public static RequestForm GetFormByID(int FormID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestForms.Where(t => t.FormID == FormID).SingleOrDefault();
            }
        }

    }

    #region Custom Class

    public class DocumentsByCustomer
    {

        public long? AnnounceID { get; set; }
        public string AnnounceTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueNumber { get; set; }
        public byte? AnnounceStatus { get; set; }
        public byte NeedForCustomerType { get; set; }
        public int RequestTypeID { get; set; }
        public byte? OrderType { get; set; }
        public byte? TelephonePosessionType { get; set; }
        public byte? ChargingType { get; set; }
        public byte? TelephoneType { get; set; }
        public DateTime DocumentRequestTypeInsertDate { get; set; }
        public byte DocumentRequestTypeStatus { get; set; }
        public long DocumentRequestTypeID { get; set; }
        public long DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public bool? ExistOnce { get; set; }
        public byte? TypeID { get; set; }
        public bool? IsRelatedTo3PercentQuota { get; set; }
        public bool? IsRelatedToRoundContract { get; set; }
        public bool IsForcible { get; set; }
    }

    public class DocumentsByCustomerForWeb
    {

        public DocumentsByCustomer DocumentsByCustomer { get; set; }
        public bool IsAvailable { get; set; }
        public string DocumentName { get; set; }
        public bool IsForcible { get; set; }
    }


    public class DocumentRequestTypeInfo
    {
        public DocumentRequestType doc { get; set; }

        public DocumentType type { get; set; }

        public Announce announce { get; set; }

    }

    public class UsedDocs
    {
        public string DocumentName { get; set; }
        public byte PersonType { get; set; }
        public long RequestID { get; set; }
        public DateTime? RequestDate { get; set; }
        public long? CustomerID { get; set; }
        public long RequestDocumentID { get; set; }
        public long? AnnounceID { get; set; }
        public long? DocumentTypeID { get; set; }
        public int? TypeID { get; set; }
        public bool? IsRelatedTo3PercentQuota { get; set; }
        public bool? IsRelatedToRoundContract { get; set; }
        public int? RequestTypeID { get; set; }
        public long? DocumentRequestTypeID { get; set; }

    }

    public class Forms
    {
        public int ID { get; set; }
        public Binary Template { get; set; }
        public string Title { get; set; }
        public string TimeSpam { get; set; }
        public int RequestTypeID { get; set; }
        public long RequestID { get; set; }
    }

    public class UsedForms
    {
        public int ID { get; set; }
        public long RequestID { get; set; }
        public Binary file_stream { get; set; }
        public int FormID { get; set; }
        public string name { get; set; }
    }


    #endregion Custom Class


}
