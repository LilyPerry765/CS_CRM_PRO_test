using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class InquiryDB
    {
        public static Inquiry GetInquiryByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
              return  context.Inquiries.Where(t => t.RequestID == requestID).Take(1).SingleOrDefault();
            }
        }

        public static int GetInquiryCountByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Inquiries.Where(t => t.RequestID == requestID).Count();
            }
        }
    }
}
