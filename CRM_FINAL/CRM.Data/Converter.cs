using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CRM.Data
{
    public class Converter
    {
        public static int BoolToInt(bool expr)
        {
            if (expr)
                return 1;
            else
                return 0;
        }

        public static string SingleToDoubleDigit(string s)
        {
            if (System.Convert.ToInt16(s) < 10)
                return "0" + s;
            else
                return s;
        }

        public static bool IsValidType(object o, Type t)
        {
            try
            {
                System.Convert.ChangeType(o, t);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static byte[] FileToByteArray(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();

            return data;
        }

        #region NumberToLiteral

        private static string[] yekan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };

        private static string getnum3(int num3)
        {
            string s = string.Empty;
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yekan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }

        internal static string ToLiteral(string number)
        {
            number = number.RemoveComma();
            string literal = string.Empty;

            if (number == "0") return yekan[0];

            number = number.PadLeft(((number.Length - 1) / 3 + 1) * 3, '0');
            int L = number.Length / 3 - 1;
            for (int i = 0; i <= L; i++)
            {
                int b = int.Parse(number.Substring(i * 3, 3));
                if (b != 0)
                    literal = literal + getnum3(b) + " " + basex[L - i] + " و ";
            }
            literal = literal.Substring(0, literal.Length - 3);

            return literal;
        }

        #endregion NumberToLiteral

        public static string UTF8ToWindows1256(string text)
        {
            byte[] ByteStringToConvert = Encoding.UTF8.GetBytes(text);
            byte[] ByteConvertedString = Encoding.Convert(System.Text.Encoding.UTF8, Encoding.GetEncoding("windows-1256"), ByteStringToConvert);
            return System.Text.UnicodeEncoding.UTF8.GetString(ByteConvertedString);
        }
        
    }
}
