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
    /// Interaction logic for FormDesignerForm.xaml
    /// </summary>
    public partial class FormDesignerForm : Window
    {
        
        #region Properties And Fields

        StiReport stiReport = new StiReport();

        FormTemplate CurrentForm
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

        public FormDesignerForm(bool isNew, int formID)
        {
            InitializeComponent();
            if (!isNew)
            {
                CurrentForm = ReportDB.GetFormTemplateByID(formID);
                CurrentForm.Template = ReportDB.GetFormFile(formID);
            }
            else
            {
                CurrentForm= new FormTemplate();
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
            CRM.Application.Views.FormTemplateForm.temp = "";
            CRM.Application.Views.FormTemplateForm.RequestTypeID =0;
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
                    if(CurrentForm!=null)                    
                    try
                    {
                        stiReport.Load(CurrentForm.Template.ToArray());
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
