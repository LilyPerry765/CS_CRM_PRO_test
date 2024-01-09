using System;
using System.Data;
using System.Linq;
using Serenity.Data;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using Serenity.Data.Mapping;
using System.Collections;

namespace CaseManagement.Case
{
    public static class Helper
    { 
        public enum DateStringType
        {
            Short, Long, Compelete, TwoDigitsYear, DateTime, Year, NoSlash
        }

        private static string PersianDayOfWeek(DayOfWeek day)
        {
            string persianDayOfWeek = string.Empty;
            switch (day)
            {
                case DayOfWeek.Saturday:
                    persianDayOfWeek = "شنبه";
                    break;

                case DayOfWeek.Sunday:
                    persianDayOfWeek = "یکشنبه";
                    break;

                case DayOfWeek.Monday:
                    persianDayOfWeek = "دوشنبه";
                    break;

                case DayOfWeek.Tuesday:
                    persianDayOfWeek = "سه‌شنبه";
                    break;

                case DayOfWeek.Wednesday:
                    persianDayOfWeek = "چهارشنبه";
                    break;

                case DayOfWeek.Thursday:
                    persianDayOfWeek = "پنج‌شنبه";
                    break;

                case DayOfWeek.Friday:
                    persianDayOfWeek = "جمعه";
                    break;

                default:
                    break;
            }

            return persianDayOfWeek;
        }

        private static string[] PersianMonths = { "", "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static String Serialize(object o, bool indented = false)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
            xmlDoc.PrependChild(xmlDec);
            XmlElement elemRoot = xmlDoc.CreateElement(o.GetType().Name);
            xmlDoc.AppendChild(elemRoot);
            XmlElement elem = null;
            Type idType = o.GetType();
           //s List<string> validTypes = new List<string>() { "DateTime", "Int32", "Char", "String", "Int64", "Int", "String", "Byte", "Boolean", "Boolean?" };
            List<PropertyInfo> props = idType.GetProperties().ToList(); //.Where(t => validTypes.Contains(t.PropertyType.Name.ToString())).ToList();

            foreach (PropertyInfo pInfo in props)
            {
                bool flag = true;
                string propType = pInfo.PropertyType.ToString();
                if (pInfo.Name == "FieldCount")
                    break;
                
                foreach (ExpressionAttribute foreignKey in pInfo.GetCustomAttributes(typeof(ExpressionAttribute)))         
                    flag = false;

                if (flag && !propType.ToLower().Contains("list"))
               {
                   elem = xmlDoc.CreateElement(pInfo.Name);
                   var val = pInfo.GetValue(o, null);
                   elem.InnerText = val == null ? "" : val.ToString();
                   elemRoot.AppendChild(elem);
               }
            }

            StringWriter StringWriter = new StringWriter();
            XmlTextWriter TextWriter = new XmlTextWriter(StringWriter);
            xmlDoc.WriteTo(TextWriter);

            string XMLStr = StringWriter.ToString();// 

            return XMLStr;


        // return   xmlDoc.ToString();
         //xmlDoc.InnerText();
        }
        public static void SaveLog(string tableName, string persianTableName, long recordID, string recordName, string iP, IDbConnection connection, Administration.ActionLog actionID,object logData =null )
        {
            int userID = 0;
            if (!string.IsNullOrEmpty(Serenity.Authorization.UserId))
                userID = int.Parse(Serenity.Authorization.UserId);
            else
                userID = (int)recordID;

            if (actionID == Administration.ActionLog.Login)
                userID = (int)recordID;

            Administration.Entities.LogRow log = new Administration.Entities.LogRow();

            log.TableName = tableName;
            log.PersianTableName = persianTableName;
            log.RecordId = recordID;
            log.RecordName = recordName;
            log.IP = iP;
            log.ActionID = actionID;
            log.UserId = userID;
            log.InsertDate = DateTime.Now;

            if (logData != null)
            {

               /*XmlDocument xdoc = Serialize(logData);
               MemoryStream xmlStream = new MemoryStream();
               xdoc.Save(xmlStream);*/


                String LogStr = Serialize(logData);
                                    
                
                log.OldData = LogStr;
                
              //  xmlStream.Flush();
              // xmlStream.Position = 0;

            }

            var user = Administration.Entities.UserRow.Fields;

            int? provinceId = connection.List<Administration.Entities.UserRow>(user.UserId == userID).Select(t => t.ProvinceId).SingleOrDefault();
            if (provinceId != null && provinceId != 0)
                log.ProvinceId = provinceId;

            connection.Insert<Administration.Entities.LogRow>(log);
        }

