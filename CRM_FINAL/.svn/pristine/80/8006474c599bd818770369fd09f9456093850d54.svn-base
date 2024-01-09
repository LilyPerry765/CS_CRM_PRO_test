using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Stimulsoft.Report.Wpf;
using Stimulsoft.Report.Dictionary;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using Enterprise;
using Stimulsoft.Base;
using Stimulsoft.Report;
using System.Reflection;


namespace CRM.Application.Reports.Viewer
{
    public partial class ReportViewerForm : Window
    {
        public ReportViewerForm(StiReport stiReport, bool IsRenderd = false)
        {

            InitializeComponent();

            if (stiReport != null)
            {
                SetViwer();
                stiReport.CompileStandaloneReport("", StiStandaloneReportType.Show);// Compile();
                this.Background = Stimulsoft.Report.Wpf.StiThemesHelper.GetBrush("WorkBackgroundBrush", this, null);
                viewer.Report = stiReport;
                try
                {
                    if (!IsRenderd)
                    {
                        stiReport.Render(true);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex, "خطا در متد رندر گزارش ساز");
                    throw;
                }
            }
        }

        private void SetViwer()
        {
            viewer.ShowClose = false;
            viewer.ShowPageNew = false;
            viewer.ShowReportOpen = false;
            viewer.ShowToolEditor = false;
            viewer.ShowThumbnails = false;
            viewer.ToolEditorInRibbonVisibility = System.Windows.Visibility.Collapsed;
            //viewer.ReportSendEMailMetafileVisibility = System.Windows.Visibility.Collapsed;
            viewer.ShowPageDesign = false;
            viewer.ShowPageDelete = false;
            viewer.ShowReportSendEMail = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Activate();
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            viewer.Report.Show();
        }
    }
}

