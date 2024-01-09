using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media;

namespace CRM.Data
{
    public static class Helpers
    {
        #region Methods

        /// <summary>
        /// .با استفاده از نام کوتاه یک اسمبلی ، در صورتی که لود شده باشد آن را برمیگرداند
        /// </summary>
        /// <param name="assemblyshortName">نام کوتاه اسمبلی مورد نظر</param>
        /// <returns></returns>
        public static Assembly GetAssmeblyByShortName(string assemblyshortName)
        {
            Assembly result = default(Assembly);
            bool isExists = false;

            isExists = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.GetName().Name == assemblyshortName).Any();

            if (isExists)
            {
                result = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.GetName().Name == assemblyshortName).FirstOrDefault();
            }
            else
            {
                result = null;
            }

            return result;
        }

        //TODO:rad
        public static Type GetLogEntityTypeByEnumFieldName(string enumFieldName, string returnTypeNamespace, string assemblyName)
        {
            Type result = default(Type);
            Assembly assembly = GetAssmeblyByShortName(assemblyName);

            if (assembly != null)
            {
                var query = from t in assembly.GetTypes()
                            where t.Name == enumFieldName
                                  &&
                                  t.Namespace == returnTypeNamespace
                                  &&
                                  t.IsClass
                            select t;
                result = query.FirstOrDefault();
            }
            else
            {
                result = null;
            }
            return result;
        }

        //TODO:rad
        /// <summary>
        /// .با استفاده از یکی از مقادیر یک اینام خاص ،مقدار توضیحات آن مقدار از اینام را برمیگرداند
        /// </summary>
        /// <param name="value">یکی از مقادیر موجود در اینام</param>
        /// <param name="enumType">اینام خاص</param>
        /// <returns></returns>
        public static string GetEnumDescription(int? value, Type enumType)
        {
            string result = string.Empty;
            if (value == null) return result;

            FieldInfo fieldInfo = enumType.GetFields().Where(f => f.IsStatic && Convert.ToInt32(f.GetValue(null)) == value).FirstOrDefault();
            if (fieldInfo != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    result = attributes[0].Description;
                }
            }
            return result;
        }

        //TODO:rad
        /// <summary>
        /// . فایل مربوط به تنظیمات گزارش را با توجه به مسیر مشخص ، تحلیل و بررسی کرده ، سپس مشخصات مربوط به تنظیمات را برمیگرداند
        /// </summary>
        /// <param name="reportSettingFilePath">مسیر فایل تنظیمات</param>
        /// <returns></returns>
        public static ReportSetting GetReportDefaultSettings(string reportSettingFilePath)
        {
            ReportSetting specificSetting = new ReportSetting();
            try
            {
                string defaultReportSetting = string.Format(@"{0}\{1}{2}.xml", reportSettingFilePath, "ReportSetting", DB.CurrentUser.ID);
                specificSetting.SetMembersDefault();
                if (File.Exists(defaultReportSetting))
                {
                    System.Xml.Linq.XElement rootElement = default(System.Xml.Linq.XElement);
                    //کد زیر خطا میدهد چون برای دی سریالایز کردن باید کلاس مربوطه دارای یک سازنده بدون پارامتر باشد اما کلاس فونت این امکان را ندارد
                    //using (TextReader textReader = new StreamReader(defaultReportSetting))
                    //{
                    //    element = XElement.Load(textReader);
                    //    specificSetting = Deserialize<ReportSetting>(element.ToString());
                    //}
                    System.Xml.Linq.XDocument xdoc = new System.Xml.Linq.XDocument();
                    using (FileStream stream = new FileStream(defaultReportSetting, FileMode.Open))
                    {
                        xdoc = System.Xml.Linq.XDocument.Load(stream);
                    }

                    //بدست آورن روت فایل اکس ام ال
                    rootElement = xdoc.Element("ReportSetting");

                    bool headerFontIsBold = Convert.ToBoolean(rootElement.Element("HeaderFontIsBold").Value);
                    bool headerFontIsItalic = Convert.ToBoolean(rootElement.Element("HeaderFontIsItalic").Value);
                    bool headerFontIsUnderlined = Convert.ToBoolean(rootElement.Element("HeaderFontIsUnderlined").Value);
                    bool textFontIsBold = Convert.ToBoolean(rootElement.Element("TextFontIsBold").Value);
                    bool textFontIsItalic = Convert.ToBoolean(rootElement.Element("TextFontIsItalic").Value);
                    bool textFontIsUnderlined = Convert.ToBoolean(rootElement.Element("TextFontIsUnderlined").Value);

                    System.Drawing.FontStyle headerFontStyle = ((headerFontIsBold) ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular) |
                                                               ((headerFontIsItalic) ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular) |
                                                               ((headerFontIsUnderlined) ? System.Drawing.FontStyle.Underline : System.Drawing.FontStyle.Regular);

                    System.Drawing.FontStyle textFontStyle = ((textFontIsBold) ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular) |
                                                             ((textFontIsItalic) ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular) |
                                                             ((textFontIsUnderlined) ? System.Drawing.FontStyle.Underline : System.Drawing.FontStyle.Regular);

                    specificSetting.HeaderBackground = new BrushConverter().ConvertFrom(rootElement.Element("HeaderBackground").Value) as SolidColorBrush;
                    specificSetting.HeaderBorderBrush = new BrushConverter().ConvertFrom(rootElement.Element("HeaderBorderBrush").Value) as SolidColorBrush;
                    specificSetting.HeaderBorderThickness = Convert.ToDouble(rootElement.Element("HeaderBorderThickness").Value);
                    specificSetting.HeaderFont = new System.Drawing.Font(rootElement.Element("HeaderFontFamilyName").Value, Convert.ToSingle(rootElement.Element("HeaderFontSize").Value), headerFontStyle);
                    specificSetting.HeaderFontIsBold = Convert.ToBoolean(rootElement.Element("HeaderFontIsBold").Value);
                    specificSetting.HeaderFontIsItalic = Convert.ToBoolean(rootElement.Element("HeaderFontIsItalic").Value);
                    specificSetting.HeaderFontIsUnderlined = Convert.ToBoolean(rootElement.Element("HeaderFontIsUnderlined").Value);
                    specificSetting.HeaderForeground = new BrushConverter().ConvertFrom(rootElement.Element("HeaderForeground").Value) as SolidColorBrush;
                    specificSetting.HeaderHasWordWrap = Convert.ToBoolean(rootElement.Element("HeaderHasWordWrap").Value);
                    specificSetting.PrintWithPreview = Convert.ToBoolean(rootElement.Element("PrintWithPreview").Value);
                    specificSetting.ReportHasPageFooter = Convert.ToBoolean(rootElement.Element("ReportHasPageFooter").Value);
                    specificSetting.ReportHasTitle = Convert.ToBoolean(rootElement.Element("ReportHasTitle").Value);
                    specificSetting.StiPageOrientation = Convert.ToInt16(rootElement.Element("StiPageOrientation").Value);
                    specificSetting.TextBackground = new BrushConverter().ConvertFrom(rootElement.Element("TextBackground").Value) as SolidColorBrush;
                    specificSetting.TextBorderBrush = new BrushConverter().ConvertFrom(rootElement.Element("TextBorderBrush").Value) as SolidColorBrush;
                    specificSetting.TextBorderThickness = Convert.ToDouble(rootElement.Element("TextBorderThickness").Value);
                    specificSetting.TextFont = new System.Drawing.Font(rootElement.Element("TextFontFamilyName").Value, Convert.ToSingle(rootElement.Element("TextFontSize").Value), textFontStyle);
                    specificSetting.TextFontIsBold = Convert.ToBoolean(rootElement.Element("TextFontIsBold").Value);
                    specificSetting.TextFontIsItalic = Convert.ToBoolean(rootElement.Element("TextFontIsItalic").Value);
                    specificSetting.TextFontIsUnderlined = Convert.ToBoolean(rootElement.Element("TextFontIsUnderlined").Value);
                    specificSetting.TextForeground = new BrushConverter().ConvertFrom(rootElement.Element("TextForeground").Value) as SolidColorBrush;
                    specificSetting.TextHasWordWrap = Convert.ToBoolean(rootElement.Element("TextHasWordWrap").Value);
                    specificSetting.ReportHasDate = Convert.ToBoolean(rootElement.Element("ReportHasDate").Value);
                    specificSetting.ReportHasLogo = Convert.ToBoolean(rootElement.Element("ReportHasLogo").Value);
                    specificSetting.ReportHasTime = Convert.ToBoolean(rootElement.Element("ReportHasTime").Value);
                    specificSetting.ReportSumRecordsQuantity = Convert.ToBoolean(rootElement.Element("ReportSumRecordsQuantity").Value);
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "Error in ReportSetting loading.......");
                System.Windows.Forms.MessageBox.Show("خطا در بازیابی تنظیمات", "خطا", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return specificSetting;
        }

        //TODO:rad
        /// <summary>
        /// .یک رشته که از اعداد همراه با کاما تشکیل شده است را گرفته و آن را به لیستی از اعداد بدون کاما تبدیل میکند
        /// </summary>
        /// <param name="str">پارامتر ورودی</param>
        /// <returns></returns>
        public static IEnumerable<int> StringToIntList(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                yield break;
            }
            foreach (var s in str.Split(','))
            {
                int num;
                if (int.TryParse(s, out num))
                {
                    yield return num;
                }
            }

        }

        #endregion
    }
}