        public static string GetPersianDate(DateTime? date, DateStringType type)
        {
            if (date.HasValue)

                return GetPersianDate(date.Value, type);

            else
                return string.Empty;
        }

        public static string GetPersianDate(DateTime date, DateStringType type)
        {
            DateTime minDate = new DateTime(1000, 1, 1);
            DateTime maxDate = new DateTime(9999, 1, 1);

            if (date < minDate || date > maxDate) return string.Empty;

            PersianCalendar persianCalendar = new PersianCalendar();
            string result = string.Empty;

            DayOfWeek dayOfWeek = persianCalendar.GetDayOfWeek(date);
            int dayOfMonth = persianCalendar.GetDayOfMonth(date);
            int monthNumber = persianCalendar.GetMonth(date);
            int year = persianCalendar.GetYear(date);

            switch (type)
            {
                case DateStringType.Short:
                    result = string.Format("{0}/{1}/{2}", year.ToString(), ((monthNumber >= 10) ? monthNumber.ToString() : "0" + monthNumber.ToString()), ((dayOfMonth >= 10) ? dayOfMonth.ToString() : "0" + dayOfMonth.ToString()));
                    break;

                case DateStringType.Long:
                    result = string.Format("{0} {1} {2}", dayOfMonth.ToString(), PersianMonths[monthNumber], year.ToString());
                    break;

                case DateStringType.Compelete:
                    result = string.Format("{4} - {0}، {1} {2} {3}", PersianDayOfWeek(dayOfWeek), dayOfMonth.ToString(), PersianMonths[monthNumber], year.ToString(), GetTime(date));
                    //result = string.Format("{0}، {1} {2} {3}", PersianDayOfWeek(dayOfWeek), dayOfMonth.ToString(), PersianMonths[monthNumber], year.ToString());
                    break;

                case DateStringType.TwoDigitsYear:
                    result = string.Format("{0}/{1}/{2}", (year - 1300).ToString(), monthNumber.ToString(), dayOfMonth.ToString());
                    break;

                case DateStringType.DateTime:
                    result = string.Format("{3}  {0}/{1}/{2}", (year - 1300).ToString(), monthNumber.ToString(), dayOfMonth.ToString(), GetTime(date));
                    break;

                case DateStringType.Year:
                    result = year.ToString();
                    break;

                case DateStringType.NoSlash:
                    string month = (monthNumber < 10) ? "0" + monthNumber.ToString() : monthNumber.ToString();
                    string day = (dayOfMonth < 10) ? "0" + dayOfMonth.ToString() : dayOfMonth.ToString();
                    result = (year - 1300).ToString() + month + day;
                    break;

                default:
                    break;
            }
            return result;
        }

        public static string GetTime(DateTime date)
        {
            return date.ToString("HH:mm:ss");
        }

        private static readonly string[] pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private static readonly string[] en = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static string ToPersianNumber(this string strNum)
        {
            string chash = strNum;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(en[i], pn[i]);
            return chash;
        }
        public static DateTime? PersianToGregorian(string persianDate)
        {
            if (string.IsNullOrEmpty(persianDate)) return null;

            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                string[] tokens = persianDate.Split('/');

                if (tokens[0].Length == 2)
                    tokens[0] = "13" + tokens[0];
                int year = Int32.Parse(tokens[0]);
                int month = Int32.Parse(tokens[1]);
                int day = Int32.Parse(tokens[2]);
                return PersianToGregorian(day, month, year);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime PersianToGregorian(int day, int month, int year)
        {
            if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1300 || year > 1400) throw new ApplicationException("تاریخ نامعتبر است");

            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }
}