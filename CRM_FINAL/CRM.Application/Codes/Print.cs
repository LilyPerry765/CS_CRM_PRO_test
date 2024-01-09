using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Viewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace CRM.Application.Codes
{
    public static class Print
    {
        //[System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        //public static extern int AddFontResource([System.Runtime.InteropServices.In][System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]string lpFileName);
        public static void DynamicPrint(DataSet dataSet)
        {
            #region DefaultSettings

            ReportSetting currentUserReportSetting = DB.CurrentUser.ReportSetting;
            StiBrush headerBackground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                        currentUserReportSetting.HeaderBackground.Color.A,
                                                                                        currentUserReportSetting.HeaderBackground.Color.R,
                                                                                        currentUserReportSetting.HeaderBackground.Color.G,
                                                                                        currentUserReportSetting.HeaderBackground.Color.B
                                                                                       )
                                                          );
            StiBrush headerForeground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                         currentUserReportSetting.HeaderForeground.Color.A,
                                                                                         currentUserReportSetting.HeaderForeground.Color.R,
                                                                                         currentUserReportSetting.HeaderForeground.Color.G,
                                                                                         currentUserReportSetting.HeaderForeground.Color.B
                                                                                       )
                                                          );
            StiBrush textBackground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                        currentUserReportSetting.TextBackground.Color.A,
                                                                                        currentUserReportSetting.TextBackground.Color.R,
                                                                                        currentUserReportSetting.TextBackground.Color.G,
                                                                                        currentUserReportSetting.TextBackground.Color.B
                                                                                     )
                                                        );
            StiBrush textForeground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                      currentUserReportSetting.TextForeground.Color.A,
                                                                                      currentUserReportSetting.TextForeground.Color.R,
                                                                                      currentUserReportSetting.TextForeground.Color.G,
                                                                                      currentUserReportSetting.TextForeground.Color.B
                                                                                     )
                                                        );
            StiBorder headerBorder = new StiBorder(
                                                    StiBorderSides.All,
                                                    System.Drawing.Color.FromArgb
                                                                                 (
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.A,
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.R,
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.G,
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.B
                                                                                  ),
                                                    currentUserReportSetting.HeaderBorderThickness,
                                                    StiPenStyle.Solid
                                                   );
            StiBorder textBorder = new StiBorder(
                                                    StiBorderSides.All,
                                                    System.Drawing.Color.FromArgb
                                                                                 (
                                                                                  currentUserReportSetting.TextBorderBrush.Color.A,
                                                                                  currentUserReportSetting.TextBorderBrush.Color.R,
                                                                                  currentUserReportSetting.TextBorderBrush.Color.G,
                                                                                  currentUserReportSetting.TextBorderBrush.Color.B
                                                                                  ),
                                                    currentUserReportSetting.TextBorderThickness,
                                                    StiPenStyle.Solid
                                                   );

            #endregion

            DateTime currentDateTime = DB.GetServerDate();

            //MainReportS
            StiReport report = new StiReport();
            report.ReportUnit = StiReportUnitType.Inches;

            //MainPage
            StiPage mainPage = report.Pages[0];
            mainPage.Orientation = (StiPageOrientation)currentUserReportSetting.StiPageOrientation;
            mainPage.RightToLeft = true;


            #region MainReportTitleBand

            StiReportTitleBand mainReportTitleBand = new StiReportTitleBand();
            mainReportTitleBand.Name = "MainReportTitleBand";
            mainReportTitleBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 7.49, 1.9);
            mainReportTitleBand.Brush = new StiSolidBrush(System.Drawing.Color.LightSeaGreen);
            mainReportTitleBand.Border = new StiBorder(StiBorderSides.Bottom, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
            mainPage.Components.Add(mainReportTitleBand);

            StiText reportDateText = new StiText();
            reportDateText = new Stimulsoft.Report.Components.StiText();
            reportDateText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.29, 0.76, 2.03, 0.76);
            reportDateText.Name = "ReportDateText";
            reportDateText.Text = new StiExpression(":تاریخ");
            reportDateText.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.FromArgb(255, 173, 193, 217), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            reportDateText.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            reportDateText.Font = new System.Drawing.Font("Nazanin", 12F);
            reportDateText.Guid = null;
            reportDateText.Interaction = null;
            reportDateText.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            reportDateText.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            reportDateText.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);

            mainReportTitleBand.Components.Add(reportDateText);

            #endregion

            #region Header

            StiPageHeaderBand pageHeaderBand = new StiPageHeaderBand();
            pageHeaderBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 3.74, 19, 1.27);
            pageHeaderBand.Name = "pageHeaderBand";
            pageHeaderBand.Enabled = true;
            mainPage.Components.Add(pageHeaderBand);

            #endregion Header

            #region PageFooter

            StiPageFooterBand FooterBand = new StiPageFooterBand();
            FooterBand.Height = 0.5;
            FooterBand.Name = "FooterBandBand";
            FooterBand.Enabled = true;
            mainPage.Components.Add(FooterBand);

            #endregion

            #region Data

            //Create HeaderBand
            StiHeaderBand headerBand = new StiHeaderBand();
            headerBand.Height = 0.5;
            headerBand.CanGrow = true;
            headerBand.CanShrink = true;
            headerBand.Name = "HeaderBand";
            mainPage.Components.Add(headerBand);

            //Create Databand
            StiDataBand dataBand = new StiDataBand();
            dataBand.DataSourceName = "Result";
            dataBand.Height = 0.5;
            dataBand.CanGrow = true;
            dataBand.GrowToHeight = true;
            dataBand.Name = "DataBand";
            mainPage.Components.Add(dataBand);

            #endregion data

            #region GroupHeader

            StiGroupHeaderBand groupHeaderBand = new StiGroupHeaderBand();
            groupHeaderBand.Name = "groupHeaderBand";
            groupHeaderBand.Enabled = true;
            mainPage.Components.Insert(mainPage.Components.IndexOf(report.GetComponentByName("HeaderBand")) - 1, groupHeaderBand);

            StiGroupFooterBand groupFooterBand = new StiGroupFooterBand();
            groupFooterBand.Height = 0.5;
            groupFooterBand.Name = "groupFooterBand";
            groupFooterBand.Enabled = true;
            mainPage.Components.Insert(mainPage.Components.IndexOf(report.GetComponentByName("DataBand")) + 1, groupFooterBand);

            #endregion GroupHeader

            #region ReportSummery

            StiReportSummaryBand reportSummaryBand = new StiReportSummaryBand();
            reportSummaryBand.Height = 0.5;
            reportSummaryBand.Name = "reportSummaryBand";
            reportSummaryBand.Enabled = true;
            mainPage.Components.Insert(mainPage.Components.IndexOf(report.GetComponentByName("groupFooterBand")) + 1, reportSummaryBand);


            StiText pageCountText = new StiText(new RectangleD(0, 0, mainPage.Width, 0.5));
            pageCountText.Height = 0.5;
            pageCountText.Text = "جمع کل : " + "{Count()}";
            pageCountText.HorAlignment = StiTextHorAlignment.Right;
            pageCountText.VertAlignment = StiVertAlignment.Center;
            pageCountText.Name = "CountText";
            reportSummaryBand.Components.Add(pageCountText);

            #endregion



            if (dataSet.Tables[0].Columns.Count > 0)
            {
                double pos = 0;
                double columnWidth = (mainPage.Width - 1) / dataSet.Tables[0].Columns.Count;
                int nameIndex = 1;
                StiDataTableSource userSource = new StiDataTableSource("Result", "Result", "Result");


                StiText hText = new StiText();
                StiText dataText = new StiText();
                foreach (DataColumn dataColumn in dataSet.Tables[0].Columns)
                {
                    userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn(dataColumn.ColumnName, dataColumn.GetType()));

                    //Create text on header
                    hText = new StiText(new RectangleD(pos, 0, columnWidth, 0.5));
                    hText.HorAlignment = StiTextHorAlignment.Center;
                    hText.VertAlignment = StiVertAlignment.Center;
                    hText.Font = new System.Drawing.Font("Tahoma", 10);
                    hText.Brush = new StiSolidBrush(System.Drawing.Color.LightBlue);
                    hText.Interaction.SortingEnabled = true;
                    hText.Interaction.SortingColumn = "DataBand." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dataColumn.ColumnName);
                    hText.Text.Value = dataColumn.Caption;
                    hText.Name = "HeaderText" + nameIndex.ToString();
                    hText.WordWrap = true;
                    hText.CanGrow = true;
                    hText.GrowToHeight = true;
                    hText.Border.Side = StiBorderSides.All;
                    headerBand.Components.Add(hText);

                    //StiText 
                    dataText = new StiText(new RectangleD(pos, 0, columnWidth, 0.5));
                    dataText.HorAlignment = StiTextHorAlignment.Center;
                    dataText.VertAlignment = StiVertAlignment.Center;
                    dataText.Font = new System.Drawing.Font("Tahoma", 10);
                    dataText.Text = "{Result." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dataColumn.ColumnName) + "}";
                    dataText.Name = "DataText" + nameIndex.ToString();
                    dataText.WordWrap = true;
                    dataText.CanGrow = true;
                    dataText.GrowToHeight = true;
                    dataText.Border.Side = StiBorderSides.All;
                    dataText.Interaction.SortingEnabled = true;
                    dataBand.Components.Add(dataText);
                    pos = pos + columnWidth;
                    nameIndex++;
                }

                #region line
                //Create text on header
                hText = new StiText(new RectangleD(pos, 0, 1, 0.5));
                hText.Text.Value = "ردیف";
                hText.HorAlignment = StiTextHorAlignment.Center;
                hText.VertAlignment = StiVertAlignment.Center;
                hText.Font = new System.Drawing.Font("Tahoma", 10);
                hText.Brush = new StiSolidBrush(System.Drawing.Color.LightBlue);
                hText.Name = "LineText" + nameIndex.ToString();
                hText.WordWrap = true;
                hText.CanGrow = true;
                hText.GrowToHeight = true;
                hText.Border.Side = StiBorderSides.All;
                headerBand.Components.Add(hText);

                dataText = new StiText(new RectangleD(pos, 0, 1, 0.5));
                dataText.HorAlignment = StiTextHorAlignment.Center;
                dataText.VertAlignment = StiVertAlignment.Center;
                dataText.Font = new System.Drawing.Font("Tahoma", 10);
                dataText.Text = "{Line}";
                dataText.WordWrap = true;
                dataText.CanGrow = true;
                dataText.GrowToHeight = true;
                dataText.Border.Side = StiBorderSides.All;
                dataText.Interaction.SortingEnabled = true;
                dataBand.Components.Add(dataText);

                #endregion line


                report.Dictionary.DataSources.Add(userSource);

            }

            report.CacheAllData = true;
            report.RegData(dataSet.Tables[0]);
            report.Dictionary.Synchronize();

            //تنظیمات هدر
            foreach (StiText headerBandChild in headerBand.Components.OfType<StiText>().Where(st => st.Interaction != null))
            {
                headerBandChild.Brush = headerBackground;
                headerBandChild.Border = headerBorder;
                headerBandChild.Font = currentUserReportSetting.HeaderFont;
                headerBandChild.TextBrush = headerForeground;
                if (currentUserReportSetting.HeaderHasWordWrap)
                {
                    headerBandChild.WordWrap = true;
                    headerBandChild.CanGrow = true;
                    headerBandChild.GrowToHeight = true;
                }
            }

            //تنظیمات متن
            foreach (StiText dataBandChild in dataBand.Components.OfType<StiText>())
            {
                dataBandChild.Brush = textBackground;
                dataBandChild.Border = textBorder;
                dataBandChild.Font = currentUserReportSetting.TextFont;
                dataBandChild.TextBrush = textForeground;
                if (currentUserReportSetting.TextHasWordWrap)
                {
                    dataBandChild.WordWrap = true;
                    dataBandChild.CanGrow = true;
                    dataBandChild.GrowToHeight = true;
                }
            }

            report.CompileStandaloneReport("", StiStandaloneReportType.Show);

            report.Render();
            if (currentUserReportSetting.PrintWithPreview)
            {
                report.Show(true);
            }
            else
            {
                report.Print(true);
            }

            //ReportViewerForm reportViewerForm = new ReportViewerForm(report);
            //reportViewerForm.ShowDialog();
        }

        //TODO:rad
        public static void DynamicPrintV2(DataSet dataSet, string reportTitle = "", List<DataGridSelectedIndex> dataGridSelectedIndexs = null, List<DataGridSelectedIndex> groupingColumn = null, List<DataGridSelectedIndex> _sumColumn = null)
        {
            #region DefaultSettings

            ReportSetting currentUserReportSetting = DB.CurrentUser.ReportSetting;
            currentUserReportSetting.TextFont = new Font("B Nazanin", 11F, System.Drawing.FontStyle.Regular);
            currentUserReportSetting.HeaderFont = new Font("B Nazanin", 11F, System.Drawing.FontStyle.Regular);
            StiBrush headerBackground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                        currentUserReportSetting.HeaderBackground.Color.A,
                                                                                        currentUserReportSetting.HeaderBackground.Color.R,
                                                                                        currentUserReportSetting.HeaderBackground.Color.G,
                                                                                        currentUserReportSetting.HeaderBackground.Color.B
                                                                                       )
                                                          );
            StiBrush headerForeground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                         currentUserReportSetting.HeaderForeground.Color.A,
                                                                                         currentUserReportSetting.HeaderForeground.Color.R,
                                                                                         currentUserReportSetting.HeaderForeground.Color.G,
                                                                                         currentUserReportSetting.HeaderForeground.Color.B
                                                                                       )
                                                          );
            StiBrush textBackground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                        currentUserReportSetting.TextBackground.Color.A,
                                                                                        currentUserReportSetting.TextBackground.Color.R,
                                                                                        currentUserReportSetting.TextBackground.Color.G,
                                                                                        currentUserReportSetting.TextBackground.Color.B
                                                                                     )
                                                        );
            StiBrush textForeground = new StiSolidBrush(System.Drawing.Color.FromArgb(
                                                                                      currentUserReportSetting.TextForeground.Color.A,
                                                                                      currentUserReportSetting.TextForeground.Color.R,
                                                                                      currentUserReportSetting.TextForeground.Color.G,
                                                                                      currentUserReportSetting.TextForeground.Color.B
                                                                                     )
                                                        );
            StiBorder headerBorder = new StiBorder(
                                                    StiBorderSides.All,
                                                    System.Drawing.Color.FromArgb
                                                                                 (
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.A,
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.R,
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.G,
                                                                                  currentUserReportSetting.HeaderBorderBrush.Color.B
                                                                                  ),
                                                    currentUserReportSetting.HeaderBorderThickness,
                                                    StiPenStyle.Solid
                                                   );
            StiBorder textBorder = new StiBorder(
                                                    StiBorderSides.All,
                                                    System.Drawing.Color.FromArgb
                                                                                 (
                                                                                  currentUserReportSetting.TextBorderBrush.Color.A,
                                                                                  currentUserReportSetting.TextBorderBrush.Color.R,
                                                                                  currentUserReportSetting.TextBorderBrush.Color.G,
                                                                                  currentUserReportSetting.TextBorderBrush.Color.B
                                                                                  ),
                                                    currentUserReportSetting.TextBorderThickness,
                                                    StiPenStyle.Solid
                                                   );

            #endregion

            DateTime currentDate = DB.GetServerDate();
            using (StiReport report = new StiReport())
            {
                #region CreateDataSource
                StiDataTableSource userSource = new StiDataTableSource("Result", "Result", "Result");
                foreach (DataColumn dataColumn in dataSet.Tables[0].Columns)
                {
                    userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn(dataColumn.ColumnName, dataColumn.GetType()));
                }
                #endregion CreateDataSource
                //report
                report.EngineVersion = Stimulsoft.Report.Engine.StiEngineVersion.EngineV2;
                report.ReportUnit = StiReportUnitType.Inches;


                //MainPage
                StiPage mainPage = report.Pages[0];
                mainPage.Name = "MainPage";
                mainPage.Orientation = (StiPageOrientation)currentUserReportSetting.StiPageOrientation;
                mainPage.PageHeight = 11.69;
                mainPage.PageWidth = 8.27;

                //MainReportTitleBand
                StiReportTitleBand mainReportTitleBand = new StiReportTitleBand();
                mainReportTitleBand.Name = "MainReportTitleBand";
                mainReportTitleBand.ClientRectangle = new RectangleD(0, 0.2, 7.49, 1.18);
                mainReportTitleBand.Border = new StiBorder(StiBorderSides.Bottom, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
                mainReportTitleBand.Page = mainPage;
                mainReportTitleBand.Parent = mainPage;

                //BeginText
                StiText beginText = new StiText();
                beginText.Name = "BeginText";
                beginText.ClientRectangle = new RectangleD((mainPage.Width / 2) - 0.75, 0, 1.5, 0.16);
                beginText.HorAlignment = StiTextHorAlignment.Center;
                beginText.VertAlignment = StiVertAlignment.Center;
                beginText.Font = new Font("B Nazanin", 11F, System.Drawing.FontStyle.Bold);
                beginText.Page = mainPage;
                beginText.Text.Value = "بسمه تعالی";
                beginText.Parent = mainReportTitleBand;
                mainReportTitleBand.Components.Add(beginText);

                //Variables
                report.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "ReportDateVariable", "ReportDateVariable", "", typeof(string), "", false, false, false));
                report.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "ReportTimeVariable", "ReportTimeVariable", "", typeof(string), "", false, false, false));

                #region title
                //آیا گزارش دارای عنوان میباشد یا خیر
                if (currentUserReportSetting.ReportHasTitle)
                {
                    report.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "ReportTitleVariable", "ReportTitleVariable", "", typeof(string), "", false, false, false));
                    if (!string.IsNullOrEmpty(reportTitle))
                    report.Dictionary.Variables["ReportTitleVariable"].Value = string.Format("عنوان انتخابی کاربر: {0}", reportTitle);
                }
                //ReportTitleText
                StiText reportTitleText = new StiText();
                if (currentUserReportSetting.ReportHasTitle)
                {
                    reportTitleText.TextOptions = new StiTextOptions(true, false, true, 0, System.Drawing.Text.HotkeyPrefix.None, StringTrimming.None);
                    reportTitleText.Name = "ReportTitleText";
                    reportTitleText.ClientRectangle = new RectangleD((mainPage.Width / 2) - 1.2, 0.71, 2.4, 0.39);
                    reportTitleText.HorAlignment = StiTextHorAlignment.Center;
                    reportTitleText.Text.Value = report.Dictionary.Variables["ReportTitleVariable"].Value;
                    reportTitleText.VertAlignment = StiVertAlignment.Center;
                    reportTitleText.Font = new Font("B Nazanin", 10F, System.Drawing.FontStyle.Regular);
                    reportTitleText.Parent = mainReportTitleBand;
                    reportTitleText.Page = mainPage;
                    mainReportTitleBand.Components.Add(reportTitleText);
                }
                #endregion title

                #region date
                //آیا گزارش دارای تاریخ میباشد یا خیر
                if (currentUserReportSetting.ReportHasDate)
                {
                    report.Dictionary.Variables["ReportDateVariable"].Value = currentDate.ToPersian(Date.DateStringType.Short);

                    //ReportDateText
                    StiText reportDateText = new StiText();
                    reportDateText.Name = "ReportDateText";
                    reportDateText.ClientRectangle = new RectangleD(1.18, 0.63, 0.79, 0.24);
                    reportDateText.Font = new Font("B Nazanin", 11F);
                    reportDateText.Text.Value = ":تاریخ گزارش ";
                    reportDateText.Parent = mainReportTitleBand;
                    reportDateText.Page = mainPage;
                    mainReportTitleBand.Components.Add(reportDateText);
                    //ReportDateVariableText
                    StiText reportDateVariableText = new StiText();
                    reportDateVariableText.Name = "ReportDateVariableText";
                    reportDateVariableText.ClientRectangle = new RectangleD(0.08, 0.63, 1.02, 0.24);
                    reportDateVariableText.HorAlignment = StiTextHorAlignment.Center;
                    reportDateVariableText.VertAlignment = StiVertAlignment.Center;
                    reportDateVariableText.Font = new Font("B Nazanin", 10F, System.Drawing.FontStyle.Bold);
                    reportDateVariableText.Parent = mainReportTitleBand;
                    reportDateVariableText.Page = mainPage;
                    mainReportTitleBand.Components.Add(reportDateVariableText);

                    reportDateVariableText.Text.Value = report.Dictionary.Variables["ReportDateVariable"].Value;

                }
                #endregion date

                #region time
                //آیا گزارش دارای زمان میباشد یا خیر
                if (currentUserReportSetting.ReportHasTime)
                {
                    report.Dictionary.Variables["ReportTimeVariable"].Value = Date.GetTime(currentDate);

                    //ReportTimeText
                    StiText reportTimeText = new StiText();
                    reportTimeText.Name = "ReportTimeText";
                    reportTimeText.ClientRectangle = new RectangleD(1.1, 0.87, 0.79, 0.24);
                    reportTimeText.Font = new Font("B Nazanin", 11F);
                    reportTimeText.Text.Value = ":ساعت گزارش ";
                    reportTimeText.Parent = mainReportTitleBand;
                    reportTimeText.Page = mainPage;
                    mainReportTitleBand.Components.Add(reportTimeText);
                    //ReportTimeVariableText
                    StiText reportTimeVariableText = new StiText();
                    reportTimeVariableText.Name = "ReportTimeVariableText";
                    reportTimeVariableText.ClientRectangle = new RectangleD(0.08, 0.87, 1.02, 0.24);
                    reportTimeVariableText.HorAlignment = StiTextHorAlignment.Center;
                    reportTimeVariableText.VertAlignment = StiVertAlignment.Center;
                    reportTimeVariableText.Font = new Font("B Nazanin", 10F, System.Drawing.FontStyle.Bold);
                    reportTimeVariableText.Parent = mainReportTitleBand;
                    reportTimeVariableText.Page = mainPage;
                    mainReportTitleBand.Components.Add(reportTimeVariableText);

                    reportTimeVariableText.Text.Value = report.Dictionary.Variables["ReportTimeVariable"].Value;
                }

                #endregion time

                #region logo
                //آیا گزارش دارای لوگوی مخابرات میباشد یا خیر
                if (currentUserReportSetting.ReportHasLogo)
                {
                    //TelecomImage
                    StiImage telecomImage = new StiImage();
                    telecomImage.Name = "TelecomImage";
                    telecomImage.ClientRectangle = new RectangleD((mainPage.Width / 2) - 0.25, 0.24, 0.5, 0.47);
                    using (System.IO.Stream stream = CRM.Application.App.GetResourceStream(Helper.MakePackUri("/Images/logo.png")).Stream)
                    {
                        telecomImage.Image = System.Drawing.Image.FromStream(stream);
                    }
                    telecomImage.Stretch = true;
                    telecomImage.Parent = mainReportTitleBand;
                    telecomImage.Page = mainPage;
                    mainReportTitleBand.Components.Add(telecomImage);
                }

                #endregion logo

                //MainHeaderBand
                StiHeaderBand mainHeaderBand = new Stimulsoft.Report.Components.StiHeaderBand();
                mainHeaderBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.78, 7.49, 0.24);
                mainHeaderBand.Name = "MainHeaderBand";
                mainHeaderBand.Border = new Stimulsoft.Base.Drawing.StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
                mainHeaderBand.Parent = mainPage;
                mainHeaderBand.Page = mainPage;

                //MainDataBand
                StiDataBand mainDataBand = new Stimulsoft.Report.Components.StiDataBand();
                mainDataBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 2.02, 7.49, 0.24);
                mainDataBand.KeepGroupTogether = true;
                mainDataBand.Name = "MainDataBand";
                mainDataBand.DataSourceName = "Result";
                mainDataBand.Page = mainPage;
                mainDataBand.Parent = mainPage;


                //StiHeaderBand mainFooterBand = new Stimulsoft.Report.Components.StiHeaderBand();
                //mainFooterBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.78, 7.49, 0.24);
                //mainFooterBand.Name = "MainFooterBand";
                //mainFooterBand.Border = new Stimulsoft.Base.Drawing.StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
                //mainFooterBand.Parent = mainPage;
                //mainFooterBand.Page = mainPage;

                #region footer
                //آیا گزارش دارای فوتر میباشد یا خیر
                //MainPageFooter
                StiPageFooterBand mainPageFooterBand = new StiPageFooterBand();

                if (currentUserReportSetting.ReportHasPageFooter)
                {
                    mainPageFooterBand.Name = "MainPageFooterBand";
                    mainPageFooterBand.ClientRectangle = new RectangleD(0, 10.6, 7.49, 0.31);
                    mainPageFooterBand.Page = mainPage;
                    mainPageFooterBand.Parent = mainPage;

                    //PageFooterText
                    StiText pageFooterText = new StiText();
                    pageFooterText.Name = "PageFooterText";
                    pageFooterText.ClientRectangle = new RectangleD(2.13, 0.04, 3.39, 0.24);
                    pageFooterText.HorAlignment = StiTextHorAlignment.Center;
                    pageFooterText.VertAlignment = StiVertAlignment.Center;
                    pageFooterText.Font = new Font("B Nazanin", 11F);
                    pageFooterText.Parent = mainPageFooterBand;
                    pageFooterText.Page = mainPage;
                    pageFooterText.Text.Value = "صفحه {PageNumber} از {TotalPageCount} صفحه";
                    mainPageFooterBand.Components.Add(pageFooterText);
                }

                #endregion footer

                //آیا گزارش مجموع رکوردها را نمایش دهد یا خیر
                StiGroupHeaderBand totalRecordsGroupHeaderBand = new StiGroupHeaderBand();
                StiGroupFooterBand totalRecordsGroupFooterBand = new StiGroupFooterBand();

                // برای نمایش آمار وقتی ستونی انتخاب نشده است
                StiReportSummaryBand reportSummaryBand = new StiReportSummaryBand();
                reportSummaryBand.Height = 0.5;
                reportSummaryBand.Name = "reportSummaryBand";
                reportSummaryBand.Enabled = true;
                reportSummaryBand.Page = mainPage;
                reportSummaryBand.Parent = mainPage;

                List<DataColumn> dataColumns = new List<DataColumn>();
                dataGridSelectedIndexs.OrderByDescending(ti => ti.Index).ToList().ForEach(ti => 
                {
                    dataColumns.Add(dataSet.Tables[0].Columns.Cast<DataColumn>().Where(t => t.ColumnName == ti.bindingPath).SingleOrDefault());

                });
               // List<DataColumn> dataColumns = dataSet.Tables[0].Columns.Cast<DataColumn>().Where(t=>dataGridSelectedIndexs.Select(t2=>t2.bindingPath).Contains(t.ColumnName)).ToList();
                if (dataColumns.Count > 0)
                {
                    double pos = 0;
                    double columnWidth = (mainPage.Width - 1) / dataColumns.Count;
                    int nameIndex = 1;
             

                    StiText hText = new StiText();
                    StiText dataText = new StiText();
                    foreach (DataColumn dataColumn in dataColumns)
                    {


                        //Create text on header
                        hText = new StiText(new RectangleD(pos, 0, columnWidth, 0.24));
                        hText.HorAlignment = StiTextHorAlignment.Center;
                        hText.VertAlignment = StiVertAlignment.Center;
                        hText.Interaction.SortingEnabled = true;
                        hText.Font = new Font("B Nazanin", 10F);
                        hText.Interaction.SortingColumn = "MainDataBand." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dataColumn.ColumnName);
                        hText.Text.Value = dataColumn.Caption;
                        hText.Name = "HeaderText" + nameIndex.ToString();
                        hText.Border.Side = StiBorderSides.All;
                        mainHeaderBand.Components.Add(hText);


                       

                        //StiText 
                        dataText = new StiText(new RectangleD(pos, 0, columnWidth, 0.24));
                        dataText.HorAlignment = StiTextHorAlignment.Center;
                        dataText.VertAlignment = StiVertAlignment.Center;
                        dataText.Font = new Font("B Nazanin", 10F);
                        dataText.Text = "{Result." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dataColumn.ColumnName) + "}";
                        dataText.Name = "DataText" + nameIndex.ToString();
                        dataText.Border.Side = StiBorderSides.All;
                        dataText.Interaction.SortingEnabled = true;
                        mainDataBand.Components.Add(dataText);


                        //Add sum column

                        if (_sumColumn != null && _sumColumn.Count > 0)
                        {
                            if (_sumColumn.Select(t => t.bindingPath).Contains(dataColumn.ColumnName))
                            {

                                StiText sumText = new StiText(new RectangleD(pos, 0, columnWidth, 0.24));
                                sumText.Name = "sumText" + nameIndex; ;
                                sumText.Brush = new StiSolidBrush(System.Drawing.Color.LightSkyBlue);
                                sumText.Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 0.5, StiPenStyle.Solid);
                                sumText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
                                sumText.VertAlignment = StiVertAlignment.Center;
                                sumText.Type = StiSystemTextType.Totals;
                                sumText.Font = new Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold);
                                sumText.Text = "{Sum(MainDataBand,Result." + dataColumn.ColumnName + ")}";
                                reportSummaryBand.Components.Add(sumText);

                            }
                        }



                        pos = pos + columnWidth;
                        nameIndex++;
                    }

                    #region line
                    //Create text on header
                    hText = new StiText(new RectangleD(pos, 0, 1, 0.24));
                    hText.Text.Value = "ردیف";
                    hText.HorAlignment = StiTextHorAlignment.Center;
                    hText.VertAlignment = StiVertAlignment.Center;
                    hText.Font = new Font("B Nazanin", 10F);
                    hText.Name = "LineText" + nameIndex.ToString();
                    hText.Border.Side = StiBorderSides.All;
                    mainHeaderBand.Components.Add(hText);

                    dataText = new StiText(new RectangleD(pos, 0, 1, 0.24));
                    dataText.HorAlignment = StiTextHorAlignment.Center;
                    dataText.VertAlignment = StiVertAlignment.Center;
                    dataText.Text = "{Line}";
                    dataText.Border.Side = StiBorderSides.All;
                    dataText.Interaction.SortingEnabled = true;
                    mainDataBand.Components.Add(dataText);

                    #endregion line
                }
                else
                {
                    //#region ReportSummery


                    //reportSummaryBand.Height = 0.5;
                    //reportSummaryBand.Name = "reportSummaryBand";
                    //reportSummaryBand.Enabled = true;
                    //StiText pageCountText = new StiText(new RectangleD(0, 0, mainPage.Width, 0.5));
                    //pageCountText.Name = "CountText2";
                    //pageCountText.Brush = new StiSolidBrush(System.Drawing.Color.FromArgb(255, 206, 221, 176));
                    //pageCountText.Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
                    //pageCountText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.98, 0.10, 1.5, 0.24);
                    //pageCountText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
                    //pageCountText.VertAlignment = StiVertAlignment.Center;
                    //pageCountText.Font = new Font("Nazanin", 13F, System.Drawing.FontStyle.Bold);
                    //pageCountText.Height = 0.5;
                    //pageCountText.Text = string.Format("{0} : تعداد کل", dataSet.Tables[0].Rows.Count.ToString());
                    //reportSummaryBand.Components.Add(pageCountText);

                    //#endregion

                }


                #region condition

                // TotalRecordsGroupHeaderBand
                totalRecordsGroupHeaderBand.Name = "TotalRecordsGroupHeaderBand";
                totalRecordsGroupHeaderBand.Guid = null;
                totalRecordsGroupHeaderBand.Interaction = null;
                totalRecordsGroupHeaderBand.Page = mainPage;
                totalRecordsGroupHeaderBand.Parent = mainPage;




                if (groupingColumn != null && groupingColumn.Count > 0)
                {
                    totalRecordsGroupHeaderBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 2.42, 7.49, 0.31);
                    string conditionString = string.Empty;
                    byte count = default(byte);
                    groupingColumn.ForEach(t =>
                    {
                        conditionString += "{Result." + t.bindingPath + "},";

                        #region ConditionTextBox
                        StiText conditionText = new StiText(new RectangleD(mainPage.Width - (1.5 + (count * 1.5)), 0, 1.5, 0.31));
                        conditionText.Name = "conditionText" + count;
                        conditionText.Brush = new StiSolidBrush(System.Drawing.Color.FromArgb(255, 206, 221, 176));
                        conditionText.Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
                        conditionText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
                        conditionText.VertAlignment = StiVertAlignment.Center;
                        conditionText.Font = new Font("Nazanin", 13F, System.Drawing.FontStyle.Bold);
                        conditionText.Text = "{Result." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(t.bindingPath) + "}";
                        totalRecordsGroupHeaderBand.Components.Add(conditionText);
                        #endregion ConditionTextBox

                        count++;
                    });

                    conditionString = conditionString.Remove(conditionString.Length - 1, 1);
                    totalRecordsGroupHeaderBand.Condition = new StiGroupConditionExpression(conditionString);
                }

                #endregion condition

                #region Sum

                //if (_sumColumn != null && _sumColumn.Count > 0)
                //{
                //    reportSummaryBand.Height = 0.5;
                //    reportSummaryBand.Name = "reportSummaryBand";
                //    reportSummaryBand.Enabled = true;

                //    byte count = default(byte);
                //    _sumColumn.ForEach(t =>
                //    {

                //        #region ConditionTextBox


                //        StiText sumText = new StiText(new RectangleD(0, 0, mainPage.Width, 0.5));
                //        sumText.Name = "sumText" + count; ;
                //        sumText.Brush = new StiSolidBrush(System.Drawing.Color.FromArgb(255, 206, 221, 176));
                //        sumText.Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
                //        sumText.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.98, 0.10, 1.5, 0.24);
                //        sumText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
                //        sumText.VertAlignment = StiVertAlignment.Center;
                //        sumText.Font = new Font("Nazanin", 13F, System.Drawing.FontStyle.Bold);
                //        sumText.Height = 0.5;
                //        sumText.Text = string.Format("جمع {}", t.Header, dataSet.Tables[0].Rows.Count.ToString());
                //        reportSummaryBand.Components.Add(sumText);


                //        #endregion sumTextBox

                //        count++;
                //    });

                //}

                #endregion

                #region count

                if (currentUserReportSetting.ReportSumRecordsQuantity)
                {
                    // TotalRecordsGroupFooterBand
                    totalRecordsGroupFooterBand.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 3.77, 7.49, 0.31);
                    totalRecordsGroupFooterBand.Name = "TotalRecordsGroupFooterBand";
                    totalRecordsGroupFooterBand.Page = mainPage;
                    totalRecordsGroupFooterBand.Parent = mainPage;

                    // CountText
                    StiText countText = new StiText(new RectangleD(mainPage.Width - 1.5, 0, 1.5, 0.25));
                    countText.Name = "CountText";
                    countText.Brush = new StiSolidBrush(System.Drawing.Color.FromArgb(255, 206, 221, 176));
                    countText.Border = new StiBorder(StiBorderSides.All, System.Drawing.Color.Black, 1, StiPenStyle.Solid);
                    countText.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
                    countText.VertAlignment = StiVertAlignment.Center;
                    countText.Font = new Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold);
                    countText.Page = mainPage;
                    countText.Parent = totalRecordsGroupFooterBand;
                    countText.Text.Value = "{Count()} : تعداد کل";
                    totalRecordsGroupFooterBand.Components.Add(countText);
                }

                #endregion count

                #region headerSetting
                //تنظیمات هدر
                foreach (StiText headerBandChild in mainHeaderBand.Components.OfType<StiText>())
                {
                    headerBandChild.Brush = headerBackground;
                    headerBandChild.Border = headerBorder;
                    headerBandChild.Font = currentUserReportSetting.HeaderFont;
                    headerBandChild.TextBrush = headerForeground;
                    if (currentUserReportSetting.HeaderHasWordWrap)
                    {
                        headerBandChild.WordWrap = true;
                        headerBandChild.CanGrow = true;
                        headerBandChild.GrowToHeight = true;
                    }
                    headerBandChild.CanShrink = true;
                }

                #endregion headerSetting

                #region 
                //تنظیمات متن
                foreach (StiText dataBandChild in mainDataBand.Components.OfType<StiText>())
                {
                    dataBandChild.Brush = textBackground;
                    dataBandChild.Border = textBorder;
                    dataBandChild.Font = currentUserReportSetting.TextFont;
                    dataBandChild.TextBrush = textForeground;
                    if (currentUserReportSetting.TextHasWordWrap)
                    {
                        dataBandChild.WordWrap = true;
                        dataBandChild.CanGrow = true;
                        dataBandChild.GrowToHeight = true;
                    }
                    dataBandChild.CanShrink = true;
                }
                #endregion

                report.Dictionary.DataSources.Add(userSource);
                mainPage.Components.Clear();
                if (dataColumns.Count() == 0)
                {
                    mainHeaderBand.Height = 0;
                    mainDataBand.Height = 0;
                    //mainFooterBand.Height = 0;
                }
                mainPage.Components.AddRange(new StiComponent[] { mainReportTitleBand, totalRecordsGroupHeaderBand, mainHeaderBand, mainDataBand, totalRecordsGroupFooterBand, reportSummaryBand, mainPageFooterBand });
                report.CacheAllData = true;
                report.RegData(dataSet.Tables[0]);
                report.Dictionary.Synchronize();
                report.Pages.Clear();
                report.Pages.Add(mainPage);
                
                report.Render();
                //report.DesignWithWpf();
                //آیا گزارش دارای پیش نمایش میباشد یا به طور مستقیم پرینتر ارسال میشود
                if (currentUserReportSetting.PrintWithPreview)
                {
                    report.Show(true);
                }
                else
                {
                    report.Print(true);
                }
            }
        }
        public static List<DataGridSelectedIndex> AddToSelectedIndex(List<DataGridSelectedIndex> dataGridSelectedIndexs, System.Windows.Controls.DataGridColumn dataGridColumn, string content)
        {
            if (dataGridColumn.ClipboardContentBinding != null)
            {
                string bindingPath = ((System.Windows.Data.Binding)(dataGridColumn.ClipboardContentBinding)).Path.Path.ToString();
                dataGridSelectedIndexs.Add(new DataGridSelectedIndex { Index = dataGridColumn.DisplayIndex, Header = content, bindingPath = bindingPath });
            }
            return dataGridSelectedIndexs;
        }

        public static List<DataGridSelectedIndex> RemoveFromSelectedIndex(List<DataGridSelectedIndex> dataGridSelectedIndexs, System.Windows.Controls.DataGridColumn dataGridColumn, string p)
        {
            if (dataGridSelectedIndexs.Any(t => t.Index == dataGridColumn.DisplayIndex))
            {
                dataGridSelectedIndexs.Remove(dataGridSelectedIndexs.Where(t => t.Index == dataGridColumn.DisplayIndex).SingleOrDefault());
            }
            return dataGridSelectedIndexs;
        }


        public static bool SaveDataGridColumn(List<DataGridSelectedIndex> dataGridSelectedIndexs, string formName, string dataGridName, System.Collections.ObjectModel.ObservableCollection<DataGridColumn> DGColumns)
        {
            bool isSave = false;

            dataGridSelectedIndexs.ToList().ForEach(ti =>
            {
                ti.Index = DGColumns.Where(t => t.ClipboardContentBinding != null && ((System.Windows.Data.Binding)(t.ClipboardContentBinding)).Path.Path.ToString() == ti.bindingPath).SingleOrDefault().DisplayIndex;
            });
            try
            {

                DataGridColumnConfig dataGridColumnConfig = DataGridColumnConfigDB.GetDataGridColumnConfig(formName, dataGridName);

                if (dataGridColumnConfig == null)
                {
                    dataGridColumnConfig = new DataGridColumnConfig();

                    dataGridColumnConfig.UserID = DB.currentUser.ID;
                    dataGridColumnConfig.FormName = formName;
                    dataGridColumnConfig.DataGridName = dataGridName;

                    Data.Schema.DataGridColumns dataGridColumns = new Data.Schema.DataGridColumns();
                    dataGridColumns.Columns = dataGridSelectedIndexs.Select(t => t.Header).ToList();
                    dataGridColumnConfig.SelectedColumns = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DataGridColumns>(dataGridColumns, true));
                    dataGridColumnConfig.Detach();
                    DB.Save(dataGridColumnConfig, true);


                    Data.Schema.DataGridColumnsDisplayIndexs dataGridColumnsDisplayIndexs = new Data.Schema.DataGridColumnsDisplayIndexs();
                    List<Data.Schema.DataGridColumnsDisplayIndex> dataGridColumnsDisplayIndex = new List<Data.Schema.DataGridColumnsDisplayIndex>();
                    DGColumns.ToList().ForEach(t =>
                    {
                        dataGridColumnsDisplayIndex.Add(new Data.Schema.DataGridColumnsDisplayIndex() { DisplayIndex= t.DisplayIndex, Header=  ((System.Windows.Data.Binding)(t.ClipboardContentBinding)).Path.Path.ToString()});
                     
                    });
                    dataGridColumnsDisplayIndexs.DGCDisplayIndex = dataGridColumnsDisplayIndex;
                    dataGridColumnConfig.DisplayIndexColumns = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DataGridColumnsDisplayIndexs>(dataGridColumnsDisplayIndexs, true));
                    dataGridColumnConfig.Detach();
                    DB.Save(dataGridColumnConfig, true);



                }
                else
                {
                    Data.Schema.DataGridColumns dataGridColumns = new Data.Schema.DataGridColumns();
                    dataGridColumns.Columns = dataGridSelectedIndexs.Select(t => t.Header).ToList();
                    dataGridColumnConfig.SelectedColumns = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DataGridColumns>(dataGridColumns, true));
                    dataGridColumnConfig.Detach();
                    DB.Save(dataGridColumnConfig, false);

                    Data.Schema.DataGridColumnsDisplayIndexs dataGridColumnsDisplayIndexs = new Data.Schema.DataGridColumnsDisplayIndexs();
                    List<Data.Schema.DataGridColumnsDisplayIndex> dataGridColumnsDisplayIndex = new List<Data.Schema.DataGridColumnsDisplayIndex>();
                    DGColumns.ToList().ForEach(t =>
                    {
                        dataGridColumnsDisplayIndex.Add(new Data.Schema.DataGridColumnsDisplayIndex() { DisplayIndex = t.DisplayIndex, Header = (t.ClipboardContentBinding != null ? ((System.Windows.Data.Binding)(t.ClipboardContentBinding)).Path.Path.ToString() : string.Empty) });

                    });
                    dataGridColumnsDisplayIndexs.DGCDisplayIndex = dataGridColumnsDisplayIndex;
                    dataGridColumnConfig.DisplayIndexColumns = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DataGridColumnsDisplayIndexs>(dataGridColumnsDisplayIndexs, true));
                    dataGridColumnConfig.Detach();
                    DB.Save(dataGridColumnConfig, false);
                }

                DB.CurrentUser.DataGridColumnConfig = DataGridColumnConfigDB.GetDataGridColumnConfig(DB.CurrentUser.ID);
                isSave = true;

            }
            catch
            {
                isSave = false;
            }

            return isSave;
        }

        internal static void CheckingGridColumn(object content, string name)
        {
            UIElement container = content as UIElement;
            DataGridColumnConfig dataGridColumnConfig = DB.CurrentUser.DataGridColumnConfig.Where(t => t.FormName == name).SingleOrDefault();
            if (dataGridColumnConfig != null)
            {
                DataGrid dataGrid = Helper.FindVisualChildByName<DataGrid>(container, dataGridColumnConfig.DataGridName);
                if (dataGrid != null)
                {

                    if (dataGridColumnConfig.SelectedColumns != null)
                    {
                        CRM.Data.Schema.DataGridColumns columns = LogSchemaUtility.Deserialize<CRM.Data.Schema.DataGridColumns>(dataGridColumnConfig.SelectedColumns.ToString());
                        // check Column
                        if (columns.Columns.Count() > 0)
                        {
                            List<CheckBox> checkBoxs = Helper.FindVisualChildren<CheckBox>(dataGrid).ToList();
                            checkBoxs.Where(t => columns.Columns.Contains(t.Content)).ToList().ForEach(t => t.IsChecked = true);
                        }
                    }

                    // set display index

                    if (dataGridColumnConfig.DisplayIndexColumns != null)
                    {
                        CRM.Data.Schema.DataGridColumnsDisplayIndexs columnsDisplayIndex = LogSchemaUtility.Deserialize<CRM.Data.Schema.DataGridColumnsDisplayIndexs>(dataGridColumnConfig.DisplayIndexColumns.ToString());
                        columnsDisplayIndex.DGCDisplayIndex.ForEach(t => 
                        {
                            if (dataGrid.Columns.Any(t2 => t2.ClipboardContentBinding != null && ((System.Windows.Data.Binding)(t2.ClipboardContentBinding)).Path.Path.ToString() == t.Header))
                            {
                                dataGrid.Columns.Where(t2 => t2.ClipboardContentBinding != null && ((System.Windows.Data.Binding)(t2.ClipboardContentBinding)).Path.Path.ToString() == t.Header).Take(1).SingleOrDefault().DisplayIndex = t.DisplayIndex;
                            }
                        });
                        
                    }



                    //columnsDisplayIndex
                }
            }
        }

    }
}
