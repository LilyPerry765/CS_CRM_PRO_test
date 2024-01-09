using System;
using System.Globalization;

namespace CRM.Application
{
    public partial class Helper
    {
        public enum DateStringType
        {
            Short, Long, Compelete, TwoDigitsYear, DateTime, Year,Time
        }

        #region Month and Week Settings

        private static string[] PersianMonths = { "", "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        private static string[] QamariMonths = { "", "محرم", "صفر", "ربيع الاول", "ربيع الثاني", "جمادي الاول", "جمادي الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذيقعده", "ذيحجه" };
        private static string[] GregorianMonths = { "", "ژانویه", "فوریه", "مارس", "آپریل", "می", "جون", "جولای", "آگوست", "سپتامبر", "اکتبر", "نوامبر", "دسامبر" };

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

        public static string QamariDayOfWeek(DayOfWeek day)
        {
            string persianDayOfWeek = string.Empty;
            switch (day)
            {
                case DayOfWeek.Saturday:
                    persianDayOfWeek = "السبت";
                    break;

                case DayOfWeek.Sunday:
                    persianDayOfWeek = "الاحد";
                    break;

                case DayOfWeek.Monday:
                    persianDayOfWeek = "الثنين";
                    break;

                case DayOfWeek.Tuesday:
                    persianDayOfWeek = "الثلاثاء";
                    break;

                case DayOfWeek.Wednesday:
                    persianDayOfWeek = "الاربعاء";
                    break;

                case DayOfWeek.Thursday:
                    persianDayOfWeek = "الخميس";
                    break;

                case DayOfWeek.Friday:
                    persianDayOfWeek = "الجمعه";
                    break;

                default:
                    break;
            }

            return persianDayOfWeek;
        }

        #endregion Month and Week Settings

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

                case DateStringType.Time:
                    result = string.Format("{0}", GetTime(date));
                    break;
                default:
                    break;
            }
            return result;
        }
        
        public static string GetQamariDate(DateTime date, DateStringType type)
        {
            HijriCalendar qamariCalendar = new HijriCalendar();
            string result = string.Empty;

            DayOfWeek dayOfWeek = qamariCalendar.GetDayOfWeek(date);
            int dayOfMonth = qamariCalendar.GetDayOfMonth(date);
            int monthNumber = qamariCalendar.GetMonth(date);
            int year = qamariCalendar.GetYear(date);

            switch (type)
            {
                case DateStringType.Short:
                    result = string.Format("{0}/{1}/{2}", year.ToString(), monthNumber.ToString(), dayOfMonth.ToString());
                    break;

                case DateStringType.Long:
                    result = string.Format("{0} {1} {2}", dayOfMonth.ToString(), QamariMonths[monthNumber], year.ToString());
                    break;

                case DateStringType.Compelete:
                    result = string.Format("{0}، {1} {2} {3}", QamariDayOfWeek(dayOfWeek), dayOfMonth.ToString(), QamariMonths[monthNumber], year.ToString());
                    break;

                default:
                    break;
            }
            return result;
        }
        
        public static string GetGregorianDate(DateTime date, DateStringType type)
        {
            string result = string.Empty;

            switch (type)
            {
                case DateStringType.Short:
                    result = date.ToShortDateString();
                    break;

                case DateStringType.Long:
                    result = string.Format("{0} {1} {2}", date.Day.ToString(), GregorianMonths[date.Month], date.Year.ToString());
                    break;

                case DateStringType.Compelete:
                    result = string.Format("{0}، {1} {2} {3}", PersianDayOfWeek(date.DayOfWeek), date.Day.ToString(), GregorianMonths[date.Month], date.Year.ToString());
                    break;

                default:
                    break;
            }
            return result;
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
                else if ((month + monthsToAdd) == 0)
                {
                    year -= 1 ;
                    month = 12;
                }
                else
                {
                    month = (month + monthsToAdd);
                }
            }

            return string.Format("{0}/{1}/{2}", year.ToString(), month.ToString(), day.ToString());

        }

        public static string ChangeShortPersianFormat(string persianShortDate, DateStringType type)
        {
            string[] tokens = persianShortDate.Split('/');
            if (tokens[0].Length == 2)
                tokens[0] = "13" + tokens[0];
            int year = Int32.Parse(tokens[0]);
            int month = Int32.Parse(tokens[1]);
            int day = Int32.Parse(tokens[2]);
            string result = string.Empty;

            switch (type)
            {
                case DateStringType.Short:
                    result = string.Format("{0}/{1}/{2}", year.ToString(), Helper.SingleToDoubleDigit(month.ToString()), Helper.SingleToDoubleDigit(day.ToString()));
                    break;

                case DateStringType.Long:
                    result = string.Format("{0} {1} {2}", day.ToString(), PersianMonths[month], year.ToString());
                    break;

                case DateStringType.Compelete:
                    result = string.Format("{0}، {1} {2} {3}", PersianDayOfWeek(PersianToGregorian(persianShortDate).Value.DayOfWeek), day.ToString(), PersianMonths[month], year.ToString());
                    break;

                default:
                    break;
            }

            return result;
        }

        public static string ValidatePersianDate(string shortPersianDate)
        {
            return ChangeShortPersianFormat(shortPersianDate, DateStringType.Short);
        }

        public static string GetTime(DateTime date)
        {
            return date.ToString("HH:mm:ss");
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
            return  Helper.GetPersianDate(persianCalendar.AddDays(Helper.PersianToGregorian(startDateEachPart).Value.Date, addDay) , DateStringType.Short);
        }

    }
}