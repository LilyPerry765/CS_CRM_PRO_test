using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class RequestDocumnetDB
    {
        public static void SaveRequestDocument(RequestDocument reqDoc, long requestID, bool isNew)
        {
            ReferenceDocument refDoc = new ReferenceDocument();
            refDoc.RequestID = requestID;
            using (TransactionScope ChildTransactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (isNew)
                {

                    reqDoc.Detach();
                    DB.Save(reqDoc);
                    refDoc.RequestDocumentID = reqDoc.ID;
                    refDoc.Detach();
                    DB.Save(refDoc);
                }
                else
                {
                    refDoc.RequestDocumentID = reqDoc.ID;
                    refDoc.Detach();
                    DB.Save(refDoc);
                }
                ChildTransactionScope.Complete();
            }
        }

        public static void SaveRequestDocumentForWeb(RequestDocument reqDoc, long requestID, bool isNew)
        {
            ReferenceDocument refDoc = new ReferenceDocument();
            refDoc.RequestID = requestID;

            if (isNew)
            {

                reqDoc.Detach();
                DB.Save(reqDoc);
                refDoc.RequestDocumentID = reqDoc.ID;
                refDoc.Detach();
                DB.Save(refDoc);
            }
            else
            {
                refDoc.RequestDocumentID = reqDoc.ID;
                refDoc.Detach();
                DB.Save(refDoc);
            }
        }

        public static void DeleteRequestDocument(RequestDocument reqDoc, long reqID)
        {
            var refDoc = DB.SearchByPropertyName<ReferenceDocument>("RequestDocumentID", reqDoc.ID);
            DB.Delete<ReferenceDocument>(refDoc.Where(t => t.RequestID == reqID).Take(1).SingleOrDefault().ID);
            if (refDoc.Count == 1)
            {
                DB.Delete<RequestDocument>(reqDoc.ID);
            }


        }


        public static RequestDocument GetRequestDocument(long requestID, long requestDocumentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<RequestDocument> a = context.RequestDocuments.Join(context.ReferenceDocuments, RequestDocument => RequestDocument.ID, ReferenceDocument => ReferenceDocument.RequestDocumentID, (RequestDocument, ReferenceDocument) => new { RequestDocument = RequestDocument, ReferenceDocument = ReferenceDocument })
                       .Where(t => t.ReferenceDocument.RequestID == requestID && t.RequestDocument.DocumentRequestTypeID == requestDocumentID).Select(t => t.RequestDocument).ToList();

                return a.Take(1).SingleOrDefault();
            }
        }

        public static RequestDocument GetRequestDocumentByID(long requestDocumentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestDocuments.Where(t => t.ID == requestDocumentID).SingleOrDefault();
            }
        }

        

        public static RequestDocument GetLastRequestDocument(long documentRequestTypeID, long customerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestDocuments.Where(r => r.DocumentRequestTypeID == documentRequestTypeID && r.CustomerID == customerID).OrderByDescending(t => t.InsertDate).FirstOrDefault();
            }

        }

        public static bool CheckTelephoneBeRound(Request reqeust , out long? telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {

               if(context.Contracts.Where(t=> t.RequestID == reqeust.ID).Any(t=>t.TelRoundSaleID != null))
              {
                  telephone = context.Contracts.Where(t => t.RequestID == reqeust.ID).SingleOrDefault().TelRoundSale.TelephoneNo;
                  return true;
              }
              else
              {
                 
                  telephone = null;
                  return false;
              }
            }
        }
    }
}
