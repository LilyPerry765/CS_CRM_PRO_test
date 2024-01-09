using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ReferenceDocumentDB
    {
        
        public static ReferenceDocument GetReferenceDocumentByRequestDocumentIDByRequestID(long rquestDocumentID, long requestID)
        {
          using(MainDataContext context = new MainDataContext())
          {
              return context.ReferenceDocuments.Where(t => t.RequestDocumentID == rquestDocumentID && t.RequestID == requestID).SingleOrDefault();
          }
        }
    }
}
