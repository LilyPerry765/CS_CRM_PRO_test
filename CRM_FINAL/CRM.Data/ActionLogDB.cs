using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CRM.Data.Schema;

namespace CRM.Data
{
    public static class ActionLogDB
    {
        public static void AddActionLog(byte actionID, string userName, Schema.ActionLogRequest actionLogRequest)
        {
            ActionLog actionLog = new ActionLog();
            actionLog.ActionID = actionID;
            actionLog.UserName = userName;
            actionLog.Date = DB.GetServerDate();
            actionLog.Description = XElement.Parse(LogSchemaUtility.Serialize<Schema.ActionLogRequest>(actionLogRequest, true));

            actionLog.Detach();
            DB.Save(actionLog);
        }

        public static void AddLoginLog(byte actionID, string userName, Schema.LoginLog loginLog)
        {
            ActionLog actionLog = new ActionLog();
            actionLog.ActionID = actionID;
            actionLog.UserName = userName;
            actionLog.Date = DB.GetServerDate();
            actionLog.Description = XElement.Parse(LogSchemaUtility.Serialize<Schema.LoginLog>(loginLog, true));

            actionLog.Detach();
            DB.Save(actionLog);
        }

        public static void AddLogoutLog(byte actionID, string userName, Schema.LogoutLog logoutLog)
        {
            ActionLog actionLog = new ActionLog();
            actionLog.ActionID = actionID;
            actionLog.UserName = userName;
            actionLog.Date = DB.GetServerDate();
            actionLog.Description = XElement.Parse(LogSchemaUtility.Serialize<Schema.LogoutLog>(logoutLog, true));

            actionLog.Detach();
            DB.Save(actionLog);
        }

