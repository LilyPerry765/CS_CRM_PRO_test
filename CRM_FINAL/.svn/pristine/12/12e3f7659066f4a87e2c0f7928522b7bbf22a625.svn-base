using CRM.WebAPI.Models.Shahkar.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM.WebAPI.Models.Shahkar.Methods
{
    public static class ShahkarUtilities
    {
        /// <summary>
        /// .این متد شناسه مورد نظر سامانه شاهکار را بر اساس داکیومنت شاهکار ایجاد میکند
        /// </summary>
        /// <param name="serverDateTime">تاریخ</param>
        /// <param name="serviceProviderIdCreatedByShahkar">مشخصه سرویس دهنده است که از طرف سازمان تنظیم مقررات به هر سرویس دهنده اختصاص می یابد.</param>
        /// <returns>شناسه مورد نظر شاهکار - requestId</returns>
        public static string GenerateShahkarRequestId(DateTime serverDateTime, string serviceProviderIdCreatedByShahkar)
        {
            string shahkarRequestId = string.Empty;
            string currentDateString = string.Empty;

            //currentDateString = string.Format("{0}{1}", serverDateTime.ToString("yyyyMMddHHmmss"), serverDateTime.ToString("ffffff"));
            currentDateString = string.Format("{0}{1}", serverDateTime.ToString("yyyyMMddHHmmss"), (serverDateTime.Ticks / 10).ToString().Substring(11, 6));
            shahkarRequestId = string.Format("{0}{1}", serviceProviderIdCreatedByShahkar, currentDateString);
            return shahkarRequestId;
        }

    }
}
