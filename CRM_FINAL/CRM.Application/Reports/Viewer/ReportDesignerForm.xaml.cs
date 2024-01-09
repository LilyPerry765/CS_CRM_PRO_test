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
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.WpfDesign;
using Stimulsoft.Report.Viewer;
using Stimulsoft.Report.Units;
using Stimulsoft.Base.Services;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using CRM.Data;
using System.IO;

namespace CRM.Application.Reports.Viewer
{
    /// <summary>
    /// Interaction logic for ReportDesignerForm.xaml
    /// </summary>
    public partial class ReportDesignerForm : Window
    {
        #region Properties And Fields

        StiReport stiReport = new StiReport();

        ReportTemplate CurrentReport
        {
            get;
            set;
        }

        StiDataSourcesCollection CurrentStiDataSource
        {
            get;
            set;
        }

        #endregion  Properties And Fields

        #region Constructor
        public ReportDesignerForm()
        {
            InitializeComponent();
        }

        public ReportDesignerForm(bool isNew,int reportID) : this()
        {
           
            if (!isNew)
            {
                CurrentReport = ReportDB.GetReportTemplateByID(reportID);
                if (CurrentReport != null)
                {
                    CurrentReport.Template = ReportDB.GetReportFile(reportID);
                }
            }
            else
            {
                CurrentReport = new ReportTemplate();
            }
            InitiateReportDesigner(isNew);
        }

        #endregion  Constructor

        #region Mehtods

        private byte[] DeCompress(byte[] CompressReport)
        {
            MemoryStream ms = new MemoryStream(CompressReport);
            MemoryStream ms2 = new MemoryStream();
            GZipStream gZip = new GZipStream(ms, CompressionMode.Decompress, true);
            StreamReader sr = new StreamReader(gZip);
            string str = sr.ReadToEnd();
            str = str + ">";
            return ASCIIEncoding.UTF8.GetBytes(str);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            ReportDesigner.CheckClosing(new System.ComponentModel.CancelEventArgs(true));
        }

        private void InitiateReportDesigner(bool isNew)
        {
            switch (isNew)
            {
                case false:
                    StiOptions.Engine.HideMessages = true;
                    stiReport = new StiReport();
                    ReportDesigner.Report = stiReport;
                    ReportDesigner.ShowPanelMessages = false;
                    stiReport.Compile();
                    if(CurrentReport!=null)                    
                    try
                    {
                        stiReport.Load(CurrentReport.Template.ToArray());
                    }
                    catch (Exception)
                    {
 
                    }
                    break;
                case true:
                    StiOptions.Engine.HideMessages = true;
                    stiReport = new StiReport();
                    ReportDesigner.Report = stiReport;
                    ReportDesigner.ShowPanelMessages = false;
                    stiReport.Compile();
                    break;
            }
        }
        #endregion  Mehtods

    }
}