        public static List<ActionLogReport> SearcActionLogs(
            List<int> actionId,
            string userName,
            DateTime? fromDate,
            DateTime? toDate,
            int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ActionLogs
                    .Where(t => (actionId.Count == 0 || actionId.Contains(t.ActionID)) &&
                                (string.IsNullOrWhiteSpace(userName) || t.UserName.Contains(userName)) &&
                                (!fromDate.HasValue || t.Date >= fromDate) &&
                                (!toDate.HasValue || t.Date <= toDate))
                    .OrderByDescending(t => t.Date)
                    .Select(t => new ActionLogReport
                    {
                        ID = t.ID,
                        ActionID = t.ActionID,
                        UserName = t.UserName,
                        Date = Date.GetPersianDate(t.Date, Date.DateStringType.DateTime),
                        Description = GetDescriptiveActionLog(t.Description, t.ActionID)
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearcActionLogsCount(
            List<int> actionId,
            string userName,
            DateTime? fromDate,
            DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ActionLogs
                    .Where(t => (actionId.Count == 0 || actionId.Contains(t.ActionID)) &&
                                (string.IsNullOrWhiteSpace(userName) || t.UserName.Contains(userName)) &&
                                (!fromDate.HasValue || t.Date >= fromDate) &&
                                (!toDate.HasValue || t.Date <= toDate))
                    .Count();
            }
        }

        private static string GetDescriptiveActionLog(System.Xml.Linq.XElement description, byte actionID)
        {
            string result = "";
            switch (actionID)
            {
                case (byte)DB.ActionLog.Login:

                    CRM.Data.Schema.LoginLog loginLog = LogSchemaUtility.Deserialize<CRM.Data.Schema.LoginLog>(description.ToString());
                    result = "IP : " + loginLog.IPAddress;
                    break;

                case (byte)DB.ActionLog.Logout:
                    CRM.Data.Schema.LogoutLog logoutLog = LogSchemaUtility.Deserialize<CRM.Data.Schema.LogoutLog>(description.ToString());
                    result = "IP : " + logoutLog.IPAddress;
                    break;

                case (byte)DB.ActionLog.View:
                    CRM.Data.Schema.ActionLogRequest actionLogRequestView = LogSchemaUtility.Deserialize<CRM.Data.Schema.ActionLogRequest>(description.ToString());
                    result = "نام فرم : " + actionLogRequestView.FormName;
                    break;

                case (byte)DB.ActionLog.Save:
                    CRM.Data.Schema.ActionLogRequest actionLogRequestSave = LogSchemaUtility.Deserialize<CRM.Data.Schema.ActionLogRequest>(description.ToString());
                    result = "نام فرم : " + actionLogRequestSave.FormName;
                    result += (!string.IsNullOrEmpty(actionLogRequestSave.ObjectType)) ? "، " + " نام موجودیت : " + actionLogRequestSave.ObjectType : "";
                    break;

                case (byte)DB.ActionLog.Forward:
                    CRM.Data.Schema.ActionLogRequest actionLogRequestForward = LogSchemaUtility.Deserialize<CRM.Data.Schema.ActionLogRequest>(description.ToString());
                    result = "نام فرم : " + actionLogRequestForward.FormName;
                    break;

                default:
                    break;
            }

            return result;
        }

        //TODO:rad یک شیوه جدید برای جستجوی داخل فایل اکس ام ال
        /// <summary>
        /// .لیست پی سی ام های جمع آوری شده را در صورت وجود برمیگرداند
        /// </summary>
        /// <param name="centers">مراکز</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<PcmDropActionLog> SearchDropedPcm(List<int> centers, DateTime? fromDate, DateTime? toDate, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PcmDropActionLog> result = new List<PcmDropActionLog>();

                string centersId = (centers != null && centers.Count() != 0) ? string.Join(",", centers.ToArray()) : "";
                string fromDateString = (fromDate.HasValue) ? fromDate.Value.ToShortDateString() : string.Empty;
                string toDateString = (toDate.HasValue) ? toDate.Value.ToShortDateString() : string.Empty;
                string primaryQuery = string.Format(@"
                                                     SELECT 
                                                            AL.*
                                                     FROM 
                                                        ActionLog AL 
                                                     WHERE  
                                                        AL.ActionID = {0} 
                                                        AND 
                                                       ( 
                                                        '{1}' IS NULL
                                                         OR
                                                         LEN('{1}') = 0
                                                         OR
                                                         (AL.[Description].value('(/PCMDrop/CenterID)[1]','int')) IN (SELECT * FROM ufnSplitList('{1}'))
                                                       )
                                                        AND
                                                       (LEN('{2}') = 0 OR AL.Date >= '{2}')
                                                        AND
                                                       (LEN('{3}') = 0 OR AL.Date <= '{3}')
                                                     ", (int)DB.ActionLog.PCMDrop, centersId, fromDateString, toDateString);

                var secondQuery = context.ExecuteQuery<ActionLog>(primaryQuery, new object[] { }).AsQueryable();
                count = context.ExecuteQuery<ActionLog>(primaryQuery, new object[] { }).Count();
                result = secondQuery.Select(al => new PcmDropActionLog
                                                      {
                                                          ID = al.ID,
                                                          CityName = context.Centers.Where(ce => ce.ID == System.Convert.ToInt16(al.Description.Element("CenterID").Value)).Select(ce => ce.Region.City.Name).SingleOrDefault(),
                                                          CenterName = al.Description.Element("Center").Value,
                                                          Rock = !string.IsNullOrEmpty(al.Description.Element("Rock").Value) ? System.Convert.ToInt16(al.Description.Element("Rock").Value) : default(int?),
                                                          Shelf = !string.IsNullOrEmpty(al.Description.Element("Shelf").Value) ? System.Convert.ToInt16(al.Description.Element("Shelf").Value) : default(int?),
                                                          Card = !string.IsNullOrEmpty(al.Description.Element("Card").Value) ? System.Convert.ToInt16(al.Description.Element("Card").Value) : default(int?),
                                                          Date = al.Date.ToPersian(Date.DateStringType.Short)
                                                      }
                                            )
                                    .Skip(startRowIndex)
                                    .Take(pageSize)
                                    .ToList();

                return result;
            }
        }

    }
}
