using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media;
using CRM.Data;
using System.ComponentModel;
using CRM.Application.Codes;
using System.Windows.Controls;
using CRM.Application.Reports.Viewer;
using System.Collections;
using Stimulsoft.Report.Components;
using Stimulsoft.Base.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using CRM.Application.UserControls;
using CRM.Application.Codes.CustomControls;
using System.Net.NetworkInformation;
using Enterprise;
using System.Configuration;

namespace CRM.Application
{
    public partial class Helper
    {

        public static DataGridRow GetRow(DataGrid dataGrid, int index)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dataGrid.ScrollIntoView(dataGrid.Items[index]);
                dataGrid.UpdateLayout();
                row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static Uri MakePackUri(string relativeFile)
        {
            string uriString = "pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" + relativeFile;
            return new Uri(uriString);
        }

        public static BitmapImage GetBitmapImage(string relativeImage)
        {
            string imagePath = string.Format("pack://application:,,,/{0};component/Images/{1}", Assembly.GetExecutingAssembly().GetName().Name, relativeImage);
            Uri uri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }

        public static System.Drawing.Bitmap ConvertFrom(BitmapImage bitmalImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder en = new BmpBitmapEncoder();
                en.Frames.Add(BitmapFrame.Create(bitmalImage));
                en.Save(stream);
                System.Drawing.Bitmap result = new System.Drawing.Bitmap(stream);
                return new System.Drawing.Bitmap(result);
            }
        }

        public static void ToggleControlFade(FrameworkElement control, int speed)
        {
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, speed);

            if (control.Opacity == 0) control.Opacity = 1.0;

            DoubleAnimation animation = new DoubleAnimation { From = 1.0, To = 0.0, Duration = new Duration(duration) };

            //if (control.Opacity == 0.0)
            //{
            //    //animation = new DoubleAnimation { From = 0.0, To = 1.0, Duration = new Duration(duration) };
            //}

            Storyboard.SetTargetName(animation, control.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity", 1));
            storyboard.Children.Add(animation);

            storyboard.Begin(control);
        }

