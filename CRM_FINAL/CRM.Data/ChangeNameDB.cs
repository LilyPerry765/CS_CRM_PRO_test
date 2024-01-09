using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CRM.Data
{
    public static class ChangeNameDB
    {
        public static IEnumerable<ChangeNameInfo> GetChangeNameInfoByID(long id)
        {
            /*using (*/
            MainDataContext context = new MainDataContext();//)
            //{
            var x = context.ChangeNames
                .Where(t => t.ID == id)
                .Select(t => new ChangeNameInfo
                {
                    RequestDate = Date.GetPersianDate(t.Request.RequestDate, Date.DateStringType.Short),
                    TelephoneNo = t.Request.TelephoneNo.ToString(),
                    OldCustomerNationalCode = t.Customer1.NationalCodeOrRecordNo,
                    OldFirstNameOrTitle = t.Customer1.FirstNameOrTitle + " " + t.Customer1.LastName,
                    NewCustomerNationalCode = t.Customer.NationalCodeOrRecordNo,
                    NewFirstNameOrTitle = t.Customer.FirstNameOrTitle + " " + t.Customer.LastName,
                    RequestTypeName = t.Request.RequestType.Title,
                    CenterName = t.Request.Center.CenterName,
                    RequesterName = t.Request.RequesterName,
                    RequestLetterNo = t.Request.RequestLetterNo,
                    RequestLetterDate = Date.GetPersianDate(t.Request.RequestLetterDate, Date.DateStringType.Short)
                });
            ;

            context.ChangeNames.Where(t => t.ID == id).SingleOrDefault();
            return x;
            //}
        }

        public static ChangeName GetChangeNameByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeNames.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
