using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class WarningMessageDB
    {
        //TODO:rad
        /// <summary>
        /// .این متد شناسه نوع اخطار را گرفته و در صورت وجود پیغام مربوط به آن اخطار را برمیگرداند
        /// </summary>
        /// <param name="warningTypeId"></param>
        /// <returns></returns>
        public static string GetMessageByWarningTypeId(byte warningTypeId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string result = string.Empty;
                result = context.WarningMessages
                                .Where(wm => wm.WarningType == warningTypeId)
                                .Select(wm => wm.Message)
                                .SingleOrDefault();
                return result;
            }
        }

        public static List<WarningMessage> SearchWarningMessage(List<int> warningTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<WarningMessage> result = new List<WarningMessage>();

                result = context.WarningMessages
                                .Where(
                                        wm => (warningTypes.Count == 0 || warningTypes.Contains((int)wm.WarningType))
                                      )
                                .ToList();

                return result;
            }
        }

        public static WarningMessage GetWarningMessageById(int warningMessageId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                WarningMessage result = new WarningMessage();

                result = context.WarningMessages
                                .Where(wm => wm.ID == warningMessageId)
                                .SingleOrDefault();

                return result;
            }
        }

        /// <summary>
        /// .این متد شناسه نوع اخطار را گرفته  و چنانچه دارای پیغام باشد مقدار درست را برمیگرداند 
        /// </summary>
        /// <param name="warningTypeId"></param>
        /// <returns></returns>
        public static bool HasMessage(int warningTypeId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool result = false;
                result = context.WarningMessages.Where(wm => wm.WarningType == (byte)warningTypeId).Any();
                return result;
            }
        }
    }
}
