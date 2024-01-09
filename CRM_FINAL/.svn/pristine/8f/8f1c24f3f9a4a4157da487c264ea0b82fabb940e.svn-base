using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLSupportCommnetDB
    {
        public static List<ADSLSupportCommentInfo> GetCommnetbytelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSupportCommnets.Where(t => t.TelephoneNo == telephoneNo).OrderBy(t => t.InsertDate)
                    .Select(t => new ADSLSupportCommentInfo
                    {
                        ID = t.ID,
                        TelephoneNo = t.TelephoneNo,
                        User = t.User.FirstName + " " + ((t.User.LastName != null) ? t.User.LastName : ""),
                        InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                        Comment = t.Commnet
                    }).ToList();
            }
        }
    }
}
