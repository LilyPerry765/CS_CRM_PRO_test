using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using CRM.Data;
using System.Windows;
using System.Globalization;
using System.Windows.Media;
using CRM.Application;

namespace CRM.Converters
{
    [ValueConversion(typeof(long?), typeof(string))]
    public class HasValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "ندارد";
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool?), typeof(ImageSource))]
    public class AuthenticationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var authenticationValue = value as Nullable<bool>;
            if (authenticationValue.HasValue && authenticationValue.Value)
            {
                return Helper.GetBitmapImage("Authenticated_24x24.png");
            }
            else if (authenticationValue.HasValue && !authenticationValue.Value)
            {
                return Helper.GetBitmapImage("NotAuthenticated_24x24.png");
            }
            else
            {
                return Helper.GetBitmapImage("WithoutAuthentication_24x24.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


    [ValueConversion(typeof(DateTime?), typeof(string))]
    public class PersianDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateStringType = string.Empty;

            if (parameter != null) dateStringType = parameter.ToString();

            if (value == null)
                return string.Empty;
            else
            {
                if (dateStringType.ToLower().Equals("long"))
                    return Helper.GetPersianDate((DateTime)value, Helper.DateStringType.Long);

                if (dateStringType.ToLower().Equals("compelete"))
                    return Helper.GetPersianDate((DateTime)value, Helper.DateStringType.DateTime);

                else
                    return Helper.GetPersianDate((DateTime)value, Helper.DateStringType.Short);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Helper.PersianToGregorian(value.ToString());
        }
    }

    public class BooleanToSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return true;

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

    }

    [ValueConversion(typeof(byte), typeof(string))]
    public class PostContactColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return Helper.GetEnumNameByValue(typeof(DB.PostContactStatus), (byte)value);
            }
            else
            {
                return "NULL";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(byte), typeof(string))]
    public class RoundTelephoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return Helper.GetEnumDescriptionByValue(typeof(DB.RoundType), (byte)value);
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(byte?), typeof(string))]
    public class PCMStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return Helper.GetEnumDescriptionByValue(typeof(DB.PCMStatus), (int)value);
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }




    [ValueConversion(typeof(byte), typeof(ImageSource))]
    public class PostContactImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if ((byte)value == (byte)DB.PostContactConnectionType.PCMNormal || (byte)value == (byte)DB.PostContactConnectionType.PCMRemote)
                    return Helper.GetBitmapImage("element_red_16x16.png");
                else
                    return Helper.GetBitmapImage("element_16x16.png");
            }
            return Helper.GetBitmapImage("element_16x16.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return Helper.GetBitmapImage(value.ToString());
            }
            else
            {
                return Helper.GetBitmapImage("element_16x16.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class InsertedStatusConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value[0] is List<UsedDocs>)
            {
                List<UsedDocs> refer = (List<UsedDocs>)value[0];
                long cnt = 0;
                if (refer.Count != 0)
                    cnt = refer.Where(t => t.RequestID == (long)value[1] && t.DocumentTypeID == int.Parse(value[2].ToString())).Select(t => t).ToList().Count();
                if (cnt != 0)
                    return true;
                else
                    return false;
            }

            return null;

        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }

    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte integer = (byte)value;
            if (integer == byte.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter;
        }
    }

    public class DocumentTypeConverter : IValueConverter
    {
        Dictionary<string, byte> dic = EnumTypeNameHelper.DocumentTypeEnumValues;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            if (value != null)
            {
                byte a = System.Convert.ToByte(value);
                string s = dic.Where(c => c.Value == a).SingleOrDefault().Key.ToString();
                return s;

            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return dic.Where(t => t.Key == (string)value).FirstOrDefault().Value;
            }
            return -1;
        }
    }

    public class PersonTypeConverter : IValueConverter
    {
        Dictionary<string, byte> dic = EnumTypeNameHelper.PersonTypeEnumValues;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                byte a = System.Convert.ToByte(value);
                string s = dic.Where(c => c.Value == a).SingleOrDefault().Key.ToString();//.Values.Where(c => c == System.Convert.ToByte(2));
                return s;

            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return dic.Where(t => t.Key == (string)value).FirstOrDefault().Value;
            }
            return -1;
        }
    }

    public class TelephoneTypeConverter : IValueConverter
    {
        Dictionary<string, byte> dic = EnumTypeNameHelper.TelephoneTypeEnumValues;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                byte a = System.Convert.ToByte(value);
                string s = dic.Where(c => c.Value == a).SingleOrDefault().Key.ToString();//.Values.Where(c => c == System.Convert.ToByte(2));
                return s;

            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return dic.Where(t => t.Key == (string)value).FirstOrDefault().Value;
            }
            return -1;
        }
    }

    public class OrderTypeConverter : IValueConverter
    {
        Dictionary<string, byte> dic = EnumTypeNameHelper.OrderTypeEnumValues;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                byte a = System.Convert.ToByte(value);
                string s = dic.Where(c => c.Value == a).SingleOrDefault().Key.ToString();//.Values.Where(c => c == System.Convert.ToByte(2));
                return s;

            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return dic.Where(t => t.Key == (string)value).FirstOrDefault().Value;
            }
            return -1;
        }
    }

    public class RequestTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Dictionary<string, int> dic = CRM.Data.DB.GetAllEntity<RequestType>().ToDictionary(t => t.Title, t => t.ID);
            if (value != null)
            {
                int a = System.Convert.ToInt32(value);
                string s = dic.Where(c => c.Value == a).SingleOrDefault().Key.ToString();
                return s;

            }
            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Dictionary<string, int> dic = CRM.Data.DB.GetAllEntity<RequestType>().ToDictionary(t => t.Title, t => t.ID);
            if (value != null)
            {
                return dic.Where(t => t.Key == (string)value).FirstOrDefault().Value;
            }
            return -1;
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
            {
                if (value is Boolean)
                {
                    return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
                }
                else if (value is string)
                {
                    return (value.ToString().ToLower() == "true") ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            else if ((string)parameter == "VisibilitWithCheckBox")
            {
                if (value is Boolean)
                {
                    return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            else if ((string)parameter == "VisibilityNonNULL")
            {

                if (value == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else if ((string)parameter == "VisibilityNonNULLByVisibilityProperty")
            {
                if (value is SubRequesRejectReason)
                {
                    if (value == null)
                    {
                        return Visibility.Collapsed;
                    }
                    else if (((SubRequesRejectReason)value).Description == null && ((SubRequesRejectReason)value).Reason == null)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    if (value == null)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IscheckedToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boolean)
            {
                return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (value is string)
            {
                return (value.ToString().ToLower() == "true") ? Visibility.Visible : Visibility.Collapsed;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class isMalfuctionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return (int)value == (int)DB.CabinetInputStatus.Malfuction ? "True" : "False";
            }


            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BoolToOppositeBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
            {
                throw new InvalidOperationException("The target must be a boolean");
            }

            if (null == value)
            {
                return null;
            }

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
            {
                throw new InvalidOperationException("The target must be a boolean");
            }

            if (null == value)
            {
                return null;
            }
            return !(bool)value;
        }

    }

    //public class ShowRequesRejectDescriptionConverter : IMultiValueConverter
    //{
    //    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        string description;
    //        description = values[0].ToString() + values[1].ToString();
    //        return description;
    //    }
    //    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }

    //}


    public class WorkUnitsConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class ImageNameToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return Helper.GetBitmapImage(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as ImageSource).ToString();
        }
    }

    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class ActionNameToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return Helper.GetBitmapImage(string.Format("Action_{0}.png", value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as ImageSource).ToString();
        }
    }

    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class LastActionNameToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Helper.GetBitmapImage("LastAction_Confirm.png");
            else
            {
                object valueName = Helper.GetEnumNameByValue(typeof(DB.Action), (byte)value);
                return Helper.GetBitmapImage(string.Format("LastAction_{0}.png", valueName.ToString()));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as ImageSource).ToString();
        }
    }

    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class IBSngStatusNameToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Helper.GetBitmapImage("LastAction_Confirm.png");
            else
            {
                object valueName = Helper.GetEnumNameByValue(typeof(DB.IBSngStatus), (int)value);
                return Helper.GetBitmapImage(string.Format("ActionIBSng_{0}.png", valueName.ToString()));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as ImageSource).ToString();
        }
    }

    //TODO:rad
    public class BooleanToFontWeightConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = new object();
            bool tempValue = System.Convert.ToBoolean(value);
            switch (parameter.ToString())
            {
                case "Weight":
                    {
                        if (!tempValue)
                        {
                            result = FontWeights.Normal;
                        }
                        else
                        {
                            result = FontWeights.Bold;
                        }
                        break;
                    }
                case "Style":
                    {
                        if (!tempValue)
                        {
                            result = FontStyles.Normal;
                        }
                        else
                        {
                            result = FontStyles.Italic;
                        }
                        break;
                    }
                case "Decoration":
                    {
                        if (!tempValue)
                        {
                            result = "None";
                        }
                        else
                        {
                            result = TextDecorations.Underline;
                        }
                        break;
                    }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    //TODO:rad
    public class GuidToEnableDisableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            if (value != null && value is Guid)
            {
                result = true;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
