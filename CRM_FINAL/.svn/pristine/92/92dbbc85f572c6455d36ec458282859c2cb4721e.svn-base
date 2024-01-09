using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ADSLAAATypeDB
    {
        public static List<CheckableItem> GetADSLAAATypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLAAATypes.Select(t => new CheckableItem { ID = t.ID, Name = t.Name, IsChecked = false }).ToList();
            }
        }

        public static List<ADSLAAAActionLogInfo> GetADSLAAALog(string telephoneNo, string user)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLAAAActionLogs.Where(t => t.TelephoneNo.ToString() == telephoneNo || t.Wireless == telephoneNo)
                                                .Select(t => new ADSLAAAActionLogInfo
                                                {
                                                    ID = t.ID,
                                                    TelephoneNo = (t.TelephoneNo != null) ? t.TelephoneNo.ToString() : t.Wireless,
                                                    Action = DB.GetEnumDescriptionByValue(typeof(DB.ADSLAAAction), t.ActionID),
                                                    InsertDate = Date.GetPersianDate(t.InsertDate, Date.DateStringType.DateTime),
                                                    User = UserDB.GetUserFullName(t.UserID),
                                                    OldValue = t.OldValue,
                                                    NewValue = t.NewValue
                                                }).OrderByDescending(t => t.ID)
                                                .ToList();
            }
        }
    }
}
