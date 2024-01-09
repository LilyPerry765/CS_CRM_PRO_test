using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class Extensions
    {
        #region long

        public static string ToLiteral(this long number)
        {
            return CRM.Data.Converter.ToLiteral(number.ToString());
        }

        public static string ToMoneyFormat(this long number)
        {
            return CRM.Data.Strings.FormatCurrency(number);
        }

        #endregion long

        #region int

        #endregion int

        #region string

        public static string ToLiteral(this string number)
        {
            return CRM.Data.Converter.ToLiteral(number);
        }

        public static string RemoveComma(this string number)
        {
            return CRM.Data.Strings.RemoveComma(number.Trim());
        }

        public static string AddCheckDigit(this string number)
        {
            return number + CRM.Data.Strings.GetCheckDigit(number);
        }

        public static string GetCheckDigit(this string number)
        {
            return CRM.Data.Strings.GetCheckDigit(number);
        }

        public static string ToMoneyFormat(this string number)
        {
            return CRM.Data.Strings.FormatCurrency(number);
        }

        public static string AddZeroPrefix(this string number, int digits)
        {
            return CRM.Data.Strings.AddZeroPrefix(number, digits);
        }

        public static bool IsInt(this string number)
        {
            return CRM.Data.Converter.IsValidType(number.Trim(), typeof(int));
        }

        public static bool IsShort(this string number)
        {
            return CRM.Data.Converter.IsValidType(number.Trim(), typeof(short));
        }

        public static bool Ibyte(this string number)
        {
            return CRM.Data.Converter.IsValidType(number.Trim(), typeof(byte));
        }

        public static bool IsLong(this string number)
        {
            return CRM.Data.Converter.IsValidType(number.Trim(), typeof(long));
        }

        public static string UTF8ToWindows1256(this string text)
        {
            return CRM.Data.Converter.UTF8ToWindows1256(text);
        }

        #endregion string

        #region Date

        public static string ToPersian(this DateTime date, Date.DateStringType dateType)
        {
            return CRM.Data.Date.GetPersianDate(date, dateType);
        }

        public static string ToPersian(this DateTime? date, Date.DateStringType dateType)
        {
            if (date == null) return string.Empty;
            return CRM.Data.Date.GetPersianDate(date ?? DB.GetServerDate(), dateType);
        }

        #endregion Date

        #region IEnumerables-NonGeneric
        //TODO:rad
        /// <summary>
        ///.تعیین میکند که آیا این مجموعه شمارشی نان جنریک دارای عضوی هست یا خیر
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>Written by rad
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source != null && source.GetEnumerator().MoveNext())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
