using CRM.WebAPI.Models.Shahkar.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM.WebAPI.Models.Shahkar.Methods
{
    public static class ShahkarValidator
    {
        /// <summary>
        /// .تطبیق مقدار فیلدهای ارسالی به سامانه ی شاهکار را بررسی میکند
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string InvalidFieldCheck(string input)
        {
            string result = "";
            if (Regex.IsMatch(input, "InvalidField"))
            {
                result += Environment.NewLine + "مقدار نامعتبر برای " + FieldsPersianDictionary.Dictionary[input.Substring(input.IndexOf(':') + 1)];
            }
            return result;
        }

        /// <summary>
        /// .وجود یا عدم وجود مقدار برای فیلدهای ارسالی به سامانه ی شاهکار را بررسی میکند
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RequiredFieldCheck(string input)
        {
            string result = "";
            if (Regex.IsMatch(input, "RequiredField"))
            {
                result += Environment.NewLine + "عدم وجود مقدار برای " + FieldsPersianDictionary.Dictionary[input.Substring(input.IndexOf(':') + 1)];
            }
            return result;
        }
    }
}
