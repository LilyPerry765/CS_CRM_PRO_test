using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PendarPajouhAPI.Helpers
{
    public static class StringHelpers
    {
        public static string ToBase64(string plainText)
        {
            string base64String = string.Empty;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            base64String = Convert.ToBase64String(plainTextBytes);
            return base64String;
        }

        public static string FromBase64(string base64String)
        {
            string plainText = "";
            var base64Bytes = Convert.FromBase64String(base64String);
            plainText = System.Text.Encoding.UTF8.GetString(base64Bytes);
            return plainText;
        }

    }
}