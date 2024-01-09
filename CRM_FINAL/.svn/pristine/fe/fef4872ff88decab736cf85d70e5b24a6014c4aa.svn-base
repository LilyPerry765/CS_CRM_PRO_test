using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Client.Shahkar
{
    public static class Helpers
    {
        /// <summary>
        /// .یک متن ساده را به معادل 64 بیتی به منظور افزایش سطح امنیت تبدیل میکند
        /// </summary>
        /// <param name="plainText">متن ساده قابل خواندن توسط شخص</param>
        /// <returns></returns>
        public static string ToBase64(string plainText)
        {
            string base64String = string.Empty;

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            base64String = Convert.ToBase64String(plainTextBytes);

            return base64String;
        }

        /// <summary>
        /// .یک متن 64 بیتی را به معادل متن ساده تبدیل میکند
        /// </summary>
        /// <param name="base64String">متن 64 بیتی - غیر قابل خواندن توسط شخص</param>
        /// <returns></returns>
        public static string FromBase64(string base64String)
        {
            string plainText = string.Empty;

            byte[] base64StringBytes = Convert.FromBase64String(base64String);

            plainText = System.Text.Encoding.UTF8.GetString(base64StringBytes);
            return plainText;

        }

        /// <summary>
        /// .بر اساس داکیومنت شاهکار ، مواردی که سازمان تنظیم به استان (برای دسترسی به متدهای شاهکار) تحویل داده است را به معادل 64 بیتی تبدیل میکند
        /// </summary>
        /// <param name="username">نام کاربری که سازمان تنظیم به استان داده است</param>
        /// <param name="password">کلمه عبور که سازمان تنظیم به استان داده است</param>
        /// <returns></returns>
        public static string GenerateBase64Authorization(string username, string password)
        {
            string base64Authorization = string.Empty;

            string concatedInformation = string.Format("{0}:{1}", username, password);

            base64Authorization = string.Format("Basic {0}", ToBase64(concatedInformation));

            return base64Authorization;
        }

    }
}