        public static int Compare<T>(T x, T y)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public);
            FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public);
            int compareValue = 0;

            foreach (PropertyInfo property in properties)
            {
                IComparable valx = property.GetValue(x, null) as IComparable;
                if (valx == null)
                    continue;
                object valy = property.GetValue(y, null);
                compareValue = valx.CompareTo(valy);
                if (compareValue != 0)
                    return compareValue;
            }
            foreach (FieldInfo field in fields)
            {
                IComparable valx = field.GetValue(x) as IComparable;
                if (valx == null)
                    continue;
                object valy = field.GetValue(y);
                compareValue = valx.CompareTo(valy);
                if (compareValue != 0)
                    return compareValue;
            }

            return compareValue;
        }

        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null)
                return null;

            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindVisualParent<T>(parentObject);
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {


                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    string controlName = child.GetValue(System.Windows.Controls.Control.NameProperty) as string;
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(System.Windows.Controls.Control.NameProperty) as string;
                if (controlName == name)
                    return child as T;

                var result = FindVisualChildByName<T>(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var result = child as T ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        public static T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var result = FindFirstElementInVisualTree<T>(child);
                    if (result != null)
                        return result;

                }
            }
            return null;
        }

        public static List<Pair> GetEnumNameValue(Type enumType)
        {
            List<Pair> list = new List<Pair>();

            FieldInfo[] fieldInfos = enumType.GetFields().Where(t => t.IsLiteral).ToArray();

            foreach (FieldInfo item in fieldInfos)
                list.Add(new Pair()
                {
                    Name = (item.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute).Description,
                    Value = item.GetRawConstantValue()
                }
            );

            return list;
        }

        public static string GetEnumNameByValue(Type enumType, int value)
        {
            return enumType.GetFields().Where(t => t.IsLiteral && Convert.ToInt32(t.GetRawConstantValue()) == value).Select(t => t.Name).SingleOrDefault();
        }

        public static string GetEnumDescriptionByValue(Type enumType, int value)
        {
            FieldInfo fieldInfos = enumType.GetFields().Where(t => t.IsLiteral && Convert.ToInt32(t.GetRawConstantValue()) == value).SingleOrDefault();
            return (fieldInfos.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute).Description;
        }

        public static List<CheckableItem> GetEnumCheckable(Type enumType)
        {
            List<CheckableItem> list = new List<CheckableItem>();

            FieldInfo[] fieldInfos = enumType.GetFields().Where(t => t.IsLiteral).ToArray();

            foreach (FieldInfo item in fieldInfos)
                list.Add(new CheckableItem()
                {
                    ID = Convert.ToInt32(item.GetRawConstantValue()),
                    Name = (item.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute).Description,
                    IsChecked = false
                }
            );

            return list;
        }

        public static List<EnumItem> GetEnumItem(Type enumType)
        {
            List<EnumItem> list = new List<EnumItem>();

            FieldInfo[] fieldInfos = enumType.GetFields().Where(t => t.IsLiteral).ToArray();

            foreach (FieldInfo item in fieldInfos)
                list.Add(new EnumItem()
                {
                    ID = Convert.ToByte(item.GetRawConstantValue()),
                    Name = (item.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute).Description,
                    OriginalName = item.Name,
                    IsChecked = false
                }
            );

            return list;
        }
        public static List<EnumItem> GetEnumItems(Type enumType)
        {
            List<EnumItem> result = new List<EnumItem>();
            FieldInfo[] fields = enumType.GetFields().Where(f => f.IsLiteral).ToArray();
            foreach (FieldInfo fi in fields)
            {
                result.Add(new EnumItem { Name = fi.Name, EnumValue = Convert.ToInt32(fi.GetRawConstantValue()) });
            }
            return result;
        }
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

        /// <summary>
        /// .یک رشته را میگیرد و  تعیین میکند که تمامی کاراکترهای آن عددی هستند یا خیر
        /// </summary>
        /// <param name="strInput">رشته مورد نظر</param>
        /// <returns></returns>
        public static bool AllCharacterIsNumber(string strInput)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(strInput))
            {
                foreach (char c in strInput)
                {
                    if (Char.IsNumber(c))
                    {
                        continue;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// .تعیین میکند که آیا تمامی اعضای عمومی یک شیء از یک کلاس خالی هستند یا خیر
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool AllPropertyIsEmpty(object instance)
        {
            try
            {
                bool allPropertyIsempty = true;
                if (instance != null)
                {
                    var type = instance.GetType();
                    var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    allPropertyIsempty = !properties.Select(p => p.GetValue(instance, null)).Any(va => !va.IsNullOrEmpty());
                }
                else
                {
                    throw new ArgumentException("AllPropertyIsEmpty method need a non-null argument.");
                }
                return allPropertyIsempty;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// .تعیین میکند که مقدار تعیین شده برای یک فیلدی عددی در یک کنترل خاص معتبر و دارای مقادیر عددی هست یا خیر
        /// </summary>
        /// <param name="controlForChecking">کنترل خاص</param>
        /// <param name="resultNumber">.در صورت عددی بودن ، این پارامتر با مقدار وارد شده ، پر شده و  برگردانده میشود</param>
        /// <returns></returns>
        public static bool CheckDigitDataEntry(TextBox controlForChecking, out long? resultNumber)
        {
            string currentValue = controlForChecking.Text.Trim();
            bool isValidNumber = true;
            resultNumber = null;

            if (!string.IsNullOrEmpty(currentValue) && Helper.AllCharacterIsNumber(currentValue))
            {
                resultNumber = long.Parse(currentValue);
                isValidNumber = true;
            }
            else if (!string.IsNullOrEmpty(currentValue) && !Helper.AllCharacterIsNumber(currentValue))
            {
                resultNumber = null;
                isValidNumber = false;
                controlForChecking.Focus();
            }

            return isValidNumber;
        }

        /// <summary>
        ///.پیغامی را مبنی بر این که برنامه در حال ساخت گزارش است نمایش میدهد
        /// </summary>
        /// <param name="obj"></param>
        public static void ShowReportGeneratingInfo(DependencyObject obj)
        {
            DependencyObject mainParent = FindVisualParent<ReportList>(obj);

            Label reportGeneratingInfoLabel = FindVisualChildren<Label>(mainParent).Where(l => l.Tag != null && l.Tag.ToString().Equals("ReportGeneratingInfoLabel")).SingleOrDefault();
            reportGeneratingInfoLabel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// .پیغام در حال ایجاد گزارش را پاک میکند
        /// </summary>
        /// <param name="obj"></param>
        public static void CollapseReportGeneratingInfo(DependencyObject obj)
        {
            DependencyObject mainParent = FindVisualParent<ReportList>(obj);
            Label reportGeneratingInfoLabel = FindVisualChildren<Label>(mainParent).Where(l => l.Tag != null && l.Tag.ToString().Equals("ReportGeneratingInfoLabel")).SingleOrDefault();
            reportGeneratingInfoLabel.Visibility = Visibility.Collapsed;
        }

        public static List<T> GetLogicalChildCollection<T>(object parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            GetLogicalChildCollection(parent as DependencyObject, logicalCollection);
            return logicalCollection;
        }

        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

        //TODO:rad
        /// <summary>
        /// .در زمان تنظیمات گزارش با توجّه به تنظیمات مشخص شده ، یک گزارش فرضی را ایجاد میکند
        /// </summary>
        /// <param name="data">لیست فرضی برای پر کردن گزارش</param>
        /// <param name="headerFont">فونت هدر</param>
        /// <param name="headerFontIsBold">آیا فونت هدر بولد میباشد یا خیر</param>
        /// <param name="headerFontIsItalic">آیا فونت هدر ایتالیک میباشد یا خیر</param>
        /// <param name="headerFontIsUnderlined">آیا زیر فونت هدر خط کشیده میشود یا خیر</param>
        /// <param name="headerBackground">رنگ زمینه هدر</param>
        /// <param name="headerForeground">رنگ فونت هدر</param>
        /// <param name="headerBorderBrush">رنگ حاشیه هدر</param>
        /// <param name="headerBorderThickness">ضخامت حاشیه اطراف هدر</param>
        /// <param name="textFont">فونت متن گزارش</param>
        /// <param name="textFontIsBold">آیا فونت متن گزارش بولد میباشد یا خیر</param>
        /// <param name="textFontIsItalic">آیا فونت متن گزارش ایتالیک میباشد یا خیر</param>
        /// <param name="textFontIsUnderlined">آیا زیر فونت متن گزارش خط کشیده میشود یا خیر</param>
        /// <param name="textForeground">رنگ فونت متن</param>
        /// <param name="textBackground">رنگ زمینه متن گزارش</param>
        /// <param name="textBorderBrsuh">رنگ حاشیه سلول های متن گزارش</param>
        /// <param name="textBorderThcikness">ضخامت حاشیه اطراف سلول های متن گزارش</param>
        /// <param name="textHasWordWrap"></param>
        /// <param name="headerHasWordWrap"></param>
        /// <param name="printWithPreview">آیا گزارش دارای پیش نمایش باشد یا مستقیم به چاپگر ارسال شود</param>
        /// <param name="pageOrientation"></param>
        /// <param name="reportHasPagerFooter">آیا گزارش دارای فوتر باشد یا خیر</param>
        /// <param name="reportHasTitle">آیا گزارش دارای عنوان باشد یا خیر</param>
        /// <param name="reportHasDate">آیا گزارش دارای تاریخ باشد یا خیر</param>
        /// <param name="reportHasLogo">آیا گزارش دارای لوگوی مخابرات باشد یا خیر</param>
        /// <param name="reportHasTime">آیا گزارش دارای ساعت باشد یا خیر</param>
        /// <param name="reportSumRecordsQuantity">آیا گزارش تعداد کل رکوردهای گزارش را نمایش دهد</param>
        public static void ShowSampleReport(
                                            IEnumerable data, System.Drawing.Font headerFont, bool headerFontIsBold, bool headerFontIsItalic, bool headerFontIsUnderlined, SolidColorBrush headerBackground, SolidColorBrush headerForeground,
                                            SolidColorBrush headerBorderBrush, double headerBorderThickness, System.Drawing.Font textFont, bool textFontIsBold, bool textFontIsItalic, bool textFontIsUnderlined, SolidColorBrush textForeground,
                                            SolidColorBrush textBackground, SolidColorBrush textBorderBrsuh, double textBorderThcikness,
                                            bool textHasWordWrap, bool headerHasWordWrap, bool printWithPreview, int pageOrientation, bool reportHasPagerFooter,
                                            bool reportHasTitle, bool reportHasDate, bool reportHasTime, bool reportHasLogo, bool reportSumRecordsQuantity
                                           )
        {
            try
            {
                //ثبت دیتای مورد نیاز برای ایجاد گزارش
                BlankReport stiReport = new BlankReport();
                stiReport.RegData("result", "result", data);
                DateTime currentDate = DB.GetServerDate();
                StiDataBand dataBand = stiReport.GetComponentByName("MainDataBand") as StiDataBand;
                StiHeaderBand headerBand = stiReport.GetComponentByName("MainHeaderBand") as StiHeaderBand;
                StiReportTitleBand mainReportTitleBand = stiReport.GetComponentByName("MainReportTitleBand") as StiReportTitleBand;

                StiImage telecomImage = stiReport.GetComponentByName("TelecomImage") as StiImage;
                StiText dateText = stiReport.GetComponentByName("ReportDateText") as StiText;
                StiText timeText = stiReport.GetComponentByName("ReportTimeText") as StiText;
                StiText dateVariableText = stiReport.GetComponentByName("ReportDateVariableText") as StiText;
                StiText timeVariableText = stiReport.GetComponentByName("ReportTimeVariableText") as StiText;

                StiGroupFooterBand mainGroupFooterBand = stiReport.GetComponentByName("MainGroupFooterBand") as StiGroupFooterBand;
                StiGroupHeaderBand mainGroupHeaderBand = stiReport.GetComponentByName("MainGroupHeaderBand") as StiGroupHeaderBand;
                StiText counText = stiReport.GetComponentByName("CountText") as StiText;

                if (pageOrientation != 0)
                {
                    stiReport.MainPage.Orientation = (StiPageOrientation)pageOrientation;
                }

                //آیا گزارش تعداد کل رکوردها را نمایش دهد یا خیر
                if (reportSumRecordsQuantity)
                {
                    mainGroupFooterBand.Enabled = true;
                    mainGroupHeaderBand.Enabled = true;
                    counText.Enabled = true;
                }

                //آیا گزارش دارای لوگوی مخابرات هست یا خیر
                if (reportHasLogo)
                {
                    using (Stream stream = CRM.Application.App.GetResourceStream(MakePackUri("/Images/logo.png")).Stream)
                    {
                        telecomImage.Image = System.Drawing.Image.FromStream(stream);
                        telecomImage.Enabled = true;
                    }
                }

                //آیا گزارش دارای تاریخ میباشد یا خیر
                if (reportHasDate)
                {
                    stiReport.ReportDateVariable = currentDate.ToPersian(Date.DateStringType.Short);
                    dateText.Enabled = true;
                    dateVariableText.Enabled = true;
                }

                //آیا گزارش دارای زمان میباشد یا خیر
                if (reportHasTime)
                {
                    stiReport.ReportTimeVariable = Date.GetTime(currentDate);
                    timeText.Enabled = true;
                    timeVariableText.Enabled = true;
                }

                //فونت آیتم های موجود در هدر
                if (headerFont != null)
                {
                    foreach (StiText child in headerBand.Components.OfType<StiText>())
                    {
                        child.Font = headerFont;
                    }
                }

                //رنگ زمینه هدر
                if (headerBackground != null)
                {
                    foreach (StiText child in headerBand.Components.OfType<StiText>())
                    {
                        child.Brush = new StiSolidBrush(System.Drawing.Color.FromArgb(headerBackground.Color.A, headerBackground.Color.R, headerBackground.Color.G, headerBackground.Color.B));
                    }
                }

                //رنگ فونت هدر
                if (headerForeground != null)
                {
                    foreach (StiText child in headerBand.Components.OfType<StiText>())
                    {
                        child.TextBrush = new StiSolidBrush(System.Drawing.Color.FromArgb(headerForeground.Color.A, headerForeground.Color.R, headerForeground.Color.G, headerForeground.Color.B));
                    }
                }

                //رنگ و میزان ضخامت حاشیه اطراف آیتم های موجود در هدر
                if (headerBorderBrush != null)
                {
                    foreach (StiText child in headerBand.Components.OfType<StiText>())
                    {
                        child.Border = new StiBorder(
                                                     StiBorderSides.All,
                                                     System.Drawing.Color.FromArgb(headerBorderBrush.Color.A, headerBorderBrush.Color.R, headerBorderBrush.Color.G, headerBorderBrush.Color.B),
                                                     headerBorderThickness,
                                                     StiPenStyle.Solid
                                                    );
                    }
                }

                //تنظیم فونت متون گزارش
                if (textFont != null)
                {
                    foreach (StiText child in dataBand.Components.OfType<StiText>())
                    {
                        child.Font = textFont;
                    }
                }

                //تنظیم رنگ فونت متون گزارش
                if (textForeground != null)
                {
                    foreach (StiText child in dataBand.Components.OfType<StiText>())
                    {
                        child.TextBrush = new StiSolidBrush(System.Drawing.Color.FromArgb(textForeground.Color.A, textForeground.Color.R, textForeground.Color.G, textForeground.Color.B));
                    }
                }

                //تنظیم رنگ زمینه آیتمهای داخل گزارش
                if (textBackground != null)
                {
                    foreach (StiText child in dataBand.Components.OfType<StiText>())
                    {
                        child.Brush = new StiSolidBrush(System.Drawing.Color.FromArgb(textBackground.Color.A, textBackground.Color.R, textBackground.Color.G, textBackground.Color.B));
                    }
                }

                //تنظیم رنگ و میزان ضخامت حاشیه اطراف آیتمهای موجود در متن گزارش
                if (textBorderBrsuh != null)
                {
                    foreach (StiText child in dataBand.Components.OfType<StiText>())
                    {
                        child.Border = new StiBorder(
                                                     StiBorderSides.All,
                                                     System.Drawing.Color.FromArgb(textBorderBrsuh.Color.A, textBorderBrsuh.Color.R, textBorderBrsuh.Color.G, textBorderBrsuh.Color.B),
                                                     textBorderThcikness,
                                                     StiPenStyle.Solid
                                                    );
                    }
                }

                //چنانچه متن یک آیتم در هدر بیشتر از فضای اختصاص داده شده به آن باشد، قسمتی از آن دیده نخواهد شد راه حل این مشکل بلاک زیر میباشد
                if (headerHasWordWrap)
                {
                    foreach (StiText headerChild in headerBand.Components.OfType<StiText>())
                    {
                        StiTextOptions textOptions = new StiTextOptions();
                        textOptions.WordWrap = true;
                        headerChild.TextOptions = textOptions;
                        headerChild.CanGrow = true;
                        headerChild.GrowToHeight = true;
                    }
                }

                //چنانچه متن یک آیتم بیشتر از فضای اختصاص داده شده به آن باشد ،قسمتی از آن متن دیده نخواهد شد . راه حل این مشکل بلاک زیر میباشد
                if (textHasWordWrap)
                {
                    foreach (StiText textChild in dataBand.Components.OfType<StiText>())
                    {
                        StiTextOptions textOptions = new StiTextOptions();
                        textOptions.WordWrap = true;
                        textChild.TextOptions = textOptions;
                        textChild.CanGrow = true;
                        textChild.GrowToHeight = true;
                    }
                }

                //گزارش دارای فوتر باشد یا خیر
                if (reportHasPagerFooter)
                {
                    stiReport.MainPage.Components.Add(stiReport.PageFooterBand);
                }

                //آیا گزارش دارای عنوان باشد یا خیر
                if (reportHasTitle)
                {
                    //وقتی گزارش دارای عنوان باشد آنگاه باید نام ستون های گزارش با محدوده ی عنوان فاصله داشته باشند
                    //بنابراین باید فضای بیشتری باید به آن ها بدهیم و از بالا صفحه فاصله بگیرند
                    stiReport.MainHeaderBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.2, 7.49, 0.6);
                    foreach (StiText childHeader in stiReport.MainHeaderBand.Components.OfType<StiText>())
                    {
                        childHeader.Top = 0.1;
                    }
                    stiReport.ReportTitleVariable = "گزارش نمونه";
                    stiReport.TitleText.Enabled = true;
                }

                //دستور کامپایل نباید برای این گزارش نوشته شود چون به خطا میخورد 
                //stiReport.ParentReport
                //چون خاصیت بالا مقداردهی میشود و باعث میشود تا کلاس پرنت همان خاصیت هایی دا داشته باشد که گزارش جاری ما دارد
                //ایجاد هم نامی در شی گرایی خطاست
                //stiReport.Page1 stiReport.ParentReport.Page1
                //stiReport.Compile();
                //stiReport.ShowWithRibbonGUI();

                //تنظیمات برای نمایش گزارش ایجاد شده
                stiReport.Render();
                if (printWithPreview)
                {
                    stiReport.Show(true);
                }
                else
                {
                    stiReport.Print(true);
                }
                MessageBoxResult result = MessageBox.Show("آیا تنظیمات گزارش ذخیره شود؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    ReportSetting currentSetting = new ReportSetting();
                    StiText headerBandChildItem = headerBand.Components.OfType<StiText>().FirstOrDefault();
                    StiText dataBandChildItem = dataBand.Components.OfType<StiText>().FirstOrDefault();
                    currentSetting.HeaderBackground = (headerBackground != null) ? headerBackground : new SolidColorBrush(
                                                                                                                            System.Windows.Media.Color.FromArgb(
                                                                                                                                                                 StiBrush.ToColor(headerBandChildItem.Brush).A,
                                                                                                                                                                 StiBrush.ToColor(headerBandChildItem.Brush).R,
                                                                                                                                                                 StiBrush.ToColor(headerBandChildItem.Brush).G,
                                                                                                                                                                 StiBrush.ToColor(headerBandChildItem.Brush).B
                                                                                                                                                                )
                                                                                                                         );
                    currentSetting.HeaderBorderBrush = (headerBorderBrush != null) ? headerBorderBrush : new SolidColorBrush(
                                                                                                                              System.Windows.Media.Color.FromArgb(
                                                                                                                                                                  headerBandChildItem.Border.Color.A,
                                                                                                                                                                  headerBandChildItem.Border.Color.R,
                                                                                                                                                                  headerBandChildItem.Border.Color.G,
                                                                                                                                                                  headerBandChildItem.Border.Color.B
                                                                                                                                                                 )
                                                                                                                            );
                    currentSetting.HeaderBorderThickness = headerBorderThickness;
                    currentSetting.HeaderFont = (headerFont != null) ? headerFont : headerBandChildItem.Font;
                    currentSetting.HeaderForeground = (headerForeground != null) ? headerForeground : new SolidColorBrush(
                                                                                                                          System.Windows.Media.Color.FromArgb(
                                                                                                                                                                StiBrush.ToColor(headerBandChildItem.TextBrush).A,
                                                                                                                                                                StiBrush.ToColor(headerBandChildItem.TextBrush).R,
                                                                                                                                                                StiBrush.ToColor(headerBandChildItem.TextBrush).G,
                                                                                                                                                                StiBrush.ToColor(headerBandChildItem.TextBrush).B
                                                                                                                                                              )
                                                                                                                          );
                    currentSetting.StiPageOrientation = (pageOrientation > 0) ? pageOrientation : (int)StiPageOrientation.Portrait;
                    currentSetting.TextBackground = (textBackground != null) ? textBackground : new SolidColorBrush(
                                                                                                                        System.Windows.Media.Color.FromArgb(
                                                                                                                                                             StiBrush.ToColor(dataBandChildItem.Brush).A,
                                                                                                                                                             StiBrush.ToColor(dataBandChildItem.Brush).R,
                                                                                                                                                             StiBrush.ToColor(dataBandChildItem.Brush).G,
                                                                                                                                                             StiBrush.ToColor(dataBandChildItem.Brush).B
                                                                                                                                                           )
                                                                                                                   );
                    currentSetting.TextBorderBrush = (textBorderBrsuh != null) ? textBorderBrsuh : new SolidColorBrush(
                                                                                                                        System.Windows.Media.Color.FromArgb(
                                                                                                                                                             dataBandChildItem.Border.Color.A,
                                                                                                                                                             dataBandChildItem.Border.Color.R,
                                                                                                                                                             dataBandChildItem.Border.Color.G,
                                                                                                                                                             dataBandChildItem.Border.Color.B
                                                                                                                                                           )
                                                                                                                      );
                    currentSetting.TextBorderThickness = textBorderThcikness;
                    currentSetting.TextFont = (textFont != null) ? textFont : dataBandChildItem.Font;
                    currentSetting.TextForeground = (textForeground != null) ? textForeground : new SolidColorBrush(
                                                                                                                     System.Windows.Media.Color.FromArgb(
                                                                                                                                                          StiBrush.ToColor(dataBandChildItem.TextBrush).A,
                                                                                                                                                          StiBrush.ToColor(dataBandChildItem.TextBrush).R,
                                                                                                                                                          StiBrush.ToColor(dataBandChildItem.TextBrush).G,
                                                                                                                                                          StiBrush.ToColor(dataBandChildItem.TextBrush).B
                                                                                                                                                        )
                                                                                                                    );
                    currentSetting.TextHasWordWrap = textHasWordWrap;
                    currentSetting.HeaderHasWordWrap = headerHasWordWrap;
                    currentSetting.ReportHasPageFooter = reportHasPagerFooter;
                    currentSetting.ReportHasTitle = reportHasTitle;
                    currentSetting.PrintWithPreview = printWithPreview;
                    currentSetting.HeaderFontIsBold = headerFontIsBold;
                    currentSetting.HeaderFontIsItalic = headerFontIsItalic;
                    currentSetting.HeaderFontIsUnderlined = headerFontIsUnderlined;
                    currentSetting.TextFontIsBold = textFontIsBold;
                    currentSetting.TextFontIsItalic = textFontIsItalic;
                    currentSetting.TextFontIsUnderlined = textFontIsUnderlined;
                    currentSetting.ReportHasDate = reportHasDate;
                    currentSetting.ReportHasLogo = reportHasLogo;
                    currentSetting.ReportHasTime = reportHasTime;
                    currentSetting.ReportSumRecordsQuantity = reportSumRecordsQuantity;
                    var serializedObject = ReportSettingSerialize(currentSetting);

                    //چون تنظیمات گزارش در دیتا بیس ذخیره نمیشود و باید برای هر کاربری منحصر به فرد باشد باید نام فایل تنظیمات ذخیره شده به صورت زیر تعیین گردد
                    string reportSettingFileName = string.Format("ReportSetting{0}.xml", DB.CurrentUser.ID);
                    DirectoryInfo createdDirectoryInfo = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\WindowsSecurity");
                    serializedObject.Save(createdDirectoryInfo.FullName + "\\" + reportSettingFileName);

                    //بعد از اتمام عملیات ذخیره فایل تنظیمات باید تنظیمات کاربر جاری ، از نو لود شود
                    DB.CurrentUser.ReportSetting = Helpers.GetReportDefaultSettings(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\WindowsSecurity");

                    MessageBox.Show(".فایل تنظیمات گزارش باموفقیت ذخیره شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TODO:rad
        public static XmlDocument ReportSettingSerialize(object instance)
        {
            Type instanceType = instance.GetType();
            XmlDocument result = new XmlDocument();

            XmlDeclaration declartion = result.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
            result.PrependChild(declartion);

            XmlAttribute creationDateAttribute = result.CreateAttribute("CreationDate", string.Empty);
            creationDateAttribute.InnerText = GetCurrentPersianData();

            XmlAttribute userIdAttribute = result.CreateAttribute("UserID", string.Empty);
            userIdAttribute.InnerText = DB.CurrentUser.ID.ToString();

            XmlElement rootElement = result.CreateElement(instanceType.Name);
            rootElement.Attributes.Append(creationDateAttribute);
            rootElement.Attributes.Append(userIdAttribute);
            result.AppendChild(rootElement);

            XmlElement element = null;
            PropertyInfo[] properties = instanceType.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                element = result.CreateElement(pi.Name);
                var tempValue = pi.GetValue(instance, null);
                element.InnerText = (tempValue != null) ? tempValue.ToString() : string.Empty;
                rootElement.AppendChild(element);
            }
            return result;
        }

        //TODO:rad
        /// <summary>
        /// .تاریخ جاری را به صورت شمسی برمیگرداند
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPersianData()
        {
            string result = string.Empty;
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            DateTime now = DateTime.Now;
            int year = pc.GetYear(now);
            int month = pc.GetMonth(now);
            int day = pc.GetDayOfMonth(now);
            string time = TimeZone.CurrentTimeZone.ToLocalTime(now).ToShortTimeString();
            result = string.Format("{0}/{1}/{2} - {3}", year, month, day, time);
            return result;
        }

        public static void ResetSearch(System.Windows.UIElement container)
        {
            List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

            foreach (ComboBox control in controlsList.Where(t => t.GetType() == typeof(ComboBox)).ToList())
            {
                control.SelectedIndex = -1;
            }

            foreach (TextBox control in controlsList.Where(t => t.GetType() == typeof(TextBox)).ToList())
            {
                control.Text = string.Empty;
            }


            foreach (Enterprise.Controls.DatePicker control in controlsList.Where(t => t.GetType() == typeof(Enterprise.Controls.DatePicker)).ToList())
            {
                control.SelectedDate = null;
            }
            foreach (CheckableComboBox control in controlsList.Where(t => t.GetType() == typeof(CheckableComboBox)).ToList())
            {
                control.Reset();
            }

            foreach (CheckBox control in controlsList.Where(t => t.GetType() == typeof(CheckBox)).ToList())
            {
                control.IsChecked = null;
            }

            foreach (NumericTextBox control in controlsList.Where(t => t.GetType() == typeof(NumericTextBox)).ToList())
            {
                control.Text = string.Empty;
            }


            foreach (RadioButton control in controlsList.Where(t => t.GetType() == typeof(RadioButton)).ToList())
            {
                control.IsChecked = false;
            }


        }

        /// <summary>
        /// .چک میکند که آی پی مشخص ، پینگ میشود یا خیر
        /// </summary>
        /// <param name="nameOrAddress"></param>
        /// <returns></returns>
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                if (reply.Status == IPStatus.Success)
                {
                    pingable = true;
                }
            }
            catch (PingException pe)
            {
                Logger.WriteInfo("{0} has catght following error: ", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Write(pe);
                throw new PingException(".سرور شاهکار در دسترس نمیباشد");
            }
            return pingable;
        }

        /// <summary>
        /// .یک متن ساده را به معادل 64 بیتی به منظور افزایش سطح امنیت تبدیل میکند
        /// </summary>
        /// <param name="plainText">متن ساده قابل خواندن توسط شخص</param>
        /// <returns></returns>
        public static string ToBase64(string plainText)
        {
            string base64String = string.Empty;

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            base64String = Convert.ToBase64String(plainTextBytes);

            return base64String;
        }

        /// <summary>
        /// .یک متن 64 بیتی را به معادل متن ساده تبدیل میکند
        /// </summary>
        /// <param name="base64String">متن 64 بیتی - غیر قابل خواندن توسط شخص</param>
        /// <returns></returns>
        public static string FromBase64(string base64String)
        {
            string plainText = string.Empty;

            byte[] base64StringBytes = Convert.FromBase64String(base64String);

            plainText = System.Text.Encoding.UTF8.GetString(base64StringBytes);
            return plainText;

        }

        /// <summary>
        /// .بر اساس داکیومنت شاهکار ، مواردی که سازمان تنظیم به استان (برای دسترسی به متدهای شاهکار) تحویل داده است را به معادل 64 بیتی تبدیل میکند
        /// </summary>
        /// <param name="username">نام کاربری که سازمان تنظیم به استان داده است</param>
        /// <param name="password">کلمه عبور که سازمان تنظیم به استان داده است</param>
        /// <returns></returns>
        public static string GenerateBase64Authorization(string username, string password)
        {
            string base64Authorization = string.Empty;

            string concatedInformation = string.Format("{0}:{1}", username, password);

            base64Authorization = string.Format("Basic {0}", ToBase64(concatedInformation));

            return base64Authorization;
        }

    }
}
