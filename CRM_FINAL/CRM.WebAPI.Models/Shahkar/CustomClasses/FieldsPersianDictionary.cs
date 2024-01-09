using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Models.Shahkar.CustomClasses
{
    /// <summary>
    ///چون پاسخ هایی که از سمت سامانه شاهکار برگشت داده میشد برای کاربران کاملاً مبهم بود دیکشنری زیر را 
    ///. ایجاد کردم تا به کاربران معادل فارسی کلمات انگلیسی برگشتی از سمت شاهکار را نمایش دهم
    ///به طور مثال بلاک زیر جواب برگشتی از شاهکار است : 
    ///
    //{
    //"response":311,
    //"requestId":"030120170404144147070518",
    //"result":"InvalidField:name;InvalidField:identificationNo;",
    //"comment":"مقدار نامعتبر"
    //}
    ///منظور شاهکار از جواب بالا این بوده است که : 
    ///نام و کد ملی مشترکی که احراز هویت شده است ، دارای مقادیر غیرمعتبر  بوده است
    /// </summary>
    public static class FieldsPersianDictionary
    {
        #region Properties and Fields

        public static Dictionary<string, string> Dictionary = new Dictionary<string, string>
        {
            {"name", " : نام"},
            {"family", " : نام خانوادگی"},
            {"fatherName", " : نام پدر"},
            {"birthDate", " : تاریخ تولد"},
            {"identificationNo", " : کد ملّی"},
            {"certificateNo", " : شماره شناسنامه"},
            {"requestId"," : شناسه سیستمی"},
            {"gender"," : جنسیت"},
            {"service.phoneNumber"," : شماره تلفن ثابت"},
            {"service.kafu"," : کافو"},
            {"service.post"," : پست"},
            {"service.credit"," : نوع اعتبار"},
            {"service.general"," : همگانی بودن یا نبودن مشترک"}
        };

        #endregion

    }
}
