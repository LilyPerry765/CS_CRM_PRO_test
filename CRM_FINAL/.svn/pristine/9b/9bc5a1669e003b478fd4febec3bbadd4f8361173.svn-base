using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CRMConvertApplication
{
    public static class Date
    {
        private static string[] PersianMonths = { "", "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
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
                    result = string.Format("{0}/{1}/{2}", year.ToString(), monthNumber.ToString(), dayOfMonth.ToString());
                    break;

                case DateStringType.Long:
                    result = string.Format("{0} {1} {2}", dayOfMonth.ToString(), PersianMonths[monthNumber], year.ToString());
                    break;

                case DateStringType.Compelete:
                    result = string.Format("{0}، {1} {2} {3}", PersianDayOfWeek(dayOfWeek), dayOfMonth.ToString(), PersianMonths[monthNumber], year.ToString());
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

        public static string AddMonthToPersianDate(string persianShortDate, int monthsToAdd)
        {
            string[] tokens = persianShortDate.Split('/');
            if (tokens[0].Length == 2)
                tokens[0] = "13" + tokens[0];
            int year = Int32.Parse(tokens[0]);
            int month = Int32.Parse(tokens[1]);
            int day = Int32.Parse(tokens[2]);

            if ((month + monthsToAdd) == 24)
            {
                year += 1;
            }
            else
            {
                if ((month + monthsToAdd) > 12)
                {
                    year += (month + monthsToAdd) / 12;
                    month = (month + monthsToAdd) % 12;
                }
                else
                {
                    month = (month + monthsToAdd);
                }
            }

            return string.Format("{0}/{1}/{2}", year.ToString(), month.ToString(), day.ToString());

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

        public static string GetLastDayOfMount(string persianShortDate)
        {
            string[] tokens = persianShortDate.Split('/');
            if (tokens[0].Length == 2)
                tokens[0] = "13" + tokens[0];
            int year = Int32.Parse(tokens[0]);
            int month = Int32.Parse(tokens[1]);
            int day = Int32.Parse(tokens[2]);
            return string.Format("{0}/{1}/{2}", year.ToString(), month.ToString(), (month > 6 ? (month == 12 && (year % 4) != 3) ? 29 : 30 : 31));

        }

        public static string GetPersianDateAddDays(string startDateEachPart, int addDay)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return Date.GetPersianDate(persianCalendar.AddDays(Date.PersianToGregorian(startDateEachPart).Value.Date, addDay), DateStringType.Short);
        }
    }
}
