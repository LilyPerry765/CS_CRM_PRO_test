using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLSupportRequestDB
    {
        public static ADSLSupportRequest GetADSLSupportRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSupportRequests.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<ADSLSupportRequestInfo> GetADSLSupportRequestList(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSupportRequests.Where(t => t.Request.TelephoneNo == telephoneNo).Select(t => new ADSLSupportRequestInfo
                    {
                        ID = t.ID,
                        FirstDescription = t.FirstDescription,
                        ResultDescription = t.ResultDescription,
                        RequesterName = t.Request.User.FirstName + " " + ((t.Request.User.LastName != null) ? t.Request.User.LastName : ""),
                        RequestDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                        ResultName = (t.Request.EndDate != null) ? (t.Request.User1.FirstName + " " + ((t.Request.User1.LastName != null) ? t.Request.User1.LastName : "")) : "",
                        ResultDate =  Date.GetPersianDate(t.Request.EndDate, Date.DateStringType.DateTime),
                    }).ToList();
            }
        }
    }
}
