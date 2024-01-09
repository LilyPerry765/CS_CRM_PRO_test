using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CRM.Application.Codes
{
    public class BlankReport : Stimulsoft.Report.StiReport
    {
        #region Properties And Fields

        public Stimulsoft.Report.Components.StiPage MainPage;

        public Stimulsoft.Report.Components.StiReportTitleBand MainReportTitleBand;
        public StiText beginText;
        public StiImage TelecomImage;
        public Stimulsoft.Report.Components.StiText TitleText;
        public string ReportTitleVariable;
        public StiText ReportDateText;
        public StiText ReportDateVariableText;
        public string ReportDateVariable;
        public StiText ReportTimeText;
        public StiText ReportTimeVariableText;
        public string ReportTimeVariable;

        public Stimulsoft.Report.Components.StiGroupHeaderBand MainGroupHeaderBand;
        public Stimulsoft.Report.Components.StiGroupFooterBand MainGroupFooterBand;
        public Stimulsoft.Report.Components.StiText CountText;
        public Stimulsoft.Report.Dictionary.StiCountFunctionService CountText_Count;

        public Stimulsoft.Report.Components.StiHeaderBand MainHeaderBand;
        public Stimulsoft.Report.Components.StiText LineHeaderText;
        public Stimulsoft.Report.Components.StiText PhoneNoHeaderText;
        public Stimulsoft.Report.Components.StiText SubscriberNameHeaderText;
        public StiText AddressHeaderText;

        public Stimulsoft.Report.Components.StiDataBand MainDataBand;
        public Stimulsoft.Report.Components.StiText LineText;
        public Stimulsoft.Report.Components.StiText PhoneNoText;
        public Stimulsoft.Report.Components.StiText SubscriberNameText;
        public StiText AddressText;

        public Stimulsoft.Report.Components.StiPageFooterBand WarningPageFooterBand;
        public StiText WarningText;

        public Stimulsoft.Report.Components.StiPageFooterBand PageFooterBand;
        public StiText PageFooterText;

        public Stimulsoft.Report.Components.StiWatermark MainPage_Watermark;
        public Stimulsoft.Report.Print.StiPrinterSettings RadReport_PrinterSettings;

        public resultDataSource result;

        #endregion

        #region Constructor

        public BlankReport()
        {
            this.InitializeComponent();
        }

        #endregion

        #region EventHandlers

        public void CountText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "#%#تعداد کل : {Count()}";
            e.StoreToPrinted = true;
        }

        public void ReportTimeVariableText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, ReportTimeVariable, true);
        }

        public void ReportTimeText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ":ساعت گزارش";
        }

        public void ReportDateVariableText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, ReportDateVariable, true);
        }

        public void ReportDateText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ":تاریخ گزارش";
        }

        public void beginText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "بسمه تعالی";
        }

        public void AddressText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, result.Address, true);
        }

        public void LineHeaderText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "ردیف";
        }

        public void PhoneNoHeaderText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "شماره پرسنلی";
        }

        public void SubscriberNameHeaderText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "کارمند";
        }

        public void AddressHeaderText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "آدرس";
        }

        public void LineText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Line, true);
        }

        public void PhoneNoText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, result.PhoneNo, true);
        }

        public void SubscriberNameText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, result.SubscriberName, true);
        }

        public void PageFooterText_GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "#%#صفحه {PageNumber} از {TotalPageCount}";
            e.StoreToPrinted = true;
        }

        public void BlankReport_EndRender(object sender, EventArgs e)
        {
            PageFooterText.SetText(new StiGetValue(this.PageFooterText_GetValue_End));
        }

        public System.String PageFooterText_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            return "صفحه " + ToString(sender, PageNumber, true) + " از " + ToString(sender, TotalPageCount, true);
        }

        public void TitleText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, ReportTitleVariable, true);
        }

        public void WarningText__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ".اطلاعات نمایش داده شده در این گزارش فرضی میباشند و هیچ گونه وجود خارجی ندارند";
        }

        #endregion

        #region Methods
        private void InitializeComponent()
        {
            this.result = new resultDataSource();

            //Create ReportTitleVariable
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "ReportTitleVariable", "ReportTitleVariable", "", typeof(string), "", false, false, false));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "ReportDateVariable", "ReportDateVariable", "", typeof(string), "", false, false, false));
            this.CountText_Count = new Stimulsoft.Report.Dictionary.StiCountFunctionService();
            //Current Report Settings
            this.EngineVersion = Stimulsoft.Report.Engine.StiEngineVersion.EngineV2;
            this.NeedsCompiling = false;
            this.ReferencedAssemblies = new System.String[] {
                    "System.Dll",
                    "System.Drawing.Dll",
                    "System.Windows.Forms.Dll",
                    "System.Data.Dll",
                    "System.Xml.Dll",
                    "Stimulsoft.Controls.Dll",
                    "Stimulsoft.Base.Dll",
                    "Stimulsoft.Report.Dll"};
            this.ReportGuid = "efaf810c454e4be4bea48b9ab0ccd02b";
            this.ReportName = "SampleReport";
            this.ReportUnit = Stimulsoft.Report.StiReportUnitType.Inches;
            this.ScriptLanguage = Stimulsoft.Report.StiReportLanguageType.CSharp;

            // 
            // MainPage
            this.MainPage = new Stimulsoft.Report.Components.StiPage();
            this.MainPage.Guid = "6ef13aa4a6394763a46663eedeca2ced";
            this.MainPage.Name = "MainPage";
            this.MainPage.PageHeight = 11.69;
            this.MainPage.PageWidth = 8.27;
            this.MainPage.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 2, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.MainPage.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);

            // 
            // MainReportTitleBand
            this.MainReportTitleBand = new Stimulsoft.Report.Components.StiReportTitleBand();
            this.MainReportTitleBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.2, 7.49, 1);
            this.MainReportTitleBand.Name = "MainReportTitleBand";
            this.MainReportTitleBand.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Bottom, System.Drawing.Color.Black, 2, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.MainReportTitleBand.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.MainReportTitleBand.Guid = null;
            this.MainReportTitleBand.Interaction = null;
            // 
            // TitleText
            this.TitleText = new Stimulsoft.Report.Components.StiText();
            this.TitleText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.60, 0.16, 2.44, 0.39);
            this.TitleText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.TitleText.Name = "TitleText";
            this.TitleText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.TitleText__GetValue);
            this.TitleText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.TitleText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.FromArgb(255, 173, 193, 217), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.TitleText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.TitleText.Font = new System.Drawing.Font("B Nazanin", 16F, System.Drawing.FontStyle.Bold);
            this.TitleText.Guid = null;
            this.TitleText.Interaction = null;
            this.TitleText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.TitleText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.TitleText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.MainReportTitleBand.Guid = null;
            this.MainReportTitleBand.Interaction = null;
            this.TitleText.Enabled = false;
            // 
            // BeginText
            // 
            beginText = new Stimulsoft.Report.Components.StiText();
            beginText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.07, 0, 1.5, 0.16);
            beginText.Guid = "afcb04fd42044ce0a032042894f18ed9";
            beginText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            beginText.Name = "BeginText";
            beginText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.beginText__GetValue);
            beginText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            beginText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            beginText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            beginText.Font = new System.Drawing.Font("B Nazanin", 11F, System.Drawing.FontStyle.Bold);
            beginText.Interaction = null;
            beginText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            beginText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            beginText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            //
            //TelecomImage
            this.TelecomImage = new Stimulsoft.Report.Components.StiImage();
            TelecomImage.Name = "TelecomImage";
            this.TelecomImage.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.54, 0.55, 0.55, 0.47);
            this.TelecomImage.Guid = "2e14061c8b1442d28918c973b70e269e";
            this.TelecomImage.Stretch = true;
            this.TelecomImage.Enabled = false;
            // 
            // ReportDateText
            // 
            this.ReportDateText = new Stimulsoft.Report.Components.StiText();
            this.ReportDateText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.18, 0.55, 0.79, 0.24);
            this.ReportDateText.Guid = "626982dc24aa4fb689f7ec4e85437eae";
            this.ReportDateText.Name = "ReportDateText";
            this.ReportDateText.GetValue += ReportDateText_GetValue;
            this.ReportDateText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.ReportDateText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.ReportDateText.Font = new System.Drawing.Font("B Nazanin", 11F);
            this.ReportDateText.Interaction = null;
            this.ReportDateText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.ReportDateText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.ReportDateText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.ReportDateText.Enabled = false;
            // 
            // ReportDateVariableText
            // 
            this.ReportDateVariableText = new Stimulsoft.Report.Components.StiText();
            this.ReportDateVariableText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.08, 0.55, 1.02, 0.24);
            this.ReportDateVariableText.Guid = "e7410c979c54444eb05f5136fa8f2e29";
            this.ReportDateVariableText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.ReportDateVariableText.Name = "ReportDateVariableText";
            this.ReportDateVariableText.GetValue += ReportDateVariableText_GetValue;
            this.ReportDateVariableText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.ReportDateVariableText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Transparent, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.ReportDateVariableText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.ReportDateVariableText.Font = new System.Drawing.Font("B Nazanin", 10F, System.Drawing.FontStyle.Bold);
            this.ReportDateVariableText.Interaction = null;
            this.ReportDateVariableText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.ReportDateVariableText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.ReportDateVariableText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.ReportDateVariableText.Enabled = false;
            //
            //ReportTimeText
            //
            this.ReportTimeText = new Stimulsoft.Report.Components.StiText();
            this.ReportTimeText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.1, 0.79, 0.79, 0.24);
            this.ReportTimeText.Guid = "f150cb4538ba4a21aa18aac026f83bc7";
            this.ReportTimeText.Name = "ReportTimeText";
            this.ReportTimeText.GetValue += ReportTimeText_GetValue;
            this.ReportTimeText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.ReportTimeText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.ReportTimeText.Font = new System.Drawing.Font("B Nazanin", 11F);
            this.ReportTimeText.Interaction = null;
            this.ReportTimeText.Margins = new Stimulsoft.Report.Components.StiMargins(2.5, 0, 0, 0);
            this.ReportTimeText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.ReportTimeText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.ReportTimeText.Enabled = false;
            // 
            // ReportTimeVariableText
            // 
            this.ReportTimeVariableText = new Stimulsoft.Report.Components.StiText();
            this.ReportTimeVariableText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.08, 0.79, 1.02, 0.24);
            this.ReportTimeVariableText.Guid = "5b8e6d23c0f84bf5b48e8ed850f22435";
            this.ReportTimeVariableText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.ReportTimeVariableText.Name = "ReportTimeVariableText";
            this.ReportTimeVariableText.GetValue += ReportTimeVariableText_GetValue;
            this.ReportTimeVariableText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.ReportTimeVariableText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Transparent, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.ReportTimeVariableText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.ReportTimeVariableText.Font = new System.Drawing.Font("B Nazanin", 10F, System.Drawing.FontStyle.Bold);
            this.ReportTimeVariableText.Interaction = null;
            this.ReportTimeVariableText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.ReportTimeVariableText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.ReportTimeVariableText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.MainReportTitleBand.Guid = null;
            this.MainReportTitleBand.Interaction = null;
            this.ReportTimeVariableText.Enabled = false;
            // 
            // MainGroupHeaderBand
            // 
            this.MainGroupHeaderBand = new Stimulsoft.Report.Components.StiGroupHeaderBand();
            this.MainGroupHeaderBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.68, 7.49, 0.3);
            this.MainGroupHeaderBand.Name = "MainGroupHeaderBand";
            this.MainGroupHeaderBand.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.MainGroupHeaderBand.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.MainGroupHeaderBand.Guid = null;
            this.MainGroupHeaderBand.Interaction = null;
            this.MainGroupHeaderBand.Enabled = true;
            // 
            // MainGroupFooterBand
            // 
            this.MainGroupFooterBand = new Stimulsoft.Report.Components.StiGroupFooterBand();
            this.MainGroupFooterBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 4.08, 7.49, 0.4);
            this.MainGroupFooterBand.Name = "MainGroupFooterBand";
            this.MainGroupFooterBand.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.MainGroupFooterBand.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.MainGroupFooterBand.Enabled = true;
            // 
            // CountText
            // 
            this.CountText = new Stimulsoft.Report.Components.StiText();
            this.CountText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6, 0.12, 1.5, 0.3);
            this.CountText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.CountText.Name = "CountText";
            this.CountText.GetValue += CountText_GetValue;
            this.CountText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.CountText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Transparent, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.CountText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 206, 221, 176));
            this.CountText.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            this.CountText.Enabled = true;
            // 
            // MainHeaderBand
            this.MainHeaderBand = new Stimulsoft.Report.Components.StiHeaderBand();
            this.MainHeaderBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.2, 7.49, 0.5);
            this.MainHeaderBand.Name = "MainHeaderBand";
            this.MainHeaderBand.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.MainHeaderBand.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // LineHeaderText
            this.LineHeaderText = new Stimulsoft.Report.Components.StiText();
            this.LineHeaderText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6.5, 0, 1, 0.4);
            this.LineHeaderText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.LineHeaderText.Name = "LineHeaderText";
            this.LineHeaderText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.LineHeaderText__GetValue);
            this.LineHeaderText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.LineHeaderText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.LineHeaderText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.LineHeaderText.Font = new System.Drawing.Font("B Nazanin", 12F);
            this.LineHeaderText.Guid = null;
            this.LineHeaderText.Interaction = null;
            this.LineHeaderText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.LineHeaderText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.LineHeaderText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // PhoneNoHeaderText
            this.PhoneNoHeaderText = new Stimulsoft.Report.Components.StiText();
            this.PhoneNoHeaderText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.5, 0, 1, 0.4);
            this.PhoneNoHeaderText.Guid = "0b55f9817dd94bb0af777f0522cecdf0";
            this.PhoneNoHeaderText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.PhoneNoHeaderText.Name = "PhoneNoHeaderText";
            this.PhoneNoHeaderText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.PhoneNoHeaderText__GetValue);
            this.PhoneNoHeaderText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.PhoneNoHeaderText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.PhoneNoHeaderText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.PhoneNoHeaderText.Font = new System.Drawing.Font("B Nazanin", 12F);
            this.PhoneNoHeaderText.Interaction = null;
            this.PhoneNoHeaderText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.PhoneNoHeaderText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.PhoneNoHeaderText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // SubscriberNameHeaderText
            this.SubscriberNameHeaderText = new Stimulsoft.Report.Components.StiText();
            this.SubscriberNameHeaderText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4.5, 0, 1, 0.4);
            this.SubscriberNameHeaderText.Guid = "15495e15ef12461590038032407556e9";
            this.SubscriberNameHeaderText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.SubscriberNameHeaderText.Name = "SubscriberNameHeaderText";
            this.SubscriberNameHeaderText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.SubscriberNameHeaderText__GetValue);
            this.SubscriberNameHeaderText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.SubscriberNameHeaderText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.SubscriberNameHeaderText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.SubscriberNameHeaderText.Font = new System.Drawing.Font("B Nazanin", 12F);
            this.SubscriberNameHeaderText.Interaction = null;
            this.SubscriberNameHeaderText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.SubscriberNameHeaderText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.SubscriberNameHeaderText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            //
            //AddressHeaderText
            this.AddressHeaderText = new StiText();
            this.AddressHeaderText.ClientRectangle = new RectangleD(0, 0, 4.5, 0.4);
            this.AddressHeaderText.Guid = "df71fbc8f0684cb69e3b2b632c9c69d1";
            this.AddressHeaderText.HorAlignment = StiTextHorAlignment.Center;
            this.AddressHeaderText.Name = "AddressHeaderText";
            this.AddressHeaderText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(AddressHeaderText_GetValue);
            this.AddressHeaderText.VertAlignment = StiVertAlignment.Center;
            this.AddressHeaderText.Border = new StiBorder(StiBorderSides.All, Color.Black, 1, StiPenStyle.Solid);
            this.AddressHeaderText.Brush = new StiSolidBrush(Color.Transparent);
            this.AddressHeaderText.Font = new System.Drawing.Font("B Nazanin", 12F);
            this.AddressHeaderText.Interaction = null;
            this.AddressHeaderText.Margins = new StiMargins(0, 0, 0, 0);
            this.AddressHeaderText.TextBrush = new StiSolidBrush(Color.Black);
            this.AddressHeaderText.TextOptions = new StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, StringTrimming.None);

            // 
            // WarningPageFooterBand
            this.WarningPageFooterBand = new Stimulsoft.Report.Components.StiPageFooterBand();
            this.WarningPageFooterBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 9.79, 7.49, 0.4);
            this.WarningPageFooterBand.Name = "WarningPageFooterBand";
            this.WarningPageFooterBand.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.WarningPageFooterBand.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.WarningPageFooterBand.Guid = null;
            this.WarningPageFooterBand.Interaction = null;
            //
            //WarningText
            this.WarningText = new Stimulsoft.Report.Components.StiText();
            this.WarningText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.WarningText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.01, 7.5, 0.4);
            this.WarningText.Name = "WarningText";
            this.WarningText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.WarningText__GetValue);
            this.WarningText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.WarningText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 173, 193, 217), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.WarningText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 255, 242, 0));
            this.WarningText.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Bold);
            this.WarningText.Guid = null;
            this.WarningText.Interaction = null;
            this.WarningText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.WarningText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.WarningText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);

            //
            //PageFooterBand
            this.PageFooterBand = new Stimulsoft.Report.Components.StiPageFooterBand();
            this.PageFooterBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 10.59, 7.49, 0.32);
            this.PageFooterBand.Guid = "d93d1a390db54d8e927a1d6b2d81ab53";
            this.PageFooterBand.Name = "PageFooterBand";
            this.PageFooterBand.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.PageFooterBand.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            PageFooterBand.Interaction = null;
            //
            //PageFooterText
            this.PageFooterText = new Stimulsoft.Report.Components.StiText();
            this.PageFooterText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.71, 0, 2.36, 0.24);
            this.PageFooterText.Guid = "1a52a259ed0249bfbb51c80490abfe3b";
            this.PageFooterText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.PageFooterText.Name = "PageFooterText";
            this.PageFooterText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(PageFooterText_GetValue);
            this.PageFooterText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.PageFooterText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.PageFooterText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(0, 0, 0, 0));
            this.PageFooterText.Font = new System.Drawing.Font("B Nazanin", 10F);
            this.PageFooterText.Interaction = null;
            this.PageFooterText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.PageFooterText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.PageFooterText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);

            // 
            // MainDataBand
            this.MainDataBand = new Stimulsoft.Report.Components.StiDataBand();
            this.MainDataBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.1, 7.49, 0.4);
            this.MainDataBand.DataSourceName = "result";
            this.MainDataBand.Name = "MainDataBand";
            this.MainDataBand.Sort = new System.String[0];
            this.MainDataBand.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.MainDataBand.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.MainDataBand.BusinessObjectGuid = null;
            // 
            // LineText
            this.LineText = new Stimulsoft.Report.Components.StiText();
            this.LineText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6.5, 0, 1, 0.4);
            this.LineText.Guid = "fdc6df14a24c44e5868fabf4cdde0d85";
            this.LineText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.LineText.Name = "LineText";
            this.LineText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.LineText__GetValue);
            this.LineText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.LineText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.LineText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.LineText.Font = new System.Drawing.Font("B Nazanin", 12F);
            this.LineText.Interaction = null;
            this.LineText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.LineText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.LineText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // PhoneNoText
            this.PhoneNoText = new Stimulsoft.Report.Components.StiText();
            this.PhoneNoText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.5, 0, 1, 0.4);
            this.PhoneNoText.Guid = "82874a94a0a24dbdb2b2657b48f783fa";
            this.PhoneNoText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.PhoneNoText.Name = "PhoneNoText";
            this.PhoneNoText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.PhoneNoText__GetValue);
            this.PhoneNoText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.PhoneNoText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.PhoneNoText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.PhoneNoText.Font = new System.Drawing.Font("B Nazanin", 12F);
            this.PhoneNoText.Interaction = null;
            this.PhoneNoText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.PhoneNoText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.PhoneNoText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // SubscriberNameText
            this.SubscriberNameText = new Stimulsoft.Report.Components.StiText();
            this.SubscriberNameText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4.5, 0, 1, 0.4);
            this.SubscriberNameText.Guid = "86970e9e996d48ecab647f3e47499a63";
            this.SubscriberNameText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.SubscriberNameText.Name = "SubscriberNameText";
            this.SubscriberNameText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.SubscriberNameText__GetValue);
            this.SubscriberNameText.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.SubscriberNameText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.SubscriberNameText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.SubscriberNameText.Font = new System.Drawing.Font("B Nazanin", 12F);
            this.SubscriberNameText.Interaction = null;
            this.SubscriberNameText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.SubscriberNameText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.SubscriberNameText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            //
            //AddressText
            this.AddressText = new StiText();
            AddressText.ClientRectangle = new RectangleD(0, 0, 4.5, 0.4);
            AddressText.Guid = "095b8a5b22b54bc7b1f22c78c5de0189";
            AddressText.HorAlignment = StiTextHorAlignment.Center;
            AddressText.Name = "AddressText";
            AddressText.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(AddressText_GetValue);
            AddressText.VertAlignment = StiVertAlignment.Center;
            AddressText.Border = new StiBorder(StiBorderSides.All, Color.Black, 1, StiPenStyle.Solid);
            AddressText.Brush = new StiSolidBrush(Color.Transparent);
            AddressText.Font = new Font("B Nazanin", 12F);
            AddressText.Interaction = null;
            AddressText.Margins = new StiMargins(0, 0, 0, 0);
            AddressText.TextBrush = new StiSolidBrush(Color.Black);
            AddressText.TextOptions = new StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, StringTrimming.None);

            //---------------------------------------------------------------------------------------------------------------------------------------------------
            this.AddressText.Page = MainPage;
            this.AddressText.Parent = MainDataBand;
            this.AddressHeaderText.Page = MainPage;
            this.AddressHeaderText.Parent = MainHeaderBand;
            this.MainHeaderBand.Guid = null;
            this.MainHeaderBand.Interaction = null;
            this.WarningPageFooterBand.Page = MainPage;
            this.WarningPageFooterBand.Parent = MainPage;
            this.WarningText.Page = this.MainPage;
            this.WarningText.Parent = this.WarningPageFooterBand;
            this.MainDataBand.DataRelationName = null;
            this.MainDataBand.Guid = null;
            this.MainDataBand.Interaction = null;
            this.MainDataBand.MasterComponent = null;
            this.MainPage.ExcelSheetValue = null;
            this.MainPage.Interaction = null;
            this.MainPage.Margins = new Stimulsoft.Report.Components.StiMargins(0.39, 0.39, 0.39, 0.39);
            this.MainPage_Watermark = new Stimulsoft.Report.Components.StiWatermark();
            this.MainPage_Watermark.Font = new System.Drawing.Font("Arial", 100F);
            this.MainPage_Watermark.Image = null;
            this.MainPage_Watermark.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0));
            this.RadReport_PrinterSettings = new Stimulsoft.Report.Print.StiPrinterSettings();
            this.PrinterSettings = this.RadReport_PrinterSettings;
            this.MainPage.Report = this;
            this.MainPage.Watermark = this.MainPage_Watermark;
            this.MainHeaderBand.Page = this.MainPage;
            this.MainHeaderBand.Parent = this.MainPage;
            this.MainReportTitleBand.Page = this.MainPage;
            this.MainReportTitleBand.Parent = this.MainPage;
            this.TitleText.Page = this.MainPage;
            this.TitleText.Parent = this.MainReportTitleBand;
            this.beginText.Parent = MainReportTitleBand;
            this.beginText.Page = MainPage;
            this.LineHeaderText.Page = this.MainPage;
            this.LineHeaderText.Parent = this.MainHeaderBand;
            this.PhoneNoHeaderText.Page = this.MainPage;
            this.PhoneNoHeaderText.Parent = this.MainHeaderBand;
            this.SubscriberNameHeaderText.Page = this.MainPage;
            this.SubscriberNameHeaderText.Parent = this.MainHeaderBand;
            this.MainDataBand.Page = this.MainPage;
            this.MainDataBand.Parent = this.MainPage;
            this.LineText.Page = this.MainPage;
            this.LineText.Parent = this.MainDataBand;
            this.PhoneNoText.Page = this.MainPage;
            this.PhoneNoText.Parent = this.MainDataBand;
            this.SubscriberNameText.Page = this.MainPage;
            this.SubscriberNameText.Parent = this.MainDataBand;
            this.CountText.Page = this.MainPage;
            this.CountText.Parent = this.MainGroupFooterBand;
            this.MainGroupFooterBand.Page = MainPage;
            this.MainGroupFooterBand.Parent = MainPage;
            this.MainGroupHeaderBand.Page = MainPage;
            this.MainGroupHeaderBand.Parent = MainPage;
            this.PageFooterBand.Page = MainPage;
            this.PageFooterBand.Parent = MainPage;
            this.PageFooterText.Page = MainPage;
            this.PageFooterText.Parent = PageFooterBand;
            this.MainGroupHeaderBand.BeginRender += MainGroupHeaderBand_BeginRender;
            this.MainGroupHeaderBand.EndRender += MainGroupHeaderBand_EndRender;
            this.MainGroupHeaderBand.Rendering += MainGroupHeaderBand_Rendering;
            this.EndRender += new EventHandler(BlankReport_EndRender);
            this.AggregateFunctions = new object[] {
                    this.CountText_Count};
            // 
            // Add to PageFooterBand.Components
            // 
            this.PageFooterBand.Components.Clear();
            this.PageFooterBand.Components.Add(PageFooterText);
            // 
            // Add to MainReportTitleBand.Components
            // 
            this.MainReportTitleBand.Components.Clear();
            this.MainReportTitleBand.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.TitleText,
                        this.beginText,
                        this.TitleText,
                        this.TelecomImage,
                        this.ReportDateText,
                        this.ReportDateVariableText,
                        this.ReportTimeText,
                        this.ReportTimeVariableText});
            // 
            // 
            // Add to MainHeaderBand.Components
            // 
            this.MainHeaderBand.Components.Clear();
            this.MainHeaderBand.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.LineHeaderText,
                        this.PhoneNoHeaderText,
                        this.SubscriberNameHeaderText,
                        this.AddressHeaderText});
            // 
            // Add to MainDataBand.Components
            // 
            this.MainDataBand.Components.Clear();
            this.MainDataBand.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.LineText,
                        this.PhoneNoText,
                        this.SubscriberNameText,
                        this.AddressText});
            //
            //Add to MainGroupFooterBand.Components
            //
            this.MainGroupFooterBand.Components.Clear();
            this.MainGroupFooterBand.Components.AddRange(new StiComponent[] { this.CountText });
            // 
            // Add to WarningPageFooterBand.Components
            // 
            this.WarningPageFooterBand.Components.Clear();
            this.WarningPageFooterBand.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.WarningText});
            // 
            // Add to MainPage.Components
            // 
            this.MainPage.Components.Clear();
            this.MainPage.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        MainReportTitleBand,
                        this.MainGroupHeaderBand,
                        this.MainGroupFooterBand,
                        this.MainHeaderBand,
                        this.MainDataBand,
                        this.WarningPageFooterBand});
            // 
            // Add to Pages
            // 
            this.Pages.Clear();
            this.Pages.AddRange(new Stimulsoft.Report.Components.StiPage[] {
                        this.MainPage});
            this.result.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("SubscriberName", "SubscriberName", "SubscriberName", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("PhoneNo", "PhoneNo", "PhoneNo", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Address","Address","Address",typeof(string))});
            this.DataSources.Add(this.result);
        }

        public void MainGroupHeaderBand_Rendering(object sender, EventArgs e)
        {
            this.CountText_Count.CalcItem(null);
        }

        public void MainGroupHeaderBand_EndRender(object sender, EventArgs e)
        {
            this.CountText.SetText(new Stimulsoft.Report.Components.StiGetValue(this.CountText_GetValue_End));
        }

        public System.String CountText_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            return "تعداد کل : " + ToString(sender, ((long)(StiReport.ChangeType(this.CountText_Count.GetValue(), typeof(long), true))), true);
        }

        public void MainGroupHeaderBand_BeginRender(object sender, EventArgs e)
        {
            this.CountText_Count.Init();
            this.CountText.TextValue = "";
        }

        #endregion

        #region DataSource result

        public class resultDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {

            public resultDataSource() :
                base("result", "result")
            {
            }

            public virtual string SubscriberName
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["SubscriberName"], typeof(string), true)));
                }
            }

            public virtual string PhoneNo
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["PhoneNo"], typeof(string), true)));
                }
            }

            public virtual string Address
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Address"], typeof(string), true)));
                }
            }
        }

        #endregion DataSource result
    }
}
