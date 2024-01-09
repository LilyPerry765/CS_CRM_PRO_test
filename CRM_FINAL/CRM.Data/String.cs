using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRM.Data
{
    public class Strings
    {
        public static string SubString(string s, int maxLength)
        {
            if (s.Length > maxLength)
            {
                s = s.Substring(0, maxLength);
                s = (s.IndexOf(' ') > 0) ? s.Substring(0, s.LastIndexOf(' ')) : s;
                return s + " ...";
            }
            else
                return s;
        }

        public static string RemoveComma(string s)
        {
            return s.Replace(",", string.Empty);
        }

        public static string FormatCurrency(int? s)
        {
            return (s == null) ? string.Empty : (s ?? 0).ToString("#،0.#");
        }

        public static string FormatCurrency(long? s)
        {
            return (s == null) ? string.Empty : (s ?? 0).ToString("#،0.#");
        }

        public static string FormatCurrency(string s)
        {
            s = RemoveComma(s);

            if (s.Length <= 3) return s;

            char[] c = s.ToCharArray();
            int point = c.Length % 3;
            string output = string.Empty;

            for (int i = 0; i < c.Length; i++)
            {
                if (i % 3 == point && i != 0)
                    output += ",";
                output += c[i];
            }
            return output;
        }

        public static string ReverseString(string s)
        {
            char[] c = s.ToCharArray();
            string output = string.Empty;
            for (int i = c.Length - 1; i >= 0; i--)
            {
                output += c[i];
            }
            return output;
        }

        public static string StripTags(string s)
        {
            while (s.IndexOf('<') >= 0)
            {
                s = s.Substring(0, s.IndexOf('<')) + " " + s.Substring(s.IndexOf('>') + 1);
            }
            return s;
        }

        public static string SearchTokensForSQL(string s)
        {
            if ((s.IndexOf("\"") == 0) && (s.LastIndexOf("\"") == s.Length - 1))
                return string.Format("\"{0}\"", System.Web.HttpContext.Current.Server.HtmlEncode(s.Replace("\"", string.Empty)));

            string[] delemiters = { " ", "-" };
            string[] tokens = s.Split(delemiters, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
                tokens[i] = System.Web.HttpContext.Current.Server.HtmlEncode(tokens[i]);
            return string.Format("\"{0}\"", string.Join("\" AND \"", tokens));
        }

        public static string AddZeroPrefix(string s, int digits)
        {
            //s.PadLeft(digits, '0');
            int length = s.Length;

            if (length >= digits) return s;

            for (int i = 0; i < digits - length; i++) s = "0" + s;

            return s;
        }

        public static string GetCheckDigit(string digits)
        {
            char[] chars = digits.ToCharArray();
            int sum = 0;

            for (int i = chars.Length - 1, mul = 2; i >= 0; i--, mul++)
            {
                if (mul > 7) mul = 2;
                sum += Convert.ToInt16(chars[i].ToString()) * mul;
            }

            int remained = sum % 11;

            if (remained <= 1) return "0";

            return (11 - remained).ToString();
        }
    }
}
